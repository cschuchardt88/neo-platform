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
    /// Groups the neo-node <c>broadcast</c> commands.
    /// </summary>
    internal class BroadcastCommand : Command
    {
        /// <summary>
        /// Initializes the <c>broadcast</c> command.
        /// </summary>
        public BroadcastCommand() : base("broadcast", "Broadcast a network message.")
        {
            Add(new BroadcastAddrCommand());
            Add(new BroadcastBlockCommand());
            Add(new BroadcastGetBlocksCommand());
            Add(new BroadcastGetHeadersCommand());
            Add(new BroadcastGetDataCommand());
            Add(new BroadcastInvCommand());
            Add(new BroadcastTransactionCommand());
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>broadcast addr</c>.
    /// </summary>
    internal class BroadcastAddrCommand : Command
    {
        /// <summary>
        /// Initializes the <c>broadcast addr</c> command.
        /// </summary>
        public BroadcastAddrCommand() : base("addr", "Broadcast an address.")
        {
            Add(CommandArgument.Required("address", "IP address to broadcast."));
            Add(CommandArgument.Required("port", "Port to broadcast."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>broadcast block</c>.
    /// </summary>
    internal class BroadcastBlockCommand : Command
    {
        /// <summary>
        /// Initializes the <c>broadcast block</c> command.
        /// </summary>
        public BroadcastBlockCommand() : base("block", "Broadcast a block by hash or height.")
        {
            Add(CommandArgument.Required("hash-or-height", "Block hash or block height."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>broadcast getblocks</c>.
    /// </summary>
    internal class BroadcastGetBlocksCommand : Command
    {
        /// <summary>
        /// Initializes the <c>broadcast getblocks</c> command.
        /// </summary>
        public BroadcastGetBlocksCommand() : base("getblocks", "Broadcast a getblocks request.")
        {
            Add(CommandArgument.Required("hash", "Hash to start the getblocks request from."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>broadcast getheaders</c>.
    /// </summary>
    internal class BroadcastGetHeadersCommand : Command
    {
        /// <summary>
        /// Initializes the <c>broadcast getheaders</c> command.
        /// </summary>
        public BroadcastGetHeadersCommand() : base("getheaders", "Broadcast a getheaders request.")
        {
            Add(CommandArgument.Required("index", "Block index to start the getheaders request from."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>broadcast getdata</c>.
    /// </summary>
    internal class BroadcastGetDataCommand : Command
    {
        /// <summary>
        /// Initializes the <c>broadcast getdata</c> command.
        /// </summary>
        public BroadcastGetDataCommand() : base("getdata", "Broadcast a getdata request.")
        {
            Add(CommandArgument.Required("type", "Inventory type."));
            Add(CommandArgument.Many("hashes", "Inventory hashes to request."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>broadcast inv</c>.
    /// </summary>
    internal class BroadcastInvCommand : Command
    {
        /// <summary>
        /// Initializes the <c>broadcast inv</c> command.
        /// </summary>
        public BroadcastInvCommand() : base("inv", "Broadcast an inventory message.")
        {
            Add(CommandArgument.Required("type", "Inventory type."));
            Add(CommandArgument.Many("hashes", "Inventory hashes to announce."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>broadcast transaction</c>.
    /// </summary>
    internal class BroadcastTransactionCommand : Command
    {
        /// <summary>
        /// Initializes the <c>broadcast transaction</c> command.
        /// </summary>
        public BroadcastTransactionCommand() : base("transaction", "Broadcast a transaction by hash.")
        {
            Add(CommandArgument.Required("hash", "Transaction hash."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>relay</c>.
    /// </summary>
    internal class RelayCommand : Command
    {
        /// <summary>
        /// Initializes the <c>relay</c> command.
        /// </summary>
        public RelayCommand() : base("relay", "Relay a JSON transaction.")
        {
            Add(CommandArgument.Required("json", "JSON object to relay."));
        }
    }
}
