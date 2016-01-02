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
using android.media;
using android.graphics;
using android.view;

namespace com.oculus.oculus360videossdk
{
    static class MainActivity
    {
        // defined at 
        // X:\opensource\ovr_sdk_mobile_1.0.0.0\VrSamples\Native\Oculus360VideosSDK\Src\Oculus360Videos.cpp

        [Script(IsPInvoke = true)]
        public static void nativeSetVideoSize(long appPtr, int width, int height);
        [Script(IsPInvoke = true)]
        public static SurfaceTexture nativePrepareNewVideo(long appPtr);
        [Script(IsPInvoke = true)]
        public static void nativeFrameAvailable(long appPtr);
        [Script(IsPInvoke = true)]
        public static void nativeVideoCompletion(long appPtr);
        [Script(IsPInvoke = true)]
        public static long nativeSetAppInterface(Activity act, string fromPackageNameString, string commandString, string uriString);
    }
}

namespace x360video.Activities
{

    static class xMarshal
    {
        // called by?

        [Script(IsPInvoke = true)]
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




    public class LocalApplication : Application
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160102/x360videos

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

            Console.WriteLine("enter x360video LocalApplication onCreate, first time?");
        }

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150607-1/vrcubeworld
        /** Load jni .so on initialization */
        static LocalApplication()
        {
            Console.WriteLine("enter x360video LocalApplication cctor");
            java.lang.System.loadLibrary("vrapi");
            java.lang.System.loadLibrary("main");

            var stringFromJNI = xMarshal.stringFromJNI();

            Console.WriteLine(new { stringFromJNI });
        }
    }





    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]

    // http://swagos.blogspot.com/2012/12/various-themes-available-in-android_28.html
    // Theme.Holo.Dialog.Alert
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Light.Dialog")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.DeviceDefault.Light")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.DeviceDefault.Dialog")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.DeviceDefault.Light.Dialog")]

    // works for 2.4 too
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent")]
    public class ApplicationActivity
        //: Activity,
         : com.oculus.vrappframework.VrActivity,


        android.graphics.SurfaceTexture.OnFrameAvailableListener,
        MediaPlayer.OnVideoSizeChangedListener,
        MediaPlayer.OnCompletionListener,
        MediaPlayer.OnErrorListener,
        AudioManager.OnAudioFocusChangeListener

        // defined at?
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160102/x360videos

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151212/androidudpclipboard
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160101/ovrwindwheelndk

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


        void requestAudioFocus()
        {
            // Request audio focus
            int result = audioManager.requestAudioFocus(this, AudioManager.STREAM_MUSIC,
                AudioManager.AUDIOFOCUS_GAIN);
            if (result == AudioManager.AUDIOFOCUS_REQUEST_GRANTED)
            {
                //Log.d(TAG, "startMovie(): GRANTED audio focus");
            }
            else if (result == AudioManager.AUDIOFOCUS_REQUEST_GRANTED)
            {
                //Log.d(TAG, "startMovie(): FAILED to gain audio focus");
            }
        }

        void releaseAudioFocus()
        {
            audioManager.abandonAudioFocus(this);
        }




        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this);

            b.setText("Vibrate!");

            var sw = Stopwatch.StartNew();



            Action cleanup = delegate { };

            Notification reuse = null;
            var notificationIntent = new Intent(this, typeof(ApplicationActivity).ToClass());
            var contentIntent = PendingIntent.getActivity(this, 0, notificationIntent, 0);

            Action<string> SetClipboard = value =>
            {
                Console.WriteLine("SetClipboard " + new { value });

                this.runOnUiThread(
                    delegate
                    {
                        cleanup();

                        b.setText(value);




                        if (reuse != null)
                        {
                            reuse.setLatestEventInfo(
                                 this,
                                 contentTitle: value,
                                 contentText: "",
                                 contentIntent: contentIntent);

                            return;
                        }

                        var xNotificationManager = (NotificationManager)this.getSystemService(Activity.NOTIFICATION_SERVICE);

                        // see http://developer.android.com/reference/android/app/Notification.html
                        var xNotification = new Notification(
                            //android.R.drawable.ic_dialog_alert,
                            android.R.drawable.ic_menu_view,
                            //tickerText: "not used?",
                            tickerText: value,


                            when: 0
                            //java.lang.System.currentTimeMillis()
                        );

                        //notification.defaults |= Notification.DEFAULT_SOUND;



                        // flags = Notification.FLAG_ONGOING_EVENT 

                        var FLAG_ONGOING_EVENT = 0x00000002;
                        //notification.flags |= Notification.FLAG_ONGOING_EVENT;
                        //xNotification.flags |= FLAG_ONGOING_EVENT;

                        xNotification.setLatestEventInfo(
                            this,
                            contentTitle: value,
                            contentText: "",
                            contentIntent: contentIntent);

                        //notification.defaults |= Notification.DEFAULT_VIBRATE;
                        //notification.defaults |= Notification.DEFAULT_LIGHTS;
                        // http://androiddrawableexplorer.appspot.com/

                        var id = (int)sw.ElapsedMilliseconds;

                        xNotificationManager.notify(id, xNotification);

                        var xVibrator = (Vibrator)this.getSystemService(Context.VIBRATOR_SERVICE);
                        xVibrator.vibrate(600);



                        #region setPrimaryClip
                        android.content.ClipboardManager clipboard = (android.content.ClipboardManager)getSystemService(CLIPBOARD_SERVICE);
                        ClipData clip = ClipData.newPlainText("label", value);
                        clipboard.setPrimaryClip(clip);
                        #endregion

                        reuse = xNotification;


                        cleanup += delegate
                        {
                            // https://developer.android.com/reference/android/app/Notification.html

                            if (xNotification == null)
                                return;

                            xNotificationManager.cancel(id);
                        };
                    }
                );
            };


            //b.AtClick(
            //    delegate
            //    {
            //        // CLRMainn hybrid like?
            //        SetClipboard(xMarshal.stringFromJNI());
            //    }
            //);

            SetClipboard(xMarshal.stringFromJNI());

            #region lets listen to incoming udp
            // could we define our chrome app inline in here?
            // or in a chrome app. could we define the android app inline?
            #region ReceiveAsync
            Action<IPAddress> f = async nic =>
            {
                b.setText("awaiting at " + nic);


                WifiManager wifi = (WifiManager)this.getSystemService(Context.WIFI_SERVICE);
                var lo = wifi.createMulticastLock("udp:49814");
                lo.acquire();

                // Z:\jsc.svn\examples\java\android\x360video\ApplicationActivity.cs
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

            // jsc could pass this ptr to ctor for context..
            var t = new EditText(this) { };

            t.AttachTo(ll);

            ll.addView(b);



            this.setContentView(sv);


            //this.ShowLongToast("http://my.jsc-solutions.net x");
        }





        SurfaceTexture movieTexture = null;
        Surface movieSurface = null;
        MediaPlayer mediaPlayer = null;
        AudioManager audioManager = null;


        public void onFrameAvailable(SurfaceTexture value)
        {
            throw new NotImplementedException();
        }

        public void onVideoSizeChanged(MediaPlayer arg0, int arg1, int arg2)
        {
            throw new NotImplementedException();
        }

        public void onCompletion(MediaPlayer value)
        {
            throw new NotImplementedException();
        }

        public bool onError(MediaPlayer arg0, int arg1, int arg2)
        {
            throw new NotImplementedException();
        }

        public void onAudioFocusChange(int value)
        {
            throw new NotImplementedException();
        }





        //public virtual 

        public long base_getAppPtr()
        {
            return (this as dynamic).getAppPtr();
        }

        public void startMovie(String pathName)
        {
            //Log.v(TAG, "startMovie " + pathName);

            //synchronized (this) 
            {
                // Request audio focus
                requestAudioFocus();

                // Have native code pause any playing movie,
                // allocate a new external texture,
                // and create a surfaceTexture with it.
                movieTexture = com.oculus.oculus360videossdk.MainActivity.nativePrepareNewVideo(base_getAppPtr());
                movieTexture.setOnFrameAvailableListener(this);
                movieSurface = new Surface(movieTexture);

                if (mediaPlayer != null)
                {
                    mediaPlayer.release();
                }

                //Log.v(TAG, "MediaPlayer.create");

                //synchronized (this) {
                mediaPlayer = new MediaPlayer();
                //}


                mediaPlayer.setOnVideoSizeChangedListener(this);
                mediaPlayer.setOnCompletionListener(this);
                mediaPlayer.setSurface(movieSurface);

                try
                {
                    //Log.v(TAG, "mediaPlayer.setDataSource()");
                    mediaPlayer.setDataSource(pathName);
                }
                catch //(IOException t) 
                {
                    //Log.e(TAG, "mediaPlayer.setDataSource failed");
                }

                try
                {
                    //Log.v(TAG, "mediaPlayer.prepare");
                    mediaPlayer.prepare();
                }
                catch //(IOException t) 
                {
                    //Log.e(TAG, "mediaPlayer.prepare failed:" + t.getMessage());
                }
                //Log.v(TAG, "mediaPlayer.start");

                // If this movie has a saved position, seek there before starting
                // This seems to make movie switching crashier.
                int seekPos = getPreferences(MODE_PRIVATE).getInt(pathName + "_pos", 0);
                if (seekPos > 0)
                {
                    try
                    {
                        mediaPlayer.seekTo(seekPos);
                    }
                    catch //( IllegalStateException ise ) 
                    {
                        //Log.d( TAG, "mediaPlayer.seekTo(): Caught illegalStateException: " + ise.toString() );
                    }
                }

                mediaPlayer.setLooping(false);

                try
                {
                    mediaPlayer.start();
                }
                catch //( IllegalStateException ise ) 
                {
                    //Log.d( TAG, "mediaPlayer.start(): Caught illegalStateException: " + ise.toString() );
                }

                mediaPlayer.setVolume(1.0f, 1.0f);

                // Save the current movie now that it was successfully started
                var edit = getPreferences(MODE_PRIVATE).edit();
                edit.putString("currentMovie", pathName);
                edit.commit();
            }

            //Log.v(TAG, "returning");
        }
    }
}
