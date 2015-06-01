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


// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150518/testchromeasasset
[assembly: ScriptCoreLib.Shared.ScriptResourcesAttribute("libs/armeabi_v7a")]
// https://msdn.microsoft.com/en-us/library/ms165341.aspx
namespace testchromeasasset.activities
{
    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150527
    // (at Binary XML file line #65): <activity> does not have valid android:name


    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "18")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "8")]

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    public class LocalApplication :
        //Application

        global::org.chromium.chrome.shell.ChromeShellApplication
    {
        // https://developer.android.com/reference/android/support/multidex/MultiDexApplication.html
        // can we get AIR via it?
        // https://android.googlesource.com/platform/frameworks/multidex/+/master/library/src/android/support/multidex/MultiDexApplication.java

        public override void onCreate()
        {
            // nested type optimized out?

            // https://stackoverflow.com/questions/30093998/java-lang-nosuchfielderror-android-support-v7-appcompat-rstyleable-theme-windo
            //var appcw = android.R.style.the;

            //var appc = android.support.v7.appcompat.R.style.Theme_AppCompat;

            //{ android.support.v4.widget.DrawerLayout.ViewDragCallback ref0; }
            ////{ android.support.v7.widget.ActionMenuPresenter.ActionMenuPopupCallback ref0; }

            //// https://github.com/android/platform_frameworks_base/blob/master/core/java/android/widget/ActionMenuPresenter.java
            //{ android.support.v7.widget.ActionMenuPresenter ref0; }

            Console.WriteLine("enter LocalApplication onCreate "
                // chrome java
                // U:\chromium\src\out\Release\lib.java\chrome_java.jar\org\chromium\chrome\browser\invalidation\
                //+ " " + typeof(global::org.chromium.chrome.browser.invalidation.UniqueIdInvalidationClientNameGenerator)
                );

            // https://stackoverflow.com/questions/7686482/when-does-applications-oncreate-method-is-called-on-android
            Toast.makeText(this, "LocalApplication", Toast.LENGTH_LONG).show();

            // jsc background compiler may have detected by now that the referenced jar
            // files were update on the build server,
            // and we would be happy to see new intellisense!

            // initonly?
            // public static int progress;


            //var x = (ViewGroup)inflater.inflate(org.chromium.chrome.shell.R.layout.chrome_shell_activity, null);
            //Console.WriteLine("enter ApplicationActivity " + new { x });
            Console.WriteLine("enter LocalApplication R.layout.chrome_shell_activity=0x" + org.chromium.chrome.shell.R.layout.chrome_shell_activity.ToString("x8") + " " + new { org.chromium.chrome.shell.R.layout.chrome_shell_activity });
            Console.WriteLine("enter LocalApplication R.id.progress=0x" + org.chromium.chrome.shell.R.id.progress.ToString("x8") + " " + new { org.chromium.chrome.shell.R.id.progress });
            //Console.WriteLine("enter ApplicationActivity " + new { org.chromium.chrome.shell.R.layout.chrome_shell_activity });
             //org.chromium.chrome.R
            // U:\chromium\src\chrome\android\shell\java\src\org\chromium\chrome\shell\ChromeShellActivity.java
            ////var mTabManager = (org.chromium.chrome.shell.TabManager)x.findViewById(org.chromium.chrome.shell.R.id.tab_manager);
            ////Console.WriteLine("enter ApplicationActivity " + new { mTabManager });
            //Console.WriteLine("enter ApplicationActivity " + new { org.chromium.chrome.shell.R.id.tab_manager });
            //// U:\chromium\src\chrome\android\shell\java\src\org\chromium\chrome\shell\ChromeShellToolbar.java
            ////var mUrlTextView = (EditText)x.findViewById(org.chromium.chrome.shell.R.id.url);
            ////Console.WriteLine("enter ApplicationActivity " + new { mUrlTextView });
            //Console.WriteLine("enter ApplicationActivity " + new { org.chromium.chrome.shell.R.id.url });




            //var dumpAllViews = default(View, int);

            #region dumpAllViews
            var dumpAllViews = default(Action<View, int>);

            dumpAllViews = (View v, int indent) =>
            {
                // Java doesn't support variable width format code -- "%*c"
                // Fake it with string concatenation.  Lame!
                //Log.d(tag, String.format("%" + (indent * 4 + 1) + "c%s %d", ' ', v.toString(), v.getId()));

                // U:\chromium\src\chrome\android\shell\res\layout\chrome_shell_activity.xml

                Console.WriteLine("".PadLeft(indent) + v + " id=0x" + v.getId().ToString("x8") + " (" + v.getId() + ")");

                if (v is ViewGroup)
                {
                    ViewGroup vg = (ViewGroup)v;
                    for (int i = 0; i < vg.getChildCount(); i++)
                    {
                        dumpAllViews(vg.getChildAt(i), indent + 1);
                    }
                }
            };
            #endregion



            org.chromium.chrome.shell.ChromeShellActivity.__marker marker;

            //org.chromium.chrome.shell.ChromeShellActivity
            //org.chromium.chrome.shell.ChromeShellActivity.vf =
            //org.chromium.chrome.shell.ChromeShellActivity.vfinishInitialization =
            //    new xRunnable
            //    {
            //        yield = delegate
            //        {
            //            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150510


            //        }
            //    };

            org.chromium.chrome.shell.ChromeShellToolbar.vFinishInflate =
                new xRunnable
                {
                    yield = delegate
                    {
                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150510

                        //Console.WriteLine("vFinishInflate " + new { org.chromium.chrome.shell.ChromeShellToolbar.vFinishInflateArg0 });
                        Console.WriteLine("vFinishInflate R.layout.chrome_shell_activity=0x" + org.chromium.chrome.shell.R.layout.chrome_shell_activity.ToString("x8"));
                        Console.WriteLine("vFinishInflate R.id.progress=0x" + org.chromium.chrome.shell.R.id.progress.ToString("x8") + " " + new { org.chromium.chrome.shell.R.id.progress });

                        // lets inspect the xml layout we were loaded to

                        // http://stackoverflow.com/questions/12925521/findviewbyid-returning-null-for-views-from-another-fragment-views-have-id-1
                        // this is somewhat like html custom elements isnt it.
                        // delete bin to force refresh?

                        // is it possible we updated some layouts and did not regenerate R files?
                        dumpAllViews(org.chromium.chrome.shell.ChromeShellToolbar.vFinishInflateArg0, 0);
                    }
                };

      


            base.onCreate();
        }

        static testchromeasasset.web.libs.KnownSharedLibraryAssets ref0;


        //static LocalApplication()
        //{
        //    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150518/testchromeasasset

        //    // U:\chromium\src\out\Release\chrome_shell_apk\libs\armeabi-v7a\libchromeshell.so
        //    var libchromeshell = @"libs/armeabi_v7a/libchromeshell.so";
        //    // U:\chromium\src\out\Release\chrome_shell_apk\libs\armeabi-v7a\libchromium_android_linker.so
        //    var libchromium_android_linker = @"libs/armeabi_v7a/libchromium_android_linker.so";

        ////    Console.WriteLine("should we prefetch our .so for JNI_OnLoad?");
        ////    // U:\chromium\src\chrome\android\shell\chrome_shell_entry_point.cc

        ////    // couldn't find "liblibchromeshell.so"
        ////    java.lang.System.loadLibrary("chromeshell");
        //}
    }


    class xRunnable : java.lang.Runnable
    {

        public Action yield;
        public void run()
        {
            yield();
        }
    }


    //    {% set num_sandboxed_services = 20 %}
    //<meta-data android:name="org.chromium.content.browser.NUM_SANDBOXED_SERVICES"
    //           android:value="{{ num_sandboxed_services }}"/>

    // X:\jsc.svn\examples\java\android\Test\TestChromeAsAsset\TestChromeAsAsset\org\chromium\content\app\SandboxedProcessService0.cs

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "org.chromium.content.browser.NUM_PRIVILEGED_SERVICES", value = "3")]

    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(target = typeof(org.chromium.content.app.SandboxedProcessService0), name = "android:process", value = ":sandboxed_process0")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(target = typeof(org.chromium.content.app.SandboxedProcessService1), name = "android:process", value = ":sandboxed_process1")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(target = typeof(org.chromium.content.app.SandboxedProcessService2), name = "android:process", value = ":sandboxed_process2")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(target = typeof(org.chromium.content.app.SandboxedProcessService3), name = "android:process", value = ":sandboxed_process3")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(target = typeof(org.chromium.content.app.SandboxedProcessService4), name = "android:process", value = ":sandboxed_process4")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(target = typeof(org.chromium.content.app.SandboxedProcessService5), name = "android:process", value = ":sandboxed_process5")]

 
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@style/Theme.AppCompat")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@style/MainTheme")]


    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity :
        // Activity

        // public class ChromeShellActivity extends 
        //android.support.v7.app.ActionBarActivity
        // https://github.com/android/platform_frameworks_support/blob/master/v7/appcompat/src/android/support/v7/app/ActionBarActivity.java
        // https://github.com/android/platform_frameworks_support/blob/master/v7/appcompat/src/android/support/v7/app/AppCompatActivity.java
        // https://github.com/android/platform_frameworks_support/blob/master/v7/appcompat/src/android/support/v7/app/AppCompatDelegateImplV7.java

    global::org.chromium.chrome.shell.ChromeShellActivity
    {
        // https://groups.google.com/forum/#!topic/android-developers/Y5wnstMT5Lo

        // (mSubDecor == null) {
        //        E/AndroidRuntime(26170): Caused by: java.lang.IllegalArgumentException: AppCompat does not support the current theme features
        //E/AndroidRuntime(26170):        at android.support.v7.app.AppCompatDelegateImplV7.ensureSubDecor(AppCompatDelegateImplV7.java:360)
        //E/AndroidRuntime(26170):        at android.support.v7.app.AppCompatDelegateImplV7.setContentView(AppCompatDelegateImplV7.java:246)
        //E/AndroidRuntime(26170):        at android.support.v7.app.AppCompatActivity.setContentView(AppCompatActivity.java:106)
        //E/AndroidRuntime(26170):        at TestChromeAsAsset.Activities.ApplicationActivity.onCreate(ApplicationActivity.java:55)


        protected override void onCreate(Bundle savedInstanceState)
        {
            // can we make chrome into a nuget?

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150509/appcompat
            Console.WriteLine(" ");
            Console.WriteLine("enter ApplicationActivity ");
            Console.WriteLine(" ");

            //org.chromium.chrome.shell.TabManager.DEFAULT_URL = "https://explore.xavalon.net";
            //org.chromium.chrome.shell.TabManager.DEFAULT_URL = "http://webglreport.com";
            org.chromium.chrome.shell.TabManager.DEFAULT_URL = "http://webglstats.com";

            //org.chromium.@base.CommandLine.enableNativeProxy();

            //LayoutInflater inflater = LayoutInflater.from(this);


            //System.Threading.Thread.Sleep(500);

            base.onCreate(savedInstanceState);

            //   Console.WriteLine(
            //       "enter ApplicationActivity , " +

            //       new { org.chromium.chrome.shell.R.layout.chrome_shell_activity }
            //       + ", " + new { org.chromium.chrome.shell.R.id.tab_manager });

            //   setContentView(org.chromium.chrome.shell.R.layout.chrome_shell_activity);


            //   Console.WriteLine(
            //"enter ApplicationActivity , " +
            //new { mTabManager });

            //   var activity = this;
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

            //var sv = new ScrollView(this);
            //var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            //sv.addView(ll);

            //var b = new Button(this).AttachTo(ll);



            //b.WithText("base: "
            //    + typeof(global::org.chromium.@base.BaseChromiumApplication)
            //    + " " + typeof(global::org.chromium.content.app.ContentApplication)
            //    + " " + typeof(global::org.chromium.chrome.browser.ChromiumApplication)

            //    // this one wont be from the jar files...
            //    + " " + typeof(global::org.chromium.chrome.shell.ChromeShellApplication)

            //    //+ " " + typeof(global::org.chromium.ui.gfx.DeviceDisplayInfo)
            //    //+ " " + typeof(global::org.chromium.net.GURLUtils)
            //    //+ " " + typeof(global::org.chromium.content.browser.LocationProviderAdapter)

            //     //[javac] W:\src\TestChromeAsAsset\Activities\ApplicationActivity.java:22: error: AudioManagerAndroid is not public in org.chromium.media; cannot be accessed from outside package
            //    //+ " " + typeof(global::org.chromium.media.AudioManagerAndroid)
            //    //+ " " + typeof(global::org.chromium.mojo.system.impl.CoreImpl)


            //    //+ " " + global::org.chromium.@base.BaseChromiumApplication.__hello()
            //    //+ " nativeGetCoreCount: " + org.chromium.@base.CpuFeatures.getCount()
            //    );


            //b.AtClick(
            //    v =>
            //    {
            //        b.setText("AtClick");
            //    }
            //);



        }



    }


}



//E/DID     (28806): enter ChromeShellActivity onCreate
//E/DID     (28806): enter ChromeShellApplication initCommandLine
//W/System.err(28806): stat failed: ENOENT (No such file or directory) : /data/local/tmp/chrome-shell-command-line
//I/BrowserStartupController(28806): Initializing chromium process, singleProcess=false
//W/chromium_android_linker(28806): Couldn't load libchromium_android_linker.so, trying libchromium_android_linker.cr.so
//E/ChromeShellActivity(28806): Unable to load native library.
//E/ChromeShellActivity(28806): org.chromium.base.library_loader.ProcessInitException
//E/ChromeShellActivity(28806):   at org.chromium.base.library_loader.LibraryLoader.loadAlreadyLocked(LibraryLoader.java:410)
//E/ChromeShellActivity(28806):   at org.chromium.base.library_loader.LibraryLoader.ensureInitialized(LibraryLoader.java:154)
//E/ChromeShellActivity(28806):   at org.chromium.content.browser.BrowserStartupController.prepareToStartBrowserProcess(BrowserStartupController.java:285)
//E/ChromeShellActivity(28806):   at org.chromium.content.browser.BrowserStartupController.startBrowserProcessesAsync(BrowserStartupController.java:170)
//E/ChromeShellActivity(28806):   at org.chromium.chrome.shell.ChromeShellActivity.onCreate(ChromeShellActivity.java:156)
//E/ChromeShellActivity(28806):   at TestChromeAsAsset.Activities.ApplicationActivity.onCreate(ApplicationActivity.java:37)
//E/ChromeShellActivity(28806):   at android.app.Activity.performCreate(Activity.java:6374)
//E/ChromeShellActivity(28806):   at android.app.Instrumentation.callActivityOnCreate(Instrumentation.java:1119)
//E/ChromeShellActivity(28806):   at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2767)
//E/ChromeShellActivity(28806):   at android.app.ActivityThread.handleLaunchActivity(ActivityThread.java:2879)
//E/ChromeShellActivity(28806):   at android.app.ActivityThread.access$900(ActivityThread.java:182)
//E/ChromeShellActivity(28806):   at android.app.ActivityThread$H.handleMessage(ActivityThread.java:1475)
//E/ChromeShellActivity(28806):   at android.os.Handler.dispatchMessage(Handler.java:102)
//E/ChromeShellActivity(28806):   at android.os.Looper.loop(Looper.java:145)
//E/ChromeShellActivity(28806):   at android.app.ActivityThread.main(ActivityThread.java:6141)
//E/ChromeShellActivity(28806):   at java.lang.reflect.Method.invoke(Native Method)
//E/ChromeShellActivity(28806):   at java.lang.reflect.Method.invoke(Method.java:372)
//E/ChromeShellActivity(28806):   at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:1399)
//E/ChromeShellActivity(28806):   at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:1194)
//E/ChromeShellActivity(28806): Caused by: java.lang.UnsatisfiedLinkError: dalvik.system.PathClassLoader[DexPathList[[zip file "/system/framework/multiwindow.jar", zip f
//E/ChromeShellActivity(28806):   at java.lang.Runtime.loadLibrary(Runtime.java:366)
//E/ChromeShellActivity(28806):   at java.lang.System.loadLibrary(System.java:989)
//E/ChromeShellActivity(28806):   at org.chromium.base.library_loader.Linker.ensureInitializedLocked(Linker.java:237)
//E/ChromeShellActivity(28806):   at org.chromium.base.library_loader.Linker.isUsed(Linker.java:371)
//E/ChromeShellActivity(28806):   at org.chromium.base.library_loader.LibraryLoader.loadAlreadyLocked(LibraryLoader.java:278)
//E/ChromeShellActivity(28806):   ... 18 more
//I/art     (28806): System.exit called, status: -1