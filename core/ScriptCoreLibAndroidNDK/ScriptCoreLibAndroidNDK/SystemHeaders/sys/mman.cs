using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.sys
{
    // "R:\util\android-ndk-r10e\platforms\android-21\arch-arm64\usr\include\sys\mman.h"


    [Script(IsNative = true, Header = "sys/mman.h", IsSystemHeader = true)]
    public static class mman_h
    {
        // http://alvinalexander.com/java/jwarehouse/android/core/java/android/os/MemoryFile.java.shtml
        // http://man7.org/linux/man-pages/man2/mmap.2.html
        // http://en.wikipedia.org/wiki/Memory-mapped_file
        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\android\os\MemoryFile.cs

        // private static native FileDescriptor native_open(String name, int length) throws IOException;

        //extern void* mmap(void*, size_t, int, int, int, off_t);
    }

}
