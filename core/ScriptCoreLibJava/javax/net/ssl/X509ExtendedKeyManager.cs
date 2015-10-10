using java.net;
using java.security;
using java.security.cert;
using ScriptCoreLib;

namespace javax.net.ssl
{
    // http://developer.android.com/reference/javax/net/ssl/X509KeyManager.html
    [Script(IsNative = true)]
    public abstract class X509ExtendedKeyManager : X509KeyManager
    {
        // Z:\jsc.svn\examples\java\hybrid\Test\JVMCLRSSLServerSocket\JVMCLRSSLServerSocket\Program.cs

        public abstract string chooseEngineClientAlias(string[] keyType, Principal[] issuers, SSLEngine engine);
        public abstract string chooseEngineServerAlias(string keyType, Principal[] issuers, SSLEngine engine);




        public virtual string chooseClientAlias(string[] keyType, Principal[] issuers, Socket socket)
        {
            throw new System.NotImplementedException();
        }

        public virtual string chooseServerAlias(string keyType, Principal[] issuers, Socket socket)
        {
            throw new System.NotImplementedException();
        }

        public virtual X509Certificate[] getCertificateChain(string alias)
        {
            throw new System.NotImplementedException();
        }

        public virtual string[] getClientAliases(string keyType, Principal[] issuers)
        {
            throw new System.NotImplementedException();
        }

        public virtual PrivateKey getPrivateKey(string alias)
        {
            throw new System.NotImplementedException();
        }

        public virtual string[] getServerAliases(string keyType, Principal[] issuers)
        {
            throw new System.NotImplementedException();
        }
    }
}
