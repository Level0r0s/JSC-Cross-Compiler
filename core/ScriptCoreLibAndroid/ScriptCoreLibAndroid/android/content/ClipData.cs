using android.database;
using android.net;
using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.content
{
    // http://developer.android.com/reference/android/content/ClipData.html
    [Script(IsNative = true)]
    public class ClipData 
    {
        //public static ClipData newPlainText (CharSequence label, CharSequence text)
        public static ClipData newPlainText(string label, string text)
        {
            return null;
        }
    }
}
