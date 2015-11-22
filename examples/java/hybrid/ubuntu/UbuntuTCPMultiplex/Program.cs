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

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        public static void Main(string[] args)
        {
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

            //    Console.WriteLine("hop to ubuntu from " + new { me.Name, me.Length });

            //    //U:\>dir UbuntuTCP*
            //    // Volume in drive U is staging
            //    // Volume Serial Number is 76DF-E023

            //    // Directory of U:\

            //    //2015-11-22  17:35           317,309 UbuntuTCPMultiplex.exe
            //    //               1 File(s)        317,309 bytes


            try
            {


                var me = new FileInfo(typeof(Program2).Assembly.Location);
                var meAtUbuntu = new FileInfo("u:/" + me.Name);

                if (meAtUbuntu.Exists)
                    Console.WriteLine("will hop to ubuntu... " + new { meAtUbuntu.Exists, me.FullName });
                else
                    Console.WriteLine("hop to ubuntu done. " + new { meAtUbuntu.Exists, me.FullName });


                if (meAtUbuntu.Exists)
                {
                    // http://stackoverflow.com/questions/6223765/start-a-java-process-using-runtime-exec-processbuilder-start-with-low-priori
                    // http://stackoverflow.com/questions/13598996/putty-wont-cache-the-keys-to-access-a-server-when-run-script-in-hudson
                    // HKEY_USERS\.DEFAULT\Software\SimonTatham\PuTTY\SshHostkeys



                    //var cargs = "-batch -ssh xmikro@192.168.1.189 -P 7022 -pw xmikro ls";
                    //var cargs = @"/C start /WAIT X:\util\plink.exe -batch -ssh xmikro@192.168.1.189 -P 7022 -pw xmikro java -jar /home/xmikro/Desktop/staging/UbuntuTCPMultiplex.exe";
                    //var cargs = @"/C call X:\util\plink.exe -batch -ssh xmikro@192.168.1.189 -P 7022 -pw xmikro java -jar /home/xmikro/Desktop/staging/UbuntuTCPMultiplex.exe";
                    //var cmd = new FileInfo(@"X:\util\plink.exe");
                    //var cmd = new FileInfo(@"cmd.exe");

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

                    var l = new TcpListener(IPAddress.Any, 8082);

                    l.Start();


                    var href =
                        "http://127.0.0.1:8080";

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

                                yield(c);
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

        static async void yield(TcpClient c)
        {
            var s = c.GetStream();

            // could we switch into a worker thread?
            // jsc would need to split the stream object tho

            var buffer = new byte[1024];
            // why no implict buffer?
            var count = await s.ReadAsync(buffer, 0, buffer.Length);

            if (count > 0)
            {
                var input = Encoding.UTF8.GetString(buffer, 0, count);

                //new IHTMLPre { new { input } }.AttachToDocument();
                Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId, input });

                var outputString = "HTTP/1.0 200 OK \r\nConnection: close\r\n\r\n"
                + "hello world. jvm clr android async tcp? udp?" + new { Environment.ProcessorCount } + "\r\n";
                var obuffer = Encoding.UTF8.GetBytes(outputString);

                await s.WriteAsync(obuffer, 0, obuffer.Length);

            }

            c.Close();
        }
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