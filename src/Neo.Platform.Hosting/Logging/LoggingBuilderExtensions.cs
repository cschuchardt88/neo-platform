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

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;

namespace Neo.Platform.Hosting.Logging
{
    /// <summary>
    /// Extension methods for registering the Neo platform console logger.
    /// </summary>
    public static class LoggingBuilderExtensions
    {
        /// <summary>
        /// Adds Neo's platform logger named 'NeoPlatformLog' to the factory.
        /// </summary>
        /// <param name="builder">The extension method argument.</param>
        /// <returns>The <see cref="ILoggingBuilder"/> so that additional calls can be chained.</returns>
        public static ILoggingBuilder AddNeoPlatformLog(this ILoggingBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, NeoPlatformLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions<NeoPlatformLoggerOptions, NeoPlatformLoggerProvider>(builder.Services);

            return builder;
        }

        /// <summary>
        /// Adds Neo's platform logger. Use <paramref name="configure"/> to enable logging for specific log levels.
        /// </summary>
        /// <param name="builder">The extension method argument.</param>
        /// <param name="configure">A delegate to configure the <see cref="NeoPlatformLoggerOptions"/>.</param>
        /// <returns>The <see cref="ILoggingBuilder"/> so that additional calls can be chained.</returns>
        public static ILoggingBuilder AddNeoPlatformLog(this ILoggingBuilder builder, Action<NeoPlatformLoggerOptions> configure)
        {
            ArgumentNullException.ThrowIfNull(configure);

            builder.AddNeoPlatformLog();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
