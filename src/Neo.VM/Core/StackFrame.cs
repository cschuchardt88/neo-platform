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

using Neo.VM.Types;

namespace Neo.VM.Core
{
    public class StackFrame : IDisposable
    {
        /// <summary>
        /// The evaluation stack for this frame
        /// </summary>
        public Stack<VMObject> EvaluationStack { get; } = [];

        /// <summary>
        /// Local variables for this frame (indexed)
        /// </summary>
        public List<VMObject> Locals { get; } = [];

        /// <summary>
        /// Alternative stack (used by some NeoVM instructions)
        /// </summary>
        public Stack<VMObject> AltStack { get; } = [];

        /// <summary>
        /// Address to return to after this frame completes
        /// </summary>
        public int ReturnAddress { get; }

        /// <summary>
        /// Parent frame (for nested calls)
        /// </summary>
        public StackFrame? Parent { get; }

        /// <summary>
        /// Whether this frame is currently executing
        /// </summary>
        public bool IsActive { get; private set; } = true;

        public StackFrame(int returnAddress = -1, StackFrame? parent = null)
        {
            ReturnAddress = returnAddress;
            Parent = parent;
        }

        public void Dispose()
        {
            Cleanup();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Push item onto evaluation stack with reference counting
        /// </summary>
        public void Push(VMObject item)
        {
            item.AddReference();
            EvaluationStack.Push(item);
        }

        /// <summary>
        /// Pop item from evaluation stack and release reference
        /// </summary>
        public VMObject Pop()
        {
            if (EvaluationStack.Count == 0)
                throw new InvalidOperationException("Evaluation stack underflow");

            var item = EvaluationStack.Pop();
            item.Release();
            return item;
        }

        /// <summary>
        /// Peek at top of evaluation stack without removing
        /// </summary>
        public VMObject Peek()
        {
            return EvaluationStack.Count > 0 ? EvaluationStack.Peek() : VMNull.Instance;
        }

        /// <summary>
        /// Set local variable at index (with proper ref counting)
        /// </summary>
        public void SetLocal(int index, VMObject value)
        {
            // Expand locals list if needed
            while (Locals.Count <= index)
                Locals.Add(VMNull.Instance);

            // Release old value
            Locals[index]?.Release();

            // Add new value
            value.AddReference();
            Locals[index] = value;
        }

        /// <summary>
        /// Get local variable at index
        /// </summary>
        public VMObject GetLocal(int index)
        {
            return (index >= 0 && index < Locals.Count) ? Locals[index] : VMNull.Instance;
        }

        /// <summary>
        /// Checks if this stack frame contains any circular references
        /// </summary>
        public bool HasCircularReference()
        {
            var visited = new HashSet<VMObject>();
            return DetectCycleInFrame(this, visited);
        }

        /// <summary>
        /// Clean up this frame (release all references)
        /// </summary>
        public void Cleanup()
        {
            // Release evaluation stack
            while (EvaluationStack.Count > 0)
            {
                var item = EvaluationStack.Pop();
                item?.Release();
            }

            // Release alt stack
            while (AltStack.Count > 0)
            {
                var item = AltStack.Pop();
                item?.Release();
            }

            // Release locals
            for (var i = 0; i < Locals.Count; i++)
            {
                Locals[i]?.Release();
                Locals[i] = VMNull.Instance;
            }

            IsActive = false;
        }

        private static bool DetectCycleInFrame(StackFrame frame, HashSet<VMObject> visited)
        {
            if (frame == null) return false;

            // Check Evaluation Stack
            foreach (var item in frame.EvaluationStack)
            {
                if (DetectCycle(item, visited))
                    return true;
            }

            // Check Alt Stack
            foreach (var item in frame.AltStack)
            {
                if (DetectCycle(item, visited))
                    return true;
            }

            // Check Locals
            foreach (var item in frame.Locals)
            {
                if (DetectCycle(item, visited))
                    return true;
            }

            return false;
        }

        private static bool DetectCycle(VMObject current, HashSet<VMObject> visited)
        {
            if (current == null) return false;
            if (visited.Contains(current)) return true;   // Cycle found!

            visited.Add(current);

            // Recursively check children
            foreach (var child in current.GetChildren())
            {
                if (DetectCycle(child, visited))
                    return true;
            }

            visited.Remove(current);
            return false;
        }
    }
}
