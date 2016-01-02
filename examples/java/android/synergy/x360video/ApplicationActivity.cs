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

namespace x360video.Activities
{
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
    public class ApplicationActivity : Activity
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


            b.AtClick(
                delegate
                {
                    SetClipboard("hello");
                }
            );

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


    }
}
