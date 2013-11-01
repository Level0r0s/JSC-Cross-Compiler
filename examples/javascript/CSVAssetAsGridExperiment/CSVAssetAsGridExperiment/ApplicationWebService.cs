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

namespace CSVAssetAsGridExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public async Task<DataTable> GetFoo()
        {
            return CSVAssetAsGridExperiment.Design.foo.GetDataTable();
        }
        public async Task<DataTable> GetFoo2()
        {
            return CSVAssetAsGridExperiment.Design.foo2.GetDataTable();
        }
    }
}