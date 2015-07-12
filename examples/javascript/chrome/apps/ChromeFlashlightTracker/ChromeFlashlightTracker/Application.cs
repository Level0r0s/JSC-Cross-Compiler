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
using ChromeFlashlightTracker;
using ChromeFlashlightTracker.Design;
using ChromeFlashlightTracker.HTML.Pages;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;

namespace ChromeFlashlightTracker
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

                    // would it be easy to do head tracking via webcam for VR?
                    // the app would run on android, yet
                    // two sattelites could spawn on two laptops to track the head.

                    // would we be able to thread hop between camera devices and android?

                    // http://shopap.lenovo.com/hk/en/laptops/lenovo/u-series/u330p/
                    // The U330p's integrated 720p HD webcam

                    //new IHTMLPre {
                    //    "awaiting onvideo..."
                    //}.AttachToDocument();

                    // HD wont work for chrome app?
                    var v = await Native.window.navigator.async.onvideo;

                    // http://stackoverflow.com/questions/23982463/navigatorusermediaerror-constraintname-message-name-invalidstateerr

                    Console.WriteLine("awaiting onvideo... done");


                    v.AttachToDocument();

                    v.play();

                    // what do we see at this point?

                    // first, could we detect greenscreen without having one?

                    // assuming the camera is static, we could remove the pixels that never seem to move

                    // a shader program, consuming the video would be able to apply the effects a lot faster.
                    // doing it in ui thread will slow it down.

                    //					videoHeight: 480
                    //videoWidth: 640

                    // 
                    //new IHTMLPre {

                    //    new { v.videoWidth, v.videoHeight }
                    //}.AttachToDocument();
                    // do we know the size of the cam?
                    // {{ videoWidth = 0, videoHeight = 0 }}

                    var sw = Stopwatch.StartNew();

                    //Error CS4004  Cannot await in an unsafe context TestGetUserMedia    X:\jsc.svn\examples\javascript\async\test\TestGetUserMedia\TestGetUserMedia\Application.cs  45
                    // https://social.msdn.microsoft.com/Forums/en-US/29a3ca5b-c783-4197-af08-7b3c83585e99/minor-compiler-message-unsafe-async?forum=async


                    while (v.videoWidth == 0)
                        await Native.window.async.onframe;

                    //new IHTMLPre {
                    //    new { v.videoWidth, v.videoHeight, sw.ElapsedMilliseconds, Environment.ProcessorCount }
                    //}.AttachToDocument();

                    // {{ videoWidth = 640, videoHeight = 480, ElapsedMilliseconds = 793, ProcessorCount = 4 }}
                    // {{ videoWidth = 1280, videoHeight = 720, ElapsedMilliseconds = 368, ProcessorCount = 4 }}


                    var frame0 = new CanvasRenderingContext2D(
                        v.videoWidth, v.videoHeight
                    );

                    frame0.canvas.AttachToDocument();

                    var frame0sw = Stopwatch.StartNew();
                    var frame0c = 0;

                    // battery/full speed
                    // {{ frame0c = 1752, ElapsedMilliseconds = 66 }}


                    long yscanmax = 0;
                    //long yscan64max = 0;

                    var __yscan64max = 0;
                    var __yscan64max_ix = 0;

                    var slider = new IHTMLInput
                    {
                        type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range,
                        max = 255,
                        valueAsNumber = 0x40
                    }.AttachToDocument();

                    // 
                    new IHTMLPre {
                        () => new {
                            frame0c,
                            frame0sw.ElapsedMilliseconds,
                            fps = 1000 / frame0sw.ElapsedMilliseconds ,
                            yscanmax,

                            treshold = slider.valueAsNumber,

                            __yscan64max,
                            __yscan64max_ix,


                            px = (float)(__yscan64max_ix - v.videoWidth / 2)  / (float)v.videoWidth
                        }
                    }.AttachToDocument();


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
                                       var px = (float)(__yscan64max_ix - v.videoWidth / 2) / (float)v.videoWidth;

                                       // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704
                                       var nmessage = e.counter + ":" + px + ":0";

                                       sent.innerText = nmessage;

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





                    // jsc, when can we start using semaphores?
                    // this could also trgger state sync
                    //var xx = new System.Threading.SemaphoreSlim(1);



                    new { }.With(
                        async delegate
                        {
                            // could we hop into worker thread, and await for bytes to render?

                            // this is essentially a shader

                            // switch to worker here
                            // at runtime we should know, which fields in this state are in use

                            do
                            {
                                frame0c++;
                                frame0sw = Stopwatch.StartNew();


                                frame0.drawImage(
                                    v,
                                    0, 0, v.videoWidth, v.videoHeight);


                                // could we do thread hopping here to multicore process the data without shaders?
                                // RGB,
                                // would we have each core work on 8bits. a single color?

                                // X:\jsc.svn\examples\javascript\canvas\CanvasFromBytes\CanvasFromBytes\Application.cs 


                                // PLINQ via cpu count or glsl?
                                var rgba_bytes = frame0.bytes;


                                //var rgba_pixels = (rgba[])rgba_bytes;
                                //Error CS0030  Cannot convert type 'byte[]' to 'TestGetUserMedia.rgba[]'   TestGetUserMedia X:\jsc.svn\examples\javascript\async\test\TestGetUserMedia\TestGetUserMedia\Application.cs  98

#if FPOINTERS
					unsafe
					{
						//Error CS0030  Cannot convert type 'byte[]' to 'TestGetUserMedia.rgba*'    TestGetUserMedia X:\jsc.svn\examples\javascript\async\test\TestGetUserMedia\TestGetUserMedia\Application.cs  109
						//var rgba_pixels = (rgba*)rgba_bytes;
						//Error CS0030  Cannot convert type 'byte[]' to 'void*' TestGetUserMedia X:\jsc.svn\examples\javascript\async\test\TestGetUserMedia\TestGetUserMedia\Application.cs  110

						fixed (byte* rgba_ptr = rgba_bytes)
						{
							// script: error JSC1000: running a newer compiler? opcode unsupported - [0x0035] sizeof     +1 -0

							// how would a shader do it?
							var rgba_pixels = (rgba*)rgba_ptr;

							// looks legit

							// how does it compile for js?

							for (int x = 0; x < v.videoWidth; x++)
								for (int y = 0; y < v.videoHeight; y++)
								{
									rgba_pixels[x + y * v.videoWidth].b = 0;
									rgba_pixels[x + y * v.videoWidth].g = 0;

								}

						}

					}
#endif
                                // fps4

                                // https://github.com/GoogleChrome/chrome-app-samples/tree/master/samples/camera-capture

                                // make it all blue
                                // glsl. u8vec4

                                //var yscan = new long[v.videoWidth];

                                #region yscan
                                var yscan = new int[v.videoWidth];
                                yscanmax = 0;

                                // lets deal only with first half of bytes
                                //for (int x = 0; x < rgba_bytes.Length / 2; x += 4)
                                //for (int x = 0; x < rgba_bytes.Length; x += 4)
                                for (int ix = 0; ix < v.videoWidth; ix++)
                                {
                                    var yscanix = 0;

                                    // interleave?
                                    for (int iy = v.videoHeight / 4; iy < v.videoHeight - v.videoHeight / 4; iy += 5)
                                    {

                                        var x = ix * 4;
                                        var y = iy * 4 * v.videoWidth;

                                        //// red
                                        //rgba_bytes[x + 0] = 0;
                                        //rgba_bytes[x + 1] = (byte)(1 - rgba_bytes[x + 1]);
                                        //// blue
                                        //rgba_bytes[x + 2] = 0;


                                        var r = rgba_bytes[y + x + 0];
                                        var g = rgba_bytes[y + x + 1];
                                        var b = rgba_bytes[y + x + 2];


                                        // red
                                        rgba_bytes[y + x + 0] = 0;


                                        //float rf = 255f - rgba_bytes[x + 0];
                                        ////var rf = r / 255f;

                                        ////var lux =
                                        ////    ((float)rgba_bytes[x + 0] / (255f))
                                        ////    * ((float)rgba_bytes[x + 1] / (255f))
                                        ////    * ((float)rgba_bytes[x + 2] / (255f));

                                        //rgba_bytes[x + 1] = (byte)(
                                        //    rf
                                        ////(255f * rf)
                                        //);

                                        var xg = (byte)(
                                             (3 * 255 - r - g - b)
                                             / 3
                                         );

                                        // either white out or black out?



                                        if (xg < slider.valueAsNumber)
                                        {
                                            xg = 0;
                                            yscanix += 1;
                                        }


                                        // script: error JSC1000: unknown opcode stelem.i8 at <0197> nop.try + 0x0023

                                        rgba_bytes[y + x + 1] = xg;

                                        // blue
                                        rgba_bytes[y + x + 2] = 0;
                                    }

                                    yscan[ix] = yscanix;

                                    if (yscanix > yscanmax)
                                        yscanmax = yscanix;
                                }

                                //yscanmax = yscan.Max();
                                #endregion


                                #region top visualization
                                for (int ix = 0; ix < v.videoWidth; ix++)
                                {
                                    // gives a better visualization
                                    var kf = 1 - ((float)yscan[ix] / (float)yscanmax);
                                    //var kf = ((float)yscan[ix] / (float)yscanmax);
                                    var k8 = (byte)(kf * 16f);

                                    for (int iy = 0; iy < k8; iy++)
                                    {

                                        var x = ix * 4;
                                        var y = iy * 4 * v.videoWidth;

                                        rgba_bytes[y + x + 0] = 255;
                                    }
                                }
                                #endregion

                                #region now do mipmap on the yscan 
                                var yscan64 = new int[v.videoWidth];

                                __yscan64max = 0;

                                //var lookahead = 64;
                                var lookahead = 16;

                                for (int ix = 0; ix < v.videoWidth; ix++)
                                {
                                    var yscan64ix = 0;

                                    //if (ix >= 64)
                                    //    if (ix < (v.videoWidth - 64))
                                    for (int ixx = -lookahead; ixx < lookahead; ixx++)
                                    {

                                        if (ix + ixx > 0)
                                            if (ix + ixx < v.videoWidth)
                                            {
                                                var yy = yscan[ix + ixx];

                                                if (yy > 0)
                                                    yscan64ix += yy;
                                            }
                                    }

                                    if (yscan64ix > __yscan64max)
                                    {
                                        __yscan64max = yscan64ix;
                                        __yscan64max_ix = ix;
                                    }

                                    yscan64[ix] = yscan64ix;
                                }
                                #endregion

                                // parralax X . if we get Y too we could also know the zoom in out
                                //yscan64max = yscan64.Max();
                                //yscan64max = __yscan64max_ix;

                                #region bottom visualization
                                for (int ix = 0; ix < v.videoWidth; ix++)
                                {
                                    //var kf = 1 - ((float)yscan64[ix] / (float)yscan64max);
                                    var kf = ((float)yscan64[ix] / (float)__yscan64max);
                                    var k8 = (byte)(kf * 16f);



                                    for (int iy = 0; iy < k8; iy++)
                                    {

                                        var x = ix * 4;
                                        var y = (v.videoHeight - iy) * 4 * v.videoWidth;

                                        if (ix == __yscan64max_ix)
                                            rgba_bytes[y + x + 1] = 255;
                                        else
                                            rgba_bytes[y + x + 0] = 255;
                                    }
                                }
                                #endregion


                                frame0.bytes = rgba_bytes;



                                // Permission 'videoCapture ' is unknown or URL pattern is malformed.
                            } while (await Native.window.async.onframe);
                        }
                    );



                    // {{ videoWidth = 640, videoHeight = 480, ElapsedMilliseconds = 109 }}

                    await Native.window.async.onblur;

                    // stream is not stopped yet?
                    //v.Orphanize();

                    Native.body.style.backgroundColor = "yellow";
                }
            );
        }

    }

    struct rgba
    {
        public byte r, g, b, a;
    }
}
