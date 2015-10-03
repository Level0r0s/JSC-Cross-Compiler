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
using ChromeUDPLineTo;

// ?
using ChromeUDPLineTo.Design;
using ChromeUDPLineTo.HTML.Pages;
using System.Net.Sockets;
using System.Net;
using ScriptCoreLib.JavaScript.WebGL;

namespace ChromeUDPLineTo
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // x:\jsc.svn\examples\javascript\chrome\apps\chromeudpsendasync\chromeudpsendasync\application.cs
            // reload on idle?
            // edit and continue over udp?

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150306/udp

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

                        new chrome.Notification(title: "ChromeUDPSendAsync");

                        var xappwindow = await chrome.app.window.create(
                               Native.document.location.pathname, options: null
                        );

                        //xappwindow.setAlwaysOnTop
                        xappwindow.setAlwaysOnTop(true);

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


            var c = new CanvasRenderingContext2D(800, 400);

            c.canvas.style.border = "1px solid blue";
            c.canvas.AttachToDocument();
            c.canvas.style.SetLocation(0, 0);

            Native.document.body.style.marginTop = "400px";

            new IHTMLButton { "clear" }.AttachToDocument().onclick += delegate
            {
                c.clearRect(0, 0, 800, 400);
            };

            Action begin = delegate
            {
                c.beginPath();
                c.moveTo(0, 0);
            };

            // ok his app needs to run as a chrome app.
            new { }.With(
                async delegate
                {
                    (Native.document.body.style as dynamic).webkitUserSelect = "auto";
                    Native.document.documentElement.style.overflow = IStyle.OverflowEnum.auto;


                    new IHTMLButton { "update pending... update available. click to reload.." }.AttachToDocument().onclick += delegate
                    {
                        // can we get an udp signal from the compiler when the app is out of date, when the update is pending?
                        chrome.runtime.reload();
                    };

                    var n = await chrome.socket.getNetworkList();

                    // Z:\jsc.svn\examples\javascript\chrome\hybrid\HybridHopToUDPChromeApp\Application.cs
                    var n24 = n.Where(x => x.prefixLength == 24).ToArray();

                    n24.WithEach(
                        async nic =>
                        {
                            // wifi? lan?
                            var status = new IHTMLPre { new { nic.name, nic.address } }.AttachToDocument();
                            var buffer = new IHTMLPre { }.AttachToDocument();

                            // Z:\jsc.svn\examples\javascript\chrome\hybrid\HybridHopToUDPChromeApp\Application.cs
                            //var uu = new UdpClient(40014);
                            var uu = new UdpClient(40094);

                            //args.mouse = "awaiting vertexTransform at " + nic + " :40014";

                            // X:\jsc.svn\examples\java\android\forms\FormsUDPJoinGroup\FormsUDPJoinGroup\ApplicationControl.cs
                            // X:\jsc.svn\examples\java\android\LANBroadcastListener\LANBroadcastListener\ApplicationActivity.cs
                            //uu.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"), nic);
                            uu.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"));
                            while (true)
                            {
                                var ux = await uu.ReceiveAsync(); // did we jump to ui thread?
                                                                  //Console.WriteLine("ReceiveAsync done " + Encoding.UTF8.GetString(x.Buffer));
                                                                  //args.vertexTransform = x.Buffer;


                                buffer.innerText = new { ux.Buffer.Length }.ToString();

                                // cam we get also some floats?

                                // https://www.khronos.org/registry/typedarray/specs/latest/

                                float[] f = new Float32Array(new Uint8ClampedArray(ux.Buffer).buffer);

                                // pen x, y, pressure
                                if (f.Length >= 3)
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        var fi = f[i];

                                        new IHTMLDiv { new { i, fi } }.AttachTo(buffer);

                                    }

                                    var x = f[0];
                                    var y = f[1];
                                    var pressure = f[2];


                                    begin();

                                    c.lineWidth = 1 + (pressure / 255.0 * 7);

                                    if (pressure > 0)
                                        c.strokeStyle = "blue";
                                    else
                                        c.strokeStyle = "rgba(0,0,255,0.25)";

                                    c.lineTo((int)(x * 0.1), 400 - (int)(y * 0.1));

                                    //c.lineTo(e.OffsetX, e.OffsetY);
                                    //c.lineTo(e.movementX, e.movementY);
                                    c.stroke();

                                    begin = delegate
                                    {
                                        c.beginPath();
                                        c.moveTo((int)(x * 0.1), 400 - (int)(y * 0.1));
                                    };

                                }
                                //new Float32Array()
                            }
                        }
                    );

                }
             );


        }

    }
}
