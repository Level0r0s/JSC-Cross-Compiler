using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Org.Mentalis.Network.ProxySocket;

/*
 *   Using the ProxySocket class is very easy. It works exactely like
 *   an ordinary Socket, but it offers more functionality.
 *   If you connect to a remote host, you can specify a host/port pair
 *   instead of an IPEndPoint and the ProxySocket will resolve it for
 *   you.
 *   It can also connect trough firewall proxy servers (hence the name).
 *
 *   To use a ProxySocket object with your SOCK4/5 firewall, simply
 *   adjust the Proxy properties (ProxyEndPoint, ProxyUser, ProxyPass
 *   and ProxyType).
 */

class TestApp
{
    static void Main(string[] args)
    {
        {
            // create a new ProxySocket
            ProxySocket s = new ProxySocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // set the proxy settings
            //s.ProxyEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9150);
            //s.ProxyUser = "username";
            //s.ProxyPass = "password";
            s.ProxyType = ProxyTypes.None;    // if you set this to ProxyTypes.None, 
                                                // the ProxySocket will act as a normal Socket

            // http://www.whatsmyip.org/

            //<!-- Please DO NOT use our site to power an IP bot, script or other automated IP-lookup software! - for humans only! -->
            //<h1>Your IP Address is <span id="ip">169.120.138.139</span></h1>

            // connect to the remote server
            // (note that the proxy server will resolve the domain name for us)
            s.Connect("torguard.net", 80);
            // send an HTTP request
            s.Send(Encoding.ASCII.GetBytes("GET /whats-my-ip.php HTTP/1.0\r\nHost: torguard.net\r\n\r\n"));
            // read the HTTP reply
            int recv = 0;
            byte[] buffer = new byte[8096];
            recv = s.Receive(buffer);
            while (recv > 0)
            {
                Console.Write(Encoding.ASCII.GetString(buffer, 0, recv));
                recv = s.Receive(buffer);
            }
        }

        {
            // create a new ProxySocket
            ProxySocket s = new ProxySocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // set the proxy settings
            s.ProxyEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9150);
            //s.ProxyUser = "username";
            //s.ProxyPass = "password";
            s.ProxyType = ProxyTypes.Socks5;    // if you set this to ProxyTypes.None, 
                                                // the ProxySocket will act as a normal Socket

            // http://www.whatsmyip.org/

            //<!-- Please DO NOT use our site to power an IP bot, script or other automated IP-lookup software! - for humans only! -->
            //<h1>Your IP Address is <span id="ip">169.120.138.139</span></h1>

            // connect to the remote server
            // (note that the proxy server will resolve the domain name for us)
            s.Connect("torguard.net", 80);
            // send an HTTP request
            s.Send(Encoding.ASCII.GetBytes("GET /whats-my-ip.php HTTP/1.0\r\nHost: torguard.net\r\n\r\n"));
            // read the HTTP reply
            int recv = 0;
            byte[] buffer = new byte[1024];
            recv = s.Receive(buffer);
            while (recv > 0)
            {
                Console.Write(Encoding.ASCII.GetString(buffer, 0, recv));
                recv = s.Receive(buffer);
            }
        }

        // wait until the user presses enter
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }
}
