using ScriptCoreLib;

namespace java.io
{
    // https://android.googlesource.com/platform/libcore/+/master/luni/src/main/java/java/io/FileDescriptor.java
    // http://developer.android.com/reference/java/io/FileDescriptor.html
    [Script(IsNative=true)]
    public sealed class FileDescriptor
    {
        // http://mattias.niklewski.com/2014/03/binder.html
        // https://lkml.org/lkml/2009/6/25/3

        // File descriptors can be sent from one process to another by two means. One way is by inheritance, the other is by passing through a unix domain socket. 
        // http://www.thomasstover.com/uds.html

        // This is accomplished with the lower level socket function sendmsg() that accepts both arrays of IO vectors and control data message objects 

        // The third is scenarios where a server will hand a connection's file descriptor to another already started helper process of some kind.
        // Again this area is different from OS to OS. On Linux this is done with a socket feature known as ancillary data.

        // in a multiprocess apk. can we serialize/re activate such a class?
        internal int descriptor = -1;

        public void sync() { }
    }
}
