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
using ChromeHybridCaptureAE;
using ChromeHybridCaptureAE.Design;
using ChromeHybridCaptureAE.HTML.Pages;

namespace ChromeHybridCaptureAE
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
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151101

            // first lets do some chrome extension magic.

            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;



            #region self_chrome_tabs
            object self_chrome_tabs = self_chrome.tabs;
            if (self_chrome_tabs != null)
            {
                Console.WriteLine("running as extension " + new { typeof(Application).Assembly.GetName().Name });
                //  verified.
                // running as extension {{ Name = ChromeHybridCaptureAE.Application }}

                new { }.With(
                     async delegate
                     {
                         var app = (await chrome.management.getAll()).FirstOrDefault(item => item.name.StartsWith(typeof(Application).Assembly.GetName().Name));

                         if (app == null)
                         {
                             Console.WriteLine("running as extension. app not found.");
                             return;
                         }

                         // running as extension {{ shortName = ChromeHybridCaptureAE.Application.exe, launchType = OPEN_AS_WINDOW }}
                         Console.WriteLine("running as extension " + new { app.shortName, app.launchType });

                         chrome.runtime.connect(app.id).With(
                                async (chrome.Port port) =>
                                {
                                    //Console.WriteLine("extension chrome.runtime.connect done " + new { port.name, port.sender.id });
                                    //Console.WriteLine("extension chrome.runtime.connect done " + new { port.name, port.sender });
                                    Console.WriteLine("extension chrome.runtime.connect done");

                                    var m = default(chrome.PortMessageEvent);
                                    while (m = await port.async.onmessage)
                                    {
                                        Console.WriteLine("extension  port.onMessage invoke "
                                            //+ new { xShadowIAsyncStateMachine.state, xShadowIAsyncStateMachine.TypeName }
                                            );
                                    }
                                }
                          );
                     }
                );

                return;
            }
            #endregion

            object self_chrome_socket = self_chrome.socket;
            if (self_chrome_socket != null)
            {
                if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
                {
                    Console.WriteLine("running as appwindow");
                    return;
                }

                // running as app {{ Name = ChromeHybridCaptureAE.Application }} now reenable extension..
                Console.WriteLine("running as app " + new { typeof(Application).Assembly.GetName().Name } + " now reenable extension..");

                // nuget chrome
                chrome.app.runtime.Launched += async delegate
                {
                    Console.WriteLine("app Launched");
                    // verified.

                };

                chrome.runtime.ConnectExternal += async port =>
                {
                    // app chrome.runtime.ConnectExternal {{ id = jadmeogmbokffpkdfeiemjplohfgkidd }} now click launch!
                    Console.WriteLine("app chrome.runtime.ConnectExternal " + new { port.sender.id } + " now click launch!");

                    new chrome.Notification(
                        title: typeof(Application).Assembly.GetName().Name,
                        message: "ready. click launch"
                    ).Clicked += delegate
                    {
                        // https://developer.chrome.com/apps/app_runtime

                        // management_api
                    };

                    // can we have async thing here?


                    var m = default(chrome.PortMessageEvent);
                    while (m = await port.async.onmessage)
                    {
                        //var m = await port.async.onmessage;

                        Console.WriteLine("app  port.onMessage invoke "
                        //+ new { xShadowIAsyncStateMachine.state, xShadowIAsyncStateMachine.TypeName }
                        );
                    }
                };



                return;
            }

            Console.WriteLine("running as content?");
        }

    }
}
