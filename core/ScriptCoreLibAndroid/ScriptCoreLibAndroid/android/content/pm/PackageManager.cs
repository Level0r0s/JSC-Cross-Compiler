using System;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace android.content.pm
{
    // http://developer.android.com/reference/android/content/pm/PackageManager.html
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/content/pm/PackageManager.java
    [Script(IsNative = true)]
    public abstract class PackageManager
    {
        public static readonly int GET_META_DATA = 0x00000080;

        public abstract string getApplicationLabel(ApplicationInfo info);
        public abstract android.graphics.drawable.Drawable getApplicationIcon(ApplicationInfo info);
        public abstract java.util.List<ResolveInfo> queryIntentActivities(Intent intent, int flags);
        public abstract Intent getLaunchIntentForPackage(string packageName);
        public abstract PackageInfo getPackageInfo(string packageName, int flags);
    }
}
