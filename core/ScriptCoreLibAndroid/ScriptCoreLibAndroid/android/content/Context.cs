using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using android.content.res;
using android.content.pm;
using java.io;

namespace android.content
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/content/Context.java

    // http://developer.android.com/reference/android/content/Context.html
    [Script(IsNative = true)]
    public abstract class Context
    {
        public abstract AssetManager getAssets();

        // X:\jsc.svn\examples\java\android\AndroidMultiProcTCPServerAsync\AndroidMultiProcTCPServerAsync\ApplicationActivity.cs
        public abstract java.io.File getFilesDir();

        // X:\jsc.svn\examples\javascript\android\com.abstractatech.battery\com.abstractatech.battery\ApplicationWebService.cs
        public abstract Intent registerReceiver(BroadcastReceiver receiver, IntentFilter filter);

        // Z:\jsc.svn\examples\java\android\AndroidUDPClipboard\ApplicationActivity.cs
        public static readonly string CLIPBOARD_SERVICE;

        public static readonly string SENSOR_SERVICE;
        public static readonly string ACTIVITY_SERVICE = "activity";

        public const int MODE_PRIVATE = 0;

        public static string WIFI_SERVICE;
        public static string NOTIFICATION_SERVICE;
        public static string WINDOW_SERVICE;
        public static string VIBRATOR_SERVICE;
        public static string CONNECTIVITY_SERVICE;


        // http://developer.android.com/reference/android/media/AudioManager.html
        public static string AUDIO_SERVICE;

        // members and types are to be extended by jsc at release build

        public abstract ContentResolver getContentResolver();

        public abstract void setTheme(int resid);


        public abstract Resources getResources();

        public abstract object getSystemService(string name);

        public abstract Context getApplicationContext();

        public abstract ComponentName startService(Intent service);

        public abstract void unregisterReceiver(BroadcastReceiver receiver);

        public abstract SharedPreferences getSharedPreferences(string arg0, int arg1);

        public abstract void startActivity(Intent intent);


        public abstract PackageManager getPackageManager();



        public abstract string getPackageName();


        public abstract File[] getExternalFilesDirs(string type);

    }
}
