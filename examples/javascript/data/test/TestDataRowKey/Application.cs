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
using TestDataRowKey;
using TestDataRowKey.Design;
using TestDataRowKey.HTML.Pages;

namespace TestDataRowKey
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

            new IHTMLButton { "TakeLastOne yield" }.AttachToDocument().onclick += delegate
           {
               new IHTMLHorizontalRule { }.AttachToDocument();
               base.TakeLastOne(
                   row =>
                   {
                       new IHTMLPre { new { row.Key, row.z } }.AttachToDocument();
                   }
               );

           };
        }

    }
}
