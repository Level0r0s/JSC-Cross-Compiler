using java.security;
using ScriptCoreLib;

namespace javax.net.ssl
{
    // http://developer.android.com/reference/javax/net/ssl/SSLContext.html
    // http://docs.oracle.com/javase/7/docs/api/javax/net/ssl/SSLContext.html
    [Script(IsNative = true)]
    public class SSLContext
    {
        // Z:\jsc.svn\examples\java\hybrid\Test\JVMCLRSSLServerSocket\JVMCLRSSLServerSocket\Program.cs
        public SSLServerSocketFactory getServerSocketFactory () {throw null;}



        public SSLSocketFactory getSocketFactory() { return null; }
        public void init(KeyManager[] km, TrustManager[] tm, SecureRandom sr)
        {
        }



        // tested by?
        public static SSLContext getInstance(string protocol) { return null; }
    }
}
