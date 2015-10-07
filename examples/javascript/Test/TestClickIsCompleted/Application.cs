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
using TestClickIsCompleted;
using TestClickIsCompleted.Design;
using TestClickIsCompleted.HTML.Pages;
using System.Diagnostics;

namespace TestClickIsCompleted
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
            new { }.With(
                async delegate
                {
                    var stop = new IHTMLButton {
                        "stop"
                    }.AttachToDocument().async.onclick;

                    var sw = Stopwatch.StartNew();
                    new IHTMLPre { () => new { stop.IsCompleted, sw.ElapsedMilliseconds } }.AttachToDocument();

                    while (true)
                    {
                        await Task.Delay(1000 / 15);

                        if (stop.IsCompleted)
                        {
                            sw.Stop();
                            return;
                        }
                    }
                }
            );
        }

    }
}
