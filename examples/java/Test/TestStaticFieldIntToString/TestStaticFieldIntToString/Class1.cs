using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestStaticFieldIntToString
{
    [Script(
    Implements = typeof(global::System.String)
    )]
    internal class __String
    {
    }

    [Script(Implements = typeof(global::System.Int32)
        // native type cast conflict: ,ExternalTarget="java.lang.Integer"
    //, ImplementationType = typeof(java.lang.Integer)
    )]
    internal class __Int32
    {
        [Script(DefineAsStatic = true)]
        public string ToString(string format)
        {


            return "";
        }
    }

    public class R
    {
        public class id
        {
            public static int progress;
        }
    }

    public class Class1
    {
        public static void Invoke()
        {
            // 1>script : error JSC1000: Java : Opcode not implemented: ldsflda at TestStaticFieldIntToString.Class1.Invoke

            // this = byref int?
            var x = R.id.progress.ToString("x8");
        }
    }
}
