using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.content
{
    // http://developer.android.com/reference/android/content/ComponentName.html
    [Script(IsNative = true)]
    public class ComponentName
    {
        public string getClassName() { return null; }
        public string getPackageName() { return null; }
        public ComponentName()
        {

        }

        public ComponentName(string packageName, string activityName)
        {

        }
    }
}
