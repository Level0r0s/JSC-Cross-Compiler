using java.security;
using ScriptCoreLib;

namespace javax.net.ssl
{
    // http://developer.android.com/reference/javax/net/KeyManagerFactory.html
    // http://docs.oracle.com/javase/7/docs/api/javax/net/KeyManagerFactory.html
    [Script(IsNative = true)]
    public class KeyManagerFactory
    {
        public KeyManager[] getKeyManagers()
        {
            throw null;
        }

        public void init(KeyStore ks, char[] password) { throw null; }

        public static KeyManagerFactory getInstance(string algorithm) { throw null; }
    }
}
