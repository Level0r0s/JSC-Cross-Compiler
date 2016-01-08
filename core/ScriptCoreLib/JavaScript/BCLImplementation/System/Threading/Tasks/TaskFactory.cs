using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/Tasks/TaskFactory.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Threading/Tasks/TaskFactory.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading.Tasks/TaskFactory.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\TaskFactory.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\TaskFactory.cs

    [Script(Implements = typeof(global::System.Threading.Tasks.TaskFactory))]
    public partial class __TaskFactory
    {

        public static implicit operator TaskFactory(__TaskFactory e)
        {
            return (TaskFactory)(object)e;
        }




        // X:\jsc.svn\examples\javascript\async\test\TestTaskRun\TestTaskRun\Application.cs
        public Task<TResult> StartNew<TResult>(Func<TResult> function)
        {
            //Console.WriteLine("enter StartNew");
            var x = new __Task<TResult>(function, state: null);

            x.Start();

            //Console.WriteLine("exit StartNew " + new { x });

            // can we unwrap it later?
            return x;
        }

        [Obsolete]
        public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state)
        {
            if (state == null)
            {
                // X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs
                // what happened? also, as interface cannot handle ull yet
                Debugger.Break();
            }



            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160108
            Console.WriteLine("enter __TaskFactory<TResult>.StartNew");

            var x = new __Task<TResult>(function, state);

            x.Start();

            return x;
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


        // public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state);
        public Task<TResult> StartNew(
            Func<object, TResult> function,
            object state
            )
        {
            if (state == null)
            {
                // X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs
                // what happened? also, as interface cannot handle ull yet
                Debugger.Break();
            }

            //Console.WriteLine("__TaskFactory<TResult>.StartNew");

            var x = new __Task<TResult>(function, state);

            x.Start();

            return x;
        }

        public Task<TResult> StartNew(
            Func<object, TResult> function,
            object state,
            CancellationToken c,
            TaskCreationOptions o,
            TaskScheduler s)
        {
            if (state == null)
            {
                // X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs
                // what happened? also, as interface cannot handle ull yet
                Debugger.Break();
            }

            //Console.WriteLine("__TaskFactory<TResult>.StartNew");
            var x = new __Task<TResult>();

            x.InternalInitializeInlineWorker(
                function,
                state,
                c,
                o,
                s
            );


            x.Start();

            return x;
        }
    }
}
