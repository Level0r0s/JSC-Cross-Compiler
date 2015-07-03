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

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Sockets
{
    // http://referencesource.microsoft.com/#System/net/System/Net/Sockets/UdpClient.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Net.Sockets/UdpClient.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\Sockets\UdpClient.cs
    // X:\jsc.svn\market\synergy\javascript\chrome\chrome\BCLImplementation\System\Net\Sockets\UdpClient.cs

    [Script(Implements = typeof(global::System.Net.Sockets.UdpClient))]
    internal class __UdpClient
    {
        // http://www.acc.umu.se/~bosse/High%20performance%20kernel%20mode%20web%20server%20for%20Windows.pdf
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150630/udp

        // multicast tested?


        // what comes after tcp?
        // what about async API ?

        // X:\jsc.svn\examples\java\hybrid\JVMCLRUDPReceiveAsync\JVMCLRUDPReceiveAsync\Program.cs
        // X:\jsc.svn\examples\java\hybrid\JVMCLRUDPSendAsync\JVMCLRUDPSendAsync\Program.cs
        // X:\jsc.svn\examples\java\ConsoleMulticastExperiment\ConsoleMulticastExperiment\Program.cs
        // X:\jsc.svn\examples\java\android\AndroidServiceUDPNotification\AndroidServiceUDPNotification\ApplicationActivity.cs
        // X:\jsc.svn\examples\java\android\LANBroadcastListener\LANBroadcastListener\ApplicationActivity.cs

        // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\sys\socket.cs

        #region try_new_DatagramSocket
        static java.net.DatagramSocket try_new_DatagramSocket()
        {
            #region datagramSocket
            var datagramSocket = default(java.net.DatagramSocket);

            try
            {
                // http://developer.android.com/reference/java/net/DatagramSocket.html
                // Constructs a UDP datagram socket which is bound to any available port on the localhost.
                datagramSocket = new java.net.DatagramSocket();
            }
            catch
            {
                throw;
            }
            #endregion

            return datagramSocket;
        }

        static java.net.DatagramSocket try_new_DatagramSocket(int port)
        {
            #region datagramSocket
            var datagramSocket = default(java.net.DatagramSocket);

            try
            {
                // http://developer.android.com/reference/java/net/DatagramSocket.html
                // Constructs a UDP datagram socket which is bound to the specific port aPort on the localhost. Valid values for aPort are between 0 and 65535 inclusive.
                datagramSocket = new java.net.DatagramSocket(port);
            }
            catch
            {
                throw;
            }
            #endregion

            return datagramSocket;
        }

        static java.net.DatagramSocket try_new_DatagramSocket(IPEndPoint e)
        {
            // how do we listen on specific NIC?
            return try_new_DatagramSocket(e.Port);
        }

        #endregion

        [Script]
        public class xConstructorArguments
        {
            Func<java.net.DatagramSocket> vDatagramSocket;
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
                return new xConstructorArguments { };
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

            java.net.DatagramSocket datagramSocket;

            // http://stackoverflow.com/questions/8558791/multicastsocket-vs-datagramsocket-in-broadcasting-to-multiple-clients
            // you must use MulticastSocket for receiving the multicasts; for sending them, again, you can use either DatagramSocket or MulticastSocket, and there is no difference in efficiency.

            //var buffer = new sbyte[0x10000];
            var buffer = new sbyte[0x1000];

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
                                datagram.Length, a, port
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
                                datagram.Length, (__IPAddress)endPoint.Address, endPoint.Port
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
