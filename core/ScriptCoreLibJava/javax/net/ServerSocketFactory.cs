
using java.net;
using java.security;
using ScriptCoreLib;

namespace javax.net
{
    // http://developer.android.com/reference/javax/net/ServerSocketFactory.html
    [Script(IsNative = true)]
    public abstract class ServerSocketFactory
    {
        // Z:\jsc.svn\examples\java\hybrid\Test\JVMCLRSSLServerSocket\JVMCLRSSLServerSocket\Program.cs

        public abstract ServerSocket createServerSocket(int port);
        public abstract ServerSocket createServerSocket(int port, int backlog, InetAddress iAddress);
    }
}
