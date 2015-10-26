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
using UbuntuSSLWebApplication;
using UbuntuSSLWebApplication.Design;
using UbuntuSSLWebApplication.HTML.Pages;

namespace UbuntuSSLWebApplication
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
            //Native.body.Clear();


            Native.document.body.style.backgroundColor = "cyan";

            new { }.With(
                async delegate
                {
                    var isroot = Native.window.parent == Native.window.self;

                    new IHTMLPre { new { isroot } }.AttachToDocument();

                    if (!isroot)
                    {
                        // we have the id now.

                        new IHTMLButton { "Identity" }.AttachToDocument().onclick += async e =>
                        {
                            var Identity = await base.Identity();

                            e.Element.innerText = new { Identity }.ToString();

                        };

                        return;
                    }

                    Native.document.body.style.backgroundColor = await base.Color();

                    var hostname = Native.document.location.host.TakeUntilIfAny(":");
                    var hostport = Native.document.location.host.SkipUntilOrEmpty(":");

                    await new IHTMLButton { "login " + new{
                        Native.document.location.protocol,
                        Native.document.location.host,

                        Native.document.baseURI
                    } }.AttachToDocument().async.onclick;

                    // port + 1 iframe?
                    Native.document.body.style.backgroundColor = "cyan";

                    var hostport1 = Convert.ToInt32(hostport) + 1;
                    var host1 = hostname + ":" + hostport1;

                    new IHTMLPre { new { host1 } }.AttachToDocument();

                    var baseURI1 = "https://" + host1;

                    var iframeloaded = await new IHTMLIFrame { src = baseURI1 }.AttachToDocument().async.onload;

                    new IHTMLPre { "ready! " + new { Native.document.baseURI } }.AttachToDocument();

                    // change base?
                    // would be cool if we were to be able to do that huh.
                    // isntead lets hop into the iframe?

                    //await new IHTMLButton { "upgrade" }.AttachToDocument().async.onclick;


                    //new IHTMLBase { href = baseURI1 }.AttachToHead();

                    ////Native.document.baseURI = baseURI1;

                    //new IHTMLPre { "ready! " + new { Native.document.baseURI } }.AttachToDocument();


                    //new IHTMLButton { "Identity" }.AttachToDocument().onclick += async e =>
                    //{
                    //    var Identity = await base.Identity();

                    //    e.Element.innerText = new { Identity }.ToString();

                    //};
                }
            );
        }

    }
}
