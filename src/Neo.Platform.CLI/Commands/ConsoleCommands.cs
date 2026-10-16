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
    /// Groups the neo-node <c>console</c> commands.
    /// </summary>
    internal class ConsoleCommandGroup : Command
    {
        /// <summary>
        /// Initializes the <c>console</c> command.
        /// </summary>
        public ConsoleCommandGroup() : base("console", "Control console logging.")
        {
            Add(new ConsoleLogCommand());
        }
    }

    /// <summary>
    /// Groups the neo-node <c>console log</c> commands.
    /// </summary>
    internal class ConsoleLogCommand : Command
    {
        /// <summary>
        /// Initializes the <c>console log</c> command.
        /// </summary>
        public ConsoleLogCommand() : base("log", "Turn console logging on or off.")
        {
            Add(new ConsoleLogOffCommand());
            Add(new ConsoleLogOnCommand());
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>console log off</c>.
    /// </summary>
    internal class ConsoleLogOffCommand : Command
    {
        /// <summary>
        /// Initializes the <c>console log off</c> command.
        /// </summary>
        public ConsoleLogOffCommand() : base("off", "Turn console logging off.")
        {
        }
    }

    /// <summary>
    /// CLI command for neo-node <c>console log on</c>.
    /// </summary>
    internal class ConsoleLogOnCommand : Command
    {
        /// <summary>
        /// Initializes the <c>console log on</c> command.
        /// </summary>
        public ConsoleLogOnCommand() : base("on", "Turn console logging on.")
        {
        }
    }
}
