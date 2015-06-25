using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders.linux;

namespace ScriptCoreLibNative.SystemHeaders
{

    // "X:\util\android-ndk-r10e\platforms\android-21\arch-arm64\usr\include\time.h"

    [Script(IsNative = true, Header = "linux/time.h", IsSystemHeader = true, PointerName = "struct timespec")]
    //[Script(IsNative = true, Header = "linux/time.h", IsSystemHeader = true, ExternalTarget = "struct timespec")]
    public struct timespec
    {
        //time_t tv_sec;        /* seconds */
        public uint tv_sec;        /* seconds */
        public ulong tv_nsec;       /* nanoseconds */
    };



    [Script(IsNative = true, Header = "time.h", IsSystemHeader = true)]
    public unsafe static class time_h
    {
        public const int CLOCK_MONOTONIC = 1;

        public static int clock_gettime(int clk_id, out timespec tp) { throw null; }


    }

}
