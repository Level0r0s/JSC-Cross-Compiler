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

namespace TestHttpApplication
{


    static class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            // http://stackoverflow.com/questions/11203483/run-a-java-application-as-a-service-on-linux

            // http://askubuntu.com/questions/99232/how-to-make-a-jar-file-run-on-startup-and-when-you-log-out

            // "X:\torrent\ubuntu-14.04.3-server-amd64.iso"
            // http://standards.freedesktop.org/desktop-entry-spec/desktop-entry-spec-latest.html


            System.Console.WriteLine("hello ubuntu!! " + new
            {
                // can jsc autodetect this?
                typeof(System.Web.HttpApplication).AssemblyQualifiedName
            }
            );

            //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Web\__HttpRequest.java:5: error: package javax.servlet.http does not exist
            //import javax.servlet.http.Cookie;

            Console.WriteLine("...");

            // are we running in GUI or TTY?
            // can we enumerate keystores?

            //Thread.Sleep(10000);


            // CLR not available? unless there was mono?
            //CLRProgram.CLRMain();
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
