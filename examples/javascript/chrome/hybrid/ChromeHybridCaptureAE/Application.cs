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
                // verify

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

                Console.WriteLine("running as app " + new { typeof(Application).Assembly.GetName().Name } + " now reenable extension..");

                chrome.app.runtime.Launched += async delegate
                {
                    Console.WriteLine("app Launched");
                };



                return;
            }

            Console.WriteLine("running as content?");
        }

    }
}
