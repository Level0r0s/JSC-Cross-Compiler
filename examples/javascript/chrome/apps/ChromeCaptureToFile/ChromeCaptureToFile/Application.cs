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
using ChromeCaptureToFile;
using ChromeCaptureToFile.Design;
using ChromeCaptureToFile.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;

namespace ChromeCaptureToFile
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeCaptureToFile\ChromeCaptureToFile\bin\Debug\staging\ChromeCaptureToFile.Application\web

        //        subst /D b:
        //subst b: s:\jsc.svn\examples\javascript\chrome\apps\ChromeCaptureToFile\ChromeCaptureToFile\bin\Debug\staging\ChromeCaptureToFile.Application\web

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

                        new chrome.Notification(title: "ChromeCaptureToFile");

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

            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeCaptureToFile\ChromeCaptureToFile\Application.cs

            //{{ Length = 4 }}
            //{{ prefixLength = 64, name = {D7020941-742E-4570-93B2-C0372D3D870F}, address = fe80::88c0:f0a:9ccf:cba0 }}
            //{{ prefixLength = 24, name = {D7020941-742E-4570-93B2-C0372D3D870F}, address = 192.168.43.28 }}
            //{{ prefixLength = 64, name = {A8657A4E-8BFA-41CC-87BE-6847E33E1A81}, address = 2001:0:9d38:6abd:20a6:2815:3f57:d4e3 }}
            //{{ prefixLength = 64, name = {A8657A4E-8BFA-41CC-87BE-6847E33E1A81}, address = fe80::20a6:2815:3f57:d4e3 }}

            ;

            new { }.With(
                 //async delegate
                 delegate
                 {
                     // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822

                     // http://css-infos.net/property/-webkit-user-select
                     // http://caniuse.com/#feat=user-select-none
                     //(Native.body.style as dynamic).userSelect = "auto";
                     (Native.body.style as dynamic).webkitUserSelect = "text";

                     // https://css-tricks.com/almanac/properties/u/user-select/
                     //Native.body.style.setProperty(
                     // X:\jsc.svn\examples\java\hybrid\JVMCLRNIC\JVMCLRNIC\Program.cs
                     // clr does not have it async. 



                     // here we need to open a web app, capture it and save it to file.


                     //var uri = "http://discover.xavalon.net";
                     var uri = "http://example.com";


                     //uri.ToDocumentTitle();


                     // https://groups.google.com/a/chromium.org/forum/#!topic/chromium-apps/5JfAdZg9mzY


                     // http://developer.chrome.com/apps/webview_tag.html
                     // http://stackoverflow.com/questions/16635739/google-chrome-app-webview-behavior

                     // 20140911 yes it does still work with a AppWindow frame.
                     // what about without frame?

                     new IHTMLButton { "webview" }.AttachToDocument().onclick += async delegate
                     {
                         //var webview = Native.document.createElement("webview");
                         //// You do not have permission to use <webview> tag. Be sure to declare 'webview' permission in your manifest. 
                         //webview.setAttribute("partition", "p1");
                         //webview.setAttribute("src", uri);
                         //webview.style.SetLocation(0, 128);
                         //webview.style.width = "512";
                         //webview.style.height = "512";

                         //webview.AttachToDocument();

                         ////var captureVisibleRegion = (webview as dynamic).captureVisibleRegion;

                         //// would be nice if it existed. does not huh.
                         //// {{ captureVisibleRegion = null }}
                         //var captureVisibleRegion = (IFunction)((dynamic)webview).captureVisibleRegion;
                         //new IHTMLDiv { new { captureVisibleRegion } }.AttachToDocument();

                         // webview wont do extensions?

                         // https://developer.chrome.com/apps/app_external

                         // Google Code will be turning read-only on August 25th. See this post for more information.


                         // Unchecked runtime.lastError while running app.window.create: The URL used for window creation must be local for security reasons.
                         //   var xappwindow = await chrome.app.window.create(
                         //        "http://example.com", options: null
                         //);

                         // TypeError: Cannot read property 'create' of undefined at
                         //var tab = await chrome.tabs.create(new { url = uri });

                         // can our extension see it?
                         //var w = Native.window.open(uri, "_blank", 512, 512);

                         // http://stackoverflow.com/questions/12711997/open-window-without-toolbars
                         //var w = Native.window.open(uri, target: "_blank", features: "resizable=no, toolbar=no, scrollbars=no, menubar=no, status=no, directories=no, width=512");
                         var w = Native.window.open(uri);


                         // ok now we  have a regular chrome window somewhere.
                         // if we hop to our extension, can we augment it?




                         //IHTMLImage c0002 = new IHTMLDiv { "frame2 " + new { DateTime.Now } };
                         //IHTMLImage c0001 = new IHTMLDiv { "frame1 " + new { DateTime.Now } };


                         //new IHTMLButton { "openDirectory" }.AttachToDocument().onclick += async delegate
                         //   {
                         //       new IHTMLHorizontalRule { }.AttachToDocument();

                         //       var dir = (DirectoryEntry)await chrome.fileSystem.chooseEntry(new { type = "openDirectory" });

                         //       // write webview?
                         //       await dir.WriteAllBytes("0001.png", c0001);
                         //       await dir.WriteAllBytes("0002.png", c0002);

                         //       new IHTMLPre { "done" }.ToString();
                         //   };


                     };

                 }
            );


            ;

        }

    }
}

//0200004f ChromeCaptureToFile.Application+<>c+<<-ctor>b__0_2>d+<MoveNext>0600002b
//script: error JSC1000:
//error:
//  statement cannot be a load instruction(or is it a bug?)
//  [0x0000]
//ldarg.0    +1 -0

// assembly: W:\ChromeCaptureToFile.Application.exe
// type: ChromeCaptureToFile.Application+<>c+<<-ctor>b__0_2>d+<MoveNext>0600002b, ChromeCaptureToFile.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// offset: 0x0000
//  method:Int32<0148> ldsfld.try(<MoveNext>0600002b)