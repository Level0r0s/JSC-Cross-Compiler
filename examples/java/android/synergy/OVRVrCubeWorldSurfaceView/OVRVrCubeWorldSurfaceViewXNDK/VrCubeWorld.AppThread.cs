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


        // converted from size_t
        // can do a sizeof for malloc?
        // sizeof not available for managed members?
        [Script]
        public partial class ovrAppThread
        {
            // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrApi.cs
            // set via ovrAppThread_Create
            public JavaVM JavaVm;

            public jobject ActivityObject;
            public pthread_t Thread;
            public ovrMessageQueue MessageQueue;

            // set to null by onSurfaceDestroyed
            public native_window.ANativeWindow NativeWindow;

            // called by onCreate
            // ctor?
            public void ovrAppThread_Create(JNIEnv env, jobject activityObject)
            {
                env.GetJavaVM(env, out this.JavaVm);

                this.ActivityObject = env.NewGlobalRef(env, activityObject);
                ////appThread.Thread = default(pthread_t);
                //appThread.NativeWindow = null;

                this.MessageQueue.ovrMessageQueue_Create();

                //// ldfda ?
                // new Thread( ?
                // await HopToWorkerThread?
                var createErr = pthread.pthread_create(out this.Thread, null, AppThreadFunction, this);


                //var createErr2 = pthread.pthread_create(out appThread.Thread, null,
                //    arg: appThread,
                //    start_routine: (ovrAppThread appThread0) =>
                //    {
                //        // scope sharing via arg0, in c! ready for roslyn?

                //        return null;
                //    }
                //);
            }

            // called by onDestroy, then free
            public void ovrAppThread_Destroy(JNIEnv env)
            {
                pthread.pthread_join(this.Thread, null);

                //   (/* typecast */(void(*)(JNIEnv*, jobject))env->DeleteGlobalRef)(env, appThread->ActivityObject);
                env.DeleteGlobalRef(env, this.ActivityObject);
                this.MessageQueue.ovrMessageQueue_Destroy();
            }
        }





    }


}
