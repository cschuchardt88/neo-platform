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

using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading;
using System.Threading.Tasks;

namespace Neo.Platform.CLI.Commands
{
    /// <summary>
    /// CLI command that displays configuration samples and related information.
    /// </summary>
    internal class ShowCommand : Command
    {
        /// <summary>
        /// Initializes the <c>show</c> command.
        /// </summary>
        public ShowCommand() : base("show", "Show a block, transaction, contract, mempool, node state, or GAS.")
        {
            Add(new ShowBlockCommand());
            Add(new ShowTxCommand());
            Add(new ShowContractCommand());
            Add(new ShowPoolCommand());
            Add(new ShowStateCommand());
            Add(new ShowGasCommand());

            // A parent with subcommands and no action is rejected at parse time.
            // UseCommandAction replaces this with Handler after parsing.
            SetAction(static _ => 0);
        }

        /// <summary>
        /// Handles the <c>show</c> command by writing sample configuration output.
        /// </summary>
        /// <param name="loggerFactory">The factory used to create the command logger.</param>
        public class Handler(ILoggerFactory loggerFactory) : AsynchronousCommandLineAction
        {
            private readonly ILogger _logger = loggerFactory.CreateLogger<ShowCommand>();

            /// <summary>
            /// Invokes the <c>show</c> command handler.
            /// </summary>
            /// <param name="parseResult">The parse result for the current command invocation.</param>
            /// <param name="cancellationToken">A token that can cancel the operation.</param>
            /// <returns>A completed task with exit code 0.</returns>
            public override Task<int> InvokeAsync(ParseResult parseResult, CancellationToken cancellationToken = default)
            {
                _logger.LogInformation("Hello World!!!!");

                return Task.FromResult(0);
            }
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>show block</c>.
    /// </summary>
    internal class ShowBlockCommand : Command
    {
        /// <summary>
        /// Initializes the <c>show block</c> command.
        /// </summary>
        public ShowBlockCommand() : base("block", "Show a block by index or hash.")
        {
            Add(CommandArgument.Required("index-or-hash", "Block index or hash."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>show tx</c>.
    /// </summary>
    internal class ShowTxCommand : Command
    {
        /// <summary>
        /// Initializes the <c>show tx</c> command.
        /// </summary>
        public ShowTxCommand() : base("tx", "Show a transaction by hash.")
        {
            Add(CommandArgument.Required("hash", "Transaction hash."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>show contract</c>.
    /// </summary>
    internal class ShowContractCommand : Command
    {
        /// <summary>
        /// Initializes the <c>show contract</c> command.
        /// </summary>
        public ShowContractCommand() : base("contract", "Show a contract by name or hash.")
        {
            Add(CommandArgument.Required("name-or-hash", "Contract name or script hash."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>show pool</c>.
    /// </summary>
    internal class ShowPoolCommand : Command
    {
        /// <summary>
        /// Initializes the <c>show pool</c> command.
        /// </summary>
        public ShowPoolCommand() : base("pool", "Show the current state of the mempool.")
        {
            Add(CommandArgument.Optional("verbose", "True to show each pooled transaction."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>show state</c>.
    /// </summary>
    internal class ShowStateCommand : Command
    {
        /// <summary>
        /// Initializes the <c>show state</c> command.
        /// </summary>
        public ShowStateCommand() : base("state", "Show the current state of the node.")
        {
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>show gas</c>.
    /// </summary>
    internal class ShowGasCommand : Command
    {
        /// <summary>
        /// Initializes the <c>show gas</c> command.
        /// </summary>
        public ShowGasCommand() : base("gas", "Show the GAS balance of the open wallet.")
        {
        }
    }
}
