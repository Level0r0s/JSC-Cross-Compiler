#pragma warning disable 1998

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
using ChromeThreadedCameraTracker;
using ChromeThreadedCameraTracker.Design;
using ChromeThreadedCameraTracker.HTML.Pages;
using System.Diagnostics;
using System.Threading;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Threading;
using System.Net.Sockets;
using System.Net;

namespace ChromeThreadedCameraTracker
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        //public Func<byte[], byte[]> WorkerThread(int ii3, int vw, int vh)


        /// <summary>
        /// If GearVR sends this to run on laptop we can observe the flashlight in parallax.
        /// </summary>
        public class WorkerThread
        {
            public readonly Func<byte[], byte[]> Invoke;

            public WorkerThread(int ii3, int vw, int vh)
            {
                var historic_z = new int[vw];
                var historic_x = new int[vw];
                var historic_y = new int[vw];

                //int[vw]  historic_xx = { vw/2 };
                // http://stackoverflow.com/questions/1897555/what-is-the-equivalent-of-memset-in-c
                // OpCodes.Initblk
                //Array.Clear()
                //historic_x = { vw / 2 };

                for (int n = 0; n < vw; n++) historic_x[n] = vw / 2;
                for (int n = 0; n < vh; n++) historic_y[n] = vh / 2;

                var yscan16_highp = new int[vw];

                // lowp
                var yscan16 = new int[vw];
                var yscan = new int[vw];
                var xscan = new int[vh];
                for (int n = 0; n < vh; n++) xscan[n] = 0;
                for (int n = 0; n < vw; n++) yscan[n] = 0;
                for (int n = 0; n < vw; n++) yscan16[n] = 0;

                var yscanmax_ix = 0;


                Invoke = (byte[] rgba_bytes) =>
                {
                    //var i3 = frame0c % 3;
                    //var i3 = 0;

                    var i3 = (ii3 + 1) % 3;

                    var slider = rgba_bytes[4];


                    var frameID0 =
                        rgba_bytes[0]
                        + (rgba_bytes[1] << 8);

                    var frameID = frameID0 + i3;

                    //x:\jsc.svn\examples\javascript\chrome\apps\chromeflashlighttracker\chromeflashlighttracker\application.cs
                    var xscanmax = 0;
                    var xscanmax_iy = 0;
                    var xscanmax_iyc = 0;

                    // 16pixels added up

                    var yscanmax16 = 0;
                    var yscanmax = 0;
                    //var yscanmax_ixc = 0;


                    // while we have the new frame available
                    // we may only want to deal with the are of interest...
                    var clipleft = ((yscanmax_ix - yscanmax_ix % (vw / 10)) - vw / 5).Min(vw / 2).Max(0);
                    var clipright = (vw - ((yscanmax_ix - yscanmax_ix % (vw / 10)) + vw / 5)).Min(vw / 2).Max(0);


                    #region xscan
                    for (int iy = 0; iy < vh; iy += 1)
                    {
                        var xscaniy = 0;

                        for (int ix = clipleft; ix < (vw - clipright); ix++)
                        {
                            var x = ix * 4;
                            var y = iy * 4 * vw;

                            //fixed (byte* r = &rgba_bytes[y + x + 0])
                            var r = rgba_bytes[y + x + 0];
                            var g = rgba_bytes[y + x + 1];
                            var b = rgba_bytes[y + x + 2];

                            var xg = (byte)(
                              (3 * 255 - r - g - b)
                              / 3
                            );

                            if (xg <= 0x20)
                            {
                                xg = 0;
                                xscaniy += 1;
                            }
                        }

                        xscan[iy] = xscaniy;

                        if (xscaniy > xscanmax)
                        {
                            xscanmax = xscaniy;
                            xscanmax_iy = iy;
                        }


                        if (xscaniy == xscanmax)
                        {
                            xscanmax_iyc = iy;
                        }
                    }
                    #endregion


                    #region yscan
                    for (int ix = clipleft; ix < (vw - clipright); ix++)
                    {
                        var yscanix = 0;

                        // first row contains variables?
                        for (int iy = 1; iy < vh; iy += 1)
                        {
                            var x = ix * 4;
                            var y = iy * 4 * vw;

                            //fixed (byte* r = &rgba_bytes[y + x + 0])
                            var r = rgba_bytes[y + x + 0];
                            var g = rgba_bytes[y + x + 1];
                            var b = rgba_bytes[y + x + 2];

                            var xg = (byte)(
                                  (3 * 255 - r - g - b)
                                  / 3
                              );

                            if (xg < 0x20)
                            {
                                xg = 0;
                                yscanix += 1;
                            }

                            // animate only the left 8 pixels 
                            //if (ix < 4)
                            //{
                            //    rgba_bytes[y + x + 0] = 0;
                            //    rgba_bytes[y + x + 1] = 0;
                            //    rgba_bytes[y + x + 2] = 0;

                            //    rgba_bytes[y + x + i3] = xg;

                            //    continue;
                            //}


                            //rgba_bytes[y + x + 0] = xg;
                            //rgba_bytes[y + x + 1] = xg;
                            //rgba_bytes[y + x + 2] = xg;


                            //// green
                            //rgba_bytes[y + x + 0] = 0;
                            //rgba_bytes[y + x + 1] = xg;
                            //rgba_bytes[y + x + 2] = 0;


                            // green dither?


                            // dither bg more...
                            //if (xg > 80)
                            //{
                            //    rgba_bytes[y + x + 0] = 0;
                            //    rgba_bytes[y + x + 1] = (byte)(xg - xg % 64);
                            //    rgba_bytes[y + x + 2] = 0;
                            //}
                            //else
                            rgba_bytes[y + x + 0] = 0;
                            //rgba_bytes[y + x + 0] = (byte)(xg - xg % (32 + (xg / 2)) + 31);
                            //rgba_bytes[y + x + 1] = (byte)(xg - xg % (32 + (xg / 2)));
                            rgba_bytes[y + x + 1] = (byte)(xg - xg % (32 + (xg / 2)) + 31);
                            rgba_bytes[y + x + 2] = 0;


                            var historiy_leftotrighty_projected_totopsidebar = 32 * historic_y[ix % vh] / vh;
                            if (iy >= historiy_leftotrighty_projected_totopsidebar - 2)
                                if (iy < historiy_leftotrighty_projected_totopsidebar)
                                    if ((ix) < (frameID % vw))
                                    {
                                        rgba_bytes[y + x + 0] = 0;
                                        rgba_bytes[y + x + 1] = 0;
                                        rgba_bytes[y + x + 2] = 0;
                                        //continue;
                                    }

                            var historix_leftotrighty_projected_totopsidebar = 32 * historic_x[ix % vw] / vw;
                            if (iy >= historix_leftotrighty_projected_totopsidebar - 2)
                                if (iy < historix_leftotrighty_projected_totopsidebar)
                                    if ((ix) < (frameID % vw))
                                    {
                                        rgba_bytes[y + x + 0] = 0xff;
                                        rgba_bytes[y + x + 1] = 0xff;
                                        rgba_bytes[y + x + 2] = 0xff;
                                        //continue;
                                    }


                            // wider line
                            var historicz_projected = historic_z[ix % vw];
                            if (iy >= historicz_projected - 8)
                                if (iy < historicz_projected)
                                    if ((ix) < (frameID % vw))
                                    {
                                        rgba_bytes[y + x + 0] = 0;
                                        rgba_bytes[y + x + 1] = 0;
                                        rgba_bytes[y + x + 2] = 0xff;
                                        //continue;
                                    }

                        }

                        // init
                        yscan[ix] = yscanix;
                        yscan16[ix] = yscanix;
                    }
                    #endregion

                    #region yscan16
                    for (int ix = slider; ix < vw - slider; ix++)
                    {
                        var yscanix = yscan16[ix];

                        for (int iix = 1; iix < slider; iix++)
                        {
                            yscanix += yscan[ix + iix];
                            yscanix += yscan[ix - iix];
                        }

                        //yscanix /= (1 + slider);
                        //yscanix += yscan16[ix];
                        //yscan16[ix] += (yscanix / 2) / slider;

                        yscan16_highp[ix] = yscanix;
                        yscanix = (int)((yscanix / (1 + slider)) * 0.7);
                        yscan16[ix] = yscanix;
                        //yscan16[ix] = (int)((yscanix / (1 + slider)) * 0.7);

                        if (yscan16[ix] > yscanmax16)
                        {
                            yscanmax16 = yscanix;
                            yscanmax = yscan[ix];
                            yscanmax_ix = ix;
                        }
                    }
                    #endregion

                    #region top red visualization yscan
                    for (int ix = clipleft; ix < (vw - clipright); ix += 2)
                    {
                        // gives a better visualization
                        var kf = ((float)(yscanmax - yscan[ix]) / (float)yscanmax);
                        //var kf = 1 - ((float)yscan[ix] / (float)yscanmax);
                        //var kf = ((float)yscan[ix] / (float)yscanmax);
                        //var k8 = (byte)(kf * slider);
                        var k8 = (byte)(kf * 32);
                        if (k8 > 32)
                        {
                            // Nan overflow?
                            k8 = 0;
                        }

                        for (int iy = 0; iy < k8; iy += 2)
                        {

                            var x = ix * 4;
                            var y = iy * 4 * vw;

                            rgba_bytes[y + x + 0] = 255;
                            //rgba_bytes[y + x + 1] >>= 1;
                            //rgba_bytes[y + x + 2] >>= 1;
                        }

                        // big number?
                        //var n = (int)(yscan16[ix] * 0.3);
                        var n = (int)(yscan16[ix]);


                        //for (int iy = (vh - n).Max(vh / 3); iy < vh; iy++)
                        for (int iy = (vh - n).Max(0); iy < vh; iy += 2)
                        {

                            var x = ix * 4;
                            var y = iy * 4 * vw;

                            rgba_bytes[y + x + 0] = 255;
                            //rgba_bytes[y + x + 1] >>= 2;
                            //rgba_bytes[y + x + 2] >>= 2;
                        }
                    }

                    #endregion

                    #region blue visualization
                    for (int iy = 4; iy < vh; iy += 2)
                    {
                        // gives a better visualization
                        var kf = 1 - ((float)xscan[iy] / (float)xscanmax);
                        //var kf = ((float)yscan[ix] / (float)yscanmax);
                        var k8 = (byte)(kf * 16f);

                        for (int ix = 0; ix < k8; ix++)
                        {

                            var x = ix * 4;
                            var y = iy * 4 * vw;

                            //rgba_bytes[y + x + 0] >>= 1;
                            //rgba_bytes[y + x + 1] >>= 1;
                            //rgba_bytes[y + x + 2] = 255;

                            //rgba_bytes[y + x + 0] = 0;
                            //rgba_bytes[y + x + 1] = 0;
                            //rgba_bytes[y + x + 2] = 0;

                            rgba_bytes[y + x + i3] >>= 1;
                        }
                    }

                    #endregion


                    #region crosshair!

                    var crosshairsize = (yscan16[yscanmax_ix]).Max(4);

                    historic_z[(frameID0 + 0) % vw] = yscan16_highp[yscanmax_ix];
                    historic_z[(frameID0 + 1) % vw] = yscan16_highp[yscanmax_ix];
                    historic_z[(frameID0 + 2) % vw] = yscan16_highp[yscanmax_ix];

                    // remember where is our cursor
                    historic_x[(frameID0 + 0) % vw] = yscanmax_ix;
                    historic_x[(frameID0 + 1) % vw] = yscanmax_ix;
                    historic_x[(frameID0 + 2) % vw] = yscanmax_ix;

                    historic_y[(frameID0 + 0) % vw] = xscanmax_iy;
                    historic_y[(frameID0 + 1) % vw] = xscanmax_iy;
                    historic_y[(frameID0 + 2) % vw] = xscanmax_iy;

                    // can we  pin and dereference the pointer into struct?
                    // out
                    // 24..40? lowp/ highp
                    rgba_bytes[0] = (byte)(yscan16_highp[yscanmax_ix] & 0xff);
                    rgba_bytes[1] = (byte)((yscan16_highp[yscanmax_ix] >> 8) & 0xff);

                    rgba_bytes[4] = (byte)(yscanmax_ix & 0xff);
                    rgba_bytes[5] = (byte)((yscanmax_ix >> 8) & 0xff);

                    rgba_bytes[8] = (byte)(xscanmax_iy & 0xff);

                    //yscanmax_ix = 0;

                    // 1sec at 60fps = 20

                    //for (int i = 0; i < 20; i++)
                    //{
                    //    yscanmax_ix = historic_x[(frameID0 + vw - i * 3) % vw];

                    //yscanmax_ix /= 3;
                    //#endregion


                    //crosshairsize = (int)((crosshairsize / (1 + slider)) * 0.7);

                    var crosshairsizelow = crosshairsize;
                    //((int)((crosshairsize / (1 + slider)) * 0.7).Max(4));


                    //for (int ix = yscanmax_ix; ix <= yscanmax_ixc; ix++)
                    for (int ix = (yscanmax_ix - crosshairsizelow).Max(0); ix <= (yscanmax_ix + crosshairsizelow); ix += crosshairsizelow * 2)
                    {
                        for (int iy = (xscanmax_iy - crosshairsizelow / 2); iy < (xscanmax_iy + crosshairsizelow / 2); iy += 4)
                        {
                            var x = ix * 4;
                            var y = iy * 4 * vw;

                            if (ix >= 0)
                                if (iy >= 0)
                                    if (iy < vh)
                                        if (ix < vw)
                                        {
                                            //fixed (byte* r = &rgba_bytes[y + x + 0])
                                            rgba_bytes[y + x + 0] = 0xff;
                                            //rgba_bytes[y + x + 1] = 0xff;
                                            //rgba_bytes[y + x + 2] = 0;
                                        }
                        }
                    }

                    for (int iy = (xscanmax_iy - crosshairsizelow / 2).Max(0); iy <= (xscanmax_iy + crosshairsizelow / 2).Min(vh); iy += crosshairsizelow)
                    {
                        for (int ix = (yscanmax_ix - crosshairsizelow).Max(0); ix < (yscanmax_ix + crosshairsizelow).Min(vw); ix += 4)
                        {
                            var x = ix * 4;
                            var y = iy * 4 * vw;

                            //fixed (byte* r = &rgba_bytes[y + x + 0])
                            rgba_bytes[y + x + 0] = 0xff;
                            //rgba_bytes[y + x + 1] = 0xff;
                            //rgba_bytes[y + x + 2] = 0;
                        }
                    }

                    //}

                    #endregion


                    return rgba_bytes;
                };
            }
        }

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

            // 
            // http://stackoverflow.com/questions/13076272/how-do-i-give-webkitgetusermedia-permission-in-a-chrome-extension-popup-window
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150712
            new { }.With(
                async delegate
                {
                    Native.body.Clear();
                    Native.document.documentElement.style.overflow = IStyle.OverflowEnum.auto;

                    new IHTMLPre
                    {
                        "GearVR should send analog and digital signal to start parallax tracking only if near zero rotation."
                    }.AttachToDocument();

                    // would it be easy to do head tracking via webcam for VR?
                    // the app would run on android, yet
                    // two sattelites could spawn on two laptops to track the head.

                    // would we be able to thread hop between camera devices and android?

                    // http://shopap.lenovo.com/hk/en/laptops/lenovo/u-series/u330p/
                    // The U330p's integrated 720p HD webcam


                    // HD wont work for chrome app?
                    var v = await Native.window.navigator.async.onvideo;
                    v.AttachToDocument();

                    v.play();

                    new IHTMLButton { "stop" }.AttachToDocument().onclick += delegate
                    {
                        //v.src = null;
                        v.src = "";
                        //v.pause();
                        //v.stop();
                    };

                    var sw = Stopwatch.StartNew();

                    while (v.videoWidth == 0)
                        await Native.window.async.onframe;


                    const int zoomx = 2;
                    //const int zoomy = 4;

                    //var ow = v.videoWidth / zoomx;
                    var ow = 512;
                    //var oh = v.videoHeight / zoomy;
                    //var oh = 256;
                    //var oh = 96;
                    //var oh = 256;
                    var oh = 128;

                    // 40 with udp
                    //var oh = 0xA0;
                    //var oh = 0xb0;

                    // 58
                    //var oh = 196;
                    // 35
                    //var oh = 256;

                    var aloop = new IHTMLInput
                    {
                        title = "loop it",

                        type = ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox,
                        @checked = true
                    }.AttachToDocument();

                    var slider = new IHTMLInput
                    {
                        title = "stabilizer?",

                        type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range,
                        //max = 255,
                        //max = 128,
                        max = 64,

                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150712-1
                        valueAsNumber = 48
                    }.AttachToDocument();


                    var z = 0;
                    var x = 0;
                    var y = 0;

                    var frame0in = new CanvasRenderingContext2D(ow, oh);

                    Action frame0in_drawImage = delegate
                    {

                        var clip = v.videoHeight / 6;

                        // 6000 zoom out
                        // 8000 zoom in

                        // haha. this wont work.
                        //var zz = (2000 - (z - 6000).Min(2000)) / 2000f;
                        //clip = (int)(clip * zz);

                        frame0in.drawImage(v, 0, clip, v.videoWidth, v.videoHeight - clip * 2, 0, 0, ow, oh);
                    };

                    //frame0in.canvas.AttachToDocument();

                    //var frame0out = new CanvasRenderingContext2D(ow, oh);
                    ////frame0out.canvas.AttachToDocument();

                    //var frame1out = new CanvasRenderingContext2D(ow, oh);
                    ////frame1out.canvas.AttachToDocument();

                    //var frame2out = new CanvasRenderingContext2D(ow, oh);
                    //frame2out.canvas.AttachToDocument();


                    var frame3out = new CanvasRenderingContext2D(ow, oh);
                    frame3out.canvas.AttachToDocument();


                    var frame0sw = Stopwatch.StartNew();
                    var frameID = 0;
                    var frame0sw0 = Stopwatch.StartNew();

                    var fps3 = 0;

                    // avg per sec x3
                    var fps3avg = new byte[20 * 3];



                    var sent = new IHTMLPre
                    {
                    }.AttachToDocument();


                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150701


                    // what about 255.255.255.255 ?
                    chrome.socket.getNetworkList().ContinueWithResult(async n =>
                    {
                        // which networks should we notify of our data?

                        //new IHTMLPre { new { n.Length } }.AttachToDocument();

                        foreach (var item in n)
                        {
                            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704
                            // skip ipv6
                            if (item.address.Contains(":"))
                                continue;

                            #region send
                            new IHTMLButton { "send onframe " + item.address }.AttachToDocument().With(
                                async refresh =>
                                {
                                    refresh.style.color = "blue";

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


                                    // this will eat too much memory?
                                    //div.ownerDocument.defaultView.onframe +=
                                    Native.window.onframe += e =>
                                     {
                                         //var px = (float)(__yscan64max_ix - v.videoWidth / 2) / (float)v.videoWidth;
                                         var px0 = (float)(x - ow / 2) / (float)ow;
                                         var py0 = (float)(y - oh / 2) / (float)oh;


                                         // 4000 zoom out
                                         // 7000 zoom in

                                         // haha. this wont work.
                                         //var pz = ((z - 4500) / 2000f).Max(0f).Min(1.5f);

                                         // in a dark room?
                                         var pz = ((z - 6000) / 1000f).Max(0f).Min(1.5f);

                                         // allow py only if pz is negative.
                                         // 0 means py stays
                                         // 1 means py is zero

                                         var pzz = (1.0f - pz).Max(0.0f).Min(1.0f);

                                         var py = py0 * pzz;
                                         var px = px0 * pzz;

                                         pz -= 0.3f;
                                         // !! leaning in kills parallax left and up.
                                         // rotating on gear vr will cancel all parallax.

                                         // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704
                                         var nmessage = e.counter + ":" + px + ":" + py + ":" + pz;

                                         //sent.innerText = nmessage.Replace(":", ":\n") + new { py0, pzz, py };
                                         sent.innerText = nmessage.Replace(":", ":\n");

                                         var data = Encoding.UTF8.GetBytes(nmessage);      //creates a variable b of type byte



                                         //new IHTMLPre { "about to send... " + new { data.Length } }.AttachToDocument();

                                         // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                                         socket.Send(
                                              data,
                                              data.Length,
                                              hostname: "239.1.2.3",
                                              port: 43834
                                          );
                                     };


                                }
                            );
                            #endregion

                        }
                    }
                );


                    var status = new IHTMLPre {
                        () => new {
                            frameID,
                            frame0sw.ElapsedMilliseconds,

                            fps = 1000 / frame0sw.ElapsedMilliseconds ,
                            fps3avg = (byte)fps3avg.Average(k => k),

                            x,y,z,

                            fps3,

                            // script: error JSC1000: No implementation found for this native method, please implement [System.String.Replace(System.Char, System.Char)]
                        }.ToString().Replace(",", ",\t")
                    }.AttachToDocument();


                    var fdiv = new IHTMLDiv { }.AttachToDocument();

                    new { }.With(
                        async delegate
                        {
                            var vw = ow;
                            var vh = oh;

                            #region 3 workers
                            // what about sending bytes[2,3] over?
                            byte[] rgba_bytes0in = null;
                            var rgba_bytes0in_set = new System.Threading.SemaphoreSlim(1);
                            byte[] rgba_bytes0out = null;
                            var rgba_bytes0out_set = new System.Threading.SemaphoreSlim(1);
                            //SemaphoreSlim rgba_bytes0out_set = new __SemaphoreSlim(1) { Name = "rgba_bytes0out_set" };


                            byte[] rgba_bytes1in = null;
                            var rgba_bytes1in_set = new System.Threading.SemaphoreSlim(1);
                            byte[] rgba_bytes1out = null;
                            var rgba_bytes1out_set = new System.Threading.SemaphoreSlim(1);

                            byte[] rgba_bytes2in = null;
                            var rgba_bytes2in_set = new System.Threading.SemaphoreSlim(1);
                            byte[] rgba_bytes2out = null;
                            var rgba_bytes2out_set = new System.Threading.SemaphoreSlim(1);


                            Task.Run(
                                async delegate
                                {
                                    var w = new WorkerThread(0, vw, vh);
                                    next:
                                    await rgba_bytes0in_set.WaitAsync();

                                    rgba_bytes0out = w.Invoke(rgba_bytes0in);
                                    rgba_bytes1out = null;
                                    rgba_bytes2out = null;

                                    rgba_bytes0in = null;
                                    rgba_bytes1in = null;
                                    rgba_bytes2in = null;
                                    // we should not sync back data we did not put there. nor should it be there anyway?
                                    rgba_bytes0out_set.Release();
                                    goto next;
                                }
                            );


                            Task.Run(
                                async delegate
                                {
                                    var w = new WorkerThread(1, vw, vh);
                                    next:
                                    await rgba_bytes1in_set.WaitAsync();
                                    rgba_bytes0out = null;
                                    rgba_bytes1out = w.Invoke(rgba_bytes1in);
                                    rgba_bytes2out = null;

                                    rgba_bytes0in = null;
                                    rgba_bytes1in = null;
                                    rgba_bytes2in = null;

                                    rgba_bytes1out_set.Release();
                                    goto next;
                                }
                            );


                            Task.Run(
                                async delegate
                                {
                                    var w = new WorkerThread(2, vw, vh);
                                    next:
                                    await rgba_bytes2in_set.WaitAsync();

                                    rgba_bytes0out = null;
                                    rgba_bytes1out = null;
                                    rgba_bytes2out = w.Invoke(rgba_bytes2in);
                                    rgba_bytes0in = null;
                                    rgba_bytes1in = null;
                                    rgba_bytes2in = null;


                                    rgba_bytes2out_set.Release();
                                    goto next;
                                }
                            );
                            #endregion

                            Console.WriteLine("worker0 spinning already...");

                            //do
                            await v.async.onclick;

                            {

                                frame0sw0 = Stopwatch.StartNew();
                                //Console.WriteLine(frame0sw0.ElapsedMilliseconds + " fetch frame0");

                                frame0in.drawImage(v, 0, v.videoHeight / 4, v.videoWidth, v.videoHeight / 2, 0, 0, vw, vh);
                                rgba_bytes0in = frame0in.bytes;
                                rgba_bytes0in_set.Release();
                                new IHTMLPre { frame0sw0.ElapsedMilliseconds + " frame0 posted" }.AttachTo(fdiv);

                                // await Task.Delay(1000 / 60);
                                // 300?
                                // Console.WriteLine(frame0sw0.ElapsedMilliseconds + " send frame2");
                                frame0in.drawImage(v, 0, v.videoHeight / 4, v.videoWidth, v.videoHeight / 2, 0, 0, vw, vh);
                                rgba_bytes1in = frame0in.bytes;
                                rgba_bytes1in_set.Release();
                                new IHTMLPre { frame0sw0.ElapsedMilliseconds + " frame1 posted" }.AttachTo(fdiv);

                                loop:
                                // 60fps!!
                                frameID += 3;

                                frame0in_drawImage();
                                rgba_bytes2in = frame0in.bytes;
                                rgba_bytes2in[0] = (byte)(frameID & 0xff);
                                rgba_bytes2in[1] = (byte)((frameID >> 8) & 0xff);
                                rgba_bytes2in[4] = slider;
                                rgba_bytes2in_set.Release();

                                //await Task.WhenAll(Native.window.async.onframe, rgba_bytes0out_set.WaitAsync());
                                await rgba_bytes0out_set.WaitAsync();
                                if (!aloop.@checked)
                                    new IHTMLPre { frame0sw0.ElapsedMilliseconds + " collected frame0" }.AttachTo(fdiv);
                                //frame0out.bytes = rgba_bytes0out;
                                //frame3out.drawImage(frame0out.canvas, 0, 0, vw, vh);
                                frame3out.bytes = rgba_bytes0out;
                                rgba_bytes2in = null;
                                rgba_bytes0out = null;

                                frame0in_drawImage();
                                rgba_bytes0in = frame0in.bytes;
                                rgba_bytes0in[0] = (byte)(frameID & 0xff);
                                rgba_bytes0in[1] = (byte)((frameID >> 8) & 0xff);
                                rgba_bytes0in[4] = slider;
                                rgba_bytes0in_set.Release();

                                //await Task.WhenAll(Native.window.async.onframe, rgba_bytes1out_set.WaitAsync());
                                await rgba_bytes1out_set.WaitAsync();
                                if (!aloop.@checked)
                                    new IHTMLPre { frame0sw0.ElapsedMilliseconds + " collected frame1" }.AttachTo(fdiv);
                                //frame1out.bytes = rgba_bytes1out;
                                //frame3out.drawImage(frame1out.canvas, 0, 0, vw, vh);
                                frame3out.bytes = rgba_bytes1out;
                                rgba_bytes0in = null;
                                rgba_bytes1out = null;


                                frame0in_drawImage();
                                rgba_bytes1in = frame0in.bytes;
                                rgba_bytes1in[0] = (byte)(frameID & 0xff);
                                rgba_bytes1in[1] = (byte)((frameID >> 8) & 0xff);
                                rgba_bytes1in[4] = slider;
                                rgba_bytes1in_set.Release();

                                await Task.WhenAll(Native.window.async.onframe, rgba_bytes2out_set.WaitAsync());
                                if (!aloop.@checked)
                                    new IHTMLPre { frame0sw0.ElapsedMilliseconds + " collected frame2, next? " + new { aloop.@checked } }.AttachTo(fdiv);

                                //
                                z = rgba_bytes2out[0] + (rgba_bytes2out[1] << 8);
                                x = rgba_bytes2out[4] + (rgba_bytes2out[5] << 8);
                                y = rgba_bytes2out[8];

                                //frame2out.bytes = rgba_bytes2out;
                                //frame3out.drawImage(frame2out.canvas, 0, 0, vw, vh);
                                frame3out.bytes = rgba_bytes2out;
                                rgba_bytes1in = null;
                                rgba_bytes2out = null;


                                frame0sw0.Stop();
                                frame0sw = frame0sw0;


                                //if (v.src == null)
                                //{
                                //    Native.body.style.backgroundColor = "red";
                                //    return;
                                //}

                                //if (aloop)
                                if (!aloop.@checked)
                                {
                                    //await Task.WhenAny(aloop.async.@checked, v.async.onclick);
                                    await v.async.onclick;

                                    fdiv.Clear();
                                }

                                frame0sw0 = Stopwatch.StartNew();
                                fps3 = (int)(3000 / frame0sw.ElapsedMilliseconds);
                                fps3avg[(frameID / 3) % fps3avg.Length] = (byte)fps3;

                                if (fps3 > 55)
                                {
                                    status.style.color = "blue";
                                }
                                else
                                {
                                    status.style.color = "red";
                                }

                                goto loop;


                                // 10000ms?
                                // 400ms?
                            }
                            //} while (await v.async.onclick);
                            //} while (await Native.window.async.onframe);
                        }
                    );

                    await Native.window.async.onblur;

                    Native.body.style.backgroundColor = "yellow";
                }
            );
        }

    }

    //struct rgba
    //{
    //    public byte r, g, b, a;
    //}
}


//4559ms 4 frame0 posted
//view-source:53769 4563ms 8 frame1 posted
//view-source:53769 4565ms 10 {{ frame0c = 1 }}
//view-source:53769 4570ms 15 frame2 posted
//view-source:53769 4572ms 17 collect frame0
//view-source:53769 4600ms 45 collected frame0
//view-source:53769 4609ms 54 frame0 posted
//view-source:53769 4638ms 83 collected frame1
//view-source:53769 4644ms 89 frame1 posted
//view-source:53769 4679ms 124 collected frame2
//view-source:53769 20847ms 0 {{ frame0c = 2 }}
//view-source:53769 20856ms 9 frame2 posted
//view-source:53769 20859ms 12 collect frame0