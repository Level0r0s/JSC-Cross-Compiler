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
using TestIEXElement;
using TestIEXElement.Design;
using TestIEXElement.HTML.Pages;

namespace TestIEXElement
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // resume Z:\jsc.svn\examples\javascript\test\TestIEFieldToService\ApplicationWebService.cs

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var goobar = new XElement("goo", "bar");

            var other = new XElement("other");

            // will we now loose bar?
            ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq.__XNode.InternalRebuildDocument(
                other,
                goobar
            );

            //var iesucks = new XElement("foo",

            //    // bar goes missing!
            //    //new XElement("goo", "bar")

            //    goobar
            //);
            //new IHTMLPre { new { iesucks } }.AttachToDocument();

            //view-source:55033 202ms enter XContainer Add { content = bar }
            //2015-11-23 19:08:54.236 view-source:55033 203ms XContainer InternalValueInitialize
            //2015-11-23 19:08:54.237 view-source:55033 204ms XContainer Add string { xstring = bar }
            //2015-11-23 19:08:54.237 view-source:55033 204ms XContainer InternalValueInitialize
            //2015-11-23 19:08:54.238 view-source:55033 205ms exit XContainer Add = <foo>bar</foo>
            //2015-11-23 19:08:54.239 view-source:55033 206ms exit XContainer Add = { firstChild = [object Text] }
            //2015-11-23 19:08:54.239 view-source:55033 206ms XContainer InternalValueInitialize
            //2015-11-23 19:08:54.239 view-source:55033 206ms { iesucks = <foo>bar</foo> }

            //Console.WriteLine(new { iesucks });

            new IHTMLPre { new { goobar } }.AttachToDocument();

        }

    }
}
