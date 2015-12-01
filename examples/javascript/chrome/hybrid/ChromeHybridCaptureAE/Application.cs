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
    using chrome;

    #region HopToChromeAppWindow
    public struct HopToChromeAppWindow : System.Runtime.CompilerServices.INotifyCompletion
    {
        public chrome.AppWindow window;


        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150905/hoptoudpchromeapp
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeapp

        public static Action<HopToChromeAppWindow, Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation)
        {

            VirtualOnCompleted(this, continuation);
        }

        //Error   CS1929	'HopToChromeExtension' does not contain a definition for 'GetAwaiter' and the best extension method overload 'IXMLHttpRequestAsyncExtensions.GetAwaiter(IXMLHttpRequest)' requires a receiver of type 'IXMLHttpRequest'	ChromeHybridCapture Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCapture\ChromeHybridCapture\Application.cs	404	IntelliSense

        public HopToChromeAppWindow GetAwaiter() { return this; }



        //Error   CS0117	'HopToChromeExtension' does not contain a definition for 'IsCompleted'	ChromeHybridCapture Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCapture\ChromeHybridCapture\Application.cs	409	IntelliSense
        public bool IsCompleted { get { return false; } }


        //Error CS0117	'HopToChromeExtension' does not contain a definition for 'GetResult'	ChromeHybridCapture Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCapture\ChromeHybridCapture\Application.cs	414	IntelliSense
        public void GetResult() { }

    }
    #endregion

    #region HopToChromeApp
    // Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCapture\ChromeHybridCapture\Application.cs
    public struct HopToChromeApp : System.Runtime.CompilerServices.INotifyCompletion
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeapp

        public static Action<HopToChromeApp, Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation)
        {

            VirtualOnCompleted(this, continuation);
        }

        //Error   CS1929	'HopToChromeExtension' does not contain a definition for 'GetAwaiter' and the best extension method overload 'IXMLHttpRequestAsyncExtensions.GetAwaiter(IXMLHttpRequest)' requires a receiver of type 'IXMLHttpRequest'	ChromeHybridCapture Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCapture\ChromeHybridCapture\Application.cs	404	IntelliSense

        public HopToChromeApp GetAwaiter() { return this; }



        //Error   CS0117	'HopToChromeExtension' does not contain a definition for 'IsCompleted'	ChromeHybridCapture Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCapture\ChromeHybridCapture\Application.cs	409	IntelliSense
        public bool IsCompleted { get { return false; } }


        //Error CS0117	'HopToChromeExtension' does not contain a definition for 'GetResult'	ChromeHybridCapture Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCapture\ChromeHybridCapture\Application.cs	414	IntelliSense
        public void GetResult() { }

    }
    #endregion




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
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151126
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow
        static Func<string, string> DecoratedString =
             x => x.Replace("-", "_").Replace("+", "_").Replace("<", "_").Replace(">", "_");



        // known at the extension
        static chrome.ExtensionInfo app;

        // lets have at least one window
        static chrome.AppWindow outputWindow;



        static IHTMLInput frameID;
        static IHTMLInput uri;
        static IHTMLButton go;



        static int index = 0;
        static DirectoryEntry dir;

        public Application(IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151101

            // first lets do some chrome extension magic.

            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;



            #region self_chrome_tabs extension
            object self_chrome_tabs = self_chrome.tabs;
            if (self_chrome_tabs != null)
            {
                Console.WriteLine("running as extension " + new { typeof(Application).Assembly.GetName().Name });
                //  verified.
                // running as extension {{ Name = ChromeHybridCaptureAE.Application }}

                new { }.With(
                     async delegate
                     {
                         app = (await chrome.management.getAll()).FirstOrDefault(item => item.name.StartsWith(typeof(Application).Assembly.GetName().Name));

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



                                #region extension: HopToChromeApp.VirtualOnCompleted
                                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeapp
                                HopToChromeApp.VirtualOnCompleted = async (that, continuation) =>
                                {
                                    // state 0 ? or state -1 ?
                                    Console.WriteLine("extension HopToChromeApp enter ");

                                    TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableContinuation r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);

                                    // now send the jump instruction... will it make it?
                                    port.postMessage(r.shadowstate);
                                };
                                #endregion



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

                                            var xStringField = TestSwitchToServiceContextAsync.ArrayListExtensions.AsEnumerable(xShadowIAsyncStateMachine.StringFields).FirstOrDefault(
                                               f => DecoratedString(f.FieldName) == DecoratedString(AsyncStateMachineSourceField.Name)
                                           );

                                            if (xStringField != null)
                                            {
                                                // once we are to go back to client. we need to reverse it?

                                                AsyncStateMachineSourceField.SetValue(
                                                    NewStateMachineI,
                                                    xStringField.value
                                                 );
                                                // next xml?
                                                // before lets send our strings back with the new state!
                                                // what about exceptions?
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
                #region running as appwindow
                if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
                {
                    Console.WriteLine("running as appwindow");


                    MessagePort appwindow_to_app2 = null;

                    // called by? 619  app:HopToChromeAppWindow
                    #region  appwindow Native.window.onmessage
                    Native.window.onmessage += e =>
                    {
                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow

                        // appwindow Native.window.onmessage {{ data = app to appwindow! }}

                        var message = e.data;

                        //Console.WriteLine("appwindow Native.window.onmessage " + new { e.data });


                        // extension  port.onMessage {{ message = from app hello to extension }}
                        //var expando_isstring = ScriptCoreLib.JavaScript.Runtime.Expando.Of(message).IsString;

                        // look app sent a message to extension
                        //Console.WriteLine("app  port.onMessage " + new { message });

                        {
                            // DataCloneError: Failed to execute 'postMessage' on 'Window': Port at index 0 is already neutered.

                            if (e.ports != null)
                                foreach (var port in e.ports)
                                {
                                    Console.WriteLine("appwindow    Native.window.onmessage " + new { port });

                                    appwindow_to_app2 = port;
                                }

                        }

                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeapp




                        // casting from anonymous object.
                        var xShadowIAsyncStateMachine = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)message;

                        // or constructor id?
                        Console.WriteLine("appwindow     Native.window.onmessage " + new { xShadowIAsyncStateMachine.state, xShadowIAsyncStateMachine.TypeName });

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

                               // X:\jsc.svn\examples\javascript\async\Test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\CLRWebServiceInvoke.cs
                               // field names/ tokens need to be encrypted like typeinfo.

                               // some do manual restore
                               // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionHopToTabThenIFrame\ChromeExtensionHopToTabThenIFrame\Application.cs
                               // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow
                               // or, are we supposed to initialize a string value here?

                               var xStringField = TestSwitchToServiceContextAsync.ArrayListExtensions.AsEnumerable(xShadowIAsyncStateMachine.StringFields).FirstOrDefault(
                                   f => DecoratedString(f.FieldName) == DecoratedString(AsyncStateMachineSourceField.Name)
                               );

                               if (xStringField != null)
                               {
                                   // once we are to go back to client. we need to reverse it?

                                   AsyncStateMachineSourceField.SetValue(
                                       NewStateMachineI,
                                       xStringField.value
                                    );
                                   // next xml?
                                   // before lets send our strings back with the new state!
                                   // what about exceptions?
                               }
                           }
                      );
                        #endregion

                        NewStateMachineI.MoveNext();

                    };
                    #endregion


                    #region appwindow:HopToChromeApp
                    HopToChromeApp.VirtualOnCompleted = async (that, continuation) =>
                    {
                        // do we have the port to send back our portal warp?

                        // state 0 ? or state -1 ?
                        Console.WriteLine("appwindow HopToChromeApp  enter " + new { appwindow_to_app2 });
                        // appwindow HopToChromeApp  enter {{ appwindow_to_app2 = null }}

                        //// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow
                        //// async dont like ref?
                        TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableContinuation r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);
                        // 29035ms extension  port.onMessage {{ message = do HopToChromeExtension }}
                        //appwindow_to_app.postMessage("do HopToChromeAppWindow " + new { r.shadowstate.TypeName, r.shadowstate.state });
                        // now send the jump instruction... will it make it?
                        appwindow_to_app2.postMessage(r.shadowstate);
                    };
                    #endregion

                    return;
                }
                #endregion


                // running as app {{ Name = ChromeHybridCaptureAE.Application }} now reenable extension..
                Console.WriteLine("running as app " + new { typeof(Application).Assembly.GetName().Name } + " now reenable extension..");



                #region app:appwindow_to_app
                Action<object> appwindow_to_app = data =>
                {
                    var xShadowIAsyncStateMachine = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)data;

                    Console.WriteLine("app appwindow_to_app " + new { xShadowIAsyncStateMachine.TypeName });

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

                    Console.WriteLine("app appwindow_to_app " + new { xAsyncStateMachineType });

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

                           // X:\jsc.svn\examples\javascript\async\Test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\CLRWebServiceInvoke.cs
                           // field names/ tokens need to be encrypted like typeinfo.

                           // some do manual restore
                           // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionHopToTabThenIFrame\ChromeExtensionHopToTabThenIFrame\Application.cs
                           // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow
                           // or, are we supposed to initialize a string value here?

                           var xStringField = TestSwitchToServiceContextAsync.ArrayListExtensions.AsEnumerable(xShadowIAsyncStateMachine.StringFields).FirstOrDefault(
                               f => DecoratedString(f.FieldName) == DecoratedString(AsyncStateMachineSourceField.Name)
                           );

                           if (xStringField != null)
                           {
                               // once we are to go back to client. we need to reverse it?

                               AsyncStateMachineSourceField.SetValue(
                                   NewStateMachineI,
                                   xStringField.value
                                );
                               // next xml?
                               // before lets send our strings back with the new state!
                               // what about exceptions?
                           }
                       }
                  );
                    #endregion

                    NewStateMachineI.MoveNext();
                };
                #endregion

                var c = new MessageChannel();
                var cneutered = c;
                c.port1.onmessage += e =>
                {
                    Console.WriteLine("app HopToChromeAppWindow MessageChannel onmessage " + new { e.data });

                    appwindow_to_app(e.data);
                };

                c.port1.start();
                c.port2.start();

                #region app:HopToChromeAppWindow
                HopToChromeAppWindow.VirtualOnCompleted = async (that, continuation) =>
                {
                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150824/webgliframebuffer

                    // state 0 ? or state -1 ?
                    Console.WriteLine("app HopToChromeAppWindow  enter ");

                    #region outputWindow
                    if (that.window == null)
                    {
                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow


                        if (outputWindow == null)
                        {
                            // https://developer.chrome.com/apps/app_window#type-CreateWindowOptions
                            outputWindow = await chrome.app.window.create(
                                                   Native.document.location.pathname,

                                                   // https://developer.chrome.com/apps/app_window#type-CreateWindowOptions
                                                   // this ctually works. but we wont see console on app log..
                                                   options: new { hidden = true, alwaysOnTop = true }
                                            );

                            ////xappwindow.setAlwaysOnTop

                            // or can we stay hidden?
                            //that.window.show();

                            await outputWindow.contentWindow.async.onload;
                        }
                        // reuse the window...
                        that.window = outputWindow;
                    }
                    #endregion


                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow
                    // async dont like ref?
                    TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableContinuation r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);
                    // 29035ms extension  port.onMessage {{ message = do HopToChromeExtension }}



                    // Z:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IWindow.postMessage.cs
                    // how do we use this thing?



                    //                    15ms appwindow    Native.window.onmessage: {{ ports = [object MessagePort] }}
                    //2015-08-22 20:50:18.019 view-source:53702 17ms appwindow    Native.window.onmessage: {{ port = [object MessagePort] }}
                    //that.window.contentWindow.postMessage("do HopToChromeAppWindow " + new { r.shadowstate.TypeName, r.shadowstate.state }, transfer: c.port2);

                    // now send the jump instruction... will it make it?
                    if (cneutered != null)
                    {
                        that.window.contentWindow.postMessage(r.shadowstate, transfer: cneutered.port2);
                        cneutered = null;
                    }
                    else
                    {
                        that.window.contentWindow.postMessage(r.shadowstate);

                    }
                };
                #endregion

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

                        var message = m.data;

                        #region IAsyncStateMachine
                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeapp
                        // casting from anonymous object.
                        var xShadowIAsyncStateMachine = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)message;

                        // or constructor id?
                        Console.WriteLine("app  port.onMessage " + new { xShadowIAsyncStateMachine.state, xShadowIAsyncStateMachine.TypeName });

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


                               // z:\jsc.svn\examples\javascript\async\Test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\CLRWebServiceInvoke.cs
                               // field names/ tokens need to be encrypted like typeinfo.

                               // some do manual restore
                               // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionHopToTabThenIFrame\ChromeExtensionHopToTabThenIFrame\Application.cs
                               // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow
                               // or, are we supposed to initialize a string value here?

                               var xStringField = TestSwitchToServiceContextAsync.ArrayListExtensions.AsEnumerable(xShadowIAsyncStateMachine.StringFields).FirstOrDefault(
                       f => DecoratedString(f.FieldName) == DecoratedString(AsyncStateMachineSourceField.Name)
                   );

                               if (xStringField != null)
                               {
                                   // once we are to go back to client. we need to reverse it?

                                   AsyncStateMachineSourceField.SetValue(
                           NewStateMachineI,
                           xStringField.value
                        );
                                   // next xml?
                                   // before lets send our strings back with the new state!
                                   // what about exceptions?
                               }
                           }
                      );
                        #endregion

                        NewStateMachineI.MoveNext();
                        #endregion
                    }
                };
                #endregion




                // nuget chrome
                chrome.app.runtime.Launched += async delegate
                {
                    { fixup: await Task.CompletedTask; }



                    Console.WriteLine("app Launched, will hop to extension");
                    // verified.


                    await default(HopToChromeExtension);

                    // https://developer.chrome.com/extensions/management
                    // as an extension we now can inspect our app ?
                    Console.WriteLine("hop from app to extension " + new { app.name });
                    // hop from app to extension {{ name = ChromeHybridCaptureAE.Application.exe }}


                    // what else can we do as an extension?

                    // lets hop back?

                    await default(HopToChromeApp);

                    Console.WriteLine("hop from extension to app ");

                    // ok hopping works. lets do some ui now.
                    //// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow
                    await default(HopToChromeAppWindow);


                    Native.document.documentElement.style.overflow = IStyle.OverflowEnum.auto;
                    //Native.body.style.overflow = IStyle.OverflowEnum.auto;
                    Native.body.Clear();
                    (Native.body.style as dynamic).webkitUserSelect = "text";

                    chrome.app.window.current().show();

                    // now what?

                    new IHTMLPre { "now what?" }.AttachToDocument();

                    // lets have some ui to do a fullsceen tab capture?
                    // http://earth.nullschool.net/#2015/10/31/1500Z/wind/surface/level/anim=off/overlay=temp/azimuthal_equidistant=24.64,98.15,169
                    // http://earth.nullschool.net/#2015/10/31/2100Z/wind/surface/level/anim=off/overlay=temp/azimuthal_equidistant=24.64,98.15,169

                    // TypeError: Cannot set property 'uri' of null
                    // jsc statemachine hop doesnt like scopes yet. staic it is.
                    uri = new IHTMLInput { value = "http://earth.nullschool.net/#2015/10/31/2100Z/wind/surface/level/anim=off/overlay=temp/azimuthal_equidistant=24.64,98.15,169" }.AttachToDocument();
                    uri.style.width = "100%";

                    Console.WriteLine("appwindow: " + new { uri });

                    new IHTMLPre { () => new { index } }.AttachToDocument();

                    frameID = new IHTMLInput
                    {
                        type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range,
                        min = 0,
                        //max = 3600,
                        max = 240,
                        valueAsNumber = 0
                    }.AttachToDocument().With(
                        async i =>
                        {

                            do
                            {
                                // http://earth.nullschool.net/#2015/10/01/0000Z/wind/surface/level/anim=off/overlay=temp/azimuthal_equidistant=24.64,98.15,169
                                index = (int)i.valueAsNumber;

                                var hh = 3 * index;
                                var dd = 1 + Math.Floor(hh / 24.0);

                                uri.value =
                                    "http://earth.nullschool.net/#2015/10/"
                                    + dd.ToString().PadLeft(2, '0')
                                    + "/"
                                    + (hh % 24).ToString().PadLeft(2, '0')
                                    + "00Z/wind/surface/level/anim=off/overlay=temp/azimuthal_equidistant=24.64,98.15,169";

                            }
                            while (await i.async.onchange);
                        }
                    );

                    go = new IHTMLButton { "go" }.AttachToDocument();

                    Console.WriteLine("appwindow: " + new { go });

                    go.onclick += async delegate
                    {
                        //Error CS0158  The label 'fixup' shadows another label by the same name in a contained scope ChromeHybridCaptureAE   Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCaptureAE\Application.cs   791
                        { fixup: await Task.CompletedTask; }

                        Native.body.style.backgroundColor = "yellow";

                        var scope_uri = uri.value;

                        #region  await default(HopToChromeExtension)
                        await default(HopToChromeApp);

                        Console.WriteLine("hop from appwindow to app! " + new { scope_uri });
                        // verify


                        await default(HopToChromeExtension);
                        #endregion


                        Console.WriteLine("hop from app to extension! " + new { scope_uri });

                        //  hop from app to extension! {{ scope_uri = http://earth.nullschool.net/#2015/10/31/2100Z/wind/surface/level/anim=off/overlay=temp/azimuthal_equidistant=24.64,98.15,169 }}

                        var tabwindow = await chrome.windows.create(new { state = "fullscreen", url = scope_uri });

                        // um. unless we hop into it we wont know when its ready?
                        //await Task.Delay(7000);
                        await Task.Delay(5000);

                        // Error: Invalid value for argument 2. Property 'format': Value must be one of: [jpeg, png]. at validate 
                        var captureVisibleTab = await tabwindow.id.captureVisibleTab(options: new { format = "jpeg" });

                        Console.WriteLine("extension captureVisibleTab " + new { captureVisibleTab.Length });

                        await tabwindow.id.remove();


                        #region await default(HopToChromeAppWindow)
                        await default(HopToChromeApp);
                        Console.WriteLine("app " + new { captureVisibleTab.Length });
                        await default(HopToChromeAppWindow);
                        Console.WriteLine("appwindow " + new { captureVisibleTab.Length });
                        #endregion

                        // appwindow {{ Length = 272711 }}

                        var icaptureVisibleTabFull = await new IHTMLImage { src = captureVisibleTab }.async.oncomplete;

                        // {{ width = 1920, height = 1080 }}
                        new IHTMLPre { new { icaptureVisibleTabFull.width, icaptureVisibleTabFull.height } }
                        ;
                        //                        .AttachToDocument();


                        var focus = new CanvasRenderingContext2D(icaptureVisibleTabFull.height, icaptureVisibleTabFull.height);
                        focus.drawImage(icaptureVisibleTabFull, (icaptureVisibleTabFull.width - icaptureVisibleTabFull.height) / 2, 0, icaptureVisibleTabFull.height, icaptureVisibleTabFull.height, 0, 0, icaptureVisibleTabFull.height, icaptureVisibleTabFull.height);
                        var icaptureVisibleTab = new IHTMLImage { src = focus.canvas.toDataURL() }.AttachToDocument();


                        icaptureVisibleTab.style.width = "100%";
                        icaptureVisibleTab.AttachToDocument();

                        await new IHTMLButton { "save " }.AttachToDocument().async.onclick;

                        if (dir == null)
                            dir = (DirectoryEntry)await chrome.fileSystem.chooseEntry(new { type = "openDirectory" });

                        var file = index.ToString().PadLeft(5, '0') + ".jpg";

                        await dir.WriteAllBytes(file, focus);

                        //frameID.valueAsNumber++;

                        await new IHTMLButton { "automate " + new { frameID = frameID.valueAsNumber } }.AttachToDocument().async.onclick;

                        //new IHTMLPre { uri.value }.AttachToDocument();

                        next:

                        frameID.valueAsNumber++;
                        await Native.window.async.onframe;
                        await Native.window.async.onframe;

                        //new IHTMLPre { uri.value }.AttachToDocument();

                        // go?

                        // TypeError: Cannot read property 'id' of null
                        scope_uri = uri.value;

                        #region  await default(HopToChromeExtension)
                        await default(HopToChromeApp);

                        Console.WriteLine("hop from appwindow to app! " + new { scope_uri });
                        // verify


                        await default(HopToChromeExtension);
                        #endregion
                        var tabwindow2 = await chrome.windows.create(new { state = "fullscreen", url = scope_uri });
                        await Task.Delay(5000);
                        var captureVisibleTab2 = await tabwindow2.id.captureVisibleTab(options: new { format = "jpeg" });
                        await tabwindow2.id.remove();
                        #region await default(HopToChromeAppWindow)
                        await default(HopToChromeApp);
                        Console.WriteLine("app " + new { captureVisibleTab.Length });
                        await default(HopToChromeAppWindow);
                        Console.WriteLine("appwindow " + new { captureVisibleTab.Length });
                        #endregion
                        var icaptureVisibleTabFull2 = await new IHTMLImage { src = captureVisibleTab2 }.async.oncomplete;
                        var focus2 = new CanvasRenderingContext2D(icaptureVisibleTabFull2.height, icaptureVisibleTabFull2.height);
                        focus2.drawImage(icaptureVisibleTabFull2, (icaptureVisibleTabFull2.width - icaptureVisibleTabFull2.height) / 2, 0, icaptureVisibleTabFull2.height, icaptureVisibleTabFull2.height, 0, 0, icaptureVisibleTabFull2.height, icaptureVisibleTabFull2.height);
                        var icaptureVisibleTab2 = new IHTMLImage { src = focus2.canvas.toDataURL() };
                        var file2 = index.ToString().PadLeft(5, '0') + ".jpg";
                        await dir.WriteAllBytes(file2, focus2);

                        // done?
                        goto next;

                    };


                    await default(HopToChromeApp);

                    Console.WriteLine("hop from appwindow to app!");

                };


                return;
            }

            Console.WriteLine("running as content?");
        }

    }
}
