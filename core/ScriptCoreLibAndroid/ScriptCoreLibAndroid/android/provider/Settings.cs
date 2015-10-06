using android.content;
using android.net;
using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.provider
{
    // http://developer.android.com/reference/android/provider/Settings.html
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/provider/Settings.java
    [Script(IsNative = true)]
    public class Settings
    {
        // how would we use it from
        // X:\jsc.svn\examples\java\android\AndroidEmailActivity\AndroidEmailActivity\ApplicationActivity.cs

        // http://developer.android.com/reference/android/provider/Settings.NameValueTable.html
        [Script(IsNative = true)]
        public class NameValueTable
        { }

        // http://developer.android.com/reference/android/provider/Settings.System.html
        [Script(IsNative = true)]
        public class System : NameValueTable
        {
            public static string getString(ContentResolver resolver, string name) { throw null; }


            public static readonly string TIME_12_24;

        }


        [Script(IsNative = true)]
        public class Global
        {
            public static readonly string AIRPLANE_MODE_ON;
            public static readonly string BLUETOOTH_ON;

            public static int getInt(ContentResolver cr, string name, int def) { throw null; }
        }
    }
}
