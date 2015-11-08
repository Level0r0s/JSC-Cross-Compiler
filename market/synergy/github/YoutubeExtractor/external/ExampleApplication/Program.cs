using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using YoutubeExtractor;

namespace ExampleApplication
{
    internal class Program
    {
        private static void DownloadAudio(IEnumerable<VideoInfo> videoInfos)
        {
            /*
             * We want the first extractable video with the highest audio quality.
             */
            VideoInfo video = videoInfos
                .Where(info => info.CanExtractAudio)
                .OrderByDescending(info => info.AudioBitrate)
                .First();

            /*
             * Create the audio downloader.
             * The first argument is the video where the audio should be extracted from.
             * The second argument is the path to save the audio file.
             */

            var audioDownloader = new AudioDownloader(video,
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), video.Title + video.AudioExtension));

            // Register the progress events. We treat the download progress as 85% of the progress
            // and the extraction progress only as 15% of the progress, because the download will
            // take much longer than the audio extraction.
            audioDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage * 0.85);
            audioDownloader.AudioExtractionProgressChanged += (sender, args) => Console.WriteLine(85 + args.ProgressPercentage * 0.15);

            /*
             * Execute the audio downloader.
             * For GUI applications note, that this method runs synchronously.
             */
            audioDownloader.Execute();
        }

        private static void DownloadVideo(IEnumerable<VideoInfo> videoInfos)
        {
            /*
             * Select the first .mp4 video with 360p resolution
             */
            VideoInfo video = videoInfos
                //.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);
                //.Where(info => info.VideoType == VideoType.Mp4).OrderBy(info => info.Resolution).Last();
                //.SingleOrDefault(x => x.FormatCode == 251);
                .SingleOrDefault(x => x.FormatCode == 249);


            video.DecryptDownloadUrl();

            /*
             * Create the video downloader.
             * The first argument is the video to download.
             * The second argument is the path to save the video file.
             */
            var videoDownloader = new VideoDownloader(video,
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),

                // this.SavePath = "C:\\Users\\Administrator\\Documents\\What is VR Video?"

                video.Title.Replace(":", "_")
                        .Replace("*", "_")
                        .Replace("?", "_") 
                        .Replace("°", "_")
                        + video.VideoExtension));

            // Register the ProgressChanged event and print the current progress
            videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);

            /*
             * Execute the video downloader.
             * For GUI applications note, that this method runs synchronously.
             */
            videoDownloader.Execute();
        }


        private static void Main()
        {



            // X:\jsc.svn\examples\merge\Test\TestYouTubeExtractor\TestYouTubeExtractor\Program.cs
            // x:\jsc.svn\market\synergy\github\youtubeextractor\external\exampleapplication\program.cs

            // Our test youtube link
            //const string link = "https://www.youtube.com/watch?v=SgQwBSHJNp0";
            const string link = "https://www.youtube.com/watch?v=bdq4H1CIehI";
            Debugger.Break();

            // https://www.youtube.com/get_video_info?html5=1&video_id=K_J8k43gUhY&cpn=31lmcWsqKXH4uh4N&eurl&el=embedded&hl=en_US&sts=16623&lact=2&width=1920&height=376&authuser=0&pageid=115376870514737323384&ei=nT2mVcbNIJC7cIXOq4AL&iframe=1&c=WEB&cver=html5&cplayer=UNIPLAYER&cbr=Chrome&cbrver=43.0.2357.134&cos=Windows&cosver=6.3

            // projection_type%3D1

            //  var c = Ik(b);
            //    var c = b || {};

            //        c.adaptive_fmts && (a.adaptiveFormats = c.adaptive_fmts);
            //  a.adaptiveFormats

            //     Ot(g) && (k = a.size.split("x"), k = new Tt(+k[0], +k[1], +a.fps, +a.projection_type, void 0, void 0, c));

            // this.projectionType=d||0};f

            //f.getVideoData = function() {
            //    return this.j

            //function X(a, b) {
            //    return b ? 1 == b ? a.o : a.ra[b] || null : a.D

            //f.getVideoData = function(a) {
            //    return (a = X(this.app, a || this.playerType)) && a.getVideoData()

            //WG.j = function(a) {
            //    var b;
            //    b = a.getVideoData();
            //    a = a.X().experiments;
            //    var c;
            //    !(c = a.Aa) || (c = 1 == b.Ia || 2 == b.Ia) || (c = b.Bc["3D"]) || (c = b.Bc("yt3d:enable"), c = "true" == c || "LR" == c || "RL" == c);
            //    if (c)
            //        b = "Anaglyph3D";
            //    else
            //    {
            //        a:
            //        {
            //            if (b.o)
            //        for (var d in b.o.j)
            //                    if ((c = b.o.j[d].info.video) && 2 == c.projectionType)
            //                    {
            //                        b = !0;
            //                        break a
            //                    }
            //            b = !1
            //        }
            //        b = b ? "Spherical" : a.Z ? "NoOp" : null
            //    }
            //    return b


            // jsc rewriter breaks it?
            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);

            //DownloadAudio(videoInfos);
            DownloadVideo(videoInfos);
        }
    }
}