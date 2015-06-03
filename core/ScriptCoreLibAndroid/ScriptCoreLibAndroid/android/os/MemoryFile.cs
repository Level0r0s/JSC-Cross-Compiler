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
    // https://cells-source.cs.columbia.edu/plugins/gitiles/platform/frameworks/base/+/master/core/jni/android_os_MemoryFile.cpp
    // https://android.googlesource.com/platform/frameworks/base.git/+/android-4.3_r2.1/core/jni/android_os_MemoryFile.cpp
    // https://github.com/android/platform_frameworks_base/blob/master/core/jni/android_os_MemoryFile.cpp
    // https://github.com/pelya/android-shmem

    [Script(IsNative = true)]
    public class MemoryFile
    {
        // "X:\opensource\github\libancillary\fd_recv.c"

        // http://www.normalesup.org/~george/comp/libancillary/
        // https://chromium.googlesource.com/android_tools/+/master/ndk/sources/android/crazy_linker/README.TXT

        // you don't need Binder, you can pass a file descriptor using sendmsg() and recvmsg() through a Unix local socket.
        // https://chromium.googlesource.com/android_tools/+/master/ndk/sources/android/crazy_linker/tests/test_util.h


        // LOCAL_LDLIBS :=  -lcutils
        // http://android.2317887.n4.nabble.com/ashmem-usage-td220224.html

        // http://stackoverflow.com/questions/22436414/how-to-pass-file-descriptor-to-ashmem-between-processes
        // http://stackoverflow.com/questions/12864778/shared-memory-region-in-ndk
        // https://lkml.org/lkml/2014/12/1/927

        // /proc/self/fd/3
        // /proc/43512/fd/5


        // http://stackoverflow.com/questions/15809333/duplicate-file-descriptor-of-another-process-in-linux-without-sendmsg

        // http://man7.org/linux/man-pages/man2/dup.2.html
        // http://markmail.org/message/cv2ai2kt5k5ithgs

        // http://stackoverflow.com/questions/22496130/ipc-how-to-redirect-a-command-output-to-a-shared-memory-segment-in-child
        // http://stackoverflow.com/questions/9940086/sharing-file-descriptors-across-processes
        // http://markmail.org/message/j2boxourew3ypdui
        // http://developer.android.com/reference/android/os/FileObserver.html

        // !! NDK wont help us if the SDK wont implement the PDK methods we need.

        // can we activate this type and fill in the fields?

        //[System.Runtime.CompilerServices.Dynamic]
        internal java.io.FileDescriptor mFD;        // ashmem file descriptor
        internal long mAddress;   // address of ashmem memory
        internal int mLength;    // total length of our ashmem region
        internal bool mAllowPurging = false;  // true if our ashmem region is unpinned

        // But the memory pointer created by mmap is process dependent, and so is the file descriptor from ashmem_create_region, 
        // i.e. they are only valid in the same process they are created.

        internal static int native_mmap(java.io.FileDescriptor fd, int length, int mode) { return 0; }
        // mAddress = native_mmap(mFD, length, modeToProt(mode));



        // The $ character should be used only in mechanically generated source code or, rarely, to access preexisting names on legacy systems.

        // $AOSP/frameworks/base/core/jni/android\os_MemoryFile.cpp_.
        // https://vec.io/posts/andriod-ipc-shared-memory-with-ashmem-memoryfile-and-binder

        // $(ANDROID_TOP)/system/core/cutils/ashmem.h

        // https://code.google.com/p/volatility/wiki/AndroidMemoryForensics
        // http://sssslide.com/www.slideshare.net/tetsu.koba/interprocess-communication-of-android
        // https://github.com/android/platform_system_core/blob/master/include/cutils/ashmem.h
        //  platform library with the ndk rather than the platform sdk.

        // ashmem is part of the platform sdk!

        // ?
        // AndroidRuntime::registerNativeMethods(

        // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\sys\mman.cs
        // X:\jsc.svn\examples\java\android\future\NDKHybridMockup\NDKHybridMockup\ApplicationActivity.cs

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
