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
    /// Groups the neo-node <c>create</c> commands.
    /// </summary>
    internal class CreateCommand : Command
    {
        /// <summary>
        /// Initializes the <c>create</c> command.
        /// </summary>
        public CreateCommand() : base("create", "Create a wallet or addresses.")
        {
            Add(new CreateAddressCommand());
            Add(new CreateWalletCommand());
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>create address</c>.
    /// </summary>
    internal class CreateAddressCommand : Command
    {
        /// <summary>
        /// Initializes the <c>create address</c> command.
        /// </summary>
        public CreateAddressCommand() : base("address", "Create addresses in the open wallet.")
        {
            Add(CommandArgument.Optional("count", "Number of addresses to create.", "1"));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>create wallet</c>.
    /// </summary>
    internal class CreateWalletCommand : Command
    {
        /// <summary>
        /// Initializes the <c>create wallet</c> command.
        /// </summary>
        public CreateWalletCommand() : base("wallet", "Create a wallet file.")
        {
            Add(CommandArgument.Required("path", "Path of the wallet file to create."));
            Add(CommandArgument.Optional("wif-or-file", "WIF or key file used to create the first account."));
            Add(CommandArgument.Optional("wallet-name", "Display name stored in the wallet."));
        }
    }
}
