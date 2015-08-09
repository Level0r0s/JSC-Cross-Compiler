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
using ChromeWriteFiles;
using ChromeWriteFiles.Design;
using ChromeWriteFiles.HTML.Pages;

namespace ChromeWriteFiles
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeWriteFiles\ChromeWriteFiles\bin\Debug\staging\ChromeWriteFiles.Application\web

        //        subst /D b:
        //subst b: s:\jsc.svn\examples\javascript\chrome\apps\ChromeWriteFiles\ChromeWriteFiles\bin\Debug\staging\ChromeWriteFiles.Application\web

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

                        new chrome.Notification(title: "ChromeWriteFiles");

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

            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeWriteFiles\ChromeWriteFiles\Application.cs

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

                    // https://css-tricks.com/almanac/properties/u/user-select/
                    //Native.body.style.setProperty(
                    // X:\jsc.svn\examples\java\hybrid\JVMCLRNIC\JVMCLRNIC\Program.cs
                    // clr does not have it async. 

                    var refresh = new IHTMLButton { "refresh" }.AttachToDocument();

                    while (await refresh.async.onclick)
                    {
                        new IHTMLHorizontalRule { }.AttachToDocument();

                        // TypeError: Cannot read property 'getVolumeList' of undefined
                        //var n = await chrome.fileSystem.getVolumeList();


                        //{
                        //    "fileSystem" : ["write", "directory"]
                        //    }
                        // TypeError: Cannot read property 'chooseEntry' of undefined
                        var entry = await chrome.fileSystem.chooseEntry(
                                            new { type = "openDirectory" }
                                        );

                        // {{ entry = [object DirectoryEntry] }}
                        new IHTMLPre { new { entry } }.AttachToDocument();

                        // http://sharpkit.net/help/SharpKit.PhoneGap/SharpKit.PhoneGap/DirectoryEntry/getFile(JsString,Flags,FileEntry%5D%5D,JsAction).htm
                        // https://developer.mozilla.org/en-US/docs/Web/API/DirectoryEntry#getFile
                        var dir = (DirectoryEntry)entry;

                        dir.getFile(
                            "0000.jpg",
                            new
                            {
                                create = true,
                                exclusive = false
                            },
                            fentry =>
                            {
                                // {{ fentry = [object FileEntry] }}
                                new IHTMLPre { new { fentry } }.AttachToDocument();


                                fentry.createWriter(
                                    w =>
                                    {
                                        new IHTMLPre { new { w } }.AttachToDocument();

                                        // new Blob([document.getElementById("HTMLFile").value],
                                        //{ type: 'text/plain'}

                                        var blob = new Blob(
                                            e: new[] { "hello" },
                                            args: new { type = "text/plain" }
                                        );

                                        // http://stackoverflow.com/questions/12168909/blob-from-dataurl
                                        w.write(blob);


                                        //w.write()
                                    }
                                );

                            }
                        );
                        //foreach (var item in n)
                        //{
                        //    new IHTMLPre { new { item, item.volumeId, item.writable } }.AttachToDocument();
                        //}
                    }

                }
            );
        }

    }
}
