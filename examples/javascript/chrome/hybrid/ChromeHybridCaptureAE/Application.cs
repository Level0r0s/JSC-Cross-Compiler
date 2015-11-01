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
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace ChromeHybridCaptureAE
{

    #region HopToChromeExtension
    // Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCapture\ChromeHybridCapture\Application.cs
    public struct HopToChromeExtension : System.Runtime.CompilerServices.INotifyCompletion
    {
        public static Action<HopToChromeExtension, Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation)
        {

            VirtualOnCompleted(this, continuation);
        }

        //Error   CS1929	'HopToChromeExtension' does not contain a definition for 'GetAwaiter' and the best extension method overload 'IXMLHttpRequestAsyncExtensions.GetAwaiter(IXMLHttpRequest)' requires a receiver of type 'IXMLHttpRequest'	ChromeHybridCapture Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCapture\ChromeHybridCapture\Application.cs	404	IntelliSense

        public HopToChromeExtension GetAwaiter() { return this; }



        //Error   CS0117	'HopToChromeExtension' does not contain a definition for 'IsCompleted'	ChromeHybridCapture Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCapture\ChromeHybridCapture\Application.cs	409	IntelliSense
        public bool IsCompleted { get { return false; } }


        //Error CS0117	'HopToChromeExtension' does not contain a definition for 'GetResult'	ChromeHybridCapture Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCapture\ChromeHybridCapture\Application.cs	414	IntelliSense
        public void GetResult() { }

    }
    #endregion


    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow
        static Func<string, string> DecoratedString =
             x => x.Replace("-", "_").Replace("+", "_").Replace("<", "_").Replace(">", "_");

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
                                    Console.WriteLine("extension chrome.runtime.connect done, click launch");



                                    // is async better than event += here?
                                    var m = default(chrome.PortMessageEvent);
                                    while (m = await port.async.onmessage)
                                    {
                                        var message = m.data;

                                        // verified.

                                        #region IAsyncStateMachine
                                        // casting from anonymous object.
                                        var xShadowIAsyncStateMachine = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)message;

                                        // 12961ms extension  port.onMessage {{ state = null, TypeName = null }}
                                        // or constructor id?
                                        Console.WriteLine("extension  port.onMessage " + new { xShadowIAsyncStateMachine.state, xShadowIAsyncStateMachine.TypeName });


                                        // 12468ms extension  port.onMessage {{ message = do HopToChromeExtension {{ TypeName = <Namespace>.___ctor_b__4_9_d, state = 0 }}, expando_isstring = true, is_string = false, equals_typeofstring = false }}
                                        //2015-08-22 15:49:45.729 view-source:53670 12471ms extension  port.onMessage {{ message = do HopToChromeExtension {{ TypeName = <Namespace>.___ctor_b__4_9_d, state = 0 }} }}
                                        //2015-08-22 15:49:45.733 view-source:53670 12475ms extension  port.onMessage {{ message = [object Object], expando_isstring = false, is_string = false, equals_typeofstring = false }}
                                        //2015-08-22 15:49:45.737 view-source:53670 12479ms extension  port.onMessage {{ state = 0, TypeName = <Namespace>.___ctor_b__4_9_d }}


                                        #region xAsyncStateMachineType
                                        var xAsyncStateMachineType = typeof(Application).Assembly.GetTypes().FirstOrDefault(
                                            xAsyncStateMachineTypeCandidate =>
                                            {
                                                // safety check 1

                                                //Console.WriteLine(new { sw.ElapsedMilliseconds, item.FullName });

                                                var xisIAsyncStateMachine = typeof(IAsyncStateMachine).IsAssignableFrom(xAsyncStateMachineTypeCandidate);
                                                if (xisIAsyncStateMachine)
                                                {
                                                    //Console.WriteLine(new { item.FullName, isIAsyncStateMachine });

                                                    return xAsyncStateMachineTypeCandidate.FullName == xShadowIAsyncStateMachine.TypeName;
                                                }

                                                return false;
                                            }
                                        );
                                        #endregion

                                        Console.WriteLine(new { xAsyncStateMachineType });

                                        var NewStateMachine = FormatterServices.GetUninitializedObject(xAsyncStateMachineType);
                                        var isIAsyncStateMachine = NewStateMachine is IAsyncStateMachine;

                                        var NewStateMachineI = (IAsyncStateMachine)NewStateMachine;

                                        #region 1__state
                                        xAsyncStateMachineType.GetFields(
                                          System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                                          ).WithEach(
                                           AsyncStateMachineSourceField =>
                                           {

                                               //Console.WriteLine(new { AsyncStateMachineSourceField });

                                               if (AsyncStateMachineSourceField.Name.EndsWith("1__state"))
                                               {
                                                   AsyncStateMachineSourceField.SetValue(
                                                       NewStateMachineI,
                                                       xShadowIAsyncStateMachine.state
                                                    );
                                               }


                                           }
                                      );
                                        #endregion

                                        NewStateMachineI.MoveNext();
                                        #endregion

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

                #region ConnectExternal
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


                    #region HopToChromeExtension
                    HopToChromeExtension.VirtualOnCompleted = async (that, continuation) =>
                    {
                        // state 0 ? or state -1 ?
                        Console.WriteLine("app HopToChromeExtension enter ");

                        // where is it defined?
                        // z:\jsc.svn\examples\javascript\async\Test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\ShadowIAsyncStateMachine.cs

                        // async dont like ref?
                        TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableContinuation r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);


                        // now send the jump instruction... will it make it?
                        port.postMessage(r.shadowstate);


                        // how would we know to continue from current continuation?
                        // or are we fine to rebuild the scope if we jump back?
                    };
                    #endregion



                    var m = default(chrome.PortMessageEvent);
                    while (m = await port.async.onmessage)
                    {
                        //var m = await port.async.onmessage;

                        Console.WriteLine("app  port.onMessage invoke "
                        //+ new { xShadowIAsyncStateMachine.state, xShadowIAsyncStateMachine.TypeName }
                        );
                    }
                };
                #endregion




                // nuget chrome
                chrome.app.runtime.Launched += async delegate
                {
                    fixup: await Task.CompletedTask;



                    Console.WriteLine("app Launched, will hop to extension");
                    // verified.


                    await default(HopToChromeExtension);

                    Console.WriteLine("hop from app to extension");
                    // verify

                };


                return;
            }

            Console.WriteLine("running as content?");
        }

    }
}
