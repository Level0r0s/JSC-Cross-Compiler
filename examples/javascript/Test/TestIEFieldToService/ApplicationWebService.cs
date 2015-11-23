using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestIEFieldToService
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        //public string foo = "IE sends this";

        // so at what point do we loos the value in IE?
        public VerifiableString bugcheck = new VerifiableString { value = "hello" };

        public async Task Invoke()
        {
            // IE misbehves on redux build?

            Debugger.Break();

        }
    }
}
