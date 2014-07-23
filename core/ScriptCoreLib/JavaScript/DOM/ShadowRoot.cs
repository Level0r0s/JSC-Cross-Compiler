﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://opensource.apple.com/source/WebCore/WebCore-1640/dom/ShadowRoot.idl
    // https://github.com/adobe/webkit/blob/master/Source/WebCore/dom/ShadowRoot.idl
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/ShadowRoot.webidl

    [Script(InternalConstructor = true)]
    public class ShadowRoot : IDocumentFragment
    {
        // how does this relate to the SVG foreign object concept?
        //  shadow DOM is useful because it creates encapsulation. All that means is anything in the Shadow DOM is unaffected by styling in regular stylesheets and javascript. This is one of the reasons that UI elements are found in the shadow DOM and not directly editable.



        // X:\jsc.svn\examples\javascript\Test\TestShadowDOM\TestShadowDOM\Application.cs
        //IElement getElementById(string elementId);

        // tested by ?
        public readonly string innerHTML;

        public bool applyAuthorStyles;


        // https://news.ycombinator.com/item?id=7243122

    }
}