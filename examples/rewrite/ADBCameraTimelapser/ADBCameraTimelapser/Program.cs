using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;
using System.IO;
using System.Diagnostics;

namespace ADBCameraTimelapser
{
    class Program
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150730

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150727

        // we changed code while app is running.
        // we save the file
        // now the running app could also know it is out of sync with the source code.
        // it could volunteer to review the changes and adapt
        // or vote to restart?

        static void Main(string[] args)
        {
            // error: protocol fault (status read)

            // 3330A17632C000EC        device
            //var device = "192.168.1.126:5555";
            var device = "3330A17632C000EC";
            var storage = "x:/vr/tape1";

            if (args.Length == 2)
            {
                device = args[0];
                storage = args[1];
            }

            Console.WriteLine(new { AppDomain.CurrentDomain.SetupInformation.ApplicationBase });
            Console.WriteLine(new { Environment.CommandLine });
            Console.WriteLine(new { Environment.ProcessorCount });
            Console.WriteLine(new { device });
            Console.WriteLine(new { storage });

            Console.WriteLine("hi");

            var ffmpegNNNNfiles = Directory.GetFiles(storage).Select(
                (x, i) =>
                {
                    var iNNNN = i.ToString("0000");
                    var t = Path.GetDirectoryName(x) + "/" + iNNNN + ".jpg";
                    File.Move(
                        x, t

                    );

                    return new { x, i, iNNNN };
                }
            ).ToArray();


            var adb = @"x:\util\android-sdk-windows\platform-tools\adb.exe";

            Action<string> do_adb = a =>
            {
                var sw = Stopwatch.StartNew();
                Console.WriteLine("> " + a);

                System.Diagnostics.Process.Start(
                   new System.Diagnostics.ProcessStartInfo(adb, a) { UseShellExecute = false }
                   ).WaitForExit(10000);

                Console.WriteLine("< " + a + " " + new { sw.ElapsedMilliseconds });

            };

            Func<string, string> get_adb = a =>
            {
                var p = System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo(adb, a)
                    {
                        UseShellExecute = false,

                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true
                    }
                );

                var output = p.StandardOutput.ReadToEnd();

                return output;
            };



            {
//                ---------------------------
//Microsoft Visual Studio
//---------------------------
//Unable to set the next statement.Operation not supported.Unknown error: 0x80004005.
//-------------------------- -
//OK
//-------------------------- -


              var shell_dumpsys_battery = get_adb("-s " + device + "  shell dumpsys battery");
                var itemperature = int.Parse(shell_dumpsys_battery.SkipUntilIfAny("temperature: ").TakeUntilOrEmpty("\r")) / 10.0;
                var temperature = itemperature + "C";
                var level = int.Parse(shell_dumpsys_battery.SkipUntilIfAny("level: ").TakeUntilOrEmpty("\r")) + "%";

                if (itemperature > 51)
                {
                    // "x:\util\android-sdk-windows\platform-tools\adb.exe" shell sendevent /dev/input/event4 1 116 1
                    // "x:\util\android-sdk-windows\platform-tools\adb.exe" shell sendevent /dev/input/event4 1 116 0

                    do_adb("-s " + device + " shell input keyevent 4");
                    do_adb("-s " + device + " shell input keyevent 82");
                    do_adb("-s " + device + " shell input keyevent 26");

                    Console.WriteLine("device too hot");
                    Console.ReadKey();

                }

            }

            retry:


            do_adb("devices");


            // while this will wake the device up
            // it has its cool swipe screen.
            // 

            // http://marian.schedenig.name/2014/07/03/remote-control-your-android-phone-through-adb/

            //sendevent /dev/input/event4 1 116 1
            //sleep 1 # you may want to include this line, especially if you use this code in a script
            //sendevent /dev/input/event4 1 116 0

            // http://ptspts.blogspot.com/2012/09/how-to-unlock-android-phone-using-adb.html
            // Run adb shell input text PASSWORD, replacing PASSWORD with your Android unlock password.
            // Run adb shell input keyevent 66 to simulate pressing the Enter key. (See this page for event codes of other keys.)

            // this will awaken the device from black screen

            // "x:\util\android-sdk-windows\platform-tools\adb.exe" shell sendevent /dev/input/event4 1 116 1
            // "x:\util\android-sdk-windows\platform-tools\adb.exe" shell sendevent /dev/input/event4 1 116 0
            // "x:\util\android-sdk-windows\platform-tools\adb.exe" shell input text 0000
            // "x:\util\android-sdk-windows\platform-tools\adb.exe" shell input keyevent 66 

            // dumpsys SurfaceFlinger
            // how do we know if we are sleeping?

            // "x:\util\android-sdk-windows\platform-tools\adb.exe" dumpsys battery
            // "x:\util\android-sdk-windows\platform-tools\adb.exe" dumpsys SurfaceFlinger
            // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell dumpsys activity

            //X:\vr\tape1>"x:\util\android-sdk-windows\platform-tools\adb.exe" shell dumpsys battery
            //Current Battery Service state:
            //  AC powered: false
            //  USB powered: true
            //  status: 5
            //  health: 2
            //  present: true
            //  level: 100
            //  scale: 100
            //  voltage:4161
            //  temperature: 330
            //  technology: Li-ion


            //X:\vr\tape1>"x:\util\android-sdk-windows\platform-tools\adb.exe" shell dumpsys power
            //Power Manager State:
            //  mIsPowered=true mPowerState=0 mScreenOffTime=1186133 ms
            //  mPartialCount=0
            //  mWakeLockState=
            //  mUserState=
            //  mPowerState=
            //  mLocks.gather=
            //  mNextTimeout=183974785 now=185147955 -1173s from now
            //  mDimScreen=true mStayOnConditions=0
            //  mScreenOffReason=3 mUserState=0
            //  mBroadcastQueue={-1,-1,-1}
            //  mBroadcastWhy={0,0,0}
            //  mPokey=0 mPokeAwakeonSet=false
            //  mKeyboardVisible=false mUserActivityAllowed=false
            //  mKeylightDelay=3000 mDimDelay=590000 mScreenOffDelay=7000
            //  mPreventScreenOn=false  mScreenBrightnessOverride=-1  mButtonBrightnessOverride=-1
            //  mScreenOffTimeoutSetting=600000 mMaximumScreenOffTimeout=2147483647
            //  mLastScreenOnTime=0
            //  mBroadcastWakeLock=UnsynchronizedWakeLock(mFlags=0x1 mCount=0 mHeld=false)
            //  mStayOnWhilePluggedInScreenDimLock=UnsynchronizedWakeLock(mFlags=0x6 mCount=0 mHeld=false)
            //  mStayOnWhilePluggedInPartialLock=UnsynchronizedWakeLock(mFlags=0x1 mCount=0 mHeld=false)
            //  mPreventScreenOnPartialLock=UnsynchronizedWakeLock(mFlags=0x1 mCount=0 mHeld=false)
            //  mProximityPartialLock=UnsynchronizedWakeLock(mFlags=0x1 mCount=0 mHeld=false)
            //  mProximityWakeLockCount=0
            //  mProximitySensorEnabled=false
            //  mProximitySensorActive=false
            //  mProximityPendingValue=-1
            //  mLastProximityEventTime=0
            //  mLightSensorEnabled=false
            //  mLightSensorValue=-1.0 mLightSensorPendingValue=-1.0
            //  mLightSensorScreenBrightness=116 mLightSensorButtonBrightness=0 mLightSensorKeyboardBrightness=0
            //  mUseSoftwareAutoBrightness=true
            //  mAutoBrightessEnabled=false
            //  mScreenBrightness: animating=false targetValue=0 curValue=0.0 delta=-20.0

            //mLocks.size=0:

            //mPokeLocks.size=0:
            // http://android.stackexchange.com/questions/14001/what-is-preventing-my-android-phone-from-going-to-sleep-it-is-eating-through-th

            // http://developer.android.com/tools/help/adb.html

            // -d	Direct an adb command to the only attached USB device.

            var shell_dumpsys_power = get_adb("-s " + device + " shell dumpsys power");

            //var mDimScreen = shell_dumpsys_power.Contains("mDimScreen=true");
            var SCREEN_ON_BIT = shell_dumpsys_power.Contains("SCREEN_ON_BIT");
            //var SCREEN_ON_BIT = shell_dumpsys_power.Contains("mLastScreenOnTime=0");

            //   mLastScreenOnTime=0
            if (!SCREEN_ON_BIT)
            {
                // wake up, neo

                do_adb("-s " + device + "  shell sendevent /dev/input/event4 1 116 1");
                Thread.Sleep(1);
                do_adb("-s " + device + "  shell sendevent /dev/input/event4 1 116 0");
                do_adb("-s " + device + "  shell input text 0000");
                do_adb("-s " + device + "  shell input keyevent 66");

                Thread.Sleep(1000);
                goto retry;
            }

            Thread.Sleep(7000);

            do_adb("-s " + device + "  shell \"am start -a android.media.action.IMAGE_CAPTURE\" ");

            //E/CameraEngine( 2941): Exception while compressing image.
            //E/CameraEngine( 2941): android.view.ViewRoot$CalledFromWrongThreadException: Only the original thread that created a view hierarchy can touch its views.
            //E/CameraEngine( 2941):  at android.view.ViewRoot.checkThread(ViewRoot.java:3020)
            //E/CameraEngine( 2941):  at android.view.ViewRoot.invalidateChild(ViewRoot.java:647)
            //E/CameraEngine( 2941):  at android.view.ViewRoot.invalidateChildInParent(ViewRoot.java:673)
            //E/CameraEngine( 2941):  at android.view.ViewGroup.invalidateChild(ViewGroup.java:2511)
            //E/CameraEngine( 2941):  at android.view.View.invalidate(View.java:5332)
            //E/CameraEngine( 2941):  at android.view.View.setBackgroundDrawable(View.java:7679)
            //E/CameraEngine( 2941):  at com.sec.android.app.camera.widget.TwImageButton.setButtonDrawable(TwImageButton.java:356)
            //E/CameraEngine( 2941):  at com.sec.android.app.camera.ThumbnailController.updateThumb(ThumbnailController.java:228)
            //E/CameraEngine( 2941):  at com.sec.android.app.camera.ThumbnailController.setData(ThumbnailController.java:96)
            //E/CameraEngine( 2941):  at com.sec.android.app.camera.Camera.setLastPictureThumb(Camera.java:3311)
            //E/CameraEngine( 2941):  at com.sec.android.app.camera.Camera.updateThumbnail(Camera.java:3300)
            //E/CameraEngine( 2941):  at com.sec.android.app.camera.CameraEngine$ShootingModeManager.storeImage(CameraEngine.java:2280)
            //E/CameraEngine( 2941):  at com.sec.android.app.camera.CameraEngine$ShootingModeManager.access$1900(CameraEngine.java:2084)
            //E/CameraEngine( 2941):  at com.sec.android.app.camera.CameraEngine$ShootingModeManager$1.run(CameraEngine.java:2167)
            //E/CameraEngine( 2941):  at java.lang.Thread.run(Thread.java:1019)
            //V/CameraEngine( 2941): got message...{ what=7 when=-1ms }

            Thread.Sleep(3000);

            // Samsung Galaxy S. Samsung Galaxy S smartphone with 4.00-inch 480x800 display powered by 1GHz processor alongside 512MB RAM and 5-megapixel rear camera.
            // x:\util\android-sdk-windows\platform-tools\adb.exe" -d shell "input tap 750 240"
            //do_adb("-d shell \"input tap 750 240\" ");
            // s1 does ot know about tap.

            do_adb("-s " + device + "  shell \"input keyevent 27\" ");



            {
                var a = "-s " + device + "  shell ls -l \"/sdcard/DCIM/Camera/2015-08-*.jpg\"";

                var p = System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo(adb, a)
                    {
                        UseShellExecute = false,

                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true
                    }
                );

                var output = p.StandardOutput.ReadToEnd();
                //p.WaitForExit();

                // -rwxrwx--- root     sdcard_r 366450839 2015-04-30 18:39 20150430_183825.mp4

                var files = output.Split('\n');

                foreach (var file in files)
                {
                    // -rwxrwxr-x system   sdcard_rw  1621879 2015-07-25 11:43 2015-07-25 11.43.09.jpg

                    var filename = file.Trim().SkipUntilOrEmpty(":").SkipUntilOrEmpty(" ");

                    if (string.IsNullOrEmpty(filename))
                        continue;

                    if (filename == "No such file or directory")
                    {
                        Console.Beep();
                        continue;
                    }

                    // filename = "20150430_183825.mp4"

                    Console.WriteLine(filename);

                    recheck:;
                    var shell_dumpsys_battery = get_adb("-s " + device + "  shell dumpsys battery");
                    var itemperature = int.Parse(shell_dumpsys_battery.SkipUntilIfAny("temperature: ").TakeUntilOrEmpty("\r")) / 10.0;
                    var temperature = itemperature + "C";
                    var level = int.Parse(shell_dumpsys_battery.SkipUntilIfAny("level: ").TakeUntilOrEmpty("\r")) + "%";

                    if (itemperature > 51)
                    {
                        // "x:\util\android-sdk-windows\platform-tools\adb.exe" shell sendevent /dev/input/event4 1 116 1
                        // "x:\util\android-sdk-windows\platform-tools\adb.exe" shell sendevent /dev/input/event4 1 116 0

                        do_adb("-s " + device + " shell input keyevent 26");

                        Console.WriteLine("device too hot");
                        Console.ReadKey();

                        goto recheck;
                    }

                    var i = Directory.GetFiles(storage).Count();

                    var iNNNN = i.ToString("0000");
                    Console.Title = "S1" + new { iNNNN, temperature, level }.ToString();


                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo(adb, "-s " + device + "  pull -p \"/sdcard/DCIM/Camera/" + filename + "\" \"" + storage + "/" + iNNNN + ".jpg" + "\"")
                        {
                            UseShellExecute = false,
                        }
                    ).WaitForExit();

                    ;

                    System.Diagnostics.Process.Start(
                      new System.Diagnostics.ProcessStartInfo(adb, "-s " + device + "  shell rm \"/sdcard/DCIM/Camera/" + filename + "\"")
                      {
                          UseShellExecute = false,
                      }
                  ).WaitForExit();

                }
            }

            Thread.Sleep(1000);

            goto retry;
        }
    }
}
