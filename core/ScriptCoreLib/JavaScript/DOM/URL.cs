using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/dom/URL.idl

    [Script(HasNoPrototype = true, ExternalTarget = "URL")]
    // static class?
    public class URL
    {
        // http://dev.w3.org/2006/webapi/FileAPI/#url

        // https://developer.mozilla.org/en-US/docs/Web/API/URL/revokeObjectURL

        [Script(ExternalTarget = "URL.createObjectURL")]
        public static string createObjectURL(Blob blob)
        {
            return default(string);
        }

        [Script(ExternalTarget = "URL.revokeObjectURL")]
        public static void revokeObjectURL(string url)
        {
        }
    }
}
