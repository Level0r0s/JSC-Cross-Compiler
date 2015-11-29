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

namespace TestLEST
{


    static class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            var x = 6593648.87109375;
            var y = 591168.00390625;

            Console.WriteLine(new { x, y });

            var lat = (double)LEST97.lest_function_vba.lest_geo(x, y, 0);
            var lng = (double)LEST97.lest_function_vba.lest_geo(x, y, 1);

            Console.WriteLine(new { lat, lng });

            //lat = 59.470748556421185
            //lng = 25.608322312728209

            Console.ReadLine();
            // verified.
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
