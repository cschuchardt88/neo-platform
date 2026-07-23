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

namespace Neo.Core.Tests.Net.Message
{
    [TestClass]
    public class UT_PingMessage
    {
        [TestMethod]
        public void TestSize()
        {
            var ping = PingMessage.Create(lastBlockIndex: 100, nonce: 7);
            Assert.AreEqual(12, ping.Size);
            Assert.AreEqual(ping.ToArray().Length, ping.Size);
        }

        [TestMethod]
        public void TestSerializeAndDeserialize()
        {
            var expected = PingMessage.Create(lastBlockIndex: 1_234_567u, nonce: 99u);
            var actual = expected.ToArray().AsSerializable<PingMessage>();

            Assert.AreEqual(expected.LastBlockIndex, actual.LastBlockIndex);
            Assert.AreEqual(expected.Timestamp, actual.Timestamp);
            Assert.AreEqual(expected.Nonce, actual.Nonce);
        }

        [TestMethod]
        public void TestProtocolMessageRoundTrip()
        {
            var ping = PingMessage.Create(50, 123);
            var frame = ProtocolMessage.Create(ProtocolMessageCommand.Ping, ping);
            var bytes = frame.ToArray();

            var consumed = ProtocolMessage.TryRead(bytes, out var message);
            Assert.AreEqual(bytes.Length, consumed);
            Assert.IsNotNull(message);
            Assert.AreEqual(ProtocolMessageCommand.Ping, message.Command);
            Assert.IsInstanceOfType<PingMessage>(message.Message);

            var actual = (PingMessage)message.Message;
            Assert.AreEqual(50u, actual.LastBlockIndex);
            Assert.AreEqual(123u, actual.Nonce);
        }
    }
}
