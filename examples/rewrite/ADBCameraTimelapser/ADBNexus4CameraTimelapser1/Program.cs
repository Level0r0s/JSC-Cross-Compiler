using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;
using System.IO;
using System.Diagnostics;

namespace ADBNexus4CameraTimelapser1
{
    class Program
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150727

        static void Main(string[] args)
        {
            // "x:\util\android-sdk-windows\platform-tools\adb.exe"  tcpip 5555


            // http://blogs.msdn.com/b/rds/archive/2009/03/03/top-10-rdp-protocol-misconceptions-part-1.aspx
            // if i am a remoteapp in one network
            // what if i want to become a tsclient running on tsclient cpu on another network?

            var device = "192.168.1.139:5555";
            var storage = "x:/vr/tape4";

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

            // < -s 192.168.1.126:5555 pull -p "/sdcard/DCIM/Camera/20150729_131851.jpg" "x:/vr/tape6/2400.jpg"

            Console.WriteLine("hi");
            //{ ApplicationBase = X:\jsc.svn\examples\rewrite\ADBCameraTimelapser\ADBNexus4CameraTimelapser1\bin\Debug\ }
            //{ CommandLine = "X:\jsc.svn\examples\rewrite\ADBCameraTimelapser\ADBNexus4CameraTimelapser1\bin\Debug\ADBNexus4CameraTimelapser1.vshost.exe"  }
            //{ ProcessorCount = 8 }
            //{ device = 192.168.1.126:5555 }
            //{ storage = x:/vr/tape6 }
            //hi




            if (!Debugger.IsAttached)
                Thread.Sleep(1000);


            new DirectoryInfo(storage).Create();

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
                Console.WriteLine("> " + a);

                System.Diagnostics.Process.Start(
                   new System.Diagnostics.ProcessStartInfo(adb, a) { UseShellExecute = false }
                   ).WaitForExit();

                Console.WriteLine("< " + a);

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

            //"x:\util\android-sdk-windows\platform-tools\adb.exe" connect 192.168.1.126:5555

            // wifi on?
            //List of devices attached
            //3330A17632C000EC        device
            //192.168.1.126:5555      offline

            // RSA allow dialog!
            //do_adb("disconnect " + device);

            // 192.168.1.139:5555      offline
            // error: device unauthorized. Please check the confirmation dialog on your device.

            // phone needs to be in the launcher view?
            do_adb("connect " + device);

            do_adb("devices");

            //do_adb("-s " + device + " shell \"am force-stop com.sec.android.app.camera\" ");
            //Thread.Sleep(1300);
            //do_adb("-s " + device + " shell \"am start -n com.sec.android.app.camera/.Camera\" ");
            //Thread.Sleep(4300);

            // error: device unauthorized. Please check the confirmation dialog on your device.

            //do_adb("-s " + device + " shell \"am start -a android.media.action.IMAGE_CAPTURE\" ");
            //Thread.Sleep(300);

            var roundtrip = Stopwatch.StartNew();
            goto first;
        retry:
            // did the wifi got disconnected?
            do_adb("connect " + device);

            do_adb("devices");

        first: ;

            // Debugger.Break();

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

            // 192.168.1.126:5555      device

            // "x:\util\android-sdk-windows\platform-tools\adb.exe" -s 192.168.1.126:5555  dumpsys SurfaceFlinger
            var shell_dumpsys_power = get_adb("-s " + device + " shell dumpsys power");

            if (string.IsNullOrEmpty(shell_dumpsys_power))
            {
                do_adb("-s " + device + " shell dumpsys battery");

                // error: closed

                Console.WriteLine("check camera.");
                Console.Beep();

                do_adb("-s " + device + " disconnect");

                Thread.Sleep(15000);

                do_adb("devices");


                goto retry;
            }

            //var mDimScreen = shell_dumpsys_power.Contains("mDimScreen=true");
            //var SCREEN_ON_BIT = shell_dumpsys_power.Contains("SCREEN_ON_BIT");
            var SCREEN_ON_BIT = shell_dumpsys_power.Contains("Display Blanker: blanked=false");
            //var SCREEN_ON_BIT = shell_dumpsys_power.Contains("Display Power: state=ON");
            //var SCREEN_ON_BIT = shell_dumpsys_power.Contains("mLastScreenOnTime=0");

            /// Display Power: state=ON
            /// 
            //   mLastScreenOnTime=0
            if (!SCREEN_ON_BIT)
            {
                // wake up, neo
                do_adb("-s " + device + " shell input keyevent 26");

                do_adb("-s " + device + " shell sendevent /dev/input/event4 1 116 1");
                Thread.Sleep(1);
                do_adb("-s " + device + " shell sendevent /dev/input/event4 1 116 0");
                do_adb("-s " + device + " shell input text 0000");
                do_adb("-s " + device + " shell input keyevent 66");

                Thread.Sleep(1000);
                goto retry;
            }

            // Google Nexus 4 has a 1.5GHz quad-core Snapdragon S4 Pro with Krait CPUs (meaning this thing should be crazy fast), a 4.7-inch WXGA True HD IPS Plus
            // (1280 x 768 pixels) display with Zerogap Touch technology and Corning Gorilla Glass 2, 2GB of RAM, an 8MP rear camera, a 1.3MP front camera, and a 2100 mAh battery rated 




            var now = DateTime.Now;

            Console.WriteLine(new { now, roundtrip.ElapsedMilliseconds });
            roundtrip.Restart();


            do_adb("-s " + device + " shell \"input tap 1080 400\" ");

            goto collect;

        //    //Thread.Sleep(7000);

        //    //  mFocusedActivity: ActivityRecord{7921885 u0 com.sec.android.app.camera/.Camera t6688}

        //    //do_adb("-s 192.168.1.126:5555 shell \"am start -a android.media.action.IMAGE_CAPTURE\" ");

        //    // x:\util\android-sdk-windows\platform-tools\adb.exe shell am force-stop OVRWindWheelActivity.Activities
        //    // x:\util\android-sdk-windows\platform-tools\adb.exe shell am start -n OVRWindWheelActivity.Activities/OVRWindWheelActivity.Activities.ApplicationActivity

        //    // x:\util\android-sdk-windows\platform-tools\adb.exe shell am force-stop OVRWindWheelActivity.Activities

        //    do_adb("-s 192.168.1.126:5555 shell \"am force-stop com.sec.android.app.camera\" ");
        //    Thread.Sleep(1300);
        //    do_adb("-s 192.168.1.126:5555 shell \"am start -n com.sec.android.app.camera/.Camera\" ");
        //    Thread.Sleep(4300);

        //    do_adb("-s 192.168.1.126:5555 shell \"input tap 2300 1300\" ");
        //    Thread.Sleep(3300);
        //    do_adb("-s 192.168.1.126:5555 shell \"input tap 1200 400\" ");

        //    Thread.Sleep(500);

        //    //if you know the exact position to touch for focusing the camera, you can use  adb shell input tap <x><y>

        //    // focus ´damnet
        //    //do_adb("-s 192.168.1.126:5555 shell \"input tap 700 400\" ");
        //    //Thread.Sleep(600);
        //    //do_adb("-s 192.168.1.126:5555 shell \"input tap 700 400\" ");
        //    //Thread.Sleep(600);
        //    //do_adb("-s 192.168.1.126:5555 shell \"input tap 700 400\" ");
        //    //Thread.Sleep(1600);


        //    do_adb("-s 192.168.1.126:5555 shell \"input keyevent 27\" ");

        //    //do_adb("-s 192.168.1.126:5555 shell \"input tap 1700 32\" ");

        //    //Thread.Sleep(1000);

        //    //do_adb("-s 192.168.1.126:5555 shell input keyevent 22");
        ////Thread.Sleep(1200);
        ////do_adb("-s 192.168.1.126:5555 shell input keyevent 66");

            collect: ;

            // how long will it take?
            Thread.Sleep(600);

            // /sdcard/DCIM/Camera/20150727_211544.jpg
            {
                // x:\util\android-sdk-windows\platform-tools\adb.exe -s 192.168.1.139:5555 shell ls -l "/sdcard/DCIM/Camera/IMG_20150729*.jpg"
                //var a = "-s 192.168.1.126:5555 shell ls -l \"/sdcard/DCIM/Camera/20150727*.jpg\"";

                // -rw-rw-r-- root     sdcard_rw  1177383 2012-07-12 20:27 IMG_20120712_202746.jpg
                var a = "-s " + device + " shell ls -l \"/sdcard/DCIM/Camera/IMG_201507" + DateTime.Now.Day.ToString("00") + "*.jpg\"";

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

                var beep = true;

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

                    ;

                    var i = Directory.GetFiles(storage).Count();

                    var iNNNN = i.ToString("0000");

                    //    var args = "shell dumpsys battery";
                    var shell_dumpsys_battery = get_adb("-s " + device + " shell dumpsys battery");

                    //Unhandled Exception: System.FormatException: Input string was not in a correct format.
                    //   at System.Number.StringToNumber(String str, NumberStyles options, NumberBuffer& number, NumberFormatInfo info, Boolean parseDecimal)
                    //   at System.Number.ParseInt32(String s, NumberStyles style, NumberFormatInfo info)
                    //   at System.Int32.Parse(String s)
                    //   at ADBNexus4CameraTimelapser1.Program.Main(String[] args) in x:\jsc.svn\examples\rewrite\ADBCameraTimelapser\ADBNexus4CameraTimelapser1\Program.cs:line 301

                    //temperature: 405
                    // temperature/10+"C" 
                    var temperature = int.Parse(shell_dumpsys_battery.SkipUntilIfAny("temperature: ").TakeUntilOrEmpty("\r")) / 10.0 + "C";
                    var level = int.Parse(shell_dumpsys_battery.SkipUntilIfAny("level: ").TakeUntilOrEmpty("\r")) + "%";
                    var AC = shell_dumpsys_battery.Contains("AC powered: true");
                    var BATTERY_STATUS_NOT_CHARGING = shell_dumpsys_battery.Contains("status: 4");

                    // http://stackoverflow.com/questions/13850192/how-to-lock-android-screen-via-adb

                    // why is nexus4 hovering at 50C and AC is not upping level?
                    //  temperature: 500
                    //  temperature: 370


                    // adb shell input keyevent 26 # sleep
                    //do_adb("-s " + device + " shell input keyevent 26");
                    // temperature: 410

                    //  level: 39
                    //  level: 44

                    //Current Battery Service state:
                    //  AC powered: true
                    //  USB powered: false
                    //  Wireless powered: false

                    // BATTERY_STATUS_NOT_CHARGING
                    // http://developer.android.com/reference/android/os/BatteryManager.html#BATTERY_STATUS_CHARGING
                    //  status: 4

                    //  health: 3
                    //  present: false
                    //  level: 39
                    //  scale: 100
                    //  voltage:3797
                    //  temperature: 500
                    //  technology: Li-ion


                    Console.Title = "N4" + new { iNNNN, level, temperature, AC, BATTERY_STATUS_NOT_CHARGING }.ToString();

                    // http://forum.xda-developers.com/showthread.php?t=1941201
                    // 58KBps??
                    do_adb("-s " + device + " pull -p \"/sdcard/DCIM/Camera/" + filename + "\" \"" + storage + "/" + iNNNN + ".jpg" + "\"");
                    ;

                    do_adb("-s " + device + " shell rm \"/sdcard/DCIM/Camera/" + filename + "\"");

                    beep = false;
                }

                // < devices
                //> -s 192.168.1.126:5555 shell input keyevent 26
                //error: device not found

                //                List of devices attached
                //3330A17632C000EC        device
                //192.168.1.126:5555      offline

                //    > devices
                //List of devices attached
                //3330A17632C000EC        device
                //192.168.1.126:5555      device

                // error: closed

                if (beep)
                {
                    // http://community.spiceworks.com/topic/163415-we-want-the-beep-sound-from-remote-desktop-server
                    Console.WriteLine("check camera.");
                    Console.Beep();

                    Thread.Sleep(15000);

                    // compensation?

                    //do_adb("-s " + device + " shell \"am force-stop com.sec.android.app.camera\" ");
                    //Thread.Sleep(1300);
                    //do_adb("-s " + device + " shell \"am start -n com.sec.android.app.camera/.Camera\" ");
                    //Thread.Sleep(4300);
                }

            }




            //Thread.Sleep(1000);

            goto retry;
        }
    }
}
