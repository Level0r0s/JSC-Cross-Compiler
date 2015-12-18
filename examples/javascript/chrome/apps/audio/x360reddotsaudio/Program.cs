using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Linq;

namespace x360reddotsaudio
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // items = {x360reddotsaudiolookup.item[21141]}

            //var items = x360reddotsaudiolookup.Program.Get();
            //var firstNonZero = items.FirstOrDefault(x => x.x > 0);

            ////             IL_f71a0:  ldloc      CS$0$0001
            ////  IL_f71a4:  stloc      CS$1$0000
            ////  IL_f71a8:  br.s       IL_f71aa
            ////  IL_f71aa:  ldloc      CS$1$0000
            ////  IL_f71ae:  ret
            ////} // end of method Program::Get

            //// firstNonZero { ms = 1892, x = 7 }


            //Console.WriteLine("firstNonZero " + new { firstNonZero.ms, firstNonZero.x });

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
