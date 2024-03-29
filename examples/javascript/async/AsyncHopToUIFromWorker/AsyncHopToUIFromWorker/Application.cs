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
using AsyncHopToUIFromWorker;
using AsyncHopToUIFromWorker.Design;
using AsyncHopToUIFromWorker.HTML.Pages;
using TestSwitchToServiceContextAsync;
using System.Threading;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks;
using System.Diagnostics;

namespace AsyncHopToUIFromWorker
{
    #region HopToThreadPoolAwaitable
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/CryptoStream.cs
    // simple awaitable that allows for hopping to the thread pool
    struct HopToThreadPoolAwaitable : System.Runtime.CompilerServices.INotifyCompletion
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150401
        // X:\jsc.svn\examples\javascript\Test\TestHopToThreadPoolAwaitable\TestHopToThreadPoolAwaitable\Application.cs

        public HopToThreadPoolAwaitable GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }
        public static Action<Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation) { VirtualOnCompleted(continuation); }
        //public void OnCompleted(Action continuation) { Task.Run(continuation); }
        public void GetResult() { }
    }
    #endregion

    #region HopToUIAwaitable
    // Z:\jsc.svn\examples\javascript\async\AsyncHopToUIFromWorker\AsyncHopToUIFromWorker\Application.cs
    public struct HopToUIAwaitable : System.Runtime.CompilerServices.INotifyCompletion
    {
        // basically we have to hibernate the current state to resume
        public HopToUIAwaitable GetAwaiter() { return this; }
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
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/20151102/csv
        // Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCaptureAE\Application.cs




        static Application()
        {
            #region document
            if (Native.document != null)
            {
                // patch the awaiter..
                Console.SetOut(new xConsole());


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
                HopToThreadPoolAwaitable.VirtualOnCompleted = continuation =>
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

                    HopToUIAwaitable.VirtualOnCompleted = continuation =>
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150401
            Console.Clear();

            #region start worker
            new IHTMLButton { "start worker" }.AttachTo(Native.document.documentElement).onclick +=
                async delegate
                {
                    //Native.body.Clear();
                    Console.Clear();

                    Console.WriteLine("about to start a new worker..");
                    // could we do a start thread, with semaphore slim?

                    //var s = new System.Threading.SemaphoreSlim(1);

                    var x = await Task.Run(
                        async delegate
                        {

                            Console.WriteLine("in another thread.. " + new { Thread.CurrentThread.ManagedThreadId });



                            return "done";
                        }
                    );

                    Console.WriteLine(new { x });

                };
            #endregion

#if !FHopToThreadPoolAwaitable
            #region hop to worker, do progress
            new IHTMLButton { "hop to worker, do progress" }.AttachTo(Native.document.documentElement).onclick +=
                async delegate
                {
                    // not working for 2012?

                    //Native.body.Clear();
                    Console.Clear();

                    Console.WriteLine("about to start a new worker..");
                    // could we do a start thread, with semaphore slim?

                    //var s = new System.Threading.SemaphoreSlim(1);

                    // server could support progress if it was running on a webrtc stream?
                    //IProgress<string> uiProgress = new System.Progress<string>(
                    //    (string text) =>
                    //    {
                    //        new IHTMLPre { text }.AttachToDocument().style.backgroundColor = "yellow";
                    //    }
                    //);
                    // if we were to reference Report as a delegate, it would need to be pinned and marked as owned by progress?

                    //Func<string> GetString = () => "hello world";

                    await default(HopToThreadPoolAwaitable);


                    // can we pass around delegate pointers in roslyn?
                    //Console.WriteLine("in another thread.. " + new { Thread.CurrentThread.ManagedThreadId, GetString });
                    Console.WriteLine("in another thread.. " + new { Thread.CurrentThread.ManagedThreadId });

                    // can we alread do progress?

                    //uiProgress.Report("worker to ui via progress");


                    //about to start a new worker..
                    //enter InternalInitializeInlineWorker
                    //Task scope {{ MemberName = __1__state, IsString = false, IsNumber = true, IsDelegate = false, IsProgress = false, TypeIndex = null }}
                    //Task scope {{ MemberName = __t__builder, IsString = false, IsNumber = false, IsDelegate = false, IsProgress = false, TypeIndex = type$NhpqFU35Cju_bC8JMN6oaCA }}
                    //Task scope {{ MemberName = __04000016__, IsString = false, IsNumber = false, IsDelegate = false, IsProgress = false, TypeIndex = null }}
                    //Task scope {{ MemberName = __4__this, IsString = false, IsNumber = false, IsDelegate = false, IsProgress = false, TypeIndex = type$Z_bJ0C5kF9zuuN0pxwoSkTg }}
                    //Task scope {{ MemberName = _uiProgress_5__1, IsString = false, IsNumber = false, IsDelegate = false, IsProgress = true, TypeIndex = type$_2LLVOAKnpzmSg7k8_axAcRA }}
                    //Task scope {{ MemberName = __u__1, IsString = false, IsNumber = false, IsDelegate = false, IsProgress = false, TypeIndex = type$_5ypLuXqv3zicwYxPrOc5CQ }}
                    //__worker_onfirstmessage: {{ ManagedThreadId = 10, href = https://192.168.1.196:25273/view-source#worker, MethodTargetTypeIndex = type$DlmmFVn_bKTOmYz8m7Kqf9g, MethodTargetType = ___ctor_b__1_2_d, MethodToken = jwsABpdwBjGQu09dvBXjxw, MethodType = FuncOfObjectToObject, stateTypeHandleIndex = null, stateType = null, state = [object Object], IsIProgress = false }}
                    //{{ xMember = __1__state, xMethodTargetObjectDataTypeIndex = null, xObjectData = 0, xIsProgress = null }}
                    //{{ xMember = __t__builder, xMethodTargetObjectDataTypeIndex = type$NhpqFU35Cju_bC8JMN6oaCA, xObjectData = [object Object], xIsProgress = null }}
                    //{{ xMember = __04000016__, xMethodTargetObjectDataTypeIndex = null, xObjectData = null, xIsProgress = null }}
                    //{{ xMember = __4__this, xMethodTargetObjectDataTypeIndex = type$Z_bJ0C5kF9zuuN0pxwoSkTg, xObjectData = [object Object], xIsProgress = null }}
                    //{{ xMember = _uiProgress_5__1, xMethodTargetObjectDataTypeIndex = type$_2LLVOAKnpzmSg7k8_axAcRA, xObjectData = null, xIsProgress = 1 }}
                    //{{ xMember = __u__1, xMethodTargetObjectDataTypeIndex = type$_5ypLuXqv3zicwYxPrOc5CQ, xObjectData = [object Object], xIsProgress = null }}
                    //worker Task Run function call
                    //in another thread.. {{ ManagedThreadId = 10 }}
                    //worker to ui via progress
                    //worker Task Run function has returned {{ value_Task = null, value_TaskOfT = null }}
                    //__Task.InternalStart inner complete {{ yield = {{ value = null }} }}
                };
            #endregion
#endif




#if !FHopToUIAwaitable
            #region hop to worker, do progress
			new IHTMLButton { "hop to worker, then hop to ui" }.AttachTo(Native.document.documentElement).onclick +=
				async delegate
				{
					//Native.body.Clear();
					Console.Clear();

					var sw = Stopwatch.StartNew();
					var uistring = "hello world. first time a string will hop to worker and back. can we do it for xml, and bytes soon too? " + new { Thread.CurrentThread.ManagedThreadId };

					Console.WriteLine("about to start a new worker..");

					// will it redownload source? cache it with or without  a service worker? yes, each time new SSL download. 800ms it seems
					await default(HopToThreadPoolAwaitable);

					uistring += " at worker! " + new { Thread.CurrentThread.ManagedThreadId };


					Console.WriteLine("in another thread.. " + new { Thread.CurrentThread.ManagedThreadId });

					// what if we want to hop back to the same pending state machine?
					// and then back to the thread?
					// we need to makr the thread as suspended for this statemachine for to be able to jump back?
					// or just terminate/recycle the thread?
					await default(HopToUIAwaitable);

					// back in ui yet? {{ uistring = hello world. first time a string will hop to worker and back. can we do it for xml, and bytes soon too? {{ ManagedThreadId = 1 }} at worker! {{ ManagedThreadId = 10 }} }}
					new IHTMLPre { "back in ui yet? " + new { uistring, sw.ElapsedMilliseconds } }.AttachToDocument().style.backgroundColor = "yellow";

					// can we resume?

					//about to start a new worker..
					//enter InternalInitializeInlineWorker
					//Task scope {{ MemberName = __1__state, IsString = false, IsNumber = true, IsDelegate = false, IsProgress = false, TypeIndex = null }}
					//Task scope {{ MemberName = __t__builder, IsString = false, IsNumber = false, IsDelegate = false, IsProgress = false, TypeIndex = type$NhpqFU35Cju_bC8JMN6oaCA }}
					//Task scope {{ MemberName = __04000021__, IsString = false, IsNumber = false, IsDelegate = false, IsProgress = false, TypeIndex = null }}
					//Task scope {{ MemberName = __4__this, IsString = false, IsNumber = false, IsDelegate = false, IsProgress = false, TypeIndex = type$Z_bJ0C5kF9zuuN0pxwoSkTg }}
					//Task scope {{ MemberName = __u__1, IsString = false, IsNumber = false, IsDelegate = false, IsProgress = false, TypeIndex = type$_5ypLuXqv3zicwYxPrOc5CQ }}
					//Task scope {{ MemberName = __u__2, IsString = false, IsNumber = false, IsDelegate = false, IsProgress = false, TypeIndex = type$QxQ8n4UOATqy1xnL7bpBtQ }}

					//__worker_onfirstmessage: {{ ManagedThreadId = 10, href = https://192.168.1.196:13946/view-source#worker, MethodTargetTypeIndex = type$PgZysaxv_bTu4GEkwmJdJrQ, MethodTargetType = ___ctor_b__1_5_d, MethodToken = jwsABpdwBjGQu09dvBXjxw, MethodType = FuncOfObjectToObject, stateTypeHandleIndex = null, stateType = null, state = [object Object], IsIProgress = false }}

					//{{ xMember = __1__state, xMethodTargetObjectDataTypeIndex = null, xObjectData = 0, xIsProgress = null }}
					//{{ xMember = __t__builder, xMethodTargetObjectDataTypeIndex = type$NhpqFU35Cju_bC8JMN6oaCA, xObjectData = [object Object], xIsProgress = null }}
					//{{ xMember = __04000021__, xMethodTargetObjectDataTypeIndex = null, xObjectData = null, xIsProgress = null }}
					//{{ xMember = __4__this, xMethodTargetObjectDataTypeIndex = type$Z_bJ0C5kF9zuuN0pxwoSkTg, xObjectData = [object Object], xIsProgress = null }}
					//{{ xMember = __u__1, xMethodTargetObjectDataTypeIndex = type$_5ypLuXqv3zicwYxPrOc5CQ, xObjectData = [object Object], xIsProgress = null }}
					//{{ xMember = __u__2, xMethodTargetObjectDataTypeIndex = type$QxQ8n4UOATqy1xnL7bpBtQ, xObjectData = [object Object], xIsProgress = null }}
					//worker Task Run function call
					//in another thread.. {{ ManagedThreadId = 10 }}
					//enter HopToUIAwaitable.VirtualOnCompleted
					//worker Task Run function has returned {{ value_Task = null, value_TaskOfT = null }}
					//__Task.InternalStart inner complete {{ yield = {{ value = null }} }}
				};
            #endregion

#endif


        }

    }
}

//about to start a new worker..
//enter InternalInitializeInlineWorker
//enter TaskExtensions.Unwrap
//__worker_onfirstmessage: {{ ManagedThreadId = 13, href = https://192.168.1.196:27829/view-source#worker, MethodTargetTypeIndex = type$Z_bJ0C5kF9zuuN0pxwoSkTg, MethodTargetType = __c, MethodToken = jgAABpkF9zuuN0pxwoSkTg, MethodType = FuncOfObjectToObject, stateTypeHandleIndex = null, stateType = null, state = [object Object], IsIProgress = false }}
//worker Task Run function call
//in another thread.. {{ ManagedThreadId = 13 }}
//worker Task Run function has returned {{ value_Task = {{ IsCompleted = true, Result = done }}, value_TaskOfT = {{ IsCompleted = true, Result = done }} }}
//worker Task Run ContinueWith {{ t = {{ IsCompleted = true, Result = done }} }}
//Task ContinueWithResult {{ responseCounter = 7, ResultTypeIndex = null, Result = done }}
//enter TaskExtensions.Unwrap Task<Task<TResult>> ContinueWith
//{{ x = done }}

//0200004b AsyncHopToUIFromWorker.Application+ctor>b__14>d__24+<MoveNext>06000043
//script: error JSC1000: *** stack is empty, invalid pop?
//script: error JSC1000: error at AsyncHopToUIFromWorker.Application+ctor>b__14>d__24+<MoveNext>06000043.<0020> nop.try,
// assembly: T:\AsyncHopToUIFromWorker.Application.exe
// type: AsyncHopToUIFromWorker.Application+ctor>b__14>d__24+<MoveNext>06000043, AsyncHopToUIFromWorker.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// offset: 0x0018
//  method:Int32 <0020> nop.try(<MoveNext>06000043, ctor>b__14>d__24 ByRef, AsyncHopToUIFromWorker.HopToThreadPoolAwaitable ByRef, AsyncHopToUIFromWorker.HopToThreadPoolAwaitable ByRef)
//*** Compiler cannot continue... press enter to quit.




