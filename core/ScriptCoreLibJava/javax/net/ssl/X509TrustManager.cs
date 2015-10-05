using java.security;
using java.security.cert;
using ScriptCoreLib;

namespace javax.net.ssl
{
    // http://developer.android.com/reference/javax/net/ssl/X509TrustManager.html
    // http://docs.oracle.com/javase/7/docs/api/javax/net/ssl/X509TrustManager.html
    [Script(IsNative = true)]
    public interface X509TrustManager : TrustManager
    {

        void checkServerTrusted(X509Certificate[] chain, string authType);

        void checkClientTrusted(X509Certificate[] chain, string authType);

        X509Certificate[] getAcceptedIssuers();
    }
}
