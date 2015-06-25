using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/css/CSSFontFaceRule.idl

    [Script(InternalConstructor = true)]
    public partial class CSSFontFaceRule : CSSRule
    {
        // Shared native codebases still exist, and are immensely scary in the
        //context of software security.
        //• especially those processing complex file formats written 20-30 years ago.
        // http://j00ru.vexillium.org/dump/recon2015.pdf

        public CSSStyleDeclaration style;
    }


}
