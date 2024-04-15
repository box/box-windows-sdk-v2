using System;
using System.Runtime.CompilerServices;

namespace Box.V2.Utility
{
    /// <summary>
    /// A thread safe implementation of <see cref="Random"/>, following best practices
    /// for .NET Framework and .NET Standard.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-random"/>
    internal static class ThreadSafeRandom
    {
        //
        // Adapted from https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/Random.cs
        //
        // NOTE: when/if this library updates to .NET 6+, this code can be replaced with `Random.Shared`
        //

        [ThreadStatic]
        private static Random _random;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static Random CreateRandom() => _random = new Random();

        /// <summary>
        /// An instance of <see cref="Random"/> specific to the calling thread.
        /// Do not pass this instance to other threads or contexts.
        /// </summary>
        public static Random Instance => _random ?? CreateRandom();
    }
}
