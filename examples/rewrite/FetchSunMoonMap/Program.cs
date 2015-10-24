using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FetchSunMoonMap
{
    class Program
    {
        static void Main(string[] args)
        {

            // http://www.timeanddate.com/scripts/sunmap.php?obj=moon&iso=20151016T1102

            var dir = new DirectoryInfo("x:/vr/sunmap2/");
            dir.Create();


            Action<string> f = iso =>
            {
                // most re jpgs!
                // which is it?


                //var target = dir.FullName + $"/{iso}.png";
                var target0 = dir.FullName + $"/{iso}.jpg";
                var target1 = dir.FullName + $"/{iso}.png";

                var target = target0;

                if (File.Exists(target1))
                {
                    //if (new FileInfo(target1).Length > 100KB)
                    if (new FileInfo(target1).Length > 100 * 1024)
                        target = target1;
                    else
                        // updated name
                        File.Move(target1, target);
                }

                if (File.Exists(target))
                    return;




                // http://www.timeanddate.com/worldclock/sunearth.html?iso=20151024T1316&earth=1
                // http://www.timeanddate.com/scripts/sunmap.php?iso=20151024T1316&earth=1

                //new WebClient().DownloadFile($"http://www.timeanddate.com/scripts/sunmap.php?obj=moon&iso={iso}", target);
                var c = new WebClient();
                c.DownloadProgressChanged += (sender, xargs) =>
                {
                    Console.Title = new { iso, xargs.ProgressPercentage }.ToString();

                };

                c.DownloadFileTaskAsync($" http://www.timeanddate.com/scripts/sunmap.php?iso={iso}&earth=1", target).Wait();


                if (new FileInfo(target).Length > 100 * 1024)
                    File.Move(target, target1);

            };

            for (int day = 1; day <= 24; day++)
                for (int h = 0; h <= 23; h++)
                //for (int m = 0; m <= 59; m += 1)
                {
                    var m = 0;

                    var dd = day.ToString("00");
                    var hh = h.ToString("00");
                    var mm = m.ToString("00");

                    var iso = $"201510{dd}T{hh}{mm}";

                    Console.WriteLine(new { iso });
                    f(iso);

                }

            // new WebClient().DownloadFile("http://www.timeanddate.com/scripts/sunmap.php?obj=moon&iso=20151016T1100", dir.FullName + "/20151016T1100.png");

            ;
        }
    }
}
