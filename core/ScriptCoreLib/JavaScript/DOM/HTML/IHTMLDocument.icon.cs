using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using System;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    public partial class IHTMLDocument
    {
        // any reason to use the icon for the default notification too?
        [Obsolete("experimental")]
        public IHTMLImage icon
        {
            // Z:\jsc.svn\examples\javascript\Test\TestSSLConnectionLimit\Application.cs
            // https://code.google.com/p/chromium/issues/detail?can=2&start=0&num=100&q=&colspec=ID%20Pri%20M%20Stars%20ReleaseBlock%20Cr%20Status%20Owner%20Summary%20OS%20Modified&groupby=&sort=&id=543982

            [Script(DefineAsStatic = true)]
            set
            {
                {
                }
                var link = new IHTMLLink
                {
                    rel = "icon",
                    type = "image/png",
                };

                if (value != null)
                    link.href = value.src;
                else
                    link.href = "";

                // X:\jsc.svn\examples\javascript\Test\TestDocumentIcon\TestDocumentIcon\Application.cs

                //Native.document.documentElement.insertBefore(
                //    link,
                //    Native.document.head
                //);


                link.AttachTo(
                    Native.document.head
                );

            }
        }
    }
}
