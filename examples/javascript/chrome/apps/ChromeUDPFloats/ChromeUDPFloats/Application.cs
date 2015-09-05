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
using ChromeUDPFloats;
using ChromeUDPFloats.Design;
using ChromeUDPFloats.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;
using System.Net.Sockets;
using System.Net;

namespace ChromeUDPFloats
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
            // https://www.shadertoy.com/view/lsSGRz


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

                        xappwindow.show();

                        await xappwindow.contentWindow.async.onload;

                        Console.WriteLine("chrome.app.window loaded!");
                    };


                    return;
                }
            }
            #endregion


            new { }.With(
                async delegate
                {
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
                            var status = new IHTMLPre { new { nic.address } }.AttachToDocument();
                            var buffer = new IHTMLPre { }.AttachToDocument();

                            // Z:\jsc.svn\examples\javascript\chrome\hybrid\HybridHopToUDPChromeApp\Application.cs
                            var uu = new UdpClient(40014);

                            //args.mouse = "awaiting vertexTransform at " + nic + " :40014";

                            // X:\jsc.svn\examples\java\android\forms\FormsUDPJoinGroup\FormsUDPJoinGroup\ApplicationControl.cs
                            // X:\jsc.svn\examples\java\android\LANBroadcastListener\LANBroadcastListener\ApplicationActivity.cs
                            //uu.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"), nic);
                            uu.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"));
                            while (true)
                            {
                                var x = await uu.ReceiveAsync(); // did we jump to ui thread?
                                                                 //Console.WriteLine("ReceiveAsync done " + Encoding.UTF8.GetString(x.Buffer));
                                                                 //args.vertexTransform = x.Buffer;


                                buffer.innerText = new { x.Buffer.Length }.ToString();

                                // cam we get also some floats?

                                // https://www.khronos.org/registry/typedarray/specs/latest/

                                float[] f = new Float32Array(new Uint8ClampedArray(x.Buffer).buffer);

                                if (f.Length >= 16)
                                    for (int i = 0; i < 16; i++)
                                    {
                                        var fi = f[i];

                                        new IHTMLDiv { new { i, fi } }.AttachTo(buffer);

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
