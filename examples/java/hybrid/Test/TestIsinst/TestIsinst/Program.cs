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

namespace TestIsinst
{


    static class Program
    {

        public static void Invoke(int e)
        {
            Console.WriteLine(
                new { e }
                );
        }

        public static void Invoke(object e)
        {
            if (e is int)
                Invoke((int)e);

        }



        [STAThread]
        public static void Main(string[] args)
        {
            Invoke((object)3);

            Console.ReadLine();
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

// need 2012!

//Y:\staging\web\java\__AnonymousTypes__UbuntuHello__i__d_jvm\__f__AnonymousType_16__19__17_0_1.java:36: error: reference to Format is
//        return __String.Format(null, "{{ AssemblyQualifiedName = {0} }}", objectArray2);
//                       ^
//Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task.java:311: error: cannot find symbol
//        return  __TaskExtensions.<TResult>Unwrap_06000a1b(__Task.get_InternalFactory().<ScriptCoreLibJava.BCLImplementation.System.T
//                                ^
//  symbol:   method<TResult> Unwrap_06000a1b(__Task_1<__Task_1<TResult>>)
//  location: class __TaskExtensions
//  where TResult is a type-variable:
//    TResult extends Object declared in method<TResult>Run_060001b5(__Func_1<__Task_1<TResult>>)



// rebuild ScriptCoreLibJava?

//- javac
//"X:\Program Files (x86)\Java\jdk1.7.0_79\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\TestIsinst\Program.java
//Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task.java:311: error: cannot find symbol
//        return  __TaskExtensions.<TResult>Unwrap_06000a1b(__Task.get_InternalFactory().<ScriptCoreLibJava.BCLImplementation.System.Thread
//                                ^
//  symbol:   method <TResult>Unwrap_06000a1b(__Task_1<__Task_1<TResult>>)
//  location: class __TaskExtensions
//  where TResult is a type-variable:
//    TResult extends Object declared in method <TResult>Run_060001b5(__Func_1<__Task_1<TResult>>)