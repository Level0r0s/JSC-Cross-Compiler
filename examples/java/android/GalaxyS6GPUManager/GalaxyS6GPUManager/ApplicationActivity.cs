using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;
using android.content;
using android.content.res;

namespace GalaxyS6GPUManager.Activities
{



    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "8")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "8")]

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        // https://groups.google.com/forum/#!topic/android-developers/Y5wnstMT5Lo

        public event Action<MenuItem> AtOption;

        // inspired by http://thedevelopersinfo.com/2009/10/20/dynamically-change-options-menu-items-in-android/
        public override bool onOptionsItemSelected(MenuItem value)
        {
            if (AtOption != null)
                AtOption(value);


            return false;
        }

        #region AtPrepareOptions
        public event Action<Menu> AtPrepareOptions;

        public override bool onPrepareOptionsMenu(Menu value)
        {
            if (AtPrepareOptions != null)
                AtPrepareOptions(value);



            return base.onPrepareOptionsMenu(value);
        }
        #endregion


        Action<android.content.res.Configuration> vConfigurationChanged;

        public override void onConfigurationChanged(android.content.res.Configuration value)
        {
            base.onConfigurationChanged(value);

            vConfigurationChanged(value);
        }

        protected override void onCreate(Bundle savedInstanceState)
        {
            var activity = this;
            // http://stackoverflow.com/questions/11425020/actionbar-in-a-dialogfragment
            //To show activity as dialog and dim the background, you need to declare android:theme="@style/PopupTheme" on for the chosen activity on the manifest
            //activity.requestWindowFeature(Window.FEATURE_ACTION_BAR);
            //activity.getWindow().setFlags(WindowManager_LayoutParams.FLAG_DIM_BEHIND, WindowManager_LayoutParams.FLAG_DIM_BEHIND);
            //activity.getWindow().setFlags(WindowManager_LayoutParams.FLAG_TRANSLUCENT_STATUS, WindowManager_LayoutParams.FLAG_TRANSLUCENT_STATUS);
            //var @params = activity.getWindow().getAttributes();
            ////@params.height = WindowManager_LayoutParams.FILL_PARENT;
            ////@params.width = 850; //fixed width
            ////@params.height = 450; //fixed width
            //@params.alpha = 1.0f;
            //@params.dimAmount = 0.5f;
            //activity.getWindow().setAttributes(@params);
            //activity.getWindow().setLayout(850, 850);
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this).AttachTo(ll);



            b.WithText("before AtClick");
            b.AtClick(
                v =>
                {
                    b.setText("AtClick");
                }
            );


            AtPrepareOptions +=
                value =>
                {
                    value.clear();

                    var item1 = value.add(
                         (java.lang.CharSequence)(object)"http://abstractatech.com"
                    );



                    item1.setIcon(android.R.drawable.ic_menu_view);

                    var item2 = value.add(
                        (java.lang.CharSequence)(object)"http://jsc-solutions.net"
                    );

                    //item2.setIcon(android.R.drawable.ic_menu_edit);
                    item2.setIcon(android.R.drawable.ic_menu_view);

                    var i = new Intent(Intent.ACTION_VIEW,
                        android.net.Uri.parse("http://jsc-solutions.net")
                    );

                    // http://vaibhavsarode.wordpress.com/2012/05/14/creating-our-own-activity-launcher-chooser-dialog-android-launcher-selection-dialog/
                    var ic = Intent.createChooser(i, "http://jsc-solutions.net");


                    item2.setIntent(
                        ic
                    );
                };

            AtOption +=
                item =>
                {

                    //b.WithText("menu was clicked!" + (string)(object)item.getTitle());
                    b.WithText("menu was clicked!" + new { item });
                };

            var b2 = new Button(this);

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // SystemService
            Action update = delegate
            {
                // X:\opensource\ovr_mobile_sdk_0.5.1\VRLib\jni\VrApi\VrApi.cpp
                int cpuCore = 0;


                var online =
                    System.IO.File.ReadAllText("/sys/devices/system/cpu/cpu4/online").Trim();

                if (online == "1")
                    cpuCore = 4;

                var cpuFreq =
                    System.IO.File.ReadAllText("/sys/devices/system/cpu/cpu" + cpuCore + "/cpufreq/scaling_cur_freq").Trim();

                var cpuFrezMhz =
                    long.Parse(cpuFreq) / 1000;

                // 0 if gpu is not used?
                var gpuFreq =
                  System.IO.File.ReadAllText("/sys/devices/14ac0000.mali/clock").Trim();


                // const int64_t cpuFreq = ReadFreq( "/sys/devices/system/cpu/cpu%i/cpufreq/scaling_cur_freq", cpuCore );

                b2.setText(
                    new { cpuCore, online, cpuFrezMhz, gpuFreq }.ToString()
                    + "\n " + sw.ElapsedMilliseconds
                    );
            };

            var h = new Handler();

            var next = default(Action);

            next = delegate
            {
                // await 1?

                update();

                h.postDelayed(
                    new xRunnable
                    {
                        yield = delegate
                        {
                            next();
                        }
                    },
                1);
            };

            next();


            ll.addView(b2);

            this.setContentView(sv);



            vConfigurationChanged = e =>
            {
                var orientation = getScreenOrientation();

                var SystemUiVisibility = getWindow().getDecorView().getSystemUiVisibility();

                //b2.setText(
                //    new
                //    {
                //        orientation,
                //        SystemUiVisibility
                //    }.ToString()
                //);


                if (orientation == Configuration.ORIENTATION_LANDSCAPE)
                {
                    hideSystemUI();
                }
                else
                {
                    showSystemUI();
                }
            };

            vConfigurationChanged(null);
        }

        public int getScreenOrientation()
        {//http://stackoverflow.com/questions/3663665/how-can-i-get-the-current-screen-orientation
            Display getOrient = this.getWindowManager().getDefaultDisplay();
            int orientation = Configuration.ORIENTATION_UNDEFINED;
            if (getOrient.getWidth() == getOrient.getHeight())
            {
                orientation = Configuration.ORIENTATION_SQUARE;
            }
            else
            {
                if (getOrient.getWidth() < getOrient.getHeight())
                {
                    orientation = Configuration.ORIENTATION_PORTRAIT;
                }
                else
                {
                    orientation = Configuration.ORIENTATION_LANDSCAPE;
                }
            }
            return orientation;
        }

        // This snippet hides the system bars.
        private void hideSystemUI()
        {
            // Set the IMMERSIVE flag.
            // Set the content to appear under the system bars so that the content
            // doesn't resize when the system bars hide and show.
            getWindow().getDecorView().setSystemUiVisibility(
                    View.SYSTEM_UI_FLAG_LAYOUT_STABLE
                    | View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION
                    | View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN
                    | View.SYSTEM_UI_FLAG_HIDE_NAVIGATION // hide nav bar
                    | View.SYSTEM_UI_FLAG_FULLSCREEN // hide status bar
                    | View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY);
        }

        // This snippet shows the system bars. It does this by removing all the flags
        // except for the ones that make the content appear under the system bars.
        private void showSystemUI()
        {
            getWindow().getDecorView().setSystemUiVisibility(0);
        }

    }

    class xRunnable : java.lang.Runnable
    {
        public Action yield;

        public void run()
        {
            yield();
        }
    }
}
//java.lang.RuntimeException: Unable to start activity ComponentInfo{GalaxyS6GPUManager.Activities/GalaxyS6GPUManager.Activities.ApplicationActivity}: java.lang.NullPointerException:
//       at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2814)
//       at android.app.ActivityThread.handleLaunchActivity(ActivityThread.java:2879)
//       at android.app.ActivityThread.access$900(ActivityThread.java:182)
//       at android.app.ActivityThread$H.handleMessage(ActivityThread.java:1475)
//       at android.os.Handler.dispatchMessage(Handler.java:102)
//       at android.os.Looper.loop(Looper.java:145)
//       at android.app.ActivityThread.main(ActivityThread.java:6141)
//       at java.lang.reflect.Method.invoke(Native Method)
//       at java.lang.reflect.Method.invoke(Method.java:372)
//       at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:1399)
//       at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:1194)
//Caused by: java.lang.NullPointerException: Attempt to invoke virtual method 'java.lang.Class java.lang.Object.getClass()' on a null object reference
//       at com.android.internal.app.WindowDecorActionBar.getDecorToolbar(WindowDecorActionBar.java:248)
//       at com.android.internal.app.WindowDecorActionBar.init(WindowDecorActionBar.java:201)
//       at com.android.internal.app.WindowDecorActionBar.<init>(WindowDecorActionBar.java:175)
//       at android.app.Activity.initWindowDecorActionBar(Activity.java:2261)
//       at android.app.Activity.setContentView(Activity.java:2299)
//       at GalaxyS6GPUManager.Activities.ApplicationActivity.onCreate(ApplicationActivity.java:154)
//       at android.app.Activity.performCreate(Activity.java:6374)
//       at android.app.Instrumentation.callActivityOnCreate(Instrumentation.java:1119)
//       at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2767)
