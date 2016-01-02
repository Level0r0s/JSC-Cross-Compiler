using OVRWindWheelNDK;
using ScriptCoreLib;
using ScriptCoreLibAndroidNDK.Library.Reflection;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Script()]
//[assembly: ScriptTypeFilter(ScriptType.C, typeof(OVROculus360PhotosNDK.xNativeActivity))]
[assembly: ScriptTypeFilter(ScriptType.C)]

namespace OVROculus360PhotosNDK
{
    [Script]
    unsafe partial class xNativeActivity
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160102/x360videos
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150721/ovroculus360photoshud



        // wtf
        // make.exe: *** No rule to make target `jni/OVRVrCubeWorldNative.exe.c', needed by `obj/local/armeabi-v7a/objs/OVRVrCubeWorldNative/OVRVrCubeWorldNative.exe.o'.  Stop.
        // "X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldNativeActivity\OVRVrCubeWorldNative\bin\Debug\staging\jni\OVRVrCubeWorldNative.dll.c"
        // "X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldNativeActivity\OVRVrCubeWorldNative\bin\Debug\staging\libs\armeabi-v7a\libOVRVrCubeWorldNative.so"

        // Java_TestNDKAsAsset_xActivity_stringFromJNI



        static xNativeAtStartBackgroundPanoLoadInvokeCallback xNativeAtStartBackgroundPanoLoad;

        [Script(IsNative = true)]
        //public delegate void xNativeAtStartBackgroundPanoLoadInvokeCallback(string filename, ref ovrTracking Tracking);
        //public delegate void xNativeAtStartBackgroundPanoLoadInvokeCallback(string filename, ovrTracking* Tracking);
        public delegate void xNativeAtStartBackgroundPanoLoadInvokeCallback(byte* localloc_filename, ovrTracking* Tracking);


        //static string __filename;


        [Script]
        delegate jstring yieldDelegate(
            JNIEnv env,
            jobject thiz,
            jobject args
        );

        static yieldDelegate yield;







        // used for?
        static ByteArrayWithLength malloc_filename;

        //static byte[] 
        //static void xNativeAtStartBackgroundPanoLoadInvoke(string filename, ref ovrTracking Tracking)
        static void xNativeAtStartBackgroundPanoLoadInvoke(byte* localloc_filename, ovrTracking* Tracking)
        {
            malloc_filename.FromString(localloc_filename);

            //I/xNativeActivity( 1990): \xNativeActivity.cs:56 enter xNativeAtStartBackgroundPanoLoadInvoke filename:  assets/2294472375_24a3b8ef46_o.jpg errno: 2 No such file or directory
            //I/xNativeActivity( 1990): \xNativeActivity.cs:58 Tracking.HeadPose.Pose.Orientation.x  -0.023896
            //I/xNativeActivity( 1990): \xNativeActivity.cs:59 Tracking.HeadPose.Pose.Orientation.y  -0.000054
            //I/xNativeActivity( 1990): \xNativeActivity.cs:60 Tracking.HeadPose.Pose.Orientation.z  -0.002280
            //I/xNativeActivity( 1990): \xNativeActivity.cs:71 enter xNativeAtStartBackgroundPanoLoadInvoke yield filename:  assets/2śö╚▀śö╚▀♠ errno: 110 Connection timed out

            //__filename = filename;
            //new string(

            //var loc_filename = System.Runtime.InteropServices.Marshal.PtrToStringAuto(

            ScriptCoreLibAndroidNDK.Library.ConsoleExtensions.traces("enter xNativeAtStartBackgroundPanoLoadInvoke filename: ", malloc_filename.AsString());
            //ScriptCoreLibAndroidNDK.Library.ConsoleExtensions.tracep("enter xNativeAtStartBackgroundPanoLoadInvoke filename: ", filename);
            ScriptCoreLibAndroidNDK.Library.ConsoleExtensions.tracef("Tracking.HeadPose.Pose.Orientation.x ", Tracking->HeadPose.Pose.Orientation.x);
            ScriptCoreLibAndroidNDK.Library.ConsoleExtensions.tracef("Tracking.HeadPose.Pose.Orientation.y ", Tracking->HeadPose.Pose.Orientation.y);
            ScriptCoreLibAndroidNDK.Library.ConsoleExtensions.tracef("Tracking.HeadPose.Pose.Orientation.z ", Tracking->HeadPose.Pose.Orientation.z);

            //I/xNativeActivity(17768): \xNativeActivity.cs:48 enter xNativeAtStartBackgroundPanoLoadInvoke filename:  assets/2294472375_24a3b8ef46_o.jpg errno: 2 No such file or directory
            //I/xNativeActivity(17768): \xNativeActivity.cs:50 Tracking.HeadPose.Pose.Orientation.x  -0.259649
            //I/xNativeActivity(17768): \xNativeActivity.cs:51 Tracking.HeadPose.Pose.Orientation.y  0.002016
            //I/xNativeActivity(17768): \xNativeActivity.cs:52 Tracking.HeadPose.Pose.Orientation.z  0.041219

            // x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "art" "xNativeActivity" "System.Console" "DEBUG" "PlatformActivity" "AndroidRuntime" "Oculus360Photos"


            argsF aF = null;


            yield = (env, thiz, args) =>
            {
                //ScriptCoreLibAndroidNDK.Library.ConsoleExtensions.traces("enter xNativeAtStartBackgroundPanoLoadInvoke yield filename: ", malloc_filename.AsString());


                if (aF == null)
                    aF = new argsF { env = env, fields = args };


                aF["x"] = Tracking->HeadPose.Pose.Orientation.x;
                aF["y"] = Tracking->HeadPose.Pose.Orientation.y;
                aF["z"] = Tracking->HeadPose.Pose.Orientation.z;

                //                I/DEBUG   (30772): pid: 28172, tid: 28248, name: Thread-5951  >>> OVROculus360PhotosHUD.Activities <<<
                //I/DEBUG   (30772): signal 6 (SIGABRT), code -6 (SI_TKILL), fault addr --------
                //I/DEBUG   (30772): Abort message: 'sart/runtime/check_jni.cc:65] JNI DETECTED ERROR IN APPLICATION: input is not valid Modified UTF-8: illegal continuation byte 0x1'

                // Error	19	Cannot use ref or out parameter 'Tracking' inside an anonymous method, lambda expression, or query expression	X:\jsc.svn\examples\java\android\synergy\OVROculus360PhotosNDK\OVROculus360PhotosNDK\xNativeActivity.cs	58	115	OVROculus360PhotosNDK

                //ScriptCoreLibAndroidNDK.Library.ConsoleExtensions.tracef("Tracking.HeadPose.Pose.Orientation.x ", Tracking.HeadPose.Pose.Orientation.x);

                //F/art     (28172): sart/runtime/check_jni.cc:65] JNI DETECTED ERROR IN APPLICATION: input is not valid Modified UTF-8: illegal continuation byte 0x1
                //F/art     (28172): sart/runtime/check_jni.cc:65]     string: 'pbq┌☺'
                //F/art     (28172): sart/runtime/check_jni.cc:65]     in call to NewStringUTF
                //F/art     (28172): sart/runtime/check_jni.cc:65]     from java.lang.String OVROculus360Photos.Activities.xMarshal.stringFromJNI(java.lang.Object)

                var n = env.NewStringUTF;

                //Type.GetType(

                // look almost the same file!

                // OVR_VRAPI_EXPORT const char * vrapi_GetVersionString();

                // if we change our NDK code, will nuget packaing work on the background, and also upgrade running apps?
                var v = n(env, malloc_filename.AsString());

                return v;
            };
        }

        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static jstring Java_OVROculus360Photos_Activities_xMarshal_stringFromJNI(
            // what would we be able to do inspecting the runtime?
             JNIEnv env,
            jobject thiz,
            jobject args
            )
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150607-1/vrcubeworld

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150525

            var x = default(jstring);

            if (yield != null)
                x = yield(env, thiz, args);

            return x;

            ////var n = env.NewStringUTF;

            ////Type.GetType(

            //// look almost the same file!

            //// OVR_VRAPI_EXPORT const char * vrapi_GetVersionString();

            //// if we change our NDK code, will nuget packaing work on the background, and also upgrade running apps?
            //var v = n(env, "from Java_OVROculus360PhotosNDK_Activities_xMarshal_stringFromJNI. yay");

            //return v;


            // ConfigurationCreateNuGetPackage.cs
        }




        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static long Java_OVROculus360Photos_Activities_xMarshal_nativeSetAppInterface(
             JNIEnv env,
            jclass clazz,
            jobject activity,

            jstring fromPackageNameString,
            jstring commandString,
            jstring uriString
            )
        {
            // Error	3	No overload for method '__android_log_print' takes 3 arguments	X:\jsc.svn\examples\java\android\synergy\OVROculus360PhotosNDK\OVROculus360PhotosNDK\xNativeActivity.cs	39	13	OVROculus360PhotosNDK

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150721/ovroculus360photoshud
            ScriptCoreLibAndroidNDK.Library.ConsoleExtensions.trace("enter Java_OVROculus360Photos_Activities_xMarshal_nativeSetAppInterface");


            //Oculus360Photos_h.AtStartBackgroundPanoLoad = new object();
            xNativeAtStartBackgroundPanoLoad = xNativeAtStartBackgroundPanoLoadInvoke;

            //xNativeAtStartBackgroundPanoLoad("not yet loaded", null);


            return Oculus360Photos_h.Java_com_oculus_oculus360photossdk_MainActivity_nativeSetAppInterface(
                 env,
                clazz,
                activity,
                fromPackageNameString,
                commandString,
                uriString,


                arg_AtStartBackgroundPanoLoad: xNativeAtStartBackgroundPanoLoad
            );
        }


    }

    // is it available in this project?
    //[Script(IsNative = true, Header = "VrApi.h")]
    //public class VrApi_h
    //{
    //    public static string vrapi_GetVersionString() { return null; }
    //}

    [Script(IsNative = true

        // thats a c++ header, wont help us. unless we fix it
         , Header = "Oculus360Photos.h"
        )]
    public class Oculus360Photos_h
    {
        // defined at
        // X:\opensource\ovr_mobile_sdk_0.6.0.1\VrSamples\Native\Oculus360PhotosSDK\jni\Oculus360Photos.cpp
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150721/ovroculus360photoshud
        //public static object AtStartBackgroundPanoLoad;


        public static long Java_com_oculus_oculus360photossdk_MainActivity_nativeSetAppInterface(JNIEnv env,
            jclass clazz,
            jobject activity,

            jstring fromPackageNameString,
            jstring commandString,
            jstring uriString,

            object arg_AtStartBackgroundPanoLoad
         ) { return 0; }
    }
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

//jni/OVROculus360PhotosNDK.dll.c: In function 'ScriptCoreLibAndroidNDK_BCLImplementation_System_Threading___Thread_Start':
//jni/OVROculus360PhotosNDK.dll.c:237:9: error: variable 'num0' set but not used [-Werror=unused-but-set-variable]
//     int num0;

//jni/OVROculus360PhotosNDK.dll.c:257:2: error: passing argument 1 of 'ScriptCoreLibNative_BCLImplementation_System___MulticastDelegate__ctor_6000013' from incompatible pointer type [-Werror]
//  {ScriptCoreLibNative_BCLImplementation_System___MulticastDelegate__ctor_6000013(__that, object, method); return __that; }
//  ^