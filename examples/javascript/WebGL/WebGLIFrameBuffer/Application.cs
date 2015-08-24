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
using WebGLIFrameBuffer;
using WebGLIFrameBuffer.Design;
using WebGLIFrameBuffer.HTML.Pages;
using System.Threading;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.WebGL;

namespace WebGLIFrameBuffer
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // z:\jsc.svn\examples\javascript\async\Test\TestBytesToSemaphore\TestBytesToSemaphore\Application.cs
        // z:\jsc.svn\examples\javascript\async\test\TestIFrameTaskRun\TestIFrameTaskRun\Application.cs
        // z:\jsc.svn\examples\javascript\Test\TestHopFromIFrame\TestHopFromIFrame\Application.cs
        // z:\jsc.svn\examples\javascript\Test\TestSwitchToIFrame\TestSwitchToIFrame\Application.cs

        // why?
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow
        static Func<string, string> DecoratedString =
             x => x.Replace("-", "_").Replace("+", "_").Replace("<", "_").Replace(">", "_");



        // https://developer.chrome.com/apps/tags/webview
        // or what if we were to runn as chrome appwindow and webview
        // would they be in separate gpu procsses, leeping parent responsive while gpu is buzy?

        public Application(IApp page)
        {
            #region += Launched chrome.app.window
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAppWindow\ChromeTCPServerAppWindow\Application.cs
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
                {
                    // should implement hop here instead tho

                    //new IHTMLPre { "empty window " }.AttachToDocument();
                    new IHTMLPre { () => "appwindow:  " + new { DateTime.Now.Millisecond } }.AttachToDocument();


                }
                else
                {

                    //ChromeTCPServer.TheServerWithAppWindow.Invoke(AppSource.Text);

                    ChromeTCPServer.TheServer.Invoke(AppSource.Text,


                    // null means we get a new tab, which we could control as an extension?
                    open: async uri =>
                    {
                        // app to appwindow

                        var xappwindow = await chrome.app.window.create(Native.document.location.pathname, options: null);

                        // do we have an empty window?

                        await xappwindow.contentWindow.async.onload;


                        var webview = xappwindow.contentWindow.document.createElement("webview");
                        // You do not have permission to use <webview> tag. Be sure to declare 'webview' permission in your manifest. 
                        webview.setAttribute("partition", "p1");
                        webview.setAttribute("src", uri);
                        webview.style.SetLocation(100, 80);
                        webview.style.width = "400px";
                        webview.style.height = "400px";

                        webview.AttachTo(xappwindow.contentWindow.document.body);

                    }
                );

                }

                return;
            }
            #endregion



















            Native.body.Clear();

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150824/webgliframebuffer

            // until we have webgl2 up and running,
            // iframes could work as well.
            // what we need is a set of iframes that allow
            // 360deg multishader rendering
            // to run it as a hybrid chrome app, either we need aawppwindow tcp webview or tabs window

            // each shader iframe can prep their frames ahead of time in their own pace..
            // will three.planegeometry use atlas?

            #region setup

            var isroot = Native.window.parent == Native.window;

            if (isroot)
                Native.body.style.backgroundColor = "yellow";
            else
                Native.body.style.backgroundColor = "cyan";




            // called by? 619  app:HopToChromeAppWindow
            #region  window: Native.window.onmessage
            Native.window.onmessage += e =>
            {
                Console.WriteLine("iframe:    Native.window.onmessage");

                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow
                var message = e.data;
                if (message is string)
                {
                    Console.WriteLine("iframe:    Native.window.onmessage: " + message);
                    if (e.ports != null)
                        foreach (var port in e.ports)
                        {
                            Console.WriteLine("iframe:    Native.window.onmessage " + new { port });
                            //appwindow_to_app = port;
                        }
                    return;
                }

                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeapp

                // casting from anonymous object.
                var xShadowIAsyncStateMachine = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)message;

                // or constructor id?
                Console.WriteLine("iframe:     Native.window.onmessage " + new { xShadowIAsyncStateMachine.state, xShadowIAsyncStateMachine.TypeName });

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

                        //var xisIAsyncStateMachine = typeof(mscorlib::System.Runtime.CompilerServices.IAsyncStateMachine).IsAssignableFrom(xAsyncStateMachineTypeCandidate);
                        var xisIAsyncStateMachine = typeof(global::System.Runtime.CompilerServices.IAsyncStateMachine).IsAssignableFrom(xAsyncStateMachineTypeCandidate);
                        if (xisIAsyncStateMachine)
                        {
                            //Console.WriteLine(new { item.FullName, isIAsyncStateMachine });

                            return xAsyncStateMachineTypeCandidate.FullName == xShadowIAsyncStateMachine.TypeName;
                        }

                        return false;
                    }
                );
                #endregion


                var NewStateMachine = global::System.Runtime.Serialization.FormatterServices.GetUninitializedObject(xAsyncStateMachineType);
                var isIAsyncStateMachine = NewStateMachine is global::System.Runtime.CompilerServices.IAsyncStateMachine;

                var NewStateMachineI = (global::System.Runtime.CompilerServices.IAsyncStateMachine)NewStateMachine;

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


            //Action<ScriptCoreLib.JavaScript.DOM.IWindow, Action> invokeWhenReady = (contentWindow, yield) =>
            #region invokeWhenReady, roslyn ctp needs it. rc does not?
            Action<IHTMLIFrame, Action> invokeWhenReady = (that, yield) =>
            {
                that.onload += delegate
                {
                    if (yield == null)
                    {
                        Console.WriteLine("window: HopToIFrame that.that.contentWindow.onload dismissed");
                        return;
                    }

                    Console.WriteLine("window: HopToIFrame IHTMLIFrame.onload");


                    if (yield != null)
                    {
                        yield();
                        yield = null;
                    }

                };
            };
            #endregion


            #region window:HopToChromeAppWindow
            // bugcheck: roslyn RC generates byref struct statemachine?
            //HopToIFrame.VirtualOnCompleted = async (that, continuation) =>
            HopToIFrame.VirtualOnCompleted = (that, continuation) =>
           {
               // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150824/webgliframebuffer

               // state 0 ? or state -1 ?
               Console.WriteLine("window: HopToIFrame VirtualOnCompleted enter ");

               #region yield
               Action yield = delegate
               {

                   // z:\jsc.svn\examples\javascript\async\Test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\CLRWebServiceInvoke.cs
                   // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow
                   // async dont like ref?
                   TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableContinuation r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);
                   // 29035ms extension  port.onMessage {{ message = do HopToChromeExtension }}



                   // Z:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IWindow.postMessage.cs
                   // how do we use this thing?
                   var c = new MessageChannel();

                   c.port1.onmessage += e =>
                   {
                       Console.WriteLine("window: HopToChromeAppWindow MessageChannel onmessage " + new { e.data });

                       //appwindow_to_app(e.data);
                   };

                   c.port1.start();
                   c.port2.start();

                   Console.WriteLine("window to iframe postMessage");

                   //                    15ms appwindow    Native.window.onmessage: {{ ports = [object MessagePort] }}
                   //2015-08-22 20:50:18.019 view-source:53702 17ms appwindow    Native.window.onmessage: {{ port = [object MessagePort] }}
                   that.that.contentWindow.postMessage("do HopToIFrame " + new { r.shadowstate.TypeName, r.shadowstate.state }, transfer: c.port2);

                   // now send the jump instruction... will it make it?
                   that.that.contentWindow.postMessage(r.shadowstate);
               };
               #endregion



               #region outputWindow
               if (that.that == null)
               {
                   //  TypeError: Cannot read property 'document' of null

                   // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeappwindow

                   // or should we predownload our source as we do for Worker?
                   that.that = new IHTMLIFrame
                   {
                       src = Native.document.location.href
                   };




                   // bugcheck: roslyn RC generates byref struct statemachine?
                   //await that.that.contentWindow.async.onload;

                   // listener ready?

                   // Error: InvalidOperationException: inline scope sharing not yet implemented


                   //invokeWhenReady(that.that.contentWindow, yield);
                   invokeWhenReady(that.that, yield);

                   //that.that.contentWindow.async.onload.ContinueWith(
                   //    async task =>
                   //     {
                   //         await Native.window.async.onframe;

                   //         yield();
                   //     }
                   //);

                   that.that.AttachToDocument();

                   return;
               }
               #endregion

               yield();
           };
            #endregion

            if (!isroot)
            {
                // bail as we are making the jump..
                return;
            }
            #endregion

            // multishader variant here.






            // singleshader variant here.
            new { }.With(
                async delegate
                {
                    //Error CS7069  Reference to type 'TaskAwaiter<>' claims it is defined in 'mscorlib', but it could not be found WebGLIFrameBuffer Z:\jsc.svn\examples\javascript\WebGL\WebGLIFrameBuffer\Application.cs   48


                    // cna we now have HopToChromeWebview?
                    new IHTMLButton { "window: lets do some slow thing and see if parent stays responsive.. " }.AttachToDocument().onclick += delegate
                    {
                        // simulate gpu timeout
                        Thread.Sleep(10000);

                        // are we a webview or appview?
                        // works as appwindow and webview are in different processes?

                        // until iframes start living in another frame,
                        // we can use chrome and tcp webview to get dual ui it seems.

                    };



                    await new IHTMLButton { () => "window: ready " + new { DateTime.Now.Millisecond } }.AttachToDocument().async.onclick;



                    // red cmd start. example.com 
                    // to open chrome

                    // should actually fork to do multiple shaders at the same time...
                    new IHTMLPre { () => "window: hopping to iframe to prep shader... " + new { DateTime.Now.Millisecond } }.AttachToDocument();


                    new IStyle(Native.body.css[IHTMLElement.HTMLElementEnum.iframe].style)
                    {
                        border = "1px dashed red",

                        width = "800px",
                        height = "400px"
                    };


                    await default(HopToIFrame);

                    // in the camera tracker we have semaphores after making the jump.

                    new IHTMLPre { () => "iframe: we made it..." + new { DateTime.Now.Millisecond } }.AttachToDocument();


                    // need intellisense to do some meaningful tests..
                    // lets switch from red to asus..

                    // now, can we try to lock up the ui?

                    new IHTMLButton { "iframe: lets do some slow thing and see if parent stays responsive.. " }.AttachToDocument().onclick += delegate
                    {
                        // simulate gpu timeout
                        Thread.Sleep(10000);

                        // are we a webview or appview?
                        // works as appwindow and webview are in different processes?
                    };

                    //// Cross-site iframes are currently hosted in the same process as their parent document, because we don't yet have support for hosting them in a different process.
                    //// https://www.chromium.org/developers/design-documents/site-isolation
                    //// ?  --site-per-process in chrome://flags
                    //// https://code.google.com/p/chromium/issues/detail?id=99379
                    //// http://stackoverflow.com/questions/11510483/will-a-browser-give-an-iframe-a-separate-thread-for-javascript
                    //// https://www.chromium.org/developers/design-documents/oop-iframes


                    //// this will block parent UI .. 
                    //Thread.Sleep(2000);

                    //// js thread seems to block the parent iframe too...
                    //// would it mean blockin for webgl also blocks parent?
                    //// this would make this approach rather useless?

                    //new IHTMLButton { "iframe: done. " }.AttachToDocument();

                    await new IHTMLButton { "iframe: doprograms " }.AttachToDocument().async.onclick;

                    // only multiple gpu cards seem to fix slow fps shaders
                    // or webgl2 will allow async program calls...
                    doprograms();


                }
            );
        }

        // seems like switching to iframe,
        // does not keep parent responsive.

        // framerate is also shared between iframe and parent.

        // and reload may cause continuty issues
        // gpu reload seems to be tab based..
        // even having multiple tabs, will kill webgl on both when crashed
        // having two ips wont help either.
        // so the true multishader

        // https://www.khronos.org/webgl/public-mailing-list/archives/1104/msg00041.php

        // http://www.anandtech.com/show/9124/amd-dives-deep-on-asynchronous-shading
        // https://code.google.com/p/chromium/issues/detail?id=249391
        // http://toji.github.io/shader-perf/
        // https://plus.google.com/+BrandonJonesToji/posts/4ERHkicC5Ny

        // or can we atleast move the program link time?

        // the only performance gain we could get is to batch link all shaders, keep responsive ui and then wait for it to complete.
        // await delayed shaders?
        // so it is rather useless until iframe has its own gpu blocking chain...
        // is there a way to ask if the deffered link or comple is ready?

        static void doprograms()
        {
            Native.body.style.overflow = IStyle.OverflowEnum.hidden;
            Native.body.style.margin = "0px";

            Native.body.Clear();




            // Severity	Code	Description	Project	File	Line	Source
            //Error Metadata file 'Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyPrograms\ChromeShaderToyPrograms\bin\Debug\ChromeShaderToyPrograms.exe' could not be found   WebGLIFrameBuffer Z:\jsc.svn\examples\javascript\WebGL\WebGLIFrameBuffer\CSC Build



            var gl = new WebGLRenderingContext(alpha: true);

            if (gl == null)
            {

                new IHTMLPre {
                    // https://code.google.com/p/chromium/issues/detail?id=294207
                    "Rats! WebGL hit a snag.",

                    //new IHTMLAnchor { href = "about:gpu", innerText = "about:gpu" }
                }.AttachToDocument();
                return;
            }


            Native.body.style.backgroundColor = "blue";

            gl.oncontextlost += delegate
            {
                Native.body.style.backgroundColor = "red";

                // reload?

            };

            //gl.canvas.async.oncont

            var combo = new IHTMLSelect().AttachToDocument();

            combo.style.position = IStyle.PositionEnum.absolute;
            combo.style.left = "0px";
            combo.style.top = "0px";
            //combo.style.right = "0px";
            combo.style.width = "100%";

            combo.style.backgroundColor = "rgba(255,255,255,0.5)";
            //combo.style.backgroundColor = "rgba(255,255,0,0.5)";
            //combo.style.background = "linear-gradient(to bottom, rgba(255,255,255,0.5 0%,rgba(255,255,255,0.0 100%))";
            combo.style.border = "0px solid transparent";
            combo.style.fontSize = "large";
            combo.style.paddingLeft = "1em";
            combo.style.fontFamily = IStyle.FontFamilyEnum.Verdana;
            combo.style.cursor = IStyle.CursorEnum.pointer;



            //var mAudioContext = new AudioContext();


            var c = gl.canvas.AttachToDocument();

            #region onresize
            new { }.With(
                async delegate
                {
                    do
                    {
                        c.width = Native.window.Width;
                        c.height = Native.window.Height;
                        c.style.SetSize(c.width, c.height);
                    }
                    while (await Native.window.async.onresize);
                }
            );
            #endregion



            #region CaptureMouse
            var mMouseOriX = 0;
            var mMouseOriY = 0;
            var mMousePosX = 0;
            var mMousePosY = 0;

            c.onmousedown += async ev =>
            {
                mMouseOriX = ev.CursorX;
                //mMouseOriY = ev.CursorY;
                mMouseOriY = c.height - ev.CursorY;

                mMousePosX = mMouseOriX;
                mMousePosY = mMouseOriY;

                // why aint it canvas?
                //ev.Element
                //ev.CaptureMouse();

                // using ?
                ev.Element.requestPointerLock();
                await ev.Element.async.onmouseup;
                Native.document.exitPointerLock();

                mMouseOriX = -Math.Abs(mMouseOriX);
                mMouseOriY = -Math.Abs(mMouseOriY);
            };

            //c.ontouchmove += 

            c.onmousemove += ev =>
            {
                if (ev.MouseButton == IEvent.MouseButtonEnum.Left)
                {
                    mMousePosX += ev.movementX;
                    mMousePosY += ev.movementY;
                }
            };

            c.onmousewheel += ev =>
            {
                ev.preventDefault();
                ev.stopPropagation();

                mMousePosY += 3 * ev.WheelDirection;
            };

            #endregion



            // http://www.wufoo.com/html5/attributes/05-list.html
            // http://www.w3schools.com/tags/att_input_list.asp
            //uiauto.datalist1.EnsureID();
            //uiauto.search.list = uiauto.datalist1.id;
            //uiauto.datalist1.id = "datalist1";
            //uiauto.search.list = "datalist1";
            //new IHTMLPre { new { uiauto.search.list, uiauto.datalist1.id } }.AttachToDocument();

            var sw = Stopwatch.StartNew();


            new IHTMLOption { value = "", innerText = $"{ChromeShaderToyPrograms.References.programs.Count} shaders available" }.AttachTo(combo);


            // should bind the selection to uri and reload if gpu crashes.

            #region can we have a next button?
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150807/shadertoy
            new IHTMLButton { "next" }.AttachToDocument().With(
                next =>
                {
                    new IStyle(next)
                    {
                        position = IStyle.PositionEnum.absolute,
                        right = "1em",
                        top = "2em",
                        bottom = "1em",
                        padding = "4em"
                    };

                    next.onclick += delegate
                    {
                        var n = combo.selectedIndex + 1;
                        Console.WriteLine(new { n });
                        combo.selectedIndex = n;
                    };
                }
            );
            #endregion



            ChromeShaderToyColumns.Library.ShaderToy.EffectPass pip = null;

            // http://stackoverflow.com/questions/25289390/html-how-to-make-input-type-list-only-accept-a-list-choice
            ChromeShaderToyPrograms.References.programs.Keys.WithEachIndex(
                async (key, index) =>
                {
                    var text = (1 + index) + " of " + ChromeShaderToyPrograms.References.programs.Count + " " + key.SkipUntilIfAny("ChromeShaderToy").Replace("By", " by ");

                    var option = new IHTMLOption { value = key, innerText = text }.AttachTo(combo);

                    await Native.window.async.onframe;

                    // we are about to create 100 objects. does it have any impact to UI?
                    var frag = ChromeShaderToyPrograms.References.programs[key]();
                    var len = frag.ToString().Length;

                    option.innerText = text + " " + new
                    {
                        //frame,
                        //load = load.ElapsedMilliseconds + "ms ",

                        frag = len + "bytes ",
                        // a telemetry to track while running on actual hardware
                        //fragGPU = pass0.xCreateShader.fsTranslatedShaderSource.Length + " bytes"
                    };

                    // cant we get it if we manually set it?
                    await option.async.onselect;
                    await Native.window.async.onframe;

                    var load = Stopwatch.StartNew();

                    var pass0 = new ChromeShaderToyColumns.Library.ShaderToy.EffectPass(
                        gl: gl,
                        precission: ChromeShaderToyColumns.Library.ShaderToy.DetermineShaderPrecission(gl),
                        supportDerivatives: gl.getExtension("OES_standard_derivatives") != null
                    );
                    pass0.MakeHeader_Image();
                    pass0.NewShader_Image(frag);

                    load.Stop();

                    new { }.With(
                        async delegate
                        {
                            while (await option.async.ondeselect)
                            {
                                pip = pass0;

                                await option.async.onselect;
                            }
                        }
                    );

                    var framesInSecond = 0;
                    var theSecond = Stopwatch.StartNew();

                    var frame = 0;
                    do
                    {
                        frame++;
                        framesInSecond++;

                        if (theSecond.ElapsedMilliseconds >= 1000)
                        {
                            //option.innerText = key + new { frame };
                            option.innerText = text + " " + new
                            {
                                //frame,
                                framesInSecond,
                                load = load.ElapsedMilliseconds + "ms ",

                                frag = len + "bytes ",
                                // a telemetry to track while running on actual hardware
                                fragGPU = pass0.xCreateShader.fsTranslatedShaderSource.Length + " bytes"
                            };

                            framesInSecond = 0;
                            //theSecond.Restart();
                            theSecond = Stopwatch.StartNew();
                        }


                        // can we scale?
                        pass0.Paint_Image(
                            sw.ElapsedMilliseconds / 1000.0f,

                            mMouseOriX,
                            mMouseOriY,
                            mMousePosX,
                            mMousePosY,

                            zoom: 1.0f
                        );

                        if (pip != null)
                        {
                            // can we scale?
                            pip.Paint_Image(
                                sw.ElapsedMilliseconds / 1000.0f,

                                mMouseOriX,
                                mMouseOriY,
                                mMousePosX,
                                mMousePosY,

                                zoom: 0.10f
                            );

                        }

                        // what does it do?
                        gl.flush();

                        // wither we are selected or we are pip?
                        await option.async.selected;
                    }
                    while (await Native.window.async.onframe);

                }
            );


        }

    }
}

//creating stack rewriter..
//will override Ldarg_0
//stack rewriter needs to store struct. can we create new byref struct parameters?