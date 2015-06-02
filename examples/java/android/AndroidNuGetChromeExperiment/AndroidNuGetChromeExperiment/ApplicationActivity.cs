using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;

namespace androidnugetchromeexperiment.activities
{

    public class LocalApplication :
        //Application
        global::org.chromium.chrome.shell.ChromeShellApplication
    {
        static testchromeasasset.web.libs.KnownSharedLibraryAssets ref0;

        public override void onCreate()
        {
            Console.WriteLine("LocalApplication onCreate");

            org.chromium.chrome.shell.TabManager.DEFAULT_URL = "http://twitter.com";


            Toast.makeText(this, "AndroidNuGetChromeExperiment", Toast.LENGTH_LONG).show();


            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150527/shouldskippakextraction
            base.onCreate();
        }

        //static LocalApplication()
        //{
        //    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150527/shouldskippakextraction
        //    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150518/testchromeasasset

        //    // U:\chromium\src\out\Release\chrome_shell_apk\libs\armeabi-v7a\libchromeshell.so
        //    var libchromeshell = @"libs/armeabi_v7a/libchromeshell.so";
        //    // U:\chromium\src\out\Release\chrome_shell_apk\libs\armeabi-v7a\libchromium_android_linker.so
        //    var libchromium_android_linker = @"libs/armeabi_v7a/libchromium_android_linker.so";

        //    //    Console.WriteLine("should we prefetch our .so for JNI_OnLoad?");
        //    //    // U:\chromium\src\chrome\android\shell\chrome_shell_entry_point.cc

        //    //    // couldn't find "liblibchromeshell.so"
        //    //    java.lang.System.loadLibrary("chromeshell");
        //}
    }

    // <activity android:name="ApplicationActivity" android:label="@string/xapp_name" android:launchMode="singleInstance" android:configChanges="orientation|screenSize" android:theme="@android:style/Theme.Holo.Dialog">


    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "org.chromium.content.browser.NUM_SANDBOXED_SERVICES", value = "5")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "org.chromium.content.browser.NUM_PRIVILEGED_SERVICES", value = "3")]

    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(target = typeof(org.chromium.content.app.SandboxedProcessService0), name = "android:process", value = ":sandboxed_process0")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(target = typeof(org.chromium.content.app.SandboxedProcessService1), name = "android:process", value = ":sandboxed_process1")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(target = typeof(org.chromium.content.app.SandboxedProcessService2), name = "android:process", value = ":sandboxed_process2")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(target = typeof(org.chromium.content.app.SandboxedProcessService3), name = "android:process", value = ":sandboxed_process3")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(target = typeof(org.chromium.content.app.SandboxedProcessService4), name = "android:process", value = ":sandboxed_process4")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(target = typeof(org.chromium.content.app.SandboxedProcessService5), name = "android:process", value = ":sandboxed_process5")]

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "18")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "8")]

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@style/Theme.AppCompat")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@style/MainTheme")]

    public class LauncherActivity :
    global::org.chromium.chrome.shell.ChromeShellActivity
    // Activity
    {
        testchromeasasset.activities.ApplicationActivity ref0;

        // ref

        protected override void onCreate(Bundle savedInstanceState)
        {
            Console.WriteLine("LauncherActivity onCreate");
            // and yes we are now running jar/so browser.
            // need /assets and /res too tho
            base.onCreate(savedInstanceState);




        }
    }


}

//E/AndroidRuntime( 2169): Caused by: java.lang.IllegalStateException: You need to use a Theme.AppCompat theme (or descendant) with this activity.
//E/AndroidRuntime( 2169):        at android.support.v7.app.ActionBarActivityDelegate.onCreate(ActionBarActivityDelegate.java:152)
//E/AndroidRuntime( 2169):        at android.support.v7.app.ActionBarActivityDelegateBase.onCreate(ActionBarActivityDelegateBase.java:149)
//E/AndroidRuntime( 2169):        at android.support.v7.app.ActionBarActivity.onCreate(ActionBarActivity.java:123)
//E/AndroidRuntime( 2169):        at org.chromium.chrome.shell.ChromeShellActivity.onCreate(ChromeShellActivity.java:134)
//E/AndroidRuntime( 2169):        at androidnugetchromeexperiment.activities.LauncherActivity.onCreate(LauncherActivity.java:35)
//E/AndroidRuntime( 2169):        at android.app.Activity.performCreate(Activity.java:6374)
//E/AndroidRuntime( 2169):        at android.app.Instrumentation.callActivityOnCreate(Instrumentation.java:1119)
//E/AndroidRuntime( 2169):        at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2767)
//E/AndroidRuntime( 2169):        ... 10 more


//E/AndroidRuntime( 4206): Caused by: java.lang.NullPointerException: Attempt to get length of null array
//E/AndroidRuntime( 4206):        at org.chromium.base.ResourceExtractor.shouldSkipPakExtraction(ResourceExtractor.java:482)
//E/AndroidRuntime( 4206):        at org.chromium.base.ResourceExtractor.startExtractingResources(ResourceExtractor.java:424)
//E/AndroidRuntime( 4206):        at org.chromium.content.browser.BrowserStartupController.prepareToStartBrowserProcess(BrowserStartupController.java:281)
//E/AndroidRuntime( 4206):        at org.chromium.content.browser.BrowserStartupController.startBrowserProcessesAsync(BrowserStartupController.java:170)
//E/AndroidRuntime( 4206):        at org.chromium.chrome.shell.ChromeShellActivity.onCreate(ChromeShellActivity.java:159)
//E/AndroidRuntime( 4206):        at androidnugetchromeexperiment.activities.LauncherActivity.onCreate(LauncherActivity.java:35)
//E/AndroidRuntime( 4206):        at android.app.Activity.performCreate(Activity.java:6374)
//E/AndroidRuntime( 4206):        at android.app.Instrumentation.callActivityOnCreate(Instrumentation.java:1119)
//E/AndroidRuntime( 4206):        at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2767)
//E/AndroidRuntime( 4206):        ... 10 more


//W/chromium_android_linker(28065): Couldn't load libchromium_android_linker.so, trying libchromium_android_linker.cr.so
//E/ChromeShellActivity(28065): Unable to load native library.
//E/ChromeShellActivity(28065): org.chromium.base.library_loader.ProcessInitException
//E/ChromeShellActivity(28065):   at org.chromium.base.library_loader.LibraryLoader.loadAlreadyLocked(LibraryLoader.java:415)
//E/ChromeShellActivity(28065):   at org.chromium.base.library_loader.LibraryLoader.ensureInitialized(LibraryLoader.java:156)
//E/ChromeShellActivity(28065):   at org.chromium.content.browser.BrowserStartupController.prepareToStartBrowserProcess(BrowserStartupController.java:285)
//E/ChromeShellActivity(28065):   at org.chromium.content.browser.BrowserStartupController.startBrowserProcessesAsync(BrowserStartupController.java:170)
//E/ChromeShellActivity(28065):   at org.chromium.chrome.shell.ChromeShellActivity.onCreate(ChromeShellActivity.java:159)
//E/ChromeShellActivity(28065):   at androidnugetchromeexperiment.activities.LauncherActivity.onCreate(LauncherActivity.java:37)
//E/ChromeShellActivity(28065):   at android.app.Activity.performCreate(Activity.java:6374)
//E/ChromeShellActivity(28065):   at android.app.Instrumentation.callActivityOnCreate(Instrumentation.java:1119)
//E/ChromeShellActivity(28065):   at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2767)
//E/ChromeShellActivity(28065):   at android.app.ActivityThread.handleLaunchActivity(ActivityThread.java:2879)
//E/ChromeShellActivity(28065):   at android.app.ActivityThread.access$900(ActivityThread.java:182)
//E/ChromeShellActivity(28065):   at android.app.ActivityThread$H.handleMessage(ActivityThread.java:1475)
//E/ChromeShellActivity(28065):   at android.os.Handler.dispatchMessage(Handler.java:102)
//E/ChromeShellActivity(28065):   at android.os.Looper.loop(Looper.java:145)
//E/ChromeShellActivity(28065):   at android.app.ActivityThread.main(ActivityThread.java:6141)
//E/ChromeShellActivity(28065):   at java.lang.reflect.Method.invoke(Native Method)
//E/ChromeShellActivity(28065):   at java.lang.reflect.Method.invoke(Method.java:372)
//E/ChromeShellActivity(28065):   at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:1399)
//E/ChromeShellActivity(28065):   at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:1194)
//E/ChromeShellActivity(28065): Caused by: java.lang.UnsatisfiedLinkError: dalvik.system.PathClassLoader[DexPathList[[zip file "/system/framework/multiwindow.jar
//E/ChromeShellActivity(28065):   at java.lang.Runtime.loadLibrary(Runtime.java:366)
//E/ChromeShellActivity(28065):   at java.lang.System.loadLibrary(System.java:989)
//E/ChromeShellActivity(28065):   at org.chromium.base.library_loader.Linker.ensureInitializedLocked(Linker.java:237)
//E/ChromeShellActivity(28065):   at org.chromium.base.library_loader.Linker.isUsed(Linker.java:371)
//E/ChromeShellActivity(28065):   at org.chromium.base.library_loader.LibraryLoader.loadAlreadyLocked(LibraryLoader.java:283)
//E/ChromeShellActivity(28065):   ... 18 more