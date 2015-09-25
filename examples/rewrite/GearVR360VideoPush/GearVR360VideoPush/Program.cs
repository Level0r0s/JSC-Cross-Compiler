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
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150718/360

            // because .bat files cannot do it.

            Console.WriteLine("hi");

            {
                var cmd = @"x:\util\android-sdk-windows\platform-tools\adb.exe";
                var a = "devices";

                System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo(cmd, a) { UseShellExecute = false }
                    ).WaitForExit();
            }

            var forfiles = Directory.GetFiles("x:/media", "360*.mp4");

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150614/pvr
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150910/360


            var forfiles0 = Enumerable.ToArray(
                from ff in forfiles
                let fff = new FileInfo(ff)

                //orderby ff.Contains("Quake") descending, fff.Length descending
                //orderby ff.ToLower().Contains("volcano") descending, fff.LastWriteTime descending
                orderby ff.ToLower().Contains("Star Wars".ToLower()) descending, fff.LastWriteTime descending
                //orderby fff.LastWriteTime descending
                
                select fff
                );


            var totalIndex = 0;
            var totalBytes = 0L;

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150925

            foreach (var path0 in forfiles0.Take(1))
            {
                var path = path0.FullName;

                var apkfriendlytitle = new string(
               path0.Name.Select(x => x < 127 ? x : '_').ToArray()
               );

                var pathSize = path0.Length;

                totalIndex++;
                totalBytes += pathSize;

                var totalGBytes = totalBytes / 1024 / 1024 / 1024;

                Console.WriteLine(new { totalGBytes, path });

                // cannot stat 'x:\media\360 Quake Gameplay in  _  E2M3 by Freeflow Plays.mp3.mp4': Bad file descriptor

                {
                    // http://stackoverflow.com/questions/26788998/adb-push-p-bad-file-descriptor

                    var cmd = @"x:\util\android-sdk-windows\platform-tools\adb.exe";
                    var a = "push \"" + path + "\" \"/sdcard/oculus/360Videos/" + apkfriendlytitle + "\"";

                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo(cmd, a) { UseShellExecute = false }
                        ).WaitForExit();
                }

                #region thm
                // "X:\util\ffmpeg-20150609-git-7c9fcdf-win64-static\ffmpeg-20150609-git-7c9fcdf-win64-static\bin\ffmpeg.exe\"
                // -ss 00:00:30 -t 1   -i x:\media\@file -vf \"scale=512:256, crop=256:256\" -f mjpeg X:\vr\@fname.thm"

                var thm = @"x:\vr\" + Path.ChangeExtension(apkfriendlytitle, ".thm");

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
                #endregion


            }
        }
    }
}
