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

            var dir = new DirectoryInfo("x:/vr/sun/");
            dir.Create();


            Action<string> f = iso =>
            {
                var target = dir.FullName + $"/{iso}.png";

                if (File.Exists(target))
                    return;

                new WebClient().DownloadFile($"http://www.timeanddate.com/scripts/sunmap.php?obj=moon&iso={iso}", target);
            };

            for (int day = 1; day <= 18; day++)
                for (int h = 0; h <= 23; h++)
                    for (int m = 0; m <= 59; m += 1)
                    {
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
