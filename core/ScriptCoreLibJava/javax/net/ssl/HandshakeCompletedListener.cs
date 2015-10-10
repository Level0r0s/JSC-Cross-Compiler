using java.net;
using java.security;
using ScriptCoreLib;

namespace javax.net.ssl
{
    // http://developer.android.com/reference/javax/net/ssl/HandshakeCompletedListener.html
    // http://docs.oracle.com/javase/7/docs/api/javax/net/ssl/HandshakeCompletedListener.html
    [Script(IsNative = true)]
    public interface HandshakeCompletedListener
    {

        void handshakeCompleted(HandshakeCompletedEvent _event);
    }
}
