using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace DropLESTToDisplay
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //var p = DropLESTToDisplay.NamedKeyPairs.Key1PublicKey.PublicParameters;

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
