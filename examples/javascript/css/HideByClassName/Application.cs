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
using HideByClassName;
using HideByClassName.Design;
using HideByClassName.HTML.Pages;

namespace HideByClassName
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

            var c = "mymenu";


            var css = Native.css[" ." + c];

            new IHTMLPre { new { css.rule } }.AttachToDocument();


            css.style.color = "blue";

            new IHTMLButton { className = c, innerText = "menu1" }.AttachToDocument().With(
                async e =>
                 {
                     while (await e.async.onclick)
                     {
                         css.style.display = IStyle.DisplayEnum.none;

                         var back = new IHTMLButton { "go back from " + new { e.innerText } }.AttachToDocument();
                         await back.async.onclick;
                         back.Orphanize();
                         css.style.display = IStyle.DisplayEnum.empty;
                     }
                 }
            );

            new IHTMLButton { className = c, innerText = "menu2" }.AttachToDocument().With(
               async e =>
               {
                   while (await e.async.onclick)
                   {
                       css.style.display = IStyle.DisplayEnum.none;

                       var back = new IHTMLButton { "go back from " + new { e.innerText } }.AttachToDocument();
                       await back.async.onclick;
                       back.Orphanize();
                       css.style.display = IStyle.DisplayEnum.empty;
                   }
               }
           );
        }

    }
}
