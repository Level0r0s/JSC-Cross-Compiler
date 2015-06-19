using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Script()]
[assembly: ScriptTypeFilter(ScriptType.C, typeof(OVRVrCubeWorldSurfaceViewNDK.xNativeActivity))]

namespace OVRVrCubeWorldSurfaceViewNDK
{
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
        static jstring Java_OVRVrCubeWorldSurfaceView_Activities_xMarshal_stringFromJNI(
            // what would we be able to do inspecting the runtime?
             JNIEnv env,
            jobject thiz)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150607-1/vrcubeworld

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150525

            var n = env.NewStringUTF;

            // look almost the same file!

            // OVR_VRAPI_EXPORT const char * vrapi_GetVersionString();

            // if we change our NDK code, will nuget packaing work on the background, and also upgrade running apps?
            var v = n( env,
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
