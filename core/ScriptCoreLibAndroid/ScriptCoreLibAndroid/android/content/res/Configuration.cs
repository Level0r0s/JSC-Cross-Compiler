using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.os;
using android.widget;
using ScriptCoreLib;
using java.io;
using java.util;

namespace android.content.res
{
    // http://developer.android.com/reference/android/content/res/Configuration.html
    [Script(IsNative = true)]
    public class Configuration 
    {
        // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\android\configuration.cs

        // members and types are to be extended by jsc at release build

        public Locale locale;


        public static readonly int ORIENTATION_LANDSCAPE;
        public static readonly int ORIENTATION_PORTRAIT;
    }
}
