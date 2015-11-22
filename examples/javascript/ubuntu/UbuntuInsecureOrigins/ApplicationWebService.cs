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

namespace UbuntuInsecureOrigins
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService : IDisposable
    {
        //<h1 id="time">?</h1>

        public XElement time = new XElement("h1", "??");

        public async Task Update()
        {
            time.Value = new { DateTime.Now }.ToString();

        }




        // wont be called if appwindow is closed, will be called if another page is loaded.
        public void Dispose()
        {
            // will chrome app let us know they closed?

            Console.WriteLine("Dispose");
        }
    }
}
