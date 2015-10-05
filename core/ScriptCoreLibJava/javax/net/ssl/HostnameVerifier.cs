using java.net;
using java.security;
using ScriptCoreLib;

namespace javax.net.ssl
{
    // http://developer.android.com/reference/javax/net/ssl/HostnameVerifier.html
    // http://docs.oracle.com/javase/7/docs/api/javax/net/ssl/HostnameVerifier.html
    [Script(IsNative = true)]
    public interface HostnameVerifier
    {

        bool verify(string hostname, SSLSession session);
       
    }
}
