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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestMemoryStreamReadByte;
using TestMemoryStreamReadByte.Design;
using TestMemoryStreamReadByte.HTML.Pages;

namespace TestMemoryStreamReadByte
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
            var bytes = new byte[] { 0xc0, 0xc1, 0xc2 };

            var m = new MemoryStream(bytes);

            var byte0 = m.ReadByte();
            var byte1 = m.ReadByte();
            var byte2 = m.ReadByte();
            var byte3 = m.ReadByte();

            // { byte0 = 192, byte1 = 193, byte2 = 194, byte3 = 0 }

            new IHTMLPre { new { byte0, byte1, byte2, byte3 } }.AttachToDocument();


        }

    }
}
