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

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150525/res
        //  'getApplicationLabel' in type 'android.test.mock.MockPackageManager'

        //public abstract string getApplicationLabel(ApplicationInfo info);
        public abstract java.lang.CharSequence getApplicationLabel(ApplicationInfo info);

        public abstract android.graphics.drawable.Drawable getApplicationIcon(ApplicationInfo info);
        //public abstract java.util.List<ResolveInfo> queryIntentActivities(Intent intent, int flags);
        public abstract java.util.List queryIntentActivities(Intent intent, int flags);
        public abstract Intent getLaunchIntentForPackage(string packageName);
        public abstract PackageInfo getPackageInfo(string packageName, int flags);



        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/oculus360photossdk
        // jsc java natives merge wont yet alllow to hint generic types... 
        public abstract java.util.List  getInstalledApplications(int flags);
        //public abstract java.util.List<ApplicationInfo> getInstalledApplications(int flags);


        public abstract ApplicationInfo getApplicationInfo(string packageName, int flags);
    }
}
