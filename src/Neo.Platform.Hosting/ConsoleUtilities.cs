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
using System.Diagnostics.CodeAnalysis;

namespace Neo.Platform.Hosting
{
    internal class ConsoleUtilities
    {
        private static readonly bool s_terminalColorsAreSupported = IsTerminalColorsSupported();

        /// <summary>
        /// Sets the terminal's foreground color to the specified <paramref name="consoleColor"/>.
        /// </summary>
        /// <param name="consoleColor">The color to set.</param>
        public static void SetTerminalForegroundColor(ConsoleColor consoleColor)
        {
            if (s_terminalColorsAreSupported)
            {
                Console.ForegroundColor = consoleColor;
            }
        }

        /// <summary>
        /// Sets the terminal's background color to the specified <paramref name="consoleColor"/>.
        /// </summary>
        /// <param name="consoleColor">The color to set.</param>
        public static void SetTerminalBackgroundColor(ConsoleColor consoleColor)
        {
            if (s_terminalColorsAreSupported)
            {
                Console.BackgroundColor = consoleColor;
            }
        }

        /// <summary>
        /// Resets the terminal's foreground and background colors to their defaults.
        /// </summary>
        public static void ResetTerminalColors()
        {
            if (s_terminalColorsAreSupported)
            {
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Writes a formatted message to standard output without a trailing newline.
        /// </summary>
        public static void Write([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format, params object?[] args)
        {
            if (IsOutputColorsSupported() == false)
                ResetTerminalColors();

            Console.Out.Write(string.Format(format, args));
        }

        /// <summary>
        /// Writes a formatted message to standard error without a trailing newline.
        /// </summary>
        public static void ErrorWrite([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format, params object?[] args)
        {
            if (IsErrorColorsSupported() == false)
                ResetTerminalColors();

            Console.Error.Write(string.Format(format, args));
        }

        /// <summary>
        /// Writes a newline to standard output.
        /// </summary>
        public static void WriteLine()
            => Write(Environment.NewLine);

        /// <summary>
        /// Writes a formatted message to standard output followed by a newline.
        /// </summary>
        public static void WriteLine([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format, params object?[] args)
            => Write(string.Format(format, args) + Environment.NewLine);

        /// <summary>
        /// Writes a newline to standard error.
        /// </summary>
        public static void ErrorWriteLine()
            => ErrorWrite(Environment.NewLine);

        /// <summary>
        /// Writes a formatted message to standard error followed by a newline.
        /// </summary>
        public static void ErrorWriteLine([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format, params object?[] args)
            => ErrorWrite(string.Format(format, args) + Environment.NewLine);

        /// <summary>
        /// Determines whether standard output stream supports colors.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if standard output stream supports colors; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsOutputColorsSupported()
            => s_terminalColorsAreSupported && !Console.IsOutputRedirected;

        /// <summary>
        /// Determines whether standard error stream supports colors.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if standard error stream supports colors; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsErrorColorsSupported()
            => s_terminalColorsAreSupported && !Console.IsErrorRedirected;

        private static bool IsTerminalColorsSupported()
            => !(OperatingSystem.IsBrowser()
                    || OperatingSystem.IsAndroid()
                    || OperatingSystem.IsIOS()
                    || OperatingSystem.IsTvOS());
    }
}
