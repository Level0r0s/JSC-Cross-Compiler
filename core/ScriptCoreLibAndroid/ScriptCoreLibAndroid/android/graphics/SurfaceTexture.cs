using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace android.graphics
{
    //  SurfaceTexture is the combination of a Surface and a GLES texture. 
    //  X:\jsc.svn\examples\java\android\synergy\OVROculus360PhotosNDK\OVROculus360PhotosHUD\VrActivity.cs
    // https://source.android.com/devices/graphics/architecture.html

    // https://android.googlesource.com/platform/frameworks/base.git/+/master/graphics/java/android/graphics/SurfaceTexture.java
    // http://developer.android.com/reference/android/graphics/SurfaceTexture.html

    // https://software.intel.com/sites/landingpage/mmsf/documentation/mmsf_android_example2.html
    

    [Script(IsNative = true)]
    public class SurfaceTexture
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160101/webview

        // members and types are to be extended by jsc at release build




        public void setDefaultBufferSize(int width, int height) { }
    }

}
