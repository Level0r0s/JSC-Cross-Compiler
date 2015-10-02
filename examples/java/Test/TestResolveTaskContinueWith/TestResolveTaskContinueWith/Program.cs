using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Script]
[assembly: ScriptTypeFilter(ScriptType.Java)]

namespace ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks
{
    [Script(Implements = typeof(global::System.Action<>))]
    internal partial class __Action<T>
    {

    }

    [Script(Implements = typeof(global::System.Threading.Tasks.Task))]
    internal partial class __Task
    {

        public static Task<TResult[]> WhenAll<TResult>(params Task<TResult>[] tasks)
        {
            // tasks[0].ContinueWith_06000004(null);
            tasks[0].ContinueWith(default(Action<Task<TResult>>));

            return null;
        }

    }

    [Script(Implements = typeof(global::System.Threading.Tasks.Task<>))]
    internal partial class __Task<TResult> : __Task
    {

        //  public final  __Task ContinueWith_06003dd3(
        public Task ContinueWith(Action<Task<TResult>> continuationAction)
        {
            return null;
        }

        //public Task ContinueWith(Action<Task<TResult>> continuationAction, object __needsuffix)
        //{
        //    return null;
        //}


        // public final  __Task ContinueWith_06000005(
        public Task ContinueWith(Action<object> continuationAction__needsuffix)
        {
            return null;
        }
    }
}


namespace TestResolveTaskContinueWith
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
