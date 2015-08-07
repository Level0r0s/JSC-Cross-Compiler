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
using TestOptionSelect;
using TestOptionSelect.Design;
using TestOptionSelect.HTML.Pages;

namespace TestOptionSelect
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

            var combo = new IHTMLSelect().AttachToDocument();


            #region can we have a next button?
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150807/shadertoy
            new IHTMLButton { "next" }.AttachToDocument().With(
                next =>
                {
                    new IStyle(next)
                    {
                        position = IStyle.PositionEnum.absolute,
                        right = "1px",
                        top = "1em",
                        bottom = "1em"
                    };

                    next.onclick += delegate
                    {
                        var n = (combo.selectedIndex + 1) % combo.childNodes.Length;
                        Console.WriteLine(new { n });
                        combo.selectedIndex = n;
                    };
                }
            );
            #endregion

            new[] { "red", "green", "blue" }.WithEach(
                async x =>
                {

                    var option = new IHTMLOption { value = x, innerText = new { x }.ToString() }.AttachTo(combo);

                    do
                    {
                        await option.async.onselect;

                        Native.css.style.borderLeft = $"1em {x} solid";

                        await option.async.ondeselect;


                        Native.css.style.borderRight = $"1em {x} solid";
                    }
                    while (await Native.window.async.onframe);

                }
            );
        }

    }
}
