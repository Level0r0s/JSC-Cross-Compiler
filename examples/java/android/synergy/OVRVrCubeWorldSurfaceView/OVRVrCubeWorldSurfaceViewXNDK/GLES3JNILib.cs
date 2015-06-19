using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150613/xndk
// java/c jsc hybrid layer gen?
namespace Java
{
    namespace com.oculus.gles3jni
    {
        using refovrAppThread = Int64;

        // named pointers for jsc hybrid apps?
        //public enum refovrAppThread : long { }

        using OVRVrCubeWorldSurfaceViewXNDK;
        using ScriptCoreLibNative.SystemHeaders.android;



        [Script]
        unsafe static class GLES3JNILib
        {
            #region Activity
            // named pointers! typedefs!
            static refovrAppThread onCreate(JNIEnv env, jobject obj, jobject activity)
            {
                // Error	4	Cannot take the address of, get the size of, or declare a pointer to a managed type ('OVRVrCubeWorldSurfaceViewXNDK.xovrAppThread')	X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs	489	40	OVRVrCubeWorldSurfaceViewXNDK


                var appThread = new VrCubeWorld.ovrAppThread();

                // jsc would calla ctor for us...
                appThread.ovrAppThread_Create(env, activity);

                // set property?
                appThread.MessageQueue.ovrMessageQueue_Enable(true);

                var message = default(VrCubeWorld.ovrMessage);
                message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_CREATE, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
                appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);


                //stdlib_h.malloc(sizeof(VrCubeWorld.ovrAppThread));
                //stdlib_h.malloc(sizeof(xovrAppThread));

                var __handle = (size_t)(object)appThread;
                var __ref = (refovrAppThread)(object)__handle;

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




            #region onSurface
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


            static void onKeyEvent(JNIEnv env, jobject obj, jlong handle, int keyCode, int action)
            {
                var __handle = (size_t)handle;

                var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);
                var message = default(VrCubeWorld.ovrMessage);
                message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_KEY_EVENT, VrCubeWorld.ovrMQWait.MQ_WAIT_NONE);
                message.ovrMessage_SetIntegerParm(0, keyCode);
                message.ovrMessage_SetIntegerParm(1, action);
                // ovrApp_HandleKeyEvent
                appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
            }


            // public static void onTouchEvent(long handle, int action, float x, float y) { throw null; }

            //static void Java_com_oculus_gles3jni_GLES3JNILib_onTouchEvent(
            static void onTouchEvent(JNIEnv env, jobject obj,

                 // mNativeHandle
                 jlong handle, int action, float x, float y)
            {
                var __handle = (size_t)handle;

                // await into native?
                // means prepend Java namespace
                // and ref JNIEnv?



                //X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\jni.cs

                //Error CS0208  Cannot take the address of, get the size of, or declare a pointer to a managed type('VrCubeWorld.ovrAppThread')    OVRVrCubeWorldSurfaceViewXNDK X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs 413
                // jni/OVRVrCubeWorldSurfaceViewXNDK.dll.c:566:75: error: cast to pointer from integer of different size [-Werror=int-to-pointer-cast]
                //var appThread = (ovrAppThread*)(void*)handle;

                // size_t
                var appThread = (VrCubeWorld.ovrAppThread)(object)__handle;

                var message = default(VrCubeWorld.ovrMessage);


                //     OVRVrCubeWorldSurfaceViewXNDK_VrCubeWorld_ovrMessage_ovrMessage_Init((OVRVrCubeWorldSurfaceViewXNDK_VrCubeWorld_ovrMessage)message1);
                message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_TOUCH_EVENT, VrCubeWorld.ovrMQWait.MQ_WAIT_NONE);

                message.ovrMessage_SetIntegerParm(0, action);
                message.ovrMessage_SetFloatParm(1, x);
                message.ovrMessage_SetFloatParm(2, y);
                // ovrApp_HandleTouchEvent
                appThread.MessageQueue.ovrMessageQueue_PostMessage(
                    ref message
                );
            }
        }
    }
}
