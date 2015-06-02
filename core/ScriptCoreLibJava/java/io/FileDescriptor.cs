using ScriptCoreLib;

namespace java.io
{
    // https://android.googlesource.com/platform/libcore/+/master/luni/src/main/java/java/io/FileDescriptor.java
    // http://developer.android.com/reference/java/io/FileDescriptor.html
    [Script(IsNative=true)]
    public sealed class FileDescriptor
    {
        // in a multiprocess apk. can we serialize/re activate such a class?
        internal int descriptor = -1;

        public void sync() { }
    }
}
