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

using Neo.VM.Interfaces;
using System.IO.Hashing;
using System.Numerics;

namespace Neo.VM.Types
{
    public abstract class VMObject : IVMComponent, IEquatable<VMObject>
    {
        public abstract VMObjectType Type { get; }

        public int Size => _memory.Length;
        public int RefCount => _refCount;

        private bool _disposed = false;
        private int _refCount = 1;

        protected ReadOnlyMemory<byte> _memory;

        #region IEquatable

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, this)) return true;
            return Equals(obj as VMObject);
        }

        public bool Equals(VMObject? other)
        {
            if (other is null) return false;
            if (Type != other.Type) return false;
            return _memory.Span.SequenceEqual(other._memory.Span);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Crc32.HashToUInt32(_memory.Span), RefCount);
        }

        #endregion

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Cleanup managed resources
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~VMObject()
        {
            Dispose(false);
        }

        #endregion

        #region References

        public void AddReference()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            Interlocked.Increment(ref _refCount);
        }

        public void Release()
        {
            if (_disposed) return;

            var newCount = Interlocked.Decrement(ref _refCount);
            if (newCount <= 0)
            {
                Dispose(true);
            }
        }

        #endregion

        /// <summary>
        /// Returns a deep clone of this object
        /// </summary>
        public abstract VMObject Clone();

        /// <summary>
        /// Converts this object to a <see cref="ReadOnlySpan{T}"/> (for serialization/storage)
        /// </summary>
        public virtual ReadOnlySpan<byte> GetReadOnlySpan()
        {
            return _memory.ToArray();
        }

        /// <summary>
        /// Converts this object to a signed integer (if possible)
        /// </summary>
        public virtual BigInteger GetInteger()
        {
            throw new InvalidOperationException($"Cannot convert {Type} to integer");
        }

        /// <summary>
        /// Converts this object to a boolean
        /// </summary>
        public virtual bool GetBoolean()
        {
            return true; // NeoVM treats most non-null objects as true
        }

    }
}
