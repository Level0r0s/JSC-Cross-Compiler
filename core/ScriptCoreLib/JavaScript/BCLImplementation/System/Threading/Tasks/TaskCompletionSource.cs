using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/Tasks/TaskCompletionSource.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Threading/Tasks/TaskCompletionSource.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading.Tasks/TaskCompletionSource.cs
    // https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/threading/Tasks/TaskCompletionSource.cs

    // https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Threading/Tasks/TaskCompletionSource.cs
    // https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/Threading/Tasks/TaskCompletionSource.cs


    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\TaskCompletionSource.cs

    [Script(Implements = typeof(global::System.Threading.Tasks.TaskCompletionSource<>))]
    internal class __TaskCompletionSource<TResult>
    {
        // X:\jsc.svn\examples\javascript\async\Test\TestCompletedTask\TestCompletedTask\Application.cs

        // http://stackoverflow.com/questions/15316613/real-life-scenarios-for-using-taskcompletionsourcet

        public __Task<TResult> InternalTask;

        public Task<TResult> Task { get { return this.InternalTask; } }

        public __TaskCompletionSource()
        {
            this.InternalTask = new __Task<TResult> { InternalStart = null };
        }

        public void SetResult(TResult result)
        {
            this.InternalTask.InternalSetCompleteAndYield(result);
        }

        public void SetException(Exception exception)
        {
            // now what?
            // X:\jsc.svn\examples\java\async\test\TestFromException\TestFromException\Application.cs

            InternalTask.IsFaulted = true;
            InternalTask.Exception = new AggregateException(exception.Message);

            // will it cause it to be throw to the async catch?
            // need a test for that?
            // how does it work for CLR?
        }


        public override string ToString()
        {
            // Z:\jsc.svn\examples\javascript\test\TestTaskCompletionSourceToString\Application.cs
            // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\360\x360x83\Application.cs

            return "TaskCompletionSource " + new { this.InternalTask.IsCompleted };

        }
    }


}
