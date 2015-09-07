using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Net.Sockets;
using System.Net;
using ScriptCoreLib.Extensions;
using System.Text;

namespace HybridHopToUDPChromeApp
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // chrome app is build via post build event



            Console.WriteLine("CLR jump test");

            Action<IPAddress> f = async Address =>
                 {
                     Console.WriteLine(new { Address });

                     var nicport = new Random().Next(16000, 40000);


                     var data = Encoding.UTF8.GetBytes("hello");

                     //An unhandled exception of type 'System.Net.Sockets.SocketException' occurred in mscorlib.dll

                     //Additional information: Only one usage of each socket address (protocol/network address/port) is normally permitted
                     var socket = new UdpClient();

                     socket.Client.Bind(

                         //new IPEndPoint(IPAddress.Any, port: 40014)
                         new IPEndPoint(Address, port: nicport)
                     );


                     //new IHTMLPre { "about to send... " + new { data.Length } }.AttachToDocument();

                     // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                     var s = await socket.SendAsync(
                          data,
                          data.Length,
                          hostname: "239.1.2.3",
                          port: 40014
                      );

                     socket.Close();


                 };



            System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().WithEach(
                      n =>
                      {
                          // Z:\jsc.svn\examples\javascript\chrome\hybrid\HybridHopToUDPChromeApp\Application.cs
                          // X:\jsc.svn\examples\java\android\forms\FormsUDPJoinGroup\FormsUDPJoinGroup\ApplicationControl.cs
                          // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\NetworkInformation\NetworkInterface.cs

                          var IPProperties = n.GetIPProperties();
                          var PhysicalAddress = n.GetPhysicalAddress();



                          foreach (var ip in IPProperties.UnicastAddresses)
                          {
                              // ipv4
                              if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                              {
                                  if (!IPAddress.IsLoopback(ip.Address))
                                      if (n.SupportsMulticast)
                                          f(ip.Address);
                              }
                          }




                      }
                  );

            var w = UDPServer.Invoke();
            w.Wait();

            // RewriteToUltraApplication.AsProgram.Launch(typeof(Application));


            Console.ReadKey();
        }

    }
}

//await socket.ReceiveAsync
//CLR jump test
//ReceiveAsync: 5
//await socket.ReceiveAsync
