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
            Console.WriteLine(
                new
                {
                    typeof(object).AssemblyQualifiedName
                }
                );

            // 2012desktop?
            // scriptcorelib to be rebuilt with 2012

            //- javac
            //"X:\Program Files (x86)\Java\jdk1.7.0_79\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRTaskRunContinueWait\Program.java
            //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task.java:219: error: cannot find symbol
            //            task_11.ContinueWith_060001ab(new ScriptCoreLib.Shared.BCLImplementation.System.__Action_1<ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks.__Task_1<TResult>>(classe_12,
            //                   ^
            //  symbol:   method ContinueWith_060001ab(__Action_1<__Task_1<TResult>>)

            try
            {
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
            }
            catch (Exception err)
            {
                //{ Message = , StackTrace = java.lang.NullPointerException
                //        at JVMCLRTaskRunContinueWait.Program.main(Program.java:70)

                // { err = java.lang.NullPointerException }
                Console.WriteLine(new { err.Message, err.StackTrace });

            }
            CLRProgram.CLRMain();
        }

        //{ AssemblyQualifiedName = java.lang.Object, rt }
        //Run
        //ContinueWith
        //Wait
        //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
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


// http://blogs.msdn.com/b/vcblog/archive/2015/03/03/introducing-the-universal-crt.aspx?PageIndex=5#comments



//VMCLRTaskRunContinueWait.exports.cpp
//:\program files (x86)\reference assemblies\microsoft\framework\.netframework\v4.0\system.core.dll : warning C4945: 'Exte
//       c:\windows\microsoft.net\framework64\v4.0.30319\mscorlib.dll : see declaration of 'System::Runtime::CompilerServi
//       first seen type is used; re-order imported assemblies to use the current type
//       This diagnostic occurred while importing type 'System.Runtime.CompilerServices.ExtensionAttribute' from assembly
//icrosoft (R) Incremental Linker Version 10.00.30319.01
//opyright (C) Microsoft Corporation.  All rights reserved.

//in\JVMCLRTaskRunContinueWait.exports.obj : warning LNK4042: object specified more than once; extras ignored
//INK : fatal error LNK1104: cannot open file 'ucrt.lib'


//andled Exception: System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.Invalid
//at jsc.meta.Commands.RewriteToHybridAssembly.RewriteToHybridAssembly.CreateExportDirectoryBridge(FileInfo ExportDirectoryBridgeOutput, Action`2 ImplementExportDirectoryBridge) in x:\jsc.internal.git\compiler\jsc.internal\jsc.internal\meta\Commands\
//at jsc.meta.Commands.RewriteToHybridAssembly.RewriteToHybridAssembly.Invoke() in x:\jsc.internal.git\compiler\jsc.internal\jsc.internal\meta\Commands\RewriteToHybridAssembly\RewriteToHybridAssembly.cs:line 225