using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.os
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/os/ParcelFileDescriptor.java
    // http://developer.android.com/reference/android/os/ParcelFileDescriptor.html
    [Script(IsNative = true)]
    public class ParcelFileDescriptor : Parcelable
    {
        public static readonly int MODE_READ_WRITE = 0x30000000;

        // private final FileDescriptor mFd;
        public java.io.FileDescriptor getFileDescriptor()
        {
            return null;
        }

        public long getStatSize() { return 0; }

        public int getFd() { return 0; }

        public static ParcelFileDescriptor fromSocket(java.net.Socket socket)
        {
            return null;
        }

        public static ParcelFileDescriptor dup(java.io.FileDescriptor orig)
        {
            return null;
        }

        public int describeContents()
        {
            throw new NotImplementedException();
        }

        public void writeToParcel(Parcel dest, int flags)
        {
            throw new NotImplementedException();
        }



         //private static FileDescriptor openInternal(File file, int mode

        public static ParcelFileDescriptor open(java.io.File file, int mode)
        {
            return null;
        }
    }
}
