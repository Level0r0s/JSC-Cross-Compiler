using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace UbuntuInsecureOrigins
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
#else

            var p = System.Diagnostics.Process.Start(
                //cmd.FullName,
                 "cmd.exe",
                 @"/C call X:\jsc.internal.git\keystore\red\plink.xmikro.bat  java -jar /home/xmikro/Desktop/staging/UbuntuInsecureOrigins.ApplicationWebService.exe "
             );

            //Thread.Sleep(500);

            ////p.StandardInput.Write('y');

            //Console.WriteLine("StandardInput.Write");
            //p.StandardInput.Write("y\n");

            p.WaitForExit();


#endif
        }

    }
}
