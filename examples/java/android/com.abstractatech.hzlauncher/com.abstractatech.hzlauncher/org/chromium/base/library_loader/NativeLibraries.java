package org.chromium.base.library_loader;
/**
 * This class defines the native libraries and loader options required by webview
 */
public class NativeLibraries {
    // Set to true to use the chromium linker. Only useful to save memory
    // on multi-process content-based projects. Always disabled for the Android Webview.
    public static boolean sUseLinker = true;
    // Set to true to directly load the library from the zip file using the
    // chromium linker. Always disabled for Android Webview.
    public static boolean sUseLibraryInZipFile = false;
    // Set to true to enable chromium linker test support. NEVER enable this for the
    // Android webview.
    public static boolean sEnableLinkerTests = false;
    // This is the list of native libraries to load. In the normal chromium build, this would be
    // automatically generated.
    // TODO(torne, cjhopman): Use a generated file for this.
    static final String[] LIBRARIES = { "chromium_android_linker", "chromeshell" };
    // This should match the version name string returned by the native library.
    // TODO(aberent) The Webview native library currently returns an empty string; change this
    // to a string generated at compile time, and incorporate that string in a generated
    // replacement for this file.
    static String sVersionNumber = "44.0.2371.0";
}

// D/PhoneWindow(19126): *FMB* installDecor flags : -2139029248
// E/DID     (19126): enter ChromeShellActivity onCreate AA
// E/DID     (19126): enter ChromeShellApplication initCommandLine
// W/System.err(19126): stat failed: ENOENT (No such file or directory) : /data/local/tmp/chrome-shell-command-line
// I/BrowserStartupController(19126): Initializing chromium process, singleProcess=false
// I/ResourceExtractor(19126): Extracting resource chrome_100_percent.pak
// I/ResourceExtractor(19126): Extracting resource en-GB.pak
// I/ResourceExtractor(19126): Extracting resource en-US.pak
// I/ResourceExtractor(19126): Extracting resource icudtl.dat
// I/LibraryLoader(19126): Time to load native libraries: 36 ms (timestamps 1740-1776)
// I/LibraryLoader(19126): Expected native library version number "",actual native library version number "44.0.2371.0"
// I/LibraryLoaderHelper(19126): Deleting obsolete libraries in /data/data/TestChromeAsAsset.Activities/app_fallback
// E/ChromeShellActivity(19126): Unable to load native library.
// E/ChromeShellActivity(19126): org.chromium.base.library_loader.ProcessInitException
// E/ChromeShellActivity(19126):   at org.chromium.base.library_loader.LibraryLoader.loadAlreadyLocked(LibraryLoader.java:419)
// E/ChromeShellActivity(19126):   at org.chromium.base.library_loader.LibraryLoader.ensureInitialized(LibraryLoader.java:154)
// E/ChromeShellActivity(19126):   at org.chromium.content.browser.BrowserStartupController.prepareToStartBrowserProcess(BrowserStartupController.java:285)
// E/ChromeShellActivity(19126):   at org.chromium.content.browser.BrowserStartupController.startBrowserProcessesAsync(BrowserStartupController.java:170)
// E/ChromeShellActivity(19126):   at org.chromium.chrome.shell.ChromeShellActivity.onCreate(ChromeShellActivity.java:159)
// E/ChromeShellActivity(19126):   at TestChromeAsAsset.Activities.ApplicationActivity.onCreate(ApplicationActivity.java:39)
// E/ChromeShellActivity(19126):   at android.app.Activity.performCreate(Activity.java:6374)
// E/ChromeShellActivity(19126):   at android.app.Instrumentation.callActivityOnCreate(Instrumentation.java:1119)
// E/ChromeShellActivity(19126):   at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2767)
// E/ChromeShellActivity(19126):   at android.app.ActivityThread.handleLaunchActivity(ActivityThread.java:2879)
// E/ChromeShellActivity(19126):   at android.app.ActivityThread.access$900(ActivityThread.java:182)
// E/ChromeShellActivity(19126):   at android.app.ActivityThread$H.handleMessage(ActivityThread.java:1475)
// E/ChromeShellActivity(19126):   at android.os.Handler.dispatchMessage(Handler.java:102)
// E/ChromeShellActivity(19126):   at android.os.Looper.loop(Looper.java:145)
// E/ChromeShellActivity(19126):   at android.app.ActivityThread.main(ActivityThread.java:6141)
// E/ChromeShellActivity(19126):   at java.lang.reflect.Method.invoke(Native Method)
// E/ChromeShellActivity(19126):   at java.lang.reflect.Method.invoke(Method.java:372)
// E/ChromeShellActivity(19126):   at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:1399)
// E/ChromeShellActivity(19126):   at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:1194)