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
using HybridHopToUDPChromeApp;
using HybridHopToUDPChromeApp.Design;
using HybridHopToUDPChromeApp.HTML.Pages;
using chrome;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Net.Sockets;

namespace HybridHopToUDPChromeApp
{
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150824/webgliframebuffer

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

        //Error   CS1929	'HopToChromeExtension' does not contain a definition for 'GetAwaiter' and the best extension method overload 'IXMLHttpRequestAsyncExtensions.GetAwaiter(IXMLHttpRequest)' requires a receiver of type 'IXMLHttpRequest'	HybridHopToUDPChromeApp Z:\jsc.svn\examples\javascript\chrome\hybrid\HybridHopToUDPChromeApp\HybridHopToUDPChromeApp\Application.cs	404	IntelliSense

        public HopToChromeAppWindow GetAwaiter() { return this; }



        //Error   CS0117	'HopToChromeExtension' does not contain a definition for 'IsCompleted'	HybridHopToUDPChromeApp Z:\jsc.svn\examples\javascript\chrome\hybrid\HybridHopToUDPChromeApp\HybridHopToUDPChromeApp\Application.cs	409	IntelliSense
        public bool IsCompleted { get { return false; } }


        //Error CS0117	'HopToChromeExtension' does not contain a definition for 'GetResult'	HybridHopToUDPChromeApp Z:\jsc.svn\examples\javascript\chrome\hybrid\HybridHopToUDPChromeApp\HybridHopToUDPChromeApp\Application.cs	414	IntelliSense
        public void GetResult() { }

    }

    public struct HopToChromeApp : System.Runtime.CompilerServices.INotifyCompletion
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeapp

        public static Action<HopToChromeApp, Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation)
        {

            VirtualOnCompleted(this, continuation);
        }

        //Error   CS1929	'HopToChromeExtension' does not contain a definition for 'GetAwaiter' and the best extension method overload 'IXMLHttpRequestAsyncExtensions.GetAwaiter(IXMLHttpRequest)' requires a receiver of type 'IXMLHttpRequest'	HybridHopToUDPChromeApp Z:\jsc.svn\examples\javascript\chrome\hybrid\HybridHopToUDPChromeApp\HybridHopToUDPChromeApp\Application.cs	404	IntelliSense

        public HopToChromeApp GetAwaiter() { return this; }



        //Error   CS0117	'HopToChromeExtension' does not contain a definition for 'IsCompleted'	HybridHopToUDPChromeApp Z:\jsc.svn\examples\javascript\chrome\hybrid\HybridHopToUDPChromeApp\HybridHopToUDPChromeApp\Application.cs	409	IntelliSense
        public bool IsCompleted { get { return false; } }


        //Error CS0117	'HopToChromeExtension' does not contain a definition for 'GetResult'	HybridHopToUDPChromeApp Z:\jsc.svn\examples\javascript\chrome\hybrid\HybridHopToUDPChromeApp\HybridHopToUDPChromeApp\Application.cs	414	IntelliSense
        public void GetResult() { }

    }


    public struct HopToChromeExtension : System.Runtime.CompilerServices.INotifyCompletion
    {
        public static Action<HopToChromeExtension, Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation)
        {

            VirtualOnCompleted(this, continuation);
        }

        //Error   CS1929	'HopToChromeExtension' does not contain a definition for 'GetAwaiter' and the best extension method overload 'IXMLHttpRequestAsyncExtensions.GetAwaiter(IXMLHttpRequest)' requires a receiver of type 'IXMLHttpRequest'	HybridHopToUDPChromeApp Z:\jsc.svn\examples\javascript\chrome\hybrid\HybridHopToUDPChromeApp\HybridHopToUDPChromeApp\Application.cs	404	IntelliSense

        public HopToChromeExtension GetAwaiter() { return this; }



        //Error   CS0117	'HopToChromeExtension' does not contain a definition for 'IsCompleted'	HybridHopToUDPChromeApp Z:\jsc.svn\examples\javascript\chrome\hybrid\HybridHopToUDPChromeApp\HybridHopToUDPChromeApp\Application.cs	409	IntelliSense
        public bool IsCompleted { get { return false; } }


        //Error CS0117	'HopToChromeExtension' does not contain a definition for 'GetResult'	HybridHopToUDPChromeApp Z:\jsc.svn\examples\javascript\chrome\hybrid\HybridHopToUDPChromeApp\HybridHopToUDPChromeApp\Application.cs	414	IntelliSense
        public void GetResult() { }

    }


    #region HopToChromeTab
    public struct HopToChromeTab : System.Runtime.CompilerServices.INotifyCompletion
    {
        // basically we have to hibernate the current state to resume
        public HopToChromeTab GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }

        public static Action<HopToChromeTab, Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation) { VirtualOnCompleted(this, continuation); }

        public void GetResult() { }

        // can we move GetAwaiter to extend TabIdInteger
        public TabIdInteger id;
        public static implicit operator HopToChromeTab(TabIdInteger id)
        {
            return new HopToChromeTab { id = id };
        }

        public static explicit operator HopToChromeTab(Tab tab)
        {
            return tab.id;
        }
    }
    #endregion


    //public static class xHopToChromeTab
    //{
    //	[Obsolete("while possible, reading the source code wont indicate we are about to hop.. keep the cast instead?")]
    //	public static HopToChromeTab GetAwaiter(this TabIdInteger id) { return id; }
    //}

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // why?
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow
        static Func<string, string> DecoratedString =
             x => x.Replace("-", "_").Replace("+", "_").Replace("<", "_").Replace(">", "_");




        // subst a: Z:\jsc.svn\examples\javascript\chrome\extensions\HybridHopToUDPChromeApp\HybridHopToUDPChromeApp\bin\Debug\staging\HybridHopToUDPChromeApp.Application\web


        // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPMultiPort\ChromeTCPMultiPort\Application.cs
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150719

        // X:\jsc.svn\examples\java\android\future\ADBSwitchToCompiler\ADBSwitchToCompiler\ApplicationActivity.cs

        // jsc should package displayName  the end of the view-source?
        // should we gzip the string lookup?



        // by package?
        static AppWindow outputWindow;

        static DirectoryEntry dir;
        static int index;


        //static bool nexttakeenabled;

        static Stopwatch countdown;

        public Application(IApp page)
        {
            // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeTabsExperiment\ChromeTabsExperiment\Application.cs
            Console.WriteLine("enter HybridHopToUDPChromeApp Application");


            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;


            #region self_chrome_socket app
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
                {
                    Console.WriteLine("appwindow chrome.app.window.create, is that you?");

                    MessagePort appwindow_to_app = null;

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

                        if (message is string)
                        {
                            Console.WriteLine("appwindow    Native.window.onmessage: " + message);
                            //Console.WriteLine("appwindow    Native.window.onmessage: " + new { e.ports });

                            if (e.ports != null)
                                foreach (var port in e.ports)
                                {
                                    Console.WriteLine("appwindow    Native.window.onmessage " + new { port });

                                    appwindow_to_app = port;
                                }

                            //e.po

                            return;
                        }

                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeapp




                        // casting from anonymous object.

                        // defined at?
                        // "Z:\jsc.svn\examples\javascript\async\Test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync.csproj"
                        // clean and rebuild
                        // 2>C:\Program Files (x86)\MSBuild\14.0\bin\Microsoft.Common.CurrentVersion.targets(1612,5): warning : The referenced project '..\..\..\..\async\Test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync.csproj' does not exist.
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
                        Console.WriteLine("appwindow HopToChromeApp VirtualOnCompleted enter " + new { appwindow_to_app });

                        //// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow
                        //// async dont like ref?
                        TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableContinuation r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);
                        // 29035ms extension  port.onMessage {{ message = do HopToChromeExtension }}
                        appwindow_to_app.postMessage("do HopToChromeAppWindow " + new { r.shadowstate.TypeName, r.shadowstate.state });
                        // now send the jump instruction... will it make it?
                        appwindow_to_app.postMessage(r.shadowstate);
                    };
                    #endregion


                }
                else
                {
                    //Console.WriteLine("running as app");

                    // running as app {{ FullName = HybridHopToUDPChromeApp.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null }}
                    //Console.WriteLine("running as app " + new { typeof(Application).Assembly.FullName });

                    // running as app {{ Name = HybridHopToUDPChromeApp.Application }}
                    Console.WriteLine("running as app " + new { typeof(Application).Assembly.GetName().Name } + " now reenable extension..");


                    // called by?
                    #region app:appwindow_to_app
                    Action<object> appwindow_to_app = data =>
                    {
                        // what if its a string not a dictinary?

                        var xShadowIAsyncStateMachine = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)data;

                        Console.WriteLine("app:appwindow_to_app " + new { xShadowIAsyncStateMachine.TypeName });

                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150905/hoptoudpchromeapp
                        //13882ms app HopToChromeAppWindow MessageChannel onmessage {{ data = do HopToChromeAppWindow {{ TypeName = <Namespace>.___ctor_b__5_19_d, state = 2 }} }}
                        //view-source:54032 13883ms app appwindow_to_app {{ TypeName = null }}

                        #region xAsyncStateMachineType
                        var xAsyncStateMachineType = typeof(Application).Assembly.GetTypes().FirstOrDefault(
                            xAsyncStateMachineTypeCandidate =>
                            {
                                // safety check 1

                                //Console.WriteLine(new { sw.ElapsedMilliseconds, item.FullName });

                                var xisIAsyncStateMachine = typeof(IAsyncStateMachine).IsAssignableFrom(xAsyncStateMachineTypeCandidate);
                                if (xisIAsyncStateMachine)
                                {
                                    Console.WriteLine(new { xAsyncStateMachineTypeCandidate.FullName, xisIAsyncStateMachine });

                                    return xAsyncStateMachineTypeCandidate.FullName == xShadowIAsyncStateMachine.TypeName;
                                }

                                return false;
                            }
                        );
                        #endregion

                        Console.WriteLine("app:appwindow_to_app " + new { xAsyncStateMachineType });

                        if (xAsyncStateMachineType == null)
                            Debugger.Break();



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


                    #region app:HopToChromeAppWindow
                    HopToChromeAppWindow.VirtualOnCompleted = async (that, continuation) =>
                    {
                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150824/webgliframebuffer

                        // state 0 ? or state -1 ?
                        Console.WriteLine("app HopToChromeAppWindow VirtualOnCompleted enter ");

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
                        var c = new MessageChannel();

                        c.port1.onmessage += e =>
                        {
                            //10818ms app HopToChromeAppWindow MessageChannel onmessage {{ data = do HopToChromeAppWindow {{ TypeName = <Namespace>.___ctor_b__5_19_d, state = 2 }} }}
                            //view-source:54032 10820ms app:appwindow_to_app {{ TypeName = null }}

                            if (e.data is string)
                            {
                                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150905/hoptoudpchromeapp
                                Console.WriteLine("app   c.port1.onmessage: " + e.data);
                                return;
                            }

                            Console.WriteLine("app HopToChromeAppWindow MessageChannel onmessage " + new { e.data });

                            appwindow_to_app(e.data);
                        };

                        c.port1.start();
                        c.port2.start();


                        //                    15ms appwindow    Native.window.onmessage: {{ ports = [object MessagePort] }}
                        //2015-08-22 20:50:18.019 view-source:53702 17ms appwindow    Native.window.onmessage: {{ port = [object MessagePort] }}
                        that.window.contentWindow.postMessage("do HopToChromeAppWindow " + new { r.shadowstate.TypeName, r.shadowstate.state }, transfer: c.port2);

                        // now send the jump instruction... will it make it?
                        that.window.contentWindow.postMessage(r.shadowstate);
                    };
                    #endregion




                    // called by?
                    #region app:ConnectExternal
                    chrome.runtime.ConnectExternal += port =>
                    {
                        // app chrome.runtime.ConnectExternal {{ name = , id = jadmeogmbokffpkdfeiemjplohfgkidd }}

                        //Console.WriteLine("app chrome.runtime.ConnectExternal " + new { port.name, port.sender.id });
                        Console.WriteLine("app chrome.runtime.ConnectExternal " + new { port.sender.id } + " now click launch!");

                        new chrome.Notification(title: "HybridHopToUDPChromeApp", message: "service connected. click launch").Clicked += delegate
                        {
                            // https://developer.chrome.com/apps/app_runtime

                            // management_api
                        };

                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hybrid

                        // should we now be able to hop to our tab?
                        // what about if we are in an app window?

                        // called by?
                        port.onMessage.addListener(
                            new Action<object>(
                                (message) =>
                                {
                                    // extension  port.onMessage {{ message = from app hello to extension }}
                                    //var expando_isstring = ScriptCoreLib.JavaScript.Runtime.Expando.Of(message).IsString;

                                    // look app sent a message to extension
                                    //Console.WriteLine("app  port.onMessage " + new { message });


                                    // did we not fix jsc?
                                    if (message is string)
                                    {
                                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150905/hoptoudpchromeapp
                                        Console.WriteLine("app  port.onMessage: " + message);
                                        return;
                                    }

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
                                }
                            )
                        );



                        //port.postMessage(
                        //    new
                        //    {
                        //        text = "from app hello to extension"
                        //    }
                        //);

                        port.postMessage("from app hello to extension, click launch?");

                        // enable
                        //await default(HopToChromeExtension);

                        #region HopToChromeExtension
                        HopToChromeExtension.VirtualOnCompleted = async (that, continuation) =>
                        {
                            // state 0 ? or state -1 ?
                            Console.WriteLine("app HopToChromeExtension VirtualOnCompleted enter ");

                            // where is it defined?
                            // z:\jsc.svn\examples\javascript\async\Test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\ShadowIAsyncStateMachine.cs

                            // async dont like ref?
                            TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableContinuation r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);

                            // 29035ms extension  port.onMessage {{ message = do HopToChromeExtension }}
                            port.postMessage("do HopToChromeExtension " + new { r.shadowstate.TypeName, r.shadowstate.state });


                            // now send the jump instruction... will it make it?
                            port.postMessage(r.shadowstate);


                            // how would we know to continue from current continuation?
                            // or are we fine to rebuild the scope if we jump back?
                        };
                        #endregion




                    };
                    #endregion

                    chrome.runtime.MessageExternal += (message, sender, sendResponse) =>
                    {
                        // was the extension able to pass us a message?

                        //Console.WriteLine("chrome.runtime.MessageExternal " + new { message, sender, sendResponse });
                        Console.WriteLine("app: chrome.runtime.MessageExternal " + new { message });

                        // app chrome.runtime.MessageExternal {{ message = extension to app! }}

                        // remember the connection to enable hop to extension?
                    };

                    //Action AtLaunch = delegate { };


                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150905/hoptoudpchromeapp
                    // jsc servicing compiler does it already let the apps know they need to reload?
                    #region  Launched
                    // can the extension launch us too?
                    // either the user launches by a click or we launch from extension?
                    chrome.app.runtime.Launched += async delegate
                    {
                        // state 0 ? or state -1 ?

                        Console.WriteLine("app: chrome.app.runtime.Launched before delay");

                        await Task.Delay(1);

                        Console.WriteLine("app: before HopToChromeAppWindow");

                        //// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow
                        // defined at?
                        await default(HopToChromeAppWindow);

                        // cant select ffs
                        (Native.body.style as dynamic).webkitUserSelect = "text";

                        Native.body.style.overflow = IStyle.OverflowEnum.auto;
                        Native.body.Clear();
                        chrome.app.window.current().show();
                        new IHTMLPre { "appwindow:  chrome.app.window.current().show();" }.AttachToDocument();

                        var NetworkList = await chrome.socket.getNetworkList();

                        //appwindow:  chrome.app.window.current().show();
                        //appwindow:  {{ prefixLength = 64, name = {5AE7FDA4-3FE5-42B2-905A-90962F983884}, address = 2001:7d0:8424:1f01:18fb:7bdf:54a7:e32 }}
                        //appwindow:  {{ prefixLength = 64, name = {5AE7FDA4-3FE5-42B2-905A-90962F983884}, address = fe80::18fb:7bdf:54a7:e32 }}
                        //appwindow:  {{ prefixLength = 24, name = {5AE7FDA4-3FE5-42B2-905A-90962F983884}, address = 192.168.1.13 }}
                        //appwindow:  {{ prefixLength = 64, name = {7F261BE1-E637-4E3E-B766-BD56F87515A5}, address = fe80::9cb1:846a:f8c0:ae4d }}
                        //appwindow:  {{ prefixLength = 16, name = {7F261BE1-E637-4E3E-B766-BD56F87515A5}, address = 169.254.174.77 }}
                        //appwindow:  {{ prefixLength = 64, name = {296E36A9-A4D6-4994-9496-69CCFD248303}, address = fe80::742a:74b2:f2a0:d632 }}
                        //appwindow:  {{ prefixLength = 16, name = {296E36A9-A4D6-4994-9496-69CCFD248303}, address = 169.254.214.50 }}
                        //appwindow:  {{ prefixLength = 64, name = {A588C792-795A-49EA-B7B7-FF2791BD0DDA}, address = fe80::b102:9f72:e61b:59b6 }}
                        //appwindow:  {{ prefixLength = 24, name = {A588C792-795A-49EA-B7B7-FF2791BD0DDA}, address = 192.168.81.1 }}

                        // should figure out which nics to broadcast to by default?

                        foreach (var item in NetworkList)
                        {
                            new IHTMLPre { "appwindow:  " + new { item.prefixLength, item.name, item.address } }.AttachToDocument();

                            // if we have multiple NICs broadcast em all?
                            // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeAppWindowUDPPointerLock\ChromeAppWindowUDPPointerLock\Application.cs
                        }


                        Console.WriteLine("appwindow: appwindow to app");
                        await default(HopToChromeApp);
                        Console.WriteLine("app: appwindow to app");

                        // send intent over udp?


                    };
                    #endregion


                    chrome.runtime.Startup += async delegate
                    {
                        Console.WriteLine(@"app: chrome.runtime.Startup
                        // lets start listening for udp. as the other device may want to click launch and jump into us. 
                        ");

                        // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPReceiveAsync\ChromeUDPReceiveAsync\Application.cs
                        // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPFloats\ChromeUDPFloats\Application.cs

                        var socket = new UdpClient(40014);

                        //socket.Client.Bind(
                        //    //new IPEndPoint(IPAddress.Any, port: 40000)
                        //    new IPEndPoint(IPAddress.Any, port)
                        //);


                        // no port?
                        socket.JoinMulticastGroup(
                            IPAddress.Parse("239.1.2.3")
                        );

                        while (true)
                        {
                            Console.WriteLine(@"app: chrome.runtime.Startup await socket.ReceiveAsync");

                            // Z:\jsc.svn\examples\javascript\chrome\hybrid\HybridHopToUDPChromeApp\Application.cs

                            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                            var s = await socket.ReceiveAsync();

                            Console.WriteLine($"app: recvFrom: {s.Buffer.Length}");

                        }
                    };

                    return;
                }
            }
            #endregion


            // running as regular web page?

            Console.WriteLine("running as content?");

            // were we loaded into chrome.app.window?


            //new IHTMLButton { "openDirectory" }.AttachToDocument().onclick += async delegate
            // {
            //     var dir = (DirectoryEntry)await chrome.fileSystem.chooseEntry(new { type = "openDirectory" });

            // };

        }
    }
}
