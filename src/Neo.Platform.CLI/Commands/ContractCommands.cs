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
    /// CLI command for neo-node <c>deploy</c>.
    /// </summary>
    internal class DeployCommand : Command
    {
        /// <summary>
        /// Initializes the <c>deploy</c> command.
        /// </summary>
        public DeployCommand() : base("deploy", "Deploy a contract from a NEF file.")
        {
            Add(CommandArgument.Required("file-path", "Path of the NEF file."));
            Add(CommandArgument.Optional("manifest-path", "Path of the contract manifest."));
            Add(CommandArgument.Optional("data", "JSON data passed to the deploy method."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>update</c>.
    /// </summary>
    internal class UpdateCommand : Command
    {
        /// <summary>
        /// Initializes the <c>update</c> command.
        /// </summary>
        public UpdateCommand() : base("update", "Update a deployed contract.")
        {
            Add(CommandArgument.Required("script-hash", "Script hash of the contract to update."));
            Add(CommandArgument.Required("file-path", "Path of the replacement NEF file."));
            Add(CommandArgument.Required("manifest-path", "Path of the replacement manifest."));
            Add(CommandArgument.Required("sender", "Transaction sender script hash."));
            Add(CommandArgument.Optional("data", "JSON data passed to the update method."));
            Add(CommandArgument.OptionalMany("--signer-accounts", "Additional signer script hashes."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>invoke</c>.
    /// </summary>
    internal class InvokeCommand : Command
    {
        /// <summary>
        /// Initializes the <c>invoke</c> command.
        /// </summary>
        public InvokeCommand() : base("invoke", "Invoke a contract method.")
        {
            Add(CommandArgument.Required("script-hash", "Script hash of the contract."));
            Add(CommandArgument.Required("operation", "Contract method name."));
            Add(CommandArgument.Optional("sender", "Transaction sender script hash."));
            Add(CommandArgument.Optional("max-gas", "Maximum GAS for the invocation.", "20"));
            Add(CommandArgument.OptionalMany("--contract-parameters", "JSON contract parameters."));
            Add(CommandArgument.OptionalMany("--signer-accounts", "Additional signer script hashes."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>invokeabi</c>.
    /// </summary>
    internal class InvokeAbiCommand : Command
    {
        /// <summary>
        /// Initializes the <c>invokeabi</c> command.
        /// </summary>
        public InvokeAbiCommand() : base("invokeabi", "Invoke a contract method using its ABI.")
        {
            Add(CommandArgument.Required("script-hash", "Script hash of the contract."));
            Add(CommandArgument.Required("operation", "Contract method name."));
            Add(CommandArgument.Optional("sender", "Transaction sender script hash."));
            Add(CommandArgument.Optional("max-gas", "Maximum GAS for the invocation.", "20"));
            Add(CommandArgument.OptionalMany("--args", "Arguments parsed according to the ABI."));
            Add(CommandArgument.OptionalMany("--signer-accounts", "Additional signer script hashes."));
        }
    }
}
