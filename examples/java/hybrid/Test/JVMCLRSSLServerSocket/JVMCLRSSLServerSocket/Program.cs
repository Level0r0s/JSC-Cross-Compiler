using java.io;
using java.security;
using java.security.cert;
using java.util.zip;
using javax.net.ssl;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.BCLImplementation.System.Net.Sockets;
using ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography.X509Certificates;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRSSLServerSocket
{

    class TrustEveryoneManager : javax.net.ssl.X509TrustManager
    {
        public void checkClientTrusted(X509Certificate[] arg0, String arg1) { }
        public void checkServerTrusted(X509Certificate[] arg0, String arg1) { }
        public X509Certificate[] getAcceptedIssuers()
        {
            //startHandshake...
            //{ Message = java.lang.NullPointerException, StackTrace = javax.net.ssl.SSLException: java.lang.NullPointerException
            //        at sun.security.ssl.Alerts.getSSLException(Unknown Source)
            //        at sun.security.ssl.SSLSocketImpl.fatal(Unknown Source)
            //        at sun.security.ssl.SSLSocketImpl.fatal(Unknown Source)
            //        at sun.security.ssl.SSLSocketImpl.handleException(Unknown Source)
            //        at sun.security.ssl.SSLSocketImpl.startHandshake(Unknown Source)
            //        at sun.security.ssl.SSLSocketImpl.startHandshake(Unknown Source)
            //        at JVMCLRSSLServerSocket.Program.main(Program.java:138)
            //Caused by: java.lang.NullPointerException
            //        at sun.security.ssl.HandshakeMessage$CertificateRequest.<init>(Unknown Source)
            //        at sun.security.ssl.ServerHandshaker.clientHello(Unknown Source)
            //        at sun.security.ssl.ServerHandshaker.processMessage(Unknown Source)
            //        at sun.security.ssl.Handshaker.processLoop(Unknown Source)
            //        at sun.security.ssl.Handshaker.process_record(Unknown Source)
            //        at sun.security.ssl.SSLSocketImpl.readRecord(Unknown Source)
            //        at sun.security.ssl.SSLSocketImpl.performInitialHandshake(Unknown Source)
            //        ... 3 more

            return new X509Certificate[0];
        }
    }

    class localKeyManager : javax.net.ssl.X509ExtendedKeyManager
    {
        //chooseServerAlias { keyType = EC_EC }
        //getClientAliases
        //chooseServerAlias { keyType = RSA }
        //getClientAliases
        //chooseServerAlias { keyType = RSA }
        //getClientAliases
        //chooseServerAlias { keyType = RSA }
        //getClientAliases
        //chooseServerAlias { keyType = RSA }
        //getClientAliases


        public static KeyManager[] WindowsMYKeyManagers()
        {
            Console.WriteLine("enter WindowsMYKeyManagers");
            var KeyManagers = new KeyManager[0];


            try
            {
                var xFileInputStream = default(FileInputStream);


                var xKeyStore = default(KeyStore);
                // certmgr.msc
                var xKeyStoreDefaultType = "Windows-MY";

                try
                {
                    Console.WriteLine(new { xKeyStoreDefaultType });
                    xKeyStore = KeyStore.getInstance(xKeyStoreDefaultType);
                }
                catch
                {
                    xKeyStoreDefaultType = java.security.KeyStore.getDefaultType();
                    // http://www.coderanch.com/t/377172/java/java/cacerts-JAVA-HOME-jre-lib
                    // /usr/lib/jvm/default-java/jre/lib/security/cacerts

                    Console.WriteLine(new { xKeyStoreDefaultType });
                    xKeyStore = KeyStore.getInstance(xKeyStoreDefaultType);

                    var fa = new FileInfo(typeof(Program).Assembly.Location);
                    var keystorepath = fa.Directory.FullName + "/domain.keystore";

                    try
                    {
                        xFileInputStream = new FileInputStream(keystorepath);
                    }
                    catch { throw; }
                }

                Console.WriteLine("WindowsMYKeyManagers " + new { xKeyStore });

                xKeyStore.load(xFileInputStream, null);

                KeyManagerFactory kmf = KeyManagerFactory.getInstance("SunX509");

                Console.WriteLine("WindowsMYKeyManagers " + new { kmf });


                kmf.init(xKeyStore, null);

                KeyManagers = kmf.getKeyManagers();

                Console.WriteLine("WindowsMYKeyManagers " + new { KeyManagers.Length });


                //{ xKeyStoreDefaultType = Windows-MY }
                //WindowsMYKeyManagers { xKeyStore = java.security.KeyStore@ac4d3b }
                //WindowsMYKeyManagers { kmf = javax.net.ssl.KeyManagerFactory@1c7d56b }
                //WindowsMYKeyManagers { KeyManagers = [Ljavax.net.ssl.KeyManager;@f77511 }

                // http://docs.oracle.com/javase/7/docs/api/javax/net/ssl/KeyManager.html
                // http://stackoverflow.com/questions/5292074/how-to-specify-outbound-certificate-alias-for-https-calls
                // http://www.angelfire.com/or/abhilash/site/articles/jsse-km/customKeyManager.html

                foreach (var KeyManager in KeyManagers)
                {
                    var xX509KeyManager = KeyManager as X509KeyManager;
                    if (xX509KeyManager != null)
                    {
                        Console.WriteLine("WindowsMYKeyManagers " + new { xX509KeyManager });

                    }
                }

                //WindowsMYKeyManagers { Length = 1 }
                //WindowsMYKeyManagers { xX509KeyManager = sun.security.ssl.SunX509KeyManagerImpl@ea3932 }


                //KeyStore ks = KeyStore.getInstance("JKS");
                //// initialize KeyStore object using keystore name
                //ks.load(new FileInputStream(keyFile), null);
                //kmf.init(ks, keystorePasswd.toCharArray());
                //ret = kmf.getKeyManagers();

                // chooseServerAlias { keyType = RSA, StackTrace = <__StackTrace> }

                //java.security.KeyStore ks = null;

                //KeyManagerFactory kmf

                // http://stackoverflow.com/questions/15076820/java-sslhandshakeexception-no-cipher-suites-in-common
                // http://stackoverflow.com/questions/7535154/chrome-closing-connection-on-handshake-with-java-ssl-server
            }
            catch
            {
                throw;

            }

            return KeyManagers;
        }

        public localKeyManager()
        {

            //enter localKeyManager
            //localKeyManager { xKeyStore = java.security.KeyStore@1496e57 }
            //localKeyManager { kmf = javax.net.ssl.KeyManagerFactory@1408325 }
            //{ Message = Uninitialized keystore, StackTrace = java.lang.RuntimeException: Uninitialized keystore
            //        at JVMCLRSSLServerSocket.localKeyManager.<init>(localKeyManager.java:55)
            //        at JVMCLRSSLServerSocket.Program.main(Program.java:83)
            //Caused by: java.security.KeyStoreException: Uninitialized keystore
            //        at java.security.KeyStore.aliases(Unknown Source)
            //        at sun.security.ssl.SunX509KeyManagerImpl.<init>(Unknown Source)
            //        at sun.security.ssl.KeyManagerFactoryImpl$SunX509.engineInit(Unknown Source)
            //        at javax.net.ssl.KeyManagerFactory.init(Unknown Source)
            //        at JVMCLRSSLServerSocket.localKeyManager.<init>(localKeyManager.java:49)
            //        ... 1 more
            // }


        }

        // the alias name of a matching key or null if there are no matches.
        // Chooses an alias for the server side of an SSL connection to authenticate it with the specified public key type and certificate issuers.
        public override string chooseServerAlias(string keyType, java.security.Principal[] issuers, java.net.Socket socket)
        {
            // chooseServerAlias { keyType = EC_EC }
            Console.WriteLine("chooseServerAlias " + new { keyType, StackTrace = new System.Diagnostics.StackTrace() });

            if (issuers != null)
                foreach (var issuer in issuers)
                {
                    Console.WriteLine("chooseServerAlias " + new { keyType, issuer });
                }

            // { aliasKey = 192.168.1.12, SerialNumber = c7ef5d7ff74627934e4f863f4a766a89, SimpleName = 192.168.1.12, Issuer = issuer }

            return "192.168.1.12";
        }

        // %% Invalidated:  [Session-1, SSL_NULL_WITH_NULL_NULL]


        public override string chooseClientAlias(string[] keyType, java.security.Principal[] issuers, java.net.Socket socket)
        {
            Console.WriteLine("chooseClientAlias " + new { StackTrace = new System.Diagnostics.StackTrace() });

            if (keyType != null)
                foreach (var keyType0 in keyType)
                {
                    Console.WriteLine("chooseClientAlias " + new { keyType0 });
                }


            if (issuers != null)
                foreach (var issuer in issuers)
                {
                    Console.WriteLine("chooseClientAlias " + new { issuer });
                }


            // client does not have an alies does it?
            return "wtf";
        }


        public override X509Certificate[] getCertificateChain(string alias)
        {
            Console.WriteLine("getCertificateChain");
            return null;
        }

        public override string[] getClientAliases(string keyType, java.security.Principal[] issuers)
        {
            Console.WriteLine("getClientAliases");
            return null;
        }

        public override java.security.PrivateKey getPrivateKey(string alias)
        {
            Console.WriteLine("getClientAliases");
            return null;
        }

        public override string[] getServerAliases(string keyType, java.security.Principal[] issuers)
        {
            Console.WriteLine("getServerAliases");
            return null;
        }

        public override string chooseEngineClientAlias(string[] keyType, java.security.Principal[] issuers, javax.net.ssl.SSLEngine engine)
        {
            Console.WriteLine("chooseEngineClientAlias");
            return null;
        }

        public override string chooseEngineServerAlias(string keyType, java.security.Principal[] issuers, javax.net.ssl.SSLEngine engine)
        {
            //%% Initialized:  [Session-2, SSL_NULL_WITH_NULL_NULL]
            //chooseServerAlias { keyType = EC_EC, StackTrace = <__StackTrace> }
            //Finalizer, called close()
            //getClientAliases
            //Finalizer, called closeInternal(true)

            Console.WriteLine("chooseEngineServerAlias " + new { keyType });

            return "192.168.1.12";
        }
    }

    static class Program
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151020


        class xHandshakeCompletedListener : HandshakeCompletedListener
        {
            public Action<HandshakeCompletedEvent> yield;

            public void handshakeCompleted(HandshakeCompletedEvent _event)
            {
                yield(_event);
            }
        }

        [STAThread]
        public static void Main(string[] args)
        {
            // http://www.oracle.com/technetwork/java/javase/downloads/jce-7-download-432124.html

            // https://alesaudate.wordpress.com/2010/08/09/how-to-dynamically-select-a-certificate-alias-when-invoking-web-services/

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151010

            try
            {

                Console.WriteLine(
                              new
                              {
                                  typeof(object).AssemblyQualifiedName,
                                  Environment.CurrentDirectory,

                                  // "X:\Program Files (x86)\Java\jre7\lib\security\local_policy.jar"
                                  // Location = X:\Program Files (x86)\Java\jre7\lib\rt.jar }
                                  typeof(object).Assembly.Location,

                              }
                          );


                #region useless
                // You can't do it with the system properties. You would have to write and load your own X509KeyManager and create your own SSLContext with it.

                var keyStore = java.lang.System.getProperty("javax.net.ssl.keyStore");
                Console.WriteLine(new { keyStore });
                var keyStorePassword = java.lang.System.getProperty("javax.net.ssl.keyStorePassword");
                Console.WriteLine(new { keyStorePassword });
                #endregion

                // ok lets do a server.


                // http://developer.android.com/reference/android/net/SSLCertificateSocketFactory.html

                // http://stackoverflow.com/questions/11832672/how-can-a-java-client-use-the-native-windows-my-store-to-provide-its-client-cert
                // http://docs.oracle.com/javase/7/docs/technotes/guides/security/jsse/ReadDebug.html

                //java.lang.System.setProperty("javax.net.debug", "all");

                // http://stackoverflow.com/questions/7615645/ssl-handshake-alert-unrecognized-name-error-since-upgrade-to-java-1-7-0
                java.lang.System.setProperty("jsse.enableSNIExtension", "false");


                // http://www.angelfire.com/or/abhilash/site/articles/jsse-km/customKeyManager.html


                // the reason for the SSLEngine’s complaint is that you enabled only the RSA cipher, but your certificate uses DSA keys. 
                //CLRProgram.makecert(host: "192.168.1.12", port: 8443);

                // ERR_SSL_VERSION_OR_CIPHER_MISMATCH

                //var xSSLContext = javax.net.ssl.SSLContext.getInstance("SSL");

                // For 256 bit security you need to install Oracle's unlimited strength policy files.
                // http://www.oracle.com/technetwork/java/javase/downloads/jce8-download-2133166.html

                //var xSSLContext = javax.net.ssl.SSLContext.getInstance("SSLv3");

                // { Message = TLSv1.3 SSLContext not available, StackTrace = java.security.NoSuchAlgorithmException: TLSv1.3 SSLContext not available
                //var xSSLContext = javax.net.ssl.SSLContext.getInstance("TLSv1.3");
                //var xSSLContext = javax.net.ssl.SSLContext.getInstance("TLSv1.2");
                //var xSSLContext = javax.net.ssl.SSLContext.getInstance("TLSv1.1");


                // { Message = TLS_RSA_WITH_AES_256_CBC_SHA256 SSLContext not available, StackTrace = java.security.NoSuchAlgorithmException: TLS_RSA_WITH_AES_256_CBC_SHA256 SSLContext not available

                var xSSLContext = javax.net.ssl.SSLContext.getInstance("TLSv1.2");


                Console.WriteLine(new { xSSLContext });




                // https://android.googlesource.com/platform/libcore/+/jb-mr2-release/luni/src/main/java/javax/net/ssl/KeyManagerFactory.java
                //var localKeyManager = new[] { new localKeyManager() };
                var myTrustManagerArray = new[] { new TrustEveryoneManager() };

                // null?
                xSSLContext.init(
                    // SunMSCAPI ?
                    localKeyManager.WindowsMYKeyManagers(),

                    myTrustManagerArray, new java.security.SecureRandom());




                //var cf = javax.net.ssl.SSLSocketFactory.getDefault() as javax.net.ssl.SSLSocketFactory;
                var xSSLServerSocketFactory = xSSLContext.getServerSocketFactory();

                //{ cf = sun.security.ssl.SSLSocketFactoryImpl@1fd10fa }
                //{ ssf = sun.security.ssl.SSLServerSocketFactoryImpl@7b4ed7 }
                Console.WriteLine(new { xSSLServerSocketFactory });

                //f.

                // http://www.javased.com/?api=javax.net.ssl.SSLSocketFactory
                // http://www.java2s.com/Code/JavaAPI/javax.net.ssl/SSLSocketFactorycreateSocketStringarg0intarg1.htm
                // http://saltnlight5.blogspot.com.ee/2014/10/how-to-setup-custom-sslsocketfactorys.html

                //f.createSocket(

                // http://www.herongyang.com/JDK/SSL-Socket-Server-Example-SslReverseEchoer.html
                //javax.net.ssl.SSLServerSocket.




                // http://www.javaworld.com/article/2075291/learn-java/build-secure-network-applications-with-ssl-and-the-jsse-api.html
                // -Djavax.net.ssl.keyStore
                // -Djavax.net.ssl.keyStorePassword


                // http://stackoverflow.com/questions/20798652/java-sslserversocket-presents-wrong-certificate

                // https://searchcode.com/codesearch/view/171073/

                // http://stackoverflow.com/questions/12370351/setting-the-certificate-used-by-a-java-ssl-serversocket
                // http://stackoverflow.com/questions/22230815/java-server-ssl-with-different-storepass-and-keypass

                // http://stackoverflow.com/questions/9921548/sslsocketfactory-in-java
                // https://code.google.com/p/vellum/wiki/LocalCa

                //  hg https://bitbucket.org/mfichman/mitm



                //var ssf = javax.net.ssl.SSLServerSocketFactory.getDefault() as javax.net.ssl.SSLServerSocketFactory;

                //// http://stackoverflow.com/questions/13874387/create-app-with-sslsocket-java
                //Console.WriteLine(new { ssf });


                //{ Message = Address already in use: JVM_Bind, StackTrace = java.net.BindException: Address already in use: JVM_Bind
                //        at java.net.DualStackPlainSocketImpl.bind0(Native Method)
                //        at java.net.DualStackPlainSocketImpl.socketBind(Unknown Source)
                //        at java.net.AbstractPlainSocketImpl.bind(Unknown Source)
                //        at java.net.PlainSocketImpl.bind(Unknown Source)
                //        at java.net.ServerSocket.bind(Unknown Source)
                //        at java.net.ServerSocket.<init>(Unknown Source)
                //        at java.net.ServerSocket.<init>(Unknown Source)
                //        at javax.net.ssl.SSLServerSocket.<init>(Unknown Source)
                //        at sun.security.ssl.SSLServerSocketImpl.<init>(Unknown Source)
                //        at sun.security.ssl.SSLServerSocketFactoryImpl.createServerSocket(Unknown Source)
                //        at JVMCLRSSLServerSocket.Program.main(Program.java:53)

                //C:\Windows\system32>netstat -ab

                //Active Connections

                //  Proto  Local Address          Foreign Address        State
                //  TCP    0.0.0.0:80             red:0                  LISTENING
                // Can not obtain ownership information
                //  TCP    0.0.0.0:135            red:0                  LISTENING
                //  RpcSs
                // [svchost.exe]
                //  TCP    0.0.0.0:443            red:0                  LISTENING



                // http://stackoverflow.com/questions/22225414/create-an-ssl-channel-same-pwd-for-keystore-and-trustore

                var ss443 = xSSLServerSocketFactory.createServerSocket(8443);


                Console.WriteLine(new { ss443 });


                // http://developer.android.com/reference/javax/net/ssl/SSLServerSocket.html
                var xSSLServerSocket = ss443 as javax.net.ssl.SSLServerSocket;


                // https://www.chromium.org/Home/chromium-security/education/tls
                // http://stackoverflow.com/questions/21289293/java-7-support-of-aes-gcm-in-ssl-tls
                // http://superuser.com/questions/747377/enable-tls-1-1-and-1-2-for-clients-on-java-7
                // https://blogs.oracle.com/java-platform-group/entry/java_8_will_use_tls





                xSSLServerSocket.setEnabledProtocols(new[] { "TLSv1.2", "SSLv2Hello" });


                //  Cipher suites with SHA384 and SHA256 are available only for TLS 1.2 or later.


                // http://docs.oracle.com/javase/7/docs/technotes/guides/security/StandardNames.html#ciphersuites
                // http://docs.oracle.com/javase/8/docs/technotes/guides/security/StandardNames.html#ciphersuites


                var SystemSupportedCipherSuites = xSSLServerSocket.getSupportedCipherSuites();

                SystemSupportedCipherSuites.WithEach(
                    SupportedCipherSuite =>
                    {
                        Console.WriteLine(new { SupportedCipherSuite });
                    }
                );

                //if (SystemSupportedCipherSuites.Contains())

                // https://googleonlinesecurity.blogspot.com.ee/2013/11/a-roster-of-tls-cipher-suites-weaknesses.html
                // http://stackoverflow.com/questions/21289293/java-7-support-of-aes-gcm-in-ssl-tls

                // need java 8?


                //xSSLServerSocket.setEnabledCipherSuites("TLS_RSA_WITH_AES_128_CBC_SHA");


                // https://blogs.oracle.com/java-platform-group/entry/diagnosing_tls_ssl_and_https
                // https://community.oracle.com/thread/2382681?tstart=0

                //Cipher Suites: [
                //    TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256, 
                //    TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256, 
                //    TLS_DHE_RSA_WITH_AES_128_GCM_SHA256, 
                //    Unknown 0xcc:0x14, 
                //                Unknown 0xcc:0x13, 
                //                TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA, 
                //                TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA, 
                //                TLS_DHE_RSA_WITH_AES_256_CBC_SHA, 
                //                TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA, 
                //                TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA, 
                //                TLS_DHE_RSA_WITH_AES_128_CBC_SHA, 
                //                TLS_RSA_WITH_AES_128_GCM_SHA256, 
                //                TLS_RSA_WITH_AES_256_CBC_SHA, 
                //                TLS_RSA_WITH_AES_128_CBC_SHA, 
                //                SSL_RSA_WITH_3DES_EDE_CBC_SHA]



                // Cipher Suites: [TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256, 
                // TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256, 
                // TLS_DHE_RSA_WITH_AES_128_GCM_SHA256, 
                // Unknown 0xcc:0x14, Unknown 0xcc:0x13, 
                //TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA, 
                //TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA, 
                //TLS_DHE_RSA_WITH_AES_256_CBC_SHA, 
                //TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA, 
                //TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA, 
                //TLS_DHE_RSA_WITH_AES_128_CBC_SHA, 
                //TLS_RSA_WITH_AES_128_GCM_SHA256, 
                //TLS_RSA_WITH_AES_256_CBC_SHA, 
                //TLS_RSA_WITH_AES_128_CBC_SHA, 
                //SSL_RSA_WITH_3DES_EDE_CBC_SHA
                //]

                var enabledCipherSuites = new[] {
                    //"TLS_DHE_DSS_WITH_AES_128_CBC_SHA256" 

                    // { Message = Unsupported ciphersuite TLS_RSA_WITH_AES_128_GCM_SHA256, StackTrace = java.lang.IllegalArgumentException: Unsupported ciphersuite TLS_RSA_WITH_AES_128_GCM_SHA256
                    //"TLS_RSA_WITH_AES_128_GCM_SHA256" 

                    //"TLS_RSA_WITH_AES_256_CBC_SHA" 
                    "TLS_RSA_WITH_AES_128_CBC_SHA"

                    //"SSL_RSA_WITH_3DES_EDE_CBC_SHA" 

                    // { Message = Unsupported ciphersuite TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256, StackTrace = java.lang.IllegalArgumentException: Unsupported ciphersuite TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256
                    //"TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256" 

                    // { Message = Unsupported ciphersuite TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256, StackTrace = java.lang.IllegalArgumentException: Unsupported ciphersuite TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256
                    //"TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256"

                    // { Message = Unsupported ciphersuite TLS_DHE_RSA_WITH_AES_128_GCM_SHA256, StackTrace = java.lang.IllegalArgumentException: Unsupported ciphersuite TLS_DHE_RSA_WITH_AES_128_GCM_SHA256
                    //"TLS_DHE_RSA_WITH_AES_128_GCM_SHA256"
                };

                // need id?
                //xSSLServerSocket.setNeedClientAuth(true);

                ////xSSLServerSocket.setWantClientAuth(true);

                //xSSLServerSocket.setEnabledCipherSuites(enabledCipherSuites);

                var ok = true;
                while (ok)
                {
                    //Console.WriteLine("accept...");
                    var xSSLSocket = ss443.accept() as javax.net.ssl.SSLSocket;




                    //Console.WriteLine(new { xSSLSocket });

                    // http://security.stackexchange.com/questions/76993/now-that-it-is-2015-what-ssl-tls-cipher-suites-should-be-used-in-a-high-securit
                    // java u suck.

                    //Console.WriteLine("startHandshake...");
                    try
                    {
                        // http://developer.android.com/reference/javax/net/ssl/HandshakeCompletedEvent.html

                        Func<string> getdata = () =>
                             "HTTP/1.0 200 OK\r\nConnection: close\r\n\r\n<h1>hello world</h1>";

                        // can we await for it?
                        xSSLSocket.addHandshakeCompletedListener(
                            new xHandshakeCompletedListener
                            {
                                yield = e =>
                                {
                                    try
                                    {
                                        Console.WriteLine("xHandshakeCompletedListener " + new { e.getPeerCertificates().Length });

                                        var c = e.getPeerCertificates().FirstOrDefault() as X509Certificate;

                                        var x509 = new __X509Certificate2 { InternalElement = c };


                                        if (c != null)
                                        {

                                            getdata = () =>
                                                "HTTP/1.0 200 OK\r\nConnection: close\r\n\r\n<h1>authenticated!</h1>"
                                                + new XElement("pre", new { x509.Subject, x509.SerialNumber }.ToString()
                                                    );
                                        }
                                    }
                                    catch (Exception fault)
                                    {
                                        //Caused by: javax.net.ssl.SSLPeerUnverifiedException: peer not authenticated
                                        //        at sun.security.ssl.SSLSessionImpl.getPeerCertificates(Unknown Source)
                                        //        at javax.net.ssl.HandshakeCompletedEvent.getPeerCertificates(Unknown Source)

                                        //throw;

                                        Console.WriteLine("getPeerCertificates " + new { fault.Message });
                                    }
                                }
                            }
                        );

                        xSSLSocket.startHandshake();



                        //Cipher Suites: [
                        //    TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256, 
                        //    TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256, 
                        //    TLS_DHE_RSA_WITH_AES_128_GCM_SHA256, 
                        //    Unknown 0xcc:0x14, 
                        //Unknown 0xcc:0x13, 
                        //TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA, 
                        //TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA, 
                        //TLS_DHE_RSA_WITH_AES_256_CBC_SHA, 
                        //TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA, 
                        //TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA, 
                        //TLS_DHE_RSA_WITH_AES_128_CBC_SHA, 
                        //TLS_RSA_WITH_AES_128_GCM_SHA256, 
                        //TLS_RSA_WITH_AES_256_CBC_SHA, 
                        //TLS_RSA_WITH_AES_128_CBC_SHA, 
                        //SSL_RSA_WITH_3DES_EDE_CBC_SHA]

                        // http://www.e2college.com/blogs/java_security/ssl_handshake_failure_due_to_unsupported_cipher_su.html






                        // Error	573	The type 'ScriptCoreLib.Shared.BCLImplementation.System.IO.__Stream' is defined in an assembly that is not referenced. You must add a reference to assembly 'ScriptCoreLib, Version=4.6.0.0, Culture=neutral, PublicKeyToken=null'.	Z:\jsc.svn\examples\java\hybrid\Test\JVMCLRSSLServerSocket\JVMCLRSSLServerSocket\Program.cs	68	17	JVMCLRSSLServerSocket

                        var xNetworkStream = new __NetworkStream
                        {
                            InternalInputStream = xSSLSocket.getInputStream(),
                            InternalOutputStream = xSSLSocket.getOutputStream()
                        };

                        Console.WriteLine(new { xNetworkStream });

                        // http://stackoverflow.com/questions/13874387/create-app-with-sslsocket-java

                        // http://www.java2s.com/Tutorial/Java/0320__Network/CreatinganSSLServerSocket.htm
                        // http://192.168.1.12:8443/
                        // chrome does a download of NAK EXT SOH NUL STX STX ??

                        // { byte0 = 71 }
                        //var byte0 = xNetworkStream.ReadByte();

                        //{ cf = sun.security.ssl.SSLSocketFactoryImpl@93f13f }
                        //{ ssf = sun.security.ssl.SSLServerSocketFactoryImpl@15dc721 }
                        //{ ss443 = [SSL: ServerSocket[addr=0.0.0.0/0.0.0.0,localport=8443]] }
                        //{ xSSLSocket = 1747f59[SSL_NULL_WITH_NULL_NULL: Socket[addr=/192.168.1.196,port=55953,localport=8443]] }
                        //{ xNetworkStream = ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__NetworkStream@538cc2 }
                        //{ byte0 = -1 }

                        //Console.WriteLine(new { byte0 });
                        //Console.WriteLine(new { byte0 });




                        //{ Message = Java heap space, StackTrace = java.lang.OutOfMemoryError: Java heap space
                        //        at ScriptCoreLibJava.BCLImplementation.System.IO.__MemoryStream.set_Capacity(__MemoryStream.java:110)
                        //        at ScriptCoreLibJava.BCLImplementation.System.IO.__MemoryStream.InternalEnsureCapacity(__MemoryStream.java:156)
                        //        at ScriptCoreLibJava.BCLImplementation.System.IO.__MemoryStream.WriteByte(__MemoryStream.java:140)
                        //        at ScriptCoreLibJava.BCLImplementation.System.IO.__StreamReader.ReadLine(__StreamReader.java:51)
                        //        at JVMCLRSSLServerSocket.Program.main(Program.java:145)

                        var xStreamReader = new StreamReader(xNetworkStream);
                        var line0 = xStreamReader.ReadLine();
                        Console.WriteLine(new { line0 });

                        // { line0 = GET / HTTP/1.1 }


                        // http://stackoverflow.com/questions/3662837/java-no-cipher-suites-in-common-issue-when-trying-to-securely-connect-to-serve
                        // http://stackoverflow.com/questions/15076820/java-sslhandshakeexception-no-cipher-suites-in-common


                        //Implementation not found for type import :
                        //type: System.IO.StreamWriter
                        //method: Void .ctor(System.IO.Stream)
                        //var xStreamWriter = new StreamWriter(xNetworkStream);

                        var data =
                           getdata();

                        var bytes = Encoding.UTF8.GetBytes(data);

                        xNetworkStream.Write(bytes, 0, bytes.Length);


                        xNetworkStream.Close();

                    }
                    catch (Exception fault)
                    {
                        reportHansshakeFault(fault);


                    }

                    //Thread.Sleep(5000);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(
                    new
                    {
                        err.Message,
                        err.StackTrace
                    }
                    );

            }

            //CLRProgram.CLRMain();

            Console.WriteLine("done");
            Console.ReadLine();
        }

        private static void reportHansshakeFault(Exception fault)
        {
            //startHandshake { Message = Unrecognized SSL message, plaintext connection? }
            //startHandshake { Message = Remote host closed connection during handshake }
            var skipTLSv1 = fault.Message.Contains("Client requested protocol TLSv1");
            var skipTLSv11 = fault.Message.Contains("Client requested protocol TLSv1.1");

            // unable to recover from if/else detection at JVMCLRSSLServerSocket.Program.Main

            var skip = skipTLSv1 || skipTLSv11;

            if (skip)
                return;

            Console.WriteLine(
                "startHandshake " +
                new
                {
                    fault.Message
                    //,
                    //fault.StackTrace
                }
                );
        }


    }



    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            MessageBox.Show("click to close");

        }

        public static void makecert(string host, int port)
        {
            // https://groups.google.com/forum/#!msg/akka-user/c1ac5jH_ezU/Ve_eD5rJBtkJ
            Console.WriteLine(
                new
                {
                    typeof(object).AssemblyQualifiedName,
                    Environment.CurrentDirectory
                }
            );

            Console.WriteLine(new { host, port });

        }
    }


}

//{ AssemblyQualifiedName = java.lang.Object, rt, CurrentDirectory = Z:\jsc.svn\examples\java\hybrid\Test\JVMCLRSSLServerSocket\JVMCLRSSLServerSocket\bin\Release }
//{ AssemblyQualifiedName = System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, CurrentDirectory = Z:\jsc.svn\examples\java\hybrid\Test\JVMCLRSSLServerSocket\JVMCLRSSLServerSocket\bin\Release }
//{ host = 192.168.1.12, port = 8443 }



//Allow unsafe renegotiation: false
//Allow legacy hello messages: true
//Is initial handshake: true
//Is secure renegotiation: false
//{ xSSLSocket = a63599[SSL_NULL_WITH_NULL_NULL: Socket[addr=/192.168.1.196,port=56227,localport=8443]] }
//{ xNetworkStream = ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__NetworkStream@1a9192b }
//Ignoring unsupported cipher suite: TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA256 for SSLv2Hello
//Ignoring unsupported cipher suite: TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA256 for SSLv2Hello
//Ignoring unsupported cipher suite: TLS_RSA_WITH_AES_128_CBC_SHA256 for SSLv2Hello
//Ignoring unsupported cipher suite: TLS_ECDH_ECDSA_WITH_AES_128_CBC_SHA256 for SSLv2Hello
//Ignoring unsupported cipher suite: TLS_ECDH_RSA_WITH_AES_128_CBC_SHA256 for SSLv2Hello
//Ignoring unsupported cipher suite: TLS_DHE_RSA_WITH_AES_128_CBC_SHA256 for SSLv2Hello
//Ignoring unsupported cipher suite: TLS_DHE_DSS_WITH_AES_128_CBC_SHA256 for SSLv2Hello
//Ignoring unsupported cipher suite: TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA256 for TLSv1
//Ignoring unsupported cipher suite: TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA256 for TLSv1
//Ignoring unsupported cipher suite: TLS_RSA_WITH_AES_128_CBC_SHA256 for TLSv1
//Ignoring unsupported cipher suite: TLS_ECDH_ECDSA_WITH_AES_128_CBC_SHA256 for TLSv1
//Ignoring unsupported cipher suite: TLS_ECDH_RSA_WITH_AES_128_CBC_SHA256 for TLSv1
//Ignoring unsupported cipher suite: TLS_DHE_RSA_WITH_AES_128_CBC_SHA256 for TLSv1
//Ignoring unsupported cipher suite: TLS_DHE_DSS_WITH_AES_128_CBC_SHA256 for TLSv1
//Ignoring unsupported cipher suite: TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA256 for TLSv1.1
//Ignoring unsupported cipher suite: TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA256 for TLSv1.1
//Ignoring unsupported cipher suite: TLS_RSA_WITH_AES_128_CBC_SHA256 for TLSv1.1
//Ignoring unsupported cipher suite: TLS_ECDH_ECDSA_WITH_AES_128_CBC_SHA256 for TLSv1.1
//Ignoring unsupported cipher suite: TLS_ECDH_RSA_WITH_AES_128_CBC_SHA256 for TLSv1.1
//Ignoring unsupported cipher suite: TLS_DHE_RSA_WITH_AES_128_CBC_SHA256 for TLSv1.1
//Ignoring unsupported cipher suite: TLS_DHE_DSS_WITH_AES_128_CBC_SHA256 for TLSv1.1
//[Raw read]: length = 5
//0000: 47 45 54 20 2F                                     GET /
//main, handling exception: javax.net.ssl.SSLException: Unrecognized SSL message, plaintext connection?
//main, SEND TLSv1 ALERT:  fatal, description = unexpected_message
//main, WRITE: TLSv1 Alert, length = 2
//[Raw write]: length = 7
//0000: 15 03 01 00 02 02 0A                               .......
//main, called closeSocket()
//{ byte0 = -1 }
//System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089














//    0001 020002c5 ScriptCoreLibJava::ScriptCoreLibJava.BCLImplementation.System.Text.xEncoding
//{
//    Location =
// assembly: C:\util\jsc\bin\ScriptCoreLibJava.dll
// type: ScriptCoreLibJava.BCLImplementation.System.Text.__StringBuilder, ScriptCoreLibJava, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null
// offset: 0x0018
//  method:ScriptCoreLibJava.BCLImplementation.System.Text.__StringBuilder Append(Boolean) }
//{ Location =
// assembly: C:\util\jsc\bin\ScriptCoreLibJava.dll
// type: ScriptCoreLibJava.BCLImplementation.System.Text.__StringBuilder, ScriptCoreLibJava, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null
// offset: 0x000f
//  method:ScriptCoreLibJava.BCLImplementation.System.Text.__StringBuilder Append(Boolean) }
//System.NotSupportedException: multiple stack entries instead of one
//   at jsc.ILFlowStackItem.get_SingleStackInstruction() in x:\jsc.internal.git\compiler\jsc\CodeModel\ILFlow.cs:line 139
//   at jsc.Script.CompilerCLike.WriteParameters(Prestatement p, MethodBase _method, ILFlowStackItem[] s, Int32 offset, ParameterInfo[] pi, Boolean pWritten, String op) in x:\jsc.internal.git\compiler\jsc\Languages\CompilerCLike.cs:line 297
//   at jsc.Script.CompilerCLike.WriteParameterInfoFromStack(MethodBase m, Prestatement p, ILFlowStackItem[] s, Int32 offset) in x:\jsc.internal.git\compiler\jsc\Languages\CompilerCLike.cs:line 235
//   at jsc.Languages.Java.JavaCompiler.WriteMethodCallVerified(Prestatement p, ILInstruction i, MethodBase SourceMethod) in x:\jsc.internal.git\compiler\jsc\Languages\Java\JavaCompiler.WriteMethodCallVerified.cs:line 217
