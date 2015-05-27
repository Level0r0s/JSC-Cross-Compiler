using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testchromeasasset.web.libs
{
    public class KnownSharedLibraryAssets
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150527/shouldskippakextraction
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150518/testchromeasasset

        // U:\chromium\src\out\Release\chrome_shell_apk\libs\armeabi-v7a\libchromeshell.so
        public string libchromeshell = @"libs/armeabi_v7a/libchromeshell.so";
        // U:\chromium\src\out\Release\chrome_shell_apk\libs\armeabi-v7a\libchromium_android_linker.so
        public string libchromium_android_linker = @"libs/armeabi_v7a/libchromium_android_linker.so";

        //    Console.WriteLine("should we prefetch our .so for JNI_OnLoad?");
        //    // U:\chromium\src\chrome\android\shell\chrome_shell_entry_point.cc

        //    // couldn't find "liblibchromeshell.so"
        //    java.lang.System.loadLibrary("chromeshell");
    }
}
