using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EXIFThumbnail;
using EXIFThumbnail.Design;
using EXIFThumbnail.HTML.Pages;

namespace EXIFThumbnail
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        public Application(IApp page)
        {
            Native.css[IHTMLElement.HTMLElementEnum.img].style.width = "100%";
            Native.css.children.style.width = "100%";

            WebMethod2(
                @"A string from JavaScript.",
                async (name, data64) =>
                {
                    // async using for Attach?

                    if (data64 != null)
                        new IHTMLImage { src = data64 }.AttachToDocument();

                    await new IHTMLButton { name }.AttachToDocument().async.onclick;

                    inspect(name);
                }
            );
        }

    }
}
