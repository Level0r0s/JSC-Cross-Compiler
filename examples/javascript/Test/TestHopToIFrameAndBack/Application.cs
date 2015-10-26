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
using TestHopToIFrameAndBack;
using TestHopToIFrameAndBack.Design;
using TestHopToIFrameAndBack.HTML.Pages;

namespace TestHopToIFrameAndBack
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
                  #region  magic
                  var isroot = Native.window.parent == Native.window.self;

                  new IHTMLPre { new { isroot } }.AttachToDocument();

                  if (!isroot)
                  {
                      // we have the id now.

                      Native.window.onmessage += e =>
                    {
                        // lets test messages to see if we could do a hop

                        new IHTMLPre { e.data }.AttachToDocument();

                    };
                  }
                  #endregion


                  new IHTMLButton { "Identity" }.AttachToDocument().onclick += async e =>
                  {
                      new IHTMLPre { "will jump into the iframe to do io and then jump back here" }.AttachToDocument();


                  };

              }
          );

        }

    }
}
