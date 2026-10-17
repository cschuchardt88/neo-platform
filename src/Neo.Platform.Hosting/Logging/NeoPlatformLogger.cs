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

using Microsoft.Extensions.Logging;
using Neo.Core;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Neo.Platform.Hosting.Logging
{
    /// <summary>
    /// Console logger implementation used by the Neo platform host.
    /// </summary>
    internal sealed class NeoPlatformLogger(
        string name,
        NeoPlatformLifetimeOptions platformLifetimeOptions,
        Func<NeoPlatformLoggerOptions> config) : ILogger
    {
        private DateTime DateTimeNow => _getConfig().UseUtcTimestamp ? DateTime.UtcNow : DateTime.Now;
        private bool ShowExceptionStackTrace => _getConfig().ShowExceptionStackTrace;

        private readonly string _name = name;
        private readonly Func<NeoPlatformLoggerOptions> _getConfig = config;
        private readonly bool _suppressLoggerMessages = platformLifetimeOptions.SuppressStatusMessages;

        /// <inheritdoc />
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
            => default!;

        /// <inheritdoc />
        public bool IsEnabled(LogLevel logLevel)
            => !_suppressLoggerMessages && logLevel != LogLevel.None;

        /// <inheritdoc />
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (_suppressLoggerMessages)
                return;

            if (IsEnabled(logLevel) == false)
                return;

            ArgumentNullException.ThrowIfNull(formatter);

            var message = formatter(state, exception);
            var messageIsEmpty = string.IsNullOrEmpty(message);

            if (messageIsEmpty && exception is null)
                return;
            else if (messageIsEmpty == false)
                message = $"{_name}[{eventId.Name ?? $"{eventId.Id:d}"}] {message}";

            switch (logLevel)
            {
                case LogLevel.Trace:
                    if (messageIsEmpty == false)
                        TraceMessage("{0}", message);
                    break;
                case LogLevel.Debug:
                    if (messageIsEmpty == false)
                        DebugMessage("{0}", message);
                    break;
                case LogLevel.Information:
                    if (messageIsEmpty == false)
                        InfoMessage("{0}", message);
                    break;
                case LogLevel.Warning:
                    if (messageIsEmpty == false)
                        WarningMessage("{0}", message);
                    break;
                case LogLevel.Error:
                case LogLevel.Critical:
                    if (messageIsEmpty == false)
                        ErrorMessage("{0}", message);
                    if (exception is not null)
                        ErrorMessage(exception);
                    break;
                default:
                    break;
            }
        }

        private void WriteDateTime(bool isError = false)
        {
            var format = $"[{BuildFormatString(_getConfig().TimestampFormat)}] ";
            if (isError)
                ConsoleUtilities.ErrorWrite(format, DateTimeNow);
            else
                ConsoleUtilities.Write(format, DateTimeNow);
        }

        /// <summary>
        /// Writes an information-level message with platform coloring.
        /// </summary>
        public void InfoMessage([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format, params object?[] args)
        {
            WriteDateTime();
            ConsoleUtilities.SetTerminalForegroundColor(ConsoleColor.Blue);
            ConsoleUtilities.Write($"Info: ");
            ConsoleUtilities.SetTerminalForegroundColor(ConsoleColor.White);
            ConsoleUtilities.WriteLine(format, args);
            ConsoleUtilities.ResetTerminalColors();
        }

        /// <summary>
        /// Writes a warning-level message with platform coloring.
        /// </summary>
        public void WarningMessage([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format, params object?[] args)
        {
            WriteDateTime();
            ConsoleUtilities.SetTerminalForegroundColor(ConsoleColor.Yellow);
            ConsoleUtilities.Write("Warn: ");
            ConsoleUtilities.SetTerminalForegroundColor(ConsoleColor.White);
            ConsoleUtilities.WriteLine(format, args);
            ConsoleUtilities.ResetTerminalColors();
        }

        /// <summary>
        /// Writes a debug-level message with platform coloring.
        /// </summary>
        public void DebugMessage([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format, params object?[] args)
        {
            ConsoleUtilities.SetTerminalForegroundColor(ConsoleColor.DarkGray);
            WriteDateTime();
            ConsoleUtilities.Write("Debug: ");
            ConsoleUtilities.WriteLine(format, args);
            ConsoleUtilities.ResetTerminalColors();
        }

        /// <summary>
        /// Writes a trace-level message with platform coloring.
        /// </summary>
        public void TraceMessage([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format, params object?[] args)
        {
            ConsoleUtilities.SetTerminalForegroundColor(ConsoleColor.DarkGray);
            WriteDateTime();
            ConsoleUtilities.Write("Trace: ");
            ConsoleUtilities.WriteLine(format, args);
            ConsoleUtilities.ResetTerminalColors();
        }

        /// <summary>
        /// Writes exception details to standard error, optionally including a stack trace.
        /// </summary>
        /// <param name="exception">Exception to report.</param>
        public void ErrorMessage(Exception exception)
        {
            exception = CoreUtilities.GetRootException(exception);

            ConsoleUtilities.SetTerminalForegroundColor(ConsoleColor.Red);
            WriteDateTime(isError: true);
            ConsoleUtilities.SetTerminalForegroundColor(ConsoleColor.DarkRed);
            ConsoleUtilities.ErrorWriteLine("{0}: ", exception.GetType().Name);
            ConsoleUtilities.SetTerminalForegroundColor(ConsoleColor.Red);
            ConsoleUtilities.ErrorWriteLine("   {0}", exception.Message);

            if (string.IsNullOrEmpty(exception.StackTrace) == false
                    && ShowExceptionStackTrace)
            {
                ConsoleUtilities.ErrorWriteLine("Stack Trace: ");
                ConsoleUtilities.SetTerminalForegroundColor(ConsoleColor.DarkRed);
                ConsoleUtilities.ErrorWriteLine("   {0}", exception.StackTrace.Trim());
            }

            ConsoleUtilities.ResetTerminalColors();
        }

        /// <summary>
        /// Writes an error-level formatted message to standard error.
        /// </summary>
        public void ErrorMessage([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format, params object?[] args)
        {
            ConsoleUtilities.SetTerminalForegroundColor(ConsoleColor.Red);
            WriteDateTime(isError: true);
            ConsoleUtilities.SetTerminalForegroundColor(ConsoleColor.DarkRed);
            ConsoleUtilities.ErrorWrite("Error: ");
            ConsoleUtilities.SetTerminalForegroundColor(ConsoleColor.Red);
            ConsoleUtilities.ErrorWriteLine(format, args);
            ConsoleUtilities.ResetTerminalColors();
        }

        private static string BuildFormatString(string format, int index = 0)
            => $"{{{index}:{format}}}";
    }
}
