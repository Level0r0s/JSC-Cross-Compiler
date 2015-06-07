using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;

[assembly: Script()]
[assembly: ScriptTypeFilter(ScriptType.C, typeof(OVRVrCubeWorldNative.xNativeActivity))]

namespace OVRVrCubeWorldNative
{
    // to be used by nuget user
    //partial class xActivity
    //{

    //}


    // ? what does this do?
    [Obfuscation(StripAfterObfuscation = true)]

    // x:\jsc.svn\examples\java\android\androidndknugetexperiment\androidndknugetexperiment\applicationactivity.cs
    [Script]
    partial class xNativeActivity
    {
        // wtf
        // make.exe: *** No rule to make target `jni/OVRVrCubeWorldNative.exe.c', needed by `obj/local/armeabi-v7a/objs/OVRVrCubeWorldNative/OVRVrCubeWorldNative.exe.o'.  Stop.
        // "X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldNativeActivity\OVRVrCubeWorldNative\bin\Debug\staging\jni\OVRVrCubeWorldNative.dll.c"
        // "X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldNativeActivity\OVRVrCubeWorldNative\bin\Debug\staging\libs\armeabi-v7a\libOVRVrCubeWorldNative.so"

        // Java_TestNDKAsAsset_xActivity_stringFromJNI

        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static jstring Java_OVRVrCubeWorldNative_segments_xActivity_stringFromJNI(
            // what would we be able to do inspecting the runtime?
            ref JNIEnv env,
            jobject thiz)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150607-1/vrcubeworld

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150525

            var n = env.NewStringUTF;

            // look almost the same file!

            // OVR_VRAPI_EXPORT const char * vrapi_GetVersionString();

            // if we change our NDK code, will nuget packaing work on the background, and also upgrade running apps?
            var v = n(ref env, 
                //"from Java_OVRVrCubeWorldNative_xActivity_stringFromJNI. yay"

                VrApi_h.vrapi_GetVersionString()
                );

            return v;


            // ConfigurationCreateNuGetPackage.cs
        }

    }

    [Script(IsNative = true, Header = "VrApi.h")]
    public class VrApi_h
    {
        public static string vrapi_GetVersionString() { return null; }
    }
}

//jni/VrCubeWorld_NativeActivity.c:38:19: fatal error: VrApi.h: No such file or directory
// #include "VrApi.h"
//                   ^