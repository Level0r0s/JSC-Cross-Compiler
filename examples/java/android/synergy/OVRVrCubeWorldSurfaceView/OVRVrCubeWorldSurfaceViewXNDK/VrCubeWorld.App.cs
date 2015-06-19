using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using ScriptCoreLibNative.SystemHeaders.EGL;
using ScriptCoreLibNative.SystemHeaders.GLES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVRVrCubeWorldSurfaceViewXNDK
{
    public static unsafe partial class VrCubeWorld
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewNDK\staging\jni\VrCubeWorld_SurfaceView.c


        // field of ovrApp
        [Script]
        struct ovrSimulation
        {
            public ovrVector3f CurrentRotation;

            // set default?
            public void ovrSimulation_Clear()
            {
                // 965
                this.CurrentRotation.x = 0.0f;
                this.CurrentRotation.y = 0.0f;
                this.CurrentRotation.z = 0.0f;
            }

            // called by AppThreadFunction, would we benefit if jsc marked no branch methods as inline?
            public void ovrSimulation_AdvanceSimulation(double predictedDisplayTime)
            {
                // 972
                // Update rotation.
                this.CurrentRotation.x = (float)(predictedDisplayTime);
                this.CurrentRotation.y = (float)(predictedDisplayTime);
                this.CurrentRotation.z = (float)(predictedDisplayTime);
            }
        }


        enum ovrBackButtonState
        {
            BACK_BUTTON_STATE_NONE,
            BACK_BUTTON_STATE_PENDING_DOUBLE_TAP,
            BACK_BUTTON_STATE_PENDING_SHORT_PRESS,
            BACK_BUTTON_STATE_SKIP_UP
        }


        // stackalloc at X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.AppThreadFunction.cs

        //[Script]
        //class ovrAppRef
        //{
        //    public ovrApp fields;

        //    public ovrAppRef()
        //    {
        //        this.fields.ovrApp_Clear();
        //    }
        //}

        // cant make it class yet?
        // created by AppThreadFunction
        // ref used by ovrRenderer_RenderFrame
        [Script]
        struct ovrApp
        {
            // defined at vrapi.h?

            public ovrJava Java;

            public ovrEgl Egl;
            public native_window.ANativeWindow NativeWindow;
            public bool Resumed;
            public ovrMobile Ovr;
            public ovrScene Scene;
            public ovrSimulation Simulation;
            public long FrameIndex;
            public int MinimumVsyncs;
            public ovrBackButtonState BackButtonState;
            public bool BackButtonDown;
            public double BackButtonDownStartTime;
#if MULTI_THREADED
	ovrRenderThread		RenderThread;
#else
            // set by?
            public ovrRenderer Renderer;
#endif

            //public ovrApp()
            //{
            //    ovrApp_Clear();
            //}

            // ctor
            // called by AppThreadFunction
            public
            void ovrApp_Clear()
            {
                // 1408

                this.Java.Vm = default(JavaVM);
                this.Java.Env = default(JNIEnv);
                this.Java.ActivityObject = null;
                this.NativeWindow = null;
                this.Resumed = false;
                this.Ovr = default(ovrMobile);
                this.FrameIndex = 1;
                this.MinimumVsyncs = 1;
                this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_NONE;
                this.BackButtonDown = false;
                this.BackButtonDownStartTime = 0.0;

                this.Egl = new ovrEgl();
                //this.Egl.ovrEgl_Clear();

                this.Scene = new ovrScene();
                //this.Scene.ovrScene_Clear();

                this.Simulation.ovrSimulation_Clear();
#if MULTI_THREADED
	ovrRenderThread_Clear( &app->RenderThread );
#else
                this.Renderer = new ovrRenderer();
                //this.Renderer.ovrRenderer_Clear();
#endif
            }

            // called by AppThreadFunction
            public void ovrApp_HandleVrModeChanges()
            {
                // 1432
                if (this.NativeWindow != null && this.Egl.MainSurface == egl.EGL_NO_SURFACE)
                {
                    this.Egl.ovrEgl_CreateSurface(this.NativeWindow);
                }

                if (this.Resumed != false && this.NativeWindow != null)
                {
                    if (this.Ovr == null)
                    {
                        var parms = VrApi_Helpers.vrapi_DefaultModeParms(ref Java);
                        parms.CpuLevel = 2;
                        parms.GpuLevel = 3;
                        parms.MainThreadTid = unistd.gettid();
#if MULTI_THREADED
			// Also set the renderer thread to SCHED_FIFO.
			parms.RenderThreadTid = ovrRenderThread_GetTid( &app->RenderThread );
#endif

                        //ALOGV("        eglGetCurrentSurface( EGL_DRAW ) = %p", eglGetCurrentSurface(EGL_DRAW));

                        this.Ovr = VrApi.vrapi_EnterVrMode(ref parms);

                        //ALOGV("        vrapi_EnterVrMode()");
                        //ALOGV("        eglGetCurrentSurface( EGL_DRAW ) = %p", eglGetCurrentSurface(EGL_DRAW));
                    }
                }
                else
                {
                    if (this.Ovr != null)
                    {
#if MULTI_THREADED
			// Make sure the renderer thread is no longer using the ovrMobile.
			ovrRenderThread_Wait( &app->RenderThread );
#endif
                        //ALOGV("        eglGetCurrentSurface( EGL_DRAW ) = %p", eglGetCurrentSurface(EGL_DRAW));

                        VrApi.vrapi_LeaveVrMode(this.Ovr);
                        this.Ovr = null;

                        //ALOGV("        vrapi_LeaveVrMode()");
                        //ALOGV("        eglGetCurrentSurface( EGL_DRAW ) = %p", eglGetCurrentSurface(EGL_DRAW));
                    }
                }

                if (this.NativeWindow == null && this.Egl.MainSurface != egl.EGL_NO_SURFACE)
                {
                    this.Egl.ovrEgl_DestroySurface();
                }
            }

            // called by AppThreadFunction
            public void ovrApp_BackButtonAction()
            {
                // 1484

                if (this.BackButtonState == ovrBackButtonState.BACK_BUTTON_STATE_PENDING_DOUBLE_TAP)
                {
                    //ALOGV("back button double tap");
                    this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_SKIP_UP;
                }
                else if (this.BackButtonState == ovrBackButtonState.BACK_BUTTON_STATE_PENDING_SHORT_PRESS && !this.BackButtonDown)
                {
                    if ((VrApi.vrapi_GetTimeInSeconds() - this.BackButtonDownStartTime) > VrApi_Android.BACK_BUTTON_DOUBLE_TAP_TIME_IN_SECONDS)
                    {
                        //ALOGV("back button short press");
                        //ALOGV("        ovr_StartSystemActivity( %s )", PUI_CONFIRM_QUIT);
                        VrApi_Android.ovr_StartSystemActivity(ref Java, VrApi.PUI_CONFIRM_QUIT, default(string));
                        this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_NONE;
                    }
                }
                else if (this.BackButtonState == ovrBackButtonState.BACK_BUTTON_STATE_NONE && this.BackButtonDown)
                {
                    if ((VrApi.vrapi_GetTimeInSeconds() - this.BackButtonDownStartTime) > VrApi_Android.BACK_BUTTON_LONG_PRESS_TIME_IN_SECONDS)
                    {
                        //ALOGV("back button long press");
                        //ALOGV("        ovr_StartSystemActivity( %s )", PUI_GLOBAL_MENU);
                        VrApi_Android.ovr_StartSystemActivity(ref Java, VrApi.PUI_GLOBAL_MENU, null);
                        this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_SKIP_UP;
                    }
                }
            }

            // java UI sends over to native, which the uses MQ to send over to bg thread? SharedMemory would be nice?
            // onKeyEvent
            public void ovrApp_HandleKeyEvent(keycodes.AKEYCODE keyCode, input.AInputEventAction action)
            {
                // 1513
                // cannot do this aliasing?
                //var app = this;

                // Handle GearVR back button.
                if (keyCode == keycodes.AKEYCODE.AKEYCODE_BACK)
                {
                    if (action == input.AInputEventAction.AKEY_EVENT_ACTION_DOWN)
                    {
                        if (!this.BackButtonDown)
                        {
                            if ((VrApi.vrapi_GetTimeInSeconds() - this.BackButtonDownStartTime) < VrApi_Android.BACK_BUTTON_DOUBLE_TAP_TIME_IN_SECONDS)
                            {
                                this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_PENDING_DOUBLE_TAP;
                            }
                            this.BackButtonDownStartTime = VrApi.vrapi_GetTimeInSeconds();
                        }
                        this.BackButtonDown = true;
                    }
                    else if (action == input.AInputEventAction.AKEY_EVENT_ACTION_UP)
                    {
                        if (this.BackButtonState == ovrBackButtonState.BACK_BUTTON_STATE_NONE)
                        {
                            if ((VrApi.vrapi_GetTimeInSeconds() - this.BackButtonDownStartTime) < VrApi_Android.BACK_BUTTON_SHORT_PRESS_TIME_IN_SECONDS)
                            {
                                this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_PENDING_SHORT_PRESS;
                            }
                        }
                        else if (this.BackButtonState == ovrBackButtonState.BACK_BUTTON_STATE_SKIP_UP)
                        {
                            this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_NONE;
                        }
                        this.BackButtonDown = false;
                    }
                    //return 1;
                }
                //return 0;
            }

            // onTouchEvent
            public void ovrApp_HandleTouchEvent(int action, float x, float y)
            {
                // ??? not used

            }

            // sent by?
            // called by AppThreadFunction
            public void ovrApp_HandleSystemEvents()
            {
                // 1568
                var MAX_EVENT_SIZE = 4096u;

                var eventBuffer = new byte[MAX_EVENT_SIZE];

                for (var status = VrApi_Android.ovr_GetNextPendingEvent(eventBuffer, MAX_EVENT_SIZE); status >= eVrApiEventStatus.VRAPI_EVENT_PENDING; status = VrApi_Android.ovr_GetNextPendingEvent(eventBuffer, MAX_EVENT_SIZE))
                {
                    if (status != eVrApiEventStatus.VRAPI_EVENT_PENDING)
                    {
                        if (status != eVrApiEventStatus.VRAPI_EVENT_CONSUMED)
                        {
                            //ALOGE("Error %i handing System Activities Event", status);
                        }
                        continue;
                    }
                }

            }

        }



    }


}
