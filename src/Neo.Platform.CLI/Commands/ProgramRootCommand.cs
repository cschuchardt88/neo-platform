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
    /// Root command for the Neo Platform CLI.
    /// </summary>
    internal class ProgramRootCommand : RootCommand
    {
        /// <summary>
        /// Initializes the root command and registers subcommands.
        /// </summary>
        public ProgramRootCommand() : base("Neo Platform Command-line Tool")
        {
            Add(new ShowCommand());
            Add(new ExportCommand());
            Add(new ImportCommand());
            Add(new CreateCommand());
            Add(new OpenCommand());
            Add(new CloseCommand());
            Add(new UpgradeCommand());
            Add(new DeleteCommand());
            Add(new ListCommand());
            Add(new SignCommand());
            Add(new VerifyCommand());
            Add(new SendCommand());
            Add(new CancelCommand());
            Add(new ChangeCommand());
            Add(new DeployCommand());
            Add(new UpdateCommand());
            Add(new InvokeCommand());
            Add(new InvokeAbiCommand());
            Add(new TransferCommand());
            Add(new BalanceOfCommand());
            Add(new NameCommand());
            Add(new DecimalsCommand());
            Add(new TotalSupplyCommand());
            Add(new BroadcastCommand());
            Add(new RelayCommand());
            Add(new ConsoleCommandGroup());
            Add(new InstallCommand());
            Add(new ReinstallCommand());
            Add(new UninstallCommand());
            Add(new PluginsCommand());
            Add(new ParseCommand());
            Add(new RegisterCommand());
            Add(new UnregisterCommand());
            Add(new VoteCommand());
            Add(new UnvoteCommand());
            Add(new GetCommand());
        }
    }
}
