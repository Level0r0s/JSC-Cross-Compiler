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
using TestSSLConnectionLimit;
using TestSSLConnectionLimit.Design;
using TestSSLConnectionLimit.HTML.Pages;
using CSSAzimuthMapViz.HTML.Images.FromAssets;
using CSSAzimuthMapViz;

namespace TestSSLConnectionLimit
{
    public class side
    {
        public IHTMLImage img;
    }

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
            // certmgr.msc

            Native.document.icon = null;


            //#1a < 001a 0x166c bytes
            //#05 < 0005 0x0576 bytes
            new IHTMLButton { "go wild" }.AttachToDocument()
            //.WhenClicked(

            .With(
            //async
            delegate
                {
                    // X-DevTools-Emulate-Network-Conditions-Client-Id:7C66E13B-14BC-4006-BAC1-9EE2D3AC0591

                    // can we disable the implicit favicon request?
                    //Native.document.icon = new IHTMLImage { };
                    // #1a < 001a 0x0b04 bytes


                    //Native.document.icon.AttachToDocument();

                    // favicon to fail?
                    //await Task.Delay(300);
                    //await Native.window.async.onframe;


                    // there is a secondary / request?
                    //return;

                    //Native.window.requestAnimationFrame += delegate
                    {

                        // https://code.google.com/p/chromium/issues/detail?id=543982&thanks=543982&ts=1444983390

                        Console.WriteLine("about to init sides...");
                        #region sides
                        var sides = new IHTMLImage[]
                        {
                            new azi_px(),

                                new azi_nx(),

                                new azi_py(),

                                new azi_ny(),

                                new azi_pz(),


                            // #18 < 0018 0x0550 bytes

                            new azi_nz(),
                        };
                        #endregion
                        Console.WriteLine("about to init CSS3DObject sides... did chrome just abuse TCP ?");

                    };

                }
                );

        }

    }
}
