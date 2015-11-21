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
using TestDropText;
using TestDropText.Design;
using TestDropText.HTML.Pages;

namespace TestDropText
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

                    do
                    {

                        //while (var v = Native.document.documentElement.ondrop)
                        //var v = await Native.document.documentElement.async.ondrop;
                        var v = await Native.document.documentElement.async.ondroptext;


                        new IHTMLPre {
                            //new { text = v.dataTransfer.getData("Text")}
                            //new { v.text}
                            v
                        }.AttachToDocument();


                    }
                    while (true);
                }
            );

        }

    }
}