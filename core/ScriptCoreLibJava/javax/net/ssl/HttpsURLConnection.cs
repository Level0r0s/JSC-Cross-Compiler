using java.net;
using java.security;
using ScriptCoreLib;

namespace javax.net.ssl
{
    // http://developer.android.com/reference/javax/net/ssl/HttpsURLConnection.html
    // http://docs.oracle.com/javase/7/docs/api/javax/net/ssl/HttpsURLConnection.html
    [Script(IsNative = true)]
    public abstract class HttpsURLConnection : HttpURLConnection
    {
        // Z:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\WebClient.cs


        public static void setDefaultSSLSocketFactory(SSLSocketFactory sf)
        { 
        }

        public static void setDefaultHostnameVerifier(HostnameVerifier v)
        { 
        }
    }
}
