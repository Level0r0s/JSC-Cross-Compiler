using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLibAndroidNDK.BCLImplementation.System.Reflection
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
        public __Type InternalDeclaringType;

        public string methodName;
        public string methodSignature;
        public jmethodID methodID;


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

        public void Invoke(jobject that)
        {
            //Z:\jsc.svn\examples\java\android\synergy\OVROculus360PhotosNDK\OVROculus360PhotosNDK\xNativeActivity.cs

            InternalDeclaringType.arg0_env.CallVoidMethodA(
                InternalDeclaringType.arg0_env, that, this.methodID, args: default(jvalue[])
            );
        }
    }
}
