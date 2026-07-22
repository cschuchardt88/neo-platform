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
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Neo.Core.Net
{
    public class NodeServerListener : IAsyncDisposable
    {
        public NodeConnection[] Clients => [.. _connections];

        public ProtocolSettings ProtocolSettings => _protocolSettings;

        public EndPoint LocalEndPoint => _endPoint;

        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        private readonly EndPoint _endPoint;
        private readonly Socket _serverSocket;
        private readonly ProtocolSettings _protocolSettings;

        private readonly Mutex _mutex;

        private readonly CancellationTokenSource _listeningTokenSource = new();
        private readonly CancellationToken _listeningToken;

        private readonly ConcurrentBag<NodeConnection> _connections = [];

        private bool _isActive = false;

        private Task _listeningTask = Task.CompletedTask;

        public NodeServerListener(
            IPEndPoint endPoint,
            ProtocolSettings? protocolSettings = default,
            ILoggerFactory? loggerFactory = default)
        {
            _mutex = new Mutex(false, $"{nameof(NodeServerListener)}-{endPoint}", out var createdNew);

            if (createdNew == false)
            {
                _mutex.Dispose();
                throw new ApplicationException($"Server \'{endPoint}\' is already in use.");
            }

            _endPoint = endPoint;
            _serverSocket = new(_endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _protocolSettings = protocolSettings ?? ProtocolSettings.Default;
            _loggerFactory = _loggerFactory ?? NullLoggerFactory.Instance;
            _logger = _loggerFactory.CreateLogger<NodeServerListener>();
            _listeningToken = _listeningTokenSource.Token;
        }

        public async ValueTask DisposeAsync()
        {
            _listeningTokenSource.Cancel();

            await _listeningTask;

            foreach (var client in _connections)
                await client.DisposeAsync();

            _connections.Clear();
            _serverSocket.Dispose();
            _isActive = false;

            GC.SuppressFinalize(this);
        }

        public void Start()
        {
            Start((int)SocketOptionName.MaxConnections);
        }

        public void Start(int backlog)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(backlog);

            // Already listening.
            if (_isActive)
            {
                return;
            }

            _serverSocket.Bind(_endPoint);

            try
            {
                _serverSocket.Listen(backlog);
                _listeningTask = StartAsync();
            }
            catch (SocketException)
            {
                throw;
            }

            _isActive = true;
        }

        private async Task StartAsync()
        {
            while (true)
            {
                try
                {
                    var clientSocket = await _serverSocket.AcceptAsync(_listeningToken);
                    var clientConnection = new NodeConnection(clientSocket, _loggerFactory);

                    clientConnection.Start();

                    _connections.Add(clientConnection);
                }
                catch (IOException ex) when (_listeningToken.IsCancellationRequested == false)
                {
                    _logger.LogError("{Message}", ex.ToString());
                }
                catch (OperationCanceledException) when (_listeningToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }
    }
}
