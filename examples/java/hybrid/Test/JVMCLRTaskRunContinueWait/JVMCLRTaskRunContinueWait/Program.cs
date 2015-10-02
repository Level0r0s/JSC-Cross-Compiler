using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRTaskRunContinueWait
{
    public class Class1
    {
        public Class1()
        {
        }
    }

    public class Class1<T>
    {
    }


    static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // 2012desktop?
            // scriptcorelib to be rebuilt with 2012

            //- javac
            //"X:\Program Files (x86)\Java\jdk1.7.0_79\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRTaskRunContinueWait\Program.java
            //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task.java:219: error: cannot find symbol
            //            task_11.ContinueWith_060001ab(new ScriptCoreLib.Shared.BCLImplementation.System.__Action_1<ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks.__Task_1<TResult>>(classe_12,
            //                   ^
            //  symbol:   method ContinueWith_060001ab(__Action_1<__Task_1<TResult>>)

            Task.Run(
               function: delegate
                   {
                       Console.WriteLine("Run");

                       return Task.CompletedTask;
                   }
                //).Wait();
               ).ContinueWith(
                   t =>
                   {
                       //  )).<__Task>ContinueWith(new ScriptCoreLib.Shared.BCLImplementation.System.__Func_2<__Task, __Task>(new Class1___c(), 
                       Console.WriteLine("ContinueWith");

                       return Task.CompletedTask;
                   }
               ).Wait();

            Console.WriteLine("Wait");

            CLRProgram.CLRMain();
        }


    }



    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            MessageBox.Show("click to close");

        }
    }


}
