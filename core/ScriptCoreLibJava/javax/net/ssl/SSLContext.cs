using java.security;
using ScriptCoreLib;

namespace javax.net.ssl
{
    // http://developer.android.com/reference/javax/net/ssl/SSLContext.html
    // http://docs.oracle.com/javase/7/docs/api/javax/net/ssl/SSLContext.html
    [Script(IsNative = true)]
    public class SSLContext
    {
        public SSLSocketFactory getSocketFactory() { return null; }
        public void init(KeyManager[] km, TrustManager[] tm, SecureRandom sr)
        {
        }

        public static SSLContext getInstance(string protocol) { return null; }
    }
}
