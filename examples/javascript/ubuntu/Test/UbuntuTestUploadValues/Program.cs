using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace UbuntuTestUploadValues
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //var x = Convert.ToInt32(default(string));

            //var uri = new ScriptCoreLib.Shared.BCLImplementation.System.__Uri("/upload");

            // uri = {:/upload}

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
