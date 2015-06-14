using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GearVR360VideoPush
{
    class Program
    {
        static void Main(string[] args)
        {
            // because .bat files cannot do it.

            Console.WriteLine("hi");

            {
                var cmd = @"x:\util\android-sdk-windows\platform-tools\adb.exe";
                var a = "devices";

                System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo(cmd, a) { UseShellExecute = false }
                    ).WaitForExit();
            }

            var forfiles = Directory.GetFiles("x:/media", "*360*.mp4");

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150614/pvr

            foreach (var path in forfiles)
            {
                Console.WriteLine(path);

                {
                    var cmd = @"x:\util\android-sdk-windows\platform-tools\adb.exe";
                    var a = "push \"" + path + "\" /sdcard/oculus/360Videos/";

                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo(cmd, a) { UseShellExecute = false }
                        ).WaitForExit();
                }


                // "X:\util\ffmpeg-20150609-git-7c9fcdf-win64-static\ffmpeg-20150609-git-7c9fcdf-win64-static\bin\ffmpeg.exe\"
                // -ss 00:00:30 -t 1   -i x:\media\@file -vf \"scale=512:256, crop=256:256\" -f mjpeg X:\vr\@fname.thm"

                var thm = @"x:\vr\" + Path.ChangeExtension(new FileInfo(path).Name, ".thm");

                Console.WriteLine(thm);

                {
                    var cmd = @"X:\util\ffmpeg-20150609-git-7c9fcdf-win64-static\ffmpeg-20150609-git-7c9fcdf-win64-static\bin\ffmpeg.exe";
                    var a = "-ss 00:00:30 -t 1   -i \"" + path + "\" -vf \"scale=512:256, crop=256:256\" -f mjpeg \"" + thm + "\" ";

                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo(cmd, a) { UseShellExecute = false }
                        
                        ).WaitForExit();
                }


                {
                    var cmd = @"x:\util\android-sdk-windows\platform-tools\adb.exe";
                    var a = "push \"" + thm + "\" /sdcard/oculus/360Videos/";

                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo(cmd, a) { UseShellExecute = false }
                        
                        ).WaitForExit();
                }

                File.Delete(thm);


            }
        }
    }
}
