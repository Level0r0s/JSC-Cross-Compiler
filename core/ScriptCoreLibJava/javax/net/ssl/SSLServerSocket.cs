using java.net;
using java.security;
using ScriptCoreLib;

namespace javax.net.ssl
{
    // http://developer.android.com/reference/javax/net/ssl/SSLServerSocket.html
    // http://docs.oracle.com/javase/7/docs/api/javax/net/ssl/SSLServerSocket.html
    [Script(IsNative = true)]
    public abstract class SSLServerSocket : ServerSocket
    {
        // https://www.stunnel.org/index.html

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151009

        public abstract void setWantClientAuth(bool want);
        public abstract void setNeedClientAuth(bool need);

        public abstract void setEnabledProtocols(string[] protocols);

        public abstract void setEnabledCipherSuites(string[] suites);

        public abstract string[] getSupportedCipherSuites();

    }
}
