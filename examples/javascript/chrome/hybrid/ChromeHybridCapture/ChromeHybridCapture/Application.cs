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
using ChromeHybridCapture;
using ChromeHybridCapture.Design;
using ChromeHybridCapture.HTML.Pages;
using chrome;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace ChromeHybridCapture
{
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150824/webgliframebuffer

    public struct HopToChromeAppWindow : System.Runtime.CompilerServices.INotifyCompletion
    {
        public chrome.AppWindow window;



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




        // subst a: Z:\jsc.svn\examples\javascript\chrome\extensions\ChromeHybridCapture\ChromeHybridCapture\bin\Debug\staging\ChromeHybridCapture.Application\web


        // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPMultiPort\ChromeTCPMultiPort\Application.cs
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150719

        // X:\jsc.svn\examples\java\android\future\ADBSwitchToCompiler\ADBSwitchToCompiler\ApplicationActivity.cs

        // jsc should package displayName  the end of the view-source?
        // should we gzip the string lookup?

        static AppWindow outputWindow;

        static DirectoryEntry dir;
        static int index;


        //static bool nexttakeenabled;

        static Stopwatch countdown;

        public Application(IApp page)
        {
            // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeTabsExperiment\ChromeTabsExperiment\Application.cs
            Console.WriteLine("enter ChromeHybridCapture Application");


            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;



            #region self_chrome_tabs extension
            object self_chrome_tabs = self_chrome.tabs;
            if (self_chrome_tabs != null)
            {
                // running as extension {{ FullName = <Namespace>.Application }}

                //Console.WriteLine("running as extension " + new { typeof(Application).FullName });
                //Console.WriteLine("running as extension " + new { typeof(Application).Assembly.FullName });

                // can we find and connect to app?
                Console.WriteLine("running as extension " + new { typeof(Application).Assembly.GetName().Name });

                //70ms chrome.management.getAll
                //2015-08-22 13:41:33.591 view-source:53670 98ms chrome.management.getAll {{ Length = 28 }}
                //2015-08-22 13:41:33.594 view-source:53670 101ms ExtensionInfo {{ id = aemlnmcokphbneegoefdckonejmknohh, name = ChromeHybridCapture }}
                //2015-08-22 13:41:33.597 view-source:53670 104ms ExtensionInfo {{ id = apdfllckaahabafndbhieahigkjlhalf, name = Google Drive }}
                //2015-08-22 13:41:33.599 view-source:53670 106ms ExtensionInfo {{ id = blpcfgokakmgnkcojhhkbfbldkacnbeo, name = YouTube }}
                //2015-08-22 13:41:33.602 view-source:53670 109ms ExtensionInfo {{ id = cgnjcccfcjhdnbfgjgllglbhfcgndmea, name = WebGLHZBlendCharacter }}
                //2015-08-22 13:41:33.604 view-source:53670 111ms ExtensionInfo {{ id = coobgpohoikkiipiblmjeljniedjpjpf, name = Google Search }}
                //2015-08-22 13:41:33.608 view-source:53670 114ms ExtensionInfo {{ id = fkgibadjpabiongmgoeomdbcefhabmah, name = ChromeCaptureToFile.Application.exe }}
                //2015-08-22 13:41:33.610 view-source:53670 117ms ExtensionInfo {{ id = ghbmnnjooekpmoecnnnilnnbdlolhkhi, name = Google Docs Offline }}
                //2015-08-22 13:41:33.612 view-source:53670 119ms ExtensionInfo {{ id = haebnnbpedcbhciplfhjjkbafijpncjl, name = TinEye Reverse Image Search }}
                //2015-08-22 13:41:33.614 view-source:53670 121ms ExtensionInfo {{ id = lchcahaldakdnjlkchkgncecgpcnabgo, name = Heat Zeeker }}
                //2015-08-22 13:41:33.616 view-source:53670 123ms ExtensionInfo {{ id = nhkcfbkpodjkallcfebgihcoglfaniep, name = freenode irc }}
                //2015-08-22 13:41:33.619 view-source:53670 126ms ExtensionInfo {{ id = ogmpedngmnolclkmlpcdgmfonlagkejp, name = Private Joe: Urban Warfare }}
                //2015-08-22 13:41:33.621 view-source:53670 128ms ExtensionInfo {{ id = pcklgpcdddecpmkiinpkhehanbijjepn, name = idea-remixer }}
                //2015-08-22 13:41:33.624 view-source:53670 131ms ExtensionInfo {{ id = pjkljhegncpnkpknbcohdijeoejaedia, name = Gmail }}
                //2015-08-22 13:41:33.626 view-source:53670 133ms ExtensionInfo {{ id = plgmlhohecdddhbmmkncjdmlhcmaachm, name = draw.io (Legacy) }}
                //2015-08-22 13:41:33.629 view-source:53670 136ms ExtensionInfo {{ id = aapbdbdomjkkjkaonfhkkikfgjllcleb, name = Google Translate }}
                //2015-08-22 13:41:33.631 view-source:53670 138ms ExtensionInfo {{ id = bcfddoencoiedfjgepnlhcpfikgaogdg, name = QR-Code Tag Extension }}
                //2015-08-22 13:41:33.633 view-source:53670 139ms ExtensionInfo {{ id = coblegoildgpecccijneplifmeghcgip, name = Web Cache }}
                //2015-08-22 13:41:33.635 view-source:53670 142ms ExtensionInfo {{ id = ganlifbpkcplnldliibcbegplfmcfigp, name = Collusion for Chrome }}
                //2015-08-22 13:41:33.638 view-source:53670 145ms ExtensionInfo {{ id = gighmmpiobklfepjocnamgkkbiglidom, name = AdBlock }}
                //2015-08-22 13:41:33.640 view-source:53670 147ms ExtensionInfo {{ id = iiabebggdceojiejhopnopmbkgandhha, name = Operation Heat Zeeker }}
                //2015-08-22 13:41:33.642 view-source:53670 149ms ExtensionInfo {{ id = jkgfnfnagdnjicmonpfhhdnkdjgjdamo, name = Avalon Spider Solitaire }}
                //2015-08-22 13:41:33.644 view-source:53670 151ms ExtensionInfo {{ id = kdifgkljkjhpflhalpkhehlldfakggdi, name = my.jsc-solutions.net }}
                //2015-08-22 13:41:33.646 view-source:53670 153ms ExtensionInfo {{ id = lmjegmlicamnimmfhcmpkclmigmmcbeh, name = Application Launcher for Drive (by Google) }}
                //2015-08-22 13:41:33.648 view-source:53670 155ms ExtensionInfo {{ id = mmfbcljfglbokpmkimbfghdkjmjhdgbg, name = Text }}
                //2015-08-22 13:41:33.650 view-source:53670 157ms ExtensionInfo {{ id = molncoemjfmpgdkbdlbjmhlcgniigdnf, name = Project Naptha }}
                //2015-08-22 13:41:33.652 view-source:53670 159ms ExtensionInfo {{ id = ogkcjmbhnfmlnielkjhedpcjomeaghda, name = WebGL Inspector }}
                //2015-08-22 13:41:33.653 view-source:53670 160ms ExtensionInfo {{ id = pkngagjebplcgimojegcakmnlggmcjlc, name = LBA Redux }}
                //2015-08-22 13:41:33.657 view-source:53670 164ms ExtensionInfo {{ id = ppmibgfeefcglejjlpeihfdimbkfbbnm, name = Change HTTP Request Header }}

                Action<string> __ChromeCaptureToFile_Application_sendMessage = delegate { };

                new { }.With(
                    async delegate
                    {
                        //  TypeError: chrome.management.getAll is not a function

                        Console.WriteLine("chrome.management.getAll");
                        var extensions = await chrome.management.getAll();

                        Console.WriteLine("chrome.management.getAll " + new { extensions.Length });
                        // https://developer.chrome.com/extensions/management#type-ExtensionInfo

                        //                        view - source:53670 69ms chrome.management.getAll
                        //2015 - 08 - 22 13:34:13.514 view - source:53670 89ms chrome.management.getAll { { Length = 28 } }
                        //                        2015 - 08 - 22 13:34:13.518 view - source:53670 93ms ExtensionInfo { { item = [object Object] } }

                        foreach (var item in extensions)
                        {
                            //Console.WriteLine("ExtensionInfo " + new { item });
                            //Console.WriteLine("ExtensionInfo " + new { item.id, item.name });

                            //2015-08-22 13:41:33.608 view-source:53670 114ms ExtensionInfo {{ id = fkgibadjpabiongmgoeomdbcefhabmah, name = ChromeCaptureToFile.Application.exe }}

                            // typeof(self) ?
                            if (item.name.StartsWith(typeof(Application).Assembly.GetName().Name))
                            {
                                var __item = item;

                                // we will also know when it reloads? we have to reconnect then?

                                __ChromeCaptureToFile_Application_sendMessage = message =>
                                {
                                    Console.WriteLine("extension chrome.runtime.sendMessage " + new { message, __item.id, __item.name });

                                    chrome.runtime.sendMessage(item.id, message, null);
                                };

                                chrome.runtime.sendMessage(item.id, "extension to app!", null);


                                // extension chrome.runtime.connect done {{ name = , sender = null }}
                                chrome.runtime.connect(item.id).With(
                                    port =>
                                    {
                                        //Console.WriteLine("extension chrome.runtime.connect done " + new { port.name, port.sender.id });
                                        //Console.WriteLine("extension chrome.runtime.connect done " + new { port.name, port.sender });
                                        Console.WriteLine("extension chrome.runtime.connect done");




                                        #region extension: HopToChromeApp.VirtualOnCompleted
                                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeapp
                                        HopToChromeApp.VirtualOnCompleted = async (that, continuation) =>
                                       {
                                           // state 0 ? or state -1 ?
                                           Console.WriteLine("extension HopToChromeApp VirtualOnCompleted enter ");

                                           TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableContinuation r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);

                                           // 29035ms extension  port.onMessage {{ message = do HopToChromeExtension }}
                                           port.postMessage("do HopToChromeApp " + new { r.shadowstate.TypeName, r.shadowstate.state });


                                           // now send the jump instruction... will it make it?
                                           port.postMessage(r.shadowstate);
                                       };
                                        #endregion




                                        // is the app now able to send extension messages?

                                        port.onMessage.addListener(
                                            new Action<object>(
                                                (message) =>
                                                {
                                                    // 4847ms extension  port.onMessage {{ message = do HopToChromeExtension {{ TypeName = <Namespace>.___ctor_b__4_9_d, state = 0 }}



                                                    // extension  port.onMessage {{ message = from app hello to extension }}
                                                    // extension  port.onMessage {{ message = [object Object] }}

                                                    // look app sent a message to extension

                                                    // 191ms extension  port.onMessage {{ message = from app hello to extension, is_string = false, equals_typeofstring = false }}

                                                    // 219ms extension  port.onMessage {{ message = from app hello to extension, expando_isstring = true, is_string = false, equals_typeofstring = false }}
                                                    //var expando_isstring = ScriptCoreLib.JavaScript.Runtime.Expando.Of(message).IsString;

                                                    // roslyn? wtf
                                                    //var is_string = message is string;
                                                    //var equals_typeofstring = message.GetType() == typeof(string);

                                                    //Console.WriteLine("extension  port.onMessage " + new { expando_isstring, is_string, equals_typeofstring });

                                                    // extension  port.onMessage {{ state = null, TypeName = null }}
                                                    if (message is string)
                                                    {
                                                        Console.WriteLine("extension  port.onMessage: " + message);

                                                        // Z:\jsc.svn\examples\javascript\async\Test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\ShadowIAsyncStateMachine.cs
                                                        return;
                                                    }

                                                    // casting from anonymous object.
                                                    var xShadowIAsyncStateMachine = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)message;

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
                                                }
                                            )
                                        );



                                        //chrome.tabs.Created += async tab =>
                                        //{
                                        //    port.postMessage("chrome.tabs.Created " + new { tab });

                                        //};

                                        //chrome.tabs.Updated += async (tabId, x, tab) =>
                                        //{
                                        //    //  Updated {{ i = 0, x = null, tab = null }}

                                        //    port.postMessage("chrome.tabs.Updated  " + new { tabId, x, tab });


                                        //};
                                    }
                                );
                            }
                        }

                    }
                    );

                //chrome.tabs.Created += async tab =>
                //{
                //    Console.WriteLine("chrome.tabs.Created " + new { tab });

                //};

                //chrome.tabs.Updated += async (tabId, x, tab) =>
                //{
                //    //  Updated {{ i = 0, x = null, tab = null }}

                //    Console.WriteLine("chrome.tabs.Updated  " + new { tabId, x, tab });


                //};



                return;
            }
            #endregion


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

                    // running as app {{ FullName = ChromeHybridCapture.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null }}
                    //Console.WriteLine("running as app " + new { typeof(Application).Assembly.FullName });

                    // running as app {{ Name = ChromeHybridCapture.Application }}
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





                    #region app:ConnectExternal
                    chrome.runtime.ConnectExternal += port =>
                    {
                        // app chrome.runtime.ConnectExternal {{ name = , id = jadmeogmbokffpkdfeiemjplohfgkidd }}

                        //Console.WriteLine("app chrome.runtime.ConnectExternal " + new { port.name, port.sender.id });
                        Console.WriteLine("app chrome.runtime.ConnectExternal " + new { port.sender.id } + " now click launch!");

                        new chrome.Notification(title: "ChromeHybridCapture", message: "service connected. click launch").Clicked += delegate
                        {
                            // https://developer.chrome.com/apps/app_runtime

                            // management_api
                        };

                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hybrid

                        // should we now be able to hop to our tab?
                        // what about if we are in an app window?

                        port.onMessage.addListener(
                            new Action<object>(
                                (message) =>
                                {
                                    // extension  port.onMessage {{ message = from app hello to extension }}
                                    var expando_isstring = ScriptCoreLib.JavaScript.Runtime.Expando.Of(message).IsString;

                                    // look app sent a message to extension
                                    //Console.WriteLine("app  port.onMessage " + new { message });

                                    if (expando_isstring)
                                    {
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
                        Console.WriteLine("app chrome.runtime.MessageExternal " + new { message });

                        // app chrome.runtime.MessageExternal {{ message = extension to app! }}

                        // remember the connection to enable hop to extension?
                    };

                    //Action AtLaunch = delegate { };

                    #region  Launched
                    // can the extension launch us too?
                    // either the user launches by a click or we launch from extension?
                    chrome.app.runtime.Launched += async delegate
                    {
                        // state 0 ? or state -1 ?

                        Console.WriteLine("app chrome.app.runtime.Launched before delay");

                        await Task.Delay(1);

                        nexttake:
                        //Console.WriteLine("nexttake " + new { index });
                        Console.WriteLine("nexttake");

                        // using IDisposable ?
                        await default(HopToChromeExtension);

                        #region HopToChromeExtension
                        // now this would be cool if it worked?
                        Console.WriteLine("app to extension chrome.app.runtime.Launched, only state was sent over?");

                        // can we do our thing and jump back with the capture now?

                        // lets create a tab for us to jump into..
                        //var tab = await chrome.tabs.create(new { url = "http://example.com" });

                        // https://developer.chrome.com/extensions/activeTab

                        // implict/auto context hop?
                        var tabsw = Stopwatch.StartNew();

                        // which window station?
                        var tabwindow = await chrome.windows.create(new { state = "fullscreen", url = "http://www.flightradar24.com/59.15,23.86/9" });
                        //var tabwindow = await chrome.windows.create(new { state = "fullscreen", url = "http://example.com" });

                        //var tab = await chrome.tabs.create(new { url = "http://www.flightradar24.com/59.05,24.14/8" });
                        // would we need to hop into the tab to inspec data?
                        Console.WriteLine("extension chrome.tabs.create done. about to capture... " + new { tabsw.ElapsedMilliseconds });


                        // how do we know a tab is loaded?
                        // content takes a while to load doesnt it...
                        await Task.Delay(7000);

                        // Error: Invocation of form tabs.captureVisibleTab(object, null, function) doesn't match definition tabs.captureVisibleTab(optional integer windowId, optional object options, function callback)

                        //var captureVisibleTab = await tab.windowId.captureVisibleTab(null);

                        // Unchecked runtime.lastError while running tabs.captureVisibleTab: Failed to capture tab: unknown error

                        retry_captureVisibleTab:
                        var captureVisibleTab = await tabwindow.id.captureVisibleTab(options: new { format = "png" });


                        if (captureVisibleTab == null)
                        {
                            await Task.Delay(500);
                            Console.WriteLine("extension chrome.tabs.create done. about to capture... error");
                            goto retry_captureVisibleTab;
                        }

                        // extension captureVisibleTab {{ Length = 47743 }}
                        Console.WriteLine("extension captureVisibleTab " + new { captureVisibleTab.Length });

                        await Task.Delay(500);

                        // or just unload the window?
                        //await tab.id.remove();
                        await tabwindow.id.remove();


                        Console.WriteLine("extension to app chrome.tabs.create removed, jump back?");
                        #endregion

                        await default(HopToChromeApp);

                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeapp
                        Console.WriteLine("extension to app chrome.tabs.create removed, jump back done! did the strings make it?");
                        if (captureVisibleTab == null)
                            Console.WriteLine("app chrome.tabs.create removed, jump back done! did the strings make it? no");
                        else
                            Console.WriteLine("app chrome.tabs.create removed, jump back done! did the strings make it? yes " + new { captureVisibleTab.Length });



                        Console.WriteLine("app chrome.fileSystem.chooseEntry");

                        // not available in background?
                        // TypeError: Cannot read property 'chooseEntry' of undefined
                        // Unchecked runtime.lastError while running fileSystem.chooseEntry: Invalid calling page. This function can't be called from a background page.
                        //var dir = (DirectoryEntry)await chrome.fileSystem.chooseEntry(new { type = "openDirectory" });

                        // can we jump to extension to open our tab?



                        //// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow
                        // defined at?
                        await default(HopToChromeAppWindow);

                        #region HopToChromeAppWindow
                        if (captureVisibleTab == null)
                            Console.WriteLine("appwindow chrome.tabs.create removed, jump back done! did the strings make it? no");
                        else
                            Console.WriteLine("appwindow chrome.tabs.create removed, jump back done! did the strings make it? yes " + new { captureVisibleTab.Length });


                        // 182ms appwindow chrome.fileSystem.chooseEntry
                        Console.WriteLine("appwindow chrome.fileSystem.chooseEntry");

                        // Unchecked runtime.lastError while running fileSystem.chooseEntry: User cancelled

                        Native.body.style.overflow = IStyle.OverflowEnum.auto;
                        Native.body.Clear();

                        chrome.app.window.current().show();

                        var icaptureVisibleTabFull = await new IHTMLImage { src = captureVisibleTab }.async.oncomplete;
                        ;

                        // 200, 80
                        // 1600, 880

                        // 1920x1080
                        var focusw = icaptureVisibleTabFull.width - 320;
                        var focush = icaptureVisibleTabFull.height - 200;

                        new IHTMLPre { new { focusw, focush } }.AttachToDocument();

                        var focus = new CanvasRenderingContext2D(focusw, focush);

                        focus.drawImage(icaptureVisibleTabFull, 200, 80, focusw, focush, 0, 0, focusw, focush);



                        // this is a thumbnail
                        var icaptureVisibleTab = new IHTMLImage { src = captureVisibleTab }.AttachToDocument();

                        new IStyle(icaptureVisibleTab)
                        {
                            width = "120px",
                            border = "1px dashed blue"
                        };



                        Native.document.documentElement.style.overflow = IStyle.OverflowEnum.auto;

                        if (dir == null)
                        {
                            await new IHTMLButton { "openDirectory" }.AttachToDocument().async.onclick;

                            dir = (DirectoryEntry)await chrome.fileSystem.chooseEntry(new { type = "openDirectory" });
                        }

                        var file = index.ToString().PadLeft(5, '0') + ".png";

                        new IHTMLPre { "WriteAllBytes... " + new { file } }.AttachToDocument();

                        //await dir.WriteAllBytes("0001.png", icaptureVisibleTabFull);

                        // chrome://blob-internals/
                        // those blobs wont go away...
                        // GC kicks in at 36 it seems.
                        await dir.WriteAllBytes(file, focus);
                        focus = null;

                        //                        [Window Title]
                        //Location is not available

                        //[Content]
                        //R:\ is not accessible.

                        //Logon failure: unknown user name or bad password.


                        //[OK]

                        //                        ---------------------------
                        //                        Restoring Network Connections
                        //---------------------------
                        //An error occurred while reconnecting R:
                        //                            to
                        //\\192.168.1.12\x$
                        //Microsoft Windows Network: The local device name is already in use.


                        //This connection has not been restored.
                        //---------------------------
                        //OK
                        //-------------------------- -


                        new IHTMLPre { "WriteAllBytes... done " + new { file } }.AttachToDocument();

                        index++;
                        #endregion




                        //Console.WriteLine("app chrome.app.window content loaded!");


                        //Console.WriteLine("app chrome.app.runtime.Launched ready to exit");
                        //await Task.Delay(3000);

                        //// wont work?
                        //w.close();

                        ////1343ms app chrome.runtime.MessageExternal {{ message = extension to app! }}
                        ////2015-08-22 15:18:44.738 view-source:53670 1357ms app chrome.runtime.ConnectExternal {{ id = jadmeogmbokffpkdfeiemjplohfgkidd }}
                        ////2015-08-22 15:18:52.314 view-source:53670 8933ms app chrome.app.runtime.Launched
                        ////2015-08-22 15:18:52.342 view-source:53670 8961ms app chrome.app.runtime.Launched exit
                        ////2015-08-22 15:18:52.348 view-source:53670 8967ms app  port.onMessage {{ message = chrome.tabs.Created {{ tab = [object Object] }} }}
                        ////2015-08-22 15:18:52.652 view-source:53670 9271ms app  port.onMessage {{ message = chrome.tabs.Updated  {{ tabId = 419, x = [object Object], tab = [object Object] }} }}
                        ////2015-08-22 15:18:52.690 view-source:53670 9308ms app  port.onMessage {{ message = chrome.tabs.Updated  {{ tabId = 419, x = [object Object], tab = [object Object] }} }}

                        //Console.WriteLine("app chrome.app.runtime.Launched exit");


                        // or tcp?
                        //await new IHTMLButton { "next take" }.AttachToDocument().async.onclick;


                        // var countdown = Stopwatch.StartNew();

                        //Error CS1929  'int' does not contain a definition for 'GetAwaiter' and the best extension method overload 'IXMLHttpRequestAsyncExtensions.GetAwaiter(IXMLHttpRequest)' requires a receiver of type 'IXMLHttpRequest'  ChromeHybridCapture Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCapture\ChromeHybridCapture\Application.cs 1141    IntelliSense

                        // script: error JSC1000: No implementation found for this native method, please implement [static System.TimeSpan.op_Subtraction(System.TimeSpan, System.TimeSpan)]

                        // TypeError: Cannot set property 'countdown' of null

                        //  Cannot set property 'sw' of null
                        countdown = Stopwatch.StartNew();

                        new IHTMLPre { () => "close to abort... " + countdown.ElapsedMilliseconds }.AttachToDocument();

                        // GC?
                        await Task.Delay(2000);

                        //await Task.WhenAny(
                        //      new IHTMLButton {
                        //          "click next take or wait "
                        //      }.AttachToDocument().async.onclick,
                        //      Task.Delay(1000)
                        //  );


                        //await
                        //  new IHTMLButton {
                        //          "click next take "
                        //  }.AttachToDocument().async.onclick;


                        new IHTMLPre { "preparing..." }.AttachToDocument();

                        Console.WriteLine("appwindow: appwindow to app");

                        await default(HopToChromeApp);

                        Console.WriteLine("app: appwindow to app");

                        goto nexttake;



                    };
                    #endregion


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
