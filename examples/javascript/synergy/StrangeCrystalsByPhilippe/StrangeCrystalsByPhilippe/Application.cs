using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using StrangeCrystalsByPhilippe.Design;
using StrangeCrystalsByPhilippe.HTML.Pages;

namespace StrangeCrystalsByPhilippe
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            // http://www.tamats.com/blog/?p=431#more-431
            Native.window.onresize +=
                delegate
                {
                    var s = (double)Native.window.Width / (double)page.c.width;


                    page.c.style.transform = "scale(" + s + ")";

                };

        }

    }
}
