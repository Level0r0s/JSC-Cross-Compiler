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
using DropLESTToDisplay;
using DropLESTToDisplay.Design;
using DropLESTToDisplay.HTML.Pages;
using System.Diagnostics;
using System.IO;
using System.Threading;
using TestSwitchToServiceContextAsync;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace DropLESTToDisplay
{
    #region HopToThreadPoolAwaitable
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/CryptoStream.cs
    // simple awaitable that allows for hopping to the thread pool
    //struct HopToThreadPoolAwaitable : System.Runtime.CompilerServices.INotifyCompletion
    struct HopToWorker : System.Runtime.CompilerServices.INotifyCompletion
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150401
        // X:\jsc.svn\examples\javascript\Test\TestHopToThreadPoolAwaitable\TestHopToThreadPoolAwaitable\Application.cs

        public HopToWorker GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }
        public static Action<Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation) { VirtualOnCompleted(continuation); }
        //public void OnCompleted(Action continuation) { Task.Run(continuation); }
        public void GetResult() { }
    }
    #endregion

    #region HopToUIAwaitable
    // Z:\jsc.svn\examples\javascript\async\AsyncHopToUIFromWorker\AsyncHopToUIFromWorker\Application.cs
    //public struct HopToUIAwaitable : System.Runtime.CompilerServices.INotifyCompletion
    public struct HopToUI : System.Runtime.CompilerServices.INotifyCompletion
    {
        // basically we have to hibernate the current state to resume
        public HopToUI GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }

        public static Action<Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation) { VirtualOnCompleted(continuation); }

        public void GetResult() { }
    }
    #endregion

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        static Func<string, string> DecoratedString = x => x.Replace("-", "_").Replace("+", "_").Replace("<", "_").Replace(">", "_");


        static Application()
        {
            #region document
            if (Native.document != null)
            {
                // patch the awaiter..
                //Console.SetOut(new xConsole());


                // also. all workers we will be creating will need to start to expect switch commands...
                // how can we tap in them?

                //__worker_onfirstmessage: {{ 
                // ManagedThreadId = 10, href = https://192.168.1.196:13946/view-source#worker, 
                // MethodTargetTypeIndex = type$PgZysaxv_bTu4GEkwmJdJrQ, 
                // MethodTargetType = ___ctor_b__1_5_d, 
                // MethodToken = jwsABpdwBjGQu09dvBXjxw, 
                // MethodType = FuncOfObjectToObject, 
                // stateTypeHandleIndex = null, 
                // stateType = null, 
                // state = [object Object], 
                // IsIProgress = false }}
                // 

                // can we start a task, yet also have special access for the posted messages?

                // what about threads that did not hop to background?
                // dont they want to hop to background?
                HopToWorker.VirtualOnCompleted = continuation =>
                {
                    Action<ShadowIAsyncStateMachine> MoveNext0 = null;

                    // async dont like ref?
                    var shadowstate = ShadowIAsyncStateMachine.FromContinuation(continuation, ref MoveNext0);
                    var MoveNext = MoveNext0;

                    Console.WriteLine("enter HopToWorker " + new { shadowstate.state });

                    // post a message to the document 
                    var xx = new __Task.InternalTaskExtensionsScope { InternalTaskExtensionsScope_function = continuation };


                    var x = new __Task<object>();

                    // reusing thread init for generic task start, although, resuming needs special implementation..
                    x.InternalInitializeInlineWorker(
                        new Action(xx.f),
                        //action,
                        default(object),
                        default(CancellationToken),
                        default(TaskCreationOptions),
                        TaskScheduler.Default,

                        yield: (worker, e) =>
                        {
                            // like operator for JSON?

                            //Console.WriteLine("enter HopToThreadPoolAwaitable InternalInitializeInlineWorker yield");

                            var data = new
                            {
                                vSetTitle = default(string),

                                HopToUIAwaitable = new
                                {
                                    // state to hop back

                                    shadowstate = default(ShadowIAsyncStateMachine)
                                }
                            };

                            data = (dynamic)e.data;

                            //if (data.HopToUIAwaitable )

                            if (data.vSetTitle != null)
                            {
                                Console.Title = data.vSetTitle;
                            }

                            if (data.HopToUIAwaitable == null)
                                return;


                            // time to hop back on continuation?

                            var that = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)data.HopToUIAwaitable.shadowstate;


                            //Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("work about to hop into ui " + new { that.state });
                            //Console.ForegroundColor = ConsoleColor.Black;


                            //enter HopToThreadPoolAwaitable yield HopToUIAwaitable
                            //worker Task Run function has returned {{ value_Task = null, value_TaskOfT = null }}
                            //__Task.InternalStart inner complete {{ yield = {{ value = null }} }}

                            // the worker should be in a suspended state, as we may want to jump back?

                            // 
                            //MoveNext(data.HopToUIAwaitable.shadowstate);



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

                            //new IHTMLPre {
                            //            "inside iframe invoke state"
                            //        }.AttachToDocument();

                            NewStateMachineI.MoveNext();

                        }
                    );


                    x.Start();


                };

                return;
            }
            #endregion


            #region worker
            if (Native.worker != null)
            {
                Console.WriteLine("about to enable HopToUI...");


                Native.worker.onfirstmessage += e =>
                {
                    Console.WriteLine("enter onfirstmessage");

                    ScriptCoreLib.JavaScript.BCLImplementation.System.__Console.vSetTitle = value =>
                    {
                        e.postMessage(
                          new
                          {
                              vSetTitle = value
                          }
                        );
                    };


                    HopToUI.VirtualOnCompleted = continuation =>
                    {
                        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151102/hoptoui

                        // first jump out?
                        InternalInlineWorker.InternalOverrideTaskOfT = new TaskCompletionSource<object>().Task;

                        // post a message to the document 

                        // um. how can we signal that we are not done?


                        //Action<ShadowIAsyncStateMachine> MoveNext = null;

                        // async dont like ref?
                        //var shadowstate = ShadowIAsyncStateMachine.FromContinuation(continuation, ref MoveNext);
                        var r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);

                        Console.WriteLine("enter HopToUI, postMessage " + new { r.shadowstate.state });


                        // postMessageAsync ? if ui wants to return, instead of restaring this thread?
                        e.postMessage(
                            new
                            {
                                HopToUIAwaitable = new
                                {
                                    // state to hop back

                                    r.shadowstate
                                }
                            }
                        );

                    };
                };


                return;
            }
            #endregion

        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // user gets to drop a csv
            // with X and Y fields.
            // where is our csv parser? assetslibary?


            #region test
            new { }.With(
                async delegate
                {
                    { fixup: await Task.CompletedTask; }

                    var bytes = new byte[] { 0xcc };

                    await new IHTMLButton { "test worker " + new { Thread.CurrentThread.ManagedThreadId, bytes = bytes.ToHexString() } }.AttachToDocument().async.onclick;

                    await default(HopToWorker);

                    var output = "hello " + new { Thread.CurrentThread.ManagedThreadId, bytes = bytes.ToHexString() };

                    Console.Title = "progress!";

                    await Task.Delay(500);

                    new { }.With(
                        async delegate
                        {
                            { fixup: await Task.CompletedTask; }

                            // is this supposed to work?
                            var output2 = "hello " + new { Thread.CurrentThread.ManagedThreadId };
                            await default(HopToUI);

                            new IHTMLPre { "progress... " + new { Thread.CurrentThread.ManagedThreadId, output2 } }.AttachToDocument();
                        }
                    );



                    await Task.Delay(500);



                    await default(HopToUI);

                    new IHTMLPre { "ready " + new { Thread.CurrentThread.ManagedThreadId, output } }.AttachToDocument();

                    // ready { ManagedThreadId = 1, output = hello { ManagedThreadId = 10 } }
                }
            );
            #endregion



            new { }.With(
                async delegate
                {



                    Native.document.documentElement.css.hover.style.backgroundColor = "pink";


                    //Native.document.documentElement.css.dragover

                    // while await ondrop ?
                    Native.document.documentElement.ondragover += ee =>
                    {
                        //ee.stopPropagation();
                        //ee.preventDefault();

                        //ee.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.

                        Native.document.documentElement.style.backgroundColor = "cyan";

                        // await leave? unless ondrop?
                    };

                    Native.document.documentElement.ondragleave += delegate
                    {
                        Native.document.documentElement.style.backgroundColor = "";
                    };



                    new IHTMLPre { "drop a file" }.AttachToDocument();

                    // { name = cncnet5.ini, size = 1985 }

                    //Native.document.documentElement.ondrop += e =>
                    var e = await Native.document.documentElement.async.ondrop;

                    Native.document.documentElement.style.backgroundColor = "yellow";



                    foreach (var f in e.dataTransfer.files.AsEnumerable())
                    {
                        new IHTMLPre { new { Thread.CurrentThread.ManagedThreadId, f.name, f.size } }.AttachToDocument();
                        // { name = download.csv, size = 20851425 }

                        var sw = Stopwatch.StartNew();

                        var bytes = await f.readAsBytes();

                        new IHTMLPre { new { Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds } }.AttachToDocument();
                        // { ElapsedMilliseconds = 72 }

                        //var m = new MemoryStream(bytes);
                        //var r = new StreamReader(m);

                        //var xstring = Encoding.UTF8.GetString(bytes);


                        ////{ name = download.csv, size = 20851425 }
                        ////{ ElapsedMilliseconds = 104 }
                        ////{ ElapsedMilliseconds = 5351, R1C1 = ﻿Jkn }
                        ////{ ElapsedMilliseconds = 5390, header = ﻿Jkn;Kohanimi;Keel;Kohanime staatus;Kohanime olek;Nimeobjekti liik;Lisainfo;Maakond,omavalitsus,asustusüksus;X;Y; }

                        //new IHTMLPre { new { sw.ElapsedMilliseconds, R1C1 = xstring.TakeUntilOrEmpty(";") } }.AttachToDocument();


                        //var r = new StringReader(xstring);


                        // script: error JSC1000: No implementation found for this native method, please implement [System.IO.StreamReader.get_BaseStream()]

                        //{
                        //    var r = new StreamReader(new MemoryStream(bytes));

                        //    //{ ElapsedMilliseconds = 167, header = ﻿Jkn;Kohanimi;Keel;Kohanime staatus;Kohanime olek;Nimeobjekti liik;Lisainfo;Maakond,omavalitsus,asustusüksus;X;Y; }


                        //    var header = r.ReadLine();

                        //    new IHTMLPre { new { Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds, header } }.AttachToDocument();


                        //    for (int i = 0; i < 10; i++)
                        //    {
                        //        var line1 = r.ReadLine();
                        //        var null1 = line1 == null;
                        //        var empty1 = string.IsNullOrEmpty(line1);
                        //        var length1 = -1;
                        //        if (line1 != null)
                        //            length1 = line1.Length;


                        //        //{ empty1 = false, line1 = 153405;Pargimaja;eesti;ametlik põhinimi;kehtiv;maaüksus, krunt, talu;;Järva maakond, Albu vald, Kaalepi küla;6551431.62;596555.84;, Position = 1008, Length = 1008 }
                        //        //{ empty1 = false, line1 = , Position = 1008, Length = 1008 }

                        //        new IHTMLPre { new { null1, empty1, length1, line1, r.BaseStream.Position, r.BaseStream.Length } }.AttachToDocument();
                        //    }


                        //}

                        // { ElapsedMilliseconds = 11929, header = ﻿Jkn;Kohanimi;Keel;Kohanime staatus;Kohanime olek;Nimeobjekti liik;Lisainfo;Maakond,omavalitsus,asustusüksus;X;Y; }
                        // { ElapsedMilliseconds = 162, line1 = 1;Lasteaia tänav;eesti;ametlik põhinimi;kehtiv;liikluspind;;Saare maakond, Kuressaare linn;6457819.16;410757.89; }


                        // are we to decode 20MB ?


                        new { }.With(
                            async delegate
                            {
                                // jsc switch rewriter should inclide it automatically to enable better hopping
                                { fixup: await Task.CompletedTask; }

                                var bytes1 = bytes;

                                await default(HopToWorker);


                                // start the static line reader.

                                WorkerReader = new StreamReader(new MemoryStream(bytes1));

                                // now before we jump back to ui. lets start reading the lines...


                                // working... { ManagedThreadId = 1, output = hello { ManagedThreadId = 11, Length = 20851425, WorkerReaderLineCount = 0 } }

                                new { }.With(
                                    async delegate
                                    {
                                        { fixup: await Task.CompletedTask; }

                                        var output = "hello " + new { Thread.CurrentThread.ManagedThreadId };
                                        await default(HopToUI);
                                        WorkerUI = new IHTMLPre { "working... " + new { Thread.CurrentThread.ManagedThreadId, output } }.AttachToDocument();

                                        // cant jump back can we?
                                    }
                                );




                                var header = WorkerReader.ReadLine();

                                var line1 = WorkerReader.ReadLine();

                                var sw1 = Stopwatch.StartNew();

                                WorkerReaderLineCount = 0;

                                var HasLine = !string.IsNullOrEmpty(line1);

                                while (HasLine)
                                {
                                    WorkerReaderLineCount++;

                                    //await Task.Delay(33);

                                    //Console.WriteLine(new { WorkerReaderLineCount });


                                    if (WorkerReaderLineCount % 500 == 1)
                                    {
                                        Console.Title = WorkerReaderLineCount + " in " + sw1.ElapsedMilliseconds + "ms";
                                    }


                                    if (WorkerReaderLineCount % 300 == 1)
                                    {
                                        new { }.With(
                                            async delegate
                                            {
                                                { fixup: await Task.CompletedTask; }

                                                var output = "working... " + WorkerReaderLineCount + " in " + sw1.ElapsedMilliseconds + "ms " + new { HasLine } + ":" + line1;
                                                await default(HopToUI);
                                                WorkerUI.innerText = output;
                                            }
                                        );
                                    }

                                    //await Task.Delay(3);

                                    line1 = WorkerReader.ReadLine();
                                    HasLine = !string.IsNullOrEmpty(line1);
                                }


                                {

                                    var output = "done " + new { Thread.CurrentThread.ManagedThreadId, WorkerReaderLineCount, sw1.ElapsedMilliseconds };
                                    await default(HopToUI);
                                    WorkerUI.innerText = "done... " + new { Thread.CurrentThread.ManagedThreadId, output };

                                    // done... { ManagedThreadId = 1, output = done { ManagedThreadId = 10, WorkerReaderLineCount = 153411 } }
                                }

                            }
                        );

                    }


                }
            );

        }

        static IHTMLPre WorkerUI;
        static StreamReader WorkerReader;
        static int WorkerReaderLineCount;
    }


}
