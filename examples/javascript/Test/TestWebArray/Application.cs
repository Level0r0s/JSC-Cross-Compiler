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
using TestWebArray;
using TestWebArray.Design;
using TestWebArray.HTML.Pages;

namespace TestWebArray
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
            new { }.With(
                async delegate
                {
                    var foo = await base.GetFoo();

                    new IHTMLPre { new { foo } }.AttachToDocument();
                    //var foo2 = await base.GetFoo2();
                    var foo2 = new List<xLatLng> { };
                    await base.GetFoo2(foo2.Add);
                    //new IHTMLPre { new { foo2.Length } }.AttachToDocument();
                    new IHTMLPre { new { foo2.Count } }.AttachToDocument();

                    //var foo3 = await base.GetFoo3();
                    //new IHTMLPre { new { foo3.Length } }.AttachToDocument();

                    var foo4 = await base.GetFoo4();
                    new IHTMLPre { new { foo4 } }.AttachToDocument();
                }
            );

        }

    }
}
