using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Threading.Tasks;

namespace ScriptCoreLibNative.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/Tasks/Task.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading.Tasks/Task.cs

    // "X:\opensource\github\WootzJs\WootzJs.Runtime\Threading\Tasks\Task.cs"
    // X:\opensource\github\SaltarelleCompiler\Runtime\CoreLib\Threading\Tasks\Task.cs
    // http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.aspx

    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\Task.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Threading\Tasks\Task\Task.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Threading\Tasks\Task\Task.cs

    // http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.aspx
    [Script(Implements = typeof(global::System.Threading.Tasks.Task))]
    internal partial class __Task
    {
        //public AutoResetEvent WaitEvent = new AutoResetEvent(false);

        public void Wait()
        {
            // for now lets use tasks that always complete?
            // Z:\jsc.svn\examples\c\android\NDKUdpClient\xNativeActivity.cs



            // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\SemaphoreSlim.cs

            //if (this.IsCompleted)
            //    return;

            //WaitEvent.WaitOne();
        }
    }

    [Script(Implements = typeof(global::System.Threading.Tasks.Task<>))]
    internal partial class __Task<TResult> : __Task
    {
        public TResult Result { get; internal set; }




        public static implicit operator __Task<TResult>(Task<TResult> e)
        {
            return (__Task<TResult>)(object)e;
        }

        public static implicit operator Task<TResult>(__Task<TResult> e)
        {
            return (Task<TResult>)(object)e;
        }



    }
}
