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
    /// Groups the neo-node <c>list</c> commands.
    /// </summary>
    internal class ListCommand : Command
    {
        /// <summary>
        /// Initializes the <c>list</c> command.
        /// </summary>
        public ListCommand() : base("list", "List addresses, assets, keys, or native contracts.")
        {
            Add(new ListAddressCommand());
            Add(new ListAssetCommand());
            Add(new ListKeyCommand());
            Add(new ListNativeContractCommand());
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>list address</c>.
    /// </summary>
    internal class ListAddressCommand : Command
    {
        /// <summary>
        /// Initializes the <c>list address</c> command.
        /// </summary>
        public ListAddressCommand() : base("address", "List addresses in the open wallet.")
        {
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>list asset</c>.
    /// </summary>
    internal class ListAssetCommand : Command
    {
        /// <summary>
        /// Initializes the <c>list asset</c> command.
        /// </summary>
        public ListAssetCommand() : base("asset", "List asset balances in the open wallet.")
        {
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>list key</c>.
    /// </summary>
    internal class ListKeyCommand : Command
    {
        /// <summary>
        /// Initializes the <c>list key</c> command.
        /// </summary>
        public ListKeyCommand() : base("key", "List keys in the open wallet.")
        {
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>list nativecontract</c>.
    /// </summary>
    internal class ListNativeContractCommand : Command
    {
        /// <summary>
        /// Initializes the <c>list nativecontract</c> command.
        /// </summary>
        public ListNativeContractCommand() : base("nativecontract", "List native contracts.")
        {
        }
    }
}
