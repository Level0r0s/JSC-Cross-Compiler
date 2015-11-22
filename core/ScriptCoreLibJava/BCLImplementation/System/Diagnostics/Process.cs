using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Net.Sockets;
using java.io;
using ScriptCoreLibJava.BCLImplementation.System.Net.Sockets;
using System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.Diagnostics
{
    // http://referencesource.microsoft.com/#System/services/monitoring/system/diagnosticts/Process.cs
    // https://github.com/dotnet/corefx/blob/master/src/System.Diagnostics.Process/src/System/Diagnostics/Process.cs


    [Script(Implements = typeof(global::System.Diagnostics.Process))]
    internal class __Process : __Component
    {
        // X:\jsc.svn\examples\java\async\test\JVMCLRTCPServerAsync\JVMCLRTCPServerAsync\Program.cs

        public java.lang.Process InternalProcess;



        public StreamWriter StandardInput { get; set; }

        public static Process Start(string fileName)
        {
            Console.WriteLine(new { fileName });

            return default(Process);
        }

        public static Process Start(string fileName, string arguments)
        {
            // Z:\jsc.svn\examples\java\hybrid\ubuntu\UbuntuTCPMultiplex\Program.cs

            Console.WriteLine("enter Start " + new { fileName, arguments });
            var x = new __Process { };

            // http://stackoverflow.com/questions/19030625/redirecting-output-of-a-process-with-process-builder


            try
            {
                var aa = new List<string> { fileName };

                aa.AddRange(
                    // jvm wants em split
                    arguments.Split(' ')
                );


                //var a = new[] { fileName, arguments };


                //var o = java.lang.Runtime.getRuntime().exec(aa.ToArray());
                //x.InternalProcess = o;

                var p = new java.lang.ProcessBuilder(aa.ToArray());
                var pp = p.redirectErrorStream(true);


                x.InternalProcess = pp.start();

                // http://stackoverflow.com/questions/26174975/fire-a-cmd-exe-command-through-processbuilder-with-visual-window


                x.StandardInput = new StreamWriter(
                    new __NetworkStream { InternalOutputStream = x.InternalProcess.getOutputStream() }
                );

                //InputStreamExtensions.ToNetworkStream(
                //var sout = x.InternalProcess.getOutputStream();
                var sout = new StreamReader(
                    x.InternalProcess.getInputStream().ToNetworkStream()
                );

                //var n = new __NetworkStream { InternalOutputStream  };
                //InputStreamExtensions

                new Thread(
                    delegate()
                    {
                        var xx = sout.ReadLine();
                        while (xx != null)
                        {
                            Console.WriteLine(xx);

                            xx = sout.ReadLine();
                        }
                    }
                ).Start();

            }
            catch (Exception err)
            {
                Console.WriteLine(new { err.Message, err.StackTrace });

                //throw err;
                //throw;

                throw new InvalidOperationException { };
            }



            //Console.WriteLine("exit Start ");
            return x;
        }



        public int ExitCode { get; set; }

        public void WaitForExit()
        {
            //Console.WriteLine("enter WaitForExit ");


            try
            {
                ExitCode = this.InternalProcess.waitFor();
            }
            catch (Exception err)
            {
                Console.WriteLine(new { err.Message, err.StackTrace });

                //throw err;
                //throw;

                throw new InvalidOperationException { };
            }
            //Console.WriteLine("exit WaitForExit " + new { x });
        }

        public static implicit operator Process(__Process e)
        {
            // jsc should catch this?
            //return (__Process)(object)e;
            return (Process)(object)e;
        }
    }
}
