using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
    // "X:\util\android-ndk-r10e\platforms\android-21\arch-arm\usr\include\malloc.h"


    [Script(IsNative = true, Header = "malloc.h", IsSystemHeader = true, PointerName = "struct mallinfo")]
    public struct mallinfo
    {
        // http://stackoverflow.com/questions/17109284/how-to-find-memory-usage-of-my-android-application-written-c-using-ndk

        // http://man7.org/linux/man-pages/man3/mallinfo.3.html

        public size_t arena;    /* non-mmapped space allocated from system */
        public size_t ordblks;  /* number of free chunks */
        public size_t smblks;   /* always 0 */
        public size_t hblks;    /* always 0 */
        public size_t hblkhd;   /* space in mmapped regions */
        public size_t usmblks;  /* maximum total allocated space */
        public size_t fsmblks;  /* always 0 */
        public size_t uordblks; /* total allocated space */
        public size_t fordblks; /* total free space */
        public size_t keepcost; /* releasable (via malloc_trim) space */
    };


    [Script(IsNative = true, Header = "malloc.h", IsSystemHeader = true)]
    public unsafe static class malloc_h
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Exception.cs

        // tested by?
        public static mallinfo mallinfo() { throw null; }

        public static void malloc_stats() { throw null; }
    }

}
