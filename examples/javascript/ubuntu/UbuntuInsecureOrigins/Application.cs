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
using UbuntuInsecureOrigins;
using UbuntuInsecureOrigins.Design;
using UbuntuInsecureOrigins.HTML.Pages;

namespace UbuntuInsecureOrigins
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // java -jar  /home/xmikro/Desktop/staging/UbuntuInsecureOrigins.ApplicationWebService.exe
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151122/ubuntuinsecureorigins

            // https://www.chromium.org/Home/chromium-security/deprecating-powerful-features-on-insecure-origins
            // https://www.chromium.org/Home/chromium-security/marking-http-as-non-secure
            // https://www.chromium.org/Home/chromium-security/prefer-secure-origins-for-powerful-new-features

            new IHTMLPre { new { Native.document.location.protocol, Native.document.location.host } }.AttachToDocument();


            // { protocol = https:, host = 192.168.1.12:13656 }

            new IHTMLHorizontalRule { }.AttachToDocument();
            new IHTMLAnchor { href = "http://" + Native.document.location.host, innerText = "http" }.AttachToDocument();


            new IHTMLHorizontalRule { }.AttachToDocument();
            new IHTMLAnchor { href = "https://" + Native.document.location.host, innerText = "https" }.AttachToDocument();

            var hostname = Native.document.location.host.TakeUntilIfAny(":");
            var hostport = Convert.ToInt32(Native.document.location.host.SkipUntilOrEmpty(":"));

            new IHTMLAnchor { href = "https://" + hostname + ":" + (hostport + 1), innerText = "https client certificate" }.AttachToDocument();

            // https://w3c.github.io/webappsec-secure-contexts/

            dynamic window = Native.window;

            new IHTMLPre { new { window.isSecureContext } }.AttachToDocument();
            // { isSecureContext = true }

            if ((bool)window.isSecureContext)
                Native.document.body.style.backgroundColor = "darkcyan";
            else
                Native.document.body.style.backgroundColor = "yellow";

            new IHTMLButton { "Update" }.AttachToDocument().onclick += async delegate
            {
                await base.Update();
            };

            //var xDisposable = base as IDisposable;
            var xDisposable = this as IDisposable;

            new IHTMLPre { new { xDisposable } }.AttachToDocument();

            var sw = Stopwatch.StartNew();

            new IHTMLPre { () => new { sw.ElapsedMilliseconds } }.AttachToDocument();
            new { }.With(
                async delegate
                {
                    await Task.Delay(33000);

                    Native.document.location.reload();
                }
            );


        }

    }
}
