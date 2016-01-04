using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GearVR360VideoPush
{
    class Program
    {
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  tcpip 5555
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell netcfg

        // should jsc remember last connected device and reconnect if disconnected?
        // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect 192.168.1.126:5555

        //W/Watchdog( 3459): *** GOODBYE!
        //I/ServiceManager( 2927): service 'lpnet' died

        static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/gearvr360videopush

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/x360videoui
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

            {
                var cmd = @"x:\util\android-sdk-windows\platform-tools\adb.exe";
                var a = "shell df";

                System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo(cmd, a) { UseShellExecute = false }
                    ).WaitForExit();
            }

            // how much memory is there?

            var forfiles = Directory.GetFiles("x:/media", "360*.mp4");

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150614/pvr
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150910/360
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151006/360

            // https://www.youtube.com/watch?v=2fhjdtQDcOo
            // not detected as 360
            // why?

            // forfiles0 = {System.IO.FileInfo[295]}
            var forfiles0 = Enumerable.ToArray(
                from ff in forfiles
                let fff = new FileInfo(ff)

                //orderby ff.Contains("Quake") descending, fff.Length descending
                //orderby ff.ToLower().Contains("volcano") descending, fff.LastWriteTime descending
                //orderby ff.ToLower().Contains("Star Wars".ToLower()) descending, fff.LastWriteTime descending
                //orderby ff.ToLower().Contains("1941".ToLower()) descending, fff.LastWriteTime descending
                //orderby ff.ToLower().Contains("Eminem".ToLower()) descending, fff.LastWriteTime descending
                //orderby ff.ToLower().Contains("cockpit".ToLower()) descending, fff.LastWriteTime descending
                //orderby ff.ToLower().Contains("mars".ToLower()) descending, fff.LastWriteTime descending
                //where ff.ToLower().Contains("mars".ToLower())
                //where ff.ToLower().Contains("mirage".ToLower())
                //where ff.ToLower().Contains("rover".ToLower())
                //where ff.ToLower().Contains("milky".ToLower())


                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151106/spherical3d
                //where ff.ToLower().Contains("_TB".ToLower())

                orderby fff.LastWriteTime descending
                orderby ff.ToLower().Contains("_TB".ToLower()) descending

                //&& ff.ToLower().Contains("mine".ToLower())

                //where ff.ToLower().Contains("night".ToLower())
                //    || ff.ToLower().Contains("lapse".ToLower()) 

                //orderby fff.LastWriteTime descending

                select fff
                );


            var totalIndex = 0;
            var totalBytes = 0L;

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150925

            Action mp4uploads = delegate { };

            //foreach (var path0 in forfiles0.Take(295))
            //foreach (var path0 in forfiles0.Take(1))
            foreach (var path0 in forfiles0)
            {
                var path = path0.FullName;

                var apkfriendlytitle = new string(
               path0.Name.Select(x => x < 127 ? x : '_').ToArray()
               );


                Console.Title = apkfriendlytitle;

                var pathSize = path0.Length;



                var totalGBytes = totalBytes / 1024 / 1024 / 1024;


                // cannot stat 'x:\media\360 Quake Gameplay in  _  E2M3 by Freeflow Plays.mp3.mp4': Bad file descriptor



                #region thm

                Directory.CreateDirectory(@"x:\vr\media\");

                // this is the full frame we can sync from android to chrome
                var jpg = @"x:\vr\media\" + Path.ChangeExtension(apkfriendlytitle, ".jpg");
                var r10 = 25 + new Random().Next(1, 10);

                var thm256x160 = @"x:\vr\media\" + Path.ChangeExtension(apkfriendlytitle, ".thm");

                // "X:\util\ffmpeg-20150609-git-7c9fcdf-win64-static\ffmpeg-20150609-git-7c9fcdf-win64-static\bin\ffmpeg.exe\"
                // -ss 00:00:30 -t 1   -i x:\media\@file -vf \"scale=512:256, crop=256:256\" -f mjpeg X:\vr\@fname.thm"


                {
                    // x:\vr\media\360 1941 Battle   _ Reenactment by World_of_Tanks.mp3.jpg: No such file or directory
                    // http://stackoverflow.com/questions/33378548/ffmpeg-crop-a-video-without-loosing-the-quality
                    // http://stackoverflow.com/questions/17621513/how-to-use-crop-filter-twice-in-ffmpeg

                    var cmd = @"X:\util\ffmpeg-20150609-git-7c9fcdf-win64-static\ffmpeg-20150609-git-7c9fcdf-win64-static\bin\ffmpeg.exe";
                    var a = "-ss 00:00:" + r10 + " -t 1   -i \"" + path + "\" -y -f mjpeg \"" + jpg + "\" ";


                    if (path0.Name.EndsWith("_TB.mp4"))
                    {
                        a = "-ss 00:00:" + r10 + " -t 1   -i \"" + path + "\" -vf \"crop=in_w:in_h/2:0:0\" -y -f mjpeg \"" + jpg + "\" ";
                    }

                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo(cmd, a) { UseShellExecute = false }

                        ).WaitForExit();

                    // 3800 x 1900  Pixels (7.22 MPixels) (2.00)
                }



                {
                    // is the thumbnail size for images the same for video?
                    var cmd = @"x:\util\android-sdk-windows\platform-tools\adb.exe";
                    var a = "push \"" + jpg + "\" /sdcard/oculus/360Photos/";

                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo(cmd, a) { UseShellExecute = false }

                        ).WaitForExit();
                }


                // 256 x 160  Pixels (1.60)
                // X:\opensource\ovr_sdk_mobile_0.6.2.0\sdcard_SDK\oculus\360Photos


                // is video360 in need for 256x256? can handle both
                //var thm = @"x:\vr\media\" + Path.ChangeExtension(apkfriendlytitle, ".thm");

                Console.WriteLine(new { thm256x160 });

                {
                    // could we use a stereo thumbnail?
                    // animated?

                    var cmd = @"X:\util\ffmpeg-20150609-git-7c9fcdf-win64-static\ffmpeg-20150609-git-7c9fcdf-win64-static\bin\ffmpeg.exe";
                    var a = "-ss 00:00:" + r10 + " -t 1   -i \"" + path + "\" -vf \"scale=512:256, crop=256:160\" -y -f mjpeg \"" + thm256x160 + "\" ";

                    if (path0.Name.EndsWith("_TB.mp4"))
                    {
                        a = "-ss 00:00:" + r10 + " -t 1   -i \"" + path + "\" -vf \"scale=640:320, crop=256:160:160:0\" -y -f mjpeg \"" + thm256x160 + "\" ";

                        //a = "-ss 00:00:" + r10 + " -t 1   -i \"" + path + "\" -vf \"crop=in_w:in_h/2:0:0\" -y -f mjpeg \"" + jpg + "\" ";
                    }

                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo(cmd, a) { UseShellExecute = false }

                        ).WaitForExit();
                }



                {
                    // is the thumbnail size for images the same for video?
                    var cmd = @"x:\util\android-sdk-windows\platform-tools\adb.exe";
                    var a = "push \"" + thm256x160 + "\" /sdcard/oculus/360Photos/";

                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo(cmd, a) { UseShellExecute = false }

                        ).WaitForExit();
                }

                {
                    var cmd = @"x:\util\android-sdk-windows\platform-tools\adb.exe";
                    var a = "push \"" + thm256x160 + "\" /sdcard/oculus/360Videos/";

                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo(cmd, a) { UseShellExecute = false }

                        ).WaitForExit();
                }


                // keep it.
                File.Delete(thm256x160);
                #endregion

                #region mp4
                // all images uploaded?
                //mp4uploads += delegate
                {
                    // are we already running?
                    // if so. can we stream upload and display?
                    // Z:\jsc.svn\examples\java\android\synergy\x360video\ApplicationActivity.startMovie.cs

                    // http://stackoverflow.com/questions/26788998/adb-push-p-bad-file-descriptor

                    var cmd = @"x:\util\android-sdk-windows\platform-tools\adb.exe";
                    var a = "push \"" + path + "\" \"/sdcard/oculus/360Videos/" + apkfriendlytitle + "\"";

                    var TitleTimer = new Thread(
                        delegate()
                        {
                            var sw = Stopwatch.StartNew();
                            while (true)
                            {
                                Thread.Sleep(1000 / 15);
                                Console.Title = new { sw.ElapsedMilliseconds, pathSize }.ToString();
                            }
                        }
                    );

                    TitleTimer.Start();

                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo(cmd, a) { UseShellExecute = false }
                        ).WaitForExit();

                    TitleTimer.Abort();

                    //325 KB/s (109215 bytes in 0.328s)
                    //538 KB/s (109215 bytes in 0.198s)
                    //428 KB/s (237778210 bytes in 541.324s)

                    totalIndex++;
                    totalBytes += pathSize;

                    Console.WriteLine(new { totalGBytes, path });

                };


                #endregion





            }

            // wait for it to upload. then check it out.
            mp4uploads();

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
