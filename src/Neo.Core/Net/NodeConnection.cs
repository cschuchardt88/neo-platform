// BSD 2-Clause License
//
// Copyright (c) 2026, Rapid Loop
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// 1. Redistributions of source code must retain the above copyright notice, this
//    list of conditions and the following disclaimer.
//
// 2. Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Neo.Core.Extensions;
using Neo.Core.Net.Message;
using System;
using System.Buffers;
using System.IO.Pipelines;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Neo.Core.Net
{
    public class NodeConnection : IAsyncDisposable
    {
        public bool Connected => _socket.Connected;

        public EndPoint? RemoteEndPoint => _socket.RemoteEndPoint;

        public EndPoint? LocalEndPoint => _socket.LocalEndPoint;

        private readonly Socket _socket;
        private readonly Pipe _networkPipe;

        private readonly ILogger<NodeConnection> _logger;

        private Task _fillingPipeTask = Task.CompletedTask;
        private Task _readingPipeTask = Task.CompletedTask;

        public NodeConnection(
            Socket socket,
            ILoggerFactory? loggerFactory = default)
        {
            _socket = socket;
            _networkPipe = new Pipe();

            _logger = (loggerFactory ?? NullLoggerFactory.Instance)
                .CreateLogger<NodeConnection>();
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                await _fillingPipeTask;
                await _readingPipeTask;
            }
            catch (Exception)
            {
                _socket.Dispose();
            }

            if (_socket.Connected)
                _socket.Dispose();
        }

        internal void Start()
        {
            try
            {
                _fillingPipeTask = DoFillPipeAsync();
                _readingPipeTask = DoReadPipeAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task DoFillPipeAsync()
        {
            var error = default(Exception);
            var input = _networkPipe.Writer;

            try
            {
                while (_socket.Connected)
                {
                    var buffer = input.GetMemory(_socket.ReceiveBufferSize);
                    var bytesReceived = await _socket.ReceiveAsync(buffer);

                    if (bytesReceived == 0)
                        break;

                    input.Advance(bytesReceived);

                    var result = await input.FlushAsync();

                    if (result.IsCompleted || result.IsCanceled)
                        break;
                }
            }
            catch (Exception ex)
            {
                error = ex;
                _logger.LogError("{Message}", ex.ToString());
            }
            finally
            {
                input.Complete(error);
            }
        }

        private async Task DoReadPipeAsync()
        {
            var unexpectedError = default(Exception);
            var output = _networkPipe.Reader;

            try
            {
                while (_socket.Connected)
                {
                    var readResult = await output.ReadAsync();

                    if (readResult.IsCanceled)
                        break;

                    var buffer = readResult.Buffer;
                    var bufferBytes = buffer.ToArray();

                    var message = bufferBytes.TryCatch(t => t?.AsSerializable<ProtocolMessage>());

                    if (message is null)
                        continue;

                    // TODO: Process Protocol

                    output.AdvanceTo(buffer.Start, buffer.End);

                    if (readResult.IsCompleted)
                        break;
                }
            }
            catch (ObjectDisposedException)
            {

            }
            catch (Exception ex)
            {
                unexpectedError = ex;
                _logger.LogError("{Message}", ex.ToString());
            }
            finally
            {
                if (_socket.Connected)
                    _socket.Disconnect(false);

                output.Complete(unexpectedError);
                _networkPipe.Writer.CancelPendingFlush();
            }
        }
    }
}
