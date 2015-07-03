using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.graphics
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/graphics/java/android/graphics/Paint.java
    // http://developer.android.com/reference/android/graphics/Paint.html
    [Script(IsNative = true)]
    public class Paint
    {

        [Script(IsNative = true)]
        public class Style
        {
            public static Style STROKE = new Style();

        }

        public void setStyle(Style style) { }
        public void setAlpha(int a) { }
            public void setColor(int color){}

          public  void setTextSize(float textSize){}

    }
}
