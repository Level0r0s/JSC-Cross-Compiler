using CSSFormsButtonCursor;
using CSSFormsButtonCursor.Design;
using CSSFormsButtonCursor.HTML.Pages;
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CSSFormsButtonCursor
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new IStyle(IStyleSheet.all[typeof(Button)] + IHTMLElement.HTMLElementEnum.button)
            {
                color = "red",
                textDecoration = "underline",
                cursor = IStyle.CursorEnum.pointer
            };

            // 1:42ms { css = { selectorElement = , rule = { selectorText = html.Button:hover, rule = null } } } 
            var css = Native.css[typeof(Button)].hover;

            //{ css = { selectorElement = , rule = { selectorText = html.Button:hover, rule = null } } }
            // 1:42ms { css = { selectorElement = , rule = { selectorText = html.Button:hover, rule = null } } } 
            Console.WriteLine(
                new { css }
            );

            new IStyle(css + IHTMLElement.HTMLElementEnum.button)
            //new IStyle(IStyleSheet.all[typeof(Button)].hover + IHTMLElement.HTMLElementEnum.button)
            {
                color = "blue",
            };

            new IStyle(IStyleSheet.all[typeof(Button)].active + IHTMLElement.HTMLElementEnum.button)
            {
                color = "black",
            };

            content.AttachControlToDocument();

        }

    }
}