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
using ChromeAppWindowUDPPointerLock;
// ?
using ChromeAppWindowUDPPointerLock.Design;
using ChromeAppWindowUDPPointerLock.HTML.Pages;
using System.Net.Sockets;
using System.Net;

namespace ChromeAppWindowUDPPointerLock
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160101/ovrwindwheelndk

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151001/udppenpressure


        // net use
        // OK           R:        \\192.168.1.12\x$         Microsoft Windows Network


        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151003/ovrwindwheelactivity
        // subst b: s:\jsc.svn\examples\javascript\chrome\apps\ChromeAppWindowUDPPointerLock\ChromeAppWindowUDPPointerLock\bin\Debug\staging\ChromeAppWindowUDPPointerLock.Application\web

        // X:\jsc.svn\examples\javascript\chrome\apps\ChromeAppWindowUDPPointerLock\ChromeAppWindowUDPPointerLock\bin\Debug\staging\ChromeAppWindowUDPPointerLock.Application\web

        // logoff logon?
        // net start LanmanServer

        //        ---------------------------
        //Restoring Network Connections
        //---------------------------
        //An error occurred while reconnecting R: to
        //\\192.168.1.12\x$
        //Microsoft Windows Network: The specified network name is no longer available.


        //This connection has not been restored.
        //---------------------------
        //OK   
        //---------------------------



        //        ---------------------------
        //Restoring Network Connections
        //---------------------------
        //An error occurred while reconnecting R: to
        //\\192.168.1.12\x$
        //Microsoft Windows Network: Multiple connections to a server or shared resource by the same user, using more than one user name, are not allowed. Disconnect all previous connections to the server or shared resource and try again.


        //This connection has not been restored.
        //---------------------------
        //OK   



        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // how are we to make this into a chrome app?
            // "X:\jsc.svn\examples\javascript\chrome\apps\ChomeAlphaAppWindow\ChomeAlphaAppWindow.sln"

            // since now jsc shows ssl support
            // how about packaging the view-source for chrome too?

            // nuget, add chrome.

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

                        new chrome.Notification(title: "Launched2");

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


            // can we also test the shadow DOM ?
            // how does it work again?


            // now we have to update our alpha window/server window
            // to be in the correct context.

            // what about property window
            // back in the vb days we made one.
            // time to do one?



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



            new IHTMLButton { "ready1" }.AttachToDocument().onclick +=
                //async
                delegate
                {
                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150701

                    new MyShadow { }.AttachTo(Native.shadow);

                    #region CaptureMouse
                    {
                        // shadow will select div from chldren
                        var div = new IHTMLDiv { }.AttachTo(Native.document.documentElement);

                        new IHTMLPre { "drag me via CaptureMouse" }.AttachTo(div);
                        var xy = new IHTMLPre { "{}" }.AttachTo(div);

                        div.css.style.backgroundColor = "transparent";
                        div.css.style.transition = "background 500ms linear";

                        div.css.active.style.backgroundColor = "yellow";

                        Native.document.documentElement.style.cursor = IStyle.CursorEnum.move;

                        div.css.hover.style.backgroundColor = "cyan";

                        div.onmousemove +=
                            e =>
                            {
                                // we could tilt the svg cursor
                                // like we do on heat zeeker:D


                                //Native.document.title = new { e.CursorX, e.CursorY }.ToString();
                                xy.innerText = new { e.CursorX, e.CursorY }.ToString();

                            };

                        div.onmousedown +=
                            async e =>
                            {
                                e.CaptureMouse();

                                await div.async.onmouseup;
                            };
                    }
                    #endregion

                    {
                        // shadow will select div from chldren
                        var div = new IHTMLDiv { }.AttachTo(Native.document.documentElement);

                        new IHTMLPre { "click to  requestPointerLock, double click to stop" }.AttachTo(div);
                        var wasd = new IHTMLPre { "{}" }.AttachTo(div);
                        var xy = new IHTMLPre { "{}" }.AttachTo(div);

                        div.css.style.backgroundColor = "transparent";
                        div.css.style.transition = "background 500ms linear";

                        div.css.active.style.backgroundColor = "yellow";

                        Native.document.documentElement.style.cursor = IStyle.CursorEnum.move;

                        div.css.hover.style.backgroundColor = "cyan";





                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704

                        var keys_ad = 0;
                        var keys_ws = 0;
                        var keys_c = 0;

                        var mousebutton = 0;
                        var mousewheel = 0;

                        var x = 0;
                        var y = 0;


                        // what about 255.255.255.255 ?
                        chrome.socket.getNetworkList().ContinueWithResult(
                             async n =>
                             {
                                 // which networks should we notify of our data?

                                 //new IHTMLPre { new { n.Length } }.AttachToDocument();

                                 foreach (var item in n)
                                 {
                                     // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704
                                     // skip ipv6
                                     if (item.address.Contains(":"))
                                         continue;

                                     //if (item.prefixLength != 4)
                                     //    continue;


                                     #region other
                                     new IHTMLButton { "send onmouseup " + item.address }.AttachTo(div).With(
                                         async refresh =>
                                         {
                                             refresh.style.display = IStyle.DisplayEnum.block;

                                             // experimental until ref count 33?
                                             await refresh.async.onmousedown;

                                             refresh.disabled = true;

                                             while (await div.async.onmouseup)
                                             {


                                                 #region xml

                                                 var nmessage = x + ":" + y;
                                                 var Host = "";
                                                 var PublicPort = "";

                                                 var message =
                                                     new XElement("string",
                                                         new XAttribute("c", "" + 1),
                                                         new XAttribute("n", nmessage),
                                                         "Visit me at " + Host + ":" + PublicPort
                                                     ).ToString();

                                                 #endregion

                                                 var data = Encoding.UTF8.GetBytes(message);	   //creates a variable b of type byte

                                                 var port = new Random().Next(16000, 40000);

                                                 //new IHTMLPre { "about to bind... " + new { port } }.AttachToDocument();
                                                 // Z:\jsc.svn\examples\javascript\chrome\hybrid\HybridHopToUDPChromeApp\Application.cs
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
                                                     port: 40804
                                                 );

                                                 socket.Close();

                                             }
                                         }
                                     );

                                     new IHTMLButton { "send onmousemove " + item.address }.AttachTo(div).With(
                                       async refresh =>
                                       {
                                           refresh.style.display = IStyle.DisplayEnum.block;

                                           // experimental until ref count 33?
                                           await refresh.async.onmousedown;

                                           refresh.disabled = true;

                                           var port = new Random().Next(16000, 40000);

                                           //new IHTMLPre { "about to bind... " + new { port } }.AttachToDocument();

                                           // where is bind async?
                                           var socket = new UdpClient();
                                           socket.Client.Bind(

                                               //new IPEndPoint(IPAddress.Any, port: 40000)
                                               new IPEndPoint(IPAddress.Parse(item.address), port)
                                           );


                                           while (await div.async.onmousemove)
                                           {
                                               var nmessage = x + ":" + y;


                                               var data = Encoding.UTF8.GetBytes(nmessage);	   //creates a variable b of type byte



                                               //new IHTMLPre { "about to send... " + new { data.Length } }.AttachToDocument();

                                               // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                                               var s = await socket.SendAsync(
                                                   data,
                                                   data.Length,
                                                   hostname: "239.1.2.3",
                                                   port: 41814
                                               );

                                               //socket.Close();

                                           }
                                       }
                                   );
                                     #endregion


                                     new IHTMLButton { "send onframe " + item.address }.AttachTo(div).With(
                                          async refresh =>
                                          {
                                              refresh.style.color = "blue";

                                              refresh.style.display = IStyle.DisplayEnum.block;

                                              // experimental until ref count 33?
                                              await refresh.async.onmousedown;

                                              UDPClipboardSend("mousedown...");


                                              refresh.disabled = true;

                                              var port = new Random().Next(16000, 40000);

                                              //new IHTMLPre { "about to bind... " + new { port } }.AttachToDocument();

                                              // where is bind async?
                                              var socket = new UdpClient();
                                              socket.Client.Bind(

                                                  //new IPEndPoint(IPAddress.Any, port: 40000)
                                                  new IPEndPoint(IPAddress.Parse(item.address), port)
                                              );


                                              // this will eat too much memory?
                                              //div.ownerDocument.defaultView.onframe +=
                                              div.onframe +=
                                                  delegate
                                                  {
                                                      // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704
                                                      var nmessage = x + ":" + y + ":" + keys_ad + ":" + keys_ws + ":" + keys_c + ":" + mousebutton + ":" + mousewheel;

                                                      UDPClipboardSend(nmessage);

                                                      var data = Encoding.UTF8.GetBytes(nmessage);	   //creates a variable b of type byte



                                                      //new IHTMLPre { "about to send... " + new { data.Length } }.AttachToDocument();


                                                      // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                                                      socket.Send(
                                                          data,
                                                          data.Length,
                                                          hostname: "239.1.2.3",
                                                          port: 41814
                                                      );

                                                      // android doesnt get it?
                                                      // restart router?
                                                      // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160101/ovrwindwheelndkg
                                                      // why wont it make it?
                                                  };

                                              return;

                                              //while (await Native.window.async.onframe)
                                              //while (await div.async.onframe)
                                              while (await div.ownerDocument.defaultView.async.onframe)
                                              {
                                                  var nmessage = x + ":" + y + ":" + keys_ad + ":" + keys_ws + ":" + keys_c + ":" + mousebutton + ":" + mousewheel;


                                                  var data = Encoding.UTF8.GetBytes(nmessage);	   //creates a variable b of type byte



                                                  //new IHTMLPre { "about to send... " + new { data.Length } }.AttachToDocument();

                                                  // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                                                  var s = await socket.SendAsync(
                                                      data,
                                                      data.Length,
                                                      hostname: "239.1.2.3",
                                                      port: 41814
                                                  );

                                                  //socket.Close();

                                              }
                                          }
                                      );
                                 }
                             }
                        );


                        div.tabIndex = 1;

                        div.onkeydown +=
                             async e =>
                             {
                                 var A = e.KeyCode == 65;
                                 var D = e.KeyCode == 68;

                                 if (A || D)
                                 {
                                     keys_ad = e.KeyCode;

                                     //Native.document.title = new { e.CursorX, e.CursorY }.ToString();
                                     wasd.innerText = new { e.KeyCode, ad = keys_ad, ws = keys_ws }.ToString();

                                     while ((await div.async.onkeyup).KeyCode != e.KeyCode) ;

                                     //var ee = await div.async.onkeyup;

                                     keys_ad = 0;

                                     wasd.innerText = new { e.KeyCode, ad = keys_ad, ws = keys_ws }.ToString();

                                     return;
                                 }

                                 // CS
                                 // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704
                                 if (e.KeyCode == 67)
                                 {
                                     keys_c = e.KeyCode;

                                     //Native.document.title = new { e.CursorX, e.CursorY }.ToString();
                                     wasd.innerText = new { e.KeyCode, keys_ad, keys_ws, keys_c }.ToString();

                                     while ((await div.async.onkeyup).KeyCode != e.KeyCode) ;

                                     //var ee = await div.async.onkeyup;

                                     keys_c = 0;

                                     wasd.innerText = new { e.KeyCode, keys_ad, keys_ws, keys_c }.ToString();

                                     return;
                                 }

                                 {
                                     keys_ws = e.KeyCode;

                                     //Native.document.title = new { e.CursorX, e.CursorY }.ToString();
                                     wasd.innerText = new { e.KeyCode, ad = keys_ad, ws = keys_ws }.ToString();

                                     while ((await div.async.onkeyup).KeyCode != e.KeyCode) ;
                                     //var ee = await div.async.onkeyup;

                                     keys_ws = 0;

                                     wasd.innerText = new { e.KeyCode, ad = keys_ad, ws = keys_ws }.ToString();
                                 }
                             };


                        div.onmousewheel += e =>
                            {
                                // since we are a chrome app. is chrome sending us wheel delta too?
                                mousewheel += e.WheelDirection;
                            };

                        div.onmousemove +=
                            e =>
                            {
                                // we could tilt the svg cursor
                                // like we do on heat zeeker:D

                                x += e.movementX;
                                y += e.movementY;


                                //Native.document.title = new { e.CursorX, e.CursorY }.ToString();
                                xy.innerText = new { x, y }.ToString();

                            };

                        div.onmousedown +=
                            async e =>
                            {
                                // wont work for RemoteApp users tho

                                mousebutton = (int)e.MouseButton;

                                // await ?
                                div.requestPointerLock();
                                //e.CaptureMouse();

                                var ee = await div.async.onmouseup;
                                //var ee = await div.async.ondblclick;

                                if (ee.MouseButton == IEvent.MouseButtonEnum.Right)
                                    Native.document.exitPointerLock();

                                mousebutton = 0;

                            };
                    }


                };

        }

    }
}
