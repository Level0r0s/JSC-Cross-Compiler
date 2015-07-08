using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/single.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Single.cs

    // X:\opensource\github\WootzJs\WootzJs.Runtime\Single.cs
    // https://github.com/bridgedotnet/Bridge/blob/master/Bridge/System/Single.cs

    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Single.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Single.cs

    [Script(Implements = typeof(global::System.Single))]
    internal class __Single
    {
        // float4 x16 = mat4

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/20150706/20150708

        [Script(OptimizedCode = "return parseFloat(e);")]
        static public __Single Parse(string e)
        {
            return default(__Single);
        }


        [Script(DefineAsStatic = true)]
        public int CompareTo(__Single e)
        {
            return Expando.Compare(this, e);
        }
    }
}

