using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace WebServiceAuthorityExperiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // m0 = -80
            // m0 = -70
            //m0 = -50
            // m0 = -66
            // m0 = -14

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
