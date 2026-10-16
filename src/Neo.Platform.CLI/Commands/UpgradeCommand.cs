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
    /// Groups the neo-node <c>upgrade</c> commands.
    /// </summary>
    internal class UpgradeCommand : Command
    {
        /// <summary>
        /// Initializes the <c>upgrade</c> command.
        /// </summary>
        public UpgradeCommand() : base("upgrade", "Upgrade a wallet.")
        {
            Add(new UpgradeWalletCommand());
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>upgrade wallet</c>.
    /// </summary>
    internal class UpgradeWalletCommand : Command
    {
        /// <summary>
        /// Initializes the <c>upgrade wallet</c> command.
        /// </summary>
        public UpgradeWalletCommand() : base("wallet", "Upgrade a wallet file.")
        {
            Add(CommandArgument.Required("path", "Path of the wallet file to upgrade."));
        }
    }
}
