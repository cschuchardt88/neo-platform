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
using Neo.Core.Net.Message;

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
    }
}
