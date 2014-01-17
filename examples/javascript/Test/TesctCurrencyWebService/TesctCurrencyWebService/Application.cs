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
using TesctCurrencyWebService;
using TesctCurrencyWebService.Design;
using TesctCurrencyWebService.HTML.Pages;

namespace TesctCurrencyWebService
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
            Action getCurrency = async delegate
            {
                var c = await this.GetConversionRate();
                if (c.ContainsKey("GBP"))
                {
                    Console.WriteLine(c["GBP"].ToString());
                }
                Console.WriteLine(c.ToString());
                var p = new IHTMLPre { innerText = c["GBP"].ToString()};
                p.AttachTo(page.body);
            };
            getCurrency();
        }

    }
}