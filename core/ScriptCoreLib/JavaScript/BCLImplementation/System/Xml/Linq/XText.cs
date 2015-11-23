using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{
    // https://github.com/dotnet/corefx/blob/master/src/System.Xml.XDocument/System/Xml/Linq/XText.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System.Xml.Linq/System.Xml.Linq/XText.cs

    [Script(Implements = typeof(XText))]
    internal class __XText : __XNode
    {
        public string InternalStringValue;

        public string Value
        {
            get
            {
                if (InternalStringValue != null)
                {
                    Console.WriteLine("__XText get " + new { InternalStringValue });
                    return InternalStringValue;
                }

                var value = ((ITextNode)this.InternalValue).text;

                Console.WriteLine("__XText get " + new { value });

                return value;
            }
            set
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151123/uploadvaluestaskasync
                Console.WriteLine("__XText set " + new { value });

                if (InternalStringValue != null)
                    InternalStringValue = value;


                var o = this.InternalValue;
                var n = this.InternalValue.ownerDocument.createTextNode(value);

                var parentNode = this.InternalValue.parentNode;

                parentNode.replaceChild(
                    n,
                    o
                );
            }
        }

    }
}
