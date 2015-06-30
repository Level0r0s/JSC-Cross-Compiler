using android.content;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.view
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/view/SurfaceView.java
    // http://developer.android.com/reference/android/view/SurfaceView.html

    // https://source.android.com/devices/graphics/architecture.html
    // Whatever you render onto this Surface will be composited by SurfaceFlinger, not by the app. This is the real power of SurfaceView: the Surface you get can be rendered by a separate thread or a separate process, isolated from any rendering performed by the app UI, and the buffers go directly to SurfaceFlinger. You can't totally ignore the UI thread -

    [Script(IsNative = true)]
    public class SurfaceView : View
    {
        // X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\ApplicationWebService.cs

        public SurfaceView(Context c)
            : base(c)
        {

        }


        public SurfaceHolder getHolder()
        {
            return default(SurfaceHolder);
        }



    }
}
