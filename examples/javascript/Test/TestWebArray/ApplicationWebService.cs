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

namespace TestWebArray
{
    public sealed class xLatLng
    {
        public double lat;
        public double lng;
    }


    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public async Task<int> GetFoo()
        {
            return 1;
        }

        // jsc cannot handle returning arrays? why?
        public async Task GetFoo2(Action<xLatLng> y)
        {
            y(new xLatLng { });

            //return new[] { new xLatLng { } };
        }

        //public async Task<xLatLng[]> GetFoo2()
        //{
        //    return new[] { new xLatLng { } };
        //}

        //public async Task<int[]> GetFoo3()
        //{
        //    return new[] { 5,5};
        //}

        public async Task<string> GetFoo4()
        {
            return "";
        }
    }
}
