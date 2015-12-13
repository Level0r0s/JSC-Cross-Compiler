using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.os;
using android.provider;
using android.webkit;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLibJava.Extensions;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using ScriptCoreLib.Extensions;
using android.net.wifi;
using System.Diagnostics;

namespace AndroidBrowserVR.Activities
{

    static class xMarshal
    {
        // called by?

        [Script(IsPInvoke = true)]
        //private long find(string lib, string fname) { return default(long); }
        public static string stringFromJNI(object args = null) { return default(string); }

        //[Script(IsPInvoke = true)]
        //public static long nativeSetAppInterface(
        //    //com.oculusvr.vrlib.VrActivity act,
        //    object act,

        //    string fromPackageNameString,
        //    string commandString,
        //    string uriString)
        //{ return default(long); }
    }


    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704/ovroculus360photoshud
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150613/record
    // "x:\util\android-sdk-windows\platform-tools\adb.exe" shell am start -n OVROculus360PhotosHUD.Activities/OVROculus360PhotosHUD.Activities.ApplicationActivity



    // "X:\opensource\ovr_mobile_sdk_0.6.0\VrSamples\Native\Oculus360PhotosSDK\src\com\oculus\oculus360photossdk\MainActivity.java"

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "com.samsung.android.vr.application.mode", value = "vr_only")]
    // https://forums.oculus.com/viewtopic.php?t=21409
    // vr_dual wont run on gearvr for now...
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "com.samsung.android.vr.application.mode", value = "dual")]
    // for dual we could switch to chrome view activity
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150721/ovroculus360photoshud

    // vr_only means we wont see our activity ui
    //<meta-data android:name="com.samsung.android.vr.application.mode" android:value="vr_only"/>
    public class LocalApplication : Application
    {
        // "x:\util\android-sdk-windows\platform-tools\adb.exe" devices
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  tcpip 5555
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell netcfg

        // should jsc remember last connected device and reconnect if disconnected?
        // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect 192.168.1.126:5555
        // restart helps.


        // x:\util\android-sdk-windows\platform-tools\adb.exe shell am force-stop AndroidBrowserVR.Activities
        // x:\util\android-sdk-windows\platform-tools\adb.exe shell am start -n AndroidBrowserVR.Activities/AndroidBrowserVR.Activities.ApplicationActivity

        // x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "xNativeActivity" "System.Console" "DEBUG" "PlatformActivity" "AndroidRuntime"


        public override void onCreate()
        {
            base.onCreate();

            Console.WriteLine("enter AndroidBrowserVR LocalApplication onCreate, first time?");

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
            Console.WriteLine("enter AndroidBrowserVR LocalApplication cctor");


            // "X:\opensource\ovr_mobile_sdk_0.6.0\VrApi\Libs\Android\armeabi-v7a\libvrapi.so"

            // did csproj copy it where it needs to be?

            // do we need it?
            java.lang.System.loadLibrary("vrapi");
            java.lang.System.loadLibrary("assimp");

            //java.lang.System.loadLibrary("assimp");

            //<!-- Tell NativeActivity the name of the .so -->
            //<meta-data android:name="android.app.lib_name" android:value="vrcubeworld" />
            // why bother?

            //java.lang.System.loadLibrary("vrcubeworld");

            // need to link it!
            // "X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldNativeActivity\OVRVrCubeWorldNative\bin\Debug\staging\libs\armeabi-v7a\libOVRVrCubeWorldNative.so"

            // incline android c would automate this step.
            //java.lang.System.loadLibrary("OVRVrCubeWorldNative");
            java.lang.System.loadLibrary("main");


            var stringFromJNI = xMarshal.stringFromJNI();

            Console.WriteLine(new { stringFromJNI });
        }
    }


    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:screenOrientation", value = "landscape")]




    // http://swagos.blogspot.com/2012/12/various-themes-available-in-android_28.html
    // Theme.Holo.Dialog.Alert
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Light.Dialog")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.DeviceDefault.Light")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.DeviceDefault.Dialog")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.DeviceDefault.Light.Dialog")]

    // works for 2.4 too
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent")]
    public class ApplicationActivity :

        // Error	5	The type 'com.oculus.vrappframework.VrActivity' is defined in an assembly that is not referenced. You must add a reference to assembly 'VrAppFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'.	Z:\jsc.svn\examples\java\android\synergy\AndroidBrowserVR\ApplicationActivity.cs	193	18	AndroidBrowserVR

        //Activity
    com.navigatevr.MainActivity
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151201/samsungbrowser

        //        connect s6 via usb .
        // turn on wifi!
        // kill adb

        //"x:\util\android-sdk-windows\platform-tools\adb.exe"  tcpip 5555
        // restarting in TCP mode port: 5555

        //13: wlan0: <BROADCAST,MULTICAST,UP,LOWER_UP> mtu 1500 qdisc pfifo_fast state UP qlen 1000
        //    inet 192.168.1.126/24 brd 192.168.1.255 scope global wlan0
        //       valid_lft forever preferred_lft forever

        // on red
        // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect  192.168.1.126:5555
        // connected to 192.168.1.126:5555



        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151121
        // http://stackoverflow.com/questions/17513502/support-for-multi-window-app-development

        //protected override void onCreate(global::android.os.Bundle savedInstanceState)
        public override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            Console.WriteLine("enter ApplicationActivity onCreate");
            base.onCreate(savedInstanceState);


            //base.load

            //var sv = new ScrollView(this);
            //var ll = new LinearLayout(this);

            //ll.setOrientation(LinearLayout.VERTICAL);
            //sv.addView(ll);

            //var b = new Button(this);

            //b.setText("Vibrate!");

            var sw = Stopwatch.StartNew();

            Action<string> SetClipboard = value =>
            {
                Console.WriteLine("SetClipboard " + new { value });

                this.runOnUiThread(
                    delegate
                    {


                        var nm = (NotificationManager)this.getSystemService(Activity.NOTIFICATION_SERVICE);


                        // see http://developer.android.com/reference/android/app/Notification.html
                        var notification = new Notification(
                            //android.R.drawable.ic_dialog_alert,
                            android.R.drawable.ic_menu_view,
                            //tickerText: "not used?",
                            tickerText: value,


                            when: 0
                            //java.lang.System.currentTimeMillis()
                        );

                        //notification.defaults |= Notification.DEFAULT_SOUND;

                        var notificationIntent = new Intent(this, typeof(ApplicationActivity).ToClass());
                        var contentIntent = PendingIntent.getActivity(this, 0, notificationIntent, 0);


                        notification.setLatestEventInfo(
                            this,
                            contentTitle: value,
                            contentText: "",
                            contentIntent: contentIntent);

                        //notification.defaults |= Notification.DEFAULT_VIBRATE;
                        //notification.defaults |= Notification.DEFAULT_LIGHTS;
                        // http://androiddrawableexplorer.appspot.com/
                        nm.notify((int)sw.ElapsedMilliseconds, notification);

                        var vibrator = (Vibrator)this.getSystemService(Context.VIBRATOR_SERVICE);
                        vibrator.vibrate(600);

                        android.content.ClipboardManager clipboard = (android.content.ClipboardManager)getSystemService(CLIPBOARD_SERVICE);
                        ClipData clip = ClipData.newPlainText("label", value);
                        clipboard.setPrimaryClip(clip);

                        base.getWebView().loadUrl(value);

                    }
                );
            };


            //b.AtClick(
            //    delegate
            //    {
            //        SetClipboard("hello");
            //    }
            //);

            #region lets listen to incoming udp
            // could we define our chrome app inline in here?
            // or in a chrome app. could we define the android app inline?
            #region ReceiveAsync
            Action<IPAddress> f = async nic =>
            {
                //b.setText("awaiting at " + nic);


                WifiManager wifi = (WifiManager)this.getSystemService(Context.WIFI_SERVICE);
                var lo = wifi.createMulticastLock("udp:49814");
                lo.acquire();

                // Z:\jsc.svn\examples\java\android\AndroidBrowserVR\ApplicationActivity.cs
                // X:\jsc.svn\examples\java\android\forms\FormsUDPJoinGroup\FormsUDPJoinGroup\ApplicationControl.cs
                // X:\jsc.svn\examples\java\android\LANBroadcastListener\LANBroadcastListener\ApplicationActivity.cs
                var uu = new UdpClient(49814);
                uu.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"), nic);
                while (true)
                {
                    // cannot get data from RED?
                    var x = await uu.ReceiveAsync(); // did we jump to ui thread?
                    //Console.WriteLine("ReceiveAsync done " + Encoding.UTF8.GetString(x.Buffer));
                    var data = Encoding.UTF8.GetString(x.Buffer);



                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704
                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704/mousedown
                    SetClipboard(data);
                }
            };

            // WithEach defined at?
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
                                    f(ip.Address);
                        }
                    }




                }
            );
            #endregion


            #endregion

            //// jsc could pass this ptr to ctor for context..
            //var t = new EditText(this) { };

            //t.AttachTo(ll);

            //ll.addView(b);



            //this.setContentView(sv);


            //this.ShowLongToast("http://my.jsc-solutions.net x");
        }


    }
}
