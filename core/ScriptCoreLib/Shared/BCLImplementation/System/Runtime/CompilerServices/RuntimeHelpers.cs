using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices
{
	// http://referencesource.microsoft.com/#mscorlib/system/runtime/compilerservices/runtimehelpers.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Runtime/CompilerServices/RuntimeHelpers.cs
	// https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/CompilerServices/RuntimeHelpers.cs
    // https://github.com/dot42/api/blob/master/System/Runtime/CompilerServices/RuntimeHelpers.cs

	[Script(Implements = typeof(global::System.Runtime.CompilerServices.RuntimeHelpers))]
	public class __RuntimeHelpers
	{
        // Z:\jsc.svn\examples\javascript\vb\LEST97\LEST97\Library\lest_function_vba.vb

		// tested by?
        public static object GetObjectValue(object obj)
        {
            return obj;
        }
	}
}
