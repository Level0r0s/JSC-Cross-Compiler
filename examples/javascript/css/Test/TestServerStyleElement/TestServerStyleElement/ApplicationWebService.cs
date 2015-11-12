using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestServerStyleElement
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public XElement style1 = new XElement("style", ":root { background-color: cyan; }");

        public async Task Invoke()
        {

            style1.Value = ":root { background-color: green; }";
            // cool. it works.

            // can .css concept be aligned?
        }

    }
}
