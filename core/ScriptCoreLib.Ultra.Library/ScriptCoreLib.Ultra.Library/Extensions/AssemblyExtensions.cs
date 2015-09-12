using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<AssemblyCopyrightAttribute> GetAssemblyCopyrightAttributes(this Assembly a)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150911/confuserex

            try
            {
                return a.GetCustomAttributes(
                                        attributeType: typeof(AssemblyCopyrightAttribute),
                                        inherit: false
                                    ).Cast<AssemblyCopyrightAttribute>();

            }
            catch
            {
                //yield break;
                return new AssemblyCopyrightAttribute[0];

            }

        }
    }
}
