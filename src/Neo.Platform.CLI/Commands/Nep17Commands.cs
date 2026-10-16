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
    /// CLI command for neo-node <c>transfer</c>.
    /// </summary>
    internal class TransferCommand : Command
    {
        /// <summary>
        /// Initializes the <c>transfer</c> command.
        /// </summary>
        public TransferCommand() : base("transfer", "Transfer a NEP-17 token.")
        {
            Add(CommandArgument.Required("token-hash", "NEP-17 contract script hash."));
            Add(CommandArgument.Required("to", "Recipient script hash."));
            Add(CommandArgument.Required("amount", "Amount to transfer."));
            Add(CommandArgument.Optional("from", "Sender script hash. Uses the default account when omitted."));
            Add(CommandArgument.Optional("data", "Extra data passed to the transfer."));
            Add(CommandArgument.OptionalMany("--signer-accounts", "Additional signer script hashes."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>balanceOf</c>.
    /// </summary>
    internal class BalanceOfCommand : Command
    {
        /// <summary>
        /// Initializes the <c>balanceOf</c> command.
        /// </summary>
        public BalanceOfCommand() : base("balanceOf", "Show a NEP-17 balance.")
        {
            Add(CommandArgument.Required("token-hash", "NEP-17 contract script hash."));
            Add(CommandArgument.Required("address", "Account script hash."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>name</c>.
    /// </summary>
    internal class NameCommand : Command
    {
        /// <summary>
        /// Initializes the <c>name</c> command.
        /// </summary>
        public NameCommand() : base("name", "Show a NEP-17 token name.")
        {
            Add(CommandArgument.Required("token-hash", "NEP-17 contract script hash."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>decimals</c>.
    /// </summary>
    internal class DecimalsCommand : Command
    {
        /// <summary>
        /// Initializes the <c>decimals</c> command.
        /// </summary>
        public DecimalsCommand() : base("decimals", "Show a NEP-17 token decimal count.")
        {
            Add(CommandArgument.Required("token-hash", "NEP-17 contract script hash."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>totalSupply</c>.
    /// </summary>
    internal class TotalSupplyCommand : Command
    {
        /// <summary>
        /// Initializes the <c>totalSupply</c> command.
        /// </summary>
        public TotalSupplyCommand() : base("totalSupply", "Show a NEP-17 total supply.")
        {
            Add(CommandArgument.Required("token-hash", "NEP-17 contract script hash."));
        }
    }
}
