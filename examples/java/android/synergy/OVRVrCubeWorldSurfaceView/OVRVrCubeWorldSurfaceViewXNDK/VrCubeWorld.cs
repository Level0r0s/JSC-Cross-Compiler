using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Script()]
[assembly: ScriptTypeFilter(ScriptType.C, typeof(OVRVrCubeWorldSurfaceViewXNDK.VrCubeWorld))]

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

    using JavaVM = Object;
    using jobject = Object;
    using pthread_t = Object;
    using pthread_cond_t = Object;
    using pthread_mutex_t = Object;
    using ovrMobile = Object;
    using ovrTracking = Object;

    using ovrMQWait = Object;
    using ovrJava = Object;
    using ANativeWindow = Int32;

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

        // does jsc initialize statically ot at cctor?
        static ovrVertexAttribute[] ProgramVertexAttributes =
            new[]
            {
                new ovrVertexAttribute { location = ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_POSITION,  name = "vertexPosition" },
                new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_COLOR,      name = "vertexColor" },
                new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_UV,      name =         "vertexUv" },
                new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_TRANSFORM,      name =  "vertexTransform" }

            };

        static void ovrGeometry_Clear(this ovrGeometry that) { }
        static void ovrGeometry_CreateCube(this ovrGeometry that) { }
        static void ovrGeometry_Destroy(this ovrGeometry that) { }
        static void ovrGeometry_CreateVAO(this ovrGeometry that) { }
        static void ovrGeometry_DestroyVAO(this ovrGeometry that) { }


        public const int MAX_PROGRAM_UNIFORMS = 8;
        public const int MAX_PROGRAM_TEXTURES = 8;

        [Script]
        struct ovrProgram
        {
            public GLuint Program;
            public GLuint VertexShader;
            public GLuint FragmentShader;
            // These will be -1 if not used by the program.
            public fixed GLint Uniforms[MAX_PROGRAM_UNIFORMS];       // ProgramUniforms[].name
            public fixed GLint Textures[MAX_PROGRAM_TEXTURES];      // Texture%i

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

        static ovrUniform[] ProgramUniforms =
            new[]
            {
    new ovrUniform { index=ovrUniform_index.UNIFORM_MODEL_MATRIX,         type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ModelMatrix" },
    new ovrUniform { index=ovrUniform_index.UNIFORM_VIEW_MATRIX,          type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ViewMatrix" },
    new ovrUniform { index=ovrUniform_index.UNIFORM_PROJECTION_MATRIX,    type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ProjectionMatrix" }

            };

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
            public ANativeWindow* NativeWindow;
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

        [Script]
        class ovrMessage {
            public int Id;
            public ovrMQWait Wait;
            public long[] Parms;
        }

        static void ovrMessage_Init(this ovrMessage that)
        { }

        static void ovrMessage_SetPointerParm(this ovrMessage that) { }
        static void ovrMessage_GetPointerParm(this ovrMessage that) { }
        static void ovrMessage_SetIntegerParm(this ovrMessage that) { }
        static void ovrMessage_GetIntegerParm(this ovrMessage that) { }
        static void ovrMessage_SetFloatParm(this ovrMessage that) { }
        static void ovrMessage_GetFloatParm(this ovrMessage that) { }

        [Script]
        class ovrMessageQueue {
            public ovrMessage[] Messages;
            public volatile int Head;  // dequeue at the head
            public volatile int Tail;  // enqueue at the tail
            public volatile bool Enabled;
            public ovrMQWait Wait;
            public pthread_mutex_t Mutex;
            public pthread_cond_t Posted;
            public pthread_cond_t Received;
            public pthread_cond_t Processed;
        }

        static void ovrMessageQueue_Create(this ovrMessageQueue that) { }
        static void ovrMessageQueue_Destroy(this ovrMessageQueue that) { }
        static void ovrMessageQueue_Enable(this ovrMessageQueue that) { }
        static void ovrMessageQueue_PostMessage(this ovrMessageQueue that) { }
        static void ovrMessageQueue_SleepUntilMessage(this ovrMessageQueue that) { }
        static void ovrMessageQueue_GetNextMessage(this ovrMessageQueue that) { }

        [Script]
        class ovrAppThread {
            public JavaVM JavaVm;
            public jobject ActivityObject;
            public pthread_t Thread;
            public ovrMessageQueue MessageQueue;
            public ANativeWindow* NativeWindow;
        }

        static void AppThreadFunction() { }
        static void ovrAppThread_Create(this ovrMessageQueue that) { }
        static void ovrAppThread_Destroy(this ovrMessageQueue that) { }

        // java, jsc hybrid?

        static void Java_com_oculus_gles3jni_GLES3JNILib_onCreate() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onStart() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onResume() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onPause() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onStop() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onDestroy() { }

        static void Java_com_oculus_gles3jni_GLES3JNILib_onSurfaceCreated() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onSurfaceChanged() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onSurfaceDestroyed() { }

        static void Java_com_oculus_gles3jni_GLES3JNILib_onKeyEvent() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onTouchEvent() { }
    }
}
