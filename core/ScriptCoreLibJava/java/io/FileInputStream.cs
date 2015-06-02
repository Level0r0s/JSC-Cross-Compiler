using ScriptCoreLib;

namespace java.io
{
    // https://android.googlesource.com/platform/libcore/+/master/luni/src/main/java/java/io/FileInputStream.java
    //  http://java.sun.com/j2se/1.4.2/docs/api/java/io/FileInputStream.html
    // http://developer.android.com/reference/java/io/FileInputStream.html
    [Script(IsNative = true)]
    public class FileInputStream : InputStream
    {
        public FileInputStream(File file)
        {
        }

        public FileInputStream(FileDescriptor file)
        {
        }

        public FileInputStream(string name)
        {
        }

  

        // X:\jsc.svn\examples\java\android\test\TestMultiProcMemoryFile\TestMultiProcMemoryFile\ApplicationActivity.cs

        public override int read()
        {
            return default(int);
        }
    }
}
