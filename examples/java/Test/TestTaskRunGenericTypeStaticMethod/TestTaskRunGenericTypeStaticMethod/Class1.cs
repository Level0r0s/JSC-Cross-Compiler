using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestTaskRunGenericTypeStaticMethod
{
    public class Class1 : ScriptCoreLibJava.IAssemblyReferenceToken
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151002

        static void Invoke()
        {
            //   public static <TResult> ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks.__Task_1<TResult> Run_06000195(ScriptCoreLib.Shared.BCLImplementation.System.__Func_1<ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks.__Task_1<TResult>> function)


            // __Task.<__Task>Run_06000195(Class1.CS___9__CachedAnonymousMethodDelegate1).Wait();
            //Task.Run<Task>(
            Task.Run(
                function: delegate
                {

                    return Task.CompletedTask;
                }
            //).Wait();
            ).ContinueWith(
                t =>
                {
                    //  )).<__Task>ContinueWith(new ScriptCoreLib.Shared.BCLImplementation.System.__Func_2<__Task, __Task>(new Class1___c(), 

                    return Task.CompletedTask;
                }
            ).Wait();
        }
    }
}

//1>   Implementation not found for type import : 
//1>   type: System.Threading.Tasks.Task
//1>   method: System.Threading.Tasks.Task`1[TResult]
//ContinueWith[TResult](System.Func`2[System.Threading.Tasks.Task, TResult])
//1>   Did you forget to add the[Script] attribute? 
//1>   Please double check the signature! 