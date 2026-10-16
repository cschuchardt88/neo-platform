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

using Microsoft.Extensions.Hosting;
using Neo.Platform.Hosting;
using System.CommandLine;

namespace Neo.Platform.CLI.Commands
{
    /// <summary>
    /// Registers the shared handler for each neo-node command scaffold.
    /// </summary>
    internal static class NodeCommandRegistration
    {
        /// <summary>
        /// Binds <see cref="NodeCommandHandler"/> to every neo-node command that has no implementation yet.
        /// </summary>
        /// <param name="builder">The host builder.</param>
        /// <returns>The same <paramref name="builder"/> for chaining.</returns>
        public static IHostBuilder UseNodeCommands(this IHostBuilder builder)
        {
            Use<ShowBlockCommand>(builder);
            Use<ShowTxCommand>(builder);
            Use<ShowContractCommand>(builder);
            Use<ShowPoolCommand>(builder);
            Use<ShowStateCommand>(builder);
            Use<ShowGasCommand>(builder);
            Use<ExportBlocksCommand>(builder);
            Use<ExportKeyCommand>(builder);
            Use<DeployCommand>(builder);
            Use<UpdateCommand>(builder);
            Use<InvokeCommand>(builder);
            Use<InvokeAbiCommand>(builder);
            Use<ConsoleLogOffCommand>(builder);
            Use<ConsoleLogOnCommand>(builder);
            Use<ListNativeContractCommand>(builder);
            Use<TransferCommand>(builder);
            Use<BalanceOfCommand>(builder);
            Use<NameCommand>(builder);
            Use<DecimalsCommand>(builder);
            Use<TotalSupplyCommand>(builder);
            Use<BroadcastAddrCommand>(builder);
            Use<BroadcastBlockCommand>(builder);
            Use<BroadcastGetBlocksCommand>(builder);
            Use<BroadcastGetHeadersCommand>(builder);
            Use<BroadcastGetDataCommand>(builder);
            Use<BroadcastInvCommand>(builder);
            Use<BroadcastTransactionCommand>(builder);
            Use<RelayCommand>(builder);
            Use<InstallCommand>(builder);
            Use<ReinstallCommand>(builder);
            Use<UninstallCommand>(builder);
            Use<PluginsCommand>(builder);
            Use<ParseCommand>(builder);
            Use<RegisterCandidateCommand>(builder);
            Use<UnregisterCandidateCommand>(builder);
            Use<VoteCommand>(builder);
            Use<UnvoteCommand>(builder);
            Use<GetCandidatesCommand>(builder);
            Use<GetCommitteeCommand>(builder);
            Use<GetNextValidatorsCommand>(builder);
            Use<GetAccountStateCommand>(builder);
            Use<OpenWalletCommand>(builder);
            Use<CloseWalletCommand>(builder);
            Use<UpgradeWalletCommand>(builder);
            Use<CreateAddressCommand>(builder);
            Use<CreateWalletCommand>(builder);
            Use<DeleteAddressCommand>(builder);
            Use<ImportMultisigAddressCommand>(builder);
            Use<ImportKeyCommand>(builder);
            Use<ImportWatchOnlyCommand>(builder);
            Use<ListAddressCommand>(builder);
            Use<ListAssetCommand>(builder);
            Use<ListKeyCommand>(builder);
            Use<SignCommand>(builder);
            Use<SignMessageCommand>(builder);
            Use<VerifyMessageCommand>(builder);
            Use<SendCommand>(builder);
            Use<CancelCommand>(builder);
            Use<ChangePasswordCommand>(builder);

            return builder;
        }

        private static void Use<TCommand>(IHostBuilder builder)
            where TCommand : Command
        {
            builder.UseCommandAction<TCommand, NodeCommandHandler>();
        }
    }
}
