using ScriptCoreLib;
using ScriptCoreLibAndroidNDK.Library;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using ScriptCoreLibNative.SystemHeaders.EGL;
using ScriptCoreLibNative.SystemHeaders.GLES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVRWindWheelNDK
{
    public static unsafe partial class VrCubeWorld
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewNDK\staging\jni\VrCubeWorld_SurfaceView.c



        //static void EglErrorString() { }
        //static void EglFrameBufferStatusString() { }
        //static void EglCheckErrors() { }


        // member of ovrApp
        public class ovrEgl
        {
            const int MAX_CONFIGS = 1024;
            readonly EGLConfig[] configs = new EGLConfig[MAX_CONFIGS];

            // could aswell be static?

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150619
            static readonly int[] configAttribs =
	            {
		            egl.EGL_BLUE_SIZE,  8,
		            egl.EGL_GREEN_SIZE, 8,
		            egl.EGL_RED_SIZE,   8,
		            egl.EGL_DEPTH_SIZE, 0,
		            egl.EGL_SAMPLES,	0,
		            egl.EGL_NONE
	            };

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150619
            static readonly int[] surfaceAttribs =
	            {
		            egl.EGL_WIDTH, 16,
		            egl.EGL_HEIGHT, 16,
		            egl.EGL_NONE
	            };


            static readonly int[] contextAttribs =
	            {
		            egl.EGL_CONTEXT_CLIENT_VERSION, 3,
		            egl.EGL_NONE
	            };

            // 141
            public int MajorVersion = 0;
            public int MinorVersion = 0;
            public EGLDisplay Display = default(EGLDisplay);
            public EGLConfig Config = default(EGLConfig);
            public EGLSurface TinySurface = egl.EGL_NO_SURFACE;
            public EGLSurface MainSurface = egl.EGL_NO_SURFACE;
            public EGLContext Context = egl.EGL_NO_CONTEXT;


            // called by AppThreadFunction
            public void ovrEgl_CreateContext(EGLContext shareEgl_Context)
            {
                // 152
                ConsoleExtensions.trace("enter ovrEgl_CreateContext, eglGetDisplay");

                if (this.Display != egl.EGL_NO_DISPLAY)
                {
                    return;
                }

                this.Display = egl.eglGetDisplay(default(native_window.ANativeWindow));

                egl.eglInitialize(this.Display, out this.MajorVersion, out this.MinorVersion);

                // Do NOT use eglChooseConfig, because the Android EGL code pushes in multisample
                // flags in eglChooseConfig if the user has selected the "force 4x MSAA" option in
                // settings, and that is completely wasted for our warp target.

                int numConfigs = 0;
                if (egl.eglGetConfigs(this.Display, configs, MAX_CONFIGS, out numConfigs) == false)
                {
                    //ALOGE( "        eglGetConfigs() failed: %s", EglErrorString( eglGetError() ) );
                    return;
                }

                ConsoleExtensions.tracei("ovrEgl_CreateContext numConfigs: ", numConfigs);
                //  \VrCubeWorld.Egl.cs:95 ovrEgl_CreateContext numConfigs:  22 


                const int egl_EGL_OPENGL_ES3_BIT_KHR = 0x0040;


                this.Config = default(EGLConfig);

                #region numConfigs
                for (int i = 0; i < numConfigs; i++)
                {
                    int value = 0;

                    egl.eglGetConfigAttrib(this.Display, configs[i], egl.EGL_RENDERABLE_TYPE, out value);
                    if ((value & egl_EGL_OPENGL_ES3_BIT_KHR) != egl_EGL_OPENGL_ES3_BIT_KHR)
                    {
                        continue;
                    }

                    // The pbuffer config also needs to be compatible with normal window rendering
                    // so it can share textures with the window context.
                    egl.eglGetConfigAttrib(this.Display, configs[i], egl.EGL_SURFACE_TYPE, out value);
                    if ((value & (egl.EGL_WINDOW_BIT | egl.EGL_PBUFFER_BIT)) != (egl.EGL_WINDOW_BIT | egl.EGL_PBUFFER_BIT))
                    {
                        continue;
                    }

                    int j = 0;
                    for (; configAttribs[j] != egl.EGL_NONE; j += 2)
                    {
                        egl.eglGetConfigAttrib(this.Display, configs[i], configAttribs[j], out value);
                        if (value != configAttribs[j + 1])
                        {
                            break;
                        }
                    }
                    if (configAttribs[j] == egl.EGL_NONE)
                    {
                        ConsoleExtensions.tracei("ovrEgl_CreateContext config found i: ", i);
                        this.Config = configs[i];
                        break;
                    }
                }
                #endregion

                if (this.Config == default(EGLConfig))
                {
                    ConsoleExtensions.tracei("ovrEgl_CreateContext eglChooseConfig failed");
                    //ALOGE( "        eglChooseConfig() failed: %s", EglErrorString( eglGetError() ) );
                    return;
                }


                //ALOGV( "        Context = eglCreateContext( Display, Config, EGL_NO_CONTEXT, contextAttribs )" );
                //this.Context = egl.eglCreateContext(this.Display, this.Config, (shareEgl != NULL) ? shareEgl->Context : egl.EGL_NO_CONTEXT, contextAttribs);
                this.Context = egl.eglCreateContext(this.Display, this.Config, shareEgl_Context, contextAttribs);
                if (this.Context == egl.EGL_NO_CONTEXT)
                {
                    //ALOGE( "        eglCreateContext() failed: %s", EglErrorString( eglGetError() ) );
                    return;
                }



                //ALOGV( "        TinySurface = eglCreatePbufferSurface( Display, Config, surfaceAttribs )" );
                this.TinySurface = egl.eglCreatePbufferSurface(this.Display, this.Config, surfaceAttribs);
                if (this.TinySurface == egl.EGL_NO_SURFACE)
                {
                    //ALOGE( "        eglCreatePbufferSurface() failed: %s", EglErrorString( eglGetError() ) );
                    egl.eglDestroyContext(this.Display, this.Context);
                    this.Context = egl.EGL_NO_CONTEXT;
                    return;
                }
                //ALOGV( "        eglMakeCurrent( Display, TinySurface, TinySurface, Context )" );
                if (egl.eglMakeCurrent(this.Display, this.TinySurface, this.TinySurface, this.Context) == false)
                {
                    //ALOGE( "        eglMakeCurrent() failed: %s", EglErrorString( eglGetError() ) );
                    egl.eglDestroySurface(this.Display, this.TinySurface);
                    egl.eglDestroyContext(this.Display, this.Context);
                    this.Context = egl.EGL_NO_CONTEXT;
                    return;
                }

                ConsoleExtensions.trace("exit ovrEgl_CreateContext");

            }


            // called by AppThreadFunction
            public void ovrEgl_DestroyContext()
            {
                // 259

                if (this.Display != egl.EGL_NO_DISPLAY)
                {
                    //ALOGE("        eglMakeCurrent( Display, EGL_NO_SURFACE, EGL_NO_SURFACE, EGL_NO_CONTEXT )");
                    if (egl.eglMakeCurrent(this.Display, egl.EGL_NO_SURFACE, egl.EGL_NO_SURFACE, egl.EGL_NO_CONTEXT) == false)
                    {
                        //ALOGE("        eglMakeCurrent() failed: %s", EglErrorString(eglGetError()));
                    }
                }
                if (this.Context != egl.EGL_NO_CONTEXT)
                {
                    //ALOGE("        eglDestroyContext( Display, Context )");
                    if (egl.eglDestroyContext(this.Display, this.Context) == false)
                    {
                        //ALOGE("        eglDestroyContext() failed: %s", EglErrorString(eglGetError()));
                    }
                    this.Context = egl.EGL_NO_CONTEXT;
                }
                if (this.TinySurface != egl.EGL_NO_SURFACE)
                {
                    //ALOGE("        eglDestroySurface( Display, TinySurface )");
                    if (egl.eglDestroySurface(this.Display, this.TinySurface) == false)
                    {
                        //ALOGE("        eglDestroySurface() failed: %s", EglErrorString(eglGetError()));
                    }
                    this.TinySurface = egl.EGL_NO_SURFACE;
                }
                if (this.Display != egl.EGL_NO_DISPLAY)
                {
                    //ALOGE("        eglTerminate( Display )");
                    if (egl.eglTerminate(this.Display) == false)
                    {
                        //ALOGE("        eglTerminate() failed: %s", EglErrorString(eglGetError()));
                    }
                    this.Display = egl.EGL_NO_DISPLAY;
                }
            }


            static readonly int[] eglCreateWindowSurface_surfaceAttribs = { egl.EGL_NONE };

            // called by ovrApp_HandleVrModeChanges
            public void ovrEgl_CreateSurface(native_window.ANativeWindow nativeWindow)
            {
                ConsoleExtensions.trace("enter ovrEgl_CreateSurface");

                //298
                if (this.MainSurface != egl.EGL_NO_SURFACE)
                {
                    return;
                }
                //ALOGV( "        MainSurface = eglCreateWindowSurface( Display, Config, nativeWindow, attribs )" );

                ConsoleExtensions.trace("ovrEgl_CreateSurface, eglCreateWindowSurface, eglMakeCurrent");

                this.MainSurface = egl.eglCreateWindowSurface(this.Display, this.Config, nativeWindow, eglCreateWindowSurface_surfaceAttribs);
                if (this.MainSurface == egl.EGL_NO_SURFACE)
                {
                    //ALOGE( "        eglCreateWindowSurface() failed: %s", EglErrorString( eglGetError() ) );
                    return;
                }
                //ALOGV( "        eglMakeCurrent( display, MainSurface, MainSurface, Context )" );
                if (egl.eglMakeCurrent(this.Display, this.MainSurface, this.MainSurface, this.Context) == false)
                {
                    //ALOGE( "        eglMakeCurrent() failed: %s", EglErrorString( eglGetError() ) );
                    return;
                }

                ConsoleExtensions.trace("exit ovrEgl_CreateSurface, ready for ovrRenderer_Create");
            }

            // called by ovrApp_Clear
            public void ovrEgl_DestroySurface()
            {
                // 320
                if (this.Context != egl.EGL_NO_CONTEXT)
                {
                    //ALOGV("        eglMakeCurrent( display, TinySurface, TinySurface, Context )");
                    if (egl.eglMakeCurrent(this.Display, this.TinySurface, this.TinySurface, this.Context) == false)
                    {
                        //ALOGE("        eglMakeCurrent() failed: %s", EglErrorString(eglGetError()));
                    }
                }
                if (this.MainSurface != egl.EGL_NO_SURFACE)
                {
                    //ALOGV("        eglDestroySurface( Display, MainSurface )");
                    if (egl.eglDestroySurface(this.Display, this.MainSurface) == false)
                    {
                        //ALOGE("        eglDestroySurface() failed: %s", EglErrorString(eglGetError()));
                    }
                    this.MainSurface = egl.EGL_NO_SURFACE;
                }
            }
        }


    }


}
