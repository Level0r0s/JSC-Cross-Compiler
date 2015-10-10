using java.security.cert;
using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.BCLImplementation.System.Net.Sockets;
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
            return null;
        }
    }

    static class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            // https://alesaudate.wordpress.com/2010/08/09/how-to-dynamically-select-a-certificate-alias-when-invoking-web-services/

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151010

            Console.WriteLine(
                          new
                          {
                              typeof(object).AssemblyQualifiedName,
                              Environment.CurrentDirectory
                          }
                      );

            // ok lets do a server.

            try
            {
                // http://stackoverflow.com/questions/11832672/how-can-a-java-client-use-the-native-windows-my-store-to-provide-its-client-cert
                // http://docs.oracle.com/javase/7/docs/technotes/guides/security/jsse/ReadDebug.html

                java.lang.System.setProperty("javax.net.debug", "all");



                // the reason for the SSLEngine’s complaint is that you enabled only the RSA cipher, but your certificate uses DSA keys. 
                CLRProgram.makecert(host: "192.168.1.12", port: 8443);

                //var xSSLContext = javax.net.ssl.SSLContext.getInstance("SSL");

                // ?
                var xSSLContext = javax.net.ssl.SSLContext.getInstance("TLSv1.2");

                Console.WriteLine(new { xSSLContext });


                var myTrustManagerArray = new[] { new TrustEveryoneManager() };

                // null?
                xSSLContext.init(
                    // SunMSCAPI ?
                    null, 
                    
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

                #region useless
                // You can't do it with the system properties. You would have to write and load your own X509KeyManager and create your own SSLContext with it.

                var keyStore = java.lang.System.getProperty("javax.net.ssl.keyStore");
                Console.WriteLine(new { keyStore });
                var keyStorePassword = java.lang.System.getProperty("javax.net.ssl.keyStorePassword");
                Console.WriteLine(new { keyStorePassword });
                #endregion


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

                var xSSLSocket = ss443.accept() as javax.net.ssl.SSLSocket;

                Console.WriteLine(new { xSSLSocket });

                xSSLSocket.setNeedClientAuth(true);

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
                var byte0 = xNetworkStream.ReadByte();

                //{ cf = sun.security.ssl.SSLSocketFactoryImpl@93f13f }
                //{ ssf = sun.security.ssl.SSLServerSocketFactoryImpl@15dc721 }
                //{ ss443 = [SSL: ServerSocket[addr=0.0.0.0/0.0.0.0,localport=8443]] }
                //{ xSSLSocket = 1747f59[SSL_NULL_WITH_NULL_NULL: Socket[addr=/192.168.1.196,port=55953,localport=8443]] }
                //{ xNetworkStream = ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__NetworkStream@538cc2 }
                //{ byte0 = -1 }

                Console.WriteLine(new { byte0 });


                //var xStreamReader = new StreamReader(xNetworkStream);
                //var line0 = xStreamReader.ReadLine();
                //Console.WriteLine(new { line0 });


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

            CLRProgram.CLRMain();
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











