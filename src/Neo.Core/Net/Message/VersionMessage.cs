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
using System.Linq;
using System.Reflection;

namespace Neo.Core.Net.Message
{
    public class VersionMessage : INeoSerializable
    {
        public const int MaxCapabilities = 32;

        private readonly static string s_version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0";

        public uint Network { get; private set; }

        public uint Version { get; private set; } = 0u;

        public uint Timestamp { get; private set; } = CoreUtilities.ToUnixTimeSeconds(DateTime.UtcNow);

        public uint Nonce { get; private set; } = RandomNumberFactory.NextUInt32();

        public bool AllowCompression { get; private set; }

        public string UserAgent { get; private set; } = $"/RapidLoop:{s_version}/";

        public NodeCapabilityMessage[] Capabilities { get; private set; } = [];

        public int Size =>
            sizeof(uint) + // Network
            sizeof(uint) + // Version
            sizeof(uint) + // Timestamp
            sizeof(uint) + // Nonce
            UserAgent.GetSerializedSize() +   // UserAgent
            Capabilities.GetSerializedSize(); // Node Capabilities

        public void Deserialize(Stream reader)
        {
            Network = reader.Read<uint>();
            Version = reader.Read<uint>();
            Timestamp = reader.Read<uint>();
            Nonce = reader.Read<uint>();
            UserAgent = reader.ReadString();
            Capabilities = new NodeCapabilityMessage[reader.ReadCompact<int>()];

            for (var i = 0; i < Capabilities.Length; i++)
                Capabilities[i] = NodeCapabilityMessage.DeserializeFrom(reader);

            if (Capabilities.Distinct().Count() != Capabilities.Length)
                throw new FormatException("Duplicate node capabilities.");

            AllowCompression = Capabilities.Any(static a =>
                a.Type == NodeCapabilityType.DisableCompression);
        }

        public void Serialize(Stream writer)
        {
            writer.Write(Network);
            writer.Write(Version);
            writer.Write(Timestamp);
            writer.Write(Nonce);
            writer.Write(UserAgent);
            writer.WriteObjects(Capabilities);
        }
    }
}
