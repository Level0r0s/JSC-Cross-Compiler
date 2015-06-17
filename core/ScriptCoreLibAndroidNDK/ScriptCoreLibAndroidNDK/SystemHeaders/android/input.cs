using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.android
{
    // "X:\util\android-ndk-r10e\platforms\android-21\arch-arm\usr\include\android\input.h"

  
    [Script(IsNative = true, Header = "android/input.h", IsSystemHeader = true)]
    public static class input
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs

        // http://mobilepearls.com/labs/native-android-api/

        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs

        public enum AInputEventType
        {
            /* Indicates that the input event is a key event. */
            AINPUT_EVENT_TYPE_KEY = 1,

            /* Indicates that the input event is a motion event. */
            AINPUT_EVENT_TYPE_MOTION = 2
        };

        // nameless enum
        public enum AInputEventAction
        {
            /* The key has been pressed down. */
            AKEY_EVENT_ACTION_DOWN = 0,

            /* The key has been released. */
            AKEY_EVENT_ACTION_UP = 1,

            /* Multiple duplicate key events have occurred in a row, or a complex string is
             * being delivered.  The repeat_count property of the key event contains the number
             * of times the given key code should be executed.
             */
            AKEY_EVENT_ACTION_MULTIPLE = 2
        };

    

        /* Get the input event type. */
        public static AInputEventType AInputEvent_getType(this AInputEvent @event) { return default(AInputEventType);}

        // AInputEvent
        [Script(IsNative = true)]
        public class AInputEvent
        {
            // a named pointer without data fields?
            // X:\jsc.svn\examples\c\android\Test\TestNDKLooper\TestNDKLooper\xNativeActivity.cs
        }

          [Script(IsNative = true)]
        public class AInputQueue
        {
        }
        
    }

}
