using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.WebService
{
    public class InternalQueryStringParser : NameValueCollection
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151022/httprequest

        // code duplication :)
        public readonly string QueryString;

        public InternalQueryStringParser(string QueryString)
        {
            if (null == QueryString)
            {
                this.QueryString = "";

                return;
            }

            this.QueryString = QueryString;

            //Console.WriteLine("InternalQueryStringParser: QueryString=" + QueryString);

            foreach (var item in QueryString.Split('&'))
            {
                var p = item.Split('=');

                if (p.Length == 2)
                {
                    var value = p[0];
                    var name = p[1];

                    this[value] = name;

                    //Console.WriteLine("InternalQueryStringParser: " + value + " = " + name);
                }

            }
        }
    }
}
