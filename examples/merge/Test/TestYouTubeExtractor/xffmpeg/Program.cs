using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace xffmpeg
{

    class Program
    {
        private static void DownloadVideo0(IEnumerable<VideoInfo> videoInfos)
        {
            var mp4 = videoInfos.Where(x => x.VideoType == VideoType.Mp4);
            var mp4video = mp4.Where(x => x.Resolution > 0).OrderBy(x => x.Resolution).ToArray();
            var mp4audio = mp4video.Where(x => x.AudioBitrate > 0).OrderBy(x => x.Resolution).ToArray();


            VideoInfo video = //videoInfos.FirstOrDefault(k => k.FormatCode == 299)
             mp4audio.OrderBy(info => info.Resolution).Last();

            video.DecryptDownloadUrl();



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

            var apkfriendlytitle = new string(
                Title.Select(x => x < 127 ? x : '_').ToArray()
                );

            // apkfriendlytitle = "___360_ - __________find the truth 360 degree trick movie ."



            var px = Path.Combine(
                //Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "x:/media",

                Title + video.VideoExtension);

            var pxa = Path.Combine(
                 //Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                 "x:/media",

                 apkfriendlytitle + video.VideoExtension);


            if (!File.Exists(pxa))
                if (File.Exists(px))
                {
                    // upgrade old naming to apk friendly.
                    File.Move(px, pxa);
                }

            var pxa_mp3 = Path.ChangeExtension(pxa, ".mp3");
            var pxa_mp4_mp4 = Path.ChangeExtension(pxa, ".mp4.mp4");
            var pxa_mp3_mp4 = Path.ChangeExtension(pxa, ".mp3.mp4");


            // do we have the end result?

            if (File.Exists(pxa_mp3_mp4))
            {
                // all done
                Debugger.Break();
            }


            if (File.Exists(pxa))
            {
            }
            else
            {
                var videoDownloader = new VideoDownloader(video, pxa);
                videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);
                videoDownloader.Execute();
            }

            // any reason to attempt upgrade?

            if (mp4video.Last().Resolution > mp4audio.Last().Resolution)
            {
                var ffmpeg = @"X:\util\ffmpeg-20150609-git-7c9fcdf-win64-static\ffmpeg-20150609-git-7c9fcdf-win64-static\bin\ffmpeg.exe";

                if (!File.Exists(pxa_mp4_mp4))
                {
                    var videoDownloader = new VideoDownloader(mp4video.Last(), pxa_mp4_mp4);
                    videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);
                    videoDownloader.Execute();
                }

                // extract mp3

                // "X:\util\ffmpeg-20150609-git-7c9fcdf-win64-static\ffmpeg-20150609-git-7c9fcdf-win64-static\bin\ffmpeg.exe" -i "X:\media\Dolphins 360° 4K.mp4" "C:\Users\Administrator\Documents\Dolphins 360_ 4K.mp3"


                // "X:\util\ffmpeg-20150609-git-7c9fcdf-win64-static\ffmpeg-20150609-git-7c9fcdf-win64-static\bin\ffmpeg.exe" -i "C:\Users\Administrator\Documents\Dolphins 360_ 4K.mp4" -i "C:\Users\Administrator\Documents\Dolphins 360_ 4K.mp3" -shortest "C:\Users\Administrator\Documents\Dolphins 360_ 4K.mp3.mp4"

                if (!File.Exists(pxa_mp3))
                    Process.Start(ffmpeg,
                        "-i \"" + pxa + "\""
                        + " \"" + pxa_mp3 + "\"").WaitForExit();

                // merge and delete

                // Additional information: The operation was canceled by the user

                Process.Start(ffmpeg,
                   " -i \"" + pxa_mp3 + "\""
                   + " -i \"" + pxa_mp4_mp4 + "\""
                   + " -c:v copy -shortest -shortest \"" + pxa_mp3_mp4 + "\"").WaitForExit();

                File.Delete(pxa_mp3);
                File.Delete(pxa_mp4_mp4);
                File.Delete(pxa);
            }

            // X:\jsc.svn\examples\merge\Test\TestYouTubeExtractor\xffmpeg\Program.cs
        }


        static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150609/360
            // https://code.google.com/p/android/issues/detail?id=8185

            const string link = "https://www.youtube.com/watch?v=sLprVF6d7Ug";
            Debugger.Break();



            // jsc rewriter breaks it?
            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);

            //DownloadAudio(videoInfos);
            DownloadVideo0(videoInfos);

        }
    }
}
