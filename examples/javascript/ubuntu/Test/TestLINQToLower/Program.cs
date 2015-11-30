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

namespace TestLINQToLower
{
    using System.Data;
    using ScriptCoreLib.Query.Experimental;


    static class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            var CountString = "";

            var count = new PerformanceResourceTimingData2ApplicationResourcePerformance().Where(x => x.path.ToLower().Contains(CountString));

            Console.WriteLine(new { count });


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
