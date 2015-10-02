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
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.Start.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\Task\Task.Start.cs

    internal partial class __Task
    {
        // Z:\jsc.svn\examples\java\Test\TestTaskRunGenericTypeStaticMethod\TestTaskRunGenericTypeStaticMethod\Class1.cs

        public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction)
        {
            return null;
        }

        public Task ContinueWith(Action<Task> continuationAction)
        {
            return ContinueWith(continuationAction, default(TaskContinuationOptions));
        }

        public Task ContinueWith(Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
        {
            var u = new TaskCompletionSource<object>();

            InvokeWhenComplete(
                delegate
                {
                    continuationAction(this);

                    u.SetResult(null);
                }
            );


            return u.Task;
        }



        public void InvokeWhenComplete(Action e)
        {
            if (this.IsCompleted)
            {
                e();
                return;
            }

            InvokeWhenCompleteLater += e;
        }

        public Action InvokeWhenCompleteLater;

    }

    internal partial class __Task<TResult> : __Task
    {

        #region ContinueWith
        public Task ContinueWith(Action<Task<TResult>> continuationAction)
        {
            return ContinueWith(continuationAction, default(TaskContinuationOptions));
        }

        public Task ContinueWith(Action<Task<TResult>> continuationAction, TaskContinuationOptions continuationOptions)
        {
            var u = new TaskCompletionSource<object>();

            // X:\jsc.svn\examples\java\Test\JVMCLRTaskStartNew\JVMCLRTaskStartNew\Program.cs

            //y { ManagedThreadId = 8, IsCompleted = true, Result = done }
            //x { ManagedThreadId = 9, IsCompleted = true, Result = done }
            //{ ManagedThreadId = 9 } and then some

            InvokeWhenComplete(
                delegate
                {
                    // tested by ?
                    if (continuationOptions == TaskContinuationOptions.ExecuteSynchronously)
                    {
                        continuationAction(this);
                        u.SetResult(null);
                        return;
                    }

                    // shall we use threadpool instead?
                    new Thread(
                        delegate ()
                        {
                            continuationAction(this);
                            u.SetResult(null);
                        }
                    )
                    { IsBackground = true }.Start();
                }
            );

            return u.Task;
        }
        #endregion
    }

}
