using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Script()]
[assembly: ScriptTypeFilter(ScriptType.C, typeof(OVROculus360PhotosNDK.xNativeActivity))]

namespace OVROculus360PhotosNDK
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
        static jlong Java_OVROculus360Photos_Activities_xMarshal_nativeSetAppInterface(
            ref JNIEnv env,
            jclass clazz,
            jobject activity,

            jstring fromPackageNameString,
            jstring commandString,
            jstring uriString
            )
        {
            // Error	3	No overload for method '__android_log_print' takes 3 arguments	X:\jsc.svn\examples\java\android\synergy\OVROculus360PhotosNDK\OVROculus360PhotosNDK\xNativeActivity.cs	39	13	OVROculus360PhotosNDK

            ScriptCoreLibAndroidNDK.Library.ConsoleExtensions.tracei(
                 "enter Java_OVROculus360Photos_Activities_xMarshal_nativeSetAppInterface"
            );

            //log.__android_log_print(
            //    log.android_LogPriority.ANDROID_LOG_INFO,
            //    "OVROculus360PhotosNDK", 
               
            //);

            //return default(jlong);
            return jlong_default;
        }

        static jlong jlong_default;

        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static jstring Java_OVROculus360Photos_Activities_xMarshal_stringFromJNI(
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
                "from Java_OVROculus360PhotosNDK_Activities_xMarshal_stringFromJNI. yay"

                //VrApi_h.vrapi_GetVersionString()
                );

            return v;


            // ConfigurationCreateNuGetPackage.cs
        }

    }

    // is it available in this project?
    //[Script(IsNative = true, Header = "VrApi.h")]
    //public class VrApi_h
    //{
    //    public static string vrapi_GetVersionString() { return null; }
    //}
}

//E/AndroidRuntime(18960): java.lang.UnsatisfiedLinkError: No implementation found for long OVROculus360Photos.Activities.xMarshal.nativeSetAppInterface(java.lang.Object, java.lang.String, java.lang.String, java.lang.String) (tried Java_OVROculus360Photos_Activities_xMarshal_nativeSetAppInterface and Java_OVROculus360Photos_Activities_xMarshal_nativeSetAppInterface__Ljava_lang_Object_2Ljava_lang_String_2Ljava_lang_String_2Ljava_lang_String_2)
//E/AndroidRuntime(18960):        at OVROculus360Photos.Activities.xMarshal.nativeSetAppInterface(Native Method)
//E/AndroidRuntime(18960):        at OVROculus360Photos.Activities.ApplicationActivity.onCreate(ApplicationActivity.java:45)
//E/AndroidRuntime(18960):        at android.app.Activity.performCreate(Activity.java:6374)
//E/AndroidRuntime(18960):        at android.app.Instrumentation.callActivityOnCreate(Instrumentation.java:1119)
//E/AndroidRuntime(18960):        at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2767)
//E/AndroidRuntime(18960):        at android.app.ActivityThread.handleLaunchActivity(ActivityThread.java:2879)
//E/AndroidRuntime(18960):        at android.app.ActivityThread.access$900(ActivityThread.java:182)
//E/AndroidRuntime(18960):        at android.app.ActivityThread$H.handleMessage(ActivityThread.java:1475)
//E/AndroidRuntime(18960):        at android.os.Handler.dispatchMessage(Handler.java:102)
//E/AndroidRuntime(18960):        at android.os.Looper.loop(Looper.java:145)
//E/AndroidRuntime(18960):        at android.app.ActivityThread.main(ActivityThread.java:6141)
//E/AndroidRuntime(18960):        at java.lang.reflect.Method.invoke(Native Method)
//E/AndroidRuntime(18960):        at java.lang.reflect.Method.invoke(Method.java:372)
//E/AndroidRuntime(18960):        at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:1399)
//E/AndroidRuntime(18960):        at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:1194)
//I/art     ( 3836): Background partial concurrent mark sweep GC freed 35396(2MB) AllocSpace objects, 51(7MB) LOS objects, 15% free, 86MB/102MB, paused 7.789ms total 52.933ms
//D/CustomFrequencyManagerService( 3472): releaseDVFSLockLocked : Getting Lock type frm List : DVFS_MIN_LIMIT  frequency : 2100000  uid : 1000  pid : 3472  tag : ROTATION_BOOSTER@36
//V/ApplicationPolicy( 3472): isApplicationStateBlocked userId 0 pkgname OVROculus360Photos.Activities