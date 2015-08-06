using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;

namespace DCIMPull
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150718/360

            // because .bat files cannot do it.

            Console.WriteLine("hi");

            var adb = @"x:\util\android-sdk-windows\platform-tools\adb.exe";

            {
                var a = "devices";

                System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo(adb, a) { UseShellExecute = false }
                    ).WaitForExit();
            }

            // X:\sdcard\DCIM>"x:\util\android-sdk-windows\platform-tools\adb.exe"  shell ls -l "/sdcard/DCIM/Camera/*.mp4"
            // -rwxrwx--- root     sdcard_r 366450839 2015-04-30 18:39 20150430_183825.mp4



            {
                var a = "shell ls -l \"/sdcard/DCIM/Camera/*.mp4\"";

                var p = System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo(adb, a)
                    {
                        UseShellExecute = false,

                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true
                    }
                );

                var output = p.StandardOutput.ReadToEnd();
                //p.WaitForExit();

                // -rwxrwx--- root     sdcard_r 366450839 2015-04-30 18:39 20150430_183825.mp4

                var files = output.Split('\n');

                foreach (var file in files)
                {
                    var filename = file.Trim().SkipUntilLastOrEmpty(" ");

                    if (string.IsNullOrEmpty(filename))
                        continue;

                    // filename = "20150430_183825.mp4"

                    Console.WriteLine(filename);

                    ;

                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo(adb, "pull -p \"/sdcard/DCIM/Camera/" + filename + "\" \"x:/sdcard/DCIM/" + filename + "\"")
                        {
                            UseShellExecute = false,
                        }
                    ).WaitForExit();

                    ;

                    System.Diagnostics.Process.Start(
                      new System.Diagnostics.ProcessStartInfo(adb, "shell rm \"/sdcard/DCIM/Camera/" + filename + "\"")
                      {
                          UseShellExecute = false,
                      }
                  ).WaitForExit();

                }
            }

            Debugger.Break();
        }
    }
}
