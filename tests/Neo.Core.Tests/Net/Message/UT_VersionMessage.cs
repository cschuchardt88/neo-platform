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
using Neo.Core.Net;
using Neo.Core.Net.Message;
using System;

namespace Neo.Core.Tests.Net.Message
{
    [TestClass]
    public class UT_VersionMessage
    {
        [TestMethod]
        public void TestSize()
        {
            var actualVersionMessage = new VersionMessage();

            Assert.AreEqual(actualVersionMessage.ToArray().Length, actualVersionMessage.Size);
        }

        [TestMethod]
        public void TestSizeWithCapabilities()
        {
            var version = VersionMessage.Create(
                network: 0x334F454E,
                nonce: 1u,
                new ServerCapabilityMessage(10333),
                new FullNodeCapabilityMessage(42),
                new ArchivalNodeCapabilityMessage());

            Assert.AreEqual(version.ToArray().Length, version.Size);
        }

        [TestMethod]
        public void TestCreate()
        {
            var expectedVersionMessage = new VersionMessage();

            var actualVersionMessage = VersionMessage.Create(
                expectedVersionMessage.Network,
                expectedVersionMessage.Nonce,
                expectedVersionMessage.Capabilities);

            Assert.AreEqual(expectedVersionMessage.Network, actualVersionMessage.Network);
            Assert.AreEqual(expectedVersionMessage.Version, actualVersionMessage.Version);
            Assert.AreEqual(expectedVersionMessage.Timestamp, actualVersionMessage.Timestamp);
            Assert.AreEqual(expectedVersionMessage.Nonce, actualVersionMessage.Nonce);
            Assert.AreEqual(expectedVersionMessage.AllowCompression, actualVersionMessage.AllowCompression);
            Assert.AreEqual(expectedVersionMessage.UserAgent, actualVersionMessage.UserAgent);
            Assert.AreSequenceEqual(expectedVersionMessage.Capabilities, actualVersionMessage.Capabilities);
        }

        [TestMethod]
        public void TestSerializeAndDeserialize()
        {
            var expectedVersionMessage = new VersionMessage();
            var expectedVersionBytes = expectedVersionMessage.ToArray();

            var actualVersionMessage = expectedVersionBytes.AsSerializable<VersionMessage>();

            Assert.AreEqual(expectedVersionMessage.Network, actualVersionMessage.Network);
            Assert.AreEqual(expectedVersionMessage.Version, actualVersionMessage.Version);
            Assert.AreEqual(expectedVersionMessage.Timestamp, actualVersionMessage.Timestamp);
            Assert.AreEqual(expectedVersionMessage.Nonce, actualVersionMessage.Nonce);
            Assert.AreEqual(expectedVersionMessage.AllowCompression, actualVersionMessage.AllowCompression);
            Assert.AreEqual(expectedVersionMessage.UserAgent, actualVersionMessage.UserAgent);
            Assert.AreSequenceEqual(expectedVersionMessage.Capabilities, actualVersionMessage.Capabilities);
        }

        [TestMethod]
        public void TestSerializeAndDeserializeWithCapabilities()
        {
            var expected = VersionMessage.Create(
                network: 0x334F454E,
                nonce: 99u,
                new ServerCapabilityMessage(10333),
                new FullNodeCapabilityMessage(1_234_567u),
                new ArchivalNodeCapabilityMessage());

            var actual = expected.ToArray().AsSerializable<VersionMessage>();

            Assert.AreEqual(expected.Network, actual.Network);
            Assert.AreEqual(expected.Nonce, actual.Nonce);
            Assert.AreEqual(expected.UserAgent, actual.UserAgent);
            Assert.IsTrue(actual.AllowCompression);
            Assert.HasCount(3, actual.Capabilities);

            Assert.IsInstanceOfType<ServerCapabilityMessage>(actual.Capabilities[0]);
            Assert.AreEqual((ushort)10333, ((ServerCapabilityMessage)actual.Capabilities[0]).Port);

            Assert.IsInstanceOfType<FullNodeCapabilityMessage>(actual.Capabilities[1]);
            Assert.AreEqual(1_234_567u, ((FullNodeCapabilityMessage)actual.Capabilities[1]).StartHeight);

            Assert.IsInstanceOfType<ArchivalNodeCapabilityMessage>(actual.Capabilities[2]);
        }

        [TestMethod]
        public void TestDisableCompressionCapability()
        {
            var version = VersionMessage.Create(
                network: 1u,
                nonce: 1u,
                new DisableCompressionCapabilityMessage());

            Assert.IsFalse(version.AllowCompression);

            var actual = version.ToArray().AsSerializable<VersionMessage>();
            Assert.IsFalse(actual.AllowCompression);
        }

        [TestMethod]
        public void TestDuplicateCapabilityTypesThrow()
        {
            var version = VersionMessage.Create(
                network: 1u,
                nonce: 1u,
                new ServerCapabilityMessage(1),
                new ServerCapabilityMessage(2));

            Assert.ThrowsExactly<FormatException>(() =>
                version.ToArray().AsSerializable<VersionMessage>());
        }

        [TestMethod]
        public void TestVersionInsideProtocolMessage()
        {
            var version = VersionMessage.Create(
                network: 0x334F454E,
                nonce: 7u,
                new FullNodeCapabilityMessage(100),
                new ServerCapabilityMessage(10333));

            var frame = ProtocolMessage.Create(ProtocolMessageCommand.Version, version);
            var bytes = frame.ToArray();

            var consumed = ProtocolMessage.TryRead(bytes, out var message);
            Assert.AreEqual(bytes.Length, consumed);
            Assert.IsNotNull(message);
            Assert.IsInstanceOfType<VersionMessage>(message.Message);

            var actual = (VersionMessage)message.Message;
            Assert.AreEqual(version.Network, actual.Network);
            Assert.AreEqual(version.Nonce, actual.Nonce);
            Assert.HasCount(2, actual.Capabilities);
        }
    }
}
