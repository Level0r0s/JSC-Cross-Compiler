using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mklinkReverseFramesForffmpeg
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = @"X:\vr\x360stereomidnightsun";

            var dirreverse = Directory.CreateDirectory(dir + "/reverse");

            var x = Directory.GetFiles(dir, "*.jpg");

            // x = {string[7200]}

            var reverse = x.Reverse().ToArray();

            for (int i = 0; i < x.Length; i++)
            {
                // http://forums.whirlpool.net.au/archive/1290443

                var cmd = "cmd";
                var cmdargs = "/C mklink /H " + dirreverse.FullName + @"\" + new FileInfo(reverse[i]).Name + " " + x[i];

                Process.Start(

                    new ProcessStartInfo(cmd, cmdargs)
                    {

                        UseShellExecute = false

                    }).WaitForExit();

            }

            //C:\Users\Arvo>mklink
            //Creates a symbolic link.

            //MKLINK [[/D] | [/H] | [/J]] Link Target

            //        /D      Creates a directory symbolic link.  Default is a file
            //                symbolic link.
            //        /H      Creates a hard link instead of a symbolic link.
            //        /J      Creates a Directory Junction.
            //        Link    specifies the new symbolic link name.
            //        Target  specifies the path (relative or absolute) that the new link
            //                refers to.

        }
    }
}
