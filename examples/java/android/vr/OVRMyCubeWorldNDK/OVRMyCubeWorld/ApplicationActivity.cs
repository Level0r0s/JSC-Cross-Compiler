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

namespace OVRMyCubeWorld.Activities
{
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]

    // https://forums.oculus.com/viewtopic.php?t=21409
    // dual shows our own popup and inserting in gearvr stays black.
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "com.samsung.android.vr.application.mode", value = "dual")]

    // what if we want to display our own welcome screen?
    // com.samsung.android.hmt.vrsvc/com.samsung.android.hmt.vrsvc.WaitActivity
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "com.samsung.android.vr.application.mode", value = "vr_only")]
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

        // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect 192.168.1.126:5555
        // x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "xNativeActivity" "System.Console" "DEBUG"
        // x:\util\android-sdk-windows\platform-tools\adb.exe shell am force-stop OVRMyCubeWorld.Activities
        // x:\util\android-sdk-windows\platform-tools\adb.exe shell am start -n OVRMyCubeWorld.Activities/OVRMyCubeWorld.Activities.ApplicationActivity
        // https://code.google.com/p/android/issues/detail?can=2&start=0&num=100&q=&colspec=ID%20Type%20Status%20Owner%20Summary%20Stars&groupby=&sort=&id=75442

        private SurfaceHolder xSurfaceHolder;
        private SurfaceView xSurfaceView;


        private ovrAppThreadPointer appThread;


        public class xSurfaceHolder_Callback : SurfaceHolder_Callback
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


        class __args
        {
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeAppWindowUDPPointerLock\ChromeAppWindowUDPPointerLock\Application.cs
            // flatland is sending input to vrland?
            public int mousex, mousey;

            public int x, y, z, w;

            //tracking.HeadPose.Pose.Orientation.x

            // these are to be streamed over udp back to flatland
            public float tracking_HeadPose_Pose_Orientation_x;
            public float tracking_HeadPose_Pose_Orientation_y;
            public float tracking_HeadPose_Pose_Orientation_z;
            public float tracking_HeadPose_Pose_Orientation_w;
        }

        __args args = new __args();

        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            Console.WriteLine("enter OVRMyCubeWorld onCreate");




            #region xCallback
            // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceView\ApplicationActivity.cs
            var xCallback = new xSurfaceHolder_Callback
            {
                onsurfaceCreated = holder =>
                {
                    //Console.WriteLine("enter onsurfaceCreated " + new { appThread });
                    if (appThread == 0)
                        return;

                    // did we use it for float window?
                    //holder.setFormat(android.graphics.PixelFormat.TRANSLUCENT);

                    GLES3JNILib.onSurfaceCreated(holder.getSurface());
                    xSurfaceHolder = holder;

                    //Console.WriteLine("exit onsurfaceCreated " + new { appThread });
                },

                onsurfaceChanged = (SurfaceHolder holder, int format, int width, int height) =>
                {
                    if (appThread == 0)
                        return;

                    GLES3JNILib.onSurfaceChanged(holder.getSurface());
                    xSurfaceHolder = holder;
                },

                onsurfaceDestroyed = holder =>
                {
                    //I/System.Console( 3549): 0ddd:0001 after OVRMyCubeWorld onCreate, attach the headset!
                    //I/System.Console( 3549): 0ddd:0001 enter onsurfaceDestroyed

                    Console.WriteLine("enter onsurfaceDestroyed");


                    if (appThread == 0)
                        return;


                    // I/DEBUG   ( 2079):     #01 pc 0000672f  /data/app/OVRMyCubeWorld.Activities-1/lib/arm/libmain.so (Java_com_oculus_gles3jni_GLES3JNILib_onSurfaceDestroyed+46)
                    GLES3JNILib.onSurfaceDestroyed();
                    xSurfaceHolder = null;
                    appThread = 0;
                }
            };
            #endregion

            // https://github.com/dalinaum/TextureViewDemo
            //  TextureView semi-translucent by calling myView.setAlpha(0.5f).
            // !! should we use TextureView instead?
            // https://groups.google.com/forum/#!topic/android-developers/jYjvm7ItpXQ
            this.xSurfaceView = new SurfaceView(this);
            //this.xSurfaceView.setZOrderOnTop(true);    // necessary
            //this.xSurfaceView.getHolder().setFormat(android.graphics.PixelFormat.TRANSPARENT);


            var sw = Stopwatch.StartNew();

            //var args = new object();

            // can we draw on back?

            #region mDraw
            var mDraw = new DrawOnTop(this)
            {
                // yes it appears top left.

                //text = "GearVR HUD"
                // (out) VrApi.vrapi_GetVersionString()
                text = () =>
                {
                    // can we listen to udp?
                    // like X:\jsc.svn\examples\java\android\AndroidServiceUDPNotification\AndroidServiceUDPNotification\ApplicationActivity.cs
                    // in vr if the other service is running it can display vr notification

                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150630/udp
                    // lets run it, and see if we can see some vr notifications as we skip a video 

                    GLES3JNILib.stringFromJNI(args);



                    return sw.ElapsedMilliseconds + "ms "
                        + new
                        {
                            // left to right
                            args.x,

                            // nod up +0.7 down -0.7
                            //ox = args.tracking_HeadPose_Pose_Orientation_x 

                            // -0.7 right +0.7 left
                            oy = args.tracking_HeadPose_Pose_Orientation_y

                            // tilt right -0.7 tilt left + 0.7
                            //oz = args.tracking_HeadPose_Pose_Orientation_z

                            // ??
                            //ow = args.tracking_HeadPose_Pose_Orientation_w
                        };
                }
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

                    // touch sending int to offfset the cubes
                    this.args.x = (int)(halfx - x);
                    this.args.y = (int)(y - halfy);

                    mDraw.x = (int)(500 + halfx - x);
                    mDraw.y = (int)(600 + y - halfy);
                    //mDraw.text = () => sw.ElapsedMilliseconds + "ms \n" + new { x, y, action }.ToString();
                    //Console.WriteLine(" ::dispatchTouchEvent( " + action + ", " + x + ", " + y + " )");
                }
                GLES3JNILib.onTouchEvent(action, x, y);

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
                GLES3JNILib.onKeyEvent(keyCode, action);

                return true;
            };
            #endregion

            this.setContentView(xSurfaceView);
            this.addContentView(mDraw, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WRAP_CONTENT, ViewGroup.LayoutParams.WRAP_CONTENT));
            // canw e add a camera too?

            //  stackoverflow.com/questions/20936480/how-to-make-surfaceview-transparent-background
            //this.setContentView(mDraw);
            //this.addContentView(xSurfaceView, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WRAP_CONTENT, ViewGroup.LayoutParams.WRAP_CONTENT));


            xSurfaceView.getHolder().addCallback(xCallback);

            //getWindow().addFlags(WindowManager_LayoutParams.FLAG_KEEP_SCREEN_ON);

            appThread = com.oculus.gles3jni.GLES3JNILib.onCreate(this);

            Console.WriteLine("after OVRMyCubeWorld onCreate, attach the headset!");

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
                //paint.setColor(android.graphics.Color.RED);
                //paint.setColor(android.graphics.Color.YELLOW);
                paint.setColor(android.graphics.Color.GREEN);
                paint.setTextSize(textSize);
                paint.setAlpha(127);

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
            GLES3JNILib.onStart();
        }

        protected override void onResume()
        {
            base.onResume();
            GLES3JNILib.onResume();
        }

        protected override void onPause()
        {
            GLES3JNILib.onPause();
            base.onPause();
        }

        protected override void onStop()
        {
            GLES3JNILib.onStop();
            base.onStop();
        }

        protected override void onDestroy()
        {
            if (xSurfaceHolder != null)
            {
                GLES3JNILib.onSurfaceDestroyed();
            }
            GLES3JNILib.onDestroy();
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


//42d0:02:01:1f ActiveJavaArchiveNativesAssemblies load X:\jsc.svn\examples\java\android\vr\OVRMyCubeWorldNDK\OVRMyCubeWorld\bin\staging.AssetsLibrary\output.jar\VrApi.dll
//System.TypeLoadException: Could not load type 'FrameCallback' from assembly 'ScriptCoreLibAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.
//System.TypeLoadException: Could not load type 'android.telephony.PhoneStateListener' from assembly 'ScriptCoreLibAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.
//System.Reflection.ReflectionTypeLoadException: Unable to load one or more of the requested types. Retrieve the LoaderExceptions property for more information.