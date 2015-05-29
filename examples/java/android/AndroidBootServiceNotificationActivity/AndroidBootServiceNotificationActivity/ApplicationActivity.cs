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
using AndroidBootServiceNotificationActivity.Activities;
//using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Threading.Tasks;
using System.Threading;

namespace AndroidBootServiceNotificationActivity.Activities
{
    // http://android-er.blogspot.com/2011/04/start-service-to-send-notification.html

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        // https://github.com/opersys/raidl


        // http://stackoverflow.com/questions/6274141/trigger-background-service-at-a-specific-time-in-android
        // http://stackoverflow.com/questions/7144908/how-is-an-intent-service-declared-in-the-android-manifest
        // http://developer.android.com/guide/topics/manifest/service-element.html

        //AtBootCompleted hack1;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://developer.android.com/guide/topics/ui/notifiers/notifications.html

            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);

            sv.addView(ll);

            #region startservice
            var startservice = new Button(this);
            startservice.setText("Start Service to send Notification");
            startservice.AtClick(
                delegate
                {
                    startservice.setEnabled(false);
                    this.ShowToast("startservice_onclick");

                    //var intent = new Intent(this, NotifyService.Class);
                    var intent = new Intent(this, typeof(NotifyService).ToClass());
                    this.startService(intent);

                    // http://developer.android.com/reference/android/app/Activity.html#recreate%28%29
                    this.recreate();
                }
            );
            ll.addView(startservice);
            #endregion

            #region stopservice
            var stopservice = new Button(this);
            stopservice.setText("Stop Service");
            stopservice.AtClick(
                delegate
                {
                    this.ShowToast("stopservice_onclick");

                    var intent = new Intent();
                    intent.setAction(NotifyService.ACTION);
                    intent.putExtra("RQS", NotifyService.RQS_STOP_SERVICE);
                    this.sendBroadcast(intent);

                    // seems stop takes a while

                    //Task.Delay(100);

                    Thread.Sleep(30);

                    this.recreate();
                }
            );
            ll.addView(stopservice);
            #endregion

            stopservice.setEnabled(false);

            // http://stackoverflow.com/questions/12891903/android-check-if-my-service-is-running-in-the-background
            var m = (ActivityManager)this.getSystemService(Context.ACTIVITY_SERVICE);


            Console.WriteLine("getRunningServices");

            var s = m.getRunningServices(1000);

            Console.WriteLine("getRunningServices " + s.size());

            var se =

                from i in Enumerable.Range(0, s.size())
                let rsi = (android.app.ActivityManager.RunningServiceInfo)s.get(i)
                let cn = rsi.service.getClassName()
                let cp = m.getRunningServiceControlPanel(rsi.service)
                orderby cn

                select new { i, rsi, cn, cp };

            //I/System.Console(32668): { cn = com.sec.enterprise.mdm.services.simpin.EnterpriseSimPin, cp =  }
            //I/System.Console(32668): { cn = com.dsi.ant.server.AntService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.gms.analytics.service.AnalyticsService, cp =  }
            //I/System.Console(32668): { cn = com.android.bluetooth.gatt.GattService, cp =  }
            //I/System.Console(32668): { cn = com.samsung.android.scloud.auth.RelayService, cp =  }
            //I/System.Console(32668): { cn = com.samsung.android.sconnect.periph.PeriphService, cp =  }
            //I/System.Console(32668): { cn = org.simalliance.openmobileapi.service.SmartcardService, cp =  }
            //I/System.Console(32668): { cn = com.samsung.android.sm.widgetapp.SMWidgetService, cp =  }
            //I/System.Console(32668): { cn = com.samsung.android.sensor.framework.SensorService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.location.fused.FusedLocationService, cp =  }
            //I/System.Console(32668): { cn = com.sec.android.app.launcher.services.LauncherService, cp =  }
            //I/System.Console(32668): { cn = com.samsung.android.app.shealth.tracker.sport.livetracker.LiveTrackerService, cp =  }
            //I/System.Console(32668): { cn = com.sec.android.widgetapp.digitalclockeasy.DigitalClockEasyService, cp =  }
            //I/System.Console(32668): { cn = com.android.bluetooth.a2dp.A2dpService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.search.core.service.SearchService, cp =  }
            //I/System.Console(32668): { cn = com.sec.android.widgetapp.ap.weather.widget.surfacewidget.WeatherSurfaceWidget, cp =  }
            //I/System.Console(32668): { cn = com.android.incallui.InCallServiceImpl, cp =  }
            //I/System.Console(32668): { cn = com.google.android.gms.gcm.http.GoogleHttpService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.location.geofencer.service.GeofenceProviderService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.hotword.service.HotwordService, cp =  }
            //I/System.Console(32668): { cn = com.samsung.sec.android.application.csc.CscUpdateService, cp =  }
            //I/System.Console(32668): { cn = com.samsung.android.health.wearable.service.WearableService, cp =  }
            //I/System.Console(32668): { cn = com.sec.spp.push.PushClientService, cp =  }
            //I/System.Console(32668): { cn = com.android.systemui.SystemUIService, cp =  }
            //I/System.Console(32668): { cn = android.hardware.location.GeofenceHardwareService, cp =  }
            //I/System.Console(32668): { cn = com.sec.android.pagebuddynotisvc.PageBuddyNotiSvc, cp =  }
            //I/System.Console(32668): { cn = com.samsung.android.thememanager.ThemeManagerService, cp =  }
            //I/System.Console(32668): { cn = com.fmm.dm.XDMService, cp =  }
            //I/System.Console(32668): { cn = com.samsung.android.MtpApplication.MtpService, cp =  }
            //I/System.Console(32668): { cn = com.android.server.telecom.BluetoothVoIPService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.gms.auth.trustagent.GoogleTrustAgent, cp =  }
            //I/System.Console(32668): { cn = com.samsung.appcessory.server.SAPService, cp =  }
            //I/System.Console(32668): { cn = com.samsung.android.app.galaxyfinder.tag.TagReadyService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.location.internal.PendingIntentCallbackService, cp =  }
            //I/System.Console(32668): { cn = com.android.incallui.SecInCallService, cp =  }
            //I/System.Console(32668): { cn = com.samsung.android.app.edge.nightclock.NightClockService, cp =  }
            //I/System.Console(32668): { cn = com.android.server.telecom.BluetoothPhoneService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.location.internal.server.GoogleLocationService, cp =  }
            //I/System.Console(32668): { cn = com.samsung.android.app.galaxyfinder.recommended.RecommendedService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.libraries.hangouts.video.VideoChatService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.location.network.NetworkLocationService, cp =  }
            //I/System.Console(32668): { cn = com.android.stk.StkAppService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.location.internal.GoogleLocationManagerService, cp =  }
            //I/System.Console(32668): { cn = com.samsung.android.service.health.HealthService, cp =  }
            //I/System.Console(32668): { cn = com.android.server.DrmEventService, cp =  }
            //I/System.Console(32668): { cn = com.android.providers.media.MtpService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.gms.deviceconnection.service.DeviceConnectionServiceBroker, cp =  }
            //I/System.Console(32668): { cn = com.android.bluetooth.hfp.HeadsetService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.voiceinteraction.GsaVoiceInteractionService, cp =  }
            //I/System.Console(32668): { cn = com.samsung.android.providers.context.ContextService, cp =  }
            //I/System.Console(32668): { cn = com.android.incallui.MCIDService, cp =  }
            //I/System.Console(32668): { cn = com.samsung.android.app.shealth.tracker.pedometer.service.PedometerService, cp =  }
            //I/System.Console(32668): { cn = com.android.internal.backup.LocalTransportService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.gms.playlog.service.PlayLogBrokerService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.gms.common.stats.GmsCoreStatsService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.gms.clearcut.service.ClearcutLoggerService, cp =  }
            //I/System.Console(32668): { cn = com.samsung.android.service.peoplestripe.PeopleStripeService, cp =  }
            //I/System.Console(32668): { cn = com.samsung.android.app.catchfavorites.catchnotifications.CatchNotificationsService, cp = PendingIntent{2a6fe01c: android.os.BinderProxy@6924ca9} }
            //I/System.Console(32668): { cn = com.samsung.android.beaconmanager.BeaconService, cp =  }
            //I/System.Console(32668): { cn = com.android.defcontainer.DefaultContainerService, cp =  }
            //I/System.Console(32668): { cn = com.android.phone.TelephonyDebugService, cp =  }
            //I/System.Console(32668): { cn = com.sec.android.service.sm.service.SecurityManagerService, cp =  }
            //I/System.Console(32668): { cn = com.android.bluetooth.pan.PanService, cp =  }
            //I/System.Console(32668): { cn = com.ime.framework.spellcheckservice.SamsungIMESpellCheckerService, cp =  }
            //I/System.Console(32668): { cn = com.google.android.gms.backup.BackupTransportService, cp =  }
            //I/System.Console(32668): { cn = com.sec.phone.SecPhoneService, cp =  }

            foreach (var ss in se)
            {


                var cn = ss.cn;

                PendingIntent cp = ss.cp;

                // whats a ControlPanel ?
                Console.WriteLine(new { ss.i, cn, cp });

                // I/System.Console(17713): { cn = AndroidBootServiceNotificationActivity.Activities.NotifyService }
                if (cn == typeof(NotifyService).FullName)
                {
                    // cannot find ourself?

                    startservice.setEnabled(false);
                    stopservice.setEnabled(true);

                    // its running

                    // http://stackoverflow.com/questions/7170730/how-to-set-a-control-panel-for-my-service-in-android
                    // http://www.techques.com/question/1-7170730/How-to-set-a-control-panel-for-my-Service-in-Android
                    // http://alvinalexander.com/java/jwarehouse/android/core/java/android/app/ActivityManagerNative.java.shtml

#if XCONTROLPANEL
                    PendingIntent cp = m.getRunningServiceControlPanel(ss.service);

                    Console.WriteLine(new { cp });
                    if (cp != null)
                    {
                    #region cpb
                        var cpb = new Button(this);
                        cpb.setText("ServiceControlPanel");
                        cpb.AtClick(
                            delegate
                            {
                                //new Intent(
                                //PendingIntent.getActivity(
                                //startActivity(cp);

                                // http://iserveandroid.blogspot.com/2011/03/how-to-launch-pending-intent.html
                                Intent intent = new Intent();

                                try
                                {
                                    cp.send(this, 0, intent);
                                }
                                catch
                                {

                                    throw;
                                }

                            }
                        );
                        ll.addView(cpb);
                    }
                    #endregion
#endif



                }
            }

            this.setContentView(sv);

            this.ShowToast("http://jsc-solutions.net");


        }


    }

    public sealed class NotifyService : Service
    {
        public const string ACTION = "NotifyServiceAction";

        public const int RQS_STOP_SERVICE = 1;

        NotifyServiceReceiver notifyServiceReceiver;

        public override void onCreate()
        {
            notifyServiceReceiver = new NotifyServiceReceiver { that = this };

            base.onCreate();
        }

        public override int onStartCommand(Intent value0, int value1, int value2)
        {
            var intentFilter = new IntentFilter();
            intentFilter.addAction(ACTION);
            registerReceiver(notifyServiceReceiver, intentFilter);


            // Send Notification
            var notificationManager = (NotificationManager)getSystemService(Context.NOTIFICATION_SERVICE);

            var myNotification = new Notification(
                android.R.drawable.star_on,
                //(CharSequence)(object)"Boot!!",
                "Boot!!",

                when: 0

            //java.lang.System.currentTimeMillis()
            );

            var context = getApplicationContext();

            var myIntent = new Intent(Intent.ACTION_VIEW, android.net.Uri.parse("http://youtube.com"));

            var pendingIntent
              = PendingIntent.getActivity(getBaseContext(),
                0, myIntent,
                Intent.FLAG_ACTIVITY_NEW_TASK);
            myNotification.defaults |= Notification.DEFAULT_SOUND;
            myNotification.flags |= Notification.FLAG_AUTO_CANCEL;
            myNotification.setLatestEventInfo(context,
                    "Boot!!",
                    "Proud to be a jsc developer :)",
               pendingIntent);
            notificationManager.notify(1, myNotification);


            return base.onStartCommand(value0, value1, value2);
        }

        public override void onDestroy()
        {
            this.unregisterReceiver(notifyServiceReceiver);
            base.onDestroy();
        }



        public override android.os.IBinder onBind(Intent value)
        {
            return null;
        }


        public class NotifyServiceReceiver : BroadcastReceiver
        {
            public NotifyService that;

            public override void onReceive(Context c, Intent i)
            {
                //android.content.IntentFilter
                //android.content.Intent.ACTION_BOOT_COMPLETED
                int rqs = i.getIntExtra("RQS", 0);

                if (rqs == RQS_STOP_SERVICE)
                    that.stopSelf();
            }
        }
    }


}

namespace foo
{


    // android.intent.action.BOOT_COMPLETED
    [ScriptCoreLib.Android.Manifest.ApplicationIntentFilterAttribute(Action = Intent.ACTION_BOOT_COMPLETED)]
    //[IntentFilter(Action = "android.intent.action.BOOT_COMPLETED")]
    public class AtBootCompleted : BroadcastReceiver
    {
        public override void onReceive(Context c, Intent i)
        {
            var that = c;

            //that.ShowToast("AtBootCompleted");

            var intent = new Intent(that, typeof(NotifyService).ToClass());
            that.startService(intent);
        }
    }

    //[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    //sealed class IntentFilterAttribute : Attribute
    //{
    //    // jsc does not support properties yet? are they even allowed in java?

    //    public string Action;
    //}

}

//0001 020001c0 ScriptCoreLibAndroid::ScriptCoreLibJava.BCLImplementation.System.Threading.__AutoResetEvent
//internal compiler error at method
// assembly: C:\util\jsc\bin\ScriptCoreLibAndroid.dll at
// type: ScriptCoreLibJava.BCLImplementation.System.Threading.__EventWaitHandle, ScriptCoreLibAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// method: Set
// Object reference not set to an instance of an object.
//    at jsc.Languages.Java.JavaCompiler.EmitTryBlock(Prestatement p) in X:\jsc.internal.git\compiler\jsc\Languages\Java\JavaCompiler.EmitTryBlock.cs:line 129