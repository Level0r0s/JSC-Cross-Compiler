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
using TestObjectIsString;
using TestObjectIsString.Design;
using TestObjectIsString.HTML.Pages;

namespace TestObjectIsString
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

            object goo = "hello world";


            var isstring = goo is string;

            var typeofgoo = goo.GetType();
            var typeofstring = typeof(string);
            var istypeofstring = goo.GetType() == typeofstring;


            // {{ isstring = false }}
            new IHTMLPre { new { isstring, typeofstring, typeofgoo, istypeofstring } }.AttachToDocument();
            // {{ isstring = false, typeofstring = [native] String, typeofgoo = ?function String() { [native code] }, istypeofstring = false }}
        }

    }
}
