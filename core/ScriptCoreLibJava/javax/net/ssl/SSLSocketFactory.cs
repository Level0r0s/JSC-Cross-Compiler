using java.net;
using java.security;
using ScriptCoreLib;

namespace javax.net.ssl
{
    // http://developer.android.com/reference/javax/net/ssl/SSLSocketFactory.html
    // http://docs.oracle.com/javase/7/docs/api/javax/net/ssl/SSLSocketFactory.html
    [Script(IsNative = true)]
    public abstract class SSLSocketFactory : SocketFactory
    {
        // Z:\jsc.svn\examples\java\hybrid\Test\JVMCLRSSLServerSocket\JVMCLRSSLServerSocket\Program.cs

        //  The default is defined by the security property 'ssl.SocketFactory.provider'.
        public static SocketFactory getDefault() { throw null; }


        // android?
        // Z:\jsc.svn\examples\java\hybrid\ubuntu\UbuntuTCPMultiplex\Program.cs
        public abstract Socket createSocket(Socket s, string host, int port, bool autoClose);

      
    }
}
