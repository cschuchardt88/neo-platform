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

using Neo.Configuration.Json;

namespace Neo.Configuration
{
    /// <summary>
    /// Configuration options for a running host environment application.
    /// </summary>
    public class HostingEnvironmentOptions : JsonModel
    {
        /// <summary>
        /// Gets or sets the name of the environment.
        /// </summary>
        /// <remarks>
        /// This value is case-sensitive and should be one of the well-known environment names.
        /// </remarks>
        public string EnvironmentName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the application. 
        /// </summary>
        /// <remarks>
        /// This property is automatically set by the host to the assembly containing the application entry point.
        /// </remarks>
        public string ApplicationName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the absolute path to the directory that contains the application content files.
        /// </summary>
        /// <remarks>
        /// Must be a valid fully qualified path to a directory.
        /// </remarks>
        public string ContentRootPath { get; set; } = string.Empty;
    }
}
