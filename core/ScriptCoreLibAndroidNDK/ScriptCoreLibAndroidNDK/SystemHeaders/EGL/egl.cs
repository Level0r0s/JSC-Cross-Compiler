using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.EGL
{
    // https://www.khronos.org/registry/egl/api/1.1/EGL/egl.h

    // named pointers yay

    [Script(IsNative = true, Header = "EGL/egl.h", IsSystemHeader = true, PointerName = "EGLSurface")]
    public class EGLSurface { }


    [Script(IsNative = true, Header = "EGL/egl.h", IsSystemHeader = true)]
    //public static class egl
    public interface egl_h
    {
    }

    [Script(IsNative = true, Header = "EGL/egl.h", IsSystemHeader = true)]
    public class egl : egl_h
    {
        // defaults?
        public const EGLSurface EGL_NO_SURFACE = default(EGLSurface);

        //#include <EGL/eglplatform.h>
        //#include <EGL/egl.h>
        //#include <GLES2/gl2platform.h>
        //#include <GLES2/gl2ext.h>
        //#include <GLES2/gl2.h>

        // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\android_native_app_glue.cs

        // http://mobilepearls.com/labs/native-android-api/

        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs
        // X:\jsc.svn\examples\c\android\Test\TestHybridOVR\TestHybridOVR\xNativeActivity.cs
    }

}
