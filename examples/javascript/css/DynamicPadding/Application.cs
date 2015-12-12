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
using DynamicPadding;
using DynamicPadding.Design;
using DynamicPadding.HTML.Pages;

namespace DynamicPadding
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
            // we have title
            // we have content
            // lets have padding done dynamically.

            // head, title {
            //    display: block;
            //}


            var range = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = 0, max = 1000 }.AttachToDocument();

            new IStyle(range)
            {
                left = "1em",
                bottom = "1em",

                position = IStyle.PositionEnum.@fixed
            };


            range.AttachTo(Native.document.documentElement);



            var title = new IStyle(IHTMLElement.HTMLElementEnum.title)
            {
                paddingLeft = "0px",
                //boxShadow = "17px -5px 88px 29px rgba(0,0,0,0.69)"
            };

            var p = new IStyle(IHTMLElement.HTMLElementEnum.p)
            {
                paddingLeft = "0px",
                //boxShadow = "17px -5px 88px 29px rgba(0,0,0,0.69)"
            };

            Native.window.onframe += delegate
            {
                title.paddingLeft = range.valueAsNumber + "px";
                p.paddingLeft = range.valueAsNumber + "px";
            };

            Native.window.onresize += delegate
            {
                range.valueAsNumber = Native.window.Width / 4;
            };
        }

    }
}
