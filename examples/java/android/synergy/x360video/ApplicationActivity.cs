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
using System.IO;
using ScriptCoreLib.Extensions;


namespace com.oculus.vrgui
{
    class xVolumeReceiver
    {
        public xVolumeReceiver()
        {
            var x = typeof(global::com.oculus.vrgui.VolumeReceiver);

        }
    }

}

namespace com.oculus.sound
{
    class xSoundPooler
    {
        public xSoundPooler()
        {
            var refSoundPooler = typeof(global::com.oculus.sound.SoundPooler);

            Console.WriteLine("xSoundPooler " + new { refSoundPooler });
        }
    }
}

namespace com.oculus.oculus360videossdk
{
    static class MainActivity
    {
        // defined at 
        // X:\opensource\ovr_sdk_mobile_1.0.0.0\VrSamples\Native\Oculus360VideosSDK\Src\Oculus360Videos.cpp

        [Script(IsPInvoke = true)]
        public static void nativeSetVideoSize(long appPtr, int width, int height) { throw null; }
        [Script(IsPInvoke = true)]
        public static SurfaceTexture nativePrepareNewVideo(long appPtr) { throw null; }
        [Script(IsPInvoke = true)]
        public static void nativeFrameAvailable(long appPtr) { throw null; }
        [Script(IsPInvoke = true)]
        public static void nativeVideoCompletion(long appPtr) { throw null; }
        [Script(IsPInvoke = true)]
        public static long nativeSetAppInterface(Activity act, string fromPackageNameString, string commandString, string uriString) { throw null; }
    }
}

namespace x360video.Activities
{
    public sealed class startMovieFromUDPArguments
    {
        public long __ptr;
        public string pathName; 
    
    }

    static class xMarshal
    {
        // called by?

        [Script(IsPInvoke = true)]
        public static string stringFromJNI(object args = null) { return default(string); }

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/x360videoui
        [Script(IsPInvoke = true)]
        public static void startMovieFromUDP(startMovieFromUDPArguments args = null) { }


        //[Script(IsPInvoke = true)]
        //public static long nativeSetAppInterface(
        //    //com.oculusvr.vrlib.VrActivity act,
        //    object act,

        //    string fromPackageNameString,
        //    string commandString,
        //    string uriString)
        //{ return default(long); }
    }



    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "com.samsung.android.vr.application.mode", value = "vr_only")]
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


        }
    }




    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:screenOrientation", value = "landscape")]

    public partial class ApplicationActivity


        // defined at?
        // "X:\opensource\ovr_sdk_mobile_1.0.0.0\VrAppFramework\Libs\Android\VrAppFramework.jar"
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
            // https://forums.oculus.com/viewtopic.php?f=67&t=27999




            var stringFromJNI = xMarshal.stringFromJNI(this);
            Console.WriteLine(new { stringFromJNI });




            //var refSystemActivities = typeof(global::com.oculus.systemutils.Ev);


            // loaded by
            // X:\opensource\ovr_sdk_mobile_1.0.0.0\VrAppSupport\VrSound\Src\SoundPool.cpp


            // "X:\opensource\ovr_sdk_mobile_1.0.0.0\VrAppSupport\VrSound\Projects\Android\src\com\oculus\sound\SoundPooler.java"

            //var refSoundPooler = typeof(global::com.oculus.sound.SoundPooler);
            //var refSoundPooler = typeof(global::com.oculus.sound.xSoundPooler);


            // static const char * videosDirectory = "Oculus/360Videos/";


            var mp4 =
              from pf in new DirectoryInfo("/sdcard/oculus/360Videos/").GetFiles()
              where pf.Extension.ToLower() == ".mp4"
              //where pf.Length == 0
              select pf;

            var mp4count = mp4.Count();

            Console.WriteLine(new { mp4count });

            foreach (var item in mp4)
            {
                Console.WriteLine(item.FullName);
            }

            var refVolumeReceiver = new com.oculus.vrgui.xVolumeReceiver { };
            var refSoundPooler = new com.oculus.sound.xSoundPooler { };
            var refVrLocale = typeof(global::com.oculus.vrlocale.VrLocale);
            //var refVolumeReceiver = typeof(global::com.oculus.vrgui.VolumeReceiver);

            //[javac] W:\src\x360video\Activities\ApplicationActivity.java:21: error: SoundPooler is not public in com.oculus.sound; cannot be accessed from outside package
            //[javac] import com.oculus.sound.SoundPooler;
            //[javac]                        ^

            var refSystemActivities = typeof(global::com.oculus.systemutils.SystemActivities);
            Console.WriteLine("enter onCreate " + new { refSystemActivities, refSoundPooler, refVrLocale, refVolumeReceiver });



            base.onCreate(savedInstanceState);

            Intent intent = getIntent();

            String commandString = com.oculus.vrappframework.VrActivity.getCommandStringFromIntent(intent);
            String fromPackageNameString = com.oculus.vrappframework.VrActivity.getPackageStringFromIntent(intent);
            String uriString = com.oculus.vrappframework.VrActivity.getUriStringFromIntent(intent);


            Console.WriteLine("onCreate " + new { fromPackageNameString, commandString, uriString });

            var p = com.oculus.oculus360videossdk.MainActivity.nativeSetAppInterface(this, fromPackageNameString, commandString, uriString);

            base_setAppPtr(p);

            audioManager = (AudioManager)getSystemService(Context.AUDIO_SERVICE);


            #region lets listen to incoming udp
            // could we define our chrome app inline in here?
            // or in a chrome app. could we define the android app inline?
            #region ReceiveAsync
            Action<IPAddress> f = async nic =>
            {
                //b.setText("awaiting at " + nic);


                WifiManager wifi = (WifiManager)this.getSystemService(Context.WIFI_SERVICE);
                var lo = wifi.createMulticastLock("udp:39814");
                lo.acquire();

                // Z:\jsc.svn\examples\java\android\AndroidUDPClipboard\ApplicationActivity.cs
                // X:\jsc.svn\examples\java\android\forms\FormsUDPJoinGroup\FormsUDPJoinGroup\ApplicationControl.cs
                // X:\jsc.svn\examples\java\android\LANBroadcastListener\LANBroadcastListener\ApplicationActivity.cs
                var uu = new UdpClient(39814);
                uu.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"), nic);
                while (true)
                {
                    // cannot get data from RED?
                    var x = await uu.ReceiveAsync(); // did we jump to ui thread?
                    //Console.WriteLine("ReceiveAsync done " + Encoding.UTF8.GetString(x.Buffer));
                    //var data = Encoding.UTF8.GetString(x.Buffer);



                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704
                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704/mousedown
                    //SetClipboard(data);


                    var md5string = x.Buffer.ToHexString();
                    var lookup = startMovieLookup.ContainsKey(md5string);


                    Console.WriteLine(new { md5string, lookup });
                    if (lookup)
                    {
                        // this wont work if we are paused.

                        this.startMovieFromUDP(
                            startMovieLookup[md5string]
                        );
                    }
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

        }

        protected override void onResume()
        {
            Console.WriteLine("enter onResume ");

            base.onResume();
        }

        protected override void onDestroy()
        {
            releaseAudioFocus();

            base.onDestroy();
        }


        Surface movieSurface = null;
        MediaPlayer mediaPlayer = null;
        AudioManager audioManager = null;


        public void onFrameAvailable(SurfaceTexture value)
        {
            com.oculus.oculus360videossdk.MainActivity.nativeFrameAvailable(base_getAppPtr());
        }

        public void onVideoSizeChanged(MediaPlayer arg0, int width, int height)
        {
            //Log.v(TAG, String.format("onVideoSizeChanged: %dx%d", width, height));
            if (width == 0 || height == 0)
            {
                //Log.e(TAG, "The video size is 0. Could be because there was no video, no display surface was set, or the value was not determined yet.");
            }
            else
            {
                com.oculus.oculus360videossdk.MainActivity.nativeSetVideoSize(base_getAppPtr(), width, height);
            }
        }

        public void onCompletion(MediaPlayer value)
        {
            com.oculus.oculus360videossdk.MainActivity.nativeVideoCompletion(base_getAppPtr());
        }

        public bool onError(MediaPlayer arg0, int arg1, int arg2)
        {
            return false;
        }

        public void onAudioFocusChange(int value)
        {
            //switch (focusChange)
            //{
            //    case AudioManager.AUDIOFOCUS_GAIN:
            //        // resume() if coming back from transient loss, raise stream volume if duck applied
            //        Log.d(TAG, "onAudioFocusChangedListener: AUDIOFOCUS_GAIN");
            //        break;
            //    case AudioManager.AUDIOFOCUS_LOSS:				// focus lost permanently
            //        // stop() if isPlaying
            //        Log.d(TAG, "onAudioFocusChangedListener: AUDIOFOCUS_LOSS");
            //        break;
            //    case AudioManager.AUDIOFOCUS_LOSS_TRANSIENT:	// focus lost temporarily
            //        // pause() if isPlaying
            //        Log.d(TAG, "onAudioFocusChangedListener: AUDIOFOCUS_LOSS_TRANSIENT");
            //        break;
            //    case AudioManager.AUDIOFOCUS_LOSS_TRANSIENT_CAN_DUCK:	// focus lost temporarily
            //        // lower stream volume
            //        Log.d(TAG, "onAudioFocusChangedListener: AUDIOFOCUS_LOSS_TRANSIENT_CAN_DUCK");
            //        break;
            //    default:
            //}

        }





        //public virtual 
        long __ptr;

        public long base_getAppPtr()
        {
            //Console.WriteLine("base_getAppPtr ");

            //return (this as dynamic).getAppPtr();


            return __ptr;
        }


        public void base_setAppPtr(long ptr)
        {
            // https://developer.oculus.com/documentation/mobilesdk/latest/concepts/mobile-native-migration/

            //appPtr is no longer directly exposed on VrActivity. Replace any references to appPtr with the appropriate accessor call:

            //      long getAppPtr();
            //      void setAppPtr(long appPtr);


            // wtf?


            // protected void setAppPtr(long);

            //var setAppPtr = typeof(com.oculus.vrappframework.VrActivity).GetMethod("setAppPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var setAppPtr = typeof(com.oculus.vrappframework.VrActivity).GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).FirstOrDefault(x => x.Name == "setAppPtr");


            //0c64:0001 base_setAppPtr { ptr = -189489152, setAppPtr = void setAppPtr(long) }


            Console.WriteLine("base_setAppPtr " + new { ptr, setAppPtr });

            setAppPtr.Invoke(this, new object[] { ptr });


            __ptr = ptr;


            //I/System.Console(32068): 7d44:0001 base_setAppPtr { ptr = -189493248 }

            //(this as dynamic).c(ptr);
        }









        public void stopMovie()
        {
            //Log.d(TAG, "stopMovie()");

            if (mediaPlayer != null)
            {
                //Log.d(TAG, "movie stopped");
                mediaPlayer.stop();
            }

            releaseAudioFocus();
        }



        // called by?
        public bool isPlaying()
        {
            //try
            //{
            if (mediaPlayer != null)
            {
                var playing = mediaPlayer.isPlaying();
                if (playing)
                {
                    //Log.d(TAG, "isPlaying() = true");
                }
                else
                {
                    //Log.d(TAG, "isPlaying() = false");
                }
                return playing;
            }
            //Log.d(TAG, "isPlaying() - NO MEDIA PLAYER");
            //}
            //catch  //(IllegalStateException ise)
            //{
            //    //Log.d(TAG, "isPlaying(): Caught illegalStateException: " + ise.toString());
            //}
            return false;
        }




        public void pauseMovie()
        {
            //Log.d(TAG, "pauseMovie()");
            try
            {
                if (mediaPlayer != null)
                {
                    //Log.d(TAG, "movie paused");
                    mediaPlayer.pause();
                }
            }
            catch //(IllegalStateException ise)
            {
                //Log.d(TAG, "pauseMovie(): Caught illegalStateException: " + ise.toString());
            }
        }


        public void resumeMovie()
        {
            //Log.d(TAG, "resumeMovie()");
            try
            {
                if (mediaPlayer != null)
                {
                    //Log.d(TAG, "movie started");
                    mediaPlayer.start();
                    mediaPlayer.setVolume(1.0f, 1.0f);
                }
            }
            catch //(IllegalStateException ise)
            {
                //Log.d(TAG, "resumeMovie(): Caught illegalStateException: " + ise.toString());
            }
        }





        // can we hop to jvm from ndk?
        public void seekToFromNative(int seekPos)
        {
            //Log.d( TAG, "seekToFromNative to " + seekPos );
            try
            {
                if (mediaPlayer != null)
                {
                    mediaPlayer.seekTo(seekPos);
                }
            }
            catch //( IllegalStateException ise ) 
            {
                //Log.d( TAG, "seekToFromNative(): Caught illegalStateException: " + ise.toString() );
            }
        }





    }
}
