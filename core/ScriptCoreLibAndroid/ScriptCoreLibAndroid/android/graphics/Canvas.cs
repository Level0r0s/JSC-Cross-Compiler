using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace android.graphics
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/graphics/java/android/graphics/Canvas.java
    // http://developer.android.com/reference/android/graphics/Canvas.html
    [Script(IsNative = true)]
    public class Canvas
    {
        // Z:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelActivity\ApplicationActivity.cs
        public void drawLine(float startX, float startY, float stopX, float stopY, Paint paint)
        { 
        }

        public void drawRect(float left, float top, float right, float bottom, Paint paint)
        { }

        // members and types are to be extended by jsc at release build
        public void drawText(string text, float x, float y, Paint paint)
        {
            //X:\jsc.svn\examples\java\android\vr\OVRMyCubeWorldNDK\OVRMyCubeWorld\ApplicationActivity.cs        
        }
    }

}
