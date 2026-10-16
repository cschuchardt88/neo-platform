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

using System;

namespace Neo.Core.Storage
{
    /// <summary>
    /// Creates, purges, and restores store backups.
    /// </summary>
    public interface IStoreBackup : IDisposable
    {
        /// <summary>
        /// Creates a new backup of the database.
        /// </summary>
        void Backup();

        /// <summary>
        /// Removes older backups, retaining the specified number of most recent ones.
        /// </summary>
        /// <param name="maxBackupsToKeep">The maximum number of most recent backups to retain.</param>
        void Purge(uint maxBackupsToKeep);

        // TODO: Add a method to verify the integrity of the latest backup before restoring it.
        //       Add a method to restore with "backupId" and "restorePath" parameters, and optionally a "walPath" and "restoreOptions" parameter.

        /// <summary>
        /// Restores the latest backup to the specified path.
        /// </summary>
        /// <param name="restoreDir">The directory that will receive the restored database.</param>
        /// <param name="walDir">Optional WAL directory. When omitted, <paramref name="restoreDir"/> is used.</param>
        void Restore(string restoreDir, string? walDir = null); // TODO: Add new parameter that allows verification before restoring.
    }
}
