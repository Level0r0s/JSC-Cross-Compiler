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
using ScriptCoreLib.Extensions;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using System.Threading.Tasks;
using ScriptCoreLibJava.BCLImplementation.System.Net.Sockets;


namespace OVROculus360Photos.Activities
{
    static class xMarshal
    {
        // called by?

        [Script(IsPInvoke = true)]
        //private long find(string lib, string fname) { return default(long); }
        public static string stringFromJNI(object args) { return default(string); }

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

    //enter AndroidLauncher { AndroidPayload =  }
    //ADB server didn't ACK
    //* failed to start daemon *
    //error: cannot connect to daemon



    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704/ovroculus360photoshud
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150613/record
    // "x:\util\android-sdk-windows\platform-tools\adb.exe" shell am start -n OVROculus360PhotosHUD.Activities/OVROculus360PhotosHUD.Activities.ApplicationActivity



    // "X:\opensource\ovr_mobile_sdk_0.6.0\VrSamples\Native\Oculus360PhotosSDK\src\com\oculus\oculus360photossdk\MainActivity.java"

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]

    // https://forums.oculus.com/viewtopic.php?t=21409
    // vr_dual wont run on gearvr for now...
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "com.samsung.android.vr.application.mode", value = "dual")]
    // for dual we could switch to chrome view activity
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150721/ovroculus360photoshud
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160101/ovroculus360photosndk

    // vr_only means we wont see our activity ui
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "com.samsung.android.vr.application.mode", value = "vr_only")]
    //<meta-data android:name="com.samsung.android.vr.application.mode" android:value="vr_only"/>
    public class LocalApplication : Application
    {
        // "x:\util\android-sdk-windows\platform-tools\adb.exe" devices
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  tcpip 5555
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell netcfg

        // should jsc remember last connected device and reconnect if disconnected?
        // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect 192.168.1.126:5555
        // restart helps.


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
                //x = "xMarshal " + OVROculus360Photos.Activities.xMarshal.stringFromJNI();
            }
            //finally
            {
                //                [javac] W:\src\__AnonymousTypes__OVRVrCubeWorldNativeActivity_AndroidActivity\__f__AnonymousType_97_0_1.java:34: error: reference to Format is ambiguous, both method Format(String,Object,Object) in __String and method Format(__IFormatProvider,String,Object[]) in __String match
                //[javac]         return __String.Format(null, "{{ api = {0} }}", objectArray2);
                //[javac]                        ^
                //[javac] Note: W:\src\ScriptCoreLibJava\BCLImplementation\System\Threading\__Thread.java uses or overrides a deprecated API.

                // https://stackoverflow.com/questions/7686482/when-does-applications-oncreate-method-is-called-on-android
                //Toast.makeText(this, "OVRVrCubeWorldNative " + x + new { api }, Toast.LENGTH_LONG).show();

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
        // X:\jsc.svn\examples\javascript\WebGL\WebGLStereoCubeMap\WebGLStereoCubeMap\Application.cs
        // http://paulbourke.net/geometry/transformationprojection/
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150723




        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150721/ovroculus360photoshud
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150711


        // "x:\util\android-sdk-windows\platform-tools\adb.exe" devices
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  tcpip 5555
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell netcfg

        // should jsc remember last connected device and reconnect if disconnected?
        // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect 192.168.1.126:5555
        // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect 192.168.1.68:5555
        // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect 192.168.1.69:5555
        // restart android?
        // restart usb debugging?


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

        class args
        {
            public long filesize;
            public string filename;

            public float x;
            public float y;
            public float z;
        }

        protected override void onCreate(Bundle savedInstanceState)
        {
            Console.WriteLine("enter OVROculus360Photos ApplicationActivity onCreate");

            base.onCreate(savedInstanceState);




            Console.WriteLine("about to convince NDK what the first image should be...");
            // http://www.flightradar24.com/18.39,37.3/2

            // http://paulbourke.net/geometry/transformationprojection/

            // http://krpano.com/download/
            // http://unrealoldfriends.activeboard.com/t47250341/creating-background-using-spacescape/?page=1

            //Convert CUBE to SPHERE droplet


            //kcube2sphere 1.18.4 - 64bit (build 2015-04-23)
            //loading...
            //loading azi_l.jpg...
            //loading azi_f.jpg...
            //loading azi_r.jpg...
            //loading azi_b.jpg...
            //loading azi_u.jpg...
            //loading azi_d.jpg...
            //done.
            //making sphere azi_sphere.tif...
            //done.

            //Press any key to continue . . .










            //C:\Windows\system32> x:\util\android-sdk-windows\platform-tools\adb.exe push X:\jsc.svn\examples\javascript\synergy\css\CSSAzimuthMapViz\CSSAzimuthMapViz\Textures  /sdcard/oculus/360Photos/
            //push: X:\jsc.svn\examples\javascript\synergy\css\CSSAzimuthMapViz\CSSAzimuthMapViz\Textures/azi_pz.jpg -> /sdcard/oculus/360Photos/azi_pz.jpg
            //push: X:\jsc.svn\examples\javascript\synergy\css\CSSAzimuthMapViz\CSSAzimuthMapViz\Textures/azi_py.jpg -> /sdcard/oculus/360Photos/azi_py.jpg
            //push: X:\jsc.svn\examples\javascript\synergy\css\CSSAzimuthMapViz\CSSAzimuthMapViz\Textures/azi_px.jpg -> /sdcard/oculus/360Photos/azi_px.jpg
            //push: X:\jsc.svn\examples\javascript\synergy\css\CSSAzimuthMapViz\CSSAzimuthMapViz\Textures/azi_nz.jpg -> /sdcard/oculus/360Photos/azi_nz.jpg
            //push: X:\jsc.svn\examples\javascript\synergy\css\CSSAzimuthMapViz\CSSAzimuthMapViz\Textures/azi_ny.jpg -> /sdcard/oculus/360Photos/azi_ny.jpg
            //push: X:\jsc.svn\examples\javascript\synergy\css\CSSAzimuthMapViz\CSSAzimuthMapViz\Textures/azi_nx.jpg -> /sdcard/oculus/360Photos/azi_nx.jpg
            //6 files pushed. 0 files skipped.
            //466 KB/s (969865 bytes in 2.030s)

            //C:\Windows\system32> x:\util\android-sdk-windows\platform-tools\adb.exe shell cp /sdcard/oculus/360Photos/humus.thm /sdcard/oculus/360Photos/azi.thm

            Action<string, string> copy =
                (from, to) =>
                {

                    try
                    {

                        // http://gis.stackexchange.com/questions/92907/re-project-raster-image-from-mercator-to-equirectangular

                        // https://en.wikipedia.org/wiki/List_of_map_projections
                        // Web Mercator
                        // https://xkcd.com/977/
                        // mercator?
                        var value = this.getResources().getAssets().open(from);
                        var s = new __NetworkStream { InternalInputStream = value };

                        // 1,392,914
                        //var buffer = new byte[1392914];
                        var buffer = new byte[4392914];

                        var len = s.Read(buffer, 0, buffer.Length);


                        var m = new MemoryStream();

                        m.Write(buffer, 0, len);

                        //s.CopyTo(

                        File.WriteAllBytes(to, m.ToArray());

                    }
                    catch
                    {
                        Console.WriteLine("about to convince NDK what the first image should be... fault");

                    }

                };

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150807/ovroculus360photosndk
            copy("2_no_clouds_4k.jpg", "/sdcard/oculus/360Photos/0.jpg");
            //copy("1.jpg", "/sdcard/oculus/360Photos/1.jpg");



            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150724/invaders
            //copy("celestial-joshua-trees-milky-way-in-saline-va.jpg", "/sdcard/oculus/360Photos/2.jpg");


            //Implementation not found for type import :
            //type: System.IO.DirectoryInfo
            //method: System.IO.FileInfo[] GetFiles()
            //Did you forget to add the [Script] attribute?
            //Please double check the signature!

            //Path.get

            var emptyFiles =
                from pf in new DirectoryInfo("/sdcard/oculus/360Photos/").GetFiles()
                where pf.Extension.ToLower() == ".jpg"
                where pf.Length == 0
                select pf;

            foreach (var emptyFile in emptyFiles.ToArray())
            {
                Console.WriteLine(new { emptyFile });

                emptyFile.Delete();
            }


            Console.WriteLine("about to convince NDK what the first image should be... done");


            var intent = getIntent();
            var commandString = com.oculus.vrappframework.VrActivity.getCommandStringFromIntent(intent);
            var fromPackageNameString = com.oculus.vrappframework.VrActivity.getPackageStringFromIntent(intent);
            var uriString = com.oculus.vrappframework.VrActivity.getUriStringFromIntent(intent);

            // D/CrashAnrDetector( 3472):     #00 pc 00092ac0  /data/app/OVROculus360Photos.Activities-1/lib/arm/libmain.so (OVR::ovrMessageQueue::PostMessage(char const*, bool, bool)+8)


            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/oculus360photossdk
            this.appPtr = OVROculus360Photos.Activities.xMarshal.nativeSetAppInterface(
                this,
                fromPackageNameString,
                commandString,
                uriString
            );

            var args = new args
            {

            };

            var uploadLength = 0L;
            var uploadPosition = 0L;

            var sw = Stopwatch.StartNew();

            #region mDraw
            var mDraw = new DrawOnTop(this)
            {
                // yes it appears top left.

                //text = "GearVR HUD"
                text = () => sw.ElapsedMilliseconds + "ms "
                    //+ "\n " + Path.GetFileName(args.filename)
                    + "\n " + args.filename

                    + "\n " + new
                    {
                        upload = (int)(100 * (uploadPosition + 1) / (args.filesize + 1)) + "%",
                        uploadPosition,
                        args.filesize,

                        // can we capture pointer?

                        args.x,
                        args.y,
                        args.z,

                        //uploadLength
                    }.ToString().Replace(",", ",\n")

                // X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeEquirectangularPanorama\ChromeEquirectangularPanorama\Application.cs
            };
            #endregion


            //Task.Run(

            #region sendTracking
            Action<IPAddress> sendTracking = nic =>
            {
                var port = new Random().Next(16000, 40000);

                //new IHTMLPre { "about to bind... " + new { port } }.AttachToDocument();

                // where is bind async?
                var socket = new UdpClient(
                     new IPEndPoint(nic, port)
                    );


                // who is on the other end?
                var nmessage = args.x + ":" + args.y + ":" + args.z + ":0:" + args.filename;

                var data = Encoding.UTF8.GetBytes(nmessage);      //creates a variable b of type byte


                //new IHTMLPre { "about to send... " + new { data.Length } }.AttachToDocument();

                // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                //Console.WriteLine("about to Send");
                // X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeEquirectangularPanorama\ChromeEquirectangularPanorama\Application.cs
                socket.Send(
                     data,
                     data.Length,
                     hostname: "239.1.2.3",
                     port: 49834
                 );



                socket.Close();

            };
            #endregion




            //I/System.Console( 9109): 2395:1fb3 enter __UdpClient ctor
            //I/System.Console( 9109): 2395:1fb3 enter __UdpClient before this.Client
            //I/System.Console( 9109): 2395:1fb3 enter __UdpClient after this.Client { Client = ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__Socket@4f1c02b }
            //I/System.Console( 9109): 2395:1fb3 enter GetAllNetworkInterfaces
            //I/System.Console( 9109): 2395:1fb3 enter __UdpClient ctor

            string current = null;
            byte[] bytes = null;

            new Thread(
                delegate()
                {
                    // bg thread


                    // bug out 1sec.
                    Thread.Sleep(1000);
                    // await gear on

                    while (true)
                    {
                        // collect tracking from ndk
                        // broadcast to udp


                        //Thread.Sleep(1000 / 15);

                        //var a = new
                        //{
                        //    // for java do we also do the fields?
                        //    x = 0
                        //};

                        args.filename = OVROculus360Photos.Activities.xMarshal.stringFromJNI(args);

                        //E/AndroidRuntime( 7601): Caused by: java.lang.NullPointerException: Attempt to invoke virtual method 'char[] java.lang.String.toCharArray()' on a null object reference
                        //E/AndroidRuntime( 7601):        at java.io.File.fixSlashes(File.java:185)
                        //E/AndroidRuntime( 7601):        at java.io.File.<init>(File.java:134)
                        //E/AndroidRuntime( 7601):        at ScriptCoreLibJava.BCLImplementation.System.IO.__File.Exists(__File.java:57)
                        //E/AndroidRuntime( 7601):        at OVROculus360PhotosHUD.Activities.ApplicationActivity___c__DisplayClass1d._onCreate_b__1b(ApplicationActivity___c__DisplayClass1d.java:95)



                        // uplink 144Mbps
                        // 18 MBps
                        #region udp broadcast
                        // overkill at 60hz
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
                                                 //fWASDC(ip.Address);
                                                 //fParallax(ip.Address);
                                                 //fvertexTransform(ip.Address);
                                                 sendTracking(ip.Address);
                                             }
                                     }
                                 }




                             }
                         );



                        #endregion

                        if (args.filename != null)
                            if (File.Exists(args.filename))
                            {
                                if (current != args.filename)
                                {
                                    current = args.filename;

                                    var ff = new FileInfo(args.filename);

                                    args.filesize = ff.Length;

                                    // we are not on ui thread.
                                    // HUD thread can freeze...
                                    // mmap?
                                    bytes = File.ReadAllBytes(args.filename);

                                    // now broadcast. at 500KBps in segments.
                                    // 8MB is 16 segments then.

                                    if (bytes.Length > 0)
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
                                                                  //fWASDC(ip.Address);
                                                                  //fParallax(ip.Address);
                                                                  //fvertexTransform(ip.Address);
                                                                  //sendTracking(ip.Address);

                                                                  var port = new Random().Next(16000, 40000);

                                                                  //new IHTMLPre { "about to bind... " + new { port } }.AttachToDocument();

                                                                  // where is bind async?
                                                                  var socket = new UdpClient(
                                                                       new IPEndPoint(ip.Address, port)
                                                                      );


                                                                  //// who is on the other end?
                                                                  //var nmessage = args.x + ":" + args.y + ":" + args.z + ":0:" + args.filename;

                                                                  //var data = Encoding.UTF8.GetBytes(nmessage);      //creates a variable b of type byte

                                                                  // http://stackoverflow.com/questions/25841/maximum-buffer-length-for-sendto

                                                                  new { }.With(
                                                                      async delegate
                                                                      {
                                                                          // reached too far?
                                                                          if (bytes.Length == 0)
                                                                              return;

                                                                          var current0 = current;

                                                                          var r = new MemoryStream(bytes);
                                                                          uploadLength = r.Length;

                                                                          var data = new byte[65507];

                                                                      next:

                                                                          if (current0 != current)
                                                                              return;

                                                                          var cc = r.Read(data, 0, data.Length);

                                                                          uploadPosition = r.Position;

                                                                          if (cc <= 0)
                                                                              return;

                                                                          //new IHTMLPre { "about to send... " + new { data.Length } }.AttachToDocument();

                                                                          // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                                                                          //Console.WriteLine("about to Send");
                                                                          // X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeEquirectangularPanorama\ChromeEquirectangularPanorama\Application.cs
                                                                          await socket.SendAsync(
                                                                               data,
                                                                               cc,
                                                                               hostname: "239.1.2.3",
                                                                               port: 49000
                                                                           );

                                                                          //await Task.Delay(1000 / 15);
                                                                          //await Task.Delay(1000 / 30);

                                                                          // no corruption
                                                                          await Task.Delay(1000 / 20);

                                                                          goto next;

                                                                      }
                                                                  );

                                                                  //socket.Close();
                                                              }
                                                      }
                                                  }




                                              }
                                          );
                                }
                            }

                        if (uploadPosition < args.filesize)
                            mDraw.color = android.graphics.Color.YELLOW;
                        else
                            mDraw.color = android.graphics.Color.GREEN;

                        mDraw.postInvalidate();
                        Thread.Sleep(1000 / 30);
                        //Thread.Sleep(1000 / 2);
                        //Thread.Sleep(1000 / 15);
                    }
                }
            ).Start();

            //this.ondispatchTouchEvent += @event =>
            //{

            //    int action = @event.getAction();
            //    float x = @event.getRawX();
            //    float y = @event.getRawY();
            //    //if (action == MotionEvent.ACTION_UP)
            //    {
            //        var halfx = 2560 / 2;
            //        var halfy = 1440 / 2;

            //        mDraw.x = (int)(500 + halfx - x);
            //        mDraw.y = (int)(600 + y - halfy);
            //        mDraw.text = () => sw.ElapsedMilliseconds + "ms \n" + new { x, y, action }.ToString();
            //        //Console.WriteLine(" ::dispatchTouchEvent( " + action + ", " + x + ", " + y + " )");
            //    }

            //    // can we move hud around and record it to gif or mp4?

            //    return true;
            //};

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
            // html hud?

            // http://stackoverflow.com/questions/6927286/force-a-view-to-redraw-itself

            public DrawOnTop(android.content.Context context)
                : base(context)
            {
            }

            // udp mouse ?
            public int x = 300; // animate it?
            public int y = 800;

            public Func<string> text = () => "hello!";

            public int textSize = 25;

            public int color = android.graphics.Color.GREEN;
            public int alpha = 255;

            protected override void onDraw(android.graphics.Canvas canvas)
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

                base.onDraw(canvas);
            }
        }

    }


}
