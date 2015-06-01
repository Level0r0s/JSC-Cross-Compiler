using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.os
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/os/MemoryFile.java
    // http://developer.android.com/reference/android/os/MemoryFile.html
    // http://alvinalexander.com/java/jwarehouse/android/core/java/android/os/MemoryFile.java.shtml
    // https://cells-source.cs.columbia.edu/plugins/gitiles/platform/frameworks/base/+/e331644cb570e74a8739cb21ffcc5875663ffa58/core/java/android/os/MemoryFile.java

    [Script(IsNative = true)]
    public class MemoryFile
    {
        // https://code.google.com/p/android/issues/detail?id=37372
        // http://src.chromium.org/svn/trunk/src/base/memory/shared_memory_posix.cc

        // http://www.programcreek.com/java-api-examples/index.php?api=android.os.MemoryFile

        // MemoryFile is a wrapper for the Linux ashmem driver.
        //  // mmap(2) protection flags from <sys/mman.h>


        public MemoryFile(string name, int length)
        {
            // native_open

            // public api wont allow to repopen memory?
        }


        // ??
        public MemoryFile(java.io.FileDescriptor fd, int length, String mode)
        {
            // native_mmap
        }

        // ??
        public java.io.FileDescriptor getFileDescriptor() { return null; }

        public static int getSize(java.io.FileDescriptor fd) { return 0; }

        public int readBytes(byte[] buffer, int srcOffset, int destOffset, int count)
        { return 0; }

        public void writeBytes(byte[] buffer, int srcOffset, int destOffset, int count)
        {

        }

        public java.io.InputStream getInputStream() { return null; }

        public java.io.OutputStream getOutputStream() { return null; }

    }
}
