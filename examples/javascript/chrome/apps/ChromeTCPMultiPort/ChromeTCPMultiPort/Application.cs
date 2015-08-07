using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ChromeTCPMultiPort;
using ChromeTCPMultiPort.Design;
using ChromeTCPMultiPort.HTML.Pages;
using chrome;
using ScriptCoreLib.JavaScript.WebGL;
using System.Net.Sockets;
using xchrome.BCLImplementation.System.Net.Sockets;
using System.Net;

namespace ChromeTCPMultiPort
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150513

        // This extension requires Maelstrom version 38 or greater. 

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // Permission 'app.window.alpha' is unknown or URL pattern is malformed.

            #region += Launched chrome.app.window
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
                {
                    Console.WriteLine("chrome.app.window.create, is that you?");

                    // pass thru
                }
                else
                {
                    // should jsc send a copresence udp message?
                    chrome.runtime.UpdateAvailable += delegate
                    {
                        new chrome.Notification(title: "UpdateAvailable");

                    };

                    chrome.app.runtime.Launched += async delegate
                    {
                        // 0:12094ms chrome.app.window.create {{ href = chrome-extension://aemlnmcokphbneegoefdckonejmknohh/_generated_background_page.html }}
                        Console.WriteLine("chrome.app.window.create " + new { Native.document.location.href });

                        new chrome.Notification(title: "ChromeNetworkInterfaces");

                        var xappwindow = await chrome.app.window.create(
                               Native.document.location.pathname, options: null
                        );

                        //xappwindow.setAlwaysOnTop

                        xappwindow.show();

                        await xappwindow.contentWindow.async.onload;

                        Console.WriteLine("chrome.app.window loaded!");
                    };


                    return;
                }
            }
            #endregion

            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeNetworkInterfaces\ChromeNetworkInterfaces\Application.cs

            //{{ Length = 4 }}
            //{{ prefixLength = 64, name = {D7020941-742E-4570-93B2-C0372D3D870F}, address = fe80::88c0:f0a:9ccf:cba0 }}
            //{{ prefixLength = 24, name = {D7020941-742E-4570-93B2-C0372D3D870F}, address = 192.168.43.28 }}
            //{{ prefixLength = 64, name = {A8657A4E-8BFA-41CC-87BE-6847E33E1A81}, address = 2001:0:9d38:6abd:20a6:2815:3f57:d4e3 }}
            //{{ prefixLength = 64, name = {A8657A4E-8BFA-41CC-87BE-6847E33E1A81}, address = fe80::20a6:2815:3f57:d4e3 }}

            new { }.With(
                async delegate
                {
                    // http://css-infos.net/property/-webkit-user-select
                    // http://caniuse.com/#feat=user-select-none
                    //(Native.body.style as dynamic).userSelect = "auto";
                    (Native.document.body.style as dynamic).webkitUserSelect = "auto";
                    Native.document.documentElement.style.overflow = IStyle.OverflowEnum.auto;

                    Native.css[IHTMLElement.HTMLElementEnum.a].style.display = IStyle.DisplayEnum.block;

                    new IHTMLPre
                    {
                        // {{ sockets = null }}
                        //new { (Native.window as dynamic).chrome.sockets }
                        // {{ socket = [object Object] }}
                        new { (Native.window as dynamic).chrome.socket }
                        //new { (Native.window as dynamic).sockets.tcpServer  }
                    }.AttachToDocument();

                    // https://css-tricks.com/almanac/properties/u/user-select/
                    //Native.body.style.setProperty(
                    // X:\jsc.svn\examples\java\hybrid\JVMCLRNIC\JVMCLRNIC\Program.cs
                    // clr does not have it async. 


                    // ?
                    var refresh = new IHTMLButton { "refresh" }.AttachToDocument();
                    do
                    {
                        new IHTMLHorizontalRule { }.AttachToDocument();


                        var n = await chrome.socket.getNetworkList();
                        var n24 = n.Where(x => x.prefixLength == 24).ToArray();

                        new IHTMLPre { new { n24.Length } }.AttachToDocument();

                        // http://www.w3schools.com/tags/att_input_type.asp


                        // chrome wont like ports like 201507
                        // android wont do ports less than 1024
                        // cloudflare has its own range of ports it likes.
                        // https://support.cloudflare.com/hc/en-us/articles/200169156-Which-ports-will-CloudFlare-work-with-

                        // https://en.wikipedia.org/wiki/List_of_TCP_and_UDP_port_numbers
                        // 65535  unsafe
                        // http://127.0.0.1:65534/

                        //var uiport = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.number, valueAsNumber = 2052 }.AttachToDocument();

                        #region uiport
                        var uiport = ((IHTMLInput)2052).AttachToDocument();



                        foreach (var item in n24)
                        {
                            //new IHTMLButton { "chrome API " + new { item.prefixLength, item.name, item.address } }.AttachToDocument().onclick += Application_onclick;

                            #region  CLR API
                            new IHTMLButton { "CLR API " + new { item.prefixLength, item.name, item.address } }.AttachToDocument().onclick += async delegate
                            {
                                new IHTMLPre { "create... " + typeof(TcpListener) }.AttachToDocument();
                                //new IHTMLPre { "create... " + typeof(TcpClient) }.AttachToDocument();
                                //new IHTMLPre { "create... " + typeof(NetworkStream) }.AttachToDocument();

                                // use ports as a date?
                                //var port = 201507;

                                int port = uiport;


                                var l = new TcpListener(IPAddress.Any, port);
                                l.Start();

                                new IHTMLAnchor
                                {

                                    target = "_blank",
                                    href = "http://127.0.0.1:" + port,
                                    //innerText = "accept.. open tab at " + "http://127.0.0.1:" + port
                                    innerText = "http://127.0.0.1:" + port
                                }.AttachToDocument();

                                while (true)
                                {
                                    // a test in jvm/android?
                                    // X:\jsc.svn\examples\java\async\test\JVMCLRTCPServerAsync\JVMCLRTCPServerAsync\Program.cs

                                    // accept {{ c = {{ resultCode = -2, socketId = null }} }}

                                    var c0 = await l.AcceptTcpClientAsync();


                                    if (c0 == null)
                                    {
                                        new IHTMLPre { "error?" }.AttachToDocument();

                                        break;
                                    }

                                    //yield(c);

                                    var c = c0;

                                    new IHTMLPre { "accept " + new { c } }.AttachToDocument();

                                    new { }.With(
                                        async delegate
                                        {

                                            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150719

                                            var s = c.GetStream();

                                            // could we switch into a worker thread?
                                            // jsc would need to split the stream object tho

                                            var buffer = new byte[1024];
                                            // why no implict buffer?
                                            var count = await s.ReadAsync(buffer, 0, buffer.Length);

                                            var input = Encoding.UTF8.GetString(buffer, 0, count);

                                            new IHTMLPre { new { input } }.AttachToDocument();


                                            var outputString = $"HTTP/1.0 200 OK \r\nConnection: close\r\n\r\nhello friend {port}.\r\n";
                                            var obuffer = Encoding.UTF8.GetBytes(outputString);

                                            // hop to client.
                                            // we could jump to the client side here..
                                            await s.WriteAsync(obuffer, 0, obuffer.Length);

                                            c.Close();
                                        }
                                    );
                                }


                            };
                            #endregion


                            #region  CLR API
                            new IHTMLButton { "cloudflare ports " + new { item.prefixLength, item.name, item.address } }.AttachToDocument().onclick += delegate
                           {
                               new IHTMLPre { "create... " + typeof(TcpListener) }.AttachToDocument();
                               //new IHTMLPre { "create... " + typeof(TcpClient) }.AttachToDocument();
                               //new IHTMLPre { "create... " + typeof(NetworkStream) }.AttachToDocument();

                               // use ports as a date?
                               //var port = 201507;

                               // https://support.cloudflare.com/hc/en-us/articles/200169156-Which-ports-will-CloudFlare-work-with-

                               var cfhttpports = new[] {
                                    8080,
                                    8880,
                                    2052,
                                    2082,
                                    2086,
                                    2095
                               };


                               foreach (var port0 in cfhttpports)
                               {
                                   var port = port0;

                                   new { }.With(
                                       async delegate
                                       {
                                           var l = new TcpListener(IPAddress.Any, port);
                                           l.Start();

                                           new IHTMLAnchor
                                           {

                                               target = "_blank",
                                               href = "http://127.0.0.1:" + port,
                                               //innerText = "accept.. open tab at " + "http://127.0.0.1:" + port
                                               innerText = "http://127.0.0.1:" + port
                                           }.AttachToDocument();

                                           while (true)
                                           {
                                               #region AcceptTcpClientAsync
                                               var c0 = await l.AcceptTcpClientAsync();
                                               if (c0 == null)
                                               {
                                                   new IHTMLPre { "error?" }.AttachToDocument();
                                                   break;
                                               }
                                               var c = c0;
                                               new IHTMLPre { "accept " + new { c } }.AttachToDocument();
                                               new { }.With(
                                                   async delegate
                                                   {

                                                       // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150719

                                                       var s = c.GetStream();

                                                       // could we switch into a worker thread?
                                                       // jsc would need to split the stream object tho

                                                       var buffer = new byte[1024];
                                                       // why no implict buffer?
                                                       var count = await s.ReadAsync(buffer, 0, buffer.Length);

                                                       var input = Encoding.UTF8.GetString(buffer, 0, count);

                                                       new IHTMLPre { new { input } }.AttachToDocument();


                                                       var outputString = $"HTTP/1.0 200 OK \r\nConnection: close\r\n\r\nhello friend {port}.\r\n";
                                                       var obuffer = Encoding.UTF8.GetBytes(outputString);

                                                       // hop to client.
                                                       // we could jump to the client side here..
                                                       await s.WriteAsync(obuffer, 0, obuffer.Length);

                                                       c.Close();
                                                   }
                                               );
                                               #endregion
                                           }
                                       }
                                   );


                               }


                           };
                            #endregion



                        }
                        #endregion
                    }
                    while (await refresh.async.onclick);

                }
            );
        }






    }
}
