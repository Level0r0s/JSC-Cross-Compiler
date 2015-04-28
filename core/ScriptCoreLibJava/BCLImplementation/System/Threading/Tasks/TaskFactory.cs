using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/Tasks/TaskFactory.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading.Tasks/TaskFactory.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\TaskFactory.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\TaskFactory.cs

    [Script(Implements = typeof(global::System.Threading.Tasks.TaskFactory))]
    internal class __TaskFactory
    {
        public static implicit operator TaskFactory(__TaskFactory e)
        {
            return (TaskFactory)(object)e;
        }

        // X:\jsc.svn\examples\java\hybrid\async\Test\JVMCLRFactoryRun\JVMCLRFactoryRun\Program.cs

        public Task<TResult> StartNew<TResult>(Func<TResult> function)
        {
            // public Task(Func<TResult> function);

            var t = new Task<TResult>(function);
            //var t = new __Task<TResult>(function);

            t.Start();

            return t;
        }

        //Implementation not found for type import :
        //type: System.Threading.Tasks.TaskFactory
        //method: System.Threading.Tasks.Task`1[TResult] StartNew[TResult](System.Func`2[System.Object,TResult], System.Object)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!
    }
}
