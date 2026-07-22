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

using Neo.Core.Extensions;
using Neo.Core.Serialization;
using System;
using System.IO;

namespace Neo.Core.Net.Message
{
    public class ProtocolMessage : INeoSerializable
    {
        public ProtocolMessageFlags Flags { get; private set; }

        public ProtocolMessageCommand Command { get; private set; }

        public INeoSerializable? Message => _protocolMessage;

        public bool Compressed => Flags.HasFlag(ProtocolMessageFlags.Compressed);

        public int Size =>
            sizeof(ProtocolMessageFlags) +
            sizeof(ProtocolMessageCommand) +
            _compressedPayloadBytes.GetSerializedSize();


        private INeoSerializable? _protocolMessage;
        private ReadOnlyMemory<byte> _compressedPayloadBytes;

        public void Deserialize(Stream reader)
        {
            Flags = reader.Read<ProtocolMessageFlags>();
            Command = reader.Read<ProtocolMessageCommand>();
            _compressedPayloadBytes = reader.ReadDynamic<byte>();

            if (_compressedPayloadBytes.Length == 0)
                return;

            var decompressedBytes = Flags.HasFlag(ProtocolMessageFlags.Compressed) ?
                _compressedPayloadBytes.Span.ToLz4Decompress() :
                _compressedPayloadBytes.Span;

            _protocolMessage = Command switch
            {
                ProtocolMessageCommand.Version => decompressedBytes.AsSerializable<VersionMessage>(),
                _ => default,
            };
        }

        public void Serialize(Stream writer)
        {
            writer.Write(Flags);
            writer.Write(Command);
            writer.Write<byte>(_compressedPayloadBytes.Span);
        }
    }
}
