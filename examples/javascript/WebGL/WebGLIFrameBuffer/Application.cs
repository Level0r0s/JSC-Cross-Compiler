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
using WebGLIFrameBuffer;
using WebGLIFrameBuffer.Design;
using WebGLIFrameBuffer.HTML.Pages;

namespace WebGLIFrameBuffer
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

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150824/webgliframebuffer

            // until we have webgl2 up and running,
            // iframes could work as well.
            // what we need is a set of iframes that allow
            // 360deg multishader rendering
            // to run it as a hybrid chrome app, either we need aawppwindow tcp webview or tabs window
        }

    }
}
