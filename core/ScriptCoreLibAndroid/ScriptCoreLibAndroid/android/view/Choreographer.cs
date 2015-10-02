using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using android.os;
using android.widget;
using ScriptCoreLib;

namespace android.view
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/view/Choreographer.java
    // http://developer.android.com/reference/android/view/Choreographer.html
    [Script(IsNative = true)]
    public class Choreographer
    {
        // members and types are to be extended by jsc at release build



        [Script(IsNative = true)]
        public interface FrameCallback
        {
            // members and types are to be extended by jsc at release build


        }
    }
}
