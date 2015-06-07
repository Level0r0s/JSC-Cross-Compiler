using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;

namespace OVRVrCubeWorldNative.segments
{
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]

    public class LocalApplication : Application
    {
        public override void onCreate()
        {
            base.onCreate();

            Console.WriteLine("enter LocalApplication onCreate, first time?");

            // https://stackoverflow.com/questions/7686482/when-does-applications-oncreate-method-is-called-on-android
            Toast.makeText(this, "LocalApplication", Toast.LENGTH_LONG).show();

        }

    }

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo")]
    public class xActivity : Activity
    {
        // NDKHybridMockup is what is should look like if jsc allows java/c for android
        // now where is the project to demonstrate nuget so
        // lets use OVRVrCubeWorldNative to be the manual version for now.

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this);

            Func<string> inline_stringFromJNI = delegate
            {
                // env.NewStringUTF
                // there should be a .h parser somewhere. via which we can generate the natives for so?
                // jsc could generate a linker code to allow us to use c exports from java..
                return global::OVRVrCubeWorldNative.VrApi_h.vrapi_GetVersionString();
            };


            b.setText(
                // X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\Program.cs
                // http://stackoverflow.com/questions/19954156/android-build-separate-apks-for-different-processor-architectures


                //  <package id="TestNDKAsAssetFromSharedLibrary" version="1.0.0.0" targetFramework="net4" userInstalled="true" />
                // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldNativeActivity\OVRVrCubeWorldNativeActivity\ApplicationActivity.cs

                // can we load that native so into a separate process?
                // switch to native and back?
                stringFromJNI()
            //"Vibrate!"
            );


            ll.addView(b);



            this.setContentView(sv);


            //this.ShowLongToast("http://my.jsc-solutions.net x");
        }



        [Script(IsPInvoke = true)]
        //private long find(string lib, string fname) { return default(long); }
        public static string stringFromJNI() { return default(string); }


        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150607-1/vrcubeworld
        /** Load jni .so on initialization */
        static xActivity()
        {
            //Log.d( TAG, "LoadLibrary" );

            // "X:\opensource\ovr_mobile_sdk_0.6.0\VrApi\Libs\Android\armeabi-v7a\libvrapi.so"

            // did csproj copy it where it needs to be?
            java.lang.System.loadLibrary("vrapi");

            //<!-- Tell NativeActivity the name of the .so -->
            //<meta-data android:name="android.app.lib_name" android:value="vrcubeworld" />
            // why bother?

            //java.lang.System.loadLibrary("vrcubeworld");

            // need to link it!
            // "X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldNativeActivity\OVRVrCubeWorldNative\bin\Debug\staging\libs\armeabi-v7a\libOVRVrCubeWorldNative.so"

            // incline android c would automate this step.
            java.lang.System.loadLibrary("OVRVrCubeWorldNative");
        }
    }
    // is that it? and off to the deep end native world??




}

//BUILD FAILED
//x:\util\android-sdk-windows\tools\ant\build.xml:542: Application package 'OVRVrCubeWorldNative' must have a minimum of 2 segments.

//    E/AndroidRuntime( 6459): java.lang.UnsatisfiedLinkError: dalvik.system.PathClassLoader[DexPathList[[zip file "/system/framework/multiwindow.jar",
//E/AndroidRuntime( 6459):        at java.lang.Runtime.loadLibrary(Runtime.java:366)
//E/AndroidRuntime( 6459):        at java.lang.System.loadLibrary(System.java:989)
//E/AndroidRuntime( 6459):        at OVRVrCubeWorldNative.segments.xActivity.<clinit>(xActivity.java:25)
//E/AndroidRuntime( 6459):        at java.lang.reflect.Constructor.newInstance(Native Method)
//E/AndroidRuntime( 6459):        at java.lang.Class.newInstance(Class.java:1650)
//E/AndroidRuntime( 6459):        at android.app.Instrumentation.newActivity(Instrumentation.java:1079)
//E/AndroidRuntime( 6459):        at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2655)
//E/AndroidRuntime( 6459):        at android.app.ActivityThread.handleLaunchActivity(ActivityThread.java:2879)
//E/AndroidRuntime( 6459):        at android.app.ActivityThread.access$900(ActivityThread.java:182)
//E/AndroidRuntime( 6459):        at android.app.ActivityThread$H.handleMessage(ActivityThread.java:1475)
//E/AndroidRuntime( 6459):        at android.os.Handler.dispatchMessage(Handler.java:102)
//E/AndroidRuntime( 6459):        at android.os.Looper.loop(Looper.java:145)
//E/AndroidRuntime( 6459):        at android.app.ActivityThread.main(ActivityThread.java:6141)
//E/AndroidRuntime( 6459):        at java.lang.reflect.Method.invoke(Native Method)
//E/AndroidRuntime( 6459):        at java.lang.reflect.Method.invoke(Method.java:372)
//E/AndroidRuntime( 6459):        at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:1399)
//E/AndroidRuntime( 6459):        at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:1194)