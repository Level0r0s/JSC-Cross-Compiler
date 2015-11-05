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
using TestEventDefaultHandler;
using TestEventDefaultHandler.Design;
using TestEventDefaultHandler.HTML.Pages;

namespace TestEventDefaultHandler
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public event Action Foo = delegate
        {

            new IHTMLPre { "Foo" }.AttachToDocument();
        };

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            Foo();

            Foo += delegate
            {
                new IHTMLPre { "more Foo" }.AttachToDocument();
            };

            Foo();
        }

    }
}
