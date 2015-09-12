using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/Tasks/TaskFactory.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading.Tasks/TaskFactory.cs

    // z:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\TaskFactory.cs
    // z:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\TaskFactory.cs

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



        public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150911/mysql

            // tested by?

            return null;
        }
    }

    [Script(Implements = typeof(global::System.Threading.Tasks.TaskFactory<>))]
    internal class __TaskFactory<TResult>
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150911/mysql

        public Task<TResult> StartNew(
      Func<TResult> function
      )
        {
            throw new NotImplementedException();
        }


        public Task<TResult> StartNew(
            Func<object, TResult> function,
            object state
            )
        {
            throw new NotImplementedException();
        }


        public Task<TResult> StartNew(
            Func<object, TResult> function,
            object state,
            CancellationToken c,
            TaskCreationOptions o,
            TaskScheduler s)
        {
            throw new NotImplementedException();

        }
    }

}
