// defined by?
//    <ProjectReference Include="..\..\..\..\..\..\..\opensource\github\Newtonsoft.Json\Src\Newtonsoft.Json\Newtonsoft.Json.csproj">
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
// defined by?
// "X:\jsc.svn\market\synergy\github\YoutubeExtractor\external\YoutubeExtractor.sln"
using YoutubeExtractor;
using ScriptCoreLib.Extensions;
using System.Threading;
using System.Xml.Linq;
using System.Runtime.InteropServices;

namespace TestYouTubeExtractor
{
    // Z:\jsc.svn\examples\merge\Test\TestYouTubeExtractor\TestYouTubeExtractor\Program.cs

    static class Extensions
    {
        //var page0 = new WebClient().DownloadString(src);

        public static string DownloadStringOrRetry(this WebClient c, string u)
        {
            while (true)
                try { return c.DownloadString(u); }
                catch (Exception err) { Console.WriteLine(new { err.Message }); Thread.Sleep(10000); }


        }

    }

    public class Program
    {
        static int countunavailable;

        // Show Details	Severity	Code	Description	Project	File	Line
        //Error NuGet Package restore failed for project TestYouTubeExtractor: Unable to find version '1.0.0.0' of package 'YoutubeExtractor'..			0

        // https://github.com/mono/taglib-sharp/


        [DllImport("mpr.dll", SetLastError = true, EntryPoint = "WNetRestoreSingleConnectionW", CharSet = CharSet.Unicode)]
        internal static extern int WNetRestoreSingleConnection(IntPtr windowHandle,
                                                     [MarshalAs(UnmanagedType.LPWStr)] string localDrive,
                                                     [MarshalAs(UnmanagedType.Bool)] bool useUI);


        //Error CS0246  The type or namespace name 'VideoInfo' could not be found(are you missing a using directive or an assembly reference?)	TestYouTubeExtractor X:\jsc.svn\examples\merge\Test\TestYouTubeExtractor\TestYouTubeExtractor\Program.cs	47

        //            ---------------------------
        //Microsoft Visual Studio
        //---------------------------
        //The project file 'x:\opensource\github\taglib-sharp\src\taglib-sharp.csproj' has been moved, renamed or is not on your computer.
        //---------------------------
        //OK
        //---------------------------


        public enum projection
        {
            x2D,
            x360,
            x360TB
        }

        // { err = System.IO.FileNotFoundException: Could not load file or assembly 'taglib-sharp,
        private static void DownloadVideo(
            string chname,


            // not detected via metadata?
            projection Spherical,
            string link,
            IEnumerable<VideoInfo> videoInfos)
        {
            //Error	3	The type 'System.Windows.UIElement' is defined in an assembly that is not referenced. You must add a reference to assembly 'PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'.	Z:\jsc.svn\examples\merge\Test\TestYouTubeExtractor\TestYouTubeExtractor\Program.cs	75	13	TestYouTubeExtractor
            //Error	4	The type 'System.Windows.Forms.Control' is defined in an assembly that is not referenced. You must add a reference to assembly 'System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'.	Z:\jsc.svn\examples\merge\Test\TestYouTubeExtractor\TestYouTubeExtractor\Program.cs	75	13	TestYouTubeExtractor

            var mp4 = videoInfos.Where(x => x.VideoType == VideoType.Mp4);
            var mp4video = mp4.Where(x => x.Resolution > 0).OrderBy(x => x.Resolution).ToArray();
            var mp4audio = mp4video.Where(x => x.AudioBitrate > 0).OrderBy(x => x.Resolution).ToArray();


            VideoInfo video = //videoInfos.FirstOrDefault(k => k.FormatCode == 299)
             mp4audio.OrderBy(info => info.Resolution).Last();


            //RequiresDecryption = false

            
            Console.WriteLine(video.Title);

            if (video.RequiresDecryption)
            {
                Console.WriteLine("cant");
                return;
                video.DecryptDownloadUrl();
            }

            // old name
            var Title =
                  video.Title
                //.Replace("/", " ")
                //.Replace("\\", " ")
                .Replace("\"", "'")
                //.Replace(":", " ")
                .Replace("&", " and ")
                //.Replace("*", " ")
                ;

            // http://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars(v=vs.110).aspx

            foreach (var item in
                Path.GetInvalidFileNameChars())
            {
                Title = Title.Replace(item, ' ');

            }

            // https://code.google.com/p/android/issues/detail?id=8185
            // http://stackoverflow.com/questions/18596245/in-c-how-can-i-detect-if-a-character-is-a-non-ascii-character


            var apkfriendlytitle_old3 = default(string);

            var apkfriendlytitle = new string(
                Title.Select(x => x < 127 ? x : '_').ToArray()
                );

            // apkfriendlytitle = "___360_ - __________find the truth 360 degree trick movie ."

            // prefix it, and loose the keyword later to be less redundant...
            if (Spherical == projection.x360)
                apkfriendlytitle = "360 " + (apkfriendlytitle.Replace("360", " ")).Trim();
            else if (Spherical == projection.x360TB)
            {
                apkfriendlytitle_old3 = "360 " + (apkfriendlytitle.Replace("360", " ")).Trim();
                apkfriendlytitle = "360 3D " + (apkfriendlytitle.Replace("360", " ")).Trim();
            }

            //apkfriendlytitle = "360 " + (apkfriendlytitle.Replace("360", " ")).Trim() + "_TB";
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151106


            if (!string.IsNullOrEmpty(chname))
                apkfriendlytitle_old3 += " by " + new string(chname.Select(x => x < 127 && char.IsLetterOrDigit(x) ? x : '_').ToArray());

            // remedy for bug run
            var apkfriendlytitle_old2 = apkfriendlytitle;
            if (!string.IsNullOrEmpty(chname))
                apkfriendlytitle_old2 += " by " + (chname.Select(x => x < 127 && char.IsLetterOrDigit(x) ? x : '_').ToArray());

            var apkfriendlytitle_old = apkfriendlytitle;
            if (!string.IsNullOrEmpty(chname))
                apkfriendlytitle_old += " by " + chname;




            // pxa_mp4 = "x:/media\\HD Video 1080p - Time Lapse with Sunsets, Clouds, Stars by LoungeV studio : Relaxing Nature Videos.mp4"
            if (!string.IsNullOrEmpty(chname))
                apkfriendlytitle += " by " + new string(chname.Select(x => x < 127 && char.IsLetterOrDigit(x) ? x : '_').ToArray());
            //if (Spherical == projection.x360TB)
            //    apkfriendlytitle += "_TB";

            // 
            //apkfriendlytitle = apkfriendlytitle.Replace("360", "_");



            //  System.IO.DirectoryNotFoundException: Could not find a part of the path 'x:\media\360 tape columns 4096x3840x2160p60 py by zproxy.mp4'.

            // clean slate?
            new DirectoryInfo("x:/media").Create();



            // px = "x:/media\\OVRMyCubeWorldNDK WASDC PointerLock.mp4"
            var px_old = Path.Combine("x:/media", apkfriendlytitle_old + video.VideoExtension);
            var px_old2 = Path.Combine("x:/media", apkfriendlytitle_old2 + video.VideoExtension);

            // "X:\media\360 What is VR Video by Jessica_Brillhart.mp3.mp4"
            var px_old3 = Path.ChangeExtension(Path.Combine("x:/media", apkfriendlytitle_old3 + video.VideoExtension), ".mp3.mp4");


            // pxa_mp4 = "x:/media\\OVRMyCubeWorldNDK WASDC PointerLock.mp4"
            var pxa_mp4 = Path.Combine("x:/media", apkfriendlytitle + video.VideoExtension);



            // pxa_mp3 = "x:/media\\OVRMyCubeWorldNDK WASDC PointerLock.mp3"
            var pxa_mp3 = Path.ChangeExtension(pxa_mp4, ".mp3");
            // pxa_mp4_mp4 = "x:/media\\OVRMyCubeWorldNDK WASDC PointerLock.mp4.mp4"
            var pxa_mp4_mp4 = Path.ChangeExtension(pxa_mp4, ".mp4.mp4");

            // pxa_mp3_mp4 = "x:/media\\OVRMyCubeWorldNDK WASDC PointerLock.mp3.mp4"
            var pxa_mp3_mp4 = Path.ChangeExtension(pxa_mp4,

                (Spherical == projection.x360TB) ?

                ".mp3._TB.mp4" :

                ".mp3.mp4"
            );

            // "X:\media\360 3D What is VR Video by Jessica_Brillhart_TB.mp4"
            var pxa_old_mp3_mp4 = Path.ChangeExtension(px_old, ".mp3.mp4");



            if (!File.Exists(pxa_mp4))
                if (File.Exists(px_old3))
                {
                    Console.WriteLine("renamed " + new { pxa_mp3_mp4 });
                    // upgrade old naming to apk friendly.
                    File.Move(px_old3, pxa_mp3_mp4);
                }



            if (!File.Exists(pxa_mp4))
                if (File.Exists(px_old2))
                {
                    Console.WriteLine("renamed " + new { pxa_mp4 });
                    // upgrade old naming to apk friendly.
                    File.Move(px_old2, pxa_mp4);
                }




            if (!File.Exists(pxa_mp4))
                if (File.Exists(px_old))
                {
                    Console.WriteLine("renamed " + new { pxa_mp4 });
                    // upgrade old naming to apk friendly.
                    File.Move(px_old, pxa_mp4);
                }


            if (!File.Exists(pxa_mp3_mp4))
                if (File.Exists(pxa_old_mp3_mp4))
                {
                    Console.WriteLine("renamed " + new { pxa_mp3_mp4 });
                    // upgrade old naming to apk friendly.
                    File.Move(pxa_old_mp3_mp4, pxa_mp3_mp4);
                }


            // do we have the end result?

            if (File.Exists(pxa_mp3_mp4))
            {
                Console.WriteLine("all done: " + pxa_mp3_mp4);

                // all done
                //Debugger.Break();
                return;
            }

            var awaitingToBeTagged = false;

            if (!File.Exists(pxa_mp4))
            {
            retry: ;

                try
                {
                    // ?????369:Timelapse of Sahara @Morocco

                    // pxa_mp4 = "x:/media\\HD Video 1080p - Time Lapse with Sunsets, Clouds, Stars by LoungeV studio : Relaxing Nature Videos.mp4"

                    var videoDownloader = new VideoDownloader(video, pxa_mp4);
                    videoDownloader.DownloadProgressChanged += (sender, args) =>
                    {
                        //ScriptCoreLib.Desktop.TaskbarProgress.SetMainWindowProgress(0.01 * args.ProgressPercentage);



                        Console.Title = "%" + args.ProgressPercentage.ToString("0.0");
                    }
                        ;
                    videoDownloader.Execute();
                    awaitingToBeTagged = true;
                }
                catch (Exception err)
                {
                    Console.WriteLine(new { video, err });

                    //                        url = http://youtube.com/watch?v=V0MWPJqVoUc }
                    //{
                    //                            url = http://s.ytimg.com/yts/jsbin/html5player-new-et_EE-vfl2stSNK/html5player-new.js }
                    //25 Flat Geo Earth claims that 'Rocked' my world view 2 / 5
                    //{
                    //                                video = Full Title: 25 Flat Geo Earth claims that 'Rocked' my world view 2 / 5.mp4, Type: Mp4, Resolution: 720p, err = System.Net.WebException: The remote server returned an error: (403) Forbidden.

                    return;

                    Debugger.Break();

                    // retry?
                    video = //videoInfos.FirstOrDefault(k => k.FormatCode == 299)
                        mp4audio.OrderBy(info => info.Resolution).Take(mp4audio.Count() - 1).LastOrDefault();

                    if (video == null)
                        return;

                    goto retry;
                }
            }

            ;

            #region any reason to attempt upgrade?

            // upgrade { link = //www.youtube.com/embed/E-SDNGMYB80 } from 360 to 480


            if (mp4video.Last().Resolution > mp4audio.Last().Resolution)

                // dont care about non HD
                if (mp4video.Last().Resolution >= 1080)
                {
                    // fk u visual studio, for closing IDE for stale license.

                    Console.WriteLine("upgrade " + new { link } + " from " + mp4audio.Last().Resolution + " to " + mp4video.Last().Resolution);


                    var ffmpeg = @"X:\util\ffmpeg-20150609-git-7c9fcdf-win64-static\ffmpeg-20150609-git-7c9fcdf-win64-static\bin\ffmpeg.exe";

                    if (File.Exists(pxa_mp4_mp4))
                        if (

                            // 15m is the timeout to not conitnue
                            (DateTime.UtcNow - File.GetLastWriteTimeUtc(pxa_mp4_mp4)).TotalMinutes > 15
                            )
                        {
                            File.Delete(pxa_mp4_mp4);

                        }

                    if (File.Exists(pxa_mp3))
                        if (

                            // 15m is the timeout to not conitnue
                            (DateTime.UtcNow - File.GetLastWriteTimeUtc(pxa_mp3)).TotalMinutes > 15
                            )
                        {
                            File.Delete(pxa_mp3);

                        }


                    if (!File.Exists(pxa_mp4_mp4))
                    {
                        var upgradeTargets = new Stack<VideoInfo>(mp4video.ToList());
                    // +		upgradeTargets.Peek()	{Full Title: Noa Neal ‘Graffiti’ 4K 360° Music Video Clip.mp4, Type: Mp4, Resolution: 2160p}	YoutubeExtractor.VideoInfo
                    // +		upgradeTargets.Peek()	{Full Title: Noa Neal ‘Graffiti’ 4K 360° Music Video Clip.mp4, Type: Mp4, Resolution: 1440p}	YoutubeExtractor.VideoInfo
                    // +		upgradeTargets.Peek()	{Full Title: Noa Neal ‘Graffiti’ 4K 360° Music Video Clip.mp4, Type: Mp4, Resolution: 1080p}	YoutubeExtractor.VideoInfo
                    // +		upgradeTargets.Peek()	{Full Title: Noa Neal ‘Graffiti’ 4K 360° Music Video Clip.mp4, Type: Mp4, Resolution: 720p}	YoutubeExtractor.VideoInfo

                        // video = {Full Title: Noa Neal ‘Graffiti’ 4K 360° Music Video Clip.mp4, Type: Mp4, Resolution: 720p}

                        retry_lesser:

                        var videoDownloader = new VideoDownloader(upgradeTargets.Peek(), pxa_mp4_mp4);
                        videoDownloader.DownloadProgressChanged += (sender, args) =>
                        {
                            //ScriptCoreLib.Desktop.TaskbarProgress.SetMainWindowProgress(0.01 * args.ProgressPercentage);



                            Console.Title = "%" + args.ProgressPercentage.ToString("0.0");
                        }
                       ;

                        // some videos may not allow that?
                        // err = {"The remote server returned an error: (403) Forbidden."}
                        try
                        {
                            videoDownloader.Execute();
                        }
                        catch (Exception err)
                        {
                            upgradeTargets.Pop();
                            // the others are also blocked?

                            //goto retry_lesser;
                            throw;
                        }
                    }

                    // extract mp3

                    // "X:\util\ffmpeg-20150609-git-7c9fcdf-win64-static\ffmpeg-20150609-git-7c9fcdf-win64-static\bin\ffmpeg.exe" -i "X:\media\Dolphins 360° 4K.mp4" "C:\Users\Administrator\Documents\Dolphins 360_ 4K.mp3"


                    // "X:\util\ffmpeg-20150609-git-7c9fcdf-win64-static\ffmpeg-20150609-git-7c9fcdf-win64-static\bin\ffmpeg.exe" -i "C:\Users\Administrator\Documents\Dolphins 360_ 4K.mp4" -i "C:\Users\Administrator\Documents\Dolphins 360_ 4K.mp3" -shortest "C:\Users\Administrator\Documents\Dolphins 360_ 4K.mp3.mp4"

                    if (!File.Exists(pxa_mp3))
                        Process.Start(ffmpeg,
                            "-i \"" + pxa_mp4 + "\""
                            + " \"" + pxa_mp3 + "\"").WaitForExit();

                    // merge and delete

                    // Additional information: The operation was canceled by the user

                    // http://superuser.com/questions/227433/whats-the-difference-between-ffmpegs-vcodec-copy-and-sameq

                    Process.Start(ffmpeg,
                       " -i \"" + pxa_mp3 + "\""
                       + " -i \"" + pxa_mp4_mp4 + "\""
                       + " -c:v copy -shortest \"" + pxa_mp3_mp4 + "\"").WaitForExit();

                    awaitingToBeTagged = true;



                    File.Delete(pxa_mp3);
                    File.Delete(pxa_mp4_mp4);
                    File.Delete(pxa_mp4);
                }
            #endregion

            // we did not upgrade.. assume it was already tagged...
            if (!awaitingToBeTagged)
                return;

            // X:\jsc.svn\examples\merge\Test\TestYouTubeExtractor\xffmpeg\Program.cs

            var pxatagged_mp4 = File.Exists(pxa_mp3_mp4) ? pxa_mp3_mp4 : pxa_mp4;

            Console.WriteLine("TagLib... " + new { new FileInfo(pxatagged_mp4).Length });

            // what about webm?
            TagLib.File videoFile = TagLib.File.Create(pxatagged_mp4);
            //TagLib.Mpeg4.AppleTag customTag = (TagLib.Mpeg4.Comm)videoFile.GetTag(TagLib.TagTypes.Apple);
            TagLib.Mpeg4.AppleTag customTag = (TagLib.Mpeg4.AppleTag)videoFile.GetTag(TagLib.TagTypes.Apple);


            //customTag.SetDashBox("Producer", "Producer1",link);
            //customTag.Comment = link;
            customTag.Album = link;
            videoFile.Save();
            videoFile.Dispose();

            // all done now..

        }

        private static void DownloadVideo1(string link, IEnumerable<VideoInfo> videoInfos)
        {
            // Show Details	Severity	Code	Description	Project	File	Line
            //Error CS0246  The type or namespace name 'ICSharpCode' could not be found(are you missing a using directive or an assembly reference?)	taglib-sharp File.cs 878


            // Show Details	Severity	Code	Description	Project	File	Line
            //Error Error opening icon file X:\opensource\github\taglib - sharp\src-- Access to the path 'X:\opensource\github\taglib-sharp\src' is denied.taglib - sharp    CSC

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150609/360
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150608
            var mp4 = videoInfos.Where(x => x.VideoType == VideoType.Mp4);
            var mp4video = mp4.Where(x => x.Resolution > 0).OrderBy(x => x.Resolution).ToArray();
            var mp4audio = mp4video.Where(x => x.AudioBitrate > 0).OrderBy(x => x.Resolution).ToArray();

            /*
             * Select the first .mp4 video with 360p resolution
             */
            //VideoInfo video = mp4audio.OrderByDescending(info => info.Resolution).First();
            // 300MB?
            //VideoInfo video = mp4audio.OrderBy(info => info.Resolution).First();
            //VideoInfo video = mp4video.OrderBy(info => info.Resolution).Last();
            //VideoInfo video = videoInfos.FirstOrDefault(k => k.FormatCode == 315)
            VideoInfo video = //videoInfos.FirstOrDefault(k => k.FormatCode == 299)
                mp4audio.OrderBy(info => info.Resolution).Last();


            video.DecryptDownloadUrl();

            var Title =
                  video.Title
                //.Replace("/", " ")
                //.Replace("\\", " ")
                .Replace("\"", "'")
                //.Replace(":", " ")
                .Replace("&", " and ")
                //.Replace("*", " ")
                ;

            // http://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars(v=vs.110).aspx

            foreach (var item in
                Path.GetInvalidFileNameChars())
            {
                Title = Title.Replace(item, ' ');

            }

            var px = Path.Combine(
                //Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
               "x:/media",

             Title + video.VideoExtension);

#if REMOTE
            var p = Path.Combine(
                //Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "r:/media",

               Title + video.VideoExtension);

            Console.WriteLine(px);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa385480(v=vs.85).aspx
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa385485(v=vs.85).aspx
            // http://stackoverflow.com/questions/8629760/how-to-force-windows-to-reconnect-to-network-drive
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa385453(v=vs.85).aspx

            try
            {
                var ee = Directory.GetFileSystemEntries("r:\\");
            }
            catch
            {
                // \\192.168.43.12\x$

                //                [Window Title]
                //        Location is not available

                //        [Content]
                //R:\ is unavailable.If the location is on this PC, make sure the device or drive is connected or the disc is inserted, and then try again.If the location is on a network, make sure you’re connected to the network or Internet, and then try again.If the location still can’t be found, it might have been moved or deleted.

                //[OK]

                // ---------------------------
                //Error
                //-------------------------- -
                //This network connection does not exist.


                //-------------------------- -
                //OK
                //-------------------------- -

                IntPtr hWnd = new IntPtr(0);
                int res = WNetRestoreSingleConnection(hWnd, "r:", true);
            }
#endif

            // res = 86
            // res = 0

            //            ---------------------------
            //            Restoring Network Connections
            //---------------------------
            //An error occurred while reconnecting r:
            //                to
            //\\RED\x$
            //Microsoft Windows Network: The local device name is already in use.


            //This connection has not been restored.
            //---------------------------
            //OK
            //-------------------------- -

#if REMOTE
            if (!File.Exists(p))
#endif

            {
                if (!File.Exists(px))
                {
                    /*
                     * Create the video downloader.
                     * The first argument is the video to download.
                     * The second argument is the path to save the video file.
                     */
                    var videoDownloader = new VideoDownloader(video, px);

                    // Register the ProgressChanged event and print the current progress
                    videoDownloader.DownloadProgressChanged += (sender, args) =>
                    {
                        //ScriptCoreLib.Desktop.TaskbarProgress.SetMainWindowProgress(0.01 * args.ProgressPercentage);



                        Console.Title = "%" + args.ProgressPercentage.ToString("0.0");
                    }
                    ;


                    /*
                     * Execute the video downloader.
                     * For GUI applications note, that this method runs synchronously.
                     */
                    videoDownloader.Execute();

                    // modify the mp4 tag only if we just fetched it...

                    // Additional information: The process cannot access the file 'C:\Users\Arvo\Documents\Dido - Don't Believe In Love.mp4' because it is being used by another process.

                    // http://stackoverflow.com/questions/18250281/reading-writing-metadata-of-audio-video-files

                    Console.WriteLine("TagLib... " + new { new FileInfo(px).Length });

                    // what about webm?
                    TagLib.File videoFile = TagLib.File.Create(px);
                    //TagLib.Mpeg4.AppleTag customTag = (TagLib.Mpeg4.Comm)videoFile.GetTag(TagLib.TagTypes.Apple);
                    TagLib.Mpeg4.AppleTag customTag = (TagLib.Mpeg4.AppleTag)videoFile.GetTag(TagLib.TagTypes.Apple);


                    //customTag.SetDashBox("Producer", "Producer1",link);
                    //customTag.Comment = link;
                    customTag.Album = link;
                    videoFile.Save();
                    videoFile.Dispose();
                }

                // http://stackoverflow.com/questions/13847669/file-move-progress-bar

#if REMOTE
                Console.WriteLine("Move... " + new { p });

                //File.Move(px, p);

                //err = System.IO.IOException: Cannot create a file when that file already exists

                // map network drive via ip. as the aias can be forgotten by the network

                // http://www.peerwisdom.org/2013/04/03/large-send-offload-and-network-performance/
                // 350 KBps???
                // http://blogs.msdn.com/b/heaths/archive/2006/04/07/571138.aspx
                // https://www.microsoft.com/en-us/download/details.aspx?id=11876
                // http://serverfault.com/questions/248728/eseutil-exe-version-8-3-106-1
                // http://blogs.technet.com/b/askperf/archive/2007/05/08/slow-large-file-copy-issues.aspx#pi169128=4
                // http://mygrassvalleyportal.force.com/gvknowledge/articles/KB_Article/Disabling-Large-Send-Offload-Enhance-Network-Bandwidth-on-Summit-9-x
                //  disable File and Printer sharing for the IPv6 interface. (press alt in ncpa.cpl, then advanced\advanced settings\Ethernet\Bindings for Ethernet - disable IPv6 File & Print sharing)

                // http://blogs.msdn.com/b/granth/archive/2010/05/10/how-to-copy-very-large-files-across-a-slow-or-unreliable-network.aspx

                //  /J           Copies using unbuffered I/O. Recommended for very large files.
                // http://www.xxcopy.com/xxcopy30.htm
                // http://stackoverflow.com/questions/48679/copy-a-file-without-using-the-windows-file-cache

                // https://github.com/SQLServerIO/UBCopy/blob/master/UBCopy/AsyncUnbuffCopyStatic.cs

                // takes forever?
                ////Process.Start(
                ////    "cmd",

                ////    "  /K xcopy \"" + px + "\" \"r:\\media\\\" /J"
                ////);


                // slow but atleast we have a progressbar?
                //new Thread(

                //    delegate ()
                //    {
                Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(px, p, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs);
#endif

                //    }
                //)
                //{ ApartmentState = ApartmentState.STA }.Start();

                //Thread.Yield();



                //                Does R:/ media\The Illusion Of Time -Documentary.mp4 specify a file name
                //or directory name on the target
                //(F = file, D = directory)?

                //err = { "Could not find a part of the path 'r:\\media'."}
                // System.IO.DirectoryNotFoundException: Could not find a part of the path 'r:\media'.
                // System.IO.IOException: The specified network name is no longer available.
                // Systxem.IO.IOException: The system cannot move the file to a different disk drive

                //Move... { p = r:/ media\KRYON 'Evolution Revealed' - Lee Carroll.mp4 }
                //{
                //    err = System.IO.DirectoryNotFoundException: Could not find a part of the path
                //'r:\media'.
                //   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
                //   at System.IO.Directory.InternalCreateDirectory(String fullPath, String path,
                //Object dirSecurityObj, Boolean checkHost)
                //   at System.IO.Directory.InternalCreateDirectoryHelper(String path, Boolean che
                //ckHost)
                //   at System.IO.Directory.CreateDirectory(String path)
                //   at Microsoft.VisualBasic.FileIO.FileSystem.CopyOrMoveFile(CopyOrMove operatio
                //n, String sourceFileName, String destinationFileName, Boolean overwrite, UIOptio
                //nInternal showUI, UICancelOption onUserCancel)
                //   at Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(String sourceFileName, St
                //ring destinationFileName, UIOption showUI)
            }
        }


        static void Main(string[] args)
        {
            //DoVideo(

            //    "https://www.youtube.com/watch?v=SgQwBSHJNp0"
            //    //"https://www.youtube.com/watch?v=h-8UCEigYTI"
            //    );

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150609/360

            //DoVideo(
            ////    //"https://www.youtube.com/watch?v=ZABusb0bsnw"
            ////    "https://www.youtube.com/watch?v=gRUk3po8TcA"
            //"https://www.youtube.com/watch?v=LgQIqxyjwYA"
            //);

#if REMOTE
            #region WNetRestoreSingleConnection
            try
            {
                var ee = Directory.GetFileSystemEntries("r:\\");
            }
            catch
            {
                // \\192.168.43.12\x$

                //                [Window Title]
                //        Location is not available

                //        [Content]
                //R:\ is unavailable.If the location is on this PC, make sure the device or drive is connected or the disc is inserted, and then try again.If the location is on a network, make sure you’re connected to the network or Internet, and then try again.If the location still can’t be found, it might have been moved or deleted.

                //[OK]

                // ---------------------------
                //Error
                //-------------------------- -
                //This network connection does not exist.


                //-------------------------- -
                //OK
                //-------------------------- -

                IntPtr hWnd = new IntPtr(0);
                int res = WNetRestoreSingleConnection(hWnd, "r:", true);
            }
            #endregion
#endif


            // or what if debugger starts asking for developer license and clicking ok kills to downloads in progress?
            // what if device looses power.
            // how are we to know or resume?


            // X:\jsc.svn\examples\merge\Test\TestJObjectParse\TestJObjectParse\Program.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150115/youtubeextractor

            // X:\jsc.svn\examples\merge\Test\TestYouTubeExtractor\TestYouTubeExtractor\Program.cs
            // x:\jsc.svn\market\synergy\github\youtubeextractor\external\exampleapplication\program.cs

            //var p = 1;

            for (int p = 1; p < 96; p++)
                foreach (var src in new[] {
                    //$"http://consciousresonance.net/?page_id=1587&paged={p}",
                    //$"https://faustuscrow.wordpress.com/page/{p}/",
                    //$"https://hiddenlighthouse.wordpress.com/page/{p}/",
                    //$"https://zproxy.wordpress.com/page/{p}/"
                    
                    "https://zproxy.wordpress.com/page/" + p + "/"

                })
                {



                    Console.WriteLine("DownloadString ... " + new { p, src });

                    // Additional information: The underlying connection was closed: An unexpected error occurred on a send.
                    // Additional information: The operation has timed out.
                    // Additional information: The underlying connection was closed: The connection was closed unexpectedly.

                    // Additional information: The request was aborted: Could not create SSL/TLS secure channel.
                    // xml tidy?
                    var page0 = new WebClient().DownloadStringOrRetry(src);

                    Console.WriteLine("DownloadString ... done " + new { p });
                    // http://stackoverflow.com/questions/281682/reference-to-undeclared-entity-exception-while-working-with-xml
                    // Additional information: Reference to undeclared entity 'raquo'. Line 11, position 73.
                    //  Additional information: The 'p' start tag on line 105 position 2 does not match the end tag of 'div'.Line 107, position 10.
                    // http://stackoverflow.com/questions/15926142/regular-expression-for-finding-href-value-of-a-a-link

                    // Command: Checkout from https://htmlagilitypack.svn.codeplex.com/svn/trunk, revision HEAD, Fully recursive, Externals included  


                    //// could it be used within a service worker?
                    //var doc = new HtmlAgilityPack.HtmlDocument();
                    //doc.LoadHtml(page0);

                    //var hrefList = doc.DocumentNode.SelectNodes("//a")
                    //                  .Select(xp => xp.GetAttributeValue("href", "not found"))
                    //                  .ToList();
                    ////var xpage0 = XElement.Parse(

                    //    System.Net.WebUtility.HtmlDecode(page0)

                    //    );

                    // http://htmlagilitypack.codeplex.com/

                    //Console.WriteLine("DownloadString ... done " + new { p, hrefList.Count });

                    //p++;


                    // https://www.youtube.com/embed/FhEYvOYceNs?

                    while (!string.IsNullOrEmpty(page0))
                    {
                        // <iframe src="//www.youtube.com/embed/umfjGNlxWcw" 

                        // <span class='embed-youtube' style='text-align:center; display: block;'><iframe class='youtube-player' type='text/html' width='640' height='390' src='https://www.youtube.com/embed/8vwzVVJ9lvg?version=3&#038;rel=1&#038;fs=1&#038;showsearch=0&#038;showinfo=1&#038;iv_load_policy=1&#038;wmode=transparent' frameborder='0' allowfullscreen='true'></iframe></span>

                        var prefix = "//www.youtube.com/embed/";
                        //var prefix = "https://www.youtube.com/embed/";
                        var embed = page0.SkipUntilOrEmpty(prefix);
                        var id = embed.TakeUntilIfAny("\"").TakeUntilIfAny("?");
                        var link = prefix + id;

                        page0 = embed.SkipUntilOrEmpty("?");


                        Console.WriteLine();

                        try
                        {

                            // a running applicaion should know when it can reload itself
                            // when all running tasks are complete and no new tasks are to be taken.

                            var videoUrl = link;

                            bool isYoutubeUrl = DownloadUrlResolver.TryNormalizeYoutubeUrl(videoUrl, out videoUrl);

                            //Console.WriteLine(new { sw.ElapsedMilliseconds, px, videoUrl });



                            // wont help
                            //var y = DownloadUrlResolver.GetDownloadUrls(link);
                            //var j = DownloadUrlResolver.LoadJson(videoUrl);
                            var c = new WebClient().DownloadString(videoUrl);

                            // "Kryon - Timing o..." The YouTube account associated with this video has been terminated due to multiple third-party notifications of copyright infringement.

                            // <link itemprop="url" href="http://www.youtube.com/user/melania1172">

                            //                    { videoUrl = http://youtube.com/watch?v=li0E4_7ap3g, ch_name = , userurl = https://youtube.com/user/ }
                            //{ url = http://youtube.com/watch?v=li0E4_7ap3g }
                            //{ err = YoutubeExtractor.YoutubeParseException: Could not parse the Youtube page for URL http://youtube.com/watch?v=li0E4_7ap3g

                            // <h1 id="unavailable-message" class="message">

                            //  'IS_UNAVAILABLE_PAGE': false,
                            var unavailable =

                                !c.Contains("'IS_UNAVAILABLE_PAGE': false") ?
                                c.SkipUntilOrEmpty("<h1 id=\"unavailable-message\" class=\"message\">").TakeUntilOrEmpty("<").Trim() : "";
                            if (unavailable != "")
                            {
                                // 180?
                                countunavailable++;
                                Console.Title = new { countunavailable }.ToString();
                                Console.WriteLine(new { videoUrl, unavailable });
                                //Thread.Sleep(3000);
                                continue;
                            }

                            var ch = c.SkipUntilOrEmpty(" <div class=\"yt-user-info\">").SkipUntilOrEmpty("<a href=\"/channel/");
                            var ch_id = ch.TakeUntilOrEmpty("\"");
                            var ch_name = ch.SkipUntilOrEmpty(">").TakeUntilOrEmpty("<");

                            // https://www.youtube.com/channel/UCP-Q2vpvpQmdShz-ASBj2fA/videos


                            // ! originally there were users, now there are thos gplus accounts?

                            //var usertoken = c.SkipUntilOrEmpty("<link itemprop=\"url\" href=\"http://www.youtube.com/user/");
                            //var userid = usertoken.TakeUntilOrEmpty("\"");
                            ////var ch_name = ch.SkipUntilOrEmpty(">").TakeUntilOrEmpty("<");

                            //var userurl = "https://youtube.com/user/" + userid;

                            Console.WriteLine(new { src, link, ch_name, ch_id });
                            //Console.WriteLine(new { page0, link });

                            // Our test youtube link
                            //const string link = "https://www.youtube.com/watch?v=BJ9v4ckXyrU";
                            //Debugger.Break();

                            // rewrite broke JObject Parse.
                            // Additional information: Bad JSON escape sequence: \5.Path 'args.afv_ad_tag_restricted_to_instream', line 1, position 3029.

                            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151022

                            var ytconfig = c.SkipUntilOrEmpty("<script>var ytplayer = ytplayer || {};ytplayer.config =").TakeUntilOrEmpty(";ytplayer.load =");


                            dynamic ytconfigJSON = Newtonsoft.Json.JsonConvert.DeserializeObject(ytconfig);
                            var ytconfigJSON_args = ytconfigJSON.args;

                            // not available for 8K 360 3D ?
                            string ytconfigJSON_args_adaptive_fmts = ytconfigJSON.args.adaptive_fmts;

                            //if (ytconfigJSON_args_adaptive_fmts == null)
                            //    Debugger.Break();

                            string adaptive_fmts = Uri.UnescapeDataString(ytconfigJSON_args_adaptive_fmts  ?? "");


                            // projection_type=3


                            // +		((dynamic)((Newtonsoft.Json.Linq.JObject)(ytconfigJSON))).args




                            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151106/spherical3d
                            var Spherical3D = adaptive_fmts.Contains("projection_type=3");
                            var Spherical = adaptive_fmts.Contains("projection_type=2");


                            if (!Spherical)
                                if (!Spherical3D)
                                {
                                    var get_video_info0 = new WebClient().DownloadString("https://www.youtube.com/get_video_info?html5=1&video_id=" + id);
                                    var get_video_info1 = Uri.UnescapeDataString(get_video_info0);

                                    var statusfail = get_video_info1.Contains("status=fail");

                                    if (statusfail)
                                    {
                                    }
                                    else
                                    {
                                        // url_encoded_fmt_stream_map=type=video
                                        Spherical3D = get_video_info1.Contains("projection_type=3");
                                        Spherical = get_video_info1.Contains("projection_type=2");
                                    }
                                }

                            // "yt:projectionType"), t = Ss(b, "yt:stereoLayout"), u = "equirectangular" == n, x, z;
                            //u && "layout_top_bottom" ==
                            //t ? x = 3 : u && !n ? x = 2 : "layout_left_right" ==


                            // jsc rewriter breaks it?
                            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);
                            // Additional information: The remote name could not be resolved: 'youtube.com'

                            //DownloadAudio(videoInfos);
                            DownloadVideo(ch_name,

                                Spherical3D ? projection.x360TB :
                                Spherical ? projection.x360 :
                                projection.x2D

                                , link, videoInfos);

                            //{
                            //    err = System.IO.IOException: Unable to read data from the transport connection: An established connection was aborted by the software in your host machine. --->System.Net.Sockets.SocketException: An established connection was aborted by the software in your host machine
                            //    at System.Net.Sockets.Socket.Receive(Byte[] buffer, Int32 offset, Int32 size, SocketFlags socketFlags)
                            //   at System.Net.Sockets.NetworkStream.Read(Byte[] buffer, Int32 offset, Int32 size)
                            //   -- - End of inner exception stack trace-- -
                            //    at System.Net.ConnectStream.Read(Byte[] buffer, Int32 offset, Int32 size)
                            //   at YoutubeExtractor.VideoDownloader.Execute()
                        }
                        catch (Exception err)
                        {
                            //ScriptCoreLib.Desktop.TaskbarProgress.SetMainWindowError();

                            // https://discutils.codeplex.com/
                            // Message = "Result cannot be called on a failed Match."
                            Console.WriteLine(new { err });

                            Thread.Sleep(3000);
                            //ScriptCoreLib.Desktop.TaskbarProgress.SetMainWindowNoProgress();

                        }

                        //goto next;

                    }

                }

            Debugger.Break();

        }


        // CALLED BY?
        public static void DoVideo(string link)
        {
            //Debugger.Break();

            try
            {

                // a running applicaion should know when it can reload itself
                // when all running tasks are complete and no new tasks are to be taken.

                var videoUrl = link;

                var prefix2 = "//www.youtube.com/watch?v=";


                var prefix = "//www.youtube.com/embed/";
                //var prefix = "https://www.youtube.com/embed/";
                var embed = link.SkipUntilOrNull(prefix) ?? link.SkipUntilOrNull(prefix2);
                var id = embed.TakeUntilIfAny("\"").TakeUntilIfAny("?");

                bool isYoutubeUrl = DownloadUrlResolver.TryNormalizeYoutubeUrl(videoUrl, out videoUrl);

                //Console.WriteLine(new { sw.ElapsedMilliseconds, px, videoUrl });



                // wont help
                //var y = DownloadUrlResolver.GetDownloadUrls(link);
                //var j = DownloadUrlResolver.LoadJson(videoUrl);
                var c = new WebClient().DownloadString(videoUrl);

                // "Kryon - Timing o..." The YouTube account associated with this video has been terminated due to multiple third-party notifications of copyright infringement.

                // <link itemprop="url" href="http://www.youtube.com/user/melania1172">

                //                    { videoUrl = http://youtube.com/watch?v=li0E4_7ap3g, ch_name = , userurl = https://youtube.com/user/ }
                //{ url = http://youtube.com/watch?v=li0E4_7ap3g }
                //{ err = YoutubeExtractor.YoutubeParseException: Could not parse the Youtube page for URL http://youtube.com/watch?v=li0E4_7ap3g

                // <h1 id="unavailable-message" class="message">

                //  'IS_UNAVAILABLE_PAGE': false,
                var unavailable =

                    !c.Contains("'IS_UNAVAILABLE_PAGE': false") ?
                    c.SkipUntilOrEmpty("<h1 id=\"unavailable-message\" class=\"message\">").TakeUntilOrEmpty("<").Trim() : "";
                if (unavailable != "")
                {
                    Console.WriteLine(new { videoUrl, unavailable });
                    Thread.Sleep(3000);
                    return;
                }

                var ch = c.SkipUntilOrEmpty(" <div class=\"yt-user-info\">").SkipUntilOrEmpty("<a href=\"/channel/");
                var ch_id = ch.TakeUntilOrEmpty("\"");
                var ch_name = ch.SkipUntilOrEmpty(">").TakeUntilOrEmpty("<");

                // https://www.youtube.com/channel/UCP-Q2vpvpQmdShz-ASBj2fA/videos


                // ! originally there were users, now there are thos gplus accounts?

                //var usertoken = c.SkipUntilOrEmpty("<link itemprop=\"url\" href=\"http://www.youtube.com/user/");
                //var userid = usertoken.TakeUntilOrEmpty("\"");
                ////var ch_name = ch.SkipUntilOrEmpty(">").TakeUntilOrEmpty("<");

                //var userurl = "https://youtube.com/user/" + userid;

                Console.WriteLine(new { link, ch_name, ch_id });
                //Console.WriteLine(new { page0, link });

                // Our test youtube link
                //const string link = "https://www.youtube.com/watch?v=BJ9v4ckXyrU";
                //Debugger.Break();

                // rewrite broke JObject Parse.
                // Additional information: Bad JSON escape sequence: \5.Path 'args.afv_ad_tag_restricted_to_instream', line 1, position 3029.



                //   <script>var ytplayer = ytplayer || {};ytplayer.config =


                var ytconfig = c.SkipUntilOrEmpty("<script>var ytplayer = ytplayer || {};ytplayer.config =").TakeUntilOrEmpty(";ytplayer.load =");


                dynamic ytconfigJSON = Newtonsoft.Json.JsonConvert.DeserializeObject(ytconfig);
                var ytconfigJSON_args = ytconfigJSON.args;
                string ytconfigJSON_args_adaptive_fmts = ytconfigJSON.args.adaptive_fmts;
                string adaptive_fmts = Uri.UnescapeDataString(ytconfigJSON_args_adaptive_fmts);


                // projection_type=3


                // +		((dynamic)((Newtonsoft.Json.Linq.JObject)(ytconfigJSON))).args


                //var get_video_info = new WebClient().DownloadString("https://www.youtube.com/get_video_info?html5=1&video_id=" + id);

                //var statusfail = get_video_info.Contains("status=fail");

                //if (statusfail)
                //    return;


                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151106/spherical3d
                var Spherical3D = adaptive_fmts.Contains("projection_type=3");
                var Spherical = adaptive_fmts.Contains("projection_type=2");

                // "yt:projectionType"), t = Ss(b, "yt:stereoLayout"), u = "equirectangular" == n, x, z;
                //u && "layout_top_bottom" ==
                //t ? x = 3 : u && !n ? x = 2 : "layout_left_right" ==

                // jsc rewriter breaks it?
                IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link, decryptSignature: false);
                // Additional information: The remote name could not be resolved: 'youtube.com'

                //DownloadAudio(videoInfos);
                DownloadVideo(ch_name,



                                Spherical3D ? projection.x360TB :
                                Spherical ? projection.x360 :
                                projection.x2D

                    , link, videoInfos);

                //{
                //    err = System.IO.IOException: Unable to read data from the transport connection: An established connection was aborted by the software in your host machine. --->System.Net.Sockets.SocketException: An established connection was aborted by the software in your host machine
                //    at System.Net.Sockets.Socket.Receive(Byte[] buffer, Int32 offset, Int32 size, SocketFlags socketFlags)
                //   at System.Net.Sockets.NetworkStream.Read(Byte[] buffer, Int32 offset, Int32 size)
                //   -- - End of inner exception stack trace-- -
                //    at System.Net.ConnectStream.Read(Byte[] buffer, Int32 offset, Int32 size)
                //   at YoutubeExtractor.VideoDownloader.Execute()
            }
            catch (Exception err)
            {
                //ScriptCoreLib.Desktop.TaskbarProgress.SetMainWindowError();

                // https://discutils.codeplex.com/
                // Message = "Result cannot be called on a failed Match."
                Console.WriteLine(new { err });

                Thread.Sleep(3000);
                //ScriptCoreLib.Desktop.TaskbarProgress.SetMainWindowNoProgress();

            }
        }
    }
}

//Severity Code    Description Project File Line
//Error Error opening icon file r:\opensource\github\taglib-sharp\src -- Access to the path 'r:\opensource\github\taglib-sharp\src' is denied.taglib-sharp r:\opensource\github\taglib-sharp\src\CSC

//Error	2	Assembly generation failed -- Referenced assembly 'ICSharpCode.SharpZipLib' does not have a strong name	x:\opensource\github\taglib-sharp\src\CSC	taglib-sharp
// Error	6	Cryptographic failure while signing assembly 'x:\opensource\github\taglib-sharp\src\obj\Debug\taglib-sharp.dll' -- 'Error reading key file 'taglib-sharp.snk' -- The system cannot find the file specified. '	x:\opensource\github\taglib-sharp\src\CSC	taglib-sharp

