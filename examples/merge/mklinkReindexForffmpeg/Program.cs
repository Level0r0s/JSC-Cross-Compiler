using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mklinkReindexForffmpeg
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = new[] {
                @"X:\p900\20151210\DCIM\101INTVL",
                @"X:\p900\20151210\DCIM\103INTVL",
                @"X:\p900\20151210\DCIM\105INTVL"
            };

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151210

            var outputs = @"X:\p900\20151210\DCIM\stars";

            new DirectoryInfo(outputs).Create();


            var all = (
                from p in inputs
                from f in Directory.GetFiles(p)
                select f
            ).ToArray();

            for (int i = 0; i < all.Length; i++)
            {
                // http://forums.whirlpool.net.au/archive/1290443
                Console.Title = new { i, all.Length }.ToString();

                var cmd = "cmd";
                var cmdargs = "/C mklink /H " + outputs + @"\" + new FileInfo(i.ToString().PadLeft(5, '0')).Name + ".jpg " + all[i];

                Process.Start(

                    new ProcessStartInfo(cmd, cmdargs)
                    {

                        UseShellExecute = false

                    }).WaitForExit();


            }
        }
    }
}
