using java.net;
using java.security;
using ScriptCoreLib;

namespace javax.net.ssl
{
    // http://developer.android.com/reference/javax/net/ssl/SSLSocket.html
    // http://docs.oracle.com/javase/7/docs/api/javax/net/ssl/SSLSocket.html
    [Script(IsNative = true)]
    public abstract class SSLSocket : Socket
    {
        public abstract void addHandshakeCompletedListener(HandshakeCompletedListener listener);


        public abstract void startHandshake();


        public abstract void setWantClientAuth(bool want);
        public abstract void setNeedClientAuth(bool need);
    }
}
