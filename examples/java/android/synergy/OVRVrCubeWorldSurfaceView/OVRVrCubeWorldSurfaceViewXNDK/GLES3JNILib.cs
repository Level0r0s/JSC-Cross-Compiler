using ScriptCoreLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if JNIEnv
using OVRVrCubeWorldSurfaceViewXNDK;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;

// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150619/ovrvrcubeworldsurfaceviewx
// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150613/xndk
// java/c jsc hybrid layer gen?

[assembly: Script(IsScriptLibrary = true)]
[assembly: ScriptTypeFilter(ScriptType.C)]

// now we have _PrivateImplementationDetails__24B4BA40_DFD3_4256_8345_0CEACAEC01A3____method0x600005e_1

//[assembly: ScriptTypeFilter(ScriptType.C, typeof(OVRVrCubeWorldSurfaceViewXNDK.VrCubeWorld))]
//[assembly: ScriptTypeFilter(ScriptType.C, typeof(com.oculus.gles3jni.GLES3JNILib))]

// .so export needs to prefox java methods by Java.
[assembly: ScriptNamespaceRename(NativeNamespaceName = "com.oculus.gles3jni", VirtualNamespaceName = "Java.com.oculus.gles3jni")]

#endif

namespace com.oculus.gles3jni
{
    // can we have named pointer between java and ndk?
    public enum ovrAppThreadPointer : long { }

    unsafe static class GLES3JNILib
    {
        // ifelse does not look that good. lets keep two sets of signatures..
#if JNIEnv
        // JVM load the .so and calls this native function
        public static jstring stringFromJNI( JNIEnv env, jobject thiz)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150607-1/vrcubeworld
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150525


            // look almost the same file!

            // OVR_VRAPI_EXPORT const char * vrapi_GetVersionString();

            // if we change our NDK code, will nuget packaing work on the background, and also upgrade running apps?
            var v = env.NewStringUTF( env,
                "hey! XNDK"

                //VrApi_h.vrapi_GetVersionString()
            );

            return v;
        }



        #region Activity lifecycle
        // named pointers! typedefs!
        static ovrAppThreadPointer onCreate(JNIEnv env, jobject obj, jobject activity)
        {
            // Error	4	Cannot take the address of, get the size of, or declare a pointer to a managed type ('OVRVrCubeWorldSurfaceViewXNDK.xovrAppThread')	X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs	489	40	OVRVrCubeWorldSurfaceViewXNDK


            var appThread = new VrCubeWorld.ovrAppThread(env, activity);

            

            // set property?
            appThread.MessageQueue.ovrMessageQueue_Enable(true);

            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_CREATE, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);


            //stdlib_h.malloc(sizeof(VrCubeWorld.ovrAppThread));
            //stdlib_h.malloc(sizeof(xovrAppThread));

            //var __handle = (size_t)(object)appThread;
            //var __ref = (ovrAppThreadPointer)(object)__handle;

            return appThread;
        }

        #region wndproc/ovrMessageQueue_PostMessage
        static void onStart(ref JNIEnv env, jobject obj, ovrAppThreadPointer handle)
        {
            //var __handle = (size_t)handle;
            //var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);
            var appThread = (VrCubeWorld.ovrAppThread)handle;

            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_START, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
        }
        static void onResume(ref JNIEnv env, jobject obj, ovrAppThreadPointer handle)
        {
            //var __handle = (size_t)handle;
            //var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);
            var appThread = (VrCubeWorld.ovrAppThread)handle;

            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_RESUME, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
        }
        static void onPause(ref JNIEnv env, jobject obj, ovrAppThreadPointer handle)
        {
            //var __handle = (size_t)handle;
            //var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);
            var appThread = (VrCubeWorld.ovrAppThread)handle;

            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_PAUSE, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
        }
        static void onStop(ref JNIEnv env, jobject obj, ovrAppThreadPointer handle)
        {
            //var __handle = (size_t)handle;
            //var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);
            var appThread = (VrCubeWorld.ovrAppThread)handle;

            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_STOP, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
        }

        #endregion

        static void onDestroy(JNIEnv env, jobject obj, ovrAppThreadPointer handle)
        {
            //var __handle = (size_t)handle;
            //var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);
            var appThread = (VrCubeWorld.ovrAppThread)handle;

            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_DESTROY, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);

            appThread.MessageQueue.ovrMessageQueue_Enable(false);


            appThread.ovrAppThread_Destroy(env);

            // finalize
            //~appThread();

            stdlib_h.free(appThread);
        }

        #endregion

        #region Surface lifecycle
        static void onSurfaceCreated(JNIEnv env, jobject obj, ovrAppThreadPointer handle, jobject surface)
        {

            //var __handle = (size_t)handle;
            //var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);
            var appThread = (VrCubeWorld.ovrAppThread)handle;

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
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
        }
        static void onSurfaceChanged(JNIEnv env, jobject obj, ovrAppThreadPointer handle, jobject surface)
        {
            //var __handle = (size_t)handle;
            //var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);
            var appThread = (VrCubeWorld.ovrAppThread)handle;


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

        static void onSurfaceDestroyed(JNIEnv env, jobject obj, ovrAppThreadPointer handle)
        {
            //var __handle = (size_t)handle;
            //var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);
            var appThread = (VrCubeWorld.ovrAppThread)handle;

            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_SURFACE_DESTROYED, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);

            native_window.ANativeWindow_release(appThread.NativeWindow);
            appThread.NativeWindow = default(native_window.ANativeWindow);
        }
        #endregion


        #region Input

        // public static void onKeyEvent(long handle, int keyCode, int action) { throw null; }
        public static void onKeyEvent(
            JNIEnv env, jobject obj, ovrAppThreadPointer handle, int keyCode, int action)
        {
 

            //var __handle = (size_t)(object)handle;
            //var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);
            var appThread = (VrCubeWorld.ovrAppThread)handle;
            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_KEY_EVENT, VrCubeWorld.ovrMQWait.MQ_WAIT_NONE);
            message[0] = keyCode;
            message[1] = action;
            //message.ovrMessage_SetIntegerParm(0, keyCode);
            //message.ovrMessage_SetIntegerParm(1, action);
            // ovrApp_HandleKeyEvent
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
 
        }


        // public static void onTouchEvent(long handle, int action, float x, float y) { throw null; }
        public static void onTouchEvent(
            JNIEnv env, jobject obj,
            ovrAppThreadPointer handle, int action, float x, float y)
        {
 

            //var __handle = (size_t)(object)handle;

            // await into native?
            // means prepend Java namespace
            // and ref JNIEnv?

            //X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\jni.cs
            
            //var appThread = (VrCubeWorld.ovrAppThread)(object)__handle;
            var appThread = (VrCubeWorld.ovrAppThread)handle;

            var message = default(VrCubeWorld.ovrMessage);

            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_TOUCH_EVENT, VrCubeWorld.ovrMQWait.MQ_WAIT_NONE);
            message[0] = action;
            message[1] = x;
            message[2] = y;

            //message.ovrMessage_SetIntegerParm(0, action);
            //message.ovrMessage_SetFloatParm(1, x);
            //message.ovrMessage_SetFloatParm(2, y);

            // ovrApp_HandleTouchEvent
            appThread.MessageQueue.ovrMessageQueue_PostMessage(
                ref message
            );
 
        }
        #endregion
#else
        
        [Script(IsPInvoke = true)]
        public static string stringFromJNI() { return default(string); }

        #region Activity lifecycle
        [Script(IsPInvoke = true)]
        public static ovrAppThreadPointer onCreate(object obj) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onStart(long handle) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onResume(long handle) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onPause(long handle) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onStop(long handle) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onDestroy(long handle) { throw null; }
        #endregion

        #region Surface lifecycle
        [Script(IsPInvoke = true)]
        public static void onSurfaceCreated(long handle, object s) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onSurfaceChanged(long handle, object s) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onSurfaceDestroyed(long handle) { throw null; }
        #endregion

        #region Input
        [Script(IsPInvoke = true)]
        public static void onKeyEvent(long handle, int keyCode, int action) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onTouchEvent(long handle, int action, float x, float y) { throw null; }
        #endregion
#endif
    }
}

