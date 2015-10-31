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
using TestDualSSLWebApplication;
using TestDualSSLWebApplication.Design;
using TestDualSSLWebApplication.HTML.Pages;

namespace TestDualSSLWebApplication
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
            // z:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\TcpListenerExtensions.cs

            new { }.With(
                async delegate
                {
                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151031

                    var hostname = Native.document.location.host.TakeUntilIfAny(":");
                    var hostport = Native.document.location.host.SkipUntilOrEmpty(":");


                    var hostport1 = Convert.ToInt32(hostport) + 1;
                    var host1 = hostname + ":" + hostport1;
                    var baseURI1 = "https://" + host1;


                    // is the SSL router knowing of our +1 concept?
                    new IHTMLAnchor { href = baseURI1, innerText = "log in" }.AttachToDocument();
                }
            );
        }

    }
}
