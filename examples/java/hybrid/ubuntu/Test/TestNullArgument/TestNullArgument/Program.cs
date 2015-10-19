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

namespace TestNullArgument
{


    class Program
    {
        static void Main(Program e = null)
        {
        }

        [STAThread]
        public static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151019/ubuntuwebapplication

            // lets make it cast nulls.
            Main();

            // Z:\jsc.svn\examples\javascript\appengine\Test\TestAppEngineApplicationId\TestAppEngineApplicationId\ApplicationWebService.cs
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
