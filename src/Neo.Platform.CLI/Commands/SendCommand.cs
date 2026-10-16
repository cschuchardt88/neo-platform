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
    /// CLI command for neo-node <c>send</c>.
    /// </summary>
    internal class SendCommand : Command
    {
        /// <summary>
        /// Initializes the <c>send</c> command.
        /// </summary>
        public SendCommand() : base("send", "Send an asset from the open wallet.")
        {
            Add(CommandArgument.Required("asset", "Asset script hash."));
            Add(CommandArgument.Required("to", "Recipient script hash."));
            Add(CommandArgument.Required("amount", "Amount to send."));
            Add(CommandArgument.Optional("from", "Sender script hash. Uses the default account when omitted."));
            Add(CommandArgument.Optional("data", "Extra data passed with the transfer."));
            Add(CommandArgument.OptionalMany("--signer-accounts", "Additional signer script hashes."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>cancel</c>.
    /// </summary>
    internal class CancelCommand : Command
    {
        /// <summary>
        /// Initializes the <c>cancel</c> command.
        /// </summary>
        public CancelCommand() : base("cancel", "Cancel a transaction.")
        {
            Add(CommandArgument.Required("txid", "Hash of the transaction to cancel."));
            Add(CommandArgument.Optional("sender", "Transaction sender script hash."));
            Add(CommandArgument.OptionalMany("--signer-accounts", "Additional signer script hashes."));
        }
    }

    /// <summary>
    /// Groups the neo-node <c>change</c> commands.
    /// </summary>
    internal class ChangeCommand : Command
    {
        /// <summary>
        /// Initializes the <c>change</c> command.
        /// </summary>
        public ChangeCommand() : base("change", "Change wallet settings.")
        {
            Add(new ChangePasswordCommand());
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>change password</c>.
    /// </summary>
    internal class ChangePasswordCommand : Command
    {
        /// <summary>
        /// Initializes the <c>change password</c> command.
        /// </summary>
        public ChangePasswordCommand() : base("password", "Change the open wallet password.")
        {
        }
    }
}
