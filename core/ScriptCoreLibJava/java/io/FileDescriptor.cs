using ScriptCoreLib;

namespace java.io
{
    // http://developer.android.com/reference/java/io/FileDescriptor.html
    [Script(IsNative=true)]
    public sealed class FileDescriptor
    {
        public void sync() { }
    }
}
