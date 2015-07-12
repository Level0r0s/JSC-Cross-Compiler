using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using ScriptCoreLib.Shared.BCLImplementation.System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs

    public partial class __Task
    {
        // X:\jsc.svn\examples\java\hybrid\JVMCLRSwitchToCLRContextAsync\JVMCLRSwitchToCLRContextAsync\Program.cs
        // X:\jsc.svn\examples\java\hybrid\JVMCLRHopToThreadPool\JVMCLRHopToThreadPool\Program.cs

        // X:\jsc.svn\examples\actionscript\FlashWorkerExperiment\FlashWorkerExperiment\ApplicationSprite.cs
        // what about AIR?

        // X:\jsc.svn\examples\javascript\async\test\TestTaskRun\TestTaskRun\Application.cs
        // X:\jsc.svn\examples\javascript\async\test\TaskAsyncTaskRun\TaskAsyncTaskRun\Application.cs

        public static Task Run(Func<Task> function)
        {
            //Console.WriteLine("enter __Task Run <- Task, then Unwrap");
            // X:\jsc.svn\examples\javascript\async\test\TestWorkerScopeProgress\TestWorkerScopeProgress\Application.cs


            return __TaskExtensions.Unwrap(
                __Task.InternalFactory.StartNew(function),
                Trace: "__Task Run <- Task, then Unwrap"
            );
        }

        public static Task<TResult> Run<TResult>(Func<Task<TResult>> function)
        {
            // X:\jsc.svn\examples\javascript\async\Test\TestUnwrap\TestUnwrap\Application.cs
            // X:\jsc.svn\examples\java\hybrid\async\Test\JVMCLRUnwrap\JVMCLRUnwrap\Program.cs

            //Console.WriteLine("enter __Task Run <- Task<TResult>, then Unwrap");


            return __TaskExtensions.Unwrap(
                __Task.InternalFactory.StartNew(function),
                Trace: "__Task Run <- Task<TResult>, then Unwrap"
            );
        }

        public static Task<TResult> Run<TResult>(Func<TResult> function)
        {
            //Console.WriteLine("enter __Task Run");

            // X:\jsc.svn\examples\javascript\WorkerMD5Experiment\WorkerMD5Experiment\Application.cs

            //new Task(
            return Task.Factory.StartNew(function);
        }


        //public static Task<TResult> Unwrap<TResult>(this Task<Task<TResult>> task);



        // used by?
        [Obsolete]
        [Script]
        public sealed class InternalTaskExtensionsScope
        {
            // scope sharing is required for roslyn/thread hopping

            [Obsolete("Special hint for JavaScript runtime, until scope sharing is implemented..")]
            public Action InternalTaskExtensionsScope_function;

            public void f()
            {
                this.InternalTaskExtensionsScope_function();
            }
        }

        [Obsolete("scope sharing, do we have it yet?")]
        public static Task Run(Action action)
        {
            // X:\jsc.svn\examples\javascript\async\AsyncHopToUIFromWorker\AsyncHopToUIFromWorker\Application.cs

            // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Extensions\TaskAsyncExtensions.cs
            // X:\jsc.svn\examples\javascript\Test\TestHopToThreadPoolAwaitable\TestHopToThreadPoolAwaitable\Application.cs
            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Extensions\TaskExtensions.cs

            //return Task.Factory.StartNew(action);




            // do we need InternalTaskExtensionsScope ?
            var xx = new InternalTaskExtensionsScope { InternalTaskExtensionsScope_function = action };


            var x = new __Task<object>();

            x.InternalInitializeInlineWorker(
                new Action(xx.f),
                //action,
                default(object),
                default(CancellationToken),
                default(TaskCreationOptions),
                TaskScheduler.Default
            );


            x.Start();

            return (Task<object>)x;
        }
    }
}
