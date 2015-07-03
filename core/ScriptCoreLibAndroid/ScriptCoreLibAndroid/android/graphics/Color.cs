using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace android.graphics
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/graphics/java/android/graphics/Color.java
    // http://developer.android.com/reference/android/graphics/Color.html
    [Script(IsNative = true)]
    public class Color
    {
        // members and types are to be extended by jsc at release build


        public static int WHITE;
        public static int GREEN;
        public static int RED;


        public static int argb(int alpha, int red, int green, int blue)
        {
            throw null;
        }
    }

}
