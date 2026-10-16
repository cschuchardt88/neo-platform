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
    /// Groups the neo-node <c>close</c> commands.
    /// </summary>
    internal class CloseCommand : Command
    {
        /// <summary>
        /// Initializes the <c>close</c> command.
        /// </summary>
        public CloseCommand() : base("close", "Close the open wallet.")
        {
            Add(new CloseWalletCommand());
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>close wallet</c>.
    /// </summary>
    internal class CloseWalletCommand : Command
    {
        /// <summary>
        /// Initializes the <c>close wallet</c> command.
        /// </summary>
        public CloseWalletCommand() : base("wallet", "Close the open wallet.")
        {
        }
    }
}
