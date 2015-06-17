//using ANativeWindow = System.Object;

using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLibNative.SystemHeaders.android;

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


            public void ovrEgl_Clear() { }
            public void ovrEgl_CreateContext(object e) { }
            public void ovrEgl_DestroyContext() { }
            public void ovrEgl_CreateSurface() { }
            public void ovrEgl_DestroySurface() { }
        }


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

            public void ovrScene_Clear() { }
            public bool ovrScene_IsCreated()
            {

                return false;
            }
            public void ovrScene_CreateVAOs() { }
            public void ovrScene_DestroyVAOs() { }
            public void ovrScene_Create() { }
            public void ovrScene_Destroy() { }

        }

        // assetslibrary
        public const string VERTEX_SHADER = @"
";
        public const string FRAGMENT_SHADER = @"
";


        [Script]
        class ovrSimulation
        {
            ovrVector3f CurrentRotation;


            public void ovrSimulation_AdvanceSimulation(double predictedDisplayTime)
            {

            }
        }

        static void ovrSimulation_Clear(this ovrSimulation that)
        { }

        [Script]
        class ovrRenderer
        {
            public ovrRenderTexture[][] RenderTextures;
            public int BufferIndex;
            public ovrMatrix4f ProjectionMatrix;
            public ovrMatrix4f TanAngleMatrix;


            // sent into vrapi_SubmitFrame
            public ovrFrameParms ovrRenderer_RenderFrame(ref ovrApp appState, ref ovrTracking tracking)
            {
                var x = default(ovrFrameParms);


                x.FrameIndex = 0;

                return x;
            }

            public void ovrRenderer_Clear() { }
            public void ovrRenderer_Create(ref ovrHmdInfo hmdInfo) { }
            public void ovrRenderer_Destroy() { }
        }




        enum ovrRenderType
        {
            RENDER_FRAME,
            RENDER_LOADING_ICON,
            RENDER_BLACK_FLUSH,
            RENDER_BLACK_FINAL
        }
       ;

        #region MULTI_THREADED
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
        #endregion

        enum ovrBackButtonState
        {
            BACK_BUTTON_STATE_NONE,
            BACK_BUTTON_STATE_PENDING_DOUBLE_TAP,
            BACK_BUTTON_STATE_PENDING_SHORT_PRESS,
            BACK_BUTTON_STATE_SKIP_UP
        }
       ;



        // stackalloc at X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.AppThreadFunction.cs
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
            public ovrRenderer Renderer;
#endif


            public void ovrApp_Clear() { }
            public void ovrApp_HandleVrModeChanges() { }
            public void ovrApp_BackButtonAction() { }

            // java UI sends over to native, which the uses MQ to send over to bg thread? SharedMemory would be nice?
            // onKeyEvent
            public void ovrApp_HandleKeyEvent(int keyCode, int action) { }

            // onTouchEvent
            public void ovrApp_HandleTouchEvent(int action, float x, float y) { }

            public void ovrApp_HandleSystemEvents() { }

        }



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
            public VrCubeWorld.MESSAGE Id;
            public ovrMQWait Wait;
            public long[] Parms;

            public void ovrMessage_Init(MESSAGE mESSAGE, ovrMQWait ovrMQWait)
            {
                // this byref?

            }

            public void ovrMessage_SetPointerParm(int i, object value) { }
            public object ovrMessage_GetPointerParm(int i) { return null; }
            public void ovrMessage_SetIntegerParm(int i, int value) { }
            public int ovrMessage_GetIntegerParm(int i) { return 0; }
            public void ovrMessage_SetFloatParm(int i, float value) { }
            public float ovrMessage_GetFloatParm(int i)
            {
                //script: error JSC1000: C : Opcode not implemented: ldc.r4 at OVRVrCubeWorldSurfaceViewXNDK.VrCubeWorld+ovrMessage.ovrMessage_GetFloatParm
                return 0;
            }


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

            public void ovrAppThread_Create(JNIEnv env, jobject activityObject)
            {
                env.GetJavaVM(env, out this.JavaVm);

                this.ActivityObject = env.NewGlobalRef(env, activityObject);
                ////appThread.Thread = default(pthread_t);
                //appThread.NativeWindow = null;

                this.MessageQueue.ovrMessageQueue_Create();

                //// ldfda ?
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

            public void ovrAppThread_Destroy(JNIEnv env)
            {
                pthread.pthread_join(this.Thread, null);

                //   (/* typecast */(void(*)(JNIEnv*, jobject))env->DeleteGlobalRef)(env, appThread->ActivityObject);
                env.DeleteGlobalRef(env, this.ActivityObject);
                this.MessageQueue.ovrMessageQueue_Destroy();
            }
        }




        // java, jsc hybrid?
    }

}
