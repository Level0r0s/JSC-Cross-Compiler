using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.os
{
    // https://android.googlesource.com/platform/frameworks/native/+/master/include/binder/BinderService.h
    // https://android.googlesource.com/platform/frameworks/native/+/master/include/binder/Parcel.h
    // #include <binder/Parcel.h>
    // https://github.com/android/platform_frameworks_base/blob/master/core/jni/android_os_Parcel.cpp
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/os/Parcel.java
    // http://developer.android.com/reference/android/os/Parcel.html
    [Script(IsNative = true)]
    public class Parcel

    {
        // http://man.cat-v.org/p9p/3/sendfd
        // http://stackoverflow.com/questions/4489433/sending-file-descriptor-over-unix-domain-socket-and-select
        // https://vec.io/posts/andriod-ipc-shared-memory-with-ashmem-memoryfile-and-binder

        // private static native void nativeWriteFileDescriptor(long nativePtr, FileDescriptor val);

        //  magics to transfer a Linux file descriptor from one process to another.
        public void writeFileDescriptor(java.io.FileDescriptor val) { }
        //{
        //    nativeWriteFileDescriptor(mNativePtr, val);
        //}

        //private static native FileDescriptor nativeReadFileDescriptor(long nativePtr);
        public java.io.FileDescriptor readRawFileDescriptor() { return null; }

        //{
        //    return nativeReadFileDescriptor(mNativePtr);
        //}

        //    /*package*/ static native FileDescriptor dupFileDescriptor(FileDescriptor orig)

        // /*package*/ static native FileDescriptor openFileDescriptor(String file,
        // open
        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\android\os\ParcelFileDescriptor.cs

        // http://stackoverflow.com/questions/19209431/send-java-fd-via-unix-domain-sockets-using-jni
    }
}
