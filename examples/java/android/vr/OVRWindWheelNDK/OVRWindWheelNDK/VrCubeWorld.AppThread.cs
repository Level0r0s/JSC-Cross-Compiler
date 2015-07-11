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
            
            public ovrHeadModelParms headModelParms;


            [Script(OptimizedCode = "return e.uordblks;")]
            public static long __uordblks(mallinfo e)
            {
                throw null;
            }

            //[Script(OptimizedCode = "return e.fordblks;")]
            //static long __fordblks(mallinfo e) { throw null; }

            //[Script(OptimizedCode = "return e.usmblks;")]
            //static long __usmblks(mallinfo e) { throw null; }

            public static long xmallinfo()
            {
                var m = malloc_h.mallinfo();

                //ConsoleExtensions.tracei("mallinfo  total allocated space: ", (int)m.uordblks);
                //ConsoleExtensions.tracei64("mallinfo    maximum total allocated space: ", (int)__usmblks(m));

                var total = __uordblks(m);
                //ConsoleExtensions.tracei64("mallinfo            total allocated space: ", (long)__uordblks(m));
                //ConsoleExtensions.tracei64("mallinfo                total free  space: ", (int)__fordblks(m));

                //I/xNativeActivity( 5501): \VrCubeWorld.AppThread.cs:72 mallinfo            total allocated space:  617 370 120

                //if (total > 96 * 1024 * 1024)
                //{
                //    ConsoleExtensions.tracei64("sanity check, are we leaking memory?");

                //    //I/xNativeActivity( 5501): \VrCubeWorld.AppThread.cs:72 mallinfo            total allocated space:  2027296912
                //    //I/xNativeActivity( 5501): \VrApi.ovrMatrix4f.cs:343 out of heap? errno: 12 Out of memory
                //    //I/xNativeActivity( 5501): \VrCubeWorld.AppThread.cs:72 mallinfo            total allocated space:  2212791352

                //    unistd._exit(-5);
                //}

                return total;
            }

            //I/xNativeActivity(21471): \VrCubeWorld.AppThread.cs:71 mallinfo  total allocated space:  2 027 243 344
            //I/xNativeActivity(21471): \VrCubeWorld.AppThread.cs:72 mallinfo  total free  space:  70 957 232
            //I/xNativeActivity(21471): \VrApi.ovrMatrix4f.cs:343 out of heap? errno: 12 Out of memory
            //I/xNativeActivity(21471): \VrCubeWorld.AppThread.cs:71 mallinfo  total allocated space:  -2 080 212 552
            //I/xNativeActivity(21471): \VrCubeWorld.AppThread.cs:72 mallinfo  total free  space:  74 286 664


            //I/xNativeActivity(15462): \VrCubeWorld.AppThread.cs:90 mallinfo  total allocated space:  5 512 048
            //I/xNativeActivity(15462): \VrCubeWorld.AppThread.cs:91 mallinfo  total free  space:  13 362 320

            //I/xNativeActivity(18481): \VrCubeWorld.AppThread.cs:71 mallinfo  total allocated space:  -2083023504
            //I/xNativeActivity(18481): \VrCubeWorld.AppThread.cs:72 mallinfo  total free  space:  76 049 040

            // https://groups.google.com/forum/#!topic/android-ndk/lcnwzszrESo
            // http://stackoverflow.com/questions/30480007/is-using-largeheap-in-android-manifest-a-good-practice
            // https://developer.android.com/reference/android/app/ActivityManager.html#getLargeMemoryClass()
            // http://dwij.co.in/increase-heap-size-of-android-application/
            // http://stackoverflow.com/questions/16957805/android-how-to-increase-application-memory-using-ndk


            // called by onCreate
            public ovrAppThread(JNIEnv env, jobject activityObject)
            {
                // 1907
                ConsoleExtensions.trace("enter ovrAppThread, call pthread_create");

                // why keep it?
                env.GetJavaVM(env, out this.JavaVm);
                this.ActivityObject = env.NewGlobalRef(env, activityObject);


                this.Thread = new System.Threading.Thread(
                    delegate()
                    {
                        // can we do closures?
                        ConsoleExtensions.trace("enter thread for vrapi_SubmitFrame");


                        //malloc_h.malloc_stats();

                        //xmallinfo();

                        //ConsoleExtensions.trace("adding memory pressure...");

                        //var mb = 8 * 1024 * 1024;
                        //var pressure = new byte[mb];

                        ////System.Threading.Thread.Sleep(1000);

                        //xmallinfo();


                        ////ConsoleExtensions.trace("adding memory pressure... store...");

                        ////for (int i = 0; i < mb; i++)
                        ////{
                        ////    pressure[i] = (byte)(0xcc ^ i);
                        ////}

                        ////xmallinfo();

                        //// GC. would jsc static analysis know it means we can free that memory?

                        //stdlib_h.free(pressure);

                        ////pressure = null;

                        ////System.Threading.Thread.Sleep(1000);
                        //xmallinfo();


                        // would our chrome app be able to switch over to ndk over udp?
                        this.AppThreadFunction();
                    }
                );
                this.Thread.Start();
            }



            //I/DEBUG   ( 2941): pid: 31621, tid: 31653, name: Thread-653  >>> OVRWindWheelActivity.Activities <<<
            //I/DEBUG   ( 2941): signal 11 (SIGSEGV), code 1 (SEGV_MAPERR), fault addr 0x8
            //I/DEBUG   ( 2941):     r0 00000000  r1 00000000  r2 ffffffff  r3 3e86c40e
            //I/DEBUG   ( 2941):     r4 3e86c40e  r5 ff4fffc0  r6 00000000  r7 3f800000
            //I/DEBUG   ( 2941):     r8 e22fe448  r9 3f772ed9  sl e22fe488  fp 00000bb5
            //I/DEBUG   ( 2941):     ip f73a0710  sp e22fe310  lr f7380375  pc f40fab72  cpsr 800f0030
            //I/DEBUG   ( 2941):
            //I/DEBUG   ( 2941): backtrace:
            //I/DEBUG   ( 2941):     #00 pc 00008b72  /data/app/OVRWindWheelActivity.Activities-1/lib/arm/libmain.so (OVRWindWheelNDK___ovrMatrix4f_CreateRotation+99)
            //I/DEBUG   ( 2941):     #01 pc 00008e61  /data/app/OVRWindWheelActivity.Activities-1/lib/arm/libmain.so (OVRWindWheelNDK_VrCubeWorld_ovrRenderer_ovrRenderer_RenderFrame+376)
            //I/DEBUG   ( 2941):     #02 pc 000098eb  /data/app/OVRWindWheelActivity.Activities-1/lib/arm/libmain.so (OVRWindWheelNDK_VrCubeWorld_ovrAppThread_AppThreadFunction+810)


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


                this.headModelParms = VrApi_Helpers.vrapi_DefaultHeadModelParms();


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


                    if (xmallinfo() > 20 * 1024 * 1024)
                    {
                        // I/xNativeActivity(24473): \VrCubeWorld.AppThread.cs:71 mallinfo    maximum total allocated space:  1825611032
                        // https://news.ycombinator.com/item?id=9179833

                        // https://www.youtube.com/watch?v=se2KMs5qrqY
                        ConsoleExtensions.tracei64("safe mode before sleep ", appState.FrameIndex);

                        // slow down VR thread...
                        System.Threading.Thread.Sleep(5000);
                        //unistd.usleep(2000 * 1000);

                        //ConsoleExtensions.tracei64("safe mode after sleep ", appState.FrameIndex);

                        //continue;
                    }



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


                        //if (tracking.Status == trackingOld.Status)
                        //    appState.tracei60(" tracking.Status ", (int)tracking.Status);
                        //else
                        //    ConsoleExtensions.tracei(" tracking.Status ", (int)tracking.Status);

                        //appState.tracei60(" tracking.HeadPose.Pose.Orientation.x ", (int)(1000 * tracking.HeadPose.Pose.Orientation.x));
                        //appState.tracei60(" tracking.HeadPose.Pose.Orientation.y ", (int)(1000 * tracking.HeadPose.Pose.Orientation.y));
                        //appState.tracei60(" tracking.HeadPose.Pose.Orientation.z ", (int)(1000 * tracking.HeadPose.Pose.Orientation.z));
                        //appState.tracei60(" tracking.HeadPose.Pose.Orientation.w ", (int)(1000 * tracking.HeadPose.Pose.Orientation.w));
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
