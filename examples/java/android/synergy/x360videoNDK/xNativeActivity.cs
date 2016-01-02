using ScriptCoreLib;
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


        // x:\util\android-sdk-windows\platform-tools\adb.exe shell am force-stop OVROculus360PhotosHUD.Activities
        // x:\util\android-sdk-windows\platform-tools\adb.exe shell am start -n OVROculus360PhotosHUD.Activities/OVROculus360PhotosHUD.Activities.ApplicationActivity
        // Warning: Activity not started, its current task has been brought to the front


        
        // x:\util\android-sdk-windows\platform-tools\adb.exe  shell dumpsys meminfo OVRWindWheelActivity.Activities


        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell screenrecord --bit-rate 6000000 "/sdcard/oculus/Movies/My Videos/3D/OVRMyCubeWorldNDK-WASDC-mousewheel.mp4"
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell screenrecord --bit-rate 6000000 "/sdcard/oculus/Movies/My Videos/3D/OVRWindWheelActivity-fakepush.mp4"





        // Z:\jsc.svn\examples\java\android\synergy\AndroidBrowserVRNDK\xNativeActivity.cs

        // add tools, staging

        // add ScriptCoreLibAndroidNDK
        // add the test function to test jsc c and lets run it on android.



        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static jstring Java_x360video_Activities_xMarshal_stringFromJNI(JNIEnv env, jobject thiz, jobject args)
        {
            ConsoleExtensions.trace("enter Java_x360video_Activities_xMarshal_stringFromJNI");

            // do we have a console yet?
            //Console.WriteLine("enter Java_AndroidBrowserVRNDK_Activities_xMarshal_stringFromJNI");


            var n = env.NewStringUTF;

            //// if we change our NDK code, will nuget packaing work on the background, and also upgrade running apps?
            var v = n(env, "hello from Java_x360video_Activities_xMarshal_stringFromJNI. yay");

            return v;
            // ConfigurationCreateNuGetPackage.cs
        }
    }
}
