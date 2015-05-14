using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace android.content.pm
{
    // http://developer.android.com/reference/android/content/pm/PackageItemInfo .html
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/content/pm/PackageItemInfo.java
    [Script(IsNative = true)]
    public class PackageItemInfo
    {
        public ApplicationInfo applicationInfo;

        // X:\jsc.svn\examples\javascript\android\AndroidListApplications\AndroidListApplications\ApplicationWebService.cs
        // "android:name" 
        public string name;
        public string packageName;
    }
}
