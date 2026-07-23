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
using Neo.Core.Factory;
using Neo.Core.Serialization;
using System;
using System.IO;

namespace Neo.Core.Net.Message
{
    /// <summary>
    /// Payload for Ping and Pong (Neo <c>PingPayload</c>).
    /// </summary>
    public class PingMessage : INeoSerializable
    {
        public uint LastBlockIndex { get; private set; }

        public uint Timestamp { get; private set; }

        public uint Nonce { get; private set; }

        public int Size =>
            sizeof(uint) + // LastBlockIndex
            sizeof(uint) + // Timestamp
            sizeof(uint);  // Nonce

        public static PingMessage Create(uint lastBlockIndex) =>
            Create(lastBlockIndex, RandomNumberFactory.NextUInt32());

        public static PingMessage Create(uint lastBlockIndex, uint nonce) =>
            new()
            {
                LastBlockIndex = lastBlockIndex,
                Timestamp = CoreUtilities.ToUnixTimeSeconds(DateTime.UtcNow),
                Nonce = nonce,
            };

        public void Deserialize(Stream reader)
        {
            LastBlockIndex = reader.Read<uint>();
            Timestamp = reader.Read<uint>();
            Nonce = reader.Read<uint>();
        }

        public void Serialize(Stream writer)
        {
            writer.Write(LastBlockIndex);
            writer.Write(Timestamp);
            writer.Write(Nonce);
        }
    }
}
