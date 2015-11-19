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
using TestMouseCaptureWhileMove;
using TestMouseCaptureWhileMove.Design;
using TestMouseCaptureWhileMove.HTML.Pages;

namespace TestMouseCaptureWhileMove
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
            var dragme = new IHTMLPre { "drag me" }.AttachToDocument().With(
                async e =>
                {
                    e.css.hover.style.backgroundColor = "yellow";
                    e.css.active.style.backgroundColor = "cyan";

                    do
                    {
                        var mousedown = await e.async.onmousedown;

                        //var release = e.CaptureMouse();
                        e.style.borderLeft = "1em solid red";

                        // https://visualstudio.uservoice.com/forums/121579-visual-studio-2015/suggestions/10773216-while-var-u
                        #region  while (var u = await e.async.onmousemove)
                        var u = default(IEvent);
                        while (u = await e.async.oncapturedmousemove)
                        #endregion
                        {

                            e.innerText = new { u.OffsetX, u.OffsetY }.ToString();
                        }

                        // done

                        //release();
                        e.style.borderLeft = "0em solid yellow";

                    }
                    while (true);
                }
            );


            new CanvasRenderingContext2D(200, 200).With(
               async c =>
               {
                   var e = c.canvas;

                   e.AttachToDocument();

                   //e.style.backgroundColor = "blue";
                   //e.css.hover.style.backgroundColor = "yellow";
                   //e.css.active.style.backgroundColor = "cyan";

                   e.style.borderRight = "blue 1em solid";
                   e.css.hover.style.borderRight = "yellow 1em solid";
                   e.css.active.style.borderRight = "cyan 1em solid";

                   do
                   {
                       var mousedown = await e.async.onmousedown;

                       //var release = e.CaptureMouse();
                       e.style.borderLeft = "1em solid red";

                       // https://visualstudio.uservoice.com/forums/121579-visual-studio-2015/suggestions/10773216-while-var-u
                       #region  while (var u = await e.async.onmousemove)
                       var u = default(IEvent);
                       while (u = await e.async.oncapturedmousemove)
                       #endregion
                       {



                           dragme.innerText = new { u.OffsetX, u.OffsetY }.ToString();
                       }

                       // done

                       //release();
                       e.style.borderLeft = "0em solid yellow";

                   }
                   while (true);
               }
           );

        }

    }
}
