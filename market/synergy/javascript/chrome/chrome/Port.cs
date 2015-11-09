extern alias xglobal;


using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace chrome
{

    // name clash?
    public class PortMessageEvent
    {
        public object data;

        public static implicit operator bool (PortMessageEvent e)
        {
            // future C# may allow if (obj)
            // but for now booleans are needed

            // enable 
            // while (await Native.window.async.onresize);
            return ((object)e != null);
        }
    }


    public class Port_async
    {
        // https://github.com/darwin/chromium-src-chrome-browser/blob/master/extensions/page_action_controller.h
        // https://github.com/darwin/chromium-src-chrome-browser/blob/master/extensions/page_action_controller.cc

        public Port that_port;

        public Task<PortMessageEvent> onmessage
        {
            get
            {
                // tested by
                // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionWithWorker\ChromeExtensionWithWorker\Application.cs

                var c = new TaskCompletionSource<PortMessageEvent>();


                ((xglobal::chrome.Port)(object)that_port).onMessage.addListener(
                    new Action<object>(
                        (message) =>
                        {
                            if (c == null)
                                return;


                            c.SetResult(
                                new PortMessageEvent { data = message }
                                );

                            c = null;
                        }
                    )
                );



                return c.Task;
            }
        }

    }



    public class Port
    {
        // Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCaptureAE\Application.cs

        // https://github.com/darwin/chromium-src-chrome-browser/blob/master/extensions/extension_tab_util.cc
        // https://github.com/darwin/chromium-src-chrome-browser/blob/master/extensions/extension_tab_util_android.cc

        // https://github.com/darwin/chromium-src-chrome-browser/blob/master/extensions/api/tabs/tabs_api.cc
        // https://github.com/darwin/chromium-src-chrome-browser/blob/master/extensions/api/tabs/tabs_api.h

        // extensions for android in 2015?


        // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionWithWorker\ChromeExtensionWithWorker\Application.cs

        public Port_async async
        {
            [method: Script(DefineAsStatic = true)]
            get
            {
                return new Port_async { that_port = this };
            }
        }
    }
}
