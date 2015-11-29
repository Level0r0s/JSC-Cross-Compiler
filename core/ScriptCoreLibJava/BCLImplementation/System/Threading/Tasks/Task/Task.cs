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

        public override string ToString()
        {
            return "Task " + new { IsCompleted };
        }

        // we have a set of context switch tests. where are they.
        // X:\jsc.svn\examples\java\android\AndroidVibrationActivity\AndroidVibrationActivity\ApplicationActivity.cs


        // used by
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\Sockets\TcpListener.cs

        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140216/task
        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxAndroidOrderByThenGroupBy\ApplicationWebService.cs


        #region Factory
        public static __TaskFactory InternalFactory
        {
            get
            {
                return new __TaskFactory();
            }
        }


        public static TaskFactory Factory
        {
            get
            {
                return InternalFactory;
            }
        }
        #endregion

        // X:\jsc.svn\examples\java\hybrid\async\Test\JVMCLRFactoryRun\JVMCLRFactoryRun\Program.cs

        public TaskStatus Status { get; set; }

        public bool IsCompleted { get; set; }

        public static __YieldAwaitable Yield()
        {
            // X:\jsc.svn\examples\javascript\Test\TestAsyncAssignArrayToEnumerable\TestAsyncAssignArrayToEnumerable\Application.cs
            // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRAsync\TestJVMCLRAsync\Program.cs

            return new __YieldAwaitable { };
        }


        public static implicit operator Task(__Task e)
        {
            return (Task)(object)e;
        }





        public AutoResetEvent WaitEvent = new AutoResetEvent(false);

        public void Wait()
        {
            // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\SemaphoreSlim.cs

            if (this.IsCompleted)
                return;

            WaitEvent.WaitOne();
        }






    }

    [Script(Implements = typeof(global::System.Threading.Tasks.Task<>))]
    internal partial class __Task<TResult> : __Task
    {



        public static implicit operator __Task<TResult>(Task<TResult> e)
        {
            return (__Task<TResult>)(object)e;
        }

        public static implicit operator Task<TResult>(__Task<TResult> e)
        {
            return (Task<TResult>)(object)e;
        }







        // script: error JSC1000: Java : class import: no implementation for System.Threading.Tasks.TaskFactory`1 at ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks.__Task`1
        public static TaskFactory<TResult> Factory
        {
            get
            {
                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150911/mysql
                // tested by?

                return null;
            }
        }

    }


    //Implementation not found for type import :
    //type: Task`1
    //method: System.Threading.Tasks.TaskFactory`1[TResult] get_Factory()
    //Did you forget to add the [Script] attribute?
    //Please double check the signature!
}
