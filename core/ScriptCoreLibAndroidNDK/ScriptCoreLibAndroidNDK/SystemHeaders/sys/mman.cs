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
        // https://lkml.org/lkml/2014/12/1/927

        // https://android.googlesource.com/platform/libnativehelper/+/master/JNIHelp.cpp
        // https://android.googlesource.com/platform/libnativehelper/+/master/include/nativehelper/JNIHelp.h

        // https://github.com/android/platform_frameworks_base/blob/master/core/jni/android_os_MemoryFile.cpp
        // http://alvinalexander.com/java/jwarehouse/android/core/java/android/os/MemoryFile.java.shtml
        // http://man7.org/linux/man-pages/man2/mmap.2.html
        // http://en.wikipedia.org/wiki/Memory-mapped_file
        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\android\os\MemoryFile.cs

        // private static native FileDescriptor native_open(String name, int length) throws IOException;

        //extern void* mmap(void*, size_t, int, int, int, off_t);
        // int fd = jniGetFDFromFileDescriptor(env, fileDescriptor);
        //   void* result = mmap(NULL, length, prot, MAP_SHARED, fd, 0);
        // X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\Program.cs

        //void *mmap(void *addr, size_t length, int prot, int flags, int fd, off_t offset);
        public static object mmap(object addr, int length, int prot, int flags, int descriptor, int offset) { return 0; }

    }

}
