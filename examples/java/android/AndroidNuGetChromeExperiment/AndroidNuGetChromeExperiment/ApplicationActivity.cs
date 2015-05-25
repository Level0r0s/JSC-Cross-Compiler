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

namespace AndroidNuGetChromeExperiment.Activities
{

    public class LocalApplication :
        //Application
        global::org.chromium.chrome.shell.ChromeShellApplication
    {
        public override void onCreate()
        {
            org.chromium.chrome.shell.TabManager.DEFAULT_URL = "http://twitter.com";


            Toast.makeText(this, "AndroidNuGetChromeExperiment", Toast.LENGTH_LONG).show();
        }

        static LocalApplication()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150518/testchromeasasset

            // U:\chromium\src\out\Release\chrome_shell_apk\libs\armeabi-v7a\libchromeshell.so
            var libchromeshell = @"libs/armeabi_v7a/libchromeshell.so";
            // U:\chromium\src\out\Release\chrome_shell_apk\libs\armeabi-v7a\libchromium_android_linker.so
            var libchromium_android_linker = @"libs/armeabi_v7a/libchromium_android_linker.so";

            //    Console.WriteLine("should we prefetch our .so for JNI_OnLoad?");
            //    // U:\chromium\src\chrome\android\shell\chrome_shell_entry_point.cc

            //    // couldn't find "liblibchromeshell.so"
            //    java.lang.System.loadLibrary("chromeshell");
        }
    }

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity :
    global::org.chromium.chrome.shell.ChromeShellActivity
    // Activity
    {
        // 

        protected override void onCreate(Bundle savedInstanceState)
        {
            // and yes we are now running jar/so browser.
            // need /assets and /res too tho
            base.onCreate(savedInstanceState);




        }
    }


}

//[aapt] The ' characters around the executable and arguments are
//[aapt] not part of the command.
//[aapt] W:\bin\AndroidManifest.xml:65: error: Error: No resource found that matches the given name (at 'theme' with value '@style/MainTheme').
//[aapt]
//[aapt] W:\bin\AndroidManifest.xml:73: error: Error: No resource found that matches the given name (at 'value' with value '@style/MainTheme').

/*
 
    <None Include="U:\chromium\src\out\Release\gen\chrome\java\res\values\generated_resources.xml">
      <Link>staging\apk\res\values\generated_resources.xml</Link>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>

<None Include="U:\chromium\src\out\Release\obj\chrome\chrome_strings_grd.gen\chrome_strings_grd\res_grit\values\android_chrome_strings.xml">
      <Link>staging\apk\res\values\android_chrome_strings.xml</Link>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Include="U:\chromium\src\out\Release\obj\ui\android\ui_strings_grd.gen\ui_strings_grd\res_grit\values\android_ui_strings.xml">
      <Link>staging\apk\res\values\android_ui_strings.xml</Link>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Include="U:\chromium\src\out\Release\obj\content\content_strings_grd.gen\content_strings_grd\res_grit\values\android_content_strings.xml">
      <Link>staging\apk\res\values\android_content_strings.xml</Link>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>

    <None Include="U:\chromium\src\third_party\android_data_chart\java\res\**\*.*">
      <Link>staging\apk\res-org.chromium.third_party.android.R\%(RecursiveDir)%(FileName)%(Extension)</Link>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Include="U:\chromium\src\third_party\android_media\java\res\**\*.*">
      <Link>staging\apk\res-org.chromium.third_party.android.media.R\%(RecursiveDir)%(FileName)%(Extension)</Link>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Include="U:\chromium\src\third_party\android_tools\sdk\extras\android\support\v7\appcompat\res\**\*.*">
      <Link>staging\apk\res-android.support.v7.appcompat.R\%(RecursiveDir)%(FileName)%(Extension)</Link>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Include="U:\chromium\src\third_party\android_tools\sdk\extras\android\support\v7\mediarouter\res\**\*.*">
      <Link>staging\apk\res-android.support.v7.mediarouter.R\%(RecursiveDir)%(FileName)%(Extension)</Link>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Include="U:\chromium\src\third_party\android_tools\sdk\extras\google\google_play_services\libproject\google-play-services_lib\res\**\*.*">
      <Link>staging\apk\res-com.google.android.gms.R\%(RecursiveDir)%(FileName)%(Extension)</Link>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Include="U:\chromium\src\chrome\android\java\res\**\*.*">
      <Link>staging\apk\res-org.chromium.chrome.R\%(RecursiveDir)%(FileName)%(Extension)</Link>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Include="U:\chromium\src\ui\android\java\res\**\*.*">
      <Link>staging\apk\res-org.chromium.ui.R\%(RecursiveDir)%(FileName)%(Extension)</Link>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Include="U:\chromium\src\components\web_contents_delegate_android\android\java\res\**\*.*">
      <Link>staging\apk\res-org.chromium.components.web_contents_delegate_android.R\%(RecursiveDir)%(FileName)%(Extension)</Link>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Include="U:\chromium\src\content\public\android\java\res\**\*.*">
      <Link>staging\apk\res-org.chromium.content.R\%(RecursiveDir)%(FileName)%(Extension)</Link>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Include="U:\chromium\src\chrome\android\shell\res\**\*.*">
      <Link>staging\apk\res-org.chromium.chrome.shell.R\%(RecursiveDir)%(FileName)%(Extension)</Link>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Include="U:\chromium\src\out\assets\chrome_shell_apk\**\*.*">
      <Link>staging\apk\assets\%(RecursiveDir)%(FileName)%(Extension)</Link>
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>

*/