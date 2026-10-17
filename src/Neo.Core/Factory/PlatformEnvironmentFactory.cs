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

using System.IO;
using System.Threading;

namespace Neo.Core.Factory
{
    /// <summary>
    /// Helper factory methods used when constructing Neo platform environments.
    /// </summary>
    public static class PlatformEnvironmentFactory
    {
        private static readonly uint s_networkSeed = 810960196u; // DEV0 Magic Code
        private static readonly string s_globalString = "Global\\";

        /// <summary>
        /// Computes a development network magic value for the given index.
        /// </summary>
        /// <param name="index">Development network index embedded in the high nibble of the magic.</param>
        /// <returns>The derived network magic number.</returns>
        public static uint GetDevNetwork(uint index)
            => s_networkSeed & ~(0xfu << 24) | (index << 24);

        /// <summary>
        /// Creates a named system mutex.
        /// </summary>
        /// <param name="name">
        /// Optional mutex name; when <see langword="null"/> or empty a random global name is used.
        /// <para>
        /// The name is case-sensitive and is prefixed with "Global\" to ensure the mutex is visible across all sessions on the machine.
        /// </para>
        /// </param>
        /// <returns>
        /// A newly created <see cref="Mutex"/> that is initially owned.
        /// </returns>
        public static Mutex CreateMutex(string? name = default)
        {
            if (string.IsNullOrEmpty(name))
                name = Path.Combine(s_globalString, Path.GetRandomFileName());
            return new(initiallyOwned: true, name);
        }
    }
}
