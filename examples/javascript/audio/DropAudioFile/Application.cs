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
using DropAudioFile;
using DropAudioFile.Design;
using DropAudioFile.HTML.Pages;

namespace DropAudioFile
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
                    new IHTMLPre { "drop mp3" }.AttachToDocument();

                    var f = await Native.document.documentElement.async.ondropfile;

                    Native.document.body.style.backgroundColor = "cyan";

                    new IHTMLPre { new { f.name, f.size } }.AttachToDocument();


                    //var u = URL.createObjectURL(f);
                    //new IHTMLPre { new { u } }.AttachToDocument();
                    //new IHTMLAudio { src = u, controls = true }.AttachToDocument();


                    page.audio = f;

                    await page.audio.async.onloadeddata;

                    Native.document.body.style.backgroundColor = "yellow";
                }
            );
        }

    }
}
