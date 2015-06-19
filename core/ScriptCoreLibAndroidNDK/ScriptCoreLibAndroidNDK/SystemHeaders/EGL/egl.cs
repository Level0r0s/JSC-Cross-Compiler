using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.EGL
{
    // https://www.khronos.org/registry/egl/api/1.1/EGL/egl.h
    // "X:\util\android-ndk-r10e\platforms\android-21\arch-arm\usr\include\EGL\egl.h"

    // named pointers yay

    [Script(IsNative = true, Header = "EGL/egl.h", IsSystemHeader = true, PointerName = "EGLDisplay")]
    public class EGLDisplay { }

    [Script(IsNative = true, Header = "EGL/egl.h", IsSystemHeader = true, PointerName = "EGLSurface")]
    public class EGLSurface { }

    [Script(IsNative = true, Header = "EGL/egl.h", IsSystemHeader = true, PointerName = "EGLConfig")]
    public class EGLConfig { }
    [Script(IsNative = true, Header = "EGL/egl.h", IsSystemHeader = true, PointerName = "EGLContext")]
    public class EGLContext { }


    [Script(IsNative = true, Header = "EGL/egl.h", IsSystemHeader = true)]
    //public static class egl
    public interface egl_h
    {
    }

    [Script(IsNative = true, Header = "EGL/egl.h", IsSystemHeader = true)]
    public unsafe class egl : egl_h
    {
        // defaults?
        public const EGLSurface EGL_NO_SURFACE = default(EGLSurface);
        public const EGLContext EGL_NO_CONTEXT = default(EGLContext);
        public const EGLDisplay EGL_NO_DISPLAY = default(EGLDisplay);

        #region #define

        /* Errors / GetError return values */
        public const int EGL_SUCCESS = 0x3000;
        public const int EGL_NOT_INITIALIZED = 0x3001;
        public const int EGL_BAD_ACCESS = 0x3002;
        public const int EGL_BAD_ALLOC = 0x3003;
        public const int EGL_BAD_ATTRIBUTE = 0x3004;
        public const int EGL_BAD_CONFIG = 0x3005;
        public const int EGL_BAD_CONTEXT = 0x3006;
        public const int EGL_BAD_CURRENT_SURFACE = 0x3007;
        public const int EGL_BAD_DISPLAY = 0x3008;
        public const int EGL_BAD_MATCH = 0x3009;
        public const int EGL_BAD_NATIVE_PIXMAP = 0x300A;
        public const int EGL_BAD_NATIVE_WINDOW = 0x300B;
        public const int EGL_BAD_PARAMETER = 0x300C;
        public const int EGL_BAD_SURFACE = 0x300D;
        public const int EGL_CONTEXT_LOST = 0x300E	/* EGL 1.1 - IMG_power_management */                               ;

        /* Reserved =0x300F-=0x301F for additional errors */

        /* Config attributes */
        public const int EGL_BUFFER_SIZE = 0x3020;
        public const int EGL_ALPHA_SIZE = 0x3021;
        public const int EGL_BLUE_SIZE = 0x3022;
        public const int EGL_GREEN_SIZE = 0x3023;
        public const int EGL_RED_SIZE = 0x3024;
        public const int EGL_DEPTH_SIZE = 0x3025;
        public const int EGL_STENCIL_SIZE = 0x3026;
        public const int EGL_CONFIG_CAVEAT = 0x3027;
        public const int EGL_CONFIG_ID = 0x3028;
        public const int EGL_LEVEL = 0x3029;
        public const int EGL_MAX_PBUFFER_HEIGHT = 0x302A;
        public const int EGL_MAX_PBUFFER_PIXELS = 0x302B;
        public const int EGL_MAX_PBUFFER_WIDTH = 0x302C;
        public const int EGL_NATIVE_RENDERABLE = 0x302D;
        public const int EGL_NATIVE_VISUAL_ID = 0x302E;
        public const int EGL_NATIVE_VISUAL_TYPE = 0x302F;
        public const int EGL_SAMPLES = 0x3031;
        public const int EGL_SAMPLE_BUFFERS = 0x3032;
        public const int EGL_SURFACE_TYPE = 0x3033;
        public const int EGL_TRANSPARENT_TYPE = 0x3034;
        public const int EGL_TRANSPARENT_BLUE_VALUE = 0x3035;
        public const int EGL_TRANSPARENT_GREEN_VALUE = 0x3036;
        public const int EGL_TRANSPARENT_RED_VALUE = 0x3037;
        public const int EGL_NONE = 0x3038	/* Attrib list terminator */                                           ;
        public const int EGL_BIND_TO_TEXTURE_RGB = 0x3039;
        public const int EGL_BIND_TO_TEXTURE_RGBA = 0x303A;
        public const int EGL_MIN_SWAP_INTERVAL = 0x303B;
        public const int EGL_MAX_SWAP_INTERVAL = 0x303C;
        public const int EGL_LUMINANCE_SIZE = 0x303D;
        public const int EGL_ALPHA_MASK_SIZE = 0x303E;
        public const int EGL_COLOR_BUFFER_TYPE = 0x303F;
        public const int EGL_RENDERABLE_TYPE = 0x3040;
        public const int EGL_MATCH_NATIVE_PIXMAP = 0x3041	/* Pseudo-attribute (not queryable) */                     ;
        public const int EGL_CONFORMANT = 0x3042;

        /* Reserved =0x3041-=0x304F for additional config attributes */

        /* Config attribute values */
        public const int EGL_SLOW_CONFIG = 0x3050	/* EGL_CONFIG_CAVEAT value */                                  ;
        public const int EGL_NON_CONFORMANT_CONFIG = 0x3051	/* EGL_CONFIG_CAVEAT value */                                  ;
        public const int EGL_TRANSPARENT_RGB = 0x3052	/* EGL_TRANSPARENT_TYPE value */                               ;
        public const int EGL_RGB_BUFFER = 0x308E	/* EGL_COLOR_BUFFER_TYPE value */                              ;
        public const int EGL_LUMINANCE_BUFFER = 0x308F	/* EGL_COLOR_BUFFER_TYPE value */                              ;

        /* More config attribute values, for EGL_TEXTURE_FORMAT */
        public const int EGL_NO_TEXTURE = 0x305C;
        public const int EGL_TEXTURE_RGB = 0x305D;
        public const int EGL_TEXTURE_RGBA = 0x305E;
        public const int EGL_TEXTURE_2D = 0x305F;

        /* Config attribute mask bits */
        public const int EGL_PBUFFER_BIT = 0x0001	/* EGL_SURFACE_TYPE mask bits */                               ;
        public const int EGL_PIXMAP_BIT = 0x0002	/* EGL_SURFACE_TYPE mask bits */                               ;
        public const int EGL_WINDOW_BIT = 0x0004	/* EGL_SURFACE_TYPE mask bits */                               ;
        public const int EGL_VG_COLORSPACE_LINEAR_BIT = 0x0020	/* EGL_SURFACE_TYPE mask bits */                           ;
        public const int EGL_VG_ALPHA_FORMAT_PRE_BIT = 0x0040	/* EGL_SURFACE_TYPE mask bits */                           ;
        public const int EGL_MULTISAMPLE_RESOLVE_BOX_BIT = 0x0200	/* EGL_SURFACE_TYPE mask bits */                       ;
        public const int EGL_SWAP_BEHAVIOR_PRESERVED_BIT = 0x0400	/* EGL_SURFACE_TYPE mask bits */                       ;

        public const int EGL_OPENGL_ES_BIT = 0x0001	/* EGL_RENDERABLE_TYPE mask bits */                                ;
        public const int EGL_OPENVG_BIT = 0x0002	/* EGL_RENDERABLE_TYPE mask bits */                            ;
        public const int EGL_OPENGL_ES2_BIT = 0x0004	/* EGL_RENDERABLE_TYPE mask bits */                            ;
        public const int EGL_OPENGL_BIT = 0x0008	/* EGL_RENDERABLE_TYPE mask bits */                            ;

        /* QueryString targets */
        public const int EGL_VENDOR = 0x3053;
        public const int EGL_VERSION = 0x3054;
        public const int EGL_EXTENSIONS = 0x3055;
        public const int EGL_CLIENT_APIS = 0x308D;

        /* QuerySurface / SurfaceAttrib / CreatePbufferSurface targets */
        public const int EGL_HEIGHT = 0x3056;
        public const int EGL_WIDTH = 0x3057;
        public const int EGL_LARGEST_PBUFFER = 0x3058;
        public const int EGL_TEXTURE_FORMAT = 0x3080;
        public const int EGL_TEXTURE_TARGET = 0x3081;
        public const int EGL_MIPMAP_TEXTURE = 0x3082;
        public const int EGL_MIPMAP_LEVEL = 0x3083;
        public const int EGL_RENDER_BUFFER = 0x3086;
        public const int EGL_VG_COLORSPACE = 0x3087;
        public const int EGL_VG_ALPHA_FORMAT = 0x3088;
        public const int EGL_HORIZONTAL_RESOLUTION = 0x3090;
        public const int EGL_VERTICAL_RESOLUTION = 0x3091;
        public const int EGL_PIXEL_ASPECT_RATIO = 0x3092;
        public const int EGL_SWAP_BEHAVIOR = 0x3093;
        public const int EGL_MULTISAMPLE_RESOLVE = 0x3099;

        /* EGL_RENDER_BUFFER values / BindTexImage / ReleaseTexImage buffer targets */
        public const int EGL_BACK_BUFFER = 0x3084;
        public const int EGL_SINGLE_BUFFER = 0x3085;

        /* OpenVG color spaces */
        public const int EGL_VG_COLORSPACE_sRGB = 0x3089	/* EGL_VG_COLORSPACE value */                              ;
        public const int EGL_VG_COLORSPACE_LINEAR = 0x308A	/* EGL_VG_COLORSPACE value */                                  ;

        /* OpenVG alpha formats */
        public const int EGL_VG_ALPHA_FORMAT_NONPRE = 0x308B	/* EGL_ALPHA_FORMAT value */                               ;
        public const int EGL_VG_ALPHA_FORMAT_PRE = 0x308C	/* EGL_ALPHA_FORMAT value */                               ;

        /* Constant scale factor by which fractional display resolutions &                                                 ;
         * aspect ratio are scaled when queried as integer values.                                                         ;
         */
        public const int EGL_DISPLAY_SCALING = 10000;

        /* Unknown display resolution/aspect ratio */
        //public const int  EGL_UNKNOWN			((EGLint)-1)                                                               ;

        /* Back buffer swap behaviors */
        public const int EGL_BUFFER_PRESERVED = 0x3094	/* EGL_SWAP_BEHAVIOR value */                                  ;
        public const int EGL_BUFFER_DESTROYED = 0x3095	/* EGL_SWAP_BEHAVIOR value */                                  ;

        /* CreatePbufferFromClientBuffer buffer types */
        public const int EGL_OPENVG_IMAGE = 0x3096;

        /* QueryContext targets */
        public const int EGL_CONTEXT_CLIENT_TYPE = 0x3097;

        /* CreateContext attributes */
        public const int EGL_CONTEXT_CLIENT_VERSION = 0x3098;

        /* Multisample resolution behaviors */
        public const int EGL_MULTISAMPLE_RESOLVE_DEFAULT = 0x309A	/* EGL_MULTISAMPLE_RESOLVE value */                    ;
        public const int EGL_MULTISAMPLE_RESOLVE_BOX = 0x309B	/* EGL_MULTISAMPLE_RESOLVE value */                        ;

        /* BindAPI/QueryAPI targets */
        public const int EGL_OPENGL_ES_API = 0x30A0;
        public const int EGL_OPENVG_API = 0x30A1;
        public const int EGL_OPENGL_API = 0x30A2;

        /* GetCurrentSurface targets */
        public const int EGL_DRAW = 0x3059;
        public const int EGL_READ = 0x305A;

        /* WaitNative engines */
        public const int EGL_CORE_NATIVE_ENGINE = 0x305B;

        /* EGL 1.2 tokens renamed for consistency in EGL 1.3 */
        //public const int  EGL_COLORSPACE			EGL_VG_COLORSPACE
        //public const int  EGL_ALPHA_FORMAT		EGL_VG_ALPHA_FORMAT
        //public const int  EGL_COLORSPACE_sRGB		EGL_VG_COLORSPACE_sRGB
        //public const int  EGL_COLORSPACE_LINEAR		EGL_VG_COLORSPACE_LINEAR
        //public const int  EGL_ALPHA_FORMAT_NONPRE		EGL_VG_ALPHA_FORMAT_NONPRE
        //public const int  EGL_ALPHA_FORMAT_PRE		EGL_VG_ALPHA_FORMAT_PRE


        #endregion




        public static EGLDisplay eglGetDisplay(ScriptCoreLibNative.SystemHeaders.android.native_window.ANativeWindow display_id) { throw null; }
        public static bool eglInitialize(EGLDisplay dpy, out int major, out int minor) { throw null; }




        public static EGLSurface eglCreatePbufferSurface(EGLDisplay dpy, EGLConfig config,
                    int[] attrib_list) { throw null; }

        public static EGLContext eglCreateContext(EGLDisplay dpy, EGLConfig config,
			    EGLContext share_context,
                int[] attrib_list) { throw null; }

        public static bool eglGetConfigAttrib(EGLDisplay dpy, EGLConfig config,
                  int attribute, out int value) { throw null; }

        public static bool eglGetConfigs(EGLDisplay dpy, EGLConfig[] configs,
             int config_size, out int num_config) { throw null; }

        public static bool eglDestroySurface(EGLDisplay dpy, EGLSurface surface) { throw null; }
        public static bool eglDestroyContext(EGLDisplay dpy, EGLContext ctx) { throw null; }

        public static bool eglMakeCurrent(EGLDisplay dpy, EGLSurface draw, EGLSurface read, EGLContext ctx) { throw null; }

        public static EGLSurface eglCreateWindowSurface(EGLDisplay dpy, EGLConfig config,
                   ScriptCoreLibNative.SystemHeaders.android.native_window.ANativeWindow win,
                  int[] attrib_list) { throw null; }


        public static bool eglTerminate(EGLDisplay dpy) { throw null; }

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
