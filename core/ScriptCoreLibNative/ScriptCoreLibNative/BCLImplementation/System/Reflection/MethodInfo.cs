using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;

namespace ScriptCoreLibNative.BCLImplementation.System.Reflection
{
    // http://referencesource.microsoft.com/#mscorlib/system/reflection/methodinfo.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Reflection/MethodInfo.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Reflection\MethodInfo.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Reflection\MethodInfo.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Reflection\MethodInfo.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Reflection\MethodInfo.cs

	[Script(Implements = typeof(global::System.Reflection.MethodInfo))]
    public class __MethodInfo
	{
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/ndktype

        public global::System.IntPtr MethodToken;



        public static implicit operator MethodInfo(__MethodInfo e)
        {
            return (MethodInfo)(object)e;
        }

        public static implicit operator __MethodInfo(MethodInfo e)
        {
            return (__MethodInfo)(object)e;
        }
	}
}
