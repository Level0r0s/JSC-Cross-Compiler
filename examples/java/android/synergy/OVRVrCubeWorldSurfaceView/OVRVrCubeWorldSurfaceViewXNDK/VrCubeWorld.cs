//using ANativeWindow = System.Object;

using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLibNative.SystemHeaders.android;
using ScriptCoreLibNative.SystemHeaders.EGL;
using ScriptCoreLibNative.SystemHeaders.GLES3;


namespace OVRVrCubeWorldSurfaceViewXNDK
{
    public static unsafe partial class VrCubeWorld
    {


#if MULTI_THREADED

        enum ovrRenderType
        {
            RENDER_FRAME,
            RENDER_LOADING_ICON,
            RENDER_BLACK_FLUSH,
            RENDER_BLACK_FINAL
        }
       ;

        [Script]
        class ovrRenderThread
        {
            public JavaVM JavaVm;
            public jobject ActivityObject;
            public ovrEgl ShareEgl;
            public pthread_t Thread;
            public int Tid;
            // Synchronization
            public bool Exit;
            public bool WorkAvailableFlag;
            public bool WorkDoneFlag;
            public pthread_cond_t WorkAvailableCondition;
            public pthread_cond_t WorkDoneCondition;
            public pthread_mutex_t Mutex;
            // Latched data for rendering.
            public ovrMobile Ovr;
            public ovrRenderType RenderType;
            public long FrameIndex;
            public int MinimumVsyncs;
            public ovrScene Scene;
            public ovrSimulation Simulation;
            public ovrTracking Tracking;

            public void ovrRenderThread_Clear() { }
            public void ovrRenderThread_Create() { }
            public void ovrRenderThread_Destroy() { }
            public void ovrRenderThread_Submit() { }
            public void ovrRenderThread_Wait() { }
            public void ovrRenderThread_GetTid() { }
        }

        static void RenderThreadFunction() { }

#endif




 


        // java, jsc hybrid?
    }

}
