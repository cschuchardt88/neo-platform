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
    /// CLI command for neo-node <c>install</c>.
    /// </summary>
    internal class InstallCommand : Command
    {
        /// <summary>
        /// Initializes the <c>install</c> command.
        /// </summary>
        public InstallCommand() : base("install", "Install a plugin.")
        {
            Add(CommandArgument.Required("plugin-name", "Plugin name."));
            Add(CommandArgument.Optional("download-url", "Download URL used when the plugin is not local."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>reinstall</c>.
    /// </summary>
    internal class ReinstallCommand : Command
    {
        /// <summary>
        /// Initializes the <c>reinstall</c> command.
        /// </summary>
        public ReinstallCommand() : base("reinstall", "Overwrite an existing plugin by force.")
        {
            Add(CommandArgument.Required("plugin-name", "Plugin name."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>uninstall</c>.
    /// </summary>
    internal class UninstallCommand : Command
    {
        /// <summary>
        /// Initializes the <c>uninstall</c> command.
        /// </summary>
        public UninstallCommand() : base("uninstall", "Uninstall a plugin.")
        {
            Add(CommandArgument.Required("plugin-name", "Plugin name."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>plugins</c>.
    /// </summary>
    internal class PluginsCommand : Command
    {
        /// <summary>
        /// Initializes the <c>plugins</c> command.
        /// </summary>
        public PluginsCommand() : base("plugins", "List installed plugins.")
        {
        }
    }
}
