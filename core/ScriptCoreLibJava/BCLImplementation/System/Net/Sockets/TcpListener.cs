using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using System.Net.Sockets;
using java.lang;
using System.Threading.Tasks;
using ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Sockets
{
    // http://referencesource.microsoft.com/#System/net/System/Net/Sockets/TCPListener.cs
    // https://github.com/mono/mono/tree/master/mcs/class/System/System.Net.Sockets/TcpListener.cs
    // X:\jsc.svn\market\synergy\javascript\chrome\chrome\BCLImplementation\System\Net\Sockets\TcpListener.cs
    // x:\jsc.svn\core\scriptcorelibjava\bclimplementation\system\net\sockets\tcplistener.cs

    [Script(Implements = typeof(global::System.Net.Sockets.TcpListener))]
    internal class __TcpListener
    {
        // X:\jsc.svn\examples\java\async\test\JVMCLRTCPServerAsync\JVMCLRTCPServerAsync\Program.cs

        // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAsync\ChromeTCPServerAsync\Application.cs
        // X:\jsc.svn\examples\java\HybridCLRJVMAPKWebServer\ClassLibrary1\Class1.cs
        // X:\jsc.svn\examples\java\SimpleWebServerExample\SimpleWebServerExample\Program.cs
        // X:\jsc.svn\examples\java\System_Net_Sockets_TcpClient\System_Net_Sockets_TcpClient\Program.cs
        // X:\jsc.svn\examples\java\PortCloner\PortCloner\Program.cs

        // what about AIR for iOS ?
        // X:\jsc.svn\examples\actionscript\air\AIRServerSocketExperiment\AIRServerSocketExperiment\ApplicationSprite.cs

        // tested by ?
        // when can we do Android, CLR and Chrome webservers via SSL ?


        public global::java.net.ServerSocket InternalSocket;
        public __IPAddress localaddr;
        public int port;

        public __TcpListener(IPAddress localaddr, int port)
        {
            this.localaddr = (__IPAddress)(object)localaddr;
            this.port = port;



            try
            {
                // http://stackoverflow.com/questions/6090891/what-is-socket-bind-and-how-to-bind-an-address
                this.InternalSocket = new global::java.net.ServerSocket();
            }
            catch
            {
                throw;
            }

            this.Server = (Socket)(object)new __Socket { InternalServerSocket = this.InternalSocket };

        }



        public Socket Server
        {
            get;
            set;
        }

        #region Start
        public void Start()
        {
            this.Start(0x7fffffff);
        }

        public void Start(int backlog)
        {
            //Console.WriteLine("enter TcpListener.Start");

            try
            {
                //this.InternalSocket = new global::java.net.ServerSocket(this.port, backlog, this.localaddr.InternalAddress);

                //         Caused by: java.net.SocketException: Socket is not bound
                //at java.net.ServerSocket.accept(ServerSocket.java:122)

                // http://stackoverflow.com/questions/10516030/java-server-socket-doesnt-reuse-address
                this.InternalSocket.bind(
                    new java.net.InetSocketAddress(
                        this.localaddr.InternalAddress,
                        port
                    ),

                    backlog
                );

            }
            catch
            {
                throw;
            }

        }
        #endregion

        public void Stop()
        {
            try
            {
                this.InternalSocket.close();
                this.InternalSocket = null;
            }
            catch
            {
                throw;
            }
        }





        [Obsolete]
        public Socket AcceptSocket()
        {
            if (InternalSocket == null)
                throw new InvalidOperationException(
                    //"Not listening. You must call the Start() method before calling this method."
                );

            var r = default(__Socket);

            try
            {
                r = new __Socket { InternalSocket = this.InternalSocket.accept() };
            }
            catch
            {
                throw;
            }

            return (Socket)(object)r;
        }

        [Obsolete]
        public TcpClient AcceptTcpClient()
        {
            //I/System.Console(28234): 6e4a:6af2 before AcceptTcpClient
            //I/System.Console(28234): 6e4a:6af2 enter AcceptTcpClient
            //I/System.Console(28234): 6e4a:6af1 enter catch { mname = <00c3> ldloca.s.try } ClauseCatchLocal:
            //I/System.Console(28234): 6e4a:6af1
            //I/System.Console(28234): __AsyncVoidMethodBuilder.SetException { exception =  }

            Console.WriteLine("enter AcceptTcpClient");
            // tested by?

            var r = default(__TcpClient);

            try
            {
                var s = this.AcceptSocket();
                r = new __TcpClient(s);
            }
            catch (global::System.Exception err)
            {
                Console.WriteLine("err AcceptTcpClient " + new { err });
                //throw err;
                throw new InvalidOperationException();
            }

            Console.WriteLine("exit AcceptTcpClient");
            return (TcpClient)(object)r;
        }


        // NET45
        public Task<Socket> AcceptSocketAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TcpClient> AcceptTcpClientAsync()
        {
            Console.WriteLine("enter AcceptTcpClientAsync");
            // X:\jsc.svn\examples\java\android\AndroidMultiProcTCPServerAsync\AndroidMultiProcTCPServerAsync\ApplicationActivity.cs

            // X:\jsc.svn\examples\java\async\Test\JVMCLRTCPServerAsync\JVMCLRTCPServerAsync\Program.cs

            // return Task<TcpClient>.Factory.FromAsync(BeginAcceptTcpClient, EndAcceptTcpClient, null);

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/2010303
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150304

            // webrtc?
            // http://referencesource.microsoft.com/#System/net/System/Net/Sockets/UDPClient.cs

            var c = new TaskCompletionSource<TcpClient>();

            __Task.Run(
                delegate
                {
                    Console.WriteLine("before AcceptTcpClient");
                    // we are operating in another thread by now...
                    var x = this.AcceptTcpClient();

                    Console.WriteLine("after AcceptTcpClient");
                    c.SetResult(x);
                }
            );

            Console.WriteLine("exit AcceptTcpClientAsync");
            return c.Task;
        }
    }
}
