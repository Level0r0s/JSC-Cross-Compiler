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
    unsafe static class GLES3JNILib
    {
#if JNIEnv

        #region Activity lifecycle
        // named pointers! typedefs!
        static long onCreate(JNIEnv env, jobject obj, jobject activity)
        {
            // Error	4	Cannot take the address of, get the size of, or declare a pointer to a managed type ('OVRVrCubeWorldSurfaceViewXNDK.xovrAppThread')	X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs	489	40	OVRVrCubeWorldSurfaceViewXNDK


            var appThread = new VrCubeWorld.ovrAppThread(env, activity);

            // jsc would calla ctor for us...
            //appThread.ovrAppThread_Create(env, activity);

            // set property?
            appThread.MessageQueue.ovrMessageQueue_Enable(true);

            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_CREATE, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);


            //stdlib_h.malloc(sizeof(VrCubeWorld.ovrAppThread));
            //stdlib_h.malloc(sizeof(xovrAppThread));

            var __handle = (size_t)(object)appThread;
            var __ref = (long)(object)__handle;

            return __ref;
        }

        #region wndproc/ovrMessageQueue_PostMessage
        static void onStart(ref JNIEnv env, jobject obj, jlong handle)
        {
            var __handle = (size_t)handle;
            var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);

            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_START, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
        }
        static void onResume(ref JNIEnv env, jobject obj, jlong handle)
        {
            var __handle = (size_t)handle;
            var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);

            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_RESUME, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
        }
        static void onPause(ref JNIEnv env, jobject obj, jlong handle)
        {
            var __handle = (size_t)handle;
            var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);

            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_PAUSE, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
        }
        static void onStop(ref JNIEnv env, jobject obj, jlong handle)
        {
            var __handle = (size_t)handle;
            var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);

            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_STOP, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
        }

        #endregion

        static void onDestroy(JNIEnv env, jobject obj, jlong handle)
        {
            var __handle = (size_t)handle;
            var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);

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
        static void onSurfaceCreated(JNIEnv env, jobject obj, jlong handle, jobject surface)
        {

            var __handle = (size_t)handle;
            var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);

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
        static void onSurfaceChanged(JNIEnv env, jobject obj, jlong handle, jobject surface)
        {
            var __handle = (size_t)handle;
            var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);


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

        static void onSurfaceDestroyed(JNIEnv env, jobject obj, jlong handle)
        {
            var __handle = (size_t)handle;
            var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);

            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_SURFACE_DESTROYED, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);

            native_window.ANativeWindow_release(appThread.NativeWindow);
            appThread.NativeWindow = default(native_window.ANativeWindow);
        }
        #endregion
#endif

        #region Input

        // public static void onKeyEvent(long handle, int keyCode, int action) { throw null; }
        public static void onKeyEvent(
#if JNIEnv
            JNIEnv env, jobject obj, 
#endif
long handle, int keyCode, int action)
        {
#if JNIEnv

            var __handle = (size_t)(object)handle;

            var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);
            var message = default(VrCubeWorld.ovrMessage);
            message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_KEY_EVENT, VrCubeWorld.ovrMQWait.MQ_WAIT_NONE);
            message[0] = keyCode;
            message[1] = action;
            //message.ovrMessage_SetIntegerParm(0, keyCode);
            //message.ovrMessage_SetIntegerParm(1, action);
            // ovrApp_HandleKeyEvent
            appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
#else
            throw null;
#endif
        }


        // public static void onTouchEvent(long handle, int action, float x, float y) { throw null; }
        public static void onTouchEvent(
#if JNIEnv
            JNIEnv env, jobject obj,
#endif
long handle, int action, float x, float y)
        {
#if JNIEnv

            var __handle = (size_t)(object)handle;

            // await into native?
            // means prepend Java namespace
            // and ref JNIEnv?

            //X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\jni.cs
            
            var appThread = (VrCubeWorld.ovrAppThread)(object)__handle;

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
#else
            throw null;
#endif
        }
        #endregion

    }
}

