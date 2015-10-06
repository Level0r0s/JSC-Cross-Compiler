using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.view
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/view/KeyEvent.java
    // http://developer.android.com/reference/android/view/KeyEvent.html
    [Script(IsNative = true)]
    public class KeyEvent : InputEvent
    {
        public static int KEYCODE_VOLUME_UP;
        public static int KEYCODE_VOLUME_DOWN;



        public int getDeviceId() { throw null; }
        public int getRepeatCount() { throw null; }
        public int getSource (){throw null;}
        // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\android\keycodes.cs

        // http://developer.android.com/reference/android/view/KeyEvent.Callback.html
        [Script(IsNative = true)]
        public interface Callback
        {

        }

        public int getKeyCode()
        {
            throw null;
        }
        public int getAction()
        {
            throw null;
        }

        // members and types are to be extended by jsc at release build
        public static int ACTION_UP;

        public const int KEYCODE_BACK = 4;

        public static int ACTION_DOWN = 0;
    }
}
