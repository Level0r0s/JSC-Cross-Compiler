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

namespace FormsUbuntuHello
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


            System.Console.WriteLine("hello ubuntu! " + new
            {
                typeof(object).AssemblyQualifiedName
            }
            );

            Console.WriteLine("...");

            try
            {
                new Form1 { }.ShowDialog();

//{ Message = , StackTrace = java.lang.RuntimeException
//        at ScriptCoreLibJava.BCLImplementation.System.Windows.Forms.__Form.ShowDialog(__Form.java:152)
//        at FormsUbuntuHello.Program.main(Program.java:30)
// }



            }
            catch (Exception err)
            {
                Console.WriteLine(new { err.Message, err.StackTrace });
            }

            Console.ReadLine();

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

//Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task.java:311: error: cannot find symbol
//        return  __TaskExtensions.<TResult>Unwrap_06000a18(__Task.get_InternalFactory().<ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks.__Task_1<TResult>>StartNew(function)
//                                ^
//  symbol:   method <TResult>Unwrap_06000a18(__Task_1<__Task_1<TResult>>)