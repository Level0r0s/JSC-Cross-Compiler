using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UbuntuUDPAdvertise
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151023/ubuntuwebapplication
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151022/httprequest
        // Z:\jsc.svn\examples\javascript\appengine\test\TestRedirect\ApplicationWebService.cs

        public void Handler(ScriptCoreLib.Ultra.WebService.WebServiceHandler h)
        {
            if (advertise == null)
            {

                // X:\jsc.svn\examples\javascript\Test\TestWebMethodIPAddress\TestWebMethodIPAddress\ApplicationWebService.cs


                #region HostUri
                var Referer = h.Context.Request.Headers["Referer"];
                if (Referer == null)
                    Referer = "any";

                var HostUri = new
                {
                    Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":"),
                    Port = h.Context.Request.Headers["Host"].SkipUntilOrEmpty(":")
                };
                #endregion

                Console.WriteLine(new { HostUri });

                //compiled! launching server! please wait...
                //20426 -> 17094
                //http://192.168.43.252:20426

                //> 0001 0x0163 bytes
                //{ HostUri = { Host = 192.168.43.252, Port = 20426 } }


                // https://managedupnp.codeplex.com/
                // http://www.fluxbytes.com/csharp/upnp-port-forwarding-the-easy-way/

                // upnp ?
                //advertise = async delegate
                advertise = delegate
                {
                    #region  NIC
                    var data =
                                         from n in NetworkInterface.GetAllNetworkInterfaces()

                                         let SupportsMulticast = n.SupportsMulticast

                                         from u in n.GetIPProperties().UnicastAddresses

                                         let IsLoopback = IPAddress.IsLoopback(u.Address)

                                         let IPv4 = u.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork

                                         // http://compnetworking.about.com/od/workingwithipaddresses/l/aa042400b.htm
                                         // http://ipaddressextensions.codeplex.com/SourceControl/latest#WorldDomination.Net/IPAddressExtensions.cs


                                         //- javac
                                         //"C:\Program Files (x86)\Java\jdk1.6.0_35\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRPrivateAddress\Program.java
                                         //java\JVMCLRPrivateAddress\Program.java:435: <T>Of(T[]) in ScriptCoreLib.Shared.BCLImplementation.System.__SZArrayEnumerator_1 
                                         // cannot be applied to <java.lang.Integer>(int[])
                                         //        return  new __AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_82__51__79_6_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_76__48__76_5_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_69__45__73_4_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_64__42__70_3_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_59__39__67_2_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_54__36__64_1_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_11__28__30_0_2<__NetworkInterface, Boolean>, __UnicastIPAddressInformation>, Boolean>, Boolean>, byte[]>, int[]>, Boolean>(__h__TransparentIdentifier5, __Enumerable.<Integer>Contains(__SZArrayEnumerator_1.<Integer>Of(__h__TransparentIdentifier5.get_PrivateAddresses()), (short)(__h__TransparentIdentifier5.get___h__TransparentIdentifier4().get_AddressBytes()[0] & 0xff)));
                                         //                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      ^
                                         //Note: Some input files use unchecked or unsafe operations.
                                         //Note: Recompile with -Xlint:unchecked for details.

                                         let get_IsPrivate = new Func<bool>(
                                                        delegate
                                                        {
                                                            //Console.WriteLine("get_IsPrivate " + new { SupportsMulticast, n.Description, u.Address, IPv4 });

                                                            var AddressBytes = u.Address.GetAddressBytes();

                                                            // should do a full mask check?
                                                            // http://en.wikipedia.org/wiki/IP_address
                                                            //var PrivateAddresses = new[] { 10, 172, 192 };

                                                            //- javac
                                                            //"C:\Program Files (x86)\Java\jdk1.6.0_35\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRPrivateAddress\Program.java
                                                            //Y:\staging\web\java\JVMCLRPrivateAddress\Program___c__DisplayClass2b.java:36: <T>Of(T[]) in ScriptCoreLib.Shared.BCLImplementation.System.__SZArrayEnumerator_1 cannot be applied to <java.lang.Integer>(int[])
                                                            //        enumerable_11 = __Enumerable.<Integer>AsEnumerable(__SZArrayEnumerator_1.<Integer>Of(new int[] {
                                                            //                                                                                ^
                                                            //Y:\staging\web\java\JVMCLRPrivateAddress\Program___c__DisplayClass2b.java:38: <TSource>Contains(ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic.__IEnumerable_1<TSource>,TSource) in ScriptCoreLib.
                                                            //        return  __Enumerable.<Integer>Contains(enumerable_11, (short)(byteArray0[0] & 0xff));
                                                            //                            ^


                                                            //return PrivateAddresses.Contains(AddressBytes[0]);

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

                                         select new
                                         {
                                             IsPrivate,
                                             IsLoopback,
                                             SupportsMulticast,
                                             IPv4,
                                             u,
                                             n
                                         };


                    var g = from x in data

                            let get_key = new Func<bool>(
                                delegate
                                {
                                    //Console.WriteLine("get_key " + new { x.IsPrivate, x.IsLoopback, x.SupportsMulticast, x.IPv4 });



                                    return x.IsPrivate && !x.IsLoopback && x.SupportsMulticast && x.IPv4;
                                }
                            )

                            let key = get_key()

                            // group by missing from scriptcorelib?

                            let gkey = new { x.u, x.n.Description, key }
                            //let gvalue = new { key }

                            // Caused by: java.lang.RuntimeException: Implement IComparable for __AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_115__82__110_d_1 vs __AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_115__82__110_d_1

                            group gkey by key;


                    // X:\jsc.svn\examples\java\hybrid\JVMCLRPrivateAddress\JVMCLRPrivateAddress\Program.cs

                    //{ Key = True, gi = 0 }
                    //{ Address = 192.168.43.12, Description = TP - LINK Wireless USB Adapter }
                    //{ Address = 192.168.136.1, Description = VMware Virtual Ethernet Adapter for VMnet1 }
                    //{ Address = 192.168.81.1, Description = VMware Virtual Ethernet Adapter for VMnet8 }

                    //g.Reverse().WithEachIndex(
                    //      (gx, gi) =>
                    //      {

                    //          Console.WriteLine(new { gx.Key, gi });

                    //          gx.WithEach(
                    //                x =>
                    //                {
                    //                    Console.WriteLine(new { x.u.Address, x.Description });

                    //                }
                    //            );

                    //      }
                    //  );
                    #endregion




                    var message =
                        new XElement("string",
                            new XAttribute("c", "" + 1),
                              "Visit me at " + HostUri.Host + ":" + HostUri.Port
                        ).ToString();


                    //Console.WriteLine(new { HostUri });
                    Console.WriteLine(new { message });

                    // android send
                    // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\Java\InternalAndroidWebServiceActivity.cs

                    // chrome send
                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServer\ChromeTCPServer\Application.cs

                    // clr send
                    // X:\jsc.svn\market\Abstractatech.Multicast\Abstractatech.Multicast\Library\MulticastListener.cs

                    // new clr send:

                    var port = new Random().Next(16000, 40000);

                    var socket = new UdpClient();


                    socket.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                    socket.ExclusiveAddressUse = false;
                    socket.EnableBroadcast = true;

                    var aa = g.Single(x => x.Key).First().u.Address;

                    Console.WriteLine(new { aa });

                    //var loc = new IPEndPoint(IPAddress.Any, port);
                    var loc = new IPEndPoint(aa, port);
                    socket.Client.Bind(loc);


                    //socket.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"), 30);

                    //var remote = new IPEndPoint(IPAddress.Parse("239.1.2.3"), 40404);
                    //socket.Connect(remote);


                    var xdata = Encoding.UTF8.GetBytes(message.ToString());    //creates a variable b of type byte

                    // 
                    //Additional information: Cannot send packets to an arbitrary host while connected.
                    //var result = await socket.SendAsync(xdata, xdata.Length, "239.1.2.3", 40404);
                    var result = socket.Send(xdata, xdata.Length, "239.1.2.3", 40404);
                    //var result = await socket.SendAsync(data, data.Length);


                    socket.Close();

                };

                advertise2 = delegate
                {
                    #region  NIC
                    var data =
                                         from n in NetworkInterface.GetAllNetworkInterfaces()

                                         let SupportsMulticast = n.SupportsMulticast

                                         from u in n.GetIPProperties().UnicastAddresses

                                         let IsLoopback = IPAddress.IsLoopback(u.Address)

                                         let IPv4 = u.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork

                                         // http://compnetworking.about.com/od/workingwithipaddresses/l/aa042400b.htm
                                         // http://ipaddressextensions.codeplex.com/SourceControl/latest#WorldDomination.Net/IPAddressExtensions.cs


                                         //- javac
                                         //"C:\Program Files (x86)\Java\jdk1.6.0_35\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRPrivateAddress\Program.java
                                         //java\JVMCLRPrivateAddress\Program.java:435: <T>Of(T[]) in ScriptCoreLib.Shared.BCLImplementation.System.__SZArrayEnumerator_1 
                                         // cannot be applied to <java.lang.Integer>(int[])
                                         //        return  new __AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_82__51__79_6_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_76__48__76_5_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_69__45__73_4_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_64__42__70_3_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_59__39__67_2_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_54__36__64_1_2<__AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_11__28__30_0_2<__NetworkInterface, Boolean>, __UnicastIPAddressInformation>, Boolean>, Boolean>, byte[]>, int[]>, Boolean>(__h__TransparentIdentifier5, __Enumerable.<Integer>Contains(__SZArrayEnumerator_1.<Integer>Of(__h__TransparentIdentifier5.get_PrivateAddresses()), (short)(__h__TransparentIdentifier5.get___h__TransparentIdentifier4().get_AddressBytes()[0] & 0xff)));
                                         //                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      ^
                                         //Note: Some input files use unchecked or unsafe operations.
                                         //Note: Recompile with -Xlint:unchecked for details.

                                         let get_IsPrivate = new Func<bool>(
                                                        delegate
                                                        {
                                                            //Console.WriteLine("get_IsPrivate " + new { SupportsMulticast, n.Description, u.Address, IPv4 });

                                                            var AddressBytes = u.Address.GetAddressBytes();

                                                            // should do a full mask check?
                                                            // http://en.wikipedia.org/wiki/IP_address
                                                            //var PrivateAddresses = new[] { 10, 172, 192 };

                                                            //- javac
                                                            //"C:\Program Files (x86)\Java\jdk1.6.0_35\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRPrivateAddress\Program.java
                                                            //Y:\staging\web\java\JVMCLRPrivateAddress\Program___c__DisplayClass2b.java:36: <T>Of(T[]) in ScriptCoreLib.Shared.BCLImplementation.System.__SZArrayEnumerator_1 cannot be applied to <java.lang.Integer>(int[])
                                                            //        enumerable_11 = __Enumerable.<Integer>AsEnumerable(__SZArrayEnumerator_1.<Integer>Of(new int[] {
                                                            //                                                                                ^
                                                            //Y:\staging\web\java\JVMCLRPrivateAddress\Program___c__DisplayClass2b.java:38: <TSource>Contains(ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic.__IEnumerable_1<TSource>,TSource) in ScriptCoreLib.
                                                            //        return  __Enumerable.<Integer>Contains(enumerable_11, (short)(byteArray0[0] & 0xff));
                                                            //                            ^


                                                            //return PrivateAddresses.Contains(AddressBytes[0]);

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

                                         select new
                                         {
                                             IsPrivate,
                                             IsLoopback,
                                             SupportsMulticast,
                                             IPv4,
                                             u,
                                             n
                                         };


                    var g = from x in data

                            let get_key = new Func<bool>(
                                delegate
                                {
                                    //Console.WriteLine("get_key " + new { x.IsPrivate, x.IsLoopback, x.SupportsMulticast, x.IPv4 });



                                    return x.IsPrivate && !x.IsLoopback && x.SupportsMulticast && x.IPv4;
                                }
                            )

                            let key = get_key()

                            // group by missing from scriptcorelib?

                            let gkey = new { x.u, x.n.Description, key }
                            //let gvalue = new { key }

                            // Caused by: java.lang.RuntimeException: Implement IComparable for __AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_115__82__110_d_1 vs __AnonymousTypes__JVMCLRPrivateAddress__i__d_jvm.__f__AnonymousType_115__82__110_d_1

                            group gkey by key;


                    // X:\jsc.svn\examples\java\hybrid\JVMCLRPrivateAddress\JVMCLRPrivateAddress\Program.cs

                    //{ Key = True, gi = 0 }
                    //{ Address = 192.168.43.12, Description = TP - LINK Wireless USB Adapter }
                    //{ Address = 192.168.136.1, Description = VMware Virtual Ethernet Adapter for VMnet1 }
                    //{ Address = 192.168.81.1, Description = VMware Virtual Ethernet Adapter for VMnet8 }

                    //g.Reverse().WithEachIndex(
                    //      (gx, gi) =>
                    //      {

                    //          Console.WriteLine(new { gx.Key, gi });

                    //          gx.WithEach(
                    //                x =>
                    //                {
                    //                    Console.WriteLine(new { x.u.Address, x.Description });

                    //                }
                    //            );

                    //      }
                    //  );
                    #endregion




                    var message =
                        new XElement("string",
                            new XAttribute("c", "" + 1),
                              "Visit me at " + HostUri.Host + ":" + HostUri.Port
                        ).ToString();


                    //Console.WriteLine(new { HostUri });
                    Console.WriteLine(new { message });

                    // android send
                    // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\Java\InternalAndroidWebServiceActivity.cs

                    // chrome send
                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServer\ChromeTCPServer\Application.cs

                    // clr send
                    // X:\jsc.svn\market\Abstractatech.Multicast\Abstractatech.Multicast\Library\MulticastListener.cs

                    // new clr send:

                    var port = new Random().Next(16000, 40000);

                    var aa = g.Single(x => x.Key).First().u.Address;

                    Console.WriteLine(new { aa });

                    //var socket = new UdpClient(new IPEndPoint(aa, port));
                    //var socket = new UdpClient();

                    // windowns needs nic hint
                    var socket = new UdpClient(new IPEndPoint(aa, port));


                    //socket.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                    //socket.ExclusiveAddressUse = false;
                    //socket.EnableBroadcast = true;





                    //socket.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"), 30);

                    //var remote = new IPEndPoint(IPAddress.Parse("239.1.2.3"), 40404);
                    //socket.Connect(remote);


                    var xdata = Encoding.UTF8.GetBytes(message.ToString());    //creates a variable b of type byte

                    // 
                    //Additional information: Cannot send packets to an arbitrary host while connected.
                    //var result = await socket.SendAsync(xdata, xdata.Length, "239.1.2.3", 40404);
                    var result = socket.Send(xdata, xdata.Length, "239.1.2.3", 40404);
                    //var result = await socket.SendAsync(data, data.Length);


                    socket.Close();

                };
            }
        }

        static Action advertise;
        static Action advertise2;
        // what about <style>...</style>



        //public async Task InvokeAdvertise()
        public Task InvokeAdvertise()
        {
            Console.WriteLine("enter InvokeAdvertise");

            //new IHTMLButton { innerText = "advertise" }.AttachToDocument().WhenClicked(x => InvokeAdvertise());

            if (advertise != null)
                advertise();


            return Task.CompletedTask;
        }

        public Task InvokeAdvertise2()
        {
            Console.WriteLine("enter InvokeAdvertise2");

            //new IHTMLButton { innerText = "advertise" }.AttachToDocument().WhenClicked(x => InvokeAdvertise());

            if (advertise2 != null)
                advertise2();


            return Task.CompletedTask;
        }
    }
}


// type: UbuntuUDPAdvertise.ApplicationWebService+<>c__DisplayClass1a+<<Handler>b__9>d__2e+<MoveNext>06000077, UbuntuU
// offset: 0x0012
//  method:Int32 <02e9> ldnull.try(<MoveNext>06000077, <<Handler>b__9>d__2e ByRef, System.Runtime.CompilerServices.Tas
//*** Compiler cannot continue... press enter to quit.

//Implementation not found for type import :
//type: System.Net.Sockets.UdpClient
//method: Void set_ExclusiveAddressUse(Boolean)
//Did you forget to add the [Script] attribute?
//Please double check the signature!
