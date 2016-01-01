using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;
using com.oculus.gles3jni;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using android.hardware;
using System.IO;
using android.net.wifi;
using android.content;

namespace OVRWindWheelActivity.Activities
{
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "18")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]

    // https://forums.oculus.com/viewtopic.php?t=21409
    // dual shows our own popup and inserting in gearvr stays black. howcom?
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "com.samsung.android.vr.application.mode", value = "dual")]

    // what if we want to display our own welcome screen?
    // com.samsung.android.hmt.vrsvc/com.samsung.android.hmt.vrsvc.WaitActivity
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "com.samsung.android.vr.application.mode", value = "vr_only")]
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704/pui_global_menu
    public class LocalApplication : Application
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160101/ovrwindwheelndk

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151002/udppenpressure
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151001/udppenpressure

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150703

        public override void onCreate()
        {
            base.onCreate();

            // where is the source for it?
            //  <Content Include="x:\opensource\ovr_mobile_sdk_0.6.0\VrApi\Libs\Android\VrApi.jar">
            // com/oculus/vrapi/VrApi
            //var api = typeof(com.oculus.vrapi.VrApi);

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
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150721/ovroculus360photoshud
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150711
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  tcpip 5555


        // should jsc remember last connected device and reconnect if disconnected?
        // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect 192.168.1.126:5555
        // restart android?
        // restart usb debugging?


        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell dumpsys SurfaceFlinger
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell dumpsys battery

        // x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "xNativeActivity" "System.Console" "DEBUG"
        // x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "xNativeActivity" "System.Console" "DEBUG" "PlatformActivity"
        // x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "xNativeActivity" "System.Console" "DEBUG" "PlatformActivity" "AndroidRuntime"


        // x:\util\android-sdk-windows\platform-tools\adb.exe shell am force-stop OVRWindWheelActivity.Activities
        // x:\util\android-sdk-windows\platform-tools\adb.exe shell am start -n OVRWindWheelActivity.Activities/OVRWindWheelActivity.Activities.ApplicationActivity
        // Warning: Activity not started, its current task has been brought to the front



        // x:\util\android-sdk-windows\platform-tools\adb.exe  shell dumpsys meminfo OVRWindWheelActivity.Activities


        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell screenrecord --bit-rate 6000000 "/sdcard/oculus/Movies/My Videos/3D/OVRMyCubeWorldNDK-WASDC-mousewheel.mp4"
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell screenrecord --bit-rate 6000000 "/sdcard/oculus/Movies/My Videos/3D/OVRWindWheelActivity-fakepush.mp4"


        // sometimes the vrsvc fails to init and shows black screen?


        //-----------+----------+------+------+----+------+-------------+--------------------------------+------------------------+------------------------+------
        //       HWC | 7f9d73c0f0 | 0000 | 0000 | 00 | 0100 | RGBA_8888   |    0.0,    0.0, 1440.0, 2560.0 |    0,    0, 1440, 2560 |    0,    0, 1440, 2560 | com.samsung.android.hmt.vrsvc/com.samsung.android.hmt.vrsvc.HomeCrashGuideActivity
        // FB TARGET | 7faf04e860 | 0000 | 0000 | 00 | 0105 | RGBA_8888   |    0.0,    0.0, 1440.0, 2560.0 |    0,    0, 1440, 2560 |    0,    0,    0,    0 | HWC_FRAMEBUFFER_TARGET



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

            public string mouse;

            public int mousex, mousey, ws, ad, c, mousebutton, mousewheel;

            public int x, y, z, w;

            //tracking.HeadPose.Pose.Orientation.x

            // these are to be streamed over udp back to flatland
            public float tracking_HeadPose_Pose_Orientation_x;
            public float tracking_HeadPose_Pose_Orientation_y;
            public float tracking_HeadPose_Pose_Orientation_z;
            public float tracking_HeadPose_Pose_Orientation_w;


            // our VR thread will stop at 64MB
            public long total_allocated_space;

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151001/udppenpressure
            public string pen;

            public string parallax;
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150712-1
            public float px;
            public float py;
            public float pz;



            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150718/udpmatrix
            // red sends via udp, java sends to ndk, then to shader...
            public byte[] vertexTransform = new byte[0];
            //public byte[] vertexTransform = null;
        }

        __args args = new __args();

        android.hardware.Camera nogc;


        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            Console.WriteLine("enter OVRWindWheelActivity onCreate");

            // http://www.mkyong.com/android/how-to-turn-onoff-camera-ledflashlight-in-android/




            #region xCallback
            // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceView\ApplicationActivity.cs
            var xCallback = new xSurfaceHolder_Callback
            {
                onsurfaceCreated = holder =>
                {
                    Console.WriteLine("enter onsurfaceCreated " + new { appThread });
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
                    //I/System.Console( 3549): 0ddd:0001 after OVRWindWheelActivity onCreate, attach the headset!
                    //I/System.Console( 3549): 0ddd:0001 enter onsurfaceDestroyed

                    //Console.WriteLine("enter onsurfaceDestroyed");


                    if (appThread == 0)
                        return;


                    // I/DEBUG   ( 2079):     #01 pc 0000672f  /data/app/OVRWindWheelActivity.Activities-1/lib/arm/libmain.so (Java_com_oculus_gles3jni_GLES3JNILib_onSurfaceDestroyed+46)
                    GLES3JNILib.onSurfaceDestroyed();
                    xSurfaceHolder = null;
                    //appThread = 0;

                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704/pui_global_menu
                }
            };
            #endregion

            // https://github.com/dalinaum/TextureViewDemo
            //  TextureView semi-translucent by calling myView.setAlpha(0.5f).
            // !! should we use TextureView instead?
            // https://groups.google.com/forum/#!topic/android-developers/jYjvm7ItpXQ
            //this.xSurfaceView.setZOrderOnTop(true);    // necessary
            //this.xSurfaceView.getHolder().setFormat(android.graphics.PixelFormat.TRANSPARENT);

            var ActivityPaused = true;





            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160101/ovrwindwheelndk
            WifiManager wifi = (WifiManager)this.getSystemService(Context.WIFI_SERVICE);
            var lo = wifi.createMulticastLock("vrudp");
            lo.acquire();

            #region ReceiveAsync
            // https://www.youtube.com/watch?v=GpmKq_qg3Tk




            var HUDStylusList = new List<Action<android.graphics.Canvas>>();

            // http://uploadvr.com/vr-hmd-specs/

            Action<android.graphics.Canvas> HUDStylus = canvas =>
            {
                // so cool. we get to use pen in vr!s
                while (HUDStylusList.Count > 1024)
                    HUDStylusList.RemoveAt(0);


                foreach (var item in HUDStylusList)
                {
                    item(canvas);

                }
            };

            #region fUDPPressure
            Action<IPAddress> fUDPPressure = async nic =>
            {
                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151003/ovrwindwheelactivity
                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150712-1
                var uu = new UdpClient(40094);

                // X:\jsc.svn\examples\javascript\chrome\apps\ChromeFlashlightTracker\ChromeFlashlightTracker\Application.cs
                //args.pre = "awaiting Parallax at " + nic + " :40094";

                var oldx = 0f;
                var oldy = 0f;

                // X:\jsc.svn\examples\java\android\forms\FormsUDPJoinGroup\FormsUDPJoinGroup\ApplicationControl.cs
                // X:\jsc.svn\examples\java\android\LANBroadcastListener\LANBroadcastListener\ApplicationActivity.cs
                uu.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"), nic);
                while (true)
                {
                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151001/udppenpressure
                    // did we break async Continue ??
                    var ux = await uu.ReceiveAsync(); // did we jump to ui thread?


                    // discard input?
                    if (ActivityPaused)
                        continue;

                    // while we have the signal turn on torch/.


                    var m = new BinaryReader(new MemoryStream(ux.Buffer));

                    var x0 = m.ReadSingle();

                    var x = 200 + x0 * 0.1f;

                    var y0 = m.ReadSingle();

                    var y = 1200 - y0 * 0.1f;

                    var pressure = m.ReadSingle();


                    new { x, y, oldx, oldy, pressure }.With(
                        segment =>
                        {
                            var paint = new android.graphics.Paint();

                            HUDStylusList.Add(
                                canvas =>
                                {
                                    //c.lineTo((int)(x * 0.1), 400 - (int)(y * 0.1));

                                    //c.lineWidth = 1 + (pressure / 255.0 * 7);
                                    // 
                                    paint.setStrokeWidth((int)(1 + (pressure / 255.0 * 6) * (pressure / 255.0 * 6)));

                                    paint.setStyle(android.graphics.Paint.Style.STROKE);

                                    if (pressure > 0)
                                        paint.setColor(android.graphics.Color.YELLOW);
                                    else
                                        paint.setColor(android.graphics.Color.RED);

                                    canvas.drawLine(segment.x, segment.y, segment.oldx, segment.oldy, paint);

                                    canvas.drawLine(2560 / 2 + segment.x, segment.y, segment.oldx + 2560 / 2, segment.oldy, paint);
                                }

                            );
                        }
                    );


                    oldx = x;
                    oldy = y;

                    args.pen = new { x, y, pressure }.ToString();

                    //Console.WriteLine(new { args.parallax });

                    //// or marshal memory?
                    //var xy = args.mouse.Split(':');


                    //args.mousey = int.Parse(xy[1]);

                    //// getchar?
                    //args.ad = int.Parse(xy[2]);
                    //args.ws = int.Parse(xy[3]);

                    //// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704
                    //args.c = int.Parse(xy[4]);
                    //// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704/mousedown
                    //args.mousebutton = int.Parse(xy[5]);
                    //args.mousewheel = int.Parse(xy[6]);
                }
            };
            #endregion


            #region fParallax
            Action<IPAddress> fParallax = async nic =>
            {
                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150712-1
                var uu = new UdpClient(43834);

                // X:\jsc.svn\examples\javascript\chrome\apps\ChromeFlashlightTracker\ChromeFlashlightTracker\Application.cs
                args.parallax = "awaiting Parallax at " + nic + " :43834";

                // X:\jsc.svn\examples\java\android\forms\FormsUDPJoinGroup\FormsUDPJoinGroup\ApplicationControl.cs
                // X:\jsc.svn\examples\java\android\LANBroadcastListener\LANBroadcastListener\ApplicationActivity.cs
                uu.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"), nic);
                while (true)
                {
                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151001/udppenpressure
                    // did we break async Continue ??
                    var x = await uu.ReceiveAsync(); // did we jump to ui thread?


                    // discard input?
                    if (ActivityPaused)
                        continue;

                    // while we have the signal turn on torch/.

                    #region await webcam feed
                    if (nogc == null)
                    {

                        // partial ?
                        var camera = android.hardware.Camera.open();
                        android.hardware.Camera.Parameters p = camera.getParameters();
                        p.setFlashMode(android.hardware.Camera.Parameters.FLASH_MODE_TORCH);
                        camera.setParameters(p);
                        camera.startPreview();

                        nogc = camera;
                    }
                    #endregion


                    //Console.WriteLine("ReceiveAsync done " + Encoding.UTF8.GetString(x.Buffer));
                    args.parallax = Encoding.UTF8.GetString(x.Buffer);

                    var xy = args.parallax.Split(':');

                    //Console.WriteLine(new { args.parallax });

                    //// or marshal memory?
                    //var xy = args.mouse.Split(':');

                    args.px = float.Parse(xy[1]);
                    args.py = float.Parse(xy[2]);
                    args.pz = float.Parse(xy[3]);

                    //args.mousey = int.Parse(xy[1]);

                    //// getchar?
                    //args.ad = int.Parse(xy[2]);
                    //args.ws = int.Parse(xy[3]);

                    //// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704
                    //args.c = int.Parse(xy[4]);
                    //// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704/mousedown
                    //args.mousebutton = int.Parse(xy[5]);
                    //args.mousewheel = int.Parse(xy[6]);
                }
            };
            #endregion

            #region fWASDC
            var fWASDCport = 41814;
            Action<IPAddress> fWASDC = async nic =>
             {
                 var uu = new UdpClient(fWASDCport);

                 args.mouse = "awaiting mouse and WASDC at " + nic + ":" + fWASDCport;

                 // X:\jsc.svn\examples\java\android\forms\FormsUDPJoinGroup\FormsUDPJoinGroup\ApplicationControl.cs
                 // X:\jsc.svn\examples\java\android\LANBroadcastListener\LANBroadcastListener\ApplicationActivity.cs
                 uu.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"), nic);
                 while (true)
                 {
                     var x = await uu.ReceiveAsync(); // did we jump to ui thread?
                     //Console.WriteLine("ReceiveAsync done " + Encoding.UTF8.GetString(x.Buffer));
                     args.mouse = Encoding.UTF8.GetString(x.Buffer);

                     // or marshal memory?
                     var xy = args.mouse.Split(':');

                     args.mousex = int.Parse(xy[0]);
                     args.mousey = int.Parse(xy[1]);

                     // getchar?
                     args.ad = int.Parse(xy[2]);
                     args.ws = int.Parse(xy[3]);

                     // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704
                     args.c = int.Parse(xy[4]);
                     // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704/mousedown
                     args.mousebutton = int.Parse(xy[5]);
                     args.mousewheel = int.Parse(xy[6]);
                 }
             };
            #endregion



            #region fvertexTransform
            // X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRUDPMatrix\Program.cs
            Action<IPAddress> fvertexTransform = async nic =>
            {
                var uu = new UdpClient(40014);

                //args.mouse = "awaiting vertexTransform at " + nic + " :40014";

                // X:\jsc.svn\examples\java\android\forms\FormsUDPJoinGroup\FormsUDPJoinGroup\ApplicationControl.cs
                // X:\jsc.svn\examples\java\android\LANBroadcastListener\LANBroadcastListener\ApplicationActivity.cs
                uu.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"), nic);
                while (true)
                {
                    var x = await uu.ReceiveAsync(); // did we jump to ui thread?
                    //Console.WriteLine("ReceiveAsync done " + Encoding.UTF8.GetString(x.Buffer));
                    args.vertexTransform = x.Buffer;


                }
            };
            #endregion


            NetworkInterface.GetAllNetworkInterfaces().WithEach(
                n =>
                {
                    // X:\jsc.svn\examples\java\android\forms\FormsUDPJoinGroup\FormsUDPJoinGroup\ApplicationControl.cs
                    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\NetworkInformation\NetworkInterface.cs

                    var IPProperties = n.GetIPProperties();
                    var PhysicalAddress = n.GetPhysicalAddress();



                    foreach (var ip in IPProperties.UnicastAddresses)
                    {
                        // ipv4
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            if (!IPAddress.IsLoopback(ip.Address))
                                if (n.SupportsMulticast)
                                {
                                    fUDPPressure(ip.Address);
                                    fWASDC(ip.Address);
                                    fParallax(ip.Address);
                                    fvertexTransform(ip.Address);
                                }
                        }
                    }




                }
            );
            #endregion


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



                    //if (args.total_allocated_space > 48 * 1024 * 1024)
                    //    this.recreate();


                    return
                        sw.ElapsedMilliseconds + "ms | " + args.total_allocated_space + " bytes \n"
                        + new { vertexTransform = args.vertexTransform.Length } + "\n"
                        + args.mouse + "\n"
                        + args.parallax + "\n"
                        + args.vertexTransform.Length + "bytes udp\n"
                        + new { args.pen } + "\n"
                        //+ new { args.mousex, args.mousey } + "\n"
                        + new
                        {
                            //args.mousex,

                            // left to right
                            //args.x,
                            //args.px,

                            args.px,
                            args.py,
                            args.pz,


                            // nod up +0.7 down -0.7
                            ox = args.tracking_HeadPose_Pose_Orientation_x,

                            // -0.7 right +0.7 left
                            oy = args.tracking_HeadPose_Pose_Orientation_y

                            // tilt right -0.7 tilt left + 0.7
                            //oz = args.tracking_HeadPose_Pose_Orientation_z

                            // ??
                            //ow = args.tracking_HeadPose_Pose_Orientation_w
                        }.ToString().Replace(",", "\n");
                }
            };

            //Task.Run(


            Func<string> safemode = () =>
            {
                return
                    sw.ElapsedMilliseconds + "ms \n"
                        + args.total_allocated_space + " bytes \n"
                    + "GC safe mode / malloc limit..";
            };




            //    canvas.drawText(text, x + 2560 / 2, y + i * 24, paint);
            mDraw.AtDraw = canvas =>
            {


                {
                    var paint = new android.graphics.Paint();


                    paint.setStrokeWidth(16);
                    paint.setStyle(android.graphics.Paint.Style.STROKE);

                    paint.setColor(android.graphics.Color.RED);

                    canvas.drawLine(0, 0, 400, 400, paint);

                    canvas.drawLine(2560 / 2, 0, 400 + 2560 / 2, 400, paint);


                    HUDStylus(canvas);
                }


                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150717/replay
                // can w visually store tracking intel. like tvs do.
                {
                    // https://code.google.com/p/android/issues/detail?id=4086

                    var paint = new android.graphics.Paint();


                    paint.setStrokeWidth(0);
                    paint.setStyle(android.graphics.Paint.Style.FILL_AND_STROKE);

                    // lets have left to right recorder as a color block

                    //// nod up +0.7 down -0.7
                    // cannot see it.
                    var rgb_left_to_right = (int)(0xffffff * (args.tracking_HeadPose_Pose_Orientation_x + 0.7) / 1.4);



                    // I/System.Console( 8999): 2327:0001 AtDraw 16 0078af2e
                    // why wont our tracking correctly show?
                    //Console.WriteLine("AtDraw 16 " + rgb_left_to_right.ToString("x8"));

                    //paint.setColor(android.graphics.Color.YELLOW);
                    paint.setColor(
                        (int)(0xff000000 | rgb_left_to_right));


                    canvas.drawRect(16, 0, 32, 32, paint);
                }

                //       ox = args.tracking_HeadPose_Pose_Orientation_x,

                //       oy = args.tracking_HeadPose_Pose_Orientation_y

                {
                    // https://code.google.com/p/android/issues/detail?id=4086

                    var paint = new android.graphics.Paint();


                    paint.setStrokeWidth(0);
                    paint.setStyle(android.graphics.Paint.Style.FILL_AND_STROKE);
                    //paint.setColor(android.graphics.Color.RED);

                    // lets have left to right recorder as a color block

                    //       // -0.7 right +0.7 left
                    var rgb_left_to_right = (int)(0xffffff * (args.tracking_HeadPose_Pose_Orientation_y + 0.7) / 1.4);

                    //paint.setColor(android.graphics.Color.YELLOW);
                    paint.setColor(
                        (int)(0xff000000 | rgb_left_to_right));

                    canvas.drawRect(16 + 64, 0, 320, 32, paint);
                }
            };

            new Thread(
                delegate()
                {
                    // bg thread

                    while (true)
                    {
                        //Thread.Sleep(1000 / 15);
                        //Thread.Sleep(1000 / 30);

                        // fullspeed



                        GLES3JNILib.stringFromJNI(args);

                        // http://developer.android.com/reference/android/graphics/Color.html
                        if (args.total_allocated_space > GLES3JNILib.safemodeMemoryLimitMB * 1024 * 1024)
                        {
                            mDraw.color = android.graphics.Color.RED;
                            mDraw.alpha = 255;

                            mDraw.text = safemode;
                            // goto secondary activity?
                        }
                        else if (args.mousebutton != 0)
                        {
                            // go a head. lean left or up
                            mDraw.color = android.graphics.Color.YELLOW;
                            mDraw.alpha = 255;
                        }
                        else
                        {
                            mDraw.color = android.graphics.Color.GREEN;

                            // not leaning in?
                            if (args.pz < 0)
                            {
                                mDraw.color = android.graphics.Color.WHITE;
                            }

                            var BaseStationEdgeX = Math.Abs(args.px) > 0.3;
                            var BaseStationEdgeY = Math.Abs(args.py) > 0.3;

                            if (BaseStationEdgeX
                                || BaseStationEdgeY
                                )
                            {
                                // base station wont track ya for long..
                                // reorient?
                                // fade to black?
                                mDraw.color = android.graphics.Color.YELLOW;
                                mDraw.alpha = 255;

                            }
                        }

                        mDraw.postInvalidate();

                        Thread.Sleep(1000 / 60);

                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150716/ovrwindwheelactivity
                        //Thread.Sleep(1000 / 15);
                        //Thread.Sleep(1000 / 4);
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
                    //mDraw.text = () => sw.ElapsedMilliseconds + "ms \n" + new { keyCode, action }.ToString();
                    //Log.v(TAG, "GLES3JNIActivity::dispatchKeyEvent( " + keyCode + ", " + action + " )");
                }
                GLES3JNILib.onKeyEvent(keyCode, action);

                return true;
            };
            #endregion


            AtPause = delegate
            {
                ActivityPaused = true;
                GLES3JNILib.onPause();


                // http://www.mkyong.com/android/how-to-turn-onoff-camera-ledflashlight-in-android/

                if (nogc != null)
                {
                    var camera = nogc;
                    var p = camera.getParameters();
                    p.setFlashMode(android.hardware.Camera.Parameters.FLASH_MODE_OFF);
                    camera.setParameters(p);
                    camera.stopPreview();

                    camera.release();
                    nogc = null;
                }
            };

            AtResume = delegate
            {
                //Console.WriteLine("enter onResume");
                ActivityPaused = false;

                // http://stackoverflow.com/questions/3527621/how-to-pause-and-resume-a-surfaceview-thread
                // http://stackoverflow.com/questions/10277694/resume-to-surfaceview-shows-black-screen
                //this.xSurfaceView.onres

                // You must ensure that the drawing thread only touches the underlying Surface while it is valid

                this.xSurfaceView = new SurfaceView(this);

                this.setContentView(xSurfaceView);
                this.addContentView(mDraw, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WRAP_CONTENT, ViewGroup.LayoutParams.WRAP_CONTENT));

                this.xSurfaceView.getHolder().addCallback(xCallback);

                GLES3JNILib.onResume();
            };

            // canw e add a camera too?

            //  stackoverflow.com/questions/20936480/how-to-make-surfaceview-transparent-background
            //this.setContentView(mDraw);
            //this.addContentView(xSurfaceView, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WRAP_CONTENT, ViewGroup.LayoutParams.WRAP_CONTENT));


            // sometimes system wants to try to black the screen it seems..
            getWindow().addFlags(WindowManager_LayoutParams.FLAG_KEEP_SCREEN_ON);

            appThread = com.oculus.gles3jni.GLES3JNILib.onCreate(this);

            Console.WriteLine("after OVRWindWheelActivity onCreate, attach the headset!");

        }


        public class DrawOnTop : View
        {
            // html hud?

            // http://stackoverflow.com/questions/6927286/force-a-view-to-redraw-itself

            public DrawOnTop(android.content.Context context)
                : base(context)
            {
            }

            // udp mouse ?
            public int x = 200; // animate it?
            public int y = 900;

            public Func<string> text = () => "hello!";

            public int textSize = 25;

            public int color = android.graphics.Color.GREEN;
            public int alpha = 80;



            public Action<android.graphics.Canvas> AtDraw;

            protected override void onDraw(android.graphics.Canvas canvas)
            {
                {
                    var paint = new android.graphics.Paint();

                    paint.setStyle(android.graphics.Paint.Style.STROKE);
                    //paint.setStyle(android.graphics.Paint.Style.FILL_AND_STROKE);
                    //paint.setColor(android.graphics.Color.RED);
                    //paint.setColor(android.graphics.Color.YELLOW);
                    paint.setColor(color);
                    paint.setTextSize(textSize);
                    paint.setAlpha(alpha);

                    var a = this.text().Split('\n');

                    a.WithEachIndex(
                        (text, i) =>
                        {

                            canvas.drawText(text, x, y + i * 24, paint);
                            canvas.drawText(text, x + 2560 / 2, y + i * 24, paint);
                        }
                    );
                }

                if (AtDraw != null)
                    AtDraw(canvas);


                base.onDraw(canvas);
            }
        }



        // why we need this?
        protected override void onNewIntent(android.content.Intent value)
        {
            base.onNewIntent(value);
            var DataString = value.getDataString();
            Console.WriteLine("exit onNewIntent " + new { DataString });

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704/pui_global_menu
        }



        Action AtResume;
        protected override void onResume()
        {
            base.onResume();


            AtResume();

        }

        #region Activity life cycle
        protected override void onStart()
        {
            base.onStart();
            GLES3JNILib.onStart();
        }


        Action AtPause;
        protected override void onPause()
        {
            AtPause();

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
            this.appThread = 0;
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


//42d0:02:01:1f ActiveJavaArchiveNativesAssemblies load X:\jsc.svn\examples\java\android\vr\OVRMyCubeWorldNDK\OVRWindWheelActivity\bin\staging.AssetsLibrary\output.jar\VrApi.dll
//System.TypeLoadException: Could not load type 'FrameCallback' from assembly 'ScriptCoreLibAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.
//System.TypeLoadException: Could not load type 'android.telephony.PhoneStateListener' from assembly 'ScriptCoreLibAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.
//System.Reflection.ReflectionTypeLoadException: Unable to load one or more of the requested types. Retrieve the LoaderExceptions property for more information.

//I/xNativeActivity( 9944): [17763920] \VrCubeWorld.Renderer.cs:556 ovrRenderer_RenderFrame, vertexTransform UDPMatrixIndex:  2
//I/xNativeActivity( 9944): [17994528] \VrCubeWorld.App.cs:154 ovrApp_HandleVrModeChanges, vrapi_LeaveVrMode
//I/xNativeActivity( 9944): [17466440] \VrCubeWorld.App.cs:165 ovrApp_HandleVrModeChanges, ovrEgl_DestroySurface
//I/xNativeActivity( 9944): [17109736] \VrCubeWorld.App.cs:118 ovrApp_HandleVrModeChanges, ovrEgl_CreateSurface
//I/xNativeActivity( 9944): [17109736] \VrCubeWorld.Egl.cs:233 enter ovrEgl_CreateSurface
//I/xNativeActivity( 9944): [17109736] \VrCubeWorld.Egl.cs:242 ovrEgl_CreateSurface, eglCreateWindowSurface, eglMakeCurrent
//I/DEBUG   (29301): *** *** *** *** *** *** *** *** *** *** *** *** *** *** *** ***
//I/DEBUG   (29301): Build fingerprint: 'samsung/zeroltexx/zerolte:5.0.2/LRX22G/G925FXXU1AOE3:user/release-keys'
//I/DEBUG   (29301): Revision: '10'
//I/DEBUG   (29301): ABI: 'arm'
//I/DEBUG   (29301): pid: 9944, tid: 10074, name: Thread-1519  >>> OVRWindWheelActivity.Activities <<<
//I/DEBUG   (29301): signal 11 (SIGSEGV), code 1 (SEGV_MAPERR), fault addr 0x1c7
//I/DEBUG   (29301):     r0 e17fedb4  r1 00000000  r2 00000001  r3 f6e853b0
//I/DEBUG   (29301):     r4 0000016f  r5 e16dc194  r6 00000001  r7 e17ff658
//I/DEBUG   (29301):     r8 e17fedb4  r9 00000000  sl 00000000  fp e959b280
//I/DEBUG   (29301):     ip f6e7cd8c  sp e17feda0  lr f6e2b9b5  pc f6e2e690  cpsr a00f0030
//I/DEBUG   (29301):
//I/DEBUG   (29301): backtrace:
//I/DEBUG   (29301):     #00 pc 00012690  /system/lib/libEGL.so (eglCreateWindowSurface+47)
//I/DEBUG   (29301):     #01 pc 00008e7d  /data/app/OVRWindWheelActivity.Activities-1/lib/arm/libmain.so (OVRWindWheelNDK_VrCubeWorld_ovrEgl_ovrEgl_CreateSurface+56)
//I/DEBUG   (29301):     #02 pc 00009515  /data/app/OVRWindWheelActivity.Activities-1/lib/arm/libmain.so (OVRWindWheelNDK_VrCubeWorld_ovrApp_ovrApp_HandleVrModeChanges+40)
//I/DEBUG   (29301):     #03 pc 0000cdd9  /data/app/OVRWindWheelActivity.Activities-1/lib/arm/libmain.so (OVRWindWheelNDK_VrCubeWorld_ovrAppThread_AppThreadFunction+600)
//I/DEBUG   (29301):     #04 pc 0000b613  /data/app/OVRWindWheelActivity.Activities-1/lib/arm/libmain.so (ScriptCoreLib_Shared_BCLImplementation_System_Threading___ThreadStart_Invoke+26)
//I/DEBUG   (29301):     #05 pc 0000b61b  /data/app/OVRWindWheelActivity.Activities-1/lib/arm/libmain.so (ScriptCoreLibAndroidNDK_BCLImplementation_System_Threading___Thread_AppThreadFunction+4)
//I/DEBUG   (29301):     #06 pc 00016bbb  /system/lib/libc.so (__pthread_start(void*)+30)
//I/DEBUG   (29301):     #07 pc 00014c83  /system/lib/libc.so (__start_thread+6)
//I/DEBUG   (29301):
//I/DEBUG   (29301): Tombstone written to: /data/tombstones/tombstone_07