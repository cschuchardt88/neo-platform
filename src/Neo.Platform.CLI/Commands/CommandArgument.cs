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
    /// Builds command arguments for neo-node command scaffolding.
    /// </summary>
    internal static class CommandArgument
    {
        /// <summary>
        /// Creates a required string argument.
        /// </summary>
        /// <param name="name">The argument name shown in help.</param>
        /// <param name="description">The argument description shown in help.</param>
        /// <returns>The required argument.</returns>
        public static Argument<string> Required(string name, string description)
        {
            return new Argument<string>(name)
            {
                Description = description
            };
        }

        /// <summary>
        /// Creates an optional string argument.
        /// </summary>
        /// <param name="name">The argument name shown in help.</param>
        /// <param name="description">The argument description shown in help.</param>
        /// <param name="defaultValue">The value used when the argument is omitted.</param>
        /// <returns>The optional argument.</returns>
        public static Argument<string> Optional(string name, string description, string defaultValue = "")
        {
            return new Argument<string>(name)
            {
                Description = description,
                DefaultValueFactory = _ => defaultValue
            };
        }

        /// <summary>
        /// Creates a string-array argument.
        /// </summary>
        /// <param name="name">The argument name shown in help.</param>
        /// <param name="description">The argument description shown in help.</param>
        /// <param name="required"><see langword="true"/> when at least one value is required.</param>
        /// <returns>The array argument.</returns>
        public static Argument<string[]> Many(string name, string description, bool required = true)
        {
            var argument = new Argument<string[]>(name)
            {
                Description = description,
                Arity = required ? ArgumentArity.OneOrMore : ArgumentArity.ZeroOrMore
            };

            if (required == false)
                argument.DefaultValueFactory = static _ => [];

            return argument;
        }

        /// <summary>
        /// Creates an optional option that accepts zero or more values.
        /// </summary>
        /// <param name="name">The option name, including the leading dashes.</param>
        /// <param name="description">The option description shown in help.</param>
        /// <returns>The option.</returns>
        public static Option<string[]> OptionalMany(string name, string description)
        {
            return new Option<string[]>(name)
            {
                Description = description,
                Arity = ArgumentArity.ZeroOrMore
            };
        }
    }
}
