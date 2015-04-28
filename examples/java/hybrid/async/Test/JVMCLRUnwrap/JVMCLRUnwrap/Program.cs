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

namespace JVMCLRUnwrap
{


    static class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {


            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            var ut = new Task<Task<string>>(
                delegate
                {
                    return Task.FromResult(
                        "async value " + new { Thread.CurrentThread.ManagedThreadId }
                    );
                }
            );

            ut.Start();
            ut.Wait();

            var uu = TaskExtensions.Unwrap(ut);
            Console.WriteLine(new { uu.Result, Thread.CurrentThread.ManagedThreadId });

            // java.lang.Object, rt
            //{ Result = async value { ManagedThreadId = 8 }, ManagedThreadId = 1 }

            // { Result = async value { ManagedThreadId = 11 }, ManagedThreadId = 1 }

            // X:\jsc.svn\examples\javascript\async\test\TestUnwrap\TestUnwrap\Application.cs
            var yt = Task.Run(
                  delegate
                  {
                      return Task.FromResult(
                          "async value " + new { Thread.CurrentThread.ManagedThreadId }
                      );
                  }
            );

            //yt.Start();
            yt.Wait();
            // { Result = async value { ManagedThreadId = 3 }, ManagedThreadId = 1 }
            Console.WriteLine(new { yt.Result, Thread.CurrentThread.ManagedThreadId });

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
