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
using WebServiceAuthorityExperiment;
using WebServiceAuthorityExperiment.Design;
using WebServiceAuthorityExperiment.HTML.Pages;

namespace WebServiceAuthorityExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // Z:\jsc.svn\examples\javascript\crypto\WebServiceAuthorityExperiment\

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new { }.With(
                async delegate
                {
                    var x = await base.GetData();


                    var o = x.value;
                    var t = new IHTMLTextArea { value = x, readOnly = true }.AttachToDocument();

                    t.style.whiteSpace = IStyle.WhiteSpaceEnum.nowrap;
                    t.style.width = "80em";
                    //t.style.right = "1em";
                    t.style.height = "20em";

                    var status = new IHTMLPre { new { verify = await base.Verify(x) } }.AttachToDocument();

                    t.style.paddingLeft = "1em";
                    t.style.borderLeft = "1em solid green";
                    t.readOnly = false;

                    //while (await t.async.onchange)

                    while (await t.async.onkeyup)
                    {
                        t.style.borderLeft = "1em solid yellow";

                        // Z:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\TextBox.cs
                        x.value = t.value.Replace(Environment.NewLine, "\n").Replace("\n", Environment.NewLine);

                        var verify = await base.Verify(x);
                        status.innerText = new { isoriginal = o == x.value, o = o.Length, t = x.value.Length, verify }.ToString();

                        if (verify)
                            t.style.borderLeft = "1em solid green";
                        else
                            t.style.borderLeft = "1em solid red";
                    }

                }
            );
        }

    }
}
