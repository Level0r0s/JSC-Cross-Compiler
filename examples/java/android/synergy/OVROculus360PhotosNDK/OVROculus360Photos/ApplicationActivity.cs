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
using android.content;

namespace OVROculus360Photos.Activities
{
    // "X:\opensource\ovr_mobile_sdk_0.6.0\VrSamples\Native\Oculus360PhotosSDK\src\com\oculus\oculus360photossdk\MainActivity.java"

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]

    // https://forums.oculus.com/viewtopic.php?t=21409
    // vr_dual wont run on gearvr for now...
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "com.samsung.android.vr.application.mode", value = "vr_dual")]

    // vr_only means we wont see our activity ui
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "com.samsung.android.vr.application.mode", value = "vr_only")]
    //<meta-data android:name="com.samsung.android.vr.application.mode" android:value="vr_only"/>
    public class LocalApplication : Application
    {
        public override void onCreate()
        {
            base.onCreate();

            Console.WriteLine("enter OVROculus360Photos LocalApplication onCreate, first time?");

            //Func<string> futureinline_stringFromJNI = delegate
            //{
            //    // env.NewStringUTF
            //    // there should be a .h parser somewhere. via which we can generate the natives for so?
            //    // jsc could generate a linker code to allow us to use c exports from java..
            //    return global::OVRVrCubeWorldSurfaceViewNDK.VrApi_h.vrapi_GetVersionString();
            //};

            var x = default(string);

            // X:\opensource\ovr_mobile_sdk_0.6.0\VrApi\Libs\Android
            // X:\jsc.svn\examples\opensource\ovr_mobile_sdk_0.6.0\VrApi\Libs\Android\VrApi.jar'


            //1cb8:01:04:0e CreateToJARImportNatives Cache { FileNameString = X:\jsc.svn\examples\opensource\ovr_mobile_sdk_0.6.0\VrApi\Libs\Android\VrApi.jar, Input = X:\jsc.svn\examples\opensource\ovr_mobile_sdk_0.6.0\VrApi\Libs\Android\VrApi.jar }
            //System.IO.DirectoryNotFoundException: Could not find a part of the path 'X:\jsc.svn\examples\opensource\ovr_mobile_sdk_0.6.0\VrApi\Libs\Android\VrApi.jar'.
            //   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
            //   at System.IO.FileStream.Init(String path, FileMode mode, FileAccess access, Int32 rights, Boolean useRights, FileShare share, Int32 bufferSize, FileOptions options, SECURITY_ATTRIBUTES secAttrs, String msgPath, Boolean bFromProxy, Boole
            //   at System.IO.FileStream..ctor(String path, FileMode mode, FileAccess access, FileShare share)
            //   at System.IO.File.OpenRead(String path)
            //   at jsc.meta.Library.CreateToJARImportNatives.<>c__DisplayClassf.<InternalWithCache>b__e(SHA1CryptoServiceProvider h) in x:\jsc.internal.git\compiler\jsc.internal\jsc.internal\meta\Library\CreateToJARImportNatives.cs:line 251
            //var api = typeof(com.oculus.vrapi.VrApi);
            // where is the source for it?
            // X:\opensource\ovr_mobile_sdk_0.5.1\VRLib\src\com\oculusvr\vrlib\VrLib.java
            // X:\opensource\ovr_mobile_sdk_0.5.1\VRLib\jni\VrApi\VrApi.cpp


            //try
            //{
            //    x = futureinline_stringFromJNI();
            //}
            //catch
            {
                x = "xMarshal " + xMarshal.stringFromJNI();
            }
            //finally
            {
                //                [javac] W:\src\__AnonymousTypes__OVRVrCubeWorldNativeActivity_AndroidActivity\__f__AnonymousType_97_0_1.java:34: error: reference to Format is ambiguous, both method Format(String,Object,Object) in __String and method Format(__IFormatProvider,String,Object[]) in __String match
                //[javac]         return __String.Format(null, "{{ api = {0} }}", objectArray2);
                //[javac]                        ^
                //[javac] Note: W:\src\ScriptCoreLibJava\BCLImplementation\System\Threading\__Thread.java uses or overrides a deprecated API.

                // https://stackoverflow.com/questions/7686482/when-does-applications-oncreate-method-is-called-on-android
                //Toast.makeText(this, "OVRVrCubeWorldNative " + x + new { api }, Toast.LENGTH_LONG).show();
                Toast.makeText(this, "OVRVrCubeWorldSurfaceView " + x 
                    //+ " " + api
                    , Toast.LENGTH_LONG).show();
            }

            //I/VrApi   (  401):              "Message":      "Thread priority security exception. Make sure the APK is signed."

        }

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150607-1/vrcubeworld
        /** Load jni .so on initialization */
        static LocalApplication()
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
            //java.lang.System.loadLibrary("OVRVrCubeWorldNative");
            java.lang.System.loadLibrary("main");
        }
    }

//E/AndroidRuntime(30914): java.lang.UnsatisfiedLinkError: No implementation found for java.lang.String OVROculus360Photos.Activities.xMarshal.stringFromJNI() (tried Java_OVROculus360Photos_Activities_xMarshal_stringFromJNI and Java_OVROculus360Photos_Activities_xMarshal_stringFromJNI__)
//E/AndroidRuntime(30914):        at OVROculus360Photos.Activities.xMarshal.stringFromJNI(Native Method)
//E/AndroidRuntime(30914):        at OVROculus360Photos.Activities.LocalApplication.onCreate(LocalApplication.java:40)
//E/AndroidRuntime(30914):        at android.app.Instrumentation.callApplicationOnCreate(Instrumentation.java:1020)
//E/AndroidRuntime(30914):        at android.app.ActivityThread.handleBindApplication(ActivityThread.java:5252)
//E/AndroidRuntime(30914):        at android.app.ActivityThread.access$1600(ActivityThread.java:182)
//E/AndroidRuntime(30914):        at android.app.ActivityThread$H.handleMessage(ActivityThread.java:1536)
//E/AndroidRuntime(30914):        at android.os.Handler.dispatchMessage(Handler.java:102)
//E/AndroidRuntime(30914):        at android.os.Looper.loop(Looper.java:145)
//E/AndroidRuntime(30914):        at android.app.ActivityThread.main(ActivityThread.java:6141)
//E/AndroidRuntime(30914):        at java.lang.reflect.Method.invoke(Native Method)
//E/AndroidRuntime(30914):        at java.lang.reflect.Method.invoke(Method.java:372)
//E/AndroidRuntime(30914):        at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:1399)
//E/AndroidRuntime(30914):        at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:1194)
//I/LoadedApk( 4548): getClassLoader :dalvik.system.PathClassLoader[DexPathList[[zip file "/system/framework/com.android.location.provider.jar", zip file "/system/framework/com.android.media.remotedisplay.jar", zip file "/data/app/com.google.android.gms-2/base.apk"],nativeLibraryDirectories=[/dat

    static class xMarshal
    {
        [Script(IsPInvoke = true)]
        //private long find(string lib, string fname) { return default(long); }
        public static string stringFromJNI() { return default(string); }

        [Script(IsPInvoke = true)]
        public static long nativeSetAppInterface(
            //com.oculusvr.vrlib.VrActivity act,
            object act,

            string fromPackageNameString, 
            string commandString, 
            string uriString)
        { return default(long); }
    }

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:screenOrientation", value = "landscape")]
    public class ApplicationActivity : com.oculus.vrappframework.VrActivity
    {
        // "X:\opensource\ovr_mobile_sdk_0.6.0\VrAppFramework\Projects\Android\src\com\oculus\vrappframework\VrActivity.java"
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150611/ovroculus360photos

        protected override void onCreate(Bundle savedInstanceState)
        {
            Console.WriteLine("enter OVROculus360Photos ApplicationActivity onCreate");

            base.onCreate(savedInstanceState);

            var intent = getIntent();
            var commandString = com.oculus.vrappframework.VrActivity.getCommandStringFromIntent(intent);
            var fromPackageNameString = com.oculus.vrappframework.VrActivity.getPackageStringFromIntent(intent);
            var uriString = com.oculus.vrappframework.VrActivity.getUriStringFromIntent(intent);

            // D/CrashAnrDetector( 3472):     #00 pc 00092ac0  /data/app/OVROculus360Photos.Activities-1/lib/arm/libmain.so (OVR::ovrMessageQueue::PostMessage(char const*, bool, bool)+8)

            this.setAppPtr(
                xMarshal.nativeSetAppInterface(
                this, 
                fromPackageNameString, 
                commandString, 
                uriString
                )
            );
        }


    }


}
