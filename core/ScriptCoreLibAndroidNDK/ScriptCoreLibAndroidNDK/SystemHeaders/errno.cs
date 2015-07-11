using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
    // "X:\util\android-ndk-r10e\platforms\android-21\arch-arm\usr\include\errno.h"

    [Script(IsNative = true, Header = "errno.h", IsSystemHeader = true)]
    public unsafe static class errno_h
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Exception.cs


        public static string strerror(int errnum) { return default(string); }

        /* internal function returning the address of the thread-specific errno */
        public static int* __errno() { return default(int*); }



        /* a macro expanding to the errno l-value */
        //#define errno   (*__errno())
    }

}
