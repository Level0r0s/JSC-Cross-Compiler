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
using TestTextAreaToString;
using TestTextAreaToString.Design;
using TestTextAreaToString.HTML.Pages;

namespace TestTextAreaToString
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
            new IHTMLTextArea { value = "hello" }.AttachToDocument().With(
                async e =>
                {
                    //while (await e.async.onchange)

                    while (await e.async.onkeyup)
                    {
                        // can we send data interfaces / fragments / concepts back to server yet?
                        AtText(e);
                    }
                }
            );
        }

    }

    partial class ApplicationWebService
    {
        public void AtText(string e)
        {
            Console.WriteLine(new { e });
        }
    }
}
