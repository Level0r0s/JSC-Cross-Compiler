using java.net;
using java.security;
using ScriptCoreLib;

namespace javax.net.ssl
{
    // http://developer.android.com/reference/javax/net/ssl/SSLServerSocketFactory.html
    // http://docs.oracle.com/javase/7/docs/api/javax/net/ssl/SSLServerSocketFactory.html
    [Script(IsNative = true)]
    public abstract class SSLServerSocketFactory : ServerSocketFactory
    {
        // Z:\jsc.svn\examples\java\hybrid\Test\JVMCLRSSLServerSocket\JVMCLRSSLServerSocket\Program.cs

        //  The default is defined by the security property 'ssl.SocketFactory.provider'.
        public static ServerSocketFactory getDefault() { throw null; }



    }
}
