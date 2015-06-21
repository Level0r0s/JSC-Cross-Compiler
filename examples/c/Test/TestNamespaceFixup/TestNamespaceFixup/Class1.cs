using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Script(IsScriptLibrary = true)]
[assembly: ScriptTypeFilter(ScriptType.C)]


// .so export needs to prefox java methods by Java.
[assembly: ScriptNamespaceRename(NativeNamespaceName = "com.oculus.gles3jni", VirtualNamespaceName = "Java.com.oculus.gles3jni")]


namespace com.oculus.gles3jni
{
    static class GLES3JNILib
    {
        // void* Java_com_oculus_gles3jni_GLES3JNILib_stringFromJNI(void* env, void* thiz)

        public static object stringFromJNI(object env, object thiz)
        {
            return null;
        }
    }
}
