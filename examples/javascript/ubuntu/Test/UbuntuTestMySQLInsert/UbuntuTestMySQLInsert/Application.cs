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
using UbuntuTestMySQLInsert;
using UbuntuTestMySQLInsert.Design;
using UbuntuTestMySQLInsert.HTML.Pages;

namespace UbuntuTestMySQLInsert
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
            new IHTMLPre { new { Native.document.location.protocol, Native.document.location.host } }.AttachToDocument();
            // NFC ?
            new IHTMLPre { new { Native.window.navigator.userAgent } }.AttachToDocument();

            new { }.With(
                async delegate
                {
                    new IHTMLButton { "ReadAll" }.AttachToDocument().onclick += async delegate
                    {
                        new IHTMLHorizontalRule { }.AttachToDocument();
                        foreach (var row in await base.ReadAll())
                        {
                            new IHTMLPre { new { row.Key, row.z } }.AttachToDocument();
                        }

                    };

                    new IHTMLButton { "TakeLastOne" }.AttachToDocument().onclick += async delegate
                    {
                        new IHTMLHorizontalRule { }.AttachToDocument();
                        var row = await base.TakeLastOne();
                        new IHTMLPre { new { row.Key, row.z } }.AttachToDocument();

                    };

                    new IHTMLButton { "TakeOne" }.AttachToDocument().onclick += async delegate
                    {
                        new IHTMLHorizontalRule { }.AttachToDocument();
                        var row = await base.TakeOne();
                        new IHTMLPre { new { row.Key, row.z } }.AttachToDocument();

                    };

                    var AddAndCount = new IHTMLButton { "AddAndCount" }.AttachToDocument();

                    var content = new { Native.document.location.protocol, Native.document.location.host, Native.window.navigator.userAgent }.ToString();

                    // show it
                    new IHTMLPre { () => new { content } }.AttachToDocument();


                    while (await AddAndCount.async.onclick)
                    {

                        var count = await base.AddAndCount(
                            new XElement("hello", content)
                        );


                        // keep our data
                        content = "the one after " + new { count, Native.document.location.protocol, Native.document.location.host, Native.window.navigator.userAgent };

                        new IHTMLPre { new { count } }.AttachToDocument();
                    }

                }
            );
        }

    }
}
