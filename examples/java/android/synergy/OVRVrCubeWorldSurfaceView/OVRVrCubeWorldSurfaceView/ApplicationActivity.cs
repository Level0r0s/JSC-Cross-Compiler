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
using com.oculus.gles3jni;

namespace OVRVrCubeWorldSurfaceView.Activities
{
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]

    // https://forums.oculus.com/viewtopic.php?t=21409
    // vr_dual wont run on gearvr for now...
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "com.samsung.android.vr.application.mode", value = "vr_dual")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "com.samsung.android.vr.application.mode", value = "vr_only")]
    //<meta-data android:name="com.samsung.android.vr.application.mode" android:value="vr_only"/>
    public class LocalApplication : Application
    {
        public override void onCreate()
        {
            base.onCreate();

            Console.WriteLine("enter LocalApplication onCreate, first time?");

            Func<string> futureinline_stringFromJNI = delegate
            {
                // env.NewStringUTF
                // there should be a .h parser somewhere. via which we can generate the natives for so?
                // jsc could generate a linker code to allow us to use c exports from java..
                return global::OVRVrCubeWorldSurfaceViewNDK.VrApi_h.vrapi_GetVersionString();
            };

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
            var api = typeof(com.oculus.vrapi.VrApi);
            // where is the source for it?
            // X:\opensource\ovr_mobile_sdk_0.5.1\VRLib\src\com\oculusvr\vrlib\VrLib.java
            // X:\opensource\ovr_mobile_sdk_0.5.1\VRLib\jni\VrApi\VrApi.cpp


            try
            {
                x = futureinline_stringFromJNI();
            }
            catch
            {
                x = "xMarshal " + xMarshal.stringFromJNI();
            }
            finally
            {
                //                [javac] W:\src\__AnonymousTypes__OVRVrCubeWorldNativeActivity_AndroidActivity\__f__AnonymousType_97_0_1.java:34: error: reference to Format is ambiguous, both method Format(String,Object,Object) in __String and method Format(__IFormatProvider,String,Object[]) in __String match
                //[javac]         return __String.Format(null, "{{ api = {0} }}", objectArray2);
                //[javac]                        ^
                //[javac] Note: W:\src\ScriptCoreLibJava\BCLImplementation\System\Threading\__Thread.java uses or overrides a deprecated API.

                // https://stackoverflow.com/questions/7686482/when-does-applications-oncreate-method-is-called-on-android
                //Toast.makeText(this, "OVRVrCubeWorldNative " + x + new { api }, Toast.LENGTH_LONG).show();
                Toast.makeText(this, "OVRVrCubeWorldNative " + x + " " + api, Toast.LENGTH_LONG).show();
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

    static class xMarshal
    {
        [Script(IsPInvoke = true)]
        //private long find(string lib, string fname) { return default(long); }
        public static string stringFromJNI() { return default(string); }
    }

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        // SurfaceHolder

        // "X:\opensource\ovr_mobile_sdk_0.6.0\VrSamples\Native\VrCubeWorld_SurfaceView\src\com\oculus\gles3jni\GLES3JNIActivity.java"
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150607-1/surfaceview

        public class xCallback : SurfaceHolder_Callback
        {

            public Action<SurfaceHolder, int, int, int> onsurfaceChanged;
            public void surfaceChanged(SurfaceHolder arg0, int format, int width, int height)
            {
                onsurfaceChanged(arg0, format, width, height);
            }

            public Action<SurfaceHolder> onsurfaceCreated;
            public void surfaceCreated(SurfaceHolder value)
            {
                onsurfaceCreated(value);
            }

            public Action<SurfaceHolder> onsurfaceDestroyed;
            public void surfaceDestroyed(SurfaceHolder value)
            {
                onsurfaceDestroyed(value);
            }
        }

        private SurfaceView mView;
        private long mNativeHandle;

        private SurfaceHolder mSurfaceHolder;
        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var xCallback = new xCallback
            {
                onsurfaceCreated = holder =>
                {
                    if (mNativeHandle != 0)
                    {
                        GLES3JNILib.onSurfaceCreated(mNativeHandle, holder.getSurface());
                        mSurfaceHolder = holder;
                    }
                },

                onsurfaceChanged = (SurfaceHolder holder, int format, int width, int height) =>
                {
                    if (mNativeHandle != 0)
                    {
                        GLES3JNILib.onSurfaceChanged(mNativeHandle, holder.getSurface());
                        mSurfaceHolder = holder;
                    }
                },

                onsurfaceDestroyed = holder =>
                {
                    if (mNativeHandle != 0)
                    {
                        GLES3JNILib.onSurfaceDestroyed(mNativeHandle);
                        mSurfaceHolder = null;
                    }
                }
            };

            mView = new SurfaceView(this);
            setContentView(mView);
            mView.getHolder().addCallback(xCallback);

            // Force the screen to stay on, rather than letting it dim and shut off
            // while the user is watching a movie.
            //getWindow().addFlags(WindowManager_LayoutParams.FLAG_KEEP_SCREEN_ON);

            // Force screen brightness to stay at maximum
            //WindowManager_LayoutParams _params = getWindow().getAttributes();
            //_params.screenBrightness = 1.0f;
            //getWindow().setAttributes(_params);

            mNativeHandle = com.oculus.gles3jni.GLES3JNILib.onCreate(this);
        }

        protected override void onStart()
        {
            base.onStart();
            GLES3JNILib.onStart(mNativeHandle);
        }

        protected override void onResume()
        {
            base.onResume();
            GLES3JNILib.onResume(mNativeHandle);
        }

        protected override void onPause()
        {
            GLES3JNILib.onPause(mNativeHandle);
            base.onPause();
        }

        protected override void onStop()
        {
            GLES3JNILib.onStop(mNativeHandle);
            base.onStop();
        }

        protected override void onDestroy()
        {
            if (mSurfaceHolder != null)
            {
                GLES3JNILib.onSurfaceDestroyed(mNativeHandle);
            }
            GLES3JNILib.onDestroy(mNativeHandle);
            base.onDestroy();
            mNativeHandle = 0;
        }

        public override bool dispatchKeyEvent(KeyEvent @event)
        {

            if (mNativeHandle != 0)
            {
                int keyCode = @event.getKeyCode();
                int action = @event.getAction();
                if (action != KeyEvent.ACTION_DOWN && action != KeyEvent.ACTION_UP)
                {
                    return base.dispatchKeyEvent(@event);
                }
                if (action == KeyEvent.ACTION_UP)
                {
                    //Log.v(TAG, "GLES3JNIActivity::dispatchKeyEvent( " + keyCode + ", " + action + " )");
                }
                GLES3JNILib.onKeyEvent(mNativeHandle, keyCode, action);
            }
            return true;
        }

        public override bool dispatchTouchEvent(MotionEvent @event)
        {
            if (mNativeHandle != 0)
            {
                int action = @event.getAction();
                float x = @event.getRawX();
                float y = @event.getRawY();
                if (action == MotionEvent.ACTION_UP)
                {
                    Console.WriteLine(" ::dispatchTouchEvent( " + action + ", " + x + ", " + y + " )");
                }
                GLES3JNILib.onTouchEvent(mNativeHandle, action, x, y);
            }
            return true;
        }
    }


}

namespace com.oculus.gles3jni
{
    // inline C ?
    public static class GLES3JNILib
    {
        // Activity lifecycle
        public static long onCreate(Activity obj) { throw null; }
        public static void onStart(long handle) { throw null; }
        public static void onResume(long handle) { throw null; }
        public static void onPause(long handle) { throw null; }
        public static void onStop(long handle) { throw null; }
        public static void onDestroy(long handle) { throw null; }

        // Surface lifecycle
        public static void onSurfaceCreated(long handle, Surface s) { throw null; }
        public static void onSurfaceChanged(long handle, Surface s) { throw null; }
        public static void onSurfaceDestroyed(long handle) { throw null; }

        // Input       
        public static void onKeyEvent(long handle, int keyCode, int action) { throw null; }
        public static void onTouchEvent(long handle, int action, float x, float y) { throw null; }
    }
}

//[javac] W:\src\ScriptCoreLib\Shared\BCLImplementation\System\Threading\Tasks\__TaskExtensions___c__DisplayClass2_1.java:38: error: cannot find symbol
//[javac]         task_10.ContinueWith_060024e2(new __Action_1<__Task_1<TResult>>(this,
//[javac]                ^