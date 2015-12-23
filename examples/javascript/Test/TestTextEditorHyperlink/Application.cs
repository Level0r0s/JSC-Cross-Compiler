using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Controls;
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
using TestTextEditorHyperlink;
using TestTextEditorHyperlink.Design;
using TestTextEditorHyperlink.HTML.Pages;

namespace TestTextEditorHyperlink
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // java -jar  /home/xmikro/Desktop/staging/TestTextEditorHyperlink.ApplicationWebService.exe


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://w3c.github.io/editing/execCommand.html

            new IHTMLAnchor { href = Native.document.location.href, innerText = Native.document.location.href }.AttachToDocument();


            new { }.With(
                async delegate
                {
                    Native.document.documentElement.style.fontFamily = IStyle.FontFamilyEnum.Courier;

                    var e = new TextEditor(Native.document.body);

                    Native.document.body.style.backgroundColor = "yellow";

                    //await e.Frame.async.onload;

                    Native.document.body.style.backgroundColor = "cyan";

                    e.Document.documentElement.style.color = "red";



                    new IHTMLInput { value = "http://example.com" }.AttachToDocument();

                    // http://www.quirksmode.org/dom/execCommand.html

                    new IHTMLButton { "unlink" }.AttachToDocument().onclick += delegate
                    {
                        e.DoCommand("unlink", "");

                    };
                    new IHTMLButton { "createlink" }.AttachToDocument().onclick += delegate
                    {
                        e.DoCommand("createlink", "http://example.com");


                    };
                }
                );
        }

    }
}
