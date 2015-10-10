using java.net;
using java.security;
using java.security.cert;
using ScriptCoreLib;

namespace javax.net.ssl
{
    // http://developer.android.com/reference/javax/net/ssl/X509KeyManager.html
    [Script(IsNative = true)]
    public interface X509KeyManager : KeyManager
    {
        // Z:\jsc.svn\examples\java\hybrid\Test\JVMCLRSSLServerSocket\JVMCLRSSLServerSocket\Program.cs

        string chooseClientAlias(string[] keyType, Principal[] issuers, Socket socket);
        string chooseServerAlias(string keyType, Principal[] issuers, Socket socket);


        X509Certificate[] getCertificateChain(string alias);

        string[] getClientAliases(string keyType, Principal[] issuers);

        PrivateKey getPrivateKey(string alias);

        string[] getServerAliases(string keyType, Principal[] issuers);


    }
}
