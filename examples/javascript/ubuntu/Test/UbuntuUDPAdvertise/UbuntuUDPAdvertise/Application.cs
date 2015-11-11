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
using UbuntuUDPAdvertise;
using UbuntuUDPAdvertise.Design;
using UbuntuUDPAdvertise.HTML.Pages;

namespace UbuntuUDPAdvertise
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
            Native.body.style.backgroundColor = "yellow";

            // cool we are running on ubuntu!

            // did we get a SSL client certificate too?
            // what about mysql?

            new { }.With(
                async delegate
                {
                    var a = new IHTMLButton { "InvokeAdvertise" }.AttachToDocument();

                    while (await a.async.onclick)
                    {

                        await InvokeAdvertise();
                    }


                }
            );

            new { }.With(
                async delegate
                {
                    var a = new IHTMLButton { "InvokeAdvertise2" }.AttachToDocument();

                    while (await a.async.onclick)
                    {

                        await InvokeAdvertise2();
                    }


                }
            );

        }

    }
}
