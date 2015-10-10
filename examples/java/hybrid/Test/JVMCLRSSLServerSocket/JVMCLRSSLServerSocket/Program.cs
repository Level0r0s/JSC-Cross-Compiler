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

namespace JVMCLRSSLServerSocket
{
    static class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151010

            Console.WriteLine(typeof(object));

            // ok lets do a server.

            try
            {
                var f = javax.net.ssl.SSLSocketFactory.getDefault();

                //  f = sun.security.ssl.SSLSocketFactoryImpl@1ae717f }
                Console.WriteLine(new { f });

                //f.

            }
            catch (Exception err)
            {
                Console.WriteLine(
                    new
                    {
                        err.Message,
                        err.StackTrace
                    }
                    );

            }

            CLRProgram.CLRMain();
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
