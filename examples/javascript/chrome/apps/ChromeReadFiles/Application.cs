
#pragma warning disable CS1998

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
using ChromeReadFiles;
using ChromeReadFiles.Design;
using ChromeReadFiles.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;
using System.Diagnostics;

namespace ChromeReadFiles
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://chrome.google.com/webstore/detail/simple-red-theme/ealcinkolodcnkokioepdoheohkffejc?hl=en
        // https://chrome.google.com/webstore/detail/pink-default-theme/odibiblpllhihdjljanngdbcfcfbpfpc?hl=en
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150831

        // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeReadFiles\ChromeReadFiles\bin\Debug\staging\ChromeReadFiles.Application\web

        //        subst /D b:
        //subst b: s:\jsc.svn\examples\javascript\chrome\apps\ChromeReadFiles\bin\Debug\staging\ChromeReadFiles.Application\web

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150809/chrome-filesystem

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

                        new chrome.Notification(title: "ChromeReadFiles");

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

            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeReadFiles\ChromeReadFiles\Application.cs

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
                    (Native.body.style as dynamic).webkitUserSelect = "text";

                    IHTMLImage c0002 = new IHTMLDiv { "frame2 " + new { DateTime.Now } };


                    var div0001 = new IHTMLDiv { "frame1 " + new { DateTime.Now } };

                    //WriteAllBytes 0001 { { size = 309688 } }
                    //WriteAllBytes 0001 { { ElapsedMilliseconds = 108 } } 3024,296875 KBps

                    //WriteAllBytes 0001 { { size = 305319 } }
                    //WriteAllBytes 0001 { { ElapsedMilliseconds = 12 } }

                    div0001.style.backgroundColor = "yellow";
                    div0001.style.SetSize(4096, 4096);


                    IHTMLImage c0001 = div0001;

                    IHTMLImage c = new IHTMLDiv { "lets render this div into file " + new { DateTime.Now } };

                    c.AttachToDocument();




                    // https://code.google.com/p/chromium/issues/detail?id=322952


                    new IHTMLButton { "read" }.AttachToDocument().onclick += async delegate
                    {
                        new IHTMLHorizontalRule { }.AttachToDocument();

                        var dir = (DirectoryEntry)await chrome.fileSystem.chooseEntry(new { type = "openDirectory" });


                        var files = await dir.createReader().readFileEntries();


                        foreach (var item in files)
                        {
                            var item0 = item;

                            new IHTMLButton { new { item0.name } }.AttachToDocument().onclick += async delegate
                            {
                                var f = await item0.file();

                                new IHTMLPre { new { f.name, f.size } }.AttachToDocument();

                                Blob imgblob = await f.readAsBytes();

                                new IHTMLImage { src = imgblob.ToObjectURL() }.AttachToDocument();


                            };

                        }
                    };



                    new IHTMLButton { "render c0001 and write to 0001.png" }.AttachToDocument().onclick += async delegate
                    {
                        new IHTMLHorizontalRule { }.AttachToDocument();

                        var dir = (DirectoryEntry)await chrome.fileSystem.chooseEntry(new { type = "openDirectory" });
                        // https://developer.chrome.com/apps/fileSystem#method-retainEntry


                        dir.getDirectory("shader0", new { create = true },
                            sub =>
                            {
                                // may want to render each frame as cubemap?

                                //new IHTMLPre { "WriteAllBytes 0002" }.AttachToDocument();
                                sub.WriteAllBytes("0002.png", c0002);

                                //sub.WriteAllBytes()
                            }
                        );

                        // will it make it bigger on disk?
                        //c0001.width = 4096;
                        //c0001.style.width = "4096px";


                        Blob blob0 = c0001;

                        new IHTMLPre { "WriteAllBytes 0001 " + new { blob0.size } }.AttachToDocument();

                        var w0 = Stopwatch.StartNew();

                        await dir.WriteAllBytes("0001.png", blob0);

                        //WriteAllBytes 0001 { { size = 308915 } }
                        //WriteAllBytes 0001 { { ElapsedMilliseconds = 103, KBps = 2928.881598907767 } }

                        //WriteAllBytes 0001 { { size = 995 } }
                        //WriteAllBytes 0001 { { ElapsedMilliseconds = 9 } }

                        var KBps = blob0.size / (w0.ElapsedMilliseconds / 1000.0) / 1024;

                        new IHTMLPre { "WriteAllBytes 0001 " + new { w0.ElapsedMilliseconds, KBps } }.AttachToDocument();



                        new IHTMLPre { "done" }.AttachToDocument();

                        //new IHTMLButton { "read" }.AttachToDocument().onclick += async delegate
                        new IHTMLButton { "read" }.AttachToDocument().onclick += delegate
                     {
                         new IHTMLHorizontalRule { }.AttachToDocument();

                         //var dir = (DirectoryEntry)await chrome.fileSystem.chooseEntry(new { type = "openDirectory" });


                         // https://developer.mozilla.org/en/docs/Web/API/DirectoryReader

                         // first. can we read back the files we just wrote?
                         // first step is to make sure we even have a api

                         //var createReader = (dir as dynamic).createReader();
                         //object createReader = (dir as dynamic).createReader();
                         DirectoryReader createReader = (dir as dynamic).createReader();

                         new IHTMLPre { createReader }.AttachToDocument();

                         // [object DirectoryReader]

                         // [object FileEntry],[object FileEntry]

                         new IHTMLPre { "readEntries 0" }.AttachToDocument();

                         createReader.readEntries(
                             entries =>
                             {
                                 //new IHTMLPre { "readEntries 0 done " + new { entries } }.AttachToDocument();
                                 new IHTMLPre { "readEntries 0 done " }.AttachToDocument();

                                 foreach (var item in entries)
                                 {
                                     new IHTMLPre { "readEntries 0 done " + new { item } }.AttachToDocument();
                                 }

                                 new IHTMLPre { "readEntries 1" }.AttachToDocument();

                                 //                                       readEntries 0
                                 //readEntries 0 done { { entries = [object FileEntry],[object FileEntry] }}
                                 //readEntries 1
                                 //readEntries 1 done { { entries1 =  } }

                                 createReader.readEntries(
                               entries1 =>
                               {
                                   // readEntries 1 done {{ entries1isnull = false }}

                                   var entries1isnull = entries1 == null;

                                   new IHTMLPre { "readEntries 1 done " + new { entries1isnull } }.AttachToDocument();


                                   new IHTMLPre { "readEntries 1 done " + new { entries1.Length } }.AttachToDocument();
                                   //new IHTMLPre { "readEntries 1 done " + new { entries1 } }.AttachToDocument();

                               }
                           );


                             }
                         );




                         //9240ms { Name = createReader, ReturnType = , IsReturnVoid = false, Count = 1 }
                         //view - source:54032 9240ms { target = [object DirectoryEntry], Name = createReader }
                         //view - source:54032 9240ms { Name = Add, ReturnType = , IsReturnVoid = true, Count = 2 }
                         //view - source:54032 9245ms { target = [object HTMLPreElement], Name = Add, arg1 = [object DirectoryReader] }
                         //view - source:73991 Uncaught TypeError: Cannot read property 'apply' of undefined


                         //await dir.WriteAllBytes("0001.png", c0001);
                         //await dir.WriteAllBytes("0002.png", c0002);

                         //new IHTMLPre { "done" }.ToString();
                     };
                    };



                }
            );
        }

    }
}


//Severity Code    Description Project File Line
//Warning CS1998  This async method lacks 'await' operators and will run synchronously.Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.	ChromeReadFiles Z:\jsc.svn\examples\javascript\chrome\apps\ChromeReadFiles\Application.cs   100
//Warning CS4014  Because this call is not awaited, execution of the current method continues before the call is completed.Consider applying the 'await' operator to the result of the call.	ChromeReadFiles Z:\jsc.svn\examples\javascript\chrome\apps\ChromeReadFiles\Application.cs   185

