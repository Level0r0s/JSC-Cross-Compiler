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

                    Console.WriteLine("enter HopToThreadPoolAwaitable.VirtualOnCompleted " + new { shadowstate.state });

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
                                HopToUIAwaitable = new
                                {
                                    // state to hop back

                                    shadowstate = default(ShadowIAsyncStateMachine)
                                }
                            };

                            data = (dynamic)e.data;

                            //if (data.HopToUIAwaitable )

                            if (data.HopToUIAwaitable == null)
                                return;


                            // time to hop back on continuation?

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("enter HopToThreadPoolAwaitable yield HopToUIAwaitable, resume state? " + new { data.HopToUIAwaitable.shadowstate.state });
                            Console.ForegroundColor = ConsoleColor.Black;


                            //enter HopToThreadPoolAwaitable yield HopToUIAwaitable
                            //worker Task Run function has returned {{ value_Task = null, value_TaskOfT = null }}
                            //__Task.InternalStart inner complete {{ yield = {{ value = null }} }}

                            // the worker should be in a suspended state, as we may want to jump back?

                            // 
                            MoveNext(data.HopToUIAwaitable.shadowstate);

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
                Console.WriteLine("about to enable HopToUIAwaitable...");

                Native.worker.onfirstmessage += e =>
                {
                    Console.WriteLine("enter onfirstmessage");

                    HopToUI.VirtualOnCompleted = continuation =>
                    {

                        // first jump out?
                        InternalInlineWorker.InternalOverrideTaskOfT = new TaskCompletionSource<object>().Task;

                        // post a message to the document 

                        // um. how can we signal that we are not done?


                        Action<ShadowIAsyncStateMachine> MoveNext = null;

                        // async dont like ref?
                        var shadowstate = ShadowIAsyncStateMachine.FromContinuation(continuation, ref MoveNext);

                        Console.WriteLine("enter HopToUIAwaitable.VirtualOnCompleted, postMessage " + new { shadowstate.state });


                        // postMessageAsync ? if ui wants to return, instead of restaring this thread?
                        e.postMessage(
                            new
                            {
                                HopToUIAwaitable = new
                                {
                                    // state to hop back

                                    shadowstate
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

                        var r = new StreamReader(new MemoryStream(bytes));

                        //{ ElapsedMilliseconds = 167, header = ﻿Jkn;Kohanimi;Keel;Kohanime staatus;Kohanime olek;Nimeobjekti liik;Lisainfo;Maakond,omavalitsus,asustusüksus;X;Y; }


                        var header = r.ReadLine();

                        new IHTMLPre { new { Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds, header } }.AttachToDocument();


                        var line1 = r.ReadLine();

                        new IHTMLPre { new { Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds, line1 } }.AttachToDocument();

                        // { ElapsedMilliseconds = 11929, header = ﻿Jkn;Kohanimi;Keel;Kohanime staatus;Kohanime olek;Nimeobjekti liik;Lisainfo;Maakond,omavalitsus,asustusüksus;X;Y; }
                        // { ElapsedMilliseconds = 162, line1 = 1;Lasteaia tänav;eesti;ametlik põhinimi;kehtiv;liikluspind;;Saare maakond, Kuressaare linn;6457819.16;410757.89; }


                        // are we to decode 20MB ?


                        new { }.With(
                            async delegate
                            {
                                { fixup: await Task.CompletedTask; }

                                var bytes1 = bytes;

                                await default(HopToWorker);

                                var output = "hello " + new { Thread.CurrentThread.ManagedThreadId, bytes1.Length };

                                // start the static line reader.

                                await default(HopToUI);

                                new IHTMLPre { "ready " + new { Thread.CurrentThread.ManagedThreadId, output } }.AttachToDocument();

                                // ready { ManagedThreadId = 1, output = hello { ManagedThreadId = 10, Length = 20851425 } }
                            }
                        );

                    }


                }
            );

        }

    }
}
