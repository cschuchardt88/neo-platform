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
using System.Net;

namespace Neo.Core.Tests.Net
{
    [TestClass]
    public class UT_NodeHandshake
    {
        private const uint Network = 0x334F454E;
        private const uint LocalNonce = 100u;
        private const uint RemoteNonce = 200u;

        [TestMethod]
        public void TestHappyPath()
        {
            var handshake = new NodeHandshake(Network, LocalNonce);

            Assert.AreEqual(NodeHandshakeState.WaitingForVersion, handshake.State);

            var versionFrame = ProtocolMessage.Create(
                ProtocolMessageCommand.Version,
                VersionMessage.Create(Network, RemoteNonce, new FullNodeCapabilityMessage(1)));

            var verack = handshake.Process(versionFrame);

            Assert.IsNotNull(verack);
            Assert.AreEqual(ProtocolMessageCommand.VersionAck, verack.Command);
            Assert.AreEqual(NodeHandshakeState.WaitingForVerack, handshake.State);
            Assert.IsNotNull(handshake.RemoteVersion);
            Assert.AreEqual(RemoteNonce, handshake.RemoteVersion.Nonce);
            Assert.IsTrue(handshake.RemoteAllowsCompression);

            var reply = handshake.Process(ProtocolMessage.Create(ProtocolMessageCommand.VersionAck));

            Assert.IsNull(reply);
            Assert.IsTrue(handshake.IsReady);
            Assert.AreEqual(NodeHandshakeState.Ready, handshake.State);
        }

        [TestMethod]
        public void TestRejectsNonVersionFirst()
        {
            var handshake = new NodeHandshake(Network, LocalNonce);

            Assert.ThrowsExactly<ProtocolViolationException>(() =>
                handshake.Process(ProtocolMessage.Create(ProtocolMessageCommand.VersionAck)));
        }

        [TestMethod]
        public void TestRejectsNetworkMismatch()
        {
            var handshake = new NodeHandshake(Network, LocalNonce);
            var versionFrame = ProtocolMessage.Create(
                ProtocolMessageCommand.Version,
                VersionMessage.Create(network: 1u, nonce: RemoteNonce));

            Assert.ThrowsExactly<ProtocolViolationException>(() =>
                handshake.Process(versionFrame));
        }

        [TestMethod]
        public void TestRejectsSelfNonce()
        {
            var handshake = new NodeHandshake(Network, LocalNonce);
            var versionFrame = ProtocolMessage.Create(
                ProtocolMessageCommand.Version,
                VersionMessage.Create(Network, LocalNonce));

            Assert.ThrowsExactly<ProtocolViolationException>(() =>
                handshake.Process(versionFrame));
        }

        [TestMethod]
        public void TestRejectsWrongCommandWhileWaitingVerack()
        {
            var handshake = new NodeHandshake(Network, LocalNonce);
            handshake.Process(ProtocolMessage.Create(
                ProtocolMessageCommand.Version,
                VersionMessage.Create(Network, RemoteNonce)));

            Assert.ThrowsExactly<ProtocolViolationException>(() =>
                handshake.Process(ProtocolMessage.Create(
                    ProtocolMessageCommand.Version,
                    VersionMessage.Create(Network, RemoteNonce + 1))));
        }

        [TestMethod]
        public void TestRejectsHandshakeMessagesAfterReady()
        {
            var handshake = new NodeHandshake(Network, LocalNonce);
            handshake.Process(ProtocolMessage.Create(
                ProtocolMessageCommand.Version,
                VersionMessage.Create(Network, RemoteNonce)));
            handshake.Process(ProtocolMessage.Create(ProtocolMessageCommand.VersionAck));

            Assert.ThrowsExactly<ProtocolViolationException>(() =>
                handshake.Process(ProtocolMessage.Create(ProtocolMessageCommand.VersionAck)));
        }

        [TestMethod]
        public void TestReadyAllowsOtherCommands()
        {
            var handshake = new NodeHandshake(Network, LocalNonce);
            handshake.Process(ProtocolMessage.Create(
                ProtocolMessageCommand.Version,
                VersionMessage.Create(Network, RemoteNonce)));
            handshake.Process(ProtocolMessage.Create(ProtocolMessageCommand.VersionAck));

            var reply = handshake.Process(ProtocolMessage.Create(ProtocolMessageCommand.Ping));

            Assert.IsNull(reply);
            Assert.IsTrue(handshake.IsReady);
        }

        [TestMethod]
        public void TestDisableCompressionCapability()
        {
            var handshake = new NodeHandshake(Network, LocalNonce);
            var versionFrame = ProtocolMessage.Create(
                ProtocolMessageCommand.Version,
                VersionMessage.Create(Network, RemoteNonce, new DisableCompressionCapabilityMessage()));

            handshake.Process(versionFrame);

            Assert.IsFalse(handshake.RemoteAllowsCompression);
        }
    }
}
