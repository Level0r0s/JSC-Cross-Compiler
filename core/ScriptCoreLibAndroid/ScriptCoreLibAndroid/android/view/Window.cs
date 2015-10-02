using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.view
{
        public static readonly int FEATURE_INDETERMINATE_PROGRESS = 5;

        public const int FEATURE_NO_TITLE = 1;

        public bool requestFeature(int featureId)
        {
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/view/Window.java

    // http://developer.android.com/reference/android/view/Window.html
    [Script(IsNative = true)]
    public abstract class Window
    {
        // members and types are to be extended by jsc at release build

            return default(bool);
        }




        public void addFlags(int flags) { }

        public virtual void setFlags(int arg0, int arg1)
        {

        }

        public abstract View getDecorView();
    }
}
