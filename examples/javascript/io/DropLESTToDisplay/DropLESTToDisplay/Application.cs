using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DropLESTToDisplay;
using DropLESTToDisplay.Design;
using DropLESTToDisplay.HTML.Pages;
using System.Diagnostics;
using System.IO;

namespace DropLESTToDisplay
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // user gets to drop a csv
            // with X and Y fields.
            // where is our csv parser? assetslibary?

            new { }.With(
                async delegate
                {
                    // while await ondrop ?
                    Native.document.documentElement.ondragover += ee =>
                    {
                        ee.stopPropagation();
                        ee.preventDefault();

                        ee.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.

                        Native.body.style.backgroundColor = "cyan";

                        // await leave? unless ondrop?
                    };

                    Native.document.documentElement.ondragleave += delegate
                    {
                        Native.body.style.backgroundColor = "";
                    };



                    new IHTMLPre { "drop a file" }.AttachToDocument();

                    // { name = cncnet5.ini, size = 1985 }

                    //Native.document.documentElement.ondrop += e =>
                    var e = await Native.document.documentElement.async.ondrop;

                    Native.body.style.backgroundColor = "yellow";

                    e.stopPropagation();
                    e.preventDefault();

                    foreach (var f in e.dataTransfer.files.AsEnumerable())
                    {
                        new IHTMLPre { new { f.name, f.size } }.AttachToDocument();
                        // { name = download.csv, size = 20851425 }

                        var sw = Stopwatch.StartNew();

                        var bytes = await f.readAsBytes();

                        new IHTMLPre { new { sw.ElapsedMilliseconds } }.AttachToDocument();
                        // { ElapsedMilliseconds = 72 }

                        //var m = new MemoryStream(bytes);
                        //var r = new StreamReader(m);

                        var xstring = Encoding.UTF8.GetString(bytes);


                        //{ name = download.csv, size = 20851425 }
                        //{ ElapsedMilliseconds = 104 }
                        //{ ElapsedMilliseconds = 5351, R1C1 = ﻿Jkn }
                        //{ ElapsedMilliseconds = 5390, header = ﻿Jkn;Kohanimi;Keel;Kohanime staatus;Kohanime olek;Nimeobjekti liik;Lisainfo;Maakond,omavalitsus,asustusüksus;X;Y; }

                        new IHTMLPre { new { sw.ElapsedMilliseconds, R1C1 = xstring.TakeUntilOrEmpty(";") } }.AttachToDocument();


                        var r = new StringReader(xstring);

                        var header = r.ReadLine();

                        new IHTMLPre { new { sw.ElapsedMilliseconds, header } }.AttachToDocument();

                        // { ElapsedMilliseconds = 11929, header = ﻿Jkn;Kohanimi;Keel;Kohanime staatus;Kohanime olek;Nimeobjekti liik;Lisainfo;Maakond,omavalitsus,asustusüksus;X;Y; }
                    }


                }
            );

        }

    }
}
