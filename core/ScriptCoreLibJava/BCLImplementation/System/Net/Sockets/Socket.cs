using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using java.net;
using System.Net.Sockets;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Sockets
{
    // X:\jsc.svn\market\synergy\javascript\chrome\chrome\BCLImplementation\System\Net\Sockets\Socket.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\Sockets\Socket.cs

    // 
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Net.Sockets/Socket.cs
    // http://referencesource.microsoft.com/#System/net/System/Net/Sockets/Socket.cs

    // https://github.com/dotnet/corefx/blob/master/src/System.Net.WebSockets/src/System/Net/WebSockets/WebSocket.cs

    [Script(Implements = typeof(global::System.Net.Sockets.Socket))]
    public class __Socket : IDisposable
    {
        // Z:\jsc.svn\examples\java\hybrid\ubuntu\UbuntuTCPMultiplex\Program.cs

        // http://www.bbc.co.uk/rd/blog/2015/10/streaming-video-on-10-gigabit-ethernet-and-beyond
        // Our application programs have become more complicated as they are now responsible for dealing with the packet headers, a job normally handled by the operating system. But in return, we have measured ten-fold performance gains when sending and receiving very high bitrate video streams. Using this technique, we can send or receive uncompressed UHD 2160p50 video (more than 8 Gbps) using a single CPU core, leaving all the rest of the server's cores free for video processing.

        // http://ftp.arl.mil/mike/ping.html
        // "X:\opensource\android-ndk-r10c\platforms\android-21\arch-arm\usr\include\sys\socket.h"

        public global::java.net.ServerSocket InternalServerSocket;
        public global::java.net.Socket InternalSocket;




        public static implicit operator global::System.Net.Sockets.Socket(__Socket i) { return (global::System.Net.Sockets.Socket)(object)i; }
        public static implicit operator __Socket(global::System.Net.Sockets.Socket i) { return (__Socket)(object)i; }




        // X:\jsc.svn\examples\java\android\synergy\OVROculus360PhotosNDK\OVROculus360PhotosHUD\ApplicationActivity.cs
        [Script]
        public delegate void BindDelegate(EndPoint localEP);
        public BindDelegate vBind;
        public void Bind(EndPoint localEP)
        {



            Console.WriteLine("enter __Socket Bind " + new { vBind });
            vBind(localEP);
        }


        public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, bool optionValue)
        {

            if (optionName == SocketOptionName.ReuseAddress)
            {
                if (this.InternalServerSocket != null)
                {
                    try
                    {
                        //Console.WriteLine("setReuseAddress... " + new { optionValue });

                        this.InternalServerSocket.setReuseAddress(optionValue);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        public EndPoint RemoteEndPoint
        {
            get
            {
                // http://stackoverflow.com/questions/15130132/getlocalsocketaddress-and-getremotesocketaddress-not-returning-correct-value

                var RemoteSocketAddress = default(InetAddress);
                try
                {
                    //             Caused by: java.net.UnknownHostException: Unable to resolve host "/192.168.1.100:57999": No address associated with hostname
                    //at java.net.InetAddress.lookupHostByName(InetAddress.java:424)
                    //at java.net.InetAddress.getAllByNameImpl(InetAddress.java:236)
                    //at java.net.InetAddress.getByName(InetAddress.java:289)


                    var a = InternalSocket.getRemoteSocketAddress().ToString();

                    if (a.StartsWith("/"))
                        a = a.Substring(1);

                    {
                        var i = a.IndexOf(":");
                        if (i > 0)
                            a = a.Substring(0, i);
                    }
                    //               Caused by: java.net.UnknownHostException: 192.168.1.103/192.168.1.103
                    //      at java.net.InetAddress.lookupHostByName(InetAddress.java:506)
                    //      at java.net.InetAddress.getAllByNameImpl(InetAddress.java:294)
                    //      at java.net.InetAddress.getByName(InetAddress.java:325)
                    //      at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__Socket.get_RemoteEndPoint(__Socket.java:53)
                    //      ... 20 more
                    //}

                    {
                        var i = a.IndexOf("/");
                        if (i > 0)
                            a = a.Substring(0, i);
                    }

                    RemoteSocketAddress = InetAddress.getByName(a);

                }
                catch
                {
                    throw;

                }
                //                [javac] V:\src\ScriptCoreLibJava\BCLImplementation\System\Net\Sockets\__Socket.java:33: unreported exception java.net.UnknownHostException; must be caught or declared to be thrown
                //[javac]         address0 = InetAddress.getByName(this.InternalSocket.getRemoteSocketAddress().toString());
                //[javac]                                         ^


                var e = new __IPEndPoint(
                    (IPAddress)(object)new __IPAddress
                    {
                        InternalAddress = RemoteSocketAddress
                    },

                        InternalSocket.getPort()
                );

                return (IPEndPoint)(object)e;
            }
        }

        public EndPoint LocalEndPoint
        {
            get
            {
                var e = new __IPEndPoint(
                    (IPAddress)(object)new __IPAddress
                    {
                        InternalAddress = InternalSocket.getLocalAddress()
                    },

                        InternalSocket.getLocalPort()
                );

                return (IPEndPoint)(object)e;
            }
        }

        public void Close()
        {
            if (this.InternalServerSocket != null)
            {
                try
                {
                    this.InternalServerSocket.close();
                }
                catch
                {
                    throw new InvalidOperationException();
                }
                return;
            }

            throw new InvalidOperationException();
        }

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
