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
using TestHopToIFrameAndBack;
using TestHopToIFrameAndBack.Design;
using TestHopToIFrameAndBack.HTML.Pages;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

using TestSwitchToServiceContextAsync;

namespace TestHopToIFrameAndBack
{
    #region HopToParent
    // Z:\jsc.svn\examples\javascript\Test\TestHopToIFrameAndBack\Application.cs
    // Z:\jsc.svn\examples\javascript\Test\TestHopFromIFrame\TestHopFromIFrame\Application.cs
    public struct HopToParent : System.Runtime.CompilerServices.INotifyCompletion
    {
        // basically we have to hibernate the current state to resume
        public HopToParent GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }

        public static Action<HopToParent, Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation) { VirtualOnCompleted(this, continuation); }

        public void GetResult() { }
    }
    #endregion

    #region HopToIFrame
    // Z:\jsc.svn\examples\javascript\Test\TestHopFromIFrame\TestHopFromIFrame\Application.cs
    public struct HopToIFrame : System.Runtime.CompilerServices.INotifyCompletion
    {
        // basically we have to hibernate the current state to resume
        public HopToIFrame GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }

        public static Action<HopToIFrame, Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation) { VirtualOnCompleted(this, continuation); }

        public void GetResult() { }


        public IHTMLIFrame frame;
        public static explicit operator HopToIFrame(IHTMLIFrame frame)
        {
            return new HopToIFrame { frame = frame };
        }
    }
    #endregion

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        static Func<string, string> DecoratedString = x => x.Replace("-", "_").Replace("+", "_").Replace("<", "_").Replace(">", "_");


        // cuz state jumping wont restore in memory refs yet...
        static IHTMLIFrame iframe;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new { }.With(
              async delegate
              {
                  #region  magic
                  var isroot = Native.window.parent == Native.window.self;

                  new IHTMLPre { new { isroot } }.AttachToDocument();

                  if (!isroot)
                  {
                      #region HopToParent
                      HopToParent.VirtualOnCompleted = async (that, continuation) =>
                      {
                          // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151102/hoptoui
                          // the state is in a member variable?

                          var r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);

                          // should not be a zero state
                          // or do we have statemachine name clash?

                          new IHTMLPre {
                                "iframe about to jump to parent " + new { r.shadowstate.state }
                            }.AttachToDocument();

                          Native.window.parent.postMessage(r.shadowstate);

                          // we actually wont use the response yet..
                      };
                      #endregion


                      // start the handshake
                      // we gain intellisense, but the type is partal, likely not respawned, acivated, initialized 

                      new IHTMLPre {
                        "inside iframe awaiting state"
                      }.AttachToDocument();




                      var c = new MessageChannel();

                      c.port1.onmessage +=
                          m =>
                          {
                              var that = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)m.data;

                              new IHTMLPre {
                                            "inside iframe got state " +
                                            new { that.state }
                                      }.AttachToDocument();

                              // about to invoke

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

                                            return item.FullName == that.TypeName;
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
                                                   that.state
                                                );
                                           }

                                           var xStringField = that.StringFields.AsEnumerable().FirstOrDefault(
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

                              new IHTMLPre {
                                        "inside iframe invoke state"
                                    }.AttachToDocument();

                              NewStateMachineI.MoveNext();

                              // we can now send one hop back?
                          };

                      c.port1.start();
                      c.port2.start();

                      Native.window.parent.postMessage(null,
                          "*",
                          c.port2
                      );





                      return;
                  }

                  var lookup = new Dictionary<IHTMLIFrame, MessageEvent> { };

                  #region HopToIFrame
                  HopToIFrame.VirtualOnCompleted = async (that, continuation) =>
                  {
                      var m = default(MessageEvent);
                      var r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);

                      if (lookup.ContainsKey(that.frame))
                      {
                          new IHTMLPre {
                            "parent already nows the iframe..."
                        }.AttachToDocument();

                          m = lookup[that.frame];

                      }
                      else
                      {
                          new IHTMLPre {
                            "parent is awaiting handshake of the newly loaded iframe..."
                        }.AttachToDocument();


                          // X:\jsc.svn\examples\javascript\Test\TestSwitchToIFrame\TestSwitchToIFrame\Application.cs
                          //var m = await that.frame.contentWindow.async.onmessage;
                          m = await that.frame.async.onmessage;

                          lookup[that.frame] = m;



                          #region onmessage
                          that.frame.ownerDocument.defaultView.onmessage +=
                              e =>
                              {
                                  if (e.source != that.frame.contentWindow)
                                      return;

                                  var shadowstate = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)e.data;

                                  // are we jumping into a new statemachine?

                                  new IHTMLPre {
                                    "parent saw iframe instructions to jump back " + new { shadowstate.state }
                              }.AttachToDocument();

                                  // about to invoke

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

                                                return item.FullName == shadowstate.TypeName;
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
                                                       shadowstate.state
                                                    );
                                               }

                                               var xStringField = shadowstate.StringFields.AsEnumerable().FirstOrDefault(
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

                                  new IHTMLPre {
                                    "parent saw iframe instructions to jump back. invoking... "
                              }.AttachToDocument();

                                  NewStateMachineI.MoveNext();

                              };
                          #endregion

                      }




                      new IHTMLPre {
                            "parent is sending state after handshake..."
                        }.AttachToDocument();


                      m.postMessage(r.shadowstate);
                  };
                  #endregion


                  #endregion


                  var hostname = Native.document.location.host.TakeUntilIfAny(":");
                  var hostport = Native.document.location.host.SkipUntilOrEmpty(":");

                  //var hostport1 = Convert.ToInt32(hostport) + 1;
                  var hostport1 = Convert.ToInt32(hostport);
                  var host1 = hostname + ":" + hostport1;
                  var baseURI1 = "https://" + host1;


                  new IHTMLButton { "Identity " + new { baseURI1 } }.AttachToDocument().onclick += async e =>
                  {
                      #region statemachine fixup?
                      await Task.CompletedTask;
                      #endregion


                      new IHTMLPre { "will jump into the iframe to do io and then jump back here" }.AttachToDocument();

                      //var iframe = new IHTMLIFrame { src = baseURI1 }.AttachToDocument();
                      iframe = new IHTMLIFrame { src = baseURI1 }.AttachToDocument();

                      // if the iframe is on another port, ssl client certificate may be prompted
                      //await (HopTo)iframe;
                      await (HopToIFrame)iframe;

                      new IHTMLPre { "did we make it into iframe yet?" }.AttachToDocument();
                      // yes

                      await new IHTMLButton { "lets jump back " }.AttachToDocument().async.onclick;

                      new IHTMLPre { "lets jump back to parent..." }.AttachToDocument();

                      //await (HopTo)Native.window.parent;
                      await default(HopToParent);

                      new IHTMLPre { "did we make it into parent yet?" }.AttachToDocument();

                      // do we have iframe ref or are we a new statemachine?
                      await new IHTMLButton { "lets jump back to iframe " + new { iframe } }.AttachToDocument().async.onclick;

                      await (HopToIFrame)iframe;

                      Native.document.body.Clear();

                      new IHTMLPre { "did we make it into iframe this time?" }.AttachToDocument();
                      // yes

                      // TypeError: Cannot read property '__4__this' of null
                      //var color = await base.Color();

                      var color = await new ApplicationWebService { }.Color();

                      Native.body.style.backgroundColor = color;

                      await default(HopToParent);

                      new IHTMLPre { "did we make it into parent yet? " + new { color } }.AttachToDocument();

                      Native.body.style.backgroundColor = color;
                  };

              }
          );

        }

    }
}

//488: { SourceMethod = Void MoveNext(), i = [0x0091]
//brtrue.s   +0 -1 }
//1894:01:01:0f RewriteToAssembly error: System.ArgumentException: Value does not fall within the expected range.
//   at jsc.ILInstruction.ByOffset(Int32 i) in x:\jsc.internal.git\compiler\jsc\CodeModel\ILInstruction.cs:line 1188
