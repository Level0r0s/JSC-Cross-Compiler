using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/single.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Single.cs

    // X:\opensource\github\WootzJs\WootzJs.Runtime\Single.cs
    // https://github.com/bridgedotnet/Bridge/blob/master/Bridge/System/Single.cs

    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Single.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Single.cs


	[Script(Implements = typeof(global::System.Single),
		ImplementationType = typeof(java.lang.Float))]
    internal class __Single
	{
        // what about multidimensional arrays?
        // X:\jsc.svn\examples\javascript\Test\TestFloatArray\TestFloatArray\Application.cs

		[Script(ExternalTarget = "parseFloat")]
		public static float Parse(string e)
		{
            return default(float);
		}
	}
}
