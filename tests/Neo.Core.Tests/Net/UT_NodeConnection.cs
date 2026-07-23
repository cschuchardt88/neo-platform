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

using Neo.Core.Net;
using Neo.Core.Net.Message;
using System;
using System.Buffers;
using System.Collections.Generic;

namespace Neo.Core.Tests.Net
{
    [TestClass]
    public class UT_NodeConnection
    {
        [TestMethod]
        public void TestTryReadFrameIncomplete()
        {
            // Flags + command only — missing VarSize length byte.
            ReadOnlySequence<byte> buffer = new([(byte)ProtocolMessageFlags.None, (byte)ProtocolMessageCommand.VersionAck]);

            var ok = NodeConnection.TryReadFrame(ref buffer, out var message);

            Assert.IsFalse(ok);
            Assert.IsNull(message);
            Assert.AreEqual(2, buffer.Length);
        }

        [TestMethod]
        public void TestTryReadFrameOneMessage()
        {
            var bytes = ProtocolMessage.Create(ProtocolMessageCommand.VersionAck).ToArray();
            ReadOnlySequence<byte> buffer = new(bytes);

            var ok = NodeConnection.TryReadFrame(ref buffer, out var message);

            Assert.IsTrue(ok);
            Assert.IsNotNull(message);
            Assert.AreEqual(ProtocolMessageCommand.VersionAck, message.Command);
            Assert.AreEqual(0, buffer.Length);
        }

        [TestMethod]
        public void TestTryReadFrameMultipleMessages()
        {
            var frame1 = ProtocolMessage.Create(ProtocolMessageCommand.VersionAck).ToArray();
            var frame2 = ProtocolMessage.Create(
                ProtocolMessageCommand.Version,
                VersionMessage.Create(0x334F454E, 42u)).ToArray();

            var combined = new byte[frame1.Length + frame2.Length];
            frame1.CopyTo(combined.AsSpan());
            frame2.CopyTo(combined.AsSpan(frame1.Length));

            ReadOnlySequence<byte> buffer = new(combined);
            var messages = new List<ProtocolMessage>();

            while (NodeConnection.TryReadFrame(ref buffer, out var message))
                messages.Add(message!);

            Assert.HasCount(2, messages);
            Assert.AreEqual(ProtocolMessageCommand.VersionAck, messages[0].Command);
            Assert.AreEqual(ProtocolMessageCommand.Version, messages[1].Command);
            Assert.IsInstanceOfType<VersionMessage>(messages[1].Message);
            Assert.AreEqual(0, buffer.Length);
        }

        [TestMethod]
        public void TestTryReadFramePartialThenComplete()
        {
            var full = ProtocolMessage.Create(ProtocolMessageCommand.VersionAck).ToArray();
            Assert.IsGreaterThan(2, full.Length);

            // First half only — incomplete.
            var partial = full.AsSpan(0, full.Length - 1).ToArray();
            ReadOnlySequence<byte> buffer = new(partial);

            Assert.IsFalse(NodeConnection.TryReadFrame(ref buffer, out _));
            Assert.AreEqual(partial.Length, buffer.Length);

            // Remaining byte arrives — full frame.
            var complete = new byte[partial.Length + 1];
            partial.CopyTo(complete.AsSpan());
            complete[^1] = full[^1];
            buffer = new ReadOnlySequence<byte>(complete);

            Assert.IsTrue(NodeConnection.TryReadFrame(ref buffer, out var message));
            Assert.IsNotNull(message);
            Assert.AreEqual(ProtocolMessageCommand.VersionAck, message.Command);
            Assert.AreEqual(0, buffer.Length);
        }

        [TestMethod]
        public void TestTryReadFrameLeavesTrailingBytes()
        {
            var frame = ProtocolMessage.Create(ProtocolMessageCommand.VersionAck).ToArray();
            var trailing = new byte[] { 0xAA, 0xBB };
            var combined = new byte[frame.Length + trailing.Length];
            frame.CopyTo(combined.AsSpan());
            trailing.CopyTo(combined.AsSpan(frame.Length));

            ReadOnlySequence<byte> buffer = new(combined);

            Assert.IsTrue(NodeConnection.TryReadFrame(ref buffer, out var message));
            Assert.IsNotNull(message);
            Assert.AreEqual(2, buffer.Length);
            Assert.AreSequenceEqual(trailing, buffer.ToArray());
        }
    }
}
