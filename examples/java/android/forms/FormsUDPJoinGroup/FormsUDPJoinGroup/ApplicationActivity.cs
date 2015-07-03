using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Extensions.Android;
using System.Windows.Forms;
using ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms;
using android.net.wifi;
using android.content;

namespace FormsUDPJoinGroup.Activities
{
    public class ApplicationActivity : Activity
    {
        // ok lets test it.
        // get the android. unlock, enable wifi.
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  tcpip 5555
        // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect 192.168.1.126:5555
        // x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "DEBUG" "xNativeActivity" "System.Console"

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);

            // http://developer.android.com/reference/android/net/wifi/WifiManager.html
            // http://developer.android.com/reference/android/net/wifi/WifiManager.html#createMulticastLock(java.lang.String)
            ((WifiManager)this.getSystemService(Context.WIFI_SERVICE)).createWifiLock(WifiManager.WIFI_MODE_FULL_HIGH_PERF, "ApplicationActivity").acquire();
            ((WifiManager)this.getSystemService(Context.WIFI_SERVICE)).createMulticastLock("ApplicationActivity").acquire();

            InitializeContent();
        }

        private void InitializeContent()
        {
            // works for clr, will it work on android?

            // http://android-developers.blogspot.com/2011/11/new-layout-widgets-space-and-gridlayout.html

            var r = default(global::ScriptCoreLib.Android.Windows.Forms.IAssemblyReferenceToken_Forms);

            var u = new ApplicationControl();

            u.AttachTo(this);


        }


    }
}

// method: System.Windows.Forms.Form FindForm()