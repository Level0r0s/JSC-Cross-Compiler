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
            ).Wait();
        }
    }
}
