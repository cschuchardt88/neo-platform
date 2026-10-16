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
    /// Groups the neo-node <c>import</c> commands.
    /// </summary>
    internal class ImportCommand : Command
    {
        /// <summary>
        /// Initializes the <c>import</c> command.
        /// </summary>
        public ImportCommand() : base("import", "Import keys, watch-only addresses, or multi-sig addresses.")
        {
            Add(new ImportMultisigAddressCommand());
            Add(new ImportKeyCommand());
            Add(new ImportWatchOnlyCommand());
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>import multisigaddress</c>.
    /// </summary>
    internal class ImportMultisigAddressCommand : Command
    {
        /// <summary>
        /// Initializes the <c>import multisigaddress</c> command.
        /// </summary>
        public ImportMultisigAddressCommand() : base("multisigaddress", "Import a multi-signature address.")
        {
            Add(CommandArgument.Required("m", "Number of signatures required."));
            Add(CommandArgument.Many("public-keys", "Public keys that make up the multi-sig address."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>import key</c>.
    /// </summary>
    internal class ImportKeyCommand : Command
    {
        /// <summary>
        /// Initializes the <c>import key</c> command.
        /// </summary>
        public ImportKeyCommand() : base("key", "Import a WIF private key or a key file.")
        {
            Add(CommandArgument.Required("wif-or-file", "WIF private key or path to a key file."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>import watchonly</c>.
    /// </summary>
    internal class ImportWatchOnlyCommand : Command
    {
        /// <summary>
        /// Initializes the <c>import watchonly</c> command.
        /// </summary>
        public ImportWatchOnlyCommand() : base("watchonly", "Import a watch-only address or address file.")
        {
            Add(CommandArgument.Required("address-or-file", "Address or path to an address file."));
        }
    }
}
