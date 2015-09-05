using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace HybridHopToUDPChromeApp
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // chrome app is build via post build event

            UDPServer.Invoke().Wait();

            // RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
