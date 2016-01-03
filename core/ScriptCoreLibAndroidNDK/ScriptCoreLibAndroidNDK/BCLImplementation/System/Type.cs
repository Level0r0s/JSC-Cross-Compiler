using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLibAndroidNDK.BCLImplementation.System
{
    // z:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\BCLImplementation\System\Type.cs
    // z:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Type.cs
    // z:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Type.cs
    // z:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Type.cs


    [Script(Implements = typeof(global::System.Type))]
    public class __Type //: __MemberInfo, __IReflect
    {

        // Z:\jsc.svn\examples\java\android\synergy\OVROculus360PhotosNDK\OVROculus360PhotosNDK\xNativeActivity.cs

        // jsc should automatically make typeof(this) available
        // my rendering them to arguments:

        public JNIEnv arg0_env;
        public jclass arg1_type;

        public jmethodID GetMethodID(string name, string sig)
        {
            return arg0_env.GetMethodID(arg0_env, arg1_type, name, sig); ;
        }
    }
}
