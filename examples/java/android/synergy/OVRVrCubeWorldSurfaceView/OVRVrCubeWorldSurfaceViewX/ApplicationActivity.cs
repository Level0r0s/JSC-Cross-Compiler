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
using System.Threading;
using System.Diagnostics;

namespace OVRVrCubeWorldSurfaceViewX.Activities
{
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]

    // https://forums.oculus.com/viewtopic.php?t=21409
    // dual shows our own popup and inserting in gearvr stays black.
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "com.samsung.android.vr.application.mode", value = "dual")]

    // what if we want to display our own welcome screen?
    // com.samsung.android.hmt.vrsvc/com.samsung.android.hmt.vrsvc.WaitActivity
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "com.samsung.android.vr.application.mode", value = "vr_only")]
    public class LocalApplication : Application
    {
        public override void onCreate()
        {
            base.onCreate();

            // where is the source for it?
            //  <Content Include="x:\opensource\ovr_mobile_sdk_0.6.0\VrApi\Libs\Android\VrApi.jar">
            // com/oculus/vrapi/VrApi
            var api = typeof(com.oculus.vrapi.VrApi);

            //I/VrApi   (  401):              "Message":      "Thread priority security exception. Make sure the APK is signed."
        }

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150607-1/vrcubeworld
        /** Load jni .so on initialization */
        static LocalApplication()
        {
            java.lang.System.loadLibrary("vrapi");
            java.lang.System.loadLibrary("main");
        }
    }


    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:screenOrientation", value = "landscape")]

    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell dumpsys battery
        // x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "xNativeActivity" "System.Console" "DEBUG"
        // x:\util\android-sdk-windows\platform-tools\adb.exe shell am force-stop OVRVrCubeWorldSurfaceViewX.Activities
        // x:\util\android-sdk-windows\platform-tools\adb.exe shell am start -n OVRVrCubeWorldSurfaceViewX.Activities/OVRVrCubeWorldSurfaceViewX.Activities.ApplicationActivity
        // https://code.google.com/p/android/issues/detail?can=2&start=0&num=100&q=&colspec=ID%20Type%20Status%20Owner%20Summary%20Stars&groupby=&sort=&id=75442

        private SurfaceHolder mSurfaceHolder;
        private SurfaceView mView;


        private ovrAppThreadPointer appThread;


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


        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            Console.WriteLine("enter OVRVrCubeWorldSurfaceViewX onCreate");




            #region xCallback
            // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceView\ApplicationActivity.cs
            var xCallback = new xCallback
            {
                onsurfaceCreated = holder =>
                {
                    //Console.WriteLine("enter onsurfaceCreated " + new { appThread });
                    if (appThread == 0)
                        return;

                    appThread.onSurfaceCreated(holder.getSurface());
                    mSurfaceHolder = holder;

                    //Console.WriteLine("exit onsurfaceCreated " + new { appThread });
                },

                onsurfaceChanged = (SurfaceHolder holder, int format, int width, int height) =>
                {
                    if (appThread == 0)
                        return;

                    appThread.onSurfaceChanged(holder.getSurface());
                    mSurfaceHolder = holder;
                },

                onsurfaceDestroyed = holder =>
                {
                    if (appThread == 0)
                        return;

                    appThread.onSurfaceDestroyed();
                    mSurfaceHolder = null;
                }
            };
            #endregion




            mView = new SurfaceView(this);
            this.setContentView(mView);

            var sw = Stopwatch.StartNew();

            #region mDraw
            var mDraw = new DrawOnTop(this)
            {
                // yes it appears top left.

                //text = "GearVR HUD"
                // (out) VrApi.vrapi_GetVersionString()
                text = () => sw.ElapsedMilliseconds + "ms " + GLES3JNILib.stringFromJNI()
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

            #region ondispatchTouchEvent
            this.ondispatchTouchEvent = @event =>
            {
                if (appThread == 0)
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
                    //mDraw.text = () => sw.ElapsedMilliseconds + "ms \n" + new { x, y, action }.ToString();
                    //Console.WriteLine(" ::dispatchTouchEvent( " + action + ", " + x + ", " + y + " )");
                }
                appThread.onTouchEvent(action, x, y);

                // can we move hud around and record it to gif or mp4?
            };
            #endregion

            #region ondispatchKeyEvent
            this.ondispatchKeyEvent = @event =>
            {
                if (appThread == 0)
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
                appThread.onKeyEvent(keyCode, action);

                return true;
            };
            #endregion


            addContentView(mDraw, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WRAP_CONTENT, ViewGroup.LayoutParams.WRAP_CONTENT));

            mView.getHolder().addCallback(xCallback);

            getWindow().addFlags(WindowManager_LayoutParams.FLAG_KEEP_SCREEN_ON);

            appThread = com.oculus.gles3jni.GLES3JNILib.onCreate(this);

            Console.WriteLine("after OVRVrCubeWorldSurfaceViewX onCreate, attach the headset!");

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
            appThread.onStart();
        }

        protected override void onResume()
        {
            base.onResume();
            appThread.onResume();
        }

        protected override void onPause()
        {
            appThread.onPause();
            base.onPause();
        }

        protected override void onStop()
        {
            appThread.onStop();
            base.onStop();
        }

        protected override void onDestroy()
        {
            if (mSurfaceHolder != null)
            {
                appThread.onSurfaceDestroyed();
            }
            appThread.onDestroy();
            base.onDestroy();
            appThread = 0;
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
