using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFMPEGInputReindex
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150729/timelapse
            // "X:\util\ffmpeg-20150609-git-7c9fcdf-win64-static\ffmpeg-20150609-git-7c9fcdf-win64-static\bin\ffmpeg.exe"  -framerate 60 -i "%04d.jpg" -s:v 2560x1920 -c:v libx264 -profile:v high -crf 20 -pix_fmt yuv420p x:/vr/tape1crop.mp4

            //var storage = @"X:\sdcard\skynet\20150728\tape1\720p";
            //var storage = @"X:\vr\tape1";
            //var storage = @"X:\vr\tape6";
            //var storage = @"X:\vr\tape6daymoon2";
            var storage = "x:/vr/tape6stars2";

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150801/hostednetwork

            var ffmpegNNNNfiles0 = Directory.GetFiles(storage).OrderBy(x => new FileInfo(x).LastWriteTime).Select(
            (x, i) =>
            {
                var iNNNN = i.ToString("00000");
                var t = Path.GetDirectoryName(x) + "\\." + iNNNN + ".jpg";

                if (new FileInfo(x).FullName != new FileInfo(t).FullName)
                {
                    File.Move(
                        x, t

                    );
                }


                return new { x, i, iNNNN };
            }
        ).ToArray();


            var ffmpegNNNNfiles = Directory.GetFiles(storage).OrderBy(x => new FileInfo(x).LastWriteTime).Select(
              (x, i) =>
              {
                  var iNNNN = i.ToString("00000");
                  var t = Path.GetDirectoryName(x) + "\\" + iNNNN + ".jpg";

                  if (new FileInfo(x).FullName != new FileInfo(t).FullName)
                  {
                      File.Move(
                          x, t

                      );
                  }


                  return new { x, i, iNNNN };
              }
          ).ToArray();

        

            // ffmpegNNNNfiles = {<>f__AnonymousType0<string,int,string>[47357]}

            Debugger.Break();
        }
    }
}
