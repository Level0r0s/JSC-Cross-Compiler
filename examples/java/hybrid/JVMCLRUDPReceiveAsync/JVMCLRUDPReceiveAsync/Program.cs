using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRUDPReceiveAsync
{

    static class Program
    {
        // X:\jsc.svn\examples\java\hybrid\JVMCLRUDPReceiveAsync\JVMCLRUDPReceiveAsync\bin\Release

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150630

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            //Console.WriteLine("");
            //var linein = Console.ReadLine();



            #region data
            var data =
                from n in NetworkInterface.GetAllNetworkInterfaces()
                let SupportsMulticast = n.SupportsMulticast
                from u in n.GetIPProperties().UnicastAddresses
                let IsLoopback = IPAddress.IsLoopback(u.Address)
                let IPv4 = u.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork

                // http://compnetworking.about.com/od/workingwithipaddresses/l/aa042400b.htm
                // http://ipaddressextensions.codeplex.com/SourceControl/latest#WorldDomination.Net/IPAddressExtensions.cs

                let get_IsPrivate = new Func<bool>(
                 delegate
                 {
                     Console.WriteLine("get_IsPrivate " + new { SupportsMulticast, n.Description, u.Address, IPv4 });

                     var AddressBytes = u.Address.GetAddressBytes();

                     // should do a full mask check?
                     // http://en.wikipedia.org/wiki/IP_address
                     //var PrivateAddresses = new[] { 10, 172, 192 };

                     if (AddressBytes[0] == 10)
                         return true;

                     if (AddressBytes[0] == 172)
                         return true;

                     if (AddressBytes[0] == 192)
                         return true;

                     return false;

                 }
                )


                let IsPrivate = get_IsPrivate()
                let IsCandidate = IsPrivate && !IsLoopback && SupportsMulticast && IPv4

                select new
                {
                    IsPrivate,
                    IsLoopback,
                    SupportsMulticast,
                    IPv4,
                    IsCandidate,

                    u,
                    n
                };
            #endregion

            var cc = data.First(x => x.IsCandidate);

            new { }.With(
                async delegate
                {
                    //var port = 8080;
                    //Console.WriteLine(":95");

                    //var cc = data.First(x => x.IsCandidate);
                    //Console.WriteLine(":98");

                    //var u = new UdpClient("127.0.0.1", port);
                    var uu = new UdpClient(49834);
                    //    new IPEndPoint(cc.u.Address, port)
                    //);

                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150721/udp
                    uu.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"), cc.u.Address);


                    Console.WriteLine(":106 ReceiveAsync...");

                    while (true)
                    {
                        // UdpReceiveResult
                        var x = await uu.ReceiveAsync();

                        Console.WriteLine(new { x.Buffer.Length } + Encoding.UTF8.GetString(x.Buffer));

                    }
                }
            );


            Console.WriteLine("invoke CLRMain...");
            try
            {
                CLRProgram.CLRMain();

                Console.WriteLine("invoke CLRMain... done");
            }
            catch (Exception err)
            {
                Console.WriteLine("invoke CLRMain... err " + new { err.Message, err.StackTrace });
            }

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


//:106 ReceiveAsync...
//invoke CLRMain...
//invoke CLRMain... err { Message = jni.CPtr.initIDs(Ljni/CPtr;)I, StackTrace = java.lang.UnsatisfiedLinkError: jni.CPtr.initIDs(Ljni/CPtr;)I
//        at jni.CPtr.initIDs(Native Method)
//        at jni.CPtr.<clinit>(CPtr.java:41)
//        at ScriptCoreLibJava.BCLImplementation.ScriptCoreLibA.Shared.__PlatformInvocationServices_Func.get_Method(__PlatformInvocationServices_Func.java:64)
//        at ScriptCoreLibJava.BCLImplementation.ScriptCoreLibA.Shared.__PlatformInvocationServices_Func.To__PlatformInvocationServices_Action(__PlatformInvocationServices_Func.java:93)
//        at ScriptCoreLibJava.BCLImplementation.ScriptCoreLibA.Shared.__PlatformInvocationServices.InvokeVoid(__PlatformInvocationServices.java:118)
//        at JVMCLRUDPReceiveAsync__i__d._0200001e__nativeimport_.export___06000081(_0200001e__nativeimport_.java:48)
//        at JVMCLRUDPReceiveAsync__i__d._0200001e_.CLRMain(_0200001e_.java:46)
//        at JVMCLRUDPReceiveAsync__i._02000005_____import.CLRMain(_02000005_____import.java:29)
//        at JVMCLRUDPReceiveAsync._02000004__________interfaceimport_.CLRMain(_02000004__________interfaceimport_.java:29)
//        at JVMCLRUDPReceiveAsync.Program.main(Program.java:271)
// }