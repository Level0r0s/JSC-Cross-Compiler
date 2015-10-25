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

namespace TestDesiredClientCertAlias
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
            //        at TestDesiredClientCertAlias.Program.main(Program.java:138)
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
            //        at TestDesiredClientCertAlias.localKeyManager.<init>(localKeyManager.java:55)
            //        at TestDesiredClientCertAlias.Program.main(Program.java:83)
            //Caused by: java.security.KeyStoreException: Uninitialized keystore
            //        at java.security.KeyStore.aliases(Unknown Source)
            //        at sun.security.ssl.SunX509KeyManagerImpl.<init>(Unknown Source)
            //        at sun.security.ssl.KeyManagerFactoryImpl$SunX509.engineInit(Unknown Source)
            //        at javax.net.ssl.KeyManagerFactory.init(Unknown Source)
            //        at TestDesiredClientCertAlias.localKeyManager.<init>(localKeyManager.java:49)
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
        // http://stackoverflow.com/questions/9179717/using-more-than-one-key-pair-in-ssl-socket-factory-connection
        // https://code.google.com/p/jsslutils/source/browse/trunk/jsslutils/src/main/java/org/jsslutils/sslcontext/keymanagers/FixedServerAliasKeyManager.java
        // http://stackoverflow.com/questions/5292074/how-to-specify-outbound-certificate-alias-for-https-calls


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
                // first lets print into console the aliases we could be choosing from
                // it should show the CA and the host alias on windows.
                // once this works. lets do an example that works with JVM keystore



                #region Certificates
                Func<string, IEnumerable<System.Security.Cryptography.X509Certificates.X509Certificate2>> Certificates = keyStoreType =>
                {
                    var a = new List<System.Security.Cryptography.X509Certificates.X509Certificate2> { };

                    try
                    {
                        // http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/6-b27/sun/security/mscapi/SunMSCAPI.java

                        // https://docs.oracle.com/javase/7/docs/technotes/guides/security/SunProviders.html

                        // https://social.msdn.microsoft.com/Forums/expression/en-US/52dca221-1e05-44c1-8c45-9e0d4a807853/java-keystoreload-for-windowsmy-pops-up-insert-smart-card-window?forum=windowssecurity
                        // I removed some personal certificaties at key manager (certmgr.msc) and wala!

                        //Client Authentication (1.3.6.1.5.5.7.3.2)
                        //Secure Email (1.3.6.1.5.5.7.3.4)


                        // https://www.chilkatsoft.com/p/p_280.asp
                        // HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography\Defaults\Provider\Microsoft Base Smart Card Crypto Provider

                        // http://stackoverflow.com/questions/27692904/how-to-avoid-smart-card-selection-popup-when-accessing-windows-my-using-java

                        // http://stackoverflow.com/questions/4552100/how-to-prevent-popups-when-loading-a-keystore
                        // http://stackoverflow.com/questions/15220976/how-to-obtain-a-users-identity-from-a-smartcard-on-windows-mscapi-with-java

                        KeyStore xKeyStore = KeyStore.getInstance(keyStoreType);
                        //Console.WriteLine(new { xKeyStore });
                        //Console.WriteLine("load... " + new { keyStoreType });
                        xKeyStore.load(null, null);
                        //Console.WriteLine("load... done");
                        //Console.WriteLine("aliases...");
                        java.util.Enumeration en = xKeyStore.aliases();
                        //Console.WriteLine("aliases... done");

                        while (en.hasMoreElements())
                        {
                            var aliasKey = (string)en.nextElement();

                            //Console.WriteLine(new { aliasKey });

                            // PCSC?
                            var c509 = xKeyStore.getCertificate(aliasKey) as java.security.cert.X509Certificate;
                            if (c509 != null)
                            {
                                System.Security.Cryptography.X509Certificates.X509Certificate2 crt = new __X509Certificate2 { FriendlyName = aliasKey, InternalElement = c509 };

                                //Console.WriteLine(new { crt.Subject, crt.SerialNumber, SimpleName = crt.GetNameInfo(System.Security.Cryptography.X509Certificates.X509NameType.SimpleName, false) });
                                //Console.WriteLine(new { aliasKey, crt.SerialNumber, SimpleName = crt.GetNameInfo(System.Security.Cryptography.X509Certificates.X509NameType.SimpleName, false), crt.Issuer });

                                a.Add(crt);

                            }
                            //if (aliasKey.equals("myKey") ) {
                            //      PrivateKey key = (PrivateKey)ks.getKey(aliasKey, "monPassword".toCharArray());
                            //      Certificate[] chain = ks.getCertificateChain(aliasKey);
                            //}
                        }

                    }
                    catch //(Exception closure)
                    {
                        throw;
                    }

                    return a;
                };
                #endregion




                Certificates("Windows-ROOT").WithEach(
                    crt =>
                    {
                        // aliasKey = peer integrity authority for cpu BFEBFBFF000306A9
                        // SimpleName = peer integrity authority for cpu BFEBFBFF000306A9
                        var SimpleName = crt.GetNameInfo(System.Security.Cryptography.X509Certificates.X509NameType.SimpleName, false);

                        if (SimpleName.StartsWith("peer integrity authority for cpu"))
                            Console.WriteLine(new { crt.FriendlyName, SimpleName, crt.SerialNumber, crt.Issuer });
                    }
                );




                Certificates("Windows-MY").GroupBy(x => x.FriendlyName).WithEach(
                    crt =>
                    {
                        //{ FriendlyName = 192.168.1.12 }

                        //{ FriendlyName = 192.168.43.12 }
                        //{ FriendlyName = 192.168.173.12 }
                        //{ FriendlyName = 192.168.42.46 }
                        //{ FriendlyName = Administrator }


                        // hide non ip certs..
                        if (!crt.Key.Contains("."))
                            return;

                        Console.WriteLine(new { FriendlyName = crt.Key });

                        //Console.WriteLine(new { crt.FriendlyName, crt.SerialNumber });
                    }
                );


                // now lets start a ssl server and convince jvm to use the first friendly name we found..

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

            Console.WriteLine("done");
            Console.ReadLine();
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

    }


}
