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
using ChromeUDPClipboard;
using ChromeUDPClipboard.Design;
using ChromeUDPClipboard.HTML.Pages;
using System.Net.Sockets;
using System.Net;

namespace ChromeUDPClipboard
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151212/androidudpclipboard
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160101/ovrwindwheelndk

        // from red to asus
        // net use
        // subst a: s:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPClipboard\bin\Debug\staging\ChromeUDPClipboard.Application\web

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
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
                    //chrome.runtime.UpdateAvailable += delegate
                    //{
                    //    new chrome.Notification(title: "UpdateAvailable");

                    //};

                    // nuget chrome
                    chrome.app.runtime.Launched += async delegate
                    {
                        // 0:12094ms chrome.app.window.create {{ href = chrome-extension://aemlnmcokphbneegoefdckonejmknohh/_generated_background_page.html }}
                        Console.WriteLine("chrome.app.window.create " + new { Native.document.location.href });

                        new chrome.Notification(title: "ChromeUDPClipboard");

                        // https://developer.chrome.com/apps/app_window#type-CreateWindowOptions
                        var xappwindow = await chrome.app.window.create(
                               Native.document.location.pathname, options: new
                               {
                                   alwaysOnTop = true,
                                   visibleOnAllWorkspaces = true
                               }
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

            Native.document.body.style.backgroundColor = "darkcyan";
            (Native.body.style as dynamic).webkitUserSelect = "text";



            new IHTMLButton { "update pending... update available. click to reload.." }.AttachToDocument().onclick += delegate
            {
                // can we get an udp signal from the compiler when the app is out of date, when the update is pending?
                chrome.runtime.reload();
            };





            new IHTMLHorizontalRule { }.AttachToDocument();

            var i = new IHTMLTextArea { }.AttachToDocument();

            new IHTMLHorizontalRule { }.AttachToDocument();


            #region UDPClipboardSend
            Action<string> UDPClipboardSend = async message =>
            {
                var n = await chrome.socket.getNetworkList();

                new IHTMLPre { new { n.Length } }.AttachToDocument();

                // LINQ and async wont mix for 2012?

                //foreach (var item in n.Where(x => x.prefixLength == 24))
                foreach (var item in n) if (item.prefixLength == 24)
                    {
                        new IHTMLPre { new { item.prefixLength, item.name, item.address } }.AttachToDocument();

                        //{ prefixLength = 64, name = {AE3B881D-488F-4C3A-93F8-7DA0D65B9300}, address = fe80::fc45:cae9:46ca:7b0f }
                        //about to bind... { port = 29129 }
                        //about to send... { Length = 0 }
                        //sent: -2
                        //{ prefixLength = 24, name = {AE3B881D-488F-4C3A-93F8-7DA0D65B9300}, address = 192.168.1.12 }
                        //about to bind... { port = 25162 }
                        //about to send... { Length = 0 }
                        //sent: 0


                        // X:\jsc.svn\examples\merge\TestDetectOpenFiles\TestDetectOpenFiles\Program.cs
                        // X:\jsc.svn\examples\javascript\chrome\apps\MulticastListenExperiment\MulticastListenExperiment\Application.cs

                        // https://code.google.com/p/chromium/issues/detail?id=455352

                        // X:\jsc.svn\examples\merge\TestDetectOpenFiles\TestDetectOpenFiles\Program.cs

                        // bind?

                        var data = Encoding.UTF8.GetBytes(message);	   //creates a variable b of type byte

                        // http://stackoverflow.com/questions/13691119/chrome-packaged-app-udp-sockets-not-working
                        // http://www.chinabtp.com/how-to-do-udp-broadcast-using-chrome-sockets-udp-api/

                        // chrome likes 0 too.
                        var port = new Random().Next(16000, 40000);
                        //var port = 0;
                        // 
                        //new IHTMLPre { "about to bind... " + new { port } }.AttachToDocument();

                        // where is bind async?
                        var socket = new UdpClient();
                        socket.Client.Bind(

                            //new IPEndPoint(IPAddress.Any, port: 40000)
                            new IPEndPoint(IPAddress.Parse(item.address), port)
                        );


                        //new IHTMLPre { "about to send... " + new { data.Length } }.AttachToDocument();

                        // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                        var s = await socket.SendAsync(
                            data,
                            data.Length,
                            hostname: "239.1.2.3",
                            port: 49814
                        );

                        //new IHTMLPre { "sent: " + s }.AttachToDocument();


                        //socket.ReceiveAsync
                        //socket.Close();

                        //new IHTMLPre { $"sent: {s}" }.AttachToDocument();

                        // android cannot see it. why? because it needs to know which NIC to use.

                    }
            };
            #endregion




            new { }.With(
                async delegate
                {
                    do
                    {
                        var v = await Native.document.documentElement.async.ondroptext;

                        i.value = v;

                        UDPClipboardSend(v);
                    }
                    while (true);
                }
            );

            new IHTMLButton { () => "send to '" + i.value + "' android" }.AttachToDocument().onclick += async delegate
            {
                UDPClipboardSend(i.value);

            };
        }

    }
}
