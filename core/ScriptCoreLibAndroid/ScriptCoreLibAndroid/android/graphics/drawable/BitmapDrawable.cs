using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.graphics.drawable
{
    // http://developer.android.com/reference/android/graphics/drawable/BitmapDrawable.html
    [Script(IsNative = true)]
    public class BitmapDrawable : Drawable
    {
        public Bitmap getBitmap() { return null; }
    }
}
