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
using TestCommaOne;
using TestCommaOne.Design;
using TestCommaOne.HTML.Pages;

namespace TestCommaOne
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
            //var x = 4505;
            var x = 1701;


            //var xx = (int)Math.Round(x / 100.0);
            //var xx = (int)Math.Floor(x / 100.0);
            var xx = (int)(x / 100.0);

            //var xxx = xx * 0.1;

            var dot = xx + "";

            if (dot.Length > 1)
            {
                dot = dot.Substring(0, dot.Length - 1) + "." + dot.Substring(dot.Length - 1);
            }

            // { dot = 1.7 }


            new IHTMLPre { new { dot } }.AttachToDocument();

            // { xxx = 1.7000000000000002 }

        }

    }
}
