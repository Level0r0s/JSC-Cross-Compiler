using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;
using System.IO;
using System.Diagnostics;

namespace ADBS6CameraTimelapser
{
    class Program
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151227/rotator
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150727

        static void Main(string[] args)
        {
        restart: ;
            int pendingimages = 0;

            Console.WriteLine("prep for channel jump?");



            //netsh wlan show hostednetwork

            //System.Diagnostics.Process.Start(
            //    //new System.Diagnostics.ProcessStartInfo("netsh", "wlan show hostednetwork setting=security") { UseShellExecute = false }
            //    new System.Diagnostics.ProcessStartInfo("netsh", "wlan show hostednetwork ") { UseShellExecute = false }
            //).WaitForExit();


            //            Hosted network settings
            //            ---------------------- -
            //                Mode                   : Allowed
            //                SSID name:
            //            "..red"
            //    Max number of clients  : 100
            //    Authentication:
            //            WPA2 - Personal
            //    Cipher:
            //            CCMP

            //Hosted network status
            //---------------------
            //    Status                 : Not started

            // https://msdn.microsoft.com/en-us/library/windows/desktop/dd815243(v=vs.85).aspx


            // restart network?
            // this will kick android away

            // System.Diagnostics.Process.Start(
            // new System.Diagnostics.ProcessStartInfo("netsh", "wlan start hostednetwork") { UseShellExecute = false }
            //  ).WaitForExit();

            // //            The hosted network couldn't be started.
            // //The group or resource is not in the correct state to perform the requested operation.

            // Thread.Sleep(500);

            // System.Diagnostics.Process.Start(
            //    new System.Diagnostics.ProcessStartInfo("ipconfig", "") { UseShellExecute = false }
            //).WaitForExit();

            Console.WriteLine("android device should now have been connected tto our network with a static address of 192.168.173.5");

            // wait for the device?
            // any device?



            // System.Diagnostics.Process.Start(
            //     //new System.Diagnostics.ProcessStartInfo("netsh", "wlan show hostednetwork setting=security") { UseShellExecute = false }
            //    new System.Diagnostics.ProcessStartInfo("netsh", "wlan show hostednetwork ") { UseShellExecute = false }
            //).WaitForExit();






            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151227/rotator
            // get device , on lapop turn on wifi

            // r


            // http://blogs.msdn.com/b/rds/archive/2009/03/03/top-10-rdp-protocol-misconceptions-part-1.aspx
            // if i am a remoteapp in one network
            // what if i want to become a tsclient running on tsclient cpu on another network?

            //var device = "192.168.43.1:5555";
            var device = "192.168.1.126:5555";
            //var device = "192.168.173.5:5555";
            //var device = "02157df2d5d4e70b";
            var storage = "x:/vr/tape30";

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
            //{ ApplicationBase = X:\jsc.svn\examples\rewrite\ADBCameraTimelapser\ADBS6CameraTimelapser\bin\Debug\ }
            //{ CommandLine = "X:\jsc.svn\examples\rewrite\ADBCameraTimelapser\ADBS6CameraTimelapser\bin\Debug\ADBS6CameraTimelapser.vshost.exe"  }
            //{ ProcessorCount = 8 }
            //{ device = 192.168.1.126:5555 }
            //{ storage = x:/vr/tape6 }
            //hi




            if (!Debugger.IsAttached)
                Thread.Sleep(1000);


            new DirectoryInfo(storage).Create();

            // Additional information: Cannot create a file when that file already exists.

            //var ffmpegNNNNfiles = Directory.GetFiles(storage).OrderBy(x => new FileInfo(x).LastWriteTime).Select(
            //    (x, i) =>
            //    {
            //        var iNNNN = i.ToString("0000");
            //        var t = Path.GetDirectoryName(x) + "/" + iNNNN + ".jpg";

            //        if (new FileInfo(x).FullName != new FileInfo(t).FullName)
            //        {
            //            File.Move(
            //                x, t

            //            );
            //        }


            //        return new { x, i, iNNNN };
            //    }
            //).ToArray();


            var adb = @"x:\util\android-sdk-windows\platform-tools\adb.exe";

            Action<string> do_adb = a =>
            {
                Console.WriteLine("> " + a);


                var f = System.Diagnostics.Process.Start(
                                   new System.Diagnostics.ProcessStartInfo(adb, a) { UseShellExecute = false }
                                   ).WaitForExit(18000);

                Console.WriteLine("< " + new { f, a });

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

            var roundtrip = Stopwatch.StartNew();

            // c

            do_adb("kill-server");
            do_adb("start-server");
            do_adb("devices");

            //            Pinging 192.168.173.5 with 32 bytes of data:
            //            Reply from 192.168.173.5: bytes = 32 time = 2293ms TTL = 64
            //Reply from 192.168.173.5: bytes = 32 time = 65ms TTL = 64
            //Reply from 192.168.173.5: bytes = 32 time = 795ms TTL = 64
            //Reply from 192.168.173.5: bytes = 32 time = 704ms TTL = 64

            //Ping statistics for 192.168.173.5:
            //    Packets: Sent = 4, Received = 4, Lost = 0(0 % loss),
            //Approximate round trip times in milli - seconds:
            //    Minimum = 65ms, Maximum = 2293ms, Average = 964ms
            //> connect 192.168.173.5:5555
            //unable to connect to :5555

            //System.Diagnostics.Process.Start(
            //    new System.Diagnostics.ProcessStartInfo("ping ", device.TakeUntilIfAny(":")) { UseShellExecute = false }
            //).WaitForExit();

            //// fuck you adb. be reliable. kill adb?
            //  "x:\util\android-sdk-windows\platform-tools\adb.exe"  tcpip 5555
            var connected = get_adb("connect " + device);

            //List of devices attached
            //192.168.1.126:5555      offline

            //< devices
            //> -s 192.168.1.126:5555 shell dumpsys battery
            //error: device unauthorized.
            //This adb server's $ADB_VENDOR_KEYS is not set
            //Try 'adb kill-server' if that seems wrong.
            //Otherwise check for a confirmation dialog on your device.
            //< -s 192.168.1.126:5555 shell dumpsys battery
            //check camera.
            //> -s 192.168.1.126:5555 disconnect
            //disconnected everything
            //< -s 192.168.1.126:5555 disconnect


            Console.WriteLine(new { connected });

            //// wtf?
            if (!connected.Contains("connected to"))
            {
                Debugger.Break();

                // 192.168.1.126:5555      offline

                Console.WriteLine("device lost...");
                Thread.Sleep(15000);

                goto restart;
            }

            // connected = "connected to 192.168.173.5:5555\r\n"


            // restart android?
            // have android reconnect wifi?
            // unable to connect to :5555

            //Thread.Sleep(1300);
            // ping 192.168.173.5
            // connected to 192.168.173.5:5555
            // 192.168.173.5:5555      offline
            // error: device offline
            // unable to connect to :5555


            do_adb("devices");

            //> devices
            //List of devices attached
            //192.168.1.126:5555      offline

            goto noreset;
            do_adb("-s " + device + " shell \"am force-stop com.sec.android.app.camera\" ");
            Thread.Sleep(1300);
            do_adb("-s " + device + " shell \"am start -n com.sec.android.app.camera/.Camera\" ");
            Thread.Sleep(4300);
        noreset: ;

            //            < devices
        //> connect 192.168.1.126:5555
        //unable to connect to :5555
        // error: device offline
        // error: device offline
        // error: device not found
        //do_adb("disconnect " + device);
        retry:
            // did the wifi got disconnected?

            // restart adb or s6?
            //do_adb("connect " + device);
            ////unable to connect to 192.168.1.126:5555:5555
            //do_adb("devices");
            //Thread.Sleep(1000);

            //> devices
            //List of devices attached
            //3330A17632C000EC        device
            //192.168.1.126:5555      offline

            //< devices
            //> devices
            //List of devices attached
            //3330A17632C000EC        device
            //192.168.1.126:5555      device



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

                Debugger.Break();

                do_adb("-s " + device + " disconnect");

                Thread.Sleep(15000);

                do_adb("devices");


                goto retry;
            }

            //var mDimScreen = shell_dumpsys_power.Contains("mDimScreen=true");
            //var SCREEN_ON_BIT = shell_dumpsys_power.Contains("SCREEN_ON_BIT");
            var SCREEN_ON_BIT = shell_dumpsys_power.Contains("Display Power: state=ON");
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

            var now = DateTime.Now;

            Console.WriteLine(new { now, roundtrip.ElapsedMilliseconds });
            roundtrip.Restart();

            // focus

            //do_adb("-s " + device + " shell \"input tap 2200 900\" ");
            //do_adb("-s " + device + " shell \"input tap 1200 900\" ");

            //Thread.Sleep(300);
            //do_adb("-s " + device + " shell \"input tap 1200 900\" ");

            Thread.Sleep(300);
            //do_adb("-s " + device + " shell \"input tap 1400 300\" ");

            //Thread.Sleep(300);


            //// focus
            //do_adb("-s " + device + " shell \"input tap 1400 300\" ");

            //Thread.Sleep(800);


            //do_adb("-s " + device + " shell \"input tap 2300 700\" ");



            //shell@zerolte:/sdcard/DCIM/CardboardCamera $ ls
            //ls
            //IMG_20151204_000246.vr.jpg


            // are we portrait?
            //do_adb("-s " + device + " shell \"input tap 700 2300\" ");
            do_adb("-s " + device + " shell \"input tap 1300 2300\" ");
            //do_adb("-s " + device + " shell \"input tap 100 700\" ");
            Thread.Sleep(600);
            do_adb("-s " + device + " shell \"input tap 700 2300\" ");
            pendingimages++;

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
            //Thread.Sleep(600);

            // at night we need more time
            Console.WriteLine("rotating...");
            //Thread.Sleep(35900);

            // EXTRA SLOW ROTATE?
            Thread.Sleep(60000);


            //shell@zerolte:/sdcard/DCIM/CardboardCamera $ ls
            //ls
            //IMG_20151204_000246.vr.jpg

            // /sdcard/DCIM/Camera/20150727_211544.jpg
            {
                //var a = "-s 192.168.1.126:5555 shell ls -l \"/sdcard/DCIM/Camera/20150727*.jpg\"";
                //var a = "-s " + device + " shell ls -l \"/sdcard/DCIM/Camera/2015" + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "*.jpg\"";
                var a = "-s " + device + " shell ls -l \"/sdcard/DCIM/CardboardCamera/IMG_2015" + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "*.jpg\"";

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

                    // is the file ready?
                    Thread.Sleep(700);

                    ;

                    var i = Directory.GetFiles(storage).Count();

                    var iNNNN = i.ToString("00000");

                    //    var args = "shell dumpsys battery";
                recheck: ;
                    var shell_dumpsys_battery = get_adb("-s " + device + " shell dumpsys battery");

                    //Unhandled Exception: System.FormatException: Input string was not in a correct format.
                    //   at System.Number.StringToNumber(String str, NumberStyles options, NumberBuffer& number, NumberFormatInfo info, Boolean parseDecimal)
                    //   at System.Number.ParseInt32(String s, NumberStyles style, NumberFormatInfo info)
                    //   at System.Int32.Parse(String s)
                    //   at ADBS6CameraTimelapser.Program.Main(String[] args) in x:\jsc.svn\examples\rewrite\ADBCameraTimelapser\ADBS6CameraTimelapser\Program.cs:line 301

                    //temperature: 405
                    // temperature/10+"C" 
                    var itemperature = int.Parse(shell_dumpsys_battery.SkipUntilIfAny("temperature: ").TakeUntilOrEmpty("\r")) / 10.0;
                    var temperature = itemperature + "C";
                    var level = int.Parse(shell_dumpsys_battery.SkipUntilIfAny("level: ").TakeUntilOrEmpty("\r")) + "%";
                    var AC = shell_dumpsys_battery.Contains("AC powered: true");
                    var currentnow = shell_dumpsys_battery.SkipUntilIfAny("current now: ").TakeUntilOrEmpty("\r");
                    if (itemperature > 45)
                    {
                        // "x:\util\android-sdk-windows\platform-tools\adb.exe" shell sendevent /dev/input/event4 1 116 1
                        // "x:\util\android-sdk-windows\platform-tools\adb.exe" shell sendevent /dev/input/event4 1 116 0

                        do_adb("-s " + device + " shell input keyevent 26");

                        Console.WriteLine("device too hot");
                        Console.ReadKey();
                        goto recheck;
                    }



                    Console.Title = "S6" + new { iNNNN, temperature, level, currentnow }.ToString();

                    // http://forum.xda-developers.com/showthread.php?t=1941201
                    // 58KBps??
                    //do_adb("-s " + device + " pull -p \"/sdcard/DCIM/Camera/" + filename + "\" \"" + storage + "/" + iNNNN + ".jpg" + "\"");
                    do_adb("-s " + device + " pull -p \"/sdcard/DCIM/CardboardCamera/" + filename + "\" \"" + storage + "/" + iNNNN + ".jpg" + "\"");
                    ;
                    // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeEquirectangularCameraExperiment\ChromeEquirectangularCameraExperiment\Application.cs

                    do_adb("-s " + device + " shell rm \"/sdcard/DCIM/CardboardCamera/" + filename + "\"");
                    //do_adb("-s " + device + " shell rm \"/sdcard/DCIM/Camera/" + filename + "\"");
                    pendingimages--;
                    if (pendingimages < 0)
                        pendingimages = 0;

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

                if (pendingimages > 0)
                {
                    Console.WriteLine("rendering?...");
                    Thread.Sleep(20000);
                }

                if (pendingimages > 1)
                {
                    // we are behind. let the device render
                    Console.WriteLine("rendering?...... backlog? battery save mode? " + new { pendingimages });
                    Thread.Sleep(10000 * pendingimages);

                }



                #region check camera
                if (false)
                    if (beep)
                    {
                        // http://community.spiceworks.com/topic/163415-we-want-the-beep-sound-from-remote-desktop-server
                        Console.WriteLine("check camera.");
                        Console.Beep();

                    recheck: ;
                        var shell_dumpsys_battery = get_adb("-s " + device + " shell dumpsys battery");

                        //Unhandled Exception: System.FormatException: Input string was not in a correct format.
                        //   at System.Number.StringToNumber(String str, NumberStyles options, NumberBuffer& number, NumberFormatInfo info, Boolean parseDecimal)
                        //   at System.Number.ParseInt32(String s, NumberStyles style, NumberFormatInfo info)
                        //   at System.Int32.Parse(String s)
                        //   at ADBS6CameraTimelapser.Program.Main(String[] args) in x:\jsc.svn\examples\rewrite\ADBCameraTimelapser\ADBS6CameraTimelapser\Program.cs:line 301

                        // 		shell_dumpsys_battery	Internal error in the expression evaluator.	


                        //temperature: 405
                        // temperature/10+"C" 
                        var xtemperature = shell_dumpsys_battery.SkipUntilIfAny("temperature: ").TakeUntilOrEmpty("\r");
                        if (string.IsNullOrEmpty(xtemperature))
                        {
                            Console.WriteLine(" adb failed? 3:34?");
                            Thread.Sleep(10000);
                            goto restart;
                        }


                        var itemperature = int.Parse(xtemperature) / 10.0;
                        var temperature = itemperature + "C";
                        var level = int.Parse(shell_dumpsys_battery.SkipUntilIfAny("level: ").TakeUntilOrEmpty("\r")) + "%";
                        var AC = shell_dumpsys_battery.Contains("AC powered: true");

                        if (itemperature > 45)
                        {
                            // "x:\util\android-sdk-windows\platform-tools\adb.exe" shell sendevent /dev/input/event4 1 116 1
                            // "x:\util\android-sdk-windows\platform-tools\adb.exe" shell sendevent /dev/input/event4 1 116 0

                            do_adb("-s " + device + " shell input keyevent 26");

                            Console.WriteLine("device too hot");
                            Console.ReadKey();
                            goto recheck;
                        }

                        Console.Title = "S6" + new { temperature, level, AC }.ToString();

                        if (!Debugger.IsAttached)
                            Thread.Sleep(15000);

                        // compensation?

                        do_adb("-s " + device + " shell \"am force-stop com.sec.android.app.camera\" ");
                        Thread.Sleep(1300);
                        do_adb("-s " + device + " shell \"am start -n com.sec.android.app.camera/.Camera\" ");
                        Thread.Sleep(4300);
                    }
                #endregion




            }




            //Thread.Sleep(1000);

            goto retry;
        }
    }
}

//---------------------------
//Microsoft Visual Studio
//---------------------------
//Unable to detach from one or more processes.Detach is illegal after an Edit and Continue on a module.
//---------------------------
//OK
//---------------------------
