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
using TestNestedApplicationType;
using TestNestedApplicationType.Design;
using TestNestedApplicationType.HTML.Pages;

namespace TestNestedApplicationType
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// Your client side code running inside a web browser as JavaScript.
        /// </summary>
        public sealed class Application // : ApplicationWebService
        {
            // 1b84:01:01:0f RewriteToAssembly error: System.NotSupportedException: Type 'TestNestedApplicationType.ApplicationWebService+Application' was not completed.

            /// <summary>
            /// This is a javascript application.
            /// </summary>
            /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
            public Application(IApp page)
            {
                //@"Hello world".ToDocumentTitle();
                //// Send data from JavaScript to the server tier
                //this.WebMethod2(
                //    @"A string from JavaScript.",
                //    value => value.ToDocumentTitle()
                //);
            }

        }


        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

    }


}
