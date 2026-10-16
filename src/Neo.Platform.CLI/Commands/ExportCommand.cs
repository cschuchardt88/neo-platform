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
    /// Groups the neo-node <c>export</c> commands.
    /// </summary>
    internal class ExportCommand : Command
    {
        /// <summary>
        /// Initializes the <c>export</c> command.
        /// </summary>
        public ExportCommand() : base("export", "Export blocks or wallet keys.")
        {
            Add(new ExportBlocksCommand());
            Add(new ExportKeyCommand());
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>export blocks</c>.
    /// </summary>
    internal class ExportBlocksCommand : Command
    {
        /// <summary>
        /// Initializes the <c>export blocks</c> command.
        /// </summary>
        public ExportBlocksCommand() : base("blocks", "Export blocks to an .acc file.")
        {
            Add(CommandArgument.Required("start", "Index of the first block to export."));
            Add(CommandArgument.Optional("count", "Number of blocks to export. Defaults to the remaining chain when omitted."));
            Add(CommandArgument.Optional("path", "Output path. Defaults to chain.{start}.acc when omitted."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>export key</c>.
    /// </summary>
    internal class ExportKeyCommand : Command
    {
        /// <summary>
        /// Initializes the <c>export key</c> command.
        /// </summary>
        public ExportKeyCommand() : base("key", "Export wallet keys.")
        {
            Add(CommandArgument.Optional("path", "File path to write the exported keys."));
            Add(CommandArgument.Optional("script-hash", "Account script hash to export. Exports every key when omitted."));
        }
    }
}
