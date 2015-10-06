using java.lang;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.view
{
    // http://developer.android.com/reference/android/view/MotionEvent.html
    [Script(IsNative = true)]
    public class MotionEvent
    {
        public static readonly int TOOL_TYPE_FINGER;

        // http://developer.android.com/reference/android/view/MotionEvent.PointerProperties.html
        [Script(IsNative = true)]
        public class PointerProperties
        {
            public int id;
            public int toolType;
        }

        // http://developer.android.com/reference/android/view/MotionEvent.PointerCoords.html
        [Script(IsNative = true)]
        public class PointerCoords
        {
            public float x;
            public float y;
        }

        public int getActionIndex (){throw null;}
        public static MotionEvent obtain(long downTime, long eventTime, int action, int pointerCount, PointerProperties[] pointerProperties, PointerCoords[] pointerCoords, int metaState, int buttonState, float xPrecision, float yPrecision, int deviceId, int edgeFlags, int source, int flags)
        {
            throw null;
        }

        public  int getDeviceId (){throw null;}
        public static int ACTION_UP;
        public static int ACTION_DOWN;
        public static int ACTION_MASK;
        public static int ACTION_POINTER_DOWN;
        public static int ACTION_MOVE;
        public static int ACTION_OUTSIDE;

        public int getAction()
        {
            return default(int);
        }


        public int getPointerCount()
        {
            throw null;
        }

        public float getRawX()
        {
            throw null;
        }


        public float getRawY()
        {
            throw null;
        }
        public float getX(int pointerIndex)
        {
            throw null;
        }
        public float getY(int pointerIndex)
        {
            throw null;
        }
        public float getX()
        {
            return default(float);
        }

        public float getY()
        {
            return default(float);
        }
    }
}
