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
                    new IHTMLPre { "hello!" }.AttachToDocument();

                    new IHTMLPre { new { base.foo, verify = await base.Verify(base.foo) } }.AttachToDocument();
                    // { foo = { value = foo string, signature = 4cb21a833052c61bf39e1381ef1acb41b0dadfd7cd65d21391fe3f06fef841b4555be1661aa94491a549023fdf5758050c086cf49a3dde7fcc8816a551a6fa40 }, verify = true }
                    base.foo.value = base.foo.value.ToUpper();

                    new IHTMLPre { new { base.foo, verify = await base.Verify(base.foo) } }.AttachToDocument();



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

//{ ElapsedMilliseconds = 0, SpecialData = hello world, sig = ccc4b11f1bdc0626b90696ebc86df6a839c4d5a0478bacbca292c97d9561c51912e1f7cb2e5161eaef47147eeee5f8a6, Verify = false }
//{ ElapsedMilliseconds = 325, SpecialData = HELLO WORLD, sig = ccc4b11f1bdc0626b90696ebc86df6a839c4d5a0478bacbca292c97d9561c51912e1f7cb2e5161eaef47147eeee5f8a6, Verify = false }
//{ ElapsedMilliseconds = 730, SpecialData = hello world, sig = ccc4b11f1bdc0626b90696ebc86df6a839c4d5a0478bacbca292c97d9561c51912e1f7cb2e5161eaef47147eeee5f8a6, Verify = false }
//{ ElapsedMilliseconds = 989, SpecialData = hello world, sig = ccc4b11f1bdc0626b90696ebc86df6a839c4d5a0478bacbca292c97d9561c51912e1f7cb2e5161eaef47147eeee5f8a6, Verify = false }



//{ ElapsedMilliseconds = 0, SpecialData = hello world, sig = 41c4b11f1bdc0626b90696ebc86df6a839c4d5a0478bacbca292c97d9561c51912e1f7cb2e5161eaef47147eeee5f8a6, Verify = true }
//{ ElapsedMilliseconds = 777, SpecialData = HELLO WORLD, sig = 41c4b11f1bdc0626b90696ebc86df6a839c4d5a0478bacbca292c97d9561c51912e1f7cb2e5161eaef47147eeee5f8a6, Verify = false }
//{ ElapsedMilliseconds = 1789, SpecialData = hello world, sig = 41c4b11f1bdc0626b90696ebc86df6a839c4d5a0478bacbca292c97d9561c51912e1f7cb2e5161eaef47147eeee5f8a6, Verify = true }
//{ ElapsedMilliseconds = 2546, SpecialData = hello world, sig = ccc4b11f1bdc0626b90696ebc86df6a839c4d5a0478bacbca292c97d9561c51912e1f7cb2e5161eaef47147eeee5f8a6, Verify = false }

//{ ElapsedMilliseconds = 0, SpecialData = hello world, sig = 66055627ca773be208c0797c484b209e7997b06559727d065b4288c924c80156083811af2692b37cfc666131034e7e158c6df31a56319d500141abe585789f81, Verify = true }
//{ ElapsedMilliseconds = 88, SpecialData = HELLO WORLD, sig = 66055627ca773be208c0797c484b209e7997b06559727d065b4288c924c80156083811af2692b37cfc666131034e7e158c6df31a56319d500141abe585789f81, Verify = false }
//{ ElapsedMilliseconds = 347, SpecialData = hello world, sig = 66055627ca773be208c0797c484b209e7997b06559727d065b4288c924c80156083811af2692b37cfc666131034e7e158c6df31a56319d500141abe585789f81, Verify = true }
//{ ElapsedMilliseconds = 608, SpecialData = hello world, sig = cc055627ca773be208c0797c484b209e7997b06559727d065b4288c924c80156083811af2692b37cfc666131034e7e158c6df31a56319d500141abe585789f81, Verify = false }