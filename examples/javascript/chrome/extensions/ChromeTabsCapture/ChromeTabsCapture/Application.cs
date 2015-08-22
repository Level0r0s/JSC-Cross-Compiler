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
using ChromeTabsCapture;
using ChromeTabsCapture.Design;
using ChromeTabsCapture.HTML.Pages;
using chrome;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace ChromeTabsCapture
{
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
        private static IHTMLButton b;

        // subst a: Z:\jsc.svn\examples\javascript\chrome\extensions\ChromeTabsCapture\ChromeTabsCapture\bin\Debug\staging\ChromeTabsCapture.Application\web


        // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPMultiPort\ChromeTCPMultiPort\Application.cs
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150719

        // X:\jsc.svn\examples\java\android\future\ADBSwitchToCompiler\ADBSwitchToCompiler\ApplicationActivity.cs

        // jsc should package displayName  the end of the view-source?
        // should we gzip the string lookup?


        static List<TabIdInteger> injectOnce = new List<TabIdInteger>();

        static Application()
        {

            // what about console? consolidate all core apps into one?

            // 0ms  cctor Application did we make the jump yet? {{ href = http://example.com/ }}
            Console.WriteLine(" cctor Application did we make the jump yet? " + new
            {

                // wont be available
                //Native.document.currentScript.src,

                Native.document.location.href
            });

            // X:\jsc.svn\examples\javascript\ScriptDynamicSourceBuilder\ScriptDynamicSourceBuilder\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs
            // or. were we injected? then our source is different?
            // makeURL ? did chrome extension prep the special url yet?
            var codetask = new WebClient().DownloadStringTaskAsync(
                         new Uri(Worker.ScriptApplicationSource, UriKind.Relative)
                    );

            #region HopToChromeTab.VirtualOnCompleted
            HopToChromeTab.VirtualOnCompleted = async (that, continuation) =>
            {
                //Console.WriteLine("HopToChromeTab.VirtualOnCompleted ");
                Console.WriteLine("HopToChromeTab.VirtualOnCompleted " + new { that.id });

                // um. whats the tab we are to jump into?
                // signal we are about to inject
                //            await that.id.insertCSS(
                //                    new
                //                    {
                //                        code = @"

                //html { 
                //border-left: 1em solid yellow;
                //}


                //"
                //                    }
                //                );


                // where is it defined?
                // z:\jsc.svn\examples\javascript\async\Test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\ShadowIAsyncStateMachine.cs

                // async dont like ref?
                var r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);


                if (injectOnce.Contains(that.id))
                {
                    Console.WriteLine("HopToChromeTab.VirtualOnCompleted again? " + new { that.id, r.shadowstate.state });

                }
                else
                {
                    // um. now what?
                    // send shadowstate over?
                    // first we have to open a channel

                    // do we have our view-source yet?
                    var code = await codetask;

                    // 5240ms HopToChromeTab.VirtualOnCompleted {{ id = 449, state = 1, Length = 3232941 }}
                    Console.WriteLine("HopToChromeTab.VirtualOnCompleted " + new { that.id, r.shadowstate.state, code.Length });

                    //// how can we inject ourselves and send a signal back to set this thing up?

                    //// https://developer.chrome.com/extensions/tabs#method-executeScript
                    //// https://developer.chrome.com/extensions/tabs#type-InjectDetails
                    //// https://developer.chrome.com/extensions/content_scripts#pi

                    //// Content scripts execute in a special environment called an isolated world. 
                    //// They have access to the DOM of the page they are injected into, but not to any JavaScript variables or 
                    //// functions created by the page. It looks to each content script as if there is no other JavaScript executing
                    //// on the page it is running on. The same is true in reverse: JavaScript running on the page cannot call any 
                    //// functions or access any variables defined by content scripts.

                    injectOnce.Add(that.id);

                    var result = await that.id.executeScript(
                        //new { file = url }
                        new { code }
                    );

                    // now what?

                    Console.WriteLine("HopToChromeTab.VirtualOnCompleted after executeScript");
                }

                // send a SETI message?

                /// whats duplicate
                var response = await that.id.sendMessage(
                    //"hello"

                    r.shadowstate
                );

                Console.WriteLine("HopToChromeTab.VirtualOnCompleted after sendMessage " + new { response });

                // HopToChromeTab.VirtualOnCompleted after sendMessage {{ response = response }}

                // https://developer.chrome.com/extensions/messaging#connect

            };
            #endregion

        }

        static void AtTab()
        {
        }

        public Application(IApp page)
        {
            // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeTabsExperiment\ChromeTabsExperiment\Application.cs
            Console.WriteLine(" enter Application");


            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_tabs = self_chrome.tabs;

            //if (self_chrome_tabs == null)
            //	return;


            //	....488: { SourceMethod = Void.ctor(ChromeTabsCapture.HTML.Pages.IApp), i = [0x00ba] brtrue.s + 0 - 1 }
            //1984:02:01 RewriteToAssembly error: System.ArgumentException: Value does not fall within the expected range.
            //at jsc.ILInstruction.ByOffset(Int32 i) in X:\jsc.internal.git\compiler\jsc\CodeModel\ILInstruction.cs:line 1184
            //at jsc.ILInstruction.get_BranchTargets() in X:\jsc.internal.git\compiler\jsc\CodeModel\ILInstruction.cs:line 1225

            #region self_chrome_tabs
            if (self_chrome_tabs != null)
            {
                Console.WriteLine("self_chrome_tabs");

                //chrome.runtime.Startup +=
                //chrome.runtime.Installed +=



                //70ms chrome.management.getAll
                //2015-08-22 13:41:33.591 view-source:53670 98ms chrome.management.getAll {{ Length = 28 }}
                //2015-08-22 13:41:33.594 view-source:53670 101ms ExtensionInfo {{ id = aemlnmcokphbneegoefdckonejmknohh, name = ChromeTabsCapture }}
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

                            if (item.name.StartsWith("ChromeCaptureToFile.Application"))
                            {
                                // we will also know when it reloads? we have to reconnect then?
                                Console.WriteLine("extension can signal apps? " + new { item.id, item.name });

                                chrome.runtime.sendMessage(item.id, "extension to app!", null);
                            }
                        }

                    }
                    );

                chrome.tabs.Created += async tab =>
                {
                    Console.WriteLine(" chrome.tabs.Created " + new { tab });

                };

                chrome.tabs.Updated += async (tabId, x, tab) =>
                {
                    //  Updated {{ i = 0, x = null, tab = null }}

                    Console.WriteLine("Updated " + new { tabId, x, tab });

                    // why the duck is it null?
                    if (tab == null)
                        tab = await chrome.tabs.get(tabId);

                    Console.WriteLine("Updated 2 " + new { tabId, x, tab });


                    // chrome://newtab/

                    //  TypeError: Cannot read property 'url' of null

                    if (tab == null)
                        return;

                    if (tab.url.StartsWith("chrome-devtools://"))
                        return;

                    if (tab.url.StartsWith("chrome://"))
                        return;


                    // while running tabs.insertCSS: The extensions gallery cannot be scripted.
                    if (tab.url.StartsWith("https://chrome.google.com/webstore/"))
                        return;


                    if (tab.status != "complete")
                        return;

                    //new chrome.Notification
                    //{
                    //	Message = "chrome.tabs.Updated " + new
                    //	{
                    //		tab.id,
                    //		tab.url,
                    //		tab.status,
                    //		tab.title
                    //	}
                    //};


                    // while running tabs.insertCSS: The tab was closed.

                    // 		public static Task<object> insertCSS(this TabIdInteger tabId, object details);
                    // public static void insertCSS(this TabIdInteger tabId, object details, IFunction callback);


                    // for some sites the bar wont show as they html element height is 0?



                    // where is the hop to iframe?
                    // X:\jsc.svn\examples\javascript\Test\TestSwitchToIFrame\TestSwitchToIFrame\Application.cs


                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150403


                    // can we goto back before to the hop?

                    // Error: Invocation of form pageAction.show(object) doesn't match definition pageAction.show(integer tabId)
                    //chrome.pageAction.show((TabIdInteger)(object)tabId);
                    chrome.pageAction.show(tabId);

                    chrome.pageAction.Clicked += async delegate
                    {
                        string captureVisibleTabImageSourceLengthString = "?";

                        Console.WriteLine("enter Clicked");

                        //chrome.pageAction.hide((TabIdInteger)(object)tabId);

                        // TypeError: Cannot read property 'hide' of undefined
                        chrome.pageAction.hide(tabId);

                        // Extension manifest must request permission to access this host.
                        // jpg data url!
                        var captureVisibleTab = (string)await chrome.tabs.captureVisibleTab(null, null);

                        var captureVisibleTabImage = new IHTMLImage { src = captureVisibleTab };
                        await captureVisibleTabImage.async.oncomplete;

                        captureVisibleTab = null;

                        var captureVisibleTabImageSource = captureVisibleTabImage.toDataURL();

                        // before await delay {{ captureVisibleTabImageSourceLength = 354874 }}
                        var captureVisibleTabImageSourceLength = captureVisibleTabImageSource.Length;
                        captureVisibleTabImageSourceLengthString = captureVisibleTabImageSourceLength + "";
                        captureVisibleTabImageSource = null;


                        Console.WriteLine("before await delay " + new { captureVisibleTabImageSourceLength });
                        // statemachine fixup? off by one?
                        await Task.Delay(1);

                        new { }.With(
                            async delegate
                            {
                                Console.WriteLine("before await HopToChromeTab");
                                await (HopToChromeTab)tab;

                                //Console.WriteLine("after await HopToChromeTab " + new { captureVisibleTabImageSource.Length });

                                //b.innerText = "pageAction! " + new { captureVisibleTabImageSource.Length };

                                Console.WriteLine("after await HopToChromeTab ");

                                // 4200ms {{ AsyncStateMachineSourceField = _captureVisibleTabImageSourceLengthString_5__5, value = 385538 }}

                                // 2038ms {{ AsyncStateMachineSourceField = _captureVisibleTabImageSourceLengthString_5__1 }}
                                // ??? why wont it make it?

                                // cuz we are not reading the sent variables.
                                //b.innerText = "pageAction! " + new { captureVisibleTabImageSourceLengthString, captureVisibleTabImageSourceLength };
                                //b.innerText = "pageAction! " + new { captureVisibleTabImageSourceLengthString, captureVisibleTabImageSourceLength };
                                //b.innerText = "pageAction! only state seems to be synchronized here... for now... ";
                                b.innerText = "pageAction! about to save!";
                            }
                        );

                        // well. can we save it?
                        // TypeError: Cannot read property 'chooseEntry' of undefined
                        // {"fileSystem": ["write", "retainEntries", "directory"]} 

                        // not available for tabs. need an app for that.
                        //var dir = (DirectoryEntry)await chrome.fileSystem.chooseEntry(new { type = "openDirectory" });


                        //await dir.WriteAllBytes("0001.png", captureVisibleTabImage);

                    };

                    // keep simple scope
                    var scope_tabId = tabId;

                    //await (HopToChromeTab)tab.id;
                    await (HopToChromeTab)tab;
                    //await tab.id;

                    // are we now on the tab?
                    // can we jump back?

                    // what about jumping with files/uploads?
                    Console.WriteLine("// are we now on the tab yet?");

                    Native.body.style.borderLeft = "1em solid red";
                    Native.document.documentElement.style.borderLeft = "1em solid cyan";




                    // lets start monitoring
                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150821

                    //chrome.tabs.captureVisibleTab

                    //chrome.pageAction.show(tab.)
                    // what api is available if we are in th tab context?

                    //var b = new IHTMLButton { "capture" }.AttachToDocument();
                    ChromeTabsCapture.Application.b = new IHTMLButton { "click pageAction above. HUD " + new { scope_tabId } }.AttachTo(Native.document.documentElement);

                    Console.WriteLine("do you see the HUD button?");

                    b.style.SetLocation(4, 4);
                    b.css.disabled.style.backgroundColor = "red";

                    //             488: { SourceMethod = Void.ctor(ChromeTabsCapture.HTML.Pages.IApp), i = [0x00eb] brtrue.s + 0 - 1 }
                    //             2bf8: 02:01:1e RewriteToAssembly error: System.ArgumentException: Value does not fall within the expected range.
                    //at jsc.ILInstruction.ByOffset(Int32 i) in x:\jsc.internal.git\compiler\jsc\CodeModel\ILInstruction.cs:line 1188
                    //at jsc.ILInstruction.get_BranchTargets() in x:\jsc.internal.git\compiler\jsc\CodeModel\ILInstruction.cs:line 1229
                    //at jsc.ILInstruction.get_BranchSources() in x:\jsc.internal.git\compiler\jsc\CodeModel\ILInstruction.cs:line 1205
                    //at jsc.ILInstruction.get_IsFlowBreak() in x:\jsc.internal.git\compiler\jsc\CodeModel\ILInstruction.cs:line 863
                    //at jsc.ILFlow.NextInstructionBranch() in x:\jsc.internal.git\compiler\jsc\CodeModel\ILFlow.cs:line 585

                    //b.onclick += delegate
                    //{
                    //    b.disabled = true;

                    //    Native.body.style.borderLeft = "1em solid red";

                    //    // would the compiler let the chome extension know it has been updated?

                    //};

                    await b.async.onclick;


                    b.disabled = true;

                    //Native.body.style.borderLeft = "1em solid red";

                    //  TypeError: Cannot read property 'captureVisibleTab' of undefined

                    //var captureVisibleTab = await chrome.tabs.captureVisibleTab(null, null);

                    //b.innerText = new { captureVisibleTab }.ToString();



                };



                return;
            }
            #endregion


            // we made the jump?
            // need it to compile. why???
            ;
            //Native.body.style.borderLeft = "1em solid red";

            // yes we did. can we talk to the chrome extension?

            // lets do some SETI

            // The runtime.onMessage event is fired in each content script running in the specified tab for the current extension.

            // Severity	Code	Description	Project	File	Line
            //Error       'runtime.onMessage' is inaccessible due to its protection level ChromeTabsCapture X:\jsc.svn\examples\javascript\chrome\extensions\ChromeTabsCapture\ChromeTabsCapture\Application.cs 272

            // public static event System.Action<object, object, object> Message

            #region chrome.runtime.Message
            chrome.runtime.Message += (object message, chrome.MessageSender sender, IFunction sendResponse) =>
            {
                var s = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)message;

                // 59ms onmessage {{ message = hello, id = aemlnmcokphbneegoefdckonejmknohh }}
                Console.WriteLine("xonmessage " + new { s.state, sender.id });
                //Native.body.style.borderLeft = "1px solid blue";

                #region xAsyncStateMachineType
                var xAsyncStateMachineType = typeof(Application).Assembly.GetTypes().FirstOrDefault(
                    item =>
                    {
                        // safety check 1

                        //Console.WriteLine(new { sw.ElapsedMilliseconds, item.FullName });

                        var xisIAsyncStateMachine = typeof(IAsyncStateMachine).IsAssignableFrom(item);
                        if (xisIAsyncStateMachine)
                        {
                            //Console.WriteLine(new { item.FullName, isIAsyncStateMachine });

                            return item.FullName == s.TypeName;
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

                           Console.WriteLine(new { AsyncStateMachineSourceField });

                           if (AsyncStateMachineSourceField.Name.EndsWith("1__state"))
                           {
                               AsyncStateMachineSourceField.SetValue(
                                   NewStateMachineI,
                                   s.state
                                );
                           }


                       }
                  );
                #endregion

                NewStateMachineI.MoveNext();

                //Task.Delay(1000).ContinueWith(
                //	delegate
                //	{
                //		sendResponse.apply(null, "response");
                //	}
                //);

            };
            #endregion


            //         Native.window.onmessage += e =>
            //{

            //};
        }

    }
}
