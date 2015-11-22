extern alias jvm;
using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.BCLImplementation.System.Net.Sockets;
using ScriptCoreLibJava.Extensions;
using System;

using System.Collections;

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace UbuntuTCPMultiplex
{

    public static class Program2
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151122

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151023/ubuntuwebapplication
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151012


        static async void IntegrityWatchdog(FileInfo f)
        {
            var user = jvm::java.lang.System.getProperty("user.name");
            var snapshot = new { f.Length };


            Console.WriteLine("hello. " + new { user, f.Name, snapshot.Length });

            do
            {
                await Task.Delay(1000);
            }
            while (f.Length == snapshot.Length);

            Console.WriteLine("module has been changed. " + new { user, f.Name, f.Length });

            Environment.Exit(555);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        public static void Main(string[] args)
        {
            //  ps aux | grep java

            // http://iak1973.blogspot.com.ee/2010/02/transfer-files-accross-machines-using.html
            //Console.WriteLine("hello");


            // Z:\jsc.svn\examples\java\hybrid\ubuntu\UbuntuBootExperiment\UbuntuBootExperiment\Program.cs

            // if we run as clr in debugger.
            // lets spawn us on U: ?
            // we are in jvm already!
            //if (Debugger.IsAttached)
            //{
            //    // release build shall prepare and copy us over to U:
            //    // what about android? can we debug hop onto android?
            //    var me = new FileInfo(System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Assembly.Location);




            try
            {
                IntegrityWatchdog(new FileInfo(typeof(Program2).Assembly.Location));


                var me = new FileInfo(typeof(Program2).Assembly.Location);

                // lets not depend on u:
                //var meAtUbuntu = new FileInfo("u:/" + me.Name);

                // already on ubuntu filesystem?
                var meAtUbuntu = me.FullName.StartsWith("/");
                if (!meAtUbuntu)
                    Console.WriteLine("will hop to ubuntu... " + new { me.FullName });
                else
                    Console.WriteLine("hop to ubuntu done. " + new { me.FullName });


                if (!meAtUbuntu)
                {
                    // http://stackoverflow.com/questions/6223765/start-a-java-process-using-runtime-exec-processbuilder-start-with-low-priori
                    // http://stackoverflow.com/questions/13598996/putty-wont-cache-the-keys-to-access-a-server-when-run-script-in-hudson
                    // HKEY_USERS\.DEFAULT\Software\SimonTatham\PuTTY\SshHostkeys

                    //Console.WriteLine(new { cmd });

                    // http://stackoverflow.com/questions/17120782/running-bat-file-with-java-processbuilder
                    var p = Process.Start(
                        //cmd.FullName,
                        "cmd.exe",
                        @"/C call X:\jsc.internal.git\keystore\red\plink.xmikro.bat  java -jar /home/xmikro/Desktop/staging/UbuntuTCPMultiplex.exe"
                    );

                    //Thread.Sleep(500);

                    ////p.StandardInput.Write('y');

                    //Console.WriteLine("StandardInput.Write");
                    //p.StandardInput.Write("y\n");

                    p.WaitForExit();

                    Console.WriteLine("WaitForExit " + new { p.ExitCode });

                    //return;
                }
                else
                {

                    Main2(args);
                }
            }
            catch (Exception fault)
            {
                Console.WriteLine("fault " + new { fault.Message, fault.StackTrace });


            }
        }


        public static void Main2(string[] args)
        {
            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );



            var keystore0 = new FileInfo(
              new FileInfo(typeof(Program2).Assembly.Location).Directory.FullName + "/.keystore"
             );


            // do we have the keys?
            Console.WriteLine(new { keystore0 });
            // { keystore0 = { FullName = /home/xmikro/Desktop/staging/.keystore, Exists = true } }



            try
            {

                var xSSLContext = jvm::javax.net.ssl.SSLContext.getInstance("TLSv1.2");
                var xTrustEveryoneManager = new[] { new ScriptCoreLib.Java.TrustEveryoneManager() };
                var xKeyManager = new[] { new ScriptCoreLib.Java.localKeyManager(keystorepath: keystore0.FullName) };

                //Console.WriteLine("SSLContext init...");
                xSSLContext.init(
                    // SunMSCAPI ?
                    xKeyManager,
                    xTrustEveryoneManager,
                    new jvm::java.security.SecureRandom()
                );

                var xSSLSocketFactory = xSSLContext.getSocketFactory();
                var xSSLServerSocketFactory = xSSLContext.getServerSocketFactory();



                // init once
                UpgradeToSSL_server = xSSLServerSocketFactory.createServerSocket(UpgradeToSSL_serverport) as jvm::javax.net.ssl.SSLServerSocket;




                // first are we able to run async?


                var s = new SemaphoreSlim(0);

                //java.lang.Object, rt
                //enter async { ManagedThreadId = 1 }
                //awaiting for SemaphoreSlim{ ManagedThreadId = 1 }
                //after delay{ ManagedThreadId = 8 }
                //http://127.0.0.1:8080
                //{ fileName = http://127.0.0.1:8080 }
                //enter catch { mname = <0032> nop.try } ClauseCatchLocal:
                //{ Message = , StackTrace = java.lang.RuntimeException
                //        at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__TcpListener.AcceptTcpClientAsync(__TcpListener.java:131)

                new { }.With(
                    async delegate
                    {
                        //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
                        //enter async { ManagedThreadId = 1 }
                        //awaiting for SemaphoreSlim{ ManagedThreadId = 1 }
                        //after delay{ ManagedThreadId = 4 }
                        //http://127.0.0.1:8080
                        //awaiting for SemaphoreSlim. done.{ ManagedThreadId = 1 }
                        //--
                        //accept { c = System.Net.Sockets.TcpClient, ManagedThreadId = 6 }
                        //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
                        //accept { c = System.Net.Sockets.TcpClient, ManagedThreadId = 8 }
                        //{ ManagedThreadId = 6, input = GET / HTTP/1.1


                        Console.WriteLine("enter async " + new { Thread.CurrentThread.ManagedThreadId });

                        // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAsync\ChromeTCPServerAsync\Application.cs
                        await Task.Delay(100);

                        Console.WriteLine("after delay" + new { Thread.CurrentThread.ManagedThreadId });

                        // Additional information: Only one usage of each socket address (protocol/network address/port) is normally permitted
                        // close the other server!

                        //var l = new TcpListener(IPAddress.Any, 8080);

                        var port = 8083;
                        var l = new TcpListener(IPAddress.Any, port);

                        l.Start();


                        var href =
                            "http://127.0.0.1:" + port;

                        Console.WriteLine(
                            href
                        );


                        // running on ubuntu?

                        //Process.Start(
                        //    href
                        //);


                        new { }.With(
                            async delegate
                            {
                                while (true)
                                {
                                    var c = await l.AcceptTcpClientAsync();

                                    Console.WriteLine("accept " + new { c, Thread.CurrentThread.ManagedThreadId });

                                    yield(c, xSSLServerSocketFactory);
                                }
                            }
                        );

                        // jump back to main thread..
                        s.Release();
                    }
                );

                Console.WriteLine("awaiting for SemaphoreSlim" + new { Thread.CurrentThread.ManagedThreadId });
                s.Wait();
                Console.WriteLine("awaiting for SemaphoreSlim. done." + new { Thread.CurrentThread.ManagedThreadId });

            }
            catch
            {
                throw;
            }

            //System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().WithEach(
            // n =>
            // {
            //	 // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\NetworkInformation\NetworkInterface.cs

            //	 var IPProperties = n.GetIPProperties();
            //	 var PhysicalAddress = n.GetPhysicalAddress();

            //	 var InetAddressesString = "";


            //	 foreach (var ip in IPProperties.UnicastAddresses)
            //	 {
            //		 //if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            //		 //{
            //		 //Console.WriteLine(ip.Address.ToString());
            //		 InetAddressesString += ", " + ip.Address;
            //		 //}
            //	 }

            //	 // Address = {192.168.43.1}
            //	 //IPProperties.GatewayAddresses.WithEach(
            //	 //    g =>
            //	 //    {
            //	 //        InetAddressesString += ", " + g.Address;
            //	 //    }
            //	 //);


            //	 Console.WriteLine(new
            //	 {
            //		 n.OperationalStatus,

            //		 n.Name,
            //		 n.Description,
            //		 n.SupportsMulticast,
            //		 //n.NetworkInterfaceType,

            //		 InetAddressesString
            //	 });
            // }
            //);

            Console.WriteLine("--");


            Console.ReadLine();

            //CLRProgram.CLRMain();
        }

        static async void yield(TcpClient c, javax.net.ssl.SSLServerSocketFactory xSSLServerSocketFactory)
        {
            var s = c.GetStream();

            // could we switch into a worker thread?
            // jsc would need to split the stream object tho

            var buffer = new byte[1024];
            // why no implict buffer?
            var count = await s.ReadAsync(buffer, 0, buffer.Length);

            if (count > 0)
            {
                if (buffer[0] == 0x16)
                {
                    __Socket csocket = c.Client;

                    // upgrade in lockstep
                    //lock (xSSLServerSocketFactory)
                    {
                        Console.WriteLine("SSL? do we have a socket to upgrade? " + new { csocket.InternalSocket, csocket.InternalServerSocket });
                        // SSL? do we have a socket to upgrade? { InternalSocket = Socket[addr=/192.168.1.196,port=56752,localport=8083], InternalServerSocket =  }

                        // can we upgrade in place?
                        // http://docs.oracle.com/javase/7/docs/api/javax/net/ssl/SSLSocketFactory.html

                        // { Message = Received fatal alert: unexpected_message, StackTrace = java.lang.RuntimeException: Received fatal alert: unexpected_message
                        // cannot peek if we upgrade?
                        s = UpgradeToSSL(xSSLServerSocketFactory, c, count, buffer);

                    }

                    // reread after SSL
                    buffer = new byte[1024];
                    count = await s.ReadAsync(buffer, 0, buffer.Length);
                }

                var input = Encoding.UTF8.GetString(buffer, 0, count);

                //new IHTMLPre { new { input } }.AttachToDocument();
                Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId, input });

                var outputString = "HTTP/1.0 200 OK \r\nConnection: close\r\n\r\n"
                + "hello world ! <a href='https://.?foo'>https</a> jvm clr android async tcp? udp?" + new { Environment.ProcessorCount } + "\r\n";
                var obuffer = Encoding.UTF8.GetBytes(outputString);

                await s.WriteAsync(obuffer, 0, obuffer.Length);

            }

            c.Close();
        }




        //static int UpgradeToSSL_serverport = 18999;
        static int UpgradeToSSL_serverport = 18199;
        static jvm::javax.net.ssl.SSLServerSocket UpgradeToSSL_server;


        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        private static NetworkStream UpgradeToSSL(javax.net.ssl.SSLServerSocketFactory xSSLServerSocketFactory, TcpClient c, int count, byte[] buffer)
        {
            NetworkStream s = null;

            //lock (xSSLServerSocketFactory)
            {
                Console.WriteLine("UpgradeToSSL " + new { count });


                // we cannot upgrade in place because byte 0x16 was already read.
                // need to feed it all to the new serversocket?

                try
                {




                    // need to bridge

                    new Thread(
                        delegate()
                        {
                            Console.WriteLine("new bridge");
                            Thread.Sleep(1);
                            // accept called yet?

                            var cc = new TcpClient();
                            cc.Connect("127.0.0.1", UpgradeToSSL_serverport);

                            Console.WriteLine("new bridge connected");

                            // first lets flush the buffer.

                            cc.GetStream().Write(buffer, 0, count);

                            // at the same time start reading server to reply to client

                            new Thread(
                                delegate()
                                {
                                    Console.WriteLine("new reply bridge");

                                    var rx = new byte[2048];
                                    var rxa = true;
                                    while (rxa)
                                    {
                                        var rxc = cc.GetStream().Read(rx, 0, rx.Length);

                                        if (rxc < 0)
                                        {
                                            rxa = false;
                                        }
                                        else
                                        {
                                            c.GetStream().Write(rx, 0, rxc);
                                        }
                                    }
                                }
                            ).Start();


                            // keep reading client and writing to server...

                            var tx = new byte[2048];
                            var txa = true;

                            while (txa)
                            {
                                var txc = c.GetStream().Read(tx, 0, tx.Length);

                                if (txc < 0)
                                {
                                    txa = false;
                                }
                                else
                                {
                                    cc.GetStream().Write(tx, 0, txc);
                                }
                            }

                        }
                    ).Start();

                    var xSSLSocket = UpgradeToSSL_server.accept() as jvm::javax.net.ssl.SSLSocket;

                    // need to init our context!
                    //var f = (javax.net.ssl.SSLSocketFactory)javax.net.ssl.SSLSocketFactory.getDefault();
                    //var xSSLSocket = (javax.net.ssl.SSLSocket)xSSLSocketFactory.createSocket(csocket.InternalSocket,

                    //    // why we need that?
                    //    "127.0.0.1", 443,
                    //    true);

                    //// now what?
                    //// ssl.startHandshake();
                    //// xSslStream.AuthenticateAsServer(


                    xSSLSocket.startHandshake();


                    var xNetworkStream = new jvm::ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__NetworkStream
                    {
                        //InternalDiagnostics = true,


                        InternalInputStream = xSSLSocket.getInputStream(),
                        InternalOutputStream = xSSLSocket.getOutputStream()
                    };

                    s = xNetworkStream;
                }
                catch
                {
                    throw;
                }
            }

            return s;
        }


        // http://stackoverflow.com/questions/13874387/create-app-with-sslsocket-java
    }


    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
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



//- javac
//"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\UbuntuTCPMultiplex\Program.java
//Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task_1.java:147: error: _1_GetAwaiter_public_ldftn_0017() in __Task_1 cannot override _1_GetAwaiter_public_ldftn_0017() in __Task
//    public final  boolean _1_GetAwaiter_public_ldftn_0017()
//                          ^
//  overridden method is final
//Y:\staging\web\java\JVMCLRTCPServerAsync__i__d\Internal\Library\StringConversions.java:207: error: incompatible types
//        for (num5 = 0; num5; num5++)
//                       ^
//  required: boolean
//  found:    int

//- javac
//"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\UbuntuTCPMultiplex\Program.java
//Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task_1.java:147: error: _1_GetAwaiter_public_ldftn_0017() in __Task_1 cannot override _1_GetAwaiter_public_ldftn_0017() in __Task
//    public final  boolean _1_GetAwaiter_public_ldftn_0017()
//                          ^
//  overridden method is final