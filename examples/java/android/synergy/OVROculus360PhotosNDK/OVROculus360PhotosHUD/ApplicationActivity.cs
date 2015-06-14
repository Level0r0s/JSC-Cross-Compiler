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
using System.Diagnostics;
using System.Threading;

namespace OVROculus360Photos.Activities
{
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
}

namespace OVROculus360PhotosHUD.Activities
{
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150613/record
    // "x:\util\android-sdk-windows\platform-tools\adb.exe" shell am start -n OVROculus360PhotosHUD.Activities/OVROculus360PhotosHUD.Activities.ApplicationActivity



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
                x = "xMarshal " + OVROculus360Photos.Activities.xMarshal.stringFromJNI();
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

            // do we need it?
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








    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:screenOrientation", value = "landscape")]
    public class ApplicationActivity : com.oculus.vrappframework.VrActivity
    {
        // http://paulbourke.net/geometry/transformationprojection/


        protected override void onCreate(Bundle savedInstanceState)
        {
            Console.WriteLine("enter OVROculus360Photos ApplicationActivity onCreate");

            base.onCreate(savedInstanceState);

            var intent = getIntent();
            var commandString = com.oculus.vrappframework.VrActivity.getCommandStringFromIntent(intent);
            var fromPackageNameString = com.oculus.vrappframework.VrActivity.getPackageStringFromIntent(intent);
            var uriString = com.oculus.vrappframework.VrActivity.getUriStringFromIntent(intent);

            // D/CrashAnrDetector( 3472):     #00 pc 00092ac0  /data/app/OVROculus360Photos.Activities-1/lib/arm/libmain.so (OVR::ovrMessageQueue::PostMessage(char const*, bool, bool)+8)

            this.appPtr = OVROculus360Photos.Activities.xMarshal.nativeSetAppInterface(
                this,
                fromPackageNameString,
                commandString,
                uriString
            );


            var sw = Stopwatch.StartNew();
            var mDraw = new DrawOnTop(this)
            {
                // yes it appears top left.

                //text = "GearVR HUD"
                text = () => sw.ElapsedMilliseconds + "ms"
            };

            //Task.Run(

            new Thread(
                delegate()
                {
                    // bg thread

                    while (true)
                    {
                        //Thread.Sleep(1000 / 15);
                        Thread.Sleep(1000 / 30);


                        mDraw.postInvalidate();
                    }
                }
            ).Start();

            this.ondispatchTouchEvent += @event =>
            {

                int action = @event.getAction();
                float x = @event.getRawX();
                float y = @event.getRawY();
                //if (action == MotionEvent.ACTION_UP)
                {
                    var halfx = 2560 / 2;
                    var halfy = 1440 / 2;

                    mDraw.x = (int)(500 + halfx - x);
                    mDraw.y = (int)(600 + y - halfy);
                    mDraw.text = () => sw.ElapsedMilliseconds + "ms \n" + new { x, y, action }.ToString();
                    //Console.WriteLine(" ::dispatchTouchEvent( " + action + ", " + x + ", " + y + " )");
                }

                // can we move hud around and record it to gif or mp4?

                return true;
            };

            // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceView\ApplicationActivity.cs
            // X:\jsc.svn\examples\java\android\AndroidLacasCameraServerActivity\AndroidLacasCameraServerActivity\ApplicationActivity.cs
            addContentView(mDraw, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WRAP_CONTENT, ViewGroup.LayoutParams.WRAP_CONTENT));


            // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect 192.168.1.126:5555
            // cmd /K x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "JniUtils"  "System.Console" "art"


            // E/JniUtils(14136): couldn't get isHybridApp, (Landroid/app/Activity;)Z

            //            I/Oculus360Photos( 9199): nativeSetAppInterface
            //I/App     ( 9199): VrAppInterface::SetActivity:
            //I/App     ( 9199): new AppLocal( 0xf51512b0 0xff8b6b80 0xeef69900 )
            //I/App     ( 9199): ----------------- AppLocal::AppLocal() -----------------
            //E/JniUtils( 9199): couldn't get getInternalCacheMemoryInBytes, (Landroid/app/Activity;)J

            //            I/JniUtils(26390): Using caller's JNIEnv
            //E/JniUtils(26390): couldn't get getInstalledPackagePath, (Ljava/lang/String;)Ljava/lang/String;

            //            I/System.Console( 3652): 0e44:0001 Searching installed packages for 'com.oculus.systemactivities'
            //I/JniUtils( 3652): ovr_GetCurrentPackageName() = OVROculus360PhotosHUD.Activities
            //I/JniUtils( 3652): ovr_GetPackageCodePath() = '/data/app/OVROculus360PhotosHUD.Activities-1/base.apk'
            //W/art     ( 3652): Attempt to remove local handle scope entry from IRT, ignoring
            //W/art     ( 3652): Attempt to remove local handle scope entry from IRT, ignoring
            //W/art     ( 3652): Attempt to remove local handle scope entry from IRT, ignoring
            //I/JniUtils( 3652): ovr_GetCurrentActivityName() = OVROculus360PhotosHUD.Activities.ApplicationActivity
            //I/JniUtils( 3652): ovr_GetCurrentPackageName() = OVROculus360PhotosHUD.Activities
            //E/JniUtils( 3652): couldn't get getLocalizedString, (Ljava/lang/String;)Ljava/lang/String;
            //I/JniUtils( 4380): ovr_GetCurrentActivityName() = com.oculus.home.HomeActivity

            // ffs
        }


        public class DrawOnTop : View
        {
            // http://stackoverflow.com/questions/6927286/force-a-view-to-redraw-itself

            public DrawOnTop(android.content.Context context)
                : base(context)
            {
            }

            // udp mouse ?
            public int x = 500; // animate it?
            public int y = 600;

            public Func<string> text = () => "hello";

            public int textSize = 25;


            protected override void onDraw(android.graphics.Canvas canvas)
            {

                var paint = new android.graphics.Paint();

                paint.setStyle(android.graphics.Paint.Style.STROKE);
                //paint.setStyle(android.graphics.Paint.Style.FILL_AND_STROKE);
                paint.setColor(android.graphics.Color.RED);
                paint.setTextSize(textSize);

                var text = this.text();

                canvas.drawText(text, x, y, paint);
                canvas.drawText(text, x + 2560 / 2, y, paint);

                base.onDraw(canvas);
            }
        }

    }


}
