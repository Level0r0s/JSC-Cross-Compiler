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
using x360reddotsaudio;
using x360reddotsaudio.Design;
using x360reddotsaudio.HTML.Pages;
using System.Net.Sockets;
using System.Net;
using x360reddotsaudio.HTML.Images.FromAssets;
using System.Diagnostics;

namespace x360reddotsaudio
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151212/androidudpclipboard

        // from red to asus
        // net use
        // subst a: s:\jsc.svn\examples\javascript\chrome\apps\x360reddotsaudio\bin\Debug\staging\x360reddotsaudio.Application\web

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

                        new chrome.Notification(title: "x360reddotsaudio");

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



            var slider_fade = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, max = 128, title = "fade" }.AttachToDocument();
            var slider_rotate = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, max = 3840, title = "rotate" }.AttachToDocument();


            var baseline = new reddots { }.AttachToDocument();


            baseline.style.width = "384px";
            baseline.style.height = "216px";

            var target = new CanvasRenderingContext2D(3840, 2160);

            target.canvas.style.width = "384px";
            target.canvas.style.height = "216px";

            target.canvas.AttachToDocument();


            //var items = x360reddotsaudiolookup.Program.Get();

            //             IL_f71a0:  ldloc      CS$0$0001
            //  IL_f71a4:  stloc      CS$1$0000
            //  IL_f71a8:  br.s       IL_f71aa
            //  IL_f71aa:  ldloc      CS$1$0000
            //  IL_f71ae:  ret
            //} // end of method Program::Get

            // firstNonZero { ms = 1892, x = 7 }


            var lines = new App { }.lookup.value.Split('\n');

            new IHTMLPre { "lines " + new { lines.Length } }.AttachToDocument();

            // lines { Length = 21142 }
            var sw = Stopwatch.StartNew();

            //lines { Length = 21142 }
            //items { Length = 0, ElapsedMilliseconds = 117 }

            new IHTMLPre { lines[0] }.AttachToDocument();


            var zms0 = lines[0].SkipUntilOrEmpty("ms = ").TakeUntilOrEmpty(",");
            var zx0 = lines[0].SkipUntilOrEmpty("x = ").TakeUntilOrEmpty("}");

            new IHTMLPre { new { zms0, zx0 } }.AttachToDocument();


            var items = (from z in lines
                         let zms = z.SkipUntilOrEmpty("ms = ").TakeUntilOrEmpty(",")
                         let zx = z.SkipUntilOrEmpty("x = ").TakeUntilOrEmpty("}")

                         where !string.IsNullOrEmpty(zms)
                         where !string.IsNullOrEmpty(zx)

                         let ms = Convert.ToInt32(zms)
                         let x = Convert.ToInt32(zx)
                         select new { ms, x }
            ).ToArray();

            new IHTMLPre { "items " + new { items.Length, sw.ElapsedMilliseconds } }.AttachToDocument();

            var firstNonZero = items.FirstOrDefault(x => x.x > 0);

            //  new item { ms = 1857, x = 0},

            //new item { ms = 902443, x = 105},

            // Uncaught TypeError: Cannot read property 'ms' of undefined

            new IHTMLPre { "firstNonZero " + new { firstNonZero.ms, firstNonZero.x } }.AttachToDocument();

            //            items { Length = 21141, ElapsedMilliseconds = 205 }
            //firstNonZero { ms = 1892, x = 7 }

            var last = items.Last();

            new IHTMLPre { "last " + new { last.ms, last.x } }.AttachToDocument();

            //last { ms = 902443, x = 105 }

            var min = items.Where(x => x.ms >= firstNonZero.ms).Min(x => x.x);
            var max = items.Where(x => x.ms >= firstNonZero.ms).Max(x => x.x);

            new IHTMLPre { new { min, max } }.AttachToDocument();

            //{ min = 0, max = 129 }

            var fps = 60;
            var lengthSeconds = 15 * 60;
            var maxFrames = fps * lengthSeconds;

            var slider_frameID = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, max = maxFrames, title = "frameID" }.AttachToDocument();



            #region DirectoryEntry
            var dir = default(DirectoryEntry);

            new IHTMLButton { "openDirectory" }.AttachToDocument().onclick += async delegate
            {
                dir = (DirectoryEntry)await chrome.fileSystem.chooseEntry(new { type = "openDirectory" });
            };


            #endregion



            new { }.With(
                async delegate
                {
                    while (await Native.window.async.onframe)
                    {
                        if (slider_frameID.valueAsNumber > 0)
                        {
                            slider_rotate.valueAsNumber = slider_frameID.valueAsNumber % 3840;



                            var frame1 = 1000.0 / fps;

                            var totalMilliseconds = (int)(slider_frameID.valueAsNumber * frame1);


                            var ms = totalMilliseconds + firstNonZero.ms;

                            var lookup = items.Where(x => x.ms > ms).First();

                            slider_fade.valueAsNumber = lookup.x;
                            Native.document.title = slider_frameID + " " + lookup.x;
                            //Native.document.title = slider_frameID + " " + totalMilliseconds + "ms " + lookup.x;
                        }

                        target.drawImage(baseline, slider_rotate.valueAsNumber, 0, 3840, 2160);
                        target.drawImage(baseline, slider_rotate.valueAsNumber - 3840, 0, 3840, 2160);


                        target.fillStyle = "rgba(0,0,0, " + (1.0 - (slider_fade.valueAsNumber / 128.0).Min(1.0)) + ")";
                        target.fillRect(0, 0, 3840, 2160);

                        // are we rendering?

                        if (dir != null)
                        {
                            if (slider_frameID.valueAsNumber < maxFrames)
                            {
                                dir.WriteAllBytes(slider_frameID.valueAsNumber.ToString().PadLeft(5, '0') + ".jpg", target);

                                slider_frameID.valueAsNumber++;
                            }
                        }
                    }
                }
            );

        }

    }
}
