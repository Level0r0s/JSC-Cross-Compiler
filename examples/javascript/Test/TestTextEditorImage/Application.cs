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
using TestTextEditorImage;
using TestTextEditorImage.Design;
using TestTextEditorImage.HTML.Pages;

namespace TestTextEditorImage
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
            var t = new ScriptCoreLib.JavaScript.Controls.TextEditor(
                page.body
            );


            new IHTMLButton { "InnerHTML" }.AttachToDocument().onclick += delegate
            {

                new IHTMLPre { t.InnerHTML }.AttachToDocument();

                // <p>x</p><p><br /></p><p><img src="https://192.168.1.12:21279/assets/TestTextEditorImage/createlink.png">&nbsp;<br /></p><p>x</p>
                // x<div><img src="https://192.168.1.12:30903/assets/TestTextEditorImage/createlink.png"></div><div>x</div>


            };

            new IHTMLButton { "xml" }.AttachToDocument().onclick += delegate
            {

                var xml = t.Document.body.AsXElement();

                // <body xmlns="http://www.w3.org/1999/xhtml" style="height: auto; border: 0; overflow: auto; background-color:transparent;">x<div><br /></div><div><img src="https://192.168.1.12:21279/assets/TestTextEditorImage/createlink.png" /><br /></div><div>x</div></body>
                //new IHTMLPre { xml }.AttachToDocument();

                new IHTMLPre { innerText = "" + xml }.AttachToDocument();

                // x<div><img src="https://192.168.1.12:30903/assets/TestTextEditorImage/createlink.png"></div><div>x</div>
            };

            new IHTMLButton { "p" }.AttachToDocument().onclick += delegate
            {

                var xml = t.Document.body.AsXElement();

                // <body xmlns="http://www.w3.org/1999/xhtml" style="height: auto; border: 0; overflow: auto; background-color:transparent;">x<div><br /></div><div><img src="https://192.168.1.12:21279/assets/TestTextEditorImage/createlink.png" /><br /></div><div>x</div></body>
                //new IHTMLPre { xml }.AttachToDocument();

                var p = new XElement("p", xml.Nodes());

                // <p>xxx<DIV><BR/></DIV><DIV><IMG src="https://192.168.1.12:24526/assets/TestTextEditorImage/createlink.png"/><BR/></DIV><DIV>x</DIV></p>

                //view-source:54860 41388ms XContainer Add xIEnumerable
                //2016-01-04 20:48:00.746 view-source:54860 41388ms XContainer Add xIEnumerable Add { item = xxx }
                //2016-01-04 20:48:00.748 view-source:54860 41388ms XContainer Add xIEnumerable Add { item = <div xmlns="http://www.w3.org/1999/xhtml"><br /></div> }
                //2016-01-04 20:48:00.750 view-source:54860 41398ms XContainer Add xIEnumerable Add { item = <div xmlns="http://www.w3.org/1999/xhtml">xxxx</div> }

                new IHTMLPre { innerText = "" + p }.AttachToDocument();

                // x<div><img src="https://192.168.1.12:30903/assets/TestTextEditorImage/createlink.png"></div><div>x</div>
            };
        }

    }
}
