//using ANativeWindow = System.Object;

using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Script()]
[assembly: ScriptTypeFilter(ScriptType.C, typeof(OVRVrCubeWorldSurfaceViewXNDK.VrCubeWorld))]
[assembly: ScriptTypeFilter(ScriptType.C, typeof(Java.com.oculus.gles3jni.GLES3JNILib))]


namespace OVRVrCubeWorldSurfaceViewXNDK
{

    using EGLDisplay = Object;
    using EGLConfig = Object;
    using EGLSurface = Object;
    using EGLContext = Object;

    using GLuint = Object;
    using GLint = Int32;
    using GLenum = Object;
    using GLboolean = Object;
    using GLsizei = Object;

    using EGLint = Int32;
    //enum EGLint { }
    //enum GLint { }

    using ovrVector3f = Object;
    using ovrMatrix4f = Object;

    //using JavaVM = Object;
    //using jobject = Object;
    //using pthread_t = Object;
    using pthread_cond_t = Object;
    using pthread_mutex_t = Object;
    using ovrMobile = Object;
    using ovrTracking = Object;

    //using ovrMQWait = Object;
    using ovrJava = Object;
    using ScriptCoreLibNative.SystemHeaders.android;

    [Script]
    public static unsafe partial class VrCubeWorld
    {

        static void EglErrorString() { }
        static void EglFrameBufferStatusString() { }
        static void EglCheckErrors() { }

        [Script]
        class ovrEgl
        {
            public EGLint MajorVersion;
            public EGLint MinorVersion;
            public EGLDisplay Display;
            public EGLConfig Config;
            public EGLSurface TinySurface;
            public EGLSurface MainSurface;
            public EGLContext Context;
        }

        static void ovrEgl_Clear(this ovrEgl that) { }
        static void ovrEgl_CreateContext(this ovrEgl that) { }
        static void ovrEgl_DestroyContext(this ovrEgl that) { }
        static void ovrEgl_CreateSurface(this ovrEgl that) { }
        static void ovrEgl_DestroySurface(this ovrEgl that) { }

        [Script]
        class ovrVertexAttribPointer
        {
            public GLuint Index;
            public GLint Size;
            public GLenum Type;
            public GLboolean Normalized;
            public GLsizei Stride;
            public void* Pointer;
        }

        const int MAX_VERTEX_ATTRIB_POINTERS = 3;

        [Script]
        class ovrGeometry
        {
            public GLuint VertexBuffer;
            public GLuint IndexBuffer;
            public GLuint VertexArrayObject;
            public int VertexCount;
            public int IndexCount;
            public ovrVertexAttribPointer[] VertexAttribs;
            //public fixed ovrVertexAttribPointer VertexAttribs[MAX_VERTEX_ATTRIB_POINTERS];

            //Error CS0270  Array size cannot be specified in a variable declaration(try initializing with a 'new' expression) OVRVrCubeWorldSurfaceViewXNDK X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs	73
            //Error CS0650  Bad array declarator: To declare a managed array the rank specifier precedes the variable's identifier. To declare a fixed size buffer field, use the fixed keyword before the field type.	OVRVrCubeWorldSurfaceViewXNDK	X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs	73
            //Error CS1642  Fixed size buffer fields may only be members of structs OVRVrCubeWorldSurfaceViewXNDK X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs	73
            //Error CS0270  Array size cannot be specified in a variable declaration(try initializing with a 'new' expression) OVRVrCubeWorldSurfaceViewXNDK X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs	73
            //Error CS1641  A fixed size buffer field must have the array size specifier after the field name   OVRVrCubeWorldSurfaceViewXNDK   X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs 73
            //Error   CS1663  Fixed size buffer type must be one of the following: bool, byte, short, int, long, char, sbyte, ushort, uint, ulong, float or double    OVRVrCubeWorldSurfaceViewXNDK   X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs 73


        }

        enum ovrVertexAttribute_location
        {
            VERTEX_ATTRIBUTE_LOCATION_POSITION,
            VERTEX_ATTRIBUTE_LOCATION_COLOR,
            VERTEX_ATTRIBUTE_LOCATION_UV,
            VERTEX_ATTRIBUTE_LOCATION_TRANSFORM
        }

        [Script]
        class ovrVertexAttribute
        {
            public ovrVertexAttribute_location location;
            public string name;
        }

        //// does jsc initialize statically ot at cctor?
        //static ovrVertexAttribute[] ProgramVertexAttributes =
        //    new[]
        //    {
        //        new ovrVertexAttribute { location = ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_POSITION,  name = "vertexPosition" },
        //        new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_COLOR,      name = "vertexColor" },
        //        new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_UV,      name =         "vertexUv" },
        //        new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_TRANSFORM,      name =  "vertexTransform" }

        //    };

        static void ovrGeometry_Clear(this ovrGeometry that) { }
        static void ovrGeometry_CreateCube(this ovrGeometry that) { }
        static void ovrGeometry_Destroy(this ovrGeometry that) { }
        static void ovrGeometry_CreateVAO(this ovrGeometry that) { }
        static void ovrGeometry_DestroyVAO(this ovrGeometry that) { }


        public const int MAX_PROGRAM_UNIFORMS = 8;
        public const int MAX_PROGRAM_TEXTURES = 8;

        [Script]
        //struct ovrProgram
        class ovrProgram
        {
            public GLuint Program;
            public GLuint VertexShader;
            public GLuint FragmentShader;
            // These will be -1 if not used by the program.
            //public fixed GLint Uniforms[MAX_PROGRAM_UNIFORMS];       // ProgramUniforms[].name
            public GLint[] Uniforms;       // ProgramUniforms[].name
            public GLint[] Textures;      // Texture%i

            //Error CS1663  Fixed size buffer type must be one of the following: bool, byte, short, int, long, char, sbyte, ushort, uint, ulong, float or double OVRVrCubeWorldSurfaceViewXNDK   X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs	130
            //Error CS1642  Fixed size buffer fields may only be members of structs OVRVrCubeWorldSurfaceViewXNDK X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs	129
        }

        public enum ovrUniform_index
        {
            UNIFORM_MODEL_MATRIX,
            UNIFORM_VIEW_MATRIX,
            UNIFORM_PROJECTION_MATRIX
        }

        public enum ovrUniform_type
        {
            UNIFORM_TYPE_VECTOR4,
            UNIFORM_TYPE_MATRIX4X4,
        }

        [Script]
        class ovrUniform
        {
            public ovrUniform_index index;
            public ovrUniform_type type;
            public string name;

            // using static enum?
        }

        //    static ovrUniform[] ProgramUniforms =
        //        new[]
        //        {
        //new ovrUniform { index=ovrUniform_index.UNIFORM_MODEL_MATRIX,         type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ModelMatrix" },
        //new ovrUniform { index=ovrUniform_index.UNIFORM_VIEW_MATRIX,          type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ViewMatrix" },
        //new ovrUniform { index=ovrUniform_index.UNIFORM_PROJECTION_MATRIX,    type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ProjectionMatrix" }

        //        };

        static void ovrProgram_Clear(this ovrProgram that) { }
        static void ovrProgram_Create(this ovrProgram that) { }
        static void ovrProgram_Destroy(this ovrProgram that) { }

        [Script]
        class ovrRenderTexture
        {
            public int Width;
            public int Height;
            public int Multisamples;
            public GLuint ColorTexture;
            public GLuint DepthBuffer;
            public GLuint FrameBuffer;
        }

        static void ovrRenderTexture_Clear(this ovrRenderTexture that) { }
        static void ovrRenderTexture_Create(this ovrRenderTexture that) { }
        static void ovrRenderTexture_Destroy(this ovrRenderTexture that) { }
        static void ovrRenderTexture_SetCurrent(this ovrRenderTexture that) { }
        static void ovrRenderTexture_SetNone(this ovrRenderTexture that) { }
        static void ovrRenderTexture_Resolve(this ovrRenderTexture that) { }

        public const int NUM_INSTANCES = 1500;

        [Script]
        class ovrScene
        {
            public bool CreatedScene;
            public bool CreatedVAOs;
            public ovrProgram Program;
            public ovrGeometry Cube;
            public GLuint InstanceTransformBuffer;
            public ovrVector3f[] CubePositions;
            public ovrVector3f[] CubeRotations;
        }

        // assetslibrary
        public const string VERTEX_SHADER = @"
";
        public const string FRAGMENT_SHADER = @"
";

        static void ovrScene_Clear(this ovrScene that) { }
        static void ovrScene_IsCreated(this ovrScene that) { }
        static void ovrScene_CreateVAOs(this ovrScene that) { }
        static void ovrScene_DestroyVAOs(this ovrScene that) { }
        static void ovrScene_Create(this ovrScene that) { }
        static void ovrScene_Destroy(this ovrScene that) { }

        [Script]
        class ovrSimulation
        {
            ovrVector3f CurrentRotation;
        }

        static void ovrSimulation_Clear(this ovrSimulation that)
        { }
        static void ovrSimulation_AdvanceSimulation(this ovrSimulation that) { }

        [Script]
        class ovrRenderer
        {
            public ovrRenderTexture[][] RenderTextures;
            public int BufferIndex;
            public ovrMatrix4f ProjectionMatrix;
            public ovrMatrix4f TanAngleMatrix;
        }

        static void ovrRenderer_Clear(this ovrRenderer that) { }
        static void ovrRenderer_Create(this ovrRenderer that) { }
        static void ovrRenderer_Destroy(this ovrRenderer that) { }
        static void ovrRenderer_RenderFrame(this ovrRenderer that) { }

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
        }

        static void RenderThreadFunction() { }
        static void ovrRenderThread_Clear(this ovrRenderThread that) { }
        static void ovrRenderThread_Create(this ovrRenderThread that) { }
        static void ovrRenderThread_Destroy(this ovrRenderThread that) { }
        static void ovrRenderThread_Submit(this ovrRenderThread that) { }
        static void ovrRenderThread_Wait(this ovrRenderThread that) { }
        static void ovrRenderThread_GetTid(this ovrRenderThread that) { }

        enum ovrBackButtonState
        {
            BACK_BUTTON_STATE_NONE,
            BACK_BUTTON_STATE_PENDING_DOUBLE_TAP,
            BACK_BUTTON_STATE_PENDING_SHORT_PRESS,
            BACK_BUTTON_STATE_SKIP_UP
        }
       ;

        [Script]
        class ovrApp
        {
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
            public ovrRenderer Renderer;
#endif
        }

        static void ovrApp_Clear(this ovrApp that) { }
        static void ovrApp_HandleVrModeChanges(this ovrApp that) { }
        static void ovrApp_BackButtonAction(this ovrApp that) { }
        static void ovrApp_HandleKeyEvent(this ovrApp that) { }
        static void ovrApp_HandleTouchEvent(this ovrApp that) { }
        static void ovrApp_HandleSystemEvents(this ovrApp that) { }


        public enum ovrMQWait
        {
            MQ_WAIT_NONE,		// don't wait
            MQ_WAIT_RECEIVED,	// wait until the consumer thread has received the message
            MQ_WAIT_PROCESSED	// wait until the consumer thread has processed the message
        };

        [Script]
        // why struct?
        public struct ovrMessage
        {
            public int Id;
            public ovrMQWait Wait;
            public long[] Parms;

            public void ovrMessage_Init(MESSAGE mESSAGE, ovrMQWait ovrMQWait)
            {
                // this byref?

            }

            public void ovrMessage_SetPointerParm() { }
            public void ovrMessage_GetPointerParm() { }
            public void ovrMessage_SetIntegerParm(int i, int value) { }
            public void ovrMessage_GetIntegerParm() { }
            public void ovrMessage_SetFloatParm(int i, float value) { }
            public void ovrMessage_GetFloatParm() { }


        }




        // struct?
        [Script]
        public class ovrMessageQueue
        {
            public ovrMessage[] Messages;
            public volatile int Head;  // dequeue at the head
            public volatile int Tail;  // enqueue at the tail
            public volatile bool Enabled;
            public ovrMQWait Wait;
            public pthread_mutex_t Mutex;
            public pthread_cond_t Posted;
            public pthread_cond_t Received;
            public pthread_cond_t Processed;


            // called by Java_com_oculus_gles3jni_GLES3JNILib_onTouchEvent
            public void ovrMessageQueue_PostMessage(ref ovrMessage message)
            {

            }

            public void ovrMessageQueue_Create() { }
            public void ovrMessageQueue_Destroy() { }
            public void ovrMessageQueue_Enable(bool v) { }
            public void ovrMessageQueue_SleepUntilMessage() { }
            public void ovrMessageQueue_GetNextMessage() { }
        }



        // nameless in the c file.
        public enum MESSAGE
        {
            MESSAGE_ON_CREATE,
            MESSAGE_ON_START,
            MESSAGE_ON_RESUME,
            MESSAGE_ON_PAUSE,
            MESSAGE_ON_STOP,
            MESSAGE_ON_DESTROY,
            MESSAGE_ON_SURFACE_CREATED,
            MESSAGE_ON_SURFACE_DESTROYED,
            MESSAGE_ON_KEY_EVENT,
            MESSAGE_ON_TOUCH_EVENT
        }

        // converted from size_t
        // can do a sizeof for malloc?
        // sizeof not available for managed members?
        [Script]
        public class ovrAppThread
        {
            // set via ovrAppThread_Create
            public JavaVM JavaVm;

            public jobject ActivityObject;
            public pthread_t Thread;
            public ovrMessageQueue MessageQueue;

            // set to null by onSurfaceDestroyed
            public native_window.ANativeWindow NativeWindow;
        }

        //static object AppThreadFunction(object arg)
        static object AppThreadFunction(ovrAppThread appThread)
        {
            //ovrAppThread* appThread = (ovrAppThread*)parm;

            return null;
        }

        public static void ovrAppThread_Create(this ovrAppThread appThread, ref JNIEnv env, jobject activityObject)
        {
            // System.NotImplementedException: { ParameterType = ScriptCoreLibNative.SystemHeaders.pthread+pthread_t&,

            env.GetJavaVM(ref env, out appThread.JavaVm);

            appThread.ActivityObject = env.NewGlobalRef(ref env, activityObject);
            //appThread.Thread = default(pthread_t);
            appThread.NativeWindow = null;

            appThread.MessageQueue.ovrMessageQueue_Create();

            // ldfda ?
            var createErr = pthread.pthread_create(out appThread.Thread, null, AppThreadFunction, appThread);


            //var createErr2 = pthread.pthread_create(out appThread.Thread, null,
            //    arg: appThread,
            //    start_routine: (ovrAppThread appThread0) =>
            //    {
            //        // scope sharing via arg0, in c! ready for roslyn?

            //        return null;
            //    }
            //);
        }

        public static void ovrAppThread_Destroy(this ovrAppThread appThread, ref JNIEnv env)
        {
            pthread.pthread_join(appThread.Thread, null);
            env.DeleteGlobalRef(ref env, appThread.ActivityObject);
            appThread.MessageQueue.ovrMessageQueue_Destroy();
        }

        // java, jsc hybrid?
    }

}

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
            static refovrAppThread onCreate(ref JNIEnv env, jobject obj, jobject activity)
            {
                // Error	4	Cannot take the address of, get the size of, or declare a pointer to a managed type ('OVRVrCubeWorldSurfaceViewXNDK.xovrAppThread')	X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs	489	40	OVRVrCubeWorldSurfaceViewXNDK


                var appThread = new VrCubeWorld.ovrAppThread();

                // jsc would calla ctor for us...
                VrCubeWorld.ovrAppThread_Create(appThread, ref env, activity);

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

            static void onDestroy(ref JNIEnv env, jobject obj, jlong handle)
            {
                var __handle = (size_t)handle;
                var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);

                var message = default(VrCubeWorld.ovrMessage);
                message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_DESTROY, VrCubeWorld.ovrMQWait.MQ_WAIT_PROCESSED);
                appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);

                appThread.MessageQueue.ovrMessageQueue_Enable(false);


                VrCubeWorld.ovrAppThread_Destroy(appThread, ref env);

                stdlib_h.free(appThread);
            }

            #endregion




            #region onSurface
            static void onSurfaceCreated(ref JNIEnv env, jobject obj, jlong handle, jobject surface)
            {

                var __handle = (size_t)handle;
                var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);

                var newNativeWindow = native_window_jni.ANativeWindow_fromSurface(ref env, surface);
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
                appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
            }
            static void onSurfaceChanged(ref JNIEnv env, jobject obj, jlong handle, jobject surface)
            {
                var __handle = (size_t)handle;
                var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);


                var newNativeWindow = native_window_jni.ANativeWindow_fromSurface(ref env, surface);
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
            static void onSurfaceDestroyed(ref JNIEnv env, jobject obj, jlong handle)
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


            static void onKeyEvent(ref JNIEnv env, jobject obj, jlong handle, int keyCode, int action)
            {
                var __handle = (size_t)handle;

                var appThread = (VrCubeWorld.ovrAppThread)(object)(__handle);
                var message = default(VrCubeWorld.ovrMessage);
                message.ovrMessage_Init(VrCubeWorld.MESSAGE.MESSAGE_ON_KEY_EVENT, VrCubeWorld.ovrMQWait.MQ_WAIT_NONE);
                message.ovrMessage_SetIntegerParm(0, keyCode);
                message.ovrMessage_SetIntegerParm(1, action);
                appThread.MessageQueue.ovrMessageQueue_PostMessage(ref message);
            }


            // public static void onTouchEvent(long handle, int action, float x, float y) { throw null; }

            //static void Java_com_oculus_gles3jni_GLES3JNILib_onTouchEvent(
            static void onTouchEvent(
                 ref JNIEnv env,
                 jobject obj,

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

                appThread.MessageQueue.ovrMessageQueue_PostMessage(
                    ref message
                );
            }
        }
    }
}
