using ScriptCoreLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if JNIEnv
using OVRMyCubeWorldNDK;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using ScriptCoreLibAndroidNDK.Library;

// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150619/ovrvrcubeworldsurfaceviewx
// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150613/xndk
// java/c jsc hybrid layer gen?

[assembly: Script(IsScriptLibrary = true)]
[assembly: ScriptTypeFilter(ScriptType.C)]

// now we have _PrivateImplementationDetails__24B4BA40_DFD3_4256_8345_0CEACAEC01A3____method0x600005e_1

//[assembly: ScriptTypeFilter(ScriptType.C, typeof(OVRMyCubeWorldNDK.VrCubeWorld))]
//[assembly: ScriptTypeFilter(ScriptType.C, typeof(com.oculus.gles3jni.GLES3JNILib))]

// .so export needs to prefox java methods by Java.
[assembly: ScriptNamespaceRename(NativeNamespaceName = "com.oculus.gles3jni", VirtualNamespaceName = "Java.com.oculus.gles3jni")]

#endif

namespace com.oculus.gles3jni
{
    // 
    // X:\jsc.svn\examples\c\android\hybrid\HybridGLES3JNIActivity\HybridGLES3JNIActivity\NDK\GLES3JNILib.cs

    // can we have named pointer between java and ndk?
    public enum ovrAppThreadPointer : long { }

    unsafe static class GLES3JNILib
    {
        // ifelse does not look that good. lets keep two sets of signatures..
#if JNIEnv

        // we could use it as offset from touch screen!
        public static int fields_xvalue;
        public static int fields_yvalue;

        public static int fields_mousex;
        public static int fields_mousey;

        public static int fields_ad;
        public static int fields_ws;
        public static int fields_c;

        //public delegate void ActionStringFloat(string fname, float f);

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150619/ovrvrcubeworldsurfaceviewx

        class args<T>
        {
        }

        class argsF
        {
            public JNIEnv env;
            public jobject fields;

            // 
            public float this[string fname]
            {

                set
                {
                    var fields_GetType = env.GetObjectClass(env, fields);
                    var fref = env.GetFieldID(env, fields_GetType, fname, "F");

                    env.SetFloatField(env, fields, fref, value);
                }
            }
        }

        class argsI
        {
            public JNIEnv env;
            public jobject fields;

            // 
            public int this[string fname]
            {

                get
                {
                    var fields_GetType = env.GetObjectClass(env, fields);
                    var fref = env.GetFieldID(env, fields_GetType, fname, "I");

                    var value = env.GetIntField(env, fields, fref);

                    return value;
                }
            }
        }


        // JVM load the .so and calls this native function
        public static jstring stringFromJNI(JNIEnv env, jobject thiz, jobject fields)
        {
            // what about jobject dynamic?

            //dynamic locfields = fields;
            //locfields.foo = "foo";

            // pickerwengs.blogspot.com/2011/12/android-programming-objects-between.html

            //System.NullReferenceException: Object reference not set to an instance of an object.
            //   at jsc.Languages.C.CCompiler.WriteDecoratedMethodName(MethodBase z) in x:\jsc.internal.git\compiler\jsc\Languages\C\CCompiler.WriteDecoratedMethodName.cs:line 60
            //   at jsc.Languages.C.CCompiler.WriteDecoratedMethodName(MethodBase z, Boolean q) in x:\jsc.internal.git\compiler\jsc\Languages\C\CCompiler.WriteDecoratedMethodName.cs:line 31

            // journals.ecs.soton.ac.uk/java/tutorial/native1.1/implementing/field.html



            // generic / per field variables?

            var aI = new argsI { env = env, fields = fields };

            GLES3JNILib.fields_xvalue = aI["x"];
            GLES3JNILib.fields_yvalue = aI["y"];

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150703/mousex
            GLES3JNILib.fields_mousex = aI["mousex"];
            GLES3JNILib.fields_mousey = aI["mousey"];

            GLES3JNILib.fields_ad = aI["ad"];
            GLES3JNILib.fields_ws = aI["ws"];

            appThread.appState.Scene.Update();

            var aF = new argsF { env = env, fields = fields };

            aF["tracking_HeadPose_Pose_Orientation_x"] = appThread.tracking.HeadPose.Pose.Orientation.x;
            aF["tracking_HeadPose_Pose_Orientation_y"] = appThread.tracking.HeadPose.Pose.Orientation.y;
            aF["tracking_HeadPose_Pose_Orientation_z"] = appThread.tracking.HeadPose.Pose.Orientation.z;
            aF["tracking_HeadPose_Pose_Orientation_w"] = appThread.tracking.HeadPose.Pose.Orientation.w;

            //void ScriptCoreLibNative_BCLImplementation_System___Action_2_Invoke(LPScriptCoreLibNative_BCLImplementation_System___Action_2, void*, void*);


            // can we do lambdas?
            //Action<string, float> set = (fname, f) =>
            //ActionStringFloat set = (fname, f) =>
            //{

            //};

            //set("tracking_HeadPose_Pose_Orientation_x", appThread.tracking.HeadPose.Pose.Orientation.x);

            //var fields_tracking_HeadPose_Pose_Orientation_x = env.GetFieldID(env, fields_GetType, "tracking_HeadPose_Pose_Orientation_x", "F");

            //env.SetFloatField(env, fields, fields_tracking_HeadPose_Pose_Orientation_x,
            //    appThread.tracking.HeadPose.Pose.Orientation.x
            //);


            // what about sending back a variable_

            //appThread.tracking.HeadPose.Pose.Orientation.x;


            // GetIntField
            // if we add a field how can we inspect it?

            // called by
            // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewX\ApplicationActivity.cs

            // jstring Java_com_oculus_gles3jni_GLES3JNILib_stringFromJNI(JNIEnv* env, jobject thiz)
            // X:\jsc.svn\examples\c\Test\TestNamespaceFixup\TestNamespaceFixup\Class1.cs

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150607-1/vrcubeworld
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150525


            // look almost the same file!

            // OVR_VRAPI_EXPORT const char * vrapi_GetVersionString();

            if (fields == null)
            {
                return env.NewStringUTF(env, "dynamic fields null?");

            }

            // if we change our NDK code, will nuget packaing work on the background, and also upgrade running apps?
            var v = env.NewStringUTF(env,
                //"hey! XNDK!"

                VrApi.vrapi_GetVersionString()
            );

            //v = env.JNIEnv_NewStringUTF(VrApi.vrapi_GetVersionString());
            // the other option is to have a debugger send the updated MSIL via edit and continue, and we would have to patch the function on the fly.

            return v;
        }



        // EGL context is supposed to be also thread local...
        // https://groups.google.com/forum/#!topic/android-ndk/ybjXY7HYlmA
        // set by onCreate
        static VrCubeWorld.ovrAppThread appThread;

        #region Activity lifecycle
        // named pointers! typedefs!
        static ovrAppThreadPointer onCreate(JNIEnv env, jobject obj, jobject activity)
        {
            // 1937
            ConsoleExtensions.trace("enter onCreate, then ovrAppThread, then ovrMessageQueue_Enable");

            appThread = new VrCubeWorld.ovrAppThread(env, activity);

            // set property?
            appThread.MessageQueue.ovrMessageQueue_Enable(true);

            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_CREATE, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);

            return appThread;
        }

        #region wndproc/ovrMessageQueue_PostMessage
        static void onStart(ref JNIEnv env, jobject obj)
        {
            // 1952
            //var appThread = (VrCubeWorld.ovrAppThread)handle;
            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_START, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
        }
        static void onResume(ref JNIEnv env, jobject obj)
        {
            // 1961
            //var appThread = (VrCubeWorld.ovrAppThread)handle;
            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_RESUME, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
        }
        static void onPause(ref JNIEnv env, jobject obj)
        {
            // 1970
            //var appThread = (VrCubeWorld.ovrAppThread)handle;
            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_PAUSE, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
        }
        static void onStop(ref JNIEnv env, jobject obj)
        {
            // 1980
            //var appThread = (VrCubeWorld.ovrAppThread)handle;
            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_STOP, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
        }

        #endregion

        static void onDestroy(JNIEnv env, jobject obj)
        {
            // 1988
            //var appThread = (VrCubeWorld.ovrAppThread)handle;
            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_DESTROY, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
            appThread.MessageQueue.ovrMessageQueue_Enable(false);
            appThread.ovrAppThread_Destroy(env);
            stdlib_h.free(appThread);
        }

        #endregion

        #region Surface lifecycle
        static void onSurfaceCreated(JNIEnv env, jobject obj, jobject surface)
        {
            // 2009
            ConsoleExtensions.trace("enter onSurfaceCreated");

            //var appThread = (VrCubeWorld.ovrAppThread)handle;

            var newNativeWindow = native_window_jni.ANativeWindow_fromSurface(env, surface);

            if (native_window.ANativeWindow_getWidth(newNativeWindow) < native_window.ANativeWindow_getHeight(newNativeWindow))
            {
                // An app that is relaunched after pressing the home button gets an initial surface with
                // the wrong orientation even though android:screenOrientation="landscape" is set in the
                // manifest. The choreographer callback will also never be called for this surface because
                // the surface is immediately replaced with a new surface with the correct orientation.
                //ALOGE("        Surface not in landscape mode!");
            }

            appThread.NativeWindow = newNativeWindow;

            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_SURFACE_CREATED, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            message.ovrMessage_SetPointerParm(0, appThread.NativeWindow);
            //ConsoleExtensions.tracei("onSurfaceCreated, post MESSAGE_ON_SURFACE_CREATED");
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);

            //ConsoleExtensions.tracei("exit onSurfaceCreated");
        }

        static void onSurfaceChanged(JNIEnv env, jobject obj, jobject surface)
        {
            // 2032
            //var appThread = (VrCubeWorld.ovrAppThread)handle;

            var newNativeWindow = native_window_jni.ANativeWindow_fromSurface(env, surface);

            if (native_window.ANativeWindow_getWidth(newNativeWindow) < native_window.ANativeWindow_getHeight(newNativeWindow))
            {
                // An app that is relaunched after pressing the home button gets an initial surface with
                // the wrong orientation even though android:screenOrientation="landscape" is set in the
                // manifest. The choreographer callback will also never be called for this surface because
                // the surface is immediately replaced with a new surface with the correct orientation.
                //ALOGE("        Surface not in landscape mode!");
            }

            if (newNativeWindow != appThread.NativeWindow)
            {
                if (appThread.NativeWindow != null)
                {
                    var message = default(VrCubeWorld.ovrMessage);
                    message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_SURFACE_DESTROYED, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
                    appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);

                    native_window.ANativeWindow_release(appThread.NativeWindow);
                    appThread.NativeWindow = default(native_window.ANativeWindow);

                }
                if (newNativeWindow != null)
                {
                    var message = default(VrCubeWorld.ovrMessage);
                    message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_SURFACE_CREATED, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
                    appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);

                    native_window.ANativeWindow_release(appThread.NativeWindow);
                    appThread.NativeWindow = default(native_window.ANativeWindow);

                }
            }
            else if (newNativeWindow != null)
            {
                native_window.ANativeWindow_release(newNativeWindow);
            }
        }

        static void onSurfaceDestroyed(JNIEnv env, jobject obj)
        {
            // 2074
            //var appThread = (VrCubeWorld.ovrAppThread)handle;
            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_SURFACE_DESTROYED, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);

            native_window.ANativeWindow_release(appThread.NativeWindow);
            appThread.NativeWindow = default(native_window.ANativeWindow);
        }
        #endregion


        #region Input

        public static void onKeyEvent(JNIEnv env, jobject obj, int keyCode, int action)
        {
            // 2094
            //var appThread = (VrCubeWorld.ovrAppThread)handle;
            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_KEY_EVENT, VrCubeWorld.ovrMQWait.MQ_WAIT_NONE);
            message[0] = keyCode;
            message[1] = action;
            // ovrApp_HandleKeyEvent
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
        }


        public static void onTouchEvent(JNIEnv env, jobject obj, int action, float x, float y)
        {
            // 2108
            //var appThread = (VrCubeWorld.ovrAppThread)handle;
            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_TOUCH_EVENT, VrCubeWorld.ovrMQWait.MQ_WAIT_NONE);
            message[0] = action;
            message[1] = x;
            message[2] = y;
            // ovrApp_HandleTouchEvent
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
        }
        #endregion
#else

        [Script(IsPInvoke = true)]
        public static string stringFromJNI(object args) { return default(string); }

        #region Activity lifecycle
        [Script(IsPInvoke = true)]
        public static ovrAppThreadPointer onCreate(object activity) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onStart() { throw null; }
        [Script(IsPInvoke = true)]
        public static void onResume() { throw null; }
        [Script(IsPInvoke = true)]
        public static void onPause() { throw null; }
        [Script(IsPInvoke = true)]
        public static void onStop() { throw null; }
        [Script(IsPInvoke = true)]
        public static void onDestroy() { throw null; }
        #endregion

        #region Surface lifecycle
        [Script(IsPInvoke = true)]
        public static void onSurfaceCreated(object surface) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onSurfaceChanged(object surface) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onSurfaceDestroyed() { throw null; }
        #endregion

        #region Input
        [Script(IsPInvoke = true)]
        public static void onKeyEvent(int keyCode, int action) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onTouchEvent(int action, float x, float y) { throw null; }
        #endregion
#endif
    }
}

