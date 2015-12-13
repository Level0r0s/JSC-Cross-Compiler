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

namespace TestPSquarePercentile
{


    static class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            // "Z:\jsc.svn\examples\java\hybrid\TestPSquarePercentile\TestPSquarePercentile\bin\Release\TestPSquarePercentile.exports"

            //math3.PSquarePercentile
        

            System.Console.WriteLine("hello !! ubuntu!! " + new
            {
                typeof(object).AssemblyQualifiedName
            }
            );

            Console.WriteLine("...");

            // are we running in GUI or TTY?
            // can we enumerate keystores?

            Thread.Sleep(10000);


            // CLR not available? unless there was mono?
            //CLRProgram.CLRMain();

        }


    }



    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain(string[] args)
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            MessageBox.Show("click to close");
        }
    }


}
 