using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace android.content.pm
{
    // http://developer.android.com/reference/android/content/pm/ActivityInfo.html
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/content/pm/ActivityInfo.java
    [Script(IsNative = true)]
    public class ActivityInfo  : ComponentInfo
    {
        // X:\jsc.svn\examples\javascript\android\AndroidListApplications\AndroidListApplications\ApplicationWebService.cs
        public static int SCREEN_ORIENTATION_LANDSCAPE = 0;

        public int theme;
    }
}
