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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestWebServiceRSA;
using TestWebServiceRSA.Design;
using TestWebServiceRSA.HTML.Pages;

namespace TestWebServiceRSA
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
                async delegate
                {
                    var sw = Stopwatch.StartNew();

                    new IHTMLPre { new { sw.ElapsedMilliseconds, base.SpecialData, sig = SpecialDataSignature48.ToHexString(), Verify = await base.Verify() } }.AttachToDocument();

                    this.SpecialData = this.SpecialData.ToUpper();

                    new IHTMLPre { new { sw.ElapsedMilliseconds, base.SpecialData, sig = SpecialDataSignature48.ToHexString(), Verify = await base.Verify() } }.AttachToDocument();

                    // try to revert
                    this.SpecialData = this.SpecialData.ToLower();

                    new IHTMLPre { new { sw.ElapsedMilliseconds, base.SpecialData, sig = SpecialDataSignature48.ToHexString(), Verify = await base.Verify() } }.AttachToDocument();

                    // modify sig
                    this.SpecialDataSignature48[0] = 0xcc;

                    new IHTMLPre { new { sw.ElapsedMilliseconds, base.SpecialData, sig = SpecialDataSignature48.ToHexString(), Verify = await base.Verify() } }.AttachToDocument();

                }
            );
        }

    }
}

//{ ElapsedMilliseconds = 0, SpecialData = hello world, sig = 41c4b11f1bdc0626b90696ebc86df6a839c4d5a0478bacbca292c97d9561c51912e1f7cb2e5161eaef47147eeee5f8a6, Verify = true }
//{ ElapsedMilliseconds = 777, SpecialData = HELLO WORLD, sig = 41c4b11f1bdc0626b90696ebc86df6a839c4d5a0478bacbca292c97d9561c51912e1f7cb2e5161eaef47147eeee5f8a6, Verify = false }
//{ ElapsedMilliseconds = 1789, SpecialData = hello world, sig = 41c4b11f1bdc0626b90696ebc86df6a839c4d5a0478bacbca292c97d9561c51912e1f7cb2e5161eaef47147eeee5f8a6, Verify = true }
//{ ElapsedMilliseconds = 2546, SpecialData = hello world, sig = ccc4b11f1bdc0626b90696ebc86df6a839c4d5a0478bacbca292c97d9561c51912e1f7cb2e5161eaef47147eeee5f8a6, Verify = false }