using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
    public enum pid_t  {}



    // "X:\util\android-ndk-r10e\platforms\android-21\arch-arm\usr\include\unistd.h"

    [Script(IsNative = true, Header = "unistd.h", IsSystemHeader = true)]
    public unsafe static class unistd
    {

        public static void _exit(int i) { throw null; }

        public static pid_t gettid() { throw null; }

        //public static int usleep(useconds_t);
        public static int usleep(int i) { return 0; }
    }

}
