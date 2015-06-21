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
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

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
                Toast.makeText(this, "OVRVrCubeWorldSurfaceView! " + x + " " + api, Toast.LENGTH_LONG).show();
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

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:screenOrientation", value = "landscape")]
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

            #region xCallback
            // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceView\ApplicationActivity.cs
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
            #endregion






            mView = new SurfaceView(this);
            this.setContentView(mView);



            //            E/AndroidRuntime(22718): Caused by: java.lang.NullPointerException: Attempt to invoke virtual method 'android.view.ViewParent android.view.View.getParent()' on a null object reference
            //E/AndroidRuntime(22718):        at android.view.ViewGroup.addViewInner(ViewGroup.java:4216)
            //E/AndroidRuntime(22718):        at android.view.ViewGroup.addView(ViewGroup.java:4070)
            //E/AndroidRuntime(22718):        at android.view.ViewGroup.addView(ViewGroup.java:4046)
            //E/AndroidRuntime(22718):        at com.android.internal.policy.impl.PhoneWindow.setContentView(PhoneWindow.java:478)
            //E/AndroidRuntime(22718):        at com.android.internal.policy.impl.PhoneWindow.setContentView(PhoneWindow.java:459)
            //E/AndroidRuntime(22718):        at android.app.Activity.setContentView(Activity.java:2298)

            var sw = Stopwatch.StartNew();

            #region mDraw
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
            #endregion


            this.ondispatchTouchEvent = @event =>
            {
                if (mNativeHandle == 0)
                    return;

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
                GLES3JNILib.onTouchEvent(mNativeHandle, action, x, y);

                // can we move hud around and record it to gif or mp4?
            };

            this.ondispatchKeyEvent = @event =>
            {
                if (mNativeHandle == 0)
                    return false;

                int keyCode = @event.getKeyCode();
                int action = @event.getAction();
                if (action != KeyEvent.ACTION_DOWN && action != KeyEvent.ACTION_UP)
                {
                    return base.dispatchKeyEvent(@event);
                }
                if (action == KeyEvent.ACTION_UP)
                {
                    // keycode 4
                    mDraw.text = () => sw.ElapsedMilliseconds + "ms \n" + new { keyCode, action }.ToString();
                    //Log.v(TAG, "GLES3JNIActivity::dispatchKeyEvent( " + keyCode + ", " + action + " )");
                }
                GLES3JNILib.onKeyEvent(mNativeHandle, keyCode, action);

                return true;
            };

            // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceView\ApplicationActivity.cs
            // X:\jsc.svn\examples\java\android\AndroidLacasCameraServerActivity\AndroidLacasCameraServerActivity\ApplicationActivity.cs
            addContentView(mDraw, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WRAP_CONTENT, ViewGroup.LayoutParams.WRAP_CONTENT));

            mView.getHolder().addCallback(xCallback);

            // Force the screen to stay on, rather than letting it dim and shut off
            // while the user is watching a movie.

            // does this disable the face sensor?
            getWindow().addFlags(WindowManager_LayoutParams.FLAG_KEEP_SCREEN_ON);

            // Force screen brightness to stay at maximum
            //WindowManager_LayoutParams _params = getWindow().getAttributes();
            //_params.screenBrightness = 1.0f;
            //getWindow().setAttributes(_params);

            mNativeHandle = com.oculus.gles3jni.GLES3JNILib.onCreate(this);

            // can we now overlay something on top of the surface?
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

            public Func<string> text = () => "hello!";

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

        #region Activity life cycle
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

        #endregion



        Func<KeyEvent, bool> ondispatchKeyEvent;
        public override bool dispatchKeyEvent(KeyEvent @event)
        {


            return ondispatchKeyEvent(@event);
        }

        Action<MotionEvent> ondispatchTouchEvent;
        public override bool dispatchTouchEvent(MotionEvent @event)
        {
            // never fired? why the duck?
            ondispatchTouchEvent(@event);


            return true;
        }
    }


}

//[javac] W:\src\ScriptCoreLib\Shared\BCLImplementation\System\Threading\Tasks\__TaskExtensions___c__DisplayClass2_1.java:38: error: cannot find symbol
//[javac]         task_10.ContinueWith_060024e2(new __Action_1<__Task_1<TResult>>(this,
//[javac]                ^


//[javac]     W:\gen\OVRVrCubeWorldSurfaceView\Activities\R.java
//[javac] W:\src\ScriptCoreLib\Shared\BCLImplementation\System\Threading\Tasks\__TaskExtensions.java:34: error: cannot find symbol
//[javac]         task.ContinueWith_06000318(new __Action_1<__Task_1<__Task_1<TResult>>>(class2_10,
//[javac]             ^
//[javac]   symbol:   method ContinueWith_06000318(__Action_1<__Task_1<__Task_1<TResult>>>)

//E/AndroidRuntime(17306): Caused by: java.lang.NullPointerException: throw with null exception
//E/AndroidRuntime(17306):        at com.oculus.gles3jni.GLES3JNILib.onCreate(GLES3JNILib.java:22)
//E/AndroidRuntime(17306):        at OVRVrCubeWorldSurfaceView.Activities.ApplicationActivity.onCreate(ApplicationActivity.java:94)
//E/AndroidRuntime(17306):        at android.app.Activity.performCreate(Activity.java:6374)
//E/AndroidRuntime(17306):        at android.app.Instrumentation.callActivityOnCreate(Instrumentation.java:1119)
//E/AndroidRuntime(17306):        at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2767)
//E/AndroidRuntime(17306):        ... 10 more

//Implementation not found for type import :
//type: System.Threading.Tasks.Task
//method: System.Threading.Tasks.Task Run(System.Func`1[System.Threading.Tasks.Task])
//Did you forget to add the [Script] attribute?
//Please double check the signature!
