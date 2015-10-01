using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using ScriptCoreLibJava.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks
{
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs

    internal partial class __Task
    {
        // X:\jsc.svn\examples\java\hybrid\JVMCLRSwitchToCLRContextAsync\JVMCLRSwitchToCLRContextAsync\Program.cs

        // X:\jsc.svn\examples\javascript\android\Test\TestPINDialog\TestPINDialog\ApplicationWebService.cs
        // X:\jsc.svn\examples\java\hybrid\JVMCLRHopToThreadPool\JVMCLRHopToThreadPool\Program.cs
        // do we have a dispatcher in jvm yet?



        //public static Task<TResult> Run<TResult>(Func<Task<TResult>> function);
        //public static Task Run(Func<Task> function);
        //public static Task<TResult> Run<TResult>(Func<TResult> function);



        //method: System.Threading.Tasks.Task Run(System.Action)
        // X:\jsc.svn\examples\java\hybrid\test\JVMCLRWhenAll\JVMCLRWhenAll\Program.cs
        public static Task Run(Action y)
        {
            // on appengine we need to do special thread creation it seems.
            // X:\jsc.svn\core\ScriptCoreLibJava.AppEngine\ScriptCoreLibJava.AppEngine\Extensions\ThreadManagerExtensions.cs
            // X:\jsc.svn\examples\c\Test\TestTaskRun\TestTaskRun\Program.cs

            var t = new TaskCompletionSource<object>();

            new Thread(
                delegate()
                {
                    // in java it we can keep our call refs.

                    y();

                    // signal ready?
                    t.SetResult(null);
                }
            ).Start();


            return t.Task;
        }

        public static Task<TResult> Run<TResult>(Func<Task<TResult>> function)
        {
            // X:\jsc.svn\examples\java\hybrid\async\Test\JVMCLRUnwrap\JVMCLRUnwrap\Program.cs
            return __Task.InternalFactory.StartNew(function).Unwrap();
        }

        // Z:\jsc.svn\examples\java\hybrid\JVMCLRWSDLMID\Program.cs
        //public static Task Run<TResult>(Func<Task> function)
        public static Task Run(Func<Task> function)
        {
            // X:\jsc.svn\examples\java\hybrid\async\Test\JVMCLRUnwrap\JVMCLRUnwrap\Program.cs
            return __Task.InternalFactory.StartNew(function).Unwrap();
        }


    }
}
