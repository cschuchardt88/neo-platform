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
    /// Groups the neo-node <c>register</c> commands.
    /// </summary>
    internal class RegisterCommand : Command
    {
        /// <summary>
        /// Initializes the <c>register</c> command.
        /// </summary>
        public RegisterCommand() : base("register", "Register a consensus candidate.")
        {
            Add(new RegisterCandidateCommand());
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>register candidate</c>.
    /// </summary>
    internal class RegisterCandidateCommand : Command
    {
        /// <summary>
        /// Initializes the <c>register candidate</c> command.
        /// </summary>
        public RegisterCandidateCommand() : base("candidate", "Register an account as a consensus candidate.")
        {
            Add(CommandArgument.Required("account", "Candidate account script hash."));
        }
    }

    /// <summary>
    /// Groups the neo-node <c>unregister</c> commands.
    /// </summary>
    internal class UnregisterCommand : Command
    {
        /// <summary>
        /// Initializes the <c>unregister</c> command.
        /// </summary>
        public UnregisterCommand() : base("unregister", "Unregister a consensus candidate.")
        {
            Add(new UnregisterCandidateCommand());
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>unregister candidate</c>.
    /// </summary>
    internal class UnregisterCandidateCommand : Command
    {
        /// <summary>
        /// Initializes the <c>unregister candidate</c> command.
        /// </summary>
        public UnregisterCandidateCommand() : base("candidate", "Unregister a consensus candidate.")
        {
            Add(CommandArgument.Required("account", "Candidate account script hash."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>vote</c>.
    /// </summary>
    internal class VoteCommand : Command
    {
        /// <summary>
        /// Initializes the <c>vote</c> command.
        /// </summary>
        public VoteCommand() : base("vote", "Vote for a consensus candidate.")
        {
            Add(CommandArgument.Required("sender-account", "Voting account script hash."));
            Add(CommandArgument.Required("public-key", "Candidate public key."));
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>unvote</c>.
    /// </summary>
    internal class UnvoteCommand : Command
    {
        /// <summary>
        /// Initializes the <c>unvote</c> command.
        /// </summary>
        public UnvoteCommand() : base("unvote", "Remove a vote.")
        {
            Add(CommandArgument.Required("sender-account", "Voting account script hash."));
        }
    }

    /// <summary>
    /// Groups the neo-node <c>get</c> commands.
    /// </summary>
    internal class GetCommand : Command
    {
        /// <summary>
        /// Initializes the <c>get</c> command.
        /// </summary>
        public GetCommand() : base("get", "Query candidates, the committee, validators, or account state.")
        {
            Add(new GetCandidatesCommand());
            Add(new GetCommitteeCommand());
            Add(new GetNextCommand());
            Add(new GetAccountStateCommand());
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>get candidates</c>.
    /// </summary>
    internal class GetCandidatesCommand : Command
    {
        /// <summary>
        /// Initializes the <c>get candidates</c> command.
        /// </summary>
        public GetCandidatesCommand() : base("candidates", "List consensus candidates.")
        {
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>get committee</c>.
    /// </summary>
    internal class GetCommitteeCommand : Command
    {
        /// <summary>
        /// Initializes the <c>get committee</c> command.
        /// </summary>
        public GetCommitteeCommand() : base("committee", "List the current committee.")
        {
        }
    }

    /// <summary>
    /// Groups the neo-node <c>get next</c> commands.
    /// </summary>
    internal class GetNextCommand : Command
    {
        /// <summary>
        /// Initializes the <c>get next</c> command.
        /// </summary>
        public GetNextCommand() : base("next", "Query the next consensus set.")
        {
            Add(new GetNextValidatorsCommand());
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>get next validators</c>.
    /// </summary>
    internal class GetNextValidatorsCommand : Command
    {
        /// <summary>
        /// Initializes the <c>get next validators</c> command.
        /// </summary>
        public GetNextValidatorsCommand() : base("validators", "List the validators for the next block.")
        {
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>get accountstate</c>.
    /// </summary>
    internal class GetAccountStateCommand : Command
    {
        /// <summary>
        /// Initializes the <c>get accountstate</c> command.
        /// </summary>
        public GetAccountStateCommand() : base("accountstate", "Show the state of an account.")
        {
            Add(CommandArgument.Required("address", "Account script hash."));
        }
    }
}
