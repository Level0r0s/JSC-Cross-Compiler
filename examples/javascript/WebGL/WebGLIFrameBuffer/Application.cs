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




        public Application(IApp page)
        {
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


            #endregion

            // multishader variant here.


            // singleshader variant here.
            new { }.With(
                async delegate
                {
                    //Error CS7069  Reference to type 'TaskAwaiter<>' claims it is defined in 'mscorlib', but it could not be found WebGLIFrameBuffer Z:\jsc.svn\examples\javascript\WebGL\WebGLIFrameBuffer\Application.cs   48


                    await new IHTMLButton { "window: UI ready. lets hop? " + new { isroot } }.AttachToDocument().async.onclick;

                    // is iframe transparent?
                    //Native.body.style.backgroundColor = "yellow";


                    // red cmd start. example.com 
                    // to open chrome

                    // should actually fork to do multiple shaders at the same time...
                    new IHTMLPre { "window: hopping to iframe to prep shader..." }.AttachToDocument();

                    await default(HopToIFrame);

                    // in the camera tracker we have semaphores after making the jump.

                    new IHTMLPre { "window: inside iframe..." }.AttachToDocument();


                    // TypeError: Cannot read property 'BiMABnBwfzaSPDOtW7Fjxw' of null
                }
            );
        }

    }
}

//creating stack rewriter..
//will override Ldarg_0
//stack rewriter needs to store struct. can we create new byref struct parameters?