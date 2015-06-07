using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.provider;
using android.view;
using android.webkit;
using android.widget;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLibJava.Extensions;

namespace AndroidNotificationActivity.Activities
{


    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141207
        // how can we shot this from NDK?
        // what if we are in vr?
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150607-1/surfaceview


        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // cmd /K c:\util\android-sdk-windows\platform-tools\adb.exe logcat
            // Camera PTP

            // http://developer.android.com/guide/topics/ui/notifiers/notifications.html

            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);

            sv.addView(ll);


            int counter = 0;

            Action yield = delegate
            {
                counter++;
                var nm = (NotificationManager)this.getSystemService(Activity.NOTIFICATION_SERVICE);


                // see http://developer.android.com/reference/android/app/Notification.html
                var notification = new Notification(
                    android.R.drawable.stat_notify_sync,
                    "not used?",

                    0
                    //java.lang.System.currentTimeMillis()
                );

                notification.defaults |= Notification.DEFAULT_SOUND;
                //notification.vibrate = new long[] { 100, 200, 200, 200, 200, 200, 1000, 200, 200, 200, 1000, 200 };
                notification.vibrate = new long[] { 1000 };

                //             script: error JSC1000: Java : unable to emit newarr at 'AndroidNotificationActivity.Activities.ApplicationActivity+<>c__DisplayClass2.<onCreate>b__0'#004c: System.Int64, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
                //at jsc.Languages.Java.JavaCompiler.<>c__DisplayClass1a7.<>c__DisplayClass1be.<CreateInstructionHandlers>b__162() in x:\jsc.internal.git\compiler\jsc\Languages\Java\JavaCompiler.OpCodes.cs:line 1720

                var notificationIntent = new Intent(this, typeof(ApplicationActivity).ToClass());
                var contentIntent = PendingIntent.getActivity(this, 0, notificationIntent, 0);


                notification.setLatestEventInfo(
                    this,
                    "Visible in Gear VR!",
                    "yet it wont vibrate",
                    contentIntent);

                //notification.defaults |= Notification.DEFAULT_VIBRATE;
                //notification.defaults |= Notification.DEFAULT_LIGHTS;
                // http://androiddrawableexplorer.appspot.com/
                nm.notify(counter, notification);
            };


            var b = new Button(this);

            b.setText("Notify!");

            // ScriptCoreLib.Ultra ?
            b.AtClick(
                delegate
                {
                    yield();
                }
            );

            ll.addView(b);

            this.setContentView(sv);

            // X:\jsc.svn\examples\java\android\HelloOpenGLES20Activity\HelloOpenGLES20Activity\ScriptCoreLib.Android\Shader.cs

            // Error	1	'AndroidNotificationActivity.Activities.ApplicationActivity' does not contain a definition for 'ShowLongToast' and no extension method 'ShowLongToast' accepting a first argument of type 'AndroidNotificationActivity.Activities.ApplicationActivity' could be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\examples\java\android\AndroidNotificationActivity\AndroidNotificationActivity\ApplicationActivity.cs	80	18	AndroidNotificationActivity
            //this.ShowLongToast("http://jsc-solutions.net");
            this.ShowToast("http://jsc-solutions.net");

            yield();

        }




    }
}
