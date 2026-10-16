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

using System.CommandLine;

namespace Neo.Platform.CLI.Commands
{
    /// <summary>
    /// CLI command for neo-node <c>sign</c>.
    /// </summary>
    internal class SignCommand : Command
    {
        /// <summary>
        /// Initializes the <c>sign</c> command.
        /// </summary>
        public SignCommand() : base("sign", "Sign a JSON transaction.")
        {
            Add(CommandArgument.Required("json", "JSON object to sign."));
            Add(new SignMessageCommand());
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>sign message</c>.
    /// </summary>
    internal class SignMessageCommand : Command
    {
        /// <summary>
        /// Initializes the <c>sign message</c> command.
        /// </summary>
        public SignMessageCommand() : base("message", "Sign a text message.")
        {
            Add(CommandArgument.Required("message", "Message text to sign."));
            Add(CommandArgument.Required("avoid-signature-replay", "True to bind the signature to this network."));
        }
    }

    /// <summary>
    /// Groups the neo-node <c>verify</c> commands.
    /// </summary>
    internal class VerifyCommand : Command
    {
        /// <summary>
        /// Initializes the <c>verify</c> command.
        /// </summary>
        public VerifyCommand() : base("verify", "Verify a signed message.")
        {
            Add(new VerifyMessageCommand());
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>verify message</c>.
    /// </summary>
    internal class VerifyMessageCommand : Command
    {
        /// <summary>
        /// Initializes the <c>verify message</c> command.
        /// </summary>
        public VerifyMessageCommand() : base("message", "Verify a signed message.")
        {
            Add(CommandArgument.Required("message", "Message text that was signed."));
            Add(CommandArgument.Required("signature", "Signature to verify."));
            Add(CommandArgument.Required("public-key", "Signer public key."));
            Add(CommandArgument.Required("salt", "Signature salt."));
            Add(CommandArgument.Required("avoid-signature-replay", "True when the signature was bound to this network."));
        }
    }
}
