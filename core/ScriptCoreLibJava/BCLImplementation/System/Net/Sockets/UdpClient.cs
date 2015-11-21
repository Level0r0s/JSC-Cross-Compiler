using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ScriptCoreLib.Shared.BCLImplementation.System.Net.Sockets;
using ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks;
using ScriptCoreLibJava.BCLImplementation.System.Net.NetworkInformation;
using ScriptCoreLib.Shared.BCLImplementation.System.Net;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Sockets
{
    // http://referencesource.microsoft.com/#System/net/System/Net/Sockets/UdpClient.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Net.Sockets/UdpClient.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\Sockets\UdpClient.cs
    // X:\jsc.svn\market\synergy\javascript\chrome\chrome\BCLImplementation\System\Net\Sockets\UdpClient.cs

    [Script(Implements = typeof(global::System.Net.Sockets.UdpClient))]
    internal class __UdpClient
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20/20130720
        // http://www.nax.cz/2014/10/03/send-udp-packet/
        // echo -n "hello" >/dev/udp/localhost/8000

        public const int MaxData = 1048576;

        // http://www.acc.umu.se/~bosse/High%20performance%20kernel%20mode%20web%20server%20for%20Windows.pdf
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150630/udp

        // multicast tested?



        public bool EnableBroadcast { get; set; }
        public bool ExclusiveAddressUse { get; set; }


        // what comes after tcp?
        // what about async API ?

        // X:\jsc.svn\examples\java\hybrid\JVMCLRUDPReceiveAsync\JVMCLRUDPReceiveAsync\Program.cs
        // X:\jsc.svn\examples\java\hybrid\JVMCLRUDPSendAsync\JVMCLRUDPSendAsync\Program.cs
        // X:\jsc.svn\examples\java\ConsoleMulticastExperiment\ConsoleMulticastExperiment\Program.cs
        // X:\jsc.svn\examples\java\android\AndroidServiceUDPNotification\AndroidServiceUDPNotification\ApplicationActivity.cs
        // X:\jsc.svn\examples\java\android\LANBroadcastListener\LANBroadcastListener\ApplicationActivity.cs

        // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\sys\socket.cs



        [Script]
        public class xConstructorArguments
        {
            public Func<java.net.DatagramSocket> vDatagramSocket;
            public java.net.DatagramSocket xDatagramSocket
            {
                get
                {
                    return vDatagramSocket();
                }
            }

            Func<java.net.MulticastSocket> vMulticastSocket;
            public java.net.MulticastSocket xMulticastSocket
            {
                get
                {
                    return vMulticastSocket();
                }
            }

            public static xConstructorArguments Of()
            {
                var xDatagramSocket = default(java.net.DatagramSocket);

                return new xConstructorArguments
                {
                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150721/ovroculus360photoshud
                    vDatagramSocket = delegate
                    {
                        // Z:\jsc.svn\examples\javascript\ubuntu\Test\UbuntuUDPAdvertise\UbuntuUDPAdvertise\ApplicationWebService.cs

                        //if (xDatagramSocket != null)
                        //    return xDatagramSocket;

                        //var lport = new Random().Next(1024, 30000);

                        //try
                        //{
                        //    Console.WriteLine("UdpClient " + new { lport });
                        //    xDatagramSocket = new java.net.DatagramSocket(lport);
                        //}
                        //catch
                        //{
                        //    throw;
                        //}


                        // bind instead?
                        return xDatagramSocket;
                    }
                };
            }

            public static xConstructorArguments Of(IPEndPoint e)
            {
                var xMulticastSocket = default(java.net.MulticastSocket);
                var xDatagramSocket = default(java.net.DatagramSocket);

                // X:\jsc.svn\examples\java\android\forms\FormsUDPJoinGroup\FormsUDPJoinGroup\ApplicationControl.cs
                return new xConstructorArguments
                {
                    vMulticastSocket = delegate
                    {
                        if (xMulticastSocket != null)
                            return xMulticastSocket;


                        #region datagramSocket

                        try
                        {
                            xMulticastSocket = new java.net.MulticastSocket(e.Port);
                            xDatagramSocket = xMulticastSocket;
                        }
                        catch
                        {
                            throw;
                        }
                        #endregion

                        return xMulticastSocket;
                    },

                    vDatagramSocket = delegate
                    {
                        if (xDatagramSocket != null)
                            return xDatagramSocket;

                        try
                        {
                            xDatagramSocket = new java.net.DatagramSocket(e.Port);
                        }
                        catch
                        {
                            throw;
                        }

                        return xDatagramSocket;
                    }
                };
            }

            // see udp joingroup
            public int port;

            public static xConstructorArguments Of(int port)
            {
                // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPFloats\ChromeUDPFloats\Application.cs

                var xMulticastSocket = default(java.net.MulticastSocket);
                var xDatagramSocket = default(java.net.DatagramSocket);

                // X:\jsc.svn\examples\java\android\forms\FormsUDPJoinGroup\FormsUDPJoinGroup\ApplicationControl.cs
                return new xConstructorArguments
                {
                    port = port,

                    vMulticastSocket = delegate
                    {
                        if (xMulticastSocket != null)
                            return xMulticastSocket;


                        #region datagramSocket

                        try
                        {
                            xMulticastSocket = new java.net.MulticastSocket(port);
                            xDatagramSocket = xMulticastSocket;
                        }
                        catch
                        {
                            throw;
                        }
                        #endregion

                        return xMulticastSocket;
                    },

                    vDatagramSocket = delegate
                    {
                        if (xDatagramSocket != null)
                            return xDatagramSocket;

                        try
                        {
                            xDatagramSocket = new java.net.DatagramSocket(port);
                        }
                        catch
                        {
                            throw;
                        }

                        return xDatagramSocket;
                    }
                };
            }
        }


        public __UdpClient(xConstructorArguments args)
        {
            //Console.WriteLine("enter __UdpClient ctor");


            // http://stackoverflow.com/questions/8558791/multicastsocket-vs-datagramsocket-in-broadcasting-to-multiple-clients
            // you must use MulticastSocket for receiving the multicasts; for sending them, again, you can use either DatagramSocket or MulticastSocket, and there is no difference in efficiency.

            //var buffer = new sbyte[0x10000];

            // X:\jsc.svn\market\synergy\javascript\chrome\chrome\BCLImplementation\System\Net\Sockets\UdpClient.cs
            //var buffer = new sbyte[0x1000];
            var buffer = new sbyte[MaxData];

            //E/dalvikvm-heap(14366): Out of memory on a 1048592-byte allocation.
            //I/dalvikvm(14366): "Thread-4310" prio=5 tid=827 RUNNABLE



            // tested by
            // X:\jsc.svn\examples\java\android\vr\OVRMyCubeWorldNDK\OVRMyCubeWorld\ApplicationActivity.cs

            #region vJoinMulticastGroup
            this.vJoinMulticastGroup = (IPAddress multicastAddr, IPAddress localAddress) =>
           {
               // http://developer.android.com/reference/java/net/InetSocketAddress.html
               // http://developer.android.com/reference/java/net/SocketAddress.html

               Console.WriteLine("enter vJoinMulticastGroup");
               // at this point we have to jump back in time and get a multicast socket.

               __IPAddress __multicastAddr = multicastAddr;
               __IPAddress __localAddress = localAddress;

               __NetworkInterface nic = __localAddress.InternalNetworkInterface;

               // X:\jsc.svn\examples\java\android\LANBroadcastListener\LANBroadcastListener\ApplicationActivity.cs
               // X:\jsc.svn\examples\java\android\forms\FormsUDPJoinGroup\FormsUDPJoinGroup\ApplicationControl.cs

               try
               {
                   args.xMulticastSocket.joinGroup(
                       new java.net.InetSocketAddress(
                            __multicastAddr.InternalAddress,
                            args.port
                       ),

                       nic
                   );
               }
               catch
               {
                   throw;
               }

           };
            #endregion


            #region vReceiveAsync
            this.vReceiveAsync = delegate
            {
                var c = new TaskCompletionSource<__UdpReceiveResult>();

                __Task.Run(
                    delegate
                    {
                        // http://stackoverflow.com/questions/10808512/datagrampacket-equivalent
                        // http://tutorials.jenkov.com/java-networking/udp-datagram-sockets.html


                        // tested by?
                        var packet = new java.net.DatagramPacket(buffer, buffer.Length);

                        try
                        {
                            args.xDatagramSocket.receive(packet);


                            var xbuffer = new byte[packet.getLength()];


                            Array.Copy(
                                buffer, xbuffer,
                                xbuffer.Length
                            );

                            var x = new __UdpReceiveResult(
                                buffer:
                                    xbuffer,

                                remoteEndPoint:
                                    new __IPEndPoint(
                                        new __IPAddress { InternalAddress = packet.getAddress() },
                                        port: packet.getPort()
                                    )
                            );

                            c.SetResult(x);
                        }
                        catch
                        {
                            // fault? 
                        }
                    }
                );


                return c.Task;
            };
            #endregion

            //Console.WriteLine("enter __UdpClient before this.Client");

            this.Client = new __Socket
           {
               #region vBind
               vBind = (EndPoint localEP) =>
               {
                   // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150721/ovroculus360photoshud

                   try
                   {
                       Console.WriteLine("enter __UdpClient vBind " + new { args.xDatagramSocket, localEP });


                       var v4 = localEP as IPEndPoint;
                       if (v4 != null)
                       {
                           __IPAddress v4a = v4.Address;



                           // http://developer.android.com/reference/java/net/InetSocketAddress.html
                           var x = new java.net.InetSocketAddress(v4a.InternalAddress, v4.Port);

                           Console.WriteLine("before __UdpClient vBind " + new { v4a.InternalAddress, v4.Port });

                           // Z:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\UDPWindWheel\Program.cs
                           // is this the first bind?

                           if (args.xDatagramSocket == null)
                           {
                               var xDatagramSocket = new java.net.DatagramSocket(v4.Port);

                               args.vDatagramSocket = () => xDatagramSocket;
                           }
                           else
                           {
                               args.xDatagramSocket.bind(x);
                           }
                       }

                   }
                   catch (Exception err)
                   {
                       Console.WriteLine("err __UdpClient vBind " + new { err.Message, err.StackTrace });
                       //throw;


                       throw new InvalidOperationException();
                   }
               }
               #endregion
           };



            //Console.WriteLine("enter __UdpClient after this.Client " + new { this.Client });


            #region vSend
            this.vSend = (byte[] datagram, int bytes, string hostname, int port) =>
            {
                //I/System.Console(22987): 59cb:0001 about to Send
                //I/System.Console(22987): 59cb:0001 enter __UdpClient vSend
                //I/System.Console(22987): 59cb:0001 err __UdpClient vSend { Message = , StackTrace = android.os.NetworkOnMainThreadException
                //I/System.Console(22987):        at android.os.StrictMode$AndroidBlockGuardPolicy.onNetwork(StrictMode.java:1147)
                //I/System.Console(22987):        at libcore.io.BlockGuardOs.sendto(BlockGuardOs.java:276)
                //I/System.Console(22987):        at libcore.io.IoBridge.sendto(IoBridge.java:513)
                //I/System.Console(22987):        at java.net.PlainDatagramSocketImpl.send(PlainDatagramSocketImpl.java:184)
                //I/System.Console(22987):        at java.net.DatagramSocket.send(DatagramSocket.java:305)
                //I/System.Console(22987):        at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__UdpClient___c__DisplayClass12.__ctor_b__6(__UdpClient___c__DisplayClass12.java:109)

                var t = this.vSendAsync(datagram, bytes, hostname, port);



                return t.Result;
            };
            #endregion


            #region vSendAsync
            this.vSendAsync = (byte[] datagram, int bytes, string hostname, int port) =>
            {
                var c = new TaskCompletionSource<int>();
                __Task.Run(
                    delegate
                    {
                        try
                        {
                            var a = global::java.net.InetAddress.getByName(hostname);
                            var packet = new java.net.DatagramPacket(
                                (sbyte[])(object)datagram,
                                bytes, a, port
                            );
                            args.xDatagramSocket.send(packet);
                            // retval tested?
                            c.SetResult(
                                packet.getLength()
                            );
                        }
                        catch
                        {
                            throw;
                        }
                    }
                );
                return c.Task;
            };
            #endregion

            #region vSendAsync2
            this.vSendAsync2 = (byte[] datagram, int bytes, IPEndPoint endPoint) =>
            {
                var c = new TaskCompletionSource<int>();
                __Task.Run(
                    delegate
                    {
                        try
                        {
                            var packet = new java.net.DatagramPacket(
                                (sbyte[])(object)datagram,
                                bytes, (__IPAddress)endPoint.Address, endPoint.Port
                            );
                            args.xDatagramSocket.send(packet);
                            // retval tested?
                            c.SetResult(
                                packet.getLength()
                            );
                        }
                        catch
                        {
                            throw;
                        }
                    }
                );
                return c.Task;
            };
            #endregion

            #region vClose
            this.vClose = delegate
            {
                try
                {
                    args.xDatagramSocket.close();
                }
                catch
                {
                }
            };
            #endregion

        }

        public __UdpClient()
            : this(xConstructorArguments.Of())
        {
        }

        public __UdpClient(int port)
            : this(xConstructorArguments.Of(port))
        {
        }


        public __UdpClient(IPEndPoint e)
            : this(xConstructorArguments.Of(e))
        {
        }


        // are we supposed to bind?
        public Socket Client { get; set; }


        public Action vClose;
        public void Close() { vClose(); }


        // X:\jsc.svn\examples\java\hybrid\JVMCLRUDPReceiveAsync\JVMCLRUDPReceiveAsync\Program.cs
        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Net\Sockets\UdpReceiveResult.cs
        public Func<Task<__UdpReceiveResult>> vReceiveAsync;
        public Task<__UdpReceiveResult> ReceiveAsync() { return vReceiveAsync(); }



        public Action<IPAddress, IPAddress> vJoinMulticastGroup;
        //public void JoinMulticastGroup(IPAddress multicastAddr) { vJoinMulticastGroup(multicastAddr); }
        public void JoinMulticastGroup(IPAddress multicastAddr, IPAddress localAddress) { vJoinMulticastGroup(multicastAddr, localAddress); }



        public SendDelegate vSend;
        [Script]
        public delegate int SendDelegate(byte[] datagram, int bytes, string hostname, int port);
        public int Send(byte[] datagram, int bytes, string hostname, int port) { return vSend(datagram, bytes, hostname, port); }



        public SendAsyncDelegate vSendAsync;
        [Script]
        public delegate Task<int> SendAsyncDelegate(byte[] datagram, int bytes, string hostname, int port);
        public Task<int> SendAsync(byte[] datagram, int bytes, string hostname, int port) { return vSendAsync(datagram, bytes, hostname, port); }



        // tested by?
        public SendAsyncDelegate2 vSendAsync2;
        [Script]
        public delegate Task<int> SendAsyncDelegate2(byte[] datagram, int bytes, IPEndPoint endPoint);
        public Task<int> SendAsync(byte[] datagram, int bytes, IPEndPoint endPoint) { return vSendAsync2(datagram, bytes, endPoint); }
    }
}
