using ScriptCoreLib;
using ScriptCoreLibAndroidNDK.BCLImplementation.System;
using ScriptCoreLibAndroidNDK.Library;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160102/x360videos
// scriptcorelibA
[assembly: ScriptCoreLib.Script()]
[assembly: ScriptCoreLib.ScriptTypeFilter(ScriptCoreLib.ScriptType.C)]


namespace x360videoNDK
{
    [Script]
    public class xNativeActivity
    {



        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell dumpsys SurfaceFlinger
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell dumpsys battery
        // x:\util\android-sdk-windows\platform-tools\adb.exe shell ls /sdcard/oculus/360Photos/

        // x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "xNativeActivity" "System.Console" "DEBUG"
        // x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "xNativeActivity" "System.Console" "DEBUG" "PlatformActivity"
        // x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "xNativeActivity" "System.Console" "DEBUG" "PlatformActivity" "AndroidRuntime" "OVR" "VrApi" "Oculus360Photos"

        // x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "xNativeActivity" "System.Console" "DEBUG" "PlatformActivity" "AndroidRuntime" "Oculus360Photos"


        // x:\util\android-sdk-windows\platform-tools\adb.exe shell am force-stop x360video.Activities
        // x:\util\android-sdk-windows\platform-tools\adb.exe shell am start -n x360video.Activities/x360video.Activities.ApplicationActivity
        // Warning: Activity not started, its current task has been brought to the front



        // x:\util\android-sdk-windows\platform-tools\adb.exe  shell dumpsys meminfo x360video.Activities


        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell screenrecord --bit-rate 6000000 "/sdcard/oculus/Movies/My Videos/3D/OVRMyCubeWorldNDK-WASDC-mousewheel.mp4"
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell screenrecord --bit-rate 6000000 "/sdcard/oculus/Movies/My Videos/3D/OVRWindWheelActivity-fakepush.mp4"





        // Z:\jsc.svn\examples\java\android\synergy\AndroidBrowserVRNDK\xNativeActivity.cs

        // add tools, staging

        // add ScriptCoreLibAndroidNDK
        // add the test function to test jsc c and lets run it on android.






        //var stringFromJNI = xMarshal.stringFromJNI(this);
        //Console.WriteLine(new { stringFromJNI });


        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static jstring Java_x360video_Activities_xMarshal_stringFromJNI(JNIEnv env, jobject thiz, jobject args)
        {
            ConsoleExtensions.trace("enter Java_x360video_Activities_xMarshal_stringFromJNI");



            if (args != null)
            {
                var loctype = env.GetObjectClass(env, args);
                var gtype = env.NewGlobalRef(env, loctype);

                //GlobalActivityClass = (jclass)jni->NewGlobalRef(jni->GetObjectClass(activity));

                // 
                var startMovieFromNative = env.GetMethodID(env, loctype, "startMovieFromNative", "(Ljava/lang/String;)V");

                ConsoleExtensions.tracei64("startMovieFromNative: ", (int)(object)startMovieFromNative);


            }

            // do we have a console yet?
            //Console.WriteLine("enter Java_AndroidBrowserVRNDK_Activities_xMarshal_stringFromJNI");


            var n = env.NewStringUTF;

            //// if we change our NDK code, will nuget packaing work on the background, and also upgrade running apps?
            var v = n(env, "hello from Java_x360video_Activities_xMarshal_stringFromJNI. yay");

            return v;
            // ConfigurationCreateNuGetPackage.cs
        }



        // called by 
        // xMarshal.startMovieFromUDP(new { this.__ptr, pathName });
        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static void Java_x360video_Activities_xMarshal_startMovieFromUDP(JNIEnv env, jobject thiz, jobject args)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/x360videoui
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/startmoviefromudp

            // this needs to be done so that we get to play movie from paused state.
            ConsoleExtensions.trace("enter Java_x360video_Activities_xMarshal_startMovieFromUDP");

            // list all menu items.
            // if one has the filename we need. activate it?

            //var menu = this.menuitems[0];


            //var __ptr = args.__ptr;
            //var pathName = args.pathName;

            var t = new __Type { arg0_env = env, arg1_type = (jclass)env.NewGlobalRef(env, env.GetObjectClass(env, args)) };

            //sage: 'sart/runtime/check_jni.cc:65] JNI DETECTED ERROR IN APPLICATION: JNI GetFieldID called with pending exception 
            // 'java.lang.NoSuchFieldError' thrown in void x360video.Activities.xMarshal.startMovieFromUDP(java.lang.Obje


            var field__ptr = env.GetFieldID(env, t.arg1_type, "__ptr", "J");

            ConsoleExtensions.tracei64("field__ptr", (long)(object)field__ptr);

            ///xNativeActivity(16201): [14349792] \xNativeActivity.cs:138 fieldpathName -2012062200
            ///xNativeActivity(16201): [14349792] \xNativeActivity.cs:139 field__ptr -2012062232

            // http://journals.ecs.soton.ac.uk/java/tutorial/native1.1/implementing/example-1.1/FieldAccess.c


            var fieldpathName = env.GetFieldID(env, t.arg1_type, "pathName", "Ljava/lang/String;");
            ConsoleExtensions.tracei64("fieldpathName", (long)(object)fieldpathName);
            var jstr = (jstring)env.GetObjectField(env, args, fieldpathName);

            var isCopy = default(bool);
            var str = env.GetStringUTFChars(env, jstr, out isCopy);
            ConsoleExtensions.traces("pathName: ", str);


            //I/xNativeActivity(19611): [13566168] \xNativeActivity.cs:118 enter Java_x360video_Activities_xMarshal_startMovieFromUDP
            //I/xNativeActivity(19611): [13566168] \xNativeActivity.cs:138 fieldpathName -2012064080
            //I/xNativeActivity(19611): [13566168] \xNativeActivity.cs:139 field__ptr -2012064112
            //I/xNativeActivity(19611): \xNativeActivity.cs:153 pathName:  /storage/emulated/0/Oculus/360Videos/360 3D 3D  VR Timelapse Hanriver TB by ___________________.mp3._TB.mp4

            // looky. 
            // we jumped from UI to NDK. and we have the string.

            // now we need to jump into C++

            // C:\Windows\system32>x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "xNativeActivity" "System.Console" "DEBUG" "PlatformActivity" "Oculus360Videos"
            Oculus360Videos_h.startMovieFromUDP(env, (object)field__ptr, str);

            ConsoleExtensions.trace("exit Java_x360video_Activities_xMarshal_startMovieFromUDP");
        }
    }



    [Script(IsNative = true

    // thats a c++ header, wont help us. unless we fix it
     , Header = "Oculus360Videos.h"
    )]
    public class Oculus360Videos_h
    {

        public static void startMovieFromUDP(JNIEnv jni, object interfacePtr, string pathName) { }


    }
}
