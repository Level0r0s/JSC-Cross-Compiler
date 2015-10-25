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
using TestDataRowKey.Data;

namespace TestDataRowKey
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151025/testdatarowkey

        public void TakeLastOne(Action<PerformanceResourceTimingData2ApplicationPerformanceRow> yield)
        {
            Console.WriteLine("enter TakeOne descending");
            var value = new PerformanceResourceTimingData2ApplicationPerformanceRow { Key = (PerformanceResourceTimingData2ApplicationPerformanceKey)13, z = new XElement("hello", "world") };
            Console.WriteLine("exit TakeOne descending " + new { value.Key });
            yield(value);
        }

        public async Task<PerformanceResourceTimingData2ApplicationPerformanceRow> TakeLastOne()
        {
            Console.WriteLine("enter TakeOne descending");
            var value = new PerformanceResourceTimingData2ApplicationPerformanceRow { Key = (PerformanceResourceTimingData2ApplicationPerformanceKey)13, z = new XElement("hello", "world") };
            Console.WriteLine("exit TakeOne descending " + new { value.Key });
            return value;
        }

        public async Task<PerformanceResourceTimingData2ApplicationPerformanceRow[]> ReadAll()
        {
            Console.WriteLine("enter ReadAll");

            var value = new[] {
                new PerformanceResourceTimingData2ApplicationPerformanceRow {
                    Key = (PerformanceResourceTimingData2ApplicationPerformanceKey)13, z = new XElement("hello", "world")
                }
               };

            return value;
        }
    }
}
