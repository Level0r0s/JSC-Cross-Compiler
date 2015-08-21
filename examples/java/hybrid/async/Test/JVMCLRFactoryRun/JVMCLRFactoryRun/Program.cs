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

namespace JVMCLRFactoryRun
{



    static class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            // ut = Id = 1, Status = Created, Method = "System.String <Main>b__0()", Result = "{Not yet computed}"
            var ut = new Task<string>(
                delegate
                {
                    return "sync value " + new { Thread.CurrentThread.ManagedThreadId };
                }
            );

            Console.WriteLine(new { ut.Status, ut.IsCompleted });

            ut.Start();
            ut.Wait();
            var uut = ut.Result;
            // { Status = RanToCompletion, IsCompleted = True, uut = sync value { ManagedThreadId = 3 } }
            Console.WriteLine(new { ut.Status, ut.IsCompleted, uut });

            //java.lang.Object, rt
            //{ Status = 0, IsCompleted = false }
            //{ Status = 5, IsCompleted = true, uut = sync value { ManagedThreadId = 8 } }

            var u = Task.Factory.StartNew(
                function: delegate
                {
                    return "sync value " + new { Thread.CurrentThread.ManagedThreadId };
                }
            );


            u.Wait();
            var uu = u.Result;
            Console.WriteLine(new { uu } + "  " + new { Thread.CurrentThread.ManagedThreadId });

            // { uu = sync value { ManagedThreadId = 9 } }  { ManagedThreadId = 1 }

            //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
            //{ uu = sync value { ManagedThreadId = 3 } }  { ManagedThreadId = 1 }
            //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089

  

            CLRProgram.CLRMain();
        }


    }


    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
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
