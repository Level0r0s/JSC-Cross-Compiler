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
using TestTaskConditionalButton;
using TestTaskConditionalButton.Design;
using TestTaskConditionalButton.HTML.Pages;

namespace TestTaskConditionalButton
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
                delegate
                {
                    Native.css[IHTMLElement.HTMLElementEnum.button].style.border = "1px solid blue";

                    var delay1 = new TaskCompletionSource<object> { };
                    //var delay = Task.Delay(3000);

                    new IHTMLButton { "delay" }.AttachToDocument().onclick += delegate { delay1.SetResult(null); };

                    var delay = delay1.Task;

                    // html:not([await1="incomplete"])
                    Native.css[delay].style.backgroundColor = "yellow";

                    //Native.document.body[delay].style.
                    // body > button:not([await2="incomplete"]) 
                    //Native.document.body.css[IHTMLElement.HTMLElementEnum.button][delay].style.backgroundColor = "cyan";
                    //Native.document.body.css[delay][IHTMLElement.HTMLElementEnum.button].style.backgroundColor = "cyan";

                    (!Native.document.body.css[delay1]).button.style.backgroundColor = "red";

                    Native.document.body.css[delay1][IHTMLElement.HTMLElementEnum.button].style.backgroundColor = "cyan";

                    // there seems to be bug if delay comes after button.


                    new IHTMLButton { "go" }.AttachToDocument().onclick += delegate
                    {

                        Native.document.location.reload();
                    };
                }
            );

        }

    }
}
