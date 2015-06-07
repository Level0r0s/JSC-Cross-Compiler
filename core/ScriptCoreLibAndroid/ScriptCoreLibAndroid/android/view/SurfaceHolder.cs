using android.content;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.view
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/view/SurfaceHolder.java
    // http://developer.android.com/reference/android/view/SurfaceHolder.html
    [Script(IsNative = true)]
    public interface SurfaceHolder
    {
        // tested by?
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceView\ApplicationActivity.cs

        void setType(int type) ;
        void addCallback(SurfaceHolder_Callback callback);


         Surface getSurface();
    }

    [Script(IsNative = true)]
    public interface SurfaceHolder_Callback
    {
        // X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\ApplicationWebService.cs

    }
}
