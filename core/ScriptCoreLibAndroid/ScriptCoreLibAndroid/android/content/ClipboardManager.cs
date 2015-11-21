using android.database;
using android.net;
using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.content
{
    // http://developer.android.com/reference/android/content/ClipboardManager.html
    [Script(IsNative = true)]
    public class ClipboardManager
    {
        public void setPrimaryClip(ClipData clip)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151120
        }
    }
}
