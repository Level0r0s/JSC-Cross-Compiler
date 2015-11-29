extern alias jvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Java
{
    // Z:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Java\InternalX509ExtendedKeyManager.cs


    // vs intellisense complains?
    public class xHandshakeCompletedListener : jvm::javax.net.ssl.HandshakeCompletedListener
    {
        public Action<jvm::javax.net.ssl.HandshakeCompletedEvent> yield;

        public void handshakeCompleted(jvm::javax.net.ssl.HandshakeCompletedEvent _event)
        {
            yield(_event);
        }
    }

    public class TrustEveryoneManager : jvm::javax.net.ssl.X509TrustManager
    {
        public void checkClientTrusted(jvm::java.security.cert.X509Certificate[] arg0, string arg1)
        {
            //Console.WriteLine("X509TrustManager checkClientTrusted " + new { arg0.Length, arg1 });
        }

        public void checkServerTrusted(jvm::java.security.cert.X509Certificate[] arg0, string arg1) { }
        public jvm::java.security.cert.X509Certificate[] getAcceptedIssuers()
        {
            // tested by ?
            var dir = new FileInfo(typeof(TrustEveryoneManager).Assembly.Location).Directory;

            // firefox needs a cert here?
            //Console.WriteLine("X509TrustManager getAcceptedIssuers " + new { dir });


            var ff = from f in dir.GetFiles()
                     where f.Name.EndsWith(".crt")

                     //jvm::java.security.cert.CertificateFactory.getInstance("X.509").generateCertificate(
                     //let crt = jvm::java.security.cert.X509Certificate.
                     select f;


            var a = new List<jvm::java.security.cert.X509Certificate> { };
            foreach (var item in ff)
            {
                //Console.WriteLine("X509TrustManager getAcceptedIssuers " + new { item.FullName });

                try
                {
                    var ksfis = new jvm::java.io.FileInputStream(item.FullName);
                    var certificate = (jvm::java.security.cert.X509Certificate)
                      jvm::java.security.cert.CertificateFactory.getInstance("X.509").generateCertificate(ksfis);

                    // Z:\jsc.svn\examples\javascript\ubuntu\UbuntuSSLWebApplication\UbuntuSSLWebApplication\ApplicationWebService.cs

                    a.Add(certificate);
                }
                catch
                {

                }

            }

            //Console.WriteLine("X509TrustManager getAcceptedIssuers " + new { a.Count });
            return a.ToArray();
        }
    }

    public class localKeyManager : jvm::javax.net.ssl.X509ExtendedKeyManager
    {
        jvm::javax.net.ssl.KeyManager[] KeyManagers = new jvm::javax.net.ssl.KeyManager[0];
        jvm::javax.net.ssl.X509KeyManager InternalX509KeyManager;

        string alias = "mykey";


        public localKeyManager(
            string keystorepath
            )
        {
            //Console.WriteLine("enter localKeyManager");


            try
            {
                var xFileInputStream = default(jvm::java.io.FileInputStream);


                var xKeyStore = default(jvm::java.security.KeyStore);
                // certmgr.msc
                var xKeyStoreDefaultType = "Windows-MY";
                var xKeyStorePassword = default(char[]);

                //try
                //{
                //    Console.WriteLine(new { xKeyStoreDefaultType });
                //    xKeyStore = KeyStore.getInstance(xKeyStoreDefaultType);
                //}
                //catch
                {
                    xKeyStoreDefaultType = jvm::java.security.KeyStore.getDefaultType();
                    // http://www.coderanch.com/t/377172/java/java/cacerts-JAVA-HOME-jre-lib
                    // /usr/lib/jvm/default-java/jre/lib/security/cacerts

                    //Console.WriteLine(new { xKeyStoreDefaultType });
                    xKeyStore = jvm::java.security.KeyStore.getInstance(xKeyStoreDefaultType);

                    //var fa = new FileInfo(typeof(Program).Assembly.Location);

                    try
                    {
                        xFileInputStream = new jvm::java.io.FileInputStream(keystorepath);
                        xKeyStorePassword = "".PadLeft(6, '0').ToCharArray();
                    }
                    catch
                    {
                        throw;
                    }
                }

                //Console.WriteLine("localKeyManager " + new { xKeyStore });

                xKeyStore.load(xFileInputStream, xKeyStorePassword);


                var en = xKeyStore.aliases();
                //Console.WriteLine("aliases... done");

                while (en.hasMoreElements())
                {
                    alias = (string)en.nextElement();
                }

                var kmf = jvm::javax.net.ssl.KeyManagerFactory.getInstance("SunX509");

                Console.WriteLine("localKeyManager " + new { kmf, xKeyStore, alias });


                kmf.init(xKeyStore, xKeyStorePassword);

                KeyManagers = kmf.getKeyManagers();

                //Console.WriteLine("localKeyManager " + new { KeyManagers.Length });


                //{ xKeyStoreDefaultType = Windows-MY }
                //WindowsMYKeyManagers { xKeyStore = java.security.KeyStore@ac4d3b }
                //WindowsMYKeyManagers { kmf = javax.net.ssl.KeyManagerFactory@1c7d56b }
                //WindowsMYKeyManagers { KeyManagers = [Ljavax.net.ssl.KeyManager;@f77511 }

                // http://docs.oracle.com/javase/7/docs/api/javax/net/ssl/KeyManager.html
                // http://stackoverflow.com/questions/5292074/how-to-specify-outbound-certificate-alias-for-https-calls
                // http://www.angelfire.com/or/abhilash/site/articles/jsse-km/customKeyManager.html

                foreach (var KeyManager in KeyManagers)
                {
                    var xX509KeyManager = KeyManager as jvm::javax.net.ssl.X509KeyManager;
                    if (xX509KeyManager != null)
                    {
                        //Console.WriteLine("localKeyManager " + new { xX509KeyManager });

                        InternalX509KeyManager = xX509KeyManager;
                    }
                }



                // http://stackoverflow.com/questions/15076820/java-sslhandshakeexception-no-cipher-suites-in-common
                // http://stackoverflow.com/questions/7535154/chrome-closing-connection-on-handshake-with-java-ssl-server
            }
            catch
            {
                throw;

            }
        }

        // the alias name of a matching key or null if there are no matches.
        // Chooses an alias for the server side of an SSL connection to authenticate it with the specified public key type and certificate issuers.
        public override string chooseServerAlias(string keyType, jvm::java.security.Principal[] issuers, jvm::java.net.Socket socket)
        {
            if (keyType != "RSA")
                return null;

            // chooseServerAlias { keyType = EC_EC }
            //Console.WriteLine("chooseServerAlias " + new { keyType });

            //if (issuers != null)
            //    foreach (var issuer in issuers)
            //    {
            //        Console.WriteLine("chooseServerAlias " + new { keyType, issuer });
            //    }

            // { aliasKey = 192.168.1.12, SerialNumber = c7ef5d7ff74627934e4f863f4a766a89, SimpleName = 192.168.1.12, Issuer = issuer }

            //return "192.168.1.12";
            return alias;
        }

        public override string chooseClientAlias(string[] keyType, jvm::java.security.Principal[] issuers, jvm::java.net.Socket socket)
        {
            Console.WriteLine("chooseClientAlias ");




            // client does not have an alies does it?
            return this.InternalX509KeyManager.chooseClientAlias(keyType, issuers, socket);
        }


        public override jvm::java.security.cert.X509Certificate[] getCertificateChain(string alias)
        {
            // firefox complains?
            //Console.WriteLine("getCertificateChain " + new { alias });

            // http://serverfault.com/questions/346278/firefox-does-not-load-certificate-chain

            var c = this.InternalX509KeyManager.getCertificateChain(alias);

            //Console.WriteLine("getCertificateChain " + new { c.Length });

            foreach (var item in c)
            {
                //Console.WriteLine("getCertificateChain " + new { item });
                //Console.WriteLine("getCertificateChain " + new { SubjectDN = item.getSubjectDN() });

            }

            return c;
        }

        public override string[] getClientAliases(string keyType, jvm::java.security.Principal[] issuers)
        {
            Console.WriteLine("getClientAliases");
            return this.InternalX509KeyManager.getClientAliases(keyType, issuers);
        }

        public override jvm::java.security.PrivateKey getPrivateKey(string alias)
        {
            //Console.WriteLine("getPrivateKey");
            return this.InternalX509KeyManager.getPrivateKey(alias);
        }

        public override string[] getServerAliases(string keyType, jvm::java.security.Principal[] issuers)
        {
            //Console.WriteLine("getServerAliases");
            return new[] {

                alias

                //"192.168.1.12" 
            };
        }

        public override string chooseEngineClientAlias(string[] keyType, jvm::java.security.Principal[] issuers, jvm::javax.net.ssl.SSLEngine engine)
        {
            Console.WriteLine("chooseEngineClientAlias");
            return null;
        }

        public override string chooseEngineServerAlias(string keyType, jvm::java.security.Principal[] issuers, jvm::javax.net.ssl.SSLEngine engine)
        {
            Console.WriteLine("chooseEngineServerAlias " + new { keyType });

            //return "192.168.1.12";
            return alias;
        }
    }


}
