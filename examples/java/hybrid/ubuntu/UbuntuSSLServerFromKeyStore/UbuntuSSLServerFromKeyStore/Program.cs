using java.io;
using java.security;
using java.security.cert;
using java.util.zip;
using javax.net.ssl;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
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

namespace UbuntuSSLServerFromKeyStore
{
    class TrustEveryoneManager : javax.net.ssl.X509TrustManager
    {
        public void checkClientTrusted(X509Certificate[] arg0, String arg1) { }
        public void checkServerTrusted(X509Certificate[] arg0, String arg1) { }
        public X509Certificate[] getAcceptedIssuers()
        {
            return new X509Certificate[0];
        }
    }

    class localKeyManager : javax.net.ssl.X509ExtendedKeyManager
    {
        KeyManager[] KeyManagers = new KeyManager[0];
        X509KeyManager InternalX509KeyManager;

        string alias = "mykey";


        public localKeyManager(
            string keystorepath
            )
        {
            Console.WriteLine("enter localKeyManager");


            try
            {
                var xFileInputStream = default(FileInputStream);


                var xKeyStore = default(KeyStore);
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
                    xKeyStoreDefaultType = java.security.KeyStore.getDefaultType();
                    // http://www.coderanch.com/t/377172/java/java/cacerts-JAVA-HOME-jre-lib
                    // /usr/lib/jvm/default-java/jre/lib/security/cacerts

                    Console.WriteLine(new { xKeyStoreDefaultType });
                    xKeyStore = KeyStore.getInstance(xKeyStoreDefaultType);

                    var fa = new FileInfo(typeof(Program).Assembly.Location);

                    try
                    {
                        xFileInputStream = new FileInputStream(keystorepath);
                        xKeyStorePassword = "".PadLeft(6, '0').ToCharArray();
                    }
                    catch
                    {
                        throw;
                    }
                }

                Console.WriteLine("localKeyManager " + new { xKeyStore });

                xKeyStore.load(xFileInputStream, xKeyStorePassword);


                java.util.Enumeration en = xKeyStore.aliases();
                //Console.WriteLine("aliases... done");

                while (en.hasMoreElements())
                {
                    alias = (string)en.nextElement();
                }

                KeyManagerFactory kmf = KeyManagerFactory.getInstance("SunX509");

                Console.WriteLine("localKeyManager " + new { kmf, alias });


                kmf.init(xKeyStore, xKeyStorePassword);

                KeyManagers = kmf.getKeyManagers();

                Console.WriteLine("localKeyManager " + new { KeyManagers.Length });


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
                        Console.WriteLine("localKeyManager " + new { xX509KeyManager });

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
        public override string chooseServerAlias(string keyType, java.security.Principal[] issuers, java.net.Socket socket)
        {
            if (keyType != "RSA")
                return null;

            // chooseServerAlias { keyType = EC_EC }
            Console.WriteLine("chooseServerAlias " + new { keyType, StackTrace = new System.Diagnostics.StackTrace() });

            //if (issuers != null)
            //    foreach (var issuer in issuers)
            //    {
            //        Console.WriteLine("chooseServerAlias " + new { keyType, issuer });
            //    }

            // { aliasKey = 192.168.1.12, SerialNumber = c7ef5d7ff74627934e4f863f4a766a89, SimpleName = 192.168.1.12, Issuer = issuer }

            //return "192.168.1.12";
            return alias;
        }

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
            return this.InternalX509KeyManager.chooseClientAlias(keyType, issuers, socket);
        }


        public override X509Certificate[] getCertificateChain(string alias)
        {
            Console.WriteLine("getCertificateChain " + new { alias });
            return this.InternalX509KeyManager.getCertificateChain(alias);
        }

        public override string[] getClientAliases(string keyType, java.security.Principal[] issuers)
        {
            Console.WriteLine("getClientAliases");
            return this.InternalX509KeyManager.getClientAliases(keyType, issuers);
        }

        public override java.security.PrivateKey getPrivateKey(string alias)
        {
            Console.WriteLine("getPrivateKey");
            return this.InternalX509KeyManager.getPrivateKey(alias);
        }

        public override string[] getServerAliases(string keyType, java.security.Principal[] issuers)
        {
            Console.WriteLine("getServerAliases");
            return new[] { 
            
                alias
                
                //"192.168.1.12" 
            };
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

            //return "192.168.1.12";
            return alias;
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

                var keystore = new FileInfo(typeof(Program).Assembly.Location.TakeUntilIfAny(".") + ".keystore");
                var portpath = new FileInfo(typeof(Program).Assembly.Location.TakeUntilIfAny(".") + ".port");

                if (!portpath.Exists)
                    System.IO.File.WriteAllText(portpath.FullName, "" + 8443);

                var port = Convert.ToInt32(
                    System.IO.File.ReadAllText(portpath.FullName).Trim()
                    );

                Console.WriteLine(new { keystore, port });

                // now lets start a ssl server and convince jvm to use the first friendly name we found..

                var xSSLContext = javax.net.ssl.SSLContext.getInstance("TLSv1.2");
                Console.WriteLine(new { xSSLContext });
                var xTrustEveryoneManager = new[] { new TrustEveryoneManager() };
                var xKeyManager = new[] { new localKeyManager(keystorepath: keystore.FullName) };

                xSSLContext.init(
                    // SunMSCAPI ?
                    xKeyManager,
                    xTrustEveryoneManager,
                    new java.security.SecureRandom()
                );

                var xSSLServerSocketFactory = xSSLContext.getServerSocketFactory();
                //var ss443 = xSSLServerSocketFactory.createServerSocket(8443);

                // { Message = Address already in use: JVM_Bind, StackTrace = java.net.BindException: Address already in use: JVM_Bind
                // stop AppHostSvc

                //[svchost.exe]
                // TCP    0.0.0.0:443            red:0                  LISTENING       4

                //var ss443 = xSSLServerSocketFactory.createServerSocket(443);
                //var ss443 = xSSLServerSocketFactory.createServerSocket(8443);
                var ss443 = xSSLServerSocketFactory.createServerSocket(port);
                Console.WriteLine(new { ss443 });

                // http://developer.android.com/reference/javax/net/ssl/SSLServerSocket.html
                var xSSLServerSocket = ss443 as javax.net.ssl.SSLServerSocket;
                xSSLServerSocket.setEnabledProtocols(new[] { "TLSv1.2", "SSLv2Hello" });


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
                        #region getPeerCertificates
                        xSSLSocket.addHandshakeCompletedListener(
                            new xHandshakeCompletedListener
                            {
                                yield = e =>
                                {
                                    try
                                    {
                                        Console.WriteLine("xHandshakeCompletedListener " + new { e.getPeerCertificates().Length });

                                        var c = e.getPeerCertificates().FirstOrDefault() as X509Certificate;

                                        var x509 = new ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography.X509Certificates.__X509Certificate2 { InternalElement = c };


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
                        #endregion

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

                        var xNetworkStream = new ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__NetworkStream
                        {
                            InternalInputStream = xSSLSocket.getInputStream(),
                            InternalOutputStream = xSSLSocket.getOutputStream()
                        };

                        //Console.WriteLine(new { xNetworkStream });

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
                        //Console.WriteLine(new { line0 });

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
    }


}

// need 2012!

//Y:\staging\web\java\__AnonymousTypes__UbuntuHello__i__d_jvm\__f__AnonymousType_16__19__17_0_1.java:36: error: reference to Format is
//        return __String.Format(null, "{{ AssemblyQualifiedName = {0} }}", objectArray2);
//                       ^
//Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task.java:311: error: cannot find symbol
//        return  __TaskExtensions.<TResult>Unwrap_06000a1b(__Task.get_InternalFactory().<ScriptCoreLibJava.BCLImplementation.System.T
//                                ^
//  symbol:   method<TResult> Unwrap_06000a1b(__Task_1<__Task_1<TResult>>)
//  location: class __TaskExtensions
//  where TResult is a type-variable:
//    TResult extends Object declared in method<TResult>Run_060001b5(__Func_1<__Task_1<TResult>>)



// rebuild ScriptCoreLibJava?

//- javac
//"X:\Program Files (x86)\Java\jdk1.7.0_79\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\UbuntuSSLServerFromKeyStore\Program.java
//Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task.java:311: error: cannot find symbol
//        return  __TaskExtensions.<TResult>Unwrap_06000a1b(__Task.get_InternalFactory().<ScriptCoreLibJava.BCLImplementation.System.Thread
//                                ^
//  symbol:   method <TResult>Unwrap_06000a1b(__Task_1<__Task_1<TResult>>)
//  location: class __TaskExtensions
//  where TResult is a type-variable:
//    TResult extends Object declared in method <TResult>Run_060001b5(__Func_1<__Task_1<TResult>>)