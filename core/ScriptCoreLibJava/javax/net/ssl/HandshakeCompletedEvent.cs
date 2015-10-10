using java.net;
using java.security;
using ScriptCoreLib;

namespace javax.net.ssl
{

    // http://developer.android.com/reference/javax/net/ssl/HandshakeCompletedEvent.html
    // http://docs.oracle.com/javase/7/docs/api/javax/net/ssl/HandshakeCompletedEvent.html
    [Script(IsNative = true)]
    public class HandshakeCompletedEvent : java.util.EventObject
    {
        public java.security.cert.Certificate[] getPeerCertificates() { throw null; }

    }
}
