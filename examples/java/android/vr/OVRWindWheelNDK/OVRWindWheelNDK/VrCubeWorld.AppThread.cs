using com.oculus.gles3jni;
using ScriptCoreLib;
using ScriptCoreLibAndroidNDK.Library;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using ScriptCoreLibNative.SystemHeaders.EGL;
using ScriptCoreLibNative.SystemHeaders.GLES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OVRWindWheelNDK
{
    public static unsafe partial class VrCubeWorld
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewNDK\staging\jni\VrCubeWorld_SurfaceView.c


        // converted from size_t/ovrAppThreadPointer
        // can do a sizeof for malloc?
        // sizeof not available for managed members?
        public partial class ovrAppThread
        {
            // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRWindWheelNDK\VrApi.cs
            // set via ovrAppThread_Create, via GetJavaVM
            // used by?
            public readonly JavaVM JavaVm;

            public readonly jobject ActivityObject;
            //public readonly pthread_t Thread;

            public readonly ovrMessageQueue MessageQueue = new ovrMessageQueue();

            // set to null by onSurfaceDestroyed
            // set by onSurfaceCreated
            public native_window.ANativeWindow NativeWindow = null;





            public ovrApp appState;


            // set by vrapi_GetPredictedTracking
            public ovrTracking trackingOld;
            public ovrTracking tracking;

            readonly System.Threading.Thread Thread;

            // called by onCreate
            public ovrAppThread(JNIEnv env, jobject activityObject)
            {
                // 1907
                ConsoleExtensions.trace("enter ovrAppThread, call pthread_create");

                // why keep it?
                env.GetJavaVM(env, out this.JavaVm);
                this.ActivityObject = env.NewGlobalRef(env, activityObject);


                this.Thread = new System.Threading.Thread(
                    this.AppThreadFunction
                );
                this.Thread.Start();
            }


            void AppThreadFunction()
            {
                // 1778
                ConsoleExtensions.trace("enter pthread_create AppThreadFunction, call vrapi_DefaultInitParms");

                var java = default(ovrJava);
                java.Vm = this.JavaVm;
                java.Vm.AttachCurrentThread(java.Vm, out java.Env, null);
                java.ActivityObject = this.ActivityObject;
                // 1785

                var initParms = VrApi_Helpers.vrapi_DefaultInitParms(ref java);
                ConsoleExtensions.trace("AppThreadFunction, call vrapi_Initialize");
                VrApi.vrapi_Initialize(ref initParms);


                ConsoleExtensions.trace("AppThreadFunction, create ovrApp, call ovrEgl_CreateContext");

                this.appState = new ovrApp(ref java);

                // 1794
                this.appState.Egl.ovrEgl_CreateContext(null);


                ConsoleExtensions.trace("AppThreadFunction, call vrapi_GetHmdInfo, then ovrRenderer_Create");
                var hmdInfo = VrApi.vrapi_GetHmdInfo(ref java);
                this.appState.Renderer.ovrRenderer_Create(ref hmdInfo);

                ConsoleExtensions.trace("AppThreadFunction, enter loop, call ovrMessageQueue_GetNextMessage");
                bool destroyed = false;
                while (!destroyed)
                {
                    #region ovrMessageQueue_GetNextMessage
                    var ok = true;
                    while (ok)
                    {
                        ovrMessage message;
                        var waitForMessages = appState.Ovr == null && !destroyed;

                        if (!this.MessageQueue.ovrMessageQueue_GetNextMessage(out message, waitForMessages)) break;
                        // 1812

                        // no switch for jsc?
                        if (message.Id == MESSAGE.MESSAGE_ON_CREATE)
                        {
                            //ConsoleExtensions.trace("AppThreadFunction, MESSAGE_ON_CREATE");
                        }
                        else if (message.Id == MESSAGE.MESSAGE_ON_START) { }
                        else if (message.Id == MESSAGE.MESSAGE_ON_RESUME)
                        {
                            appState.Resumed = true;
                            //ConsoleExtensions.trace("AppThreadFunction, MESSAGE_ON_RESUME");
                        }
                        else if (message.Id == MESSAGE.MESSAGE_ON_PAUSE) { appState.Resumed = false; }
                        else if (message.Id == MESSAGE.MESSAGE_ON_STOP) { }
                        else if (message.Id == MESSAGE.MESSAGE_ON_DESTROY) { appState.NativeWindow = null; destroyed = true; }
                        else if (message.Id == MESSAGE.MESSAGE_ON_SURFACE_CREATED)
                        {
                            //ConsoleExtensions.trace("AppThreadFunction, MESSAGE_ON_SURFACE_CREATED");
                            var m0 = message[0];
                            appState.NativeWindow = (native_window.ANativeWindow)m0.Pointer;
                        }
                        else if (message.Id == MESSAGE.MESSAGE_ON_SURFACE_DESTROYED) { appState.NativeWindow = null; }
                        else if (message.Id == MESSAGE.MESSAGE_ON_KEY_EVENT) { appState.ovrApp_HandleKeyEvent((keycodes.AKEYCODE)(int)message[0], (input.AInputEventAction)(int)message[1]); }
                        else if (message.Id == MESSAGE.MESSAGE_ON_TOUCH_EVENT)
                        {
                            //ConsoleExtensions.tracei("AppThreadFunction, MESSAGE_ON_TOUCH_EVENT");
                            appState.ovrApp_HandleTouchEvent(message[0], message[1], message[2]);
                        }

                        appState.ovrApp_HandleVrModeChanges();
                    }
                    #endregion



                    appState.ovrApp_BackButtonAction();
                    appState.ovrApp_HandleSystemEvents();

                    // not ready yet?
                    // set by vrapi_EnterVrMode
                    if (appState.Ovr == null)
                    {
                        continue;
                    }


                    #region VRAPI_FRAME_INIT_LOADING_ICON_FLUSH
                    if (!appState.Scene.ovrScene_IsCreated())
                    {
                        // need to keep the enum typename?

                        var parms = VrApi_Helpers.vrapi_DefaultFrameParms(ref appState.Java, ovrFrameInit.VRAPI_FRAME_INIT_LOADING_ICON_FLUSH, 0);
                        parms.FrameIndex = appState.FrameIndex;
                        ConsoleExtensions.trace("vrapi_SubmitFrame VRAPI_FRAME_INIT_LOADING_ICON_FLUSH");
                        appState.Ovr.vrapi_SubmitFrame(ref parms);

                        //unistd.usleep(1000);

                        appState.Scene.ovrScene_Create();

                        // keep the loader on for a moment...
                        //unistd.usleep(1000);
                    }
                    #endregion


                    //appState.tracei60("AppThreadFunction, FrameIndex ", (int)appState.FrameIndex);

                    // 1862
                    appState.FrameIndex++;

                    //ConsoleExtensions.tracei("AppThreadFunction, vrapi_GetPredictedDisplayTime");
                    var predictedDisplayTime = appState.Ovr.vrapi_GetPredictedDisplayTime(appState.FrameIndex);
                    //ConsoleExtensions.tracei("AppThreadFunction, vrapi_GetPredictedTracking");

                    this.trackingOld = this.tracking;
                    this.tracking = appState.Ovr.vrapi_GetPredictedTracking(predictedDisplayTime);

                    // like step in physics?
                    appState.Simulation.ovrSimulation_AdvanceSimulation(predictedDisplayTime);

                    {
                        //var parms = appState.Renderer.ovrRenderer_RenderFrame(ref appState, ref tracking);
                        //var parms = appState.Renderer.ovrRenderer_RenderFrame(this, appState, ref tracking);
                        //var parms = this.appState.Renderer.ovrRenderer_RenderFrame(this, ref tracking);
                        var parms = this.appState.Renderer.ovrRenderer_RenderFrame(this);

                        appState.tracei60("vrapi_SubmitFrame ", (int)appState.FrameIndex);

                        if (tracking.Status == trackingOld.Status)
                            appState.tracei60(" tracking.Status ", (int)tracking.Status);
                        else
                            ConsoleExtensions.tracei(" tracking.Status ", (int)tracking.Status);

                        appState.tracei60(" tracking.HeadPose.Pose.Orientation.x ", (int)(1000 * tracking.HeadPose.Pose.Orientation.x));
                        appState.tracei60(" tracking.HeadPose.Pose.Orientation.y ", (int)(1000 * tracking.HeadPose.Pose.Orientation.y));
                        appState.tracei60(" tracking.HeadPose.Pose.Orientation.z ", (int)(1000 * tracking.HeadPose.Pose.Orientation.z));
                        appState.tracei60(" tracking.HeadPose.Pose.Orientation.w ", (int)(1000 * tracking.HeadPose.Pose.Orientation.w));
                        appState.Ovr.vrapi_SubmitFrame(ref parms);


                    }
                    // 1891
                }

                // 1896
                appState.Renderer.ovrRenderer_Destroy();

                // 1898
                appState.Scene.ovrScene_Destroy();
                appState.Egl.ovrEgl_DestroyContext();
                VrApi.vrapi_Shutdown();

                java.Vm.DetachCurrentThread(java.Vm);
            }




            // called by onDestroy, then free
            // Dispose
            public void ovrAppThread_Destroy(JNIEnv env)
            {
                //1922
                this.Thread.Join();

                env.DeleteGlobalRef(env, this.ActivityObject);
                this.MessageQueue.ovrMessageQueue_Destroy();
            }






            public static implicit operator ovrAppThread(ovrAppThreadPointer handle)
            {
                var __handle = (size_t)(object)handle;
                var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);

                return appThread;
            }

            public static implicit operator ovrAppThreadPointer(ovrAppThread appThread)
            {
                var __handle = (size_t)(object)appThread;
                var __ref = (ovrAppThreadPointer)(object)__handle;

                return __ref;
            }
        }





    }


}
