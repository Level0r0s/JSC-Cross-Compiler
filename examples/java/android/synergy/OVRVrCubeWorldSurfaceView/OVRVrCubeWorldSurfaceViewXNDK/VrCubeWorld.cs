//using ANativeWindow = System.Object;

using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLibNative.SystemHeaders.android;
using ScriptCoreLibNative.SystemHeaders.EGL;
using ScriptCoreLibNative.SystemHeaders.GLES3;

[assembly: Script()]
[assembly: ScriptTypeFilter(ScriptType.C, typeof(OVRVrCubeWorldSurfaceViewXNDK.VrCubeWorld))]
[assembly: ScriptTypeFilter(ScriptType.C, typeof(Java.com.oculus.gles3jni.GLES3JNILib))]


namespace OVRVrCubeWorldSurfaceViewXNDK
{
    using GLuint = UInt32;

    using EGLDisplay = Object;
    using EGLConfig = Object;
    //using EGLSurface = Object;
    using EGLContext = Object;

    //using GLuint = Object;
    using GLint = Int32;
    //using GLenum = Object;
    using GLboolean = Object;
    using GLsizei = Object;

    using EGLint = Int32;
    //enum EGLint { }
    //enum GLint { }





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

            // called by ovrApp_HandleVrModeChanges
            public void ovrEgl_CreateSurface(native_window.ANativeWindow w) { }
            public void ovrEgl_DestroySurface() { }
        }


        [Script]
        class ovrVertexAttribPointer
        {
            public GLuint Index;
            public GLint Size;

            public uint Type;

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

            // sent to glBindVertexArray
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


            // called by ovrScene_Clear
            public void ovrGeometry_Clear() { }

            // called by ovrScene_Create
            public void ovrGeometry_CreateCube() { }
            public void ovrGeometry_Destroy() { }
            public void ovrGeometry_CreateVAO() { }

            // called by ovrScene_DestroyVAOs
            public void ovrGeometry_DestroyVAO() { }
        }

        enum ovrVertexAttribute_location : uint
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



        public const int MAX_PROGRAM_UNIFORMS = 8;
        public const int MAX_PROGRAM_TEXTURES = 8;

        // a field at ovrScene, keep it as class as fixed causes uglyness
        [Script]
        //struct ovrProgram
        class ovrProgram
        {
            // sent to glUseProgram
            public GLuint Program;

            public GLuint VertexShader;
            public GLuint FragmentShader;
            // These will be -1 if not used by the program.
            //public fixed GLint Uniforms[MAX_PROGRAM_UNIFORMS];       // ProgramUniforms[].name
            //public fixed int[] Uniforms[MAX_PROGRAM_UNIFORMS];      // ProgramUniforms[].name
            //public fixed int[] Textures[MAX_PROGRAM_TEXTURES];      // Texture%i

            public readonly int[] Uniforms = new int[MAX_PROGRAM_UNIFORMS];      // ProgramUniforms[].name
            public readonly int[] Textures = new int[MAX_PROGRAM_TEXTURES];      // Texture%i

            //Error CS1663  Fixed size buffer type must be one of the following: bool, byte, short, int, long, char, sbyte, ushort, uint, ulong, float or double OVRVrCubeWorldSurfaceViewXNDK   X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs	130
            //Error CS1642  Fixed size buffer fields may only be members of structs OVRVrCubeWorldSurfaceViewXNDK X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs	129

            // called by ovrScene_Clear
            public void ovrProgram_Clear() { }

            // called by  ovrScene_Create
            public void ovrProgram_Create(string vert, string frag) { }
            public void ovrProgram_Destroy() { }

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





        public const int NUM_INSTANCES = 1500;


        // assetslibrary
        public const string VERTEX_SHADER = @"
";
        public const string FRAGMENT_SHADER = @"
";


        // member of ovrApp
        // member of ovrRenderThread
        [Script]
        unsafe class ovrScene
        {
            public bool CreatedScene;
            public bool CreatedVAOs;

            public ovrProgram Program;

            public ovrGeometry Cube;

            // deleted by ovrScene_Destroy
            public GLuint InstanceTransformBuffer;

            public readonly ovrVector3f[] CubePositions = new ovrVector3f[NUM_INSTANCES];
            public readonly ovrVector3f[] CubeRotations = new ovrVector3f[NUM_INSTANCES];

            public ovrScene()
            {
                ovrScene_Clear();
            }

            // called by ovrApp_Clear
            void ovrScene_Clear()
            {
                // 817
                this.CreatedScene = false;
                this.CreatedVAOs = false;
                this.InstanceTransformBuffer = 0;

                this.Program.ovrProgram_Clear();
                this.Cube.ovrGeometry_Clear();
            }

            public bool ovrScene_IsCreated()
            {
                return this.CreatedScene;
            }
            public void ovrScene_CreateVAOs()
            {
                // 832

                if (this.CreatedVAOs)
                    return;

                this.Cube.ovrGeometry_CreateVAO();

                // Modify the VAO to use the instance transform attributes.
                gl3.glBindVertexArray(this.Cube.VertexArrayObject);
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, this.InstanceTransformBuffer);

                //for (uint i = 0; i < 4; i++)
                for (int i = 0; i < 4; i++)
                {
                    gl3.glEnableVertexAttribArray((uint)ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_TRANSFORM + (uint)i);
                    gl3.glVertexAttribPointer((uint)ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_TRANSFORM + (uint)i,
                        4, gl3.GL_FLOAT,
                        false,
                        4 * 4 * sizeof(float),
                        // offset?
                        (void*)(i * 4 * sizeof(float)));
                    gl3.glVertexAttribDivisor((uint)ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_TRANSFORM + (uint)i, 1);
                }

                gl3.glBindVertexArray(0);

                this.CreatedVAOs = true;
            }

            // called by ovrScene_Destroy
            public void ovrScene_DestroyVAOs()
            {
                if (this.CreatedVAOs)
                {
                    this.Cube.ovrGeometry_DestroyVAO();

                    this.CreatedVAOs = false;
                }
            }

            // called by AppThreadFunction
            public void ovrScene_Create()
            {
                // 864

                this.Program.ovrProgram_Create(VERTEX_SHADER, FRAGMENT_SHADER);
                this.Cube.ovrGeometry_CreateCube();

                // Create the instance transform attribute buffer.
                gl3.glGenBuffers(1, out this.InstanceTransformBuffer);
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, this.InstanceTransformBuffer);
                gl3.glBufferData(gl3.GL_ARRAY_BUFFER, NUM_INSTANCES * 4 * 4 * sizeof(float), null, gl3.GL_DYNAMIC_DRAW);
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, 0);

                // Setup random cube positions and rotations.
                for (int i = 0; i < NUM_INSTANCES; i++)
                {
                    // Using volatile keeps the compiler from optimizing away multiple calls to drand48().
                    //volatile float rx; ry, rz;
                    float rx = 0, ry = 0, rz = 0;

                    //for (; ; )

                    var notfound = true;
                    while (notfound)
                    {
                        rx = (float)(stdlib_h.drand48() - 0.5f) * (50.0f + (float)Math.Sqrt(NUM_INSTANCES));
                        ry = (float)(stdlib_h.drand48() - 0.5f) * (50.0f + (float)Math.Sqrt(NUM_INSTANCES));
                        rz = (float)(stdlib_h.drand48() - 0.5f) * (1500.0f + (float)Math.Sqrt(NUM_INSTANCES));


                        // If too close to 0,0,0
                        var too_closex = Math.Abs(rx) < 4.0f;
                        var too_closey = Math.Abs(ry) < 4.0f;
                        var too_closez = Math.Abs(rz) < 4.0f;

                        if (!too_closex)
                            if (!too_closey)
                                if (!too_closez)
                                {
                                    // Test for overlap with any of the existing cubes.
                                    bool overlap = false;
                                    for (int j = 0; j < i; j++)
                                    {
                                        if (Math.Abs(rx - this.CubePositions[j].x) < 4.0f)
                                            if (Math.Abs(ry - this.CubePositions[j].y) < 4.0f)
                                                if (Math.Abs(rz - this.CubePositions[j].z) < 4.0f)
                                                {
                                                    overlap = true;
                                                    break;
                                                }
                                    }
                                    if (!overlap)
                                    {
                                        //break;
                                        notfound = false;
                                    }
                                }
                    }

                    // Insert into list sorted based on distance.
                    int insert = 0;
                    float distSqr = rx * rx + ry * ry + rz * rz;
                    for (int j = i; j > 0; j--)
                    {
                        var otherDistSqr = default(float);

                        // fixed/break does a try/finally to zero out the pointer
                        fixed (ovrVector3f* otherPos = &this.CubePositions[j - 1])
                            otherDistSqr = otherPos->x * otherPos->x + otherPos->y * otherPos->y + otherPos->z * otherPos->z;

                        if (distSqr > otherDistSqr)
                        {
                            insert = j;
                            break;
                        }


                        this.CubePositions[j] = this.CubePositions[j - 1];
                        this.CubeRotations[j] = this.CubeRotations[j - 1];
                    }

                    this.CubePositions[insert].x = rx;
                    this.CubePositions[insert].y = ry;
                    this.CubePositions[insert].z = rz;

                    this.CubeRotations[insert].x = (float)stdlib_h.drand48();
                    this.CubeRotations[insert].y = (float)stdlib_h.drand48();
                    this.CubeRotations[insert].z = (float)stdlib_h.drand48();
                }


                this.CreatedScene = true;

                this.ovrScene_CreateVAOs();
            }

            // called by AppThreadFunction
            public void ovrScene_Destroy()
            {
                // 940
                this.ovrScene_DestroyVAOs();

                this.Program.ovrProgram_Destroy();
                this.Cube.ovrGeometry_Destroy();

                gl3.glDeleteBuffers(1, ref this.InstanceTransformBuffer);
                this.CreatedScene = false;

            }

        }



        [Script]
        struct ovrSimulation
        {
            public ovrVector3f CurrentRotation;

            public void ovrSimulation_Clear()
            {
                // 965
                this.CurrentRotation.x = 0.0f;
                this.CurrentRotation.y = 0.0f;
                this.CurrentRotation.z = 0.0f;
            }

            // called by AppThreadFunction, would we benefit if jsc marked no branch methods as inline?
            public void ovrSimulation_AdvanceSimulation(double predictedDisplayTime)
            {
                // 972
                // Update rotation.
                this.CurrentRotation.x = (float)(predictedDisplayTime);
                this.CurrentRotation.y = (float)(predictedDisplayTime);
                this.CurrentRotation.z = (float)(predictedDisplayTime);
            }
        }





        enum ovrRenderType
        {
            RENDER_FRAME,
            RENDER_LOADING_ICON,
            RENDER_BLACK_FLUSH,
            RENDER_BLACK_FINAL
        }
       ;

#if MULTI_THREADED
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

            public void ovrRenderThread_Clear() { }
            public void ovrRenderThread_Create() { }
            public void ovrRenderThread_Destroy() { }
            public void ovrRenderThread_Submit() { }
            public void ovrRenderThread_Wait() { }
            public void ovrRenderThread_GetTid() { }
        }

        static void RenderThreadFunction() { }

#endif


        enum ovrBackButtonState
        {
            BACK_BUTTON_STATE_NONE,
            BACK_BUTTON_STATE_PENDING_DOUBLE_TAP,
            BACK_BUTTON_STATE_PENDING_SHORT_PRESS,
            BACK_BUTTON_STATE_SKIP_UP
        }
       ;



        // stackalloc at X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.AppThreadFunction.cs

        //[Script]
        //class ovrAppRef
        //{
        //    public ovrApp fields;

        //    public ovrAppRef()
        //    {
        //        this.fields.ovrApp_Clear();
        //    }
        //}

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
            // set by?
            public ovrRenderer Renderer;
#endif

            //public ovrApp()
            //{
            //    ovrApp_Clear();
            //}

            // ctor
            // called by AppThreadFunction
            public void ovrApp_Clear()
            {
                // 1408

                this.Java.Vm = default(JavaVM);
                this.Java.Env = default(JNIEnv);
                this.Java.ActivityObject = null;
                this.NativeWindow = null;
                this.Resumed = false;
                this.Ovr = default(ovrMobile);
                this.FrameIndex = 1;
                this.MinimumVsyncs = 1;
                this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_NONE;
                this.BackButtonDown = false;
                this.BackButtonDownStartTime = 0.0;

                this.Egl.ovrEgl_Clear();

                this.Scene = new ovrScene();
                //this.Scene.ovrScene_Clear();

                this.Simulation.ovrSimulation_Clear();
#if MULTI_THREADED
	ovrRenderThread_Clear( &app->RenderThread );
#else
                this.Renderer = new ovrRenderer();
                //this.Renderer.ovrRenderer_Clear();
#endif
            }

            public void ovrApp_HandleVrModeChanges()
            {
                // 1432
                if (this.NativeWindow != null && this.Egl.MainSurface == egl.EGL_NO_SURFACE)
                {
                    this.Egl.ovrEgl_CreateSurface(this.NativeWindow);
                }

                if (this.Resumed != false && this.NativeWindow != null)
                {
                    if (this.Ovr == null)
                    {
                        var parms = VrApi_Helpers.vrapi_DefaultModeParms(ref Java);
                        parms.CpuLevel = 2;
                        parms.GpuLevel = 3;
                        parms.MainThreadTid = unistd.gettid();
#if MULTI_THREADED
			// Also set the renderer thread to SCHED_FIFO.
			parms.RenderThreadTid = ovrRenderThread_GetTid( &app->RenderThread );
#endif

                        //ALOGV("        eglGetCurrentSurface( EGL_DRAW ) = %p", eglGetCurrentSurface(EGL_DRAW));

                        this.Ovr = VrApi.vrapi_EnterVrMode(ref parms);

                        //ALOGV("        vrapi_EnterVrMode()");
                        //ALOGV("        eglGetCurrentSurface( EGL_DRAW ) = %p", eglGetCurrentSurface(EGL_DRAW));
                    }
                }
                else
                {
                    if (this.Ovr != null)
                    {
#if MULTI_THREADED
			// Make sure the renderer thread is no longer using the ovrMobile.
			ovrRenderThread_Wait( &app->RenderThread );
#endif
                        //ALOGV("        eglGetCurrentSurface( EGL_DRAW ) = %p", eglGetCurrentSurface(EGL_DRAW));

                        VrApi.vrapi_LeaveVrMode(this.Ovr);
                        this.Ovr = null;

                        //ALOGV("        vrapi_LeaveVrMode()");
                        //ALOGV("        eglGetCurrentSurface( EGL_DRAW ) = %p", eglGetCurrentSurface(EGL_DRAW));
                    }
                }

                if (this.NativeWindow == null && this.Egl.MainSurface != egl.EGL_NO_SURFACE)
                {
                    this.Egl.ovrEgl_DestroySurface();
                }
            }

            public void ovrApp_BackButtonAction()
            {
                // 1484

                if (this.BackButtonState == ovrBackButtonState.BACK_BUTTON_STATE_PENDING_DOUBLE_TAP)
                {
                    //ALOGV("back button double tap");
                    this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_SKIP_UP;
                }
                else if (this.BackButtonState == ovrBackButtonState.BACK_BUTTON_STATE_PENDING_SHORT_PRESS && !this.BackButtonDown)
                {
                    if ((VrApi.vrapi_GetTimeInSeconds() - this.BackButtonDownStartTime) > VrApi_Android.BACK_BUTTON_DOUBLE_TAP_TIME_IN_SECONDS)
                    {
                        //ALOGV("back button short press");
                        //ALOGV("        ovr_StartSystemActivity( %s )", PUI_CONFIRM_QUIT);
                        VrApi_Android.ovr_StartSystemActivity(ref Java, VrApi.PUI_CONFIRM_QUIT, default(string));
                        this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_NONE;
                    }
                }
                else if (this.BackButtonState == ovrBackButtonState.BACK_BUTTON_STATE_NONE && this.BackButtonDown)
                {
                    if ((VrApi.vrapi_GetTimeInSeconds() - this.BackButtonDownStartTime) > VrApi_Android.BACK_BUTTON_LONG_PRESS_TIME_IN_SECONDS)
                    {
                        //ALOGV("back button long press");
                        //ALOGV("        ovr_StartSystemActivity( %s )", PUI_GLOBAL_MENU);
                        VrApi_Android.ovr_StartSystemActivity(ref Java, VrApi.PUI_GLOBAL_MENU, null);
                        this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_SKIP_UP;
                    }
                }
            }

            // java UI sends over to native, which the uses MQ to send over to bg thread? SharedMemory would be nice?
            // onKeyEvent
            public void ovrApp_HandleKeyEvent(keycodes.AKEYCODE keyCode, input.AInputEventAction action)
            {
                // 1513
                // cannot do this aliasing?
                //var app = this;

                // Handle GearVR back button.
                if (keyCode == keycodes.AKEYCODE.AKEYCODE_BACK)
                {
                    if (action == input.AInputEventAction.AKEY_EVENT_ACTION_DOWN)
                    {
                        if (!this.BackButtonDown)
                        {
                            if ((VrApi.vrapi_GetTimeInSeconds() - this.BackButtonDownStartTime) < VrApi_Android.BACK_BUTTON_DOUBLE_TAP_TIME_IN_SECONDS)
                            {
                                this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_PENDING_DOUBLE_TAP;
                            }
                            this.BackButtonDownStartTime = VrApi.vrapi_GetTimeInSeconds();
                        }
                        this.BackButtonDown = true;
                    }
                    else if (action == input.AInputEventAction.AKEY_EVENT_ACTION_UP)
                    {
                        if (this.BackButtonState == ovrBackButtonState.BACK_BUTTON_STATE_NONE)
                        {
                            if ((VrApi.vrapi_GetTimeInSeconds() - this.BackButtonDownStartTime) < VrApi_Android.BACK_BUTTON_SHORT_PRESS_TIME_IN_SECONDS)
                            {
                                this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_PENDING_SHORT_PRESS;
                            }
                        }
                        else if (this.BackButtonState == ovrBackButtonState.BACK_BUTTON_STATE_SKIP_UP)
                        {
                            this.BackButtonState = ovrBackButtonState.BACK_BUTTON_STATE_NONE;
                        }
                        this.BackButtonDown = false;
                    }
                    //return 1;
                }
                //return 0;
            }

            // onTouchEvent
            public void ovrApp_HandleTouchEvent(int action, float x, float y)
            {
                // ??? not used

            }

            // sent by?
            public void ovrApp_HandleSystemEvents()
            {
                // 1568
                var MAX_EVENT_SIZE = 4096u;

                var eventBuffer = new byte[MAX_EVENT_SIZE];

                for (var status = VrApi_Android.ovr_GetNextPendingEvent(eventBuffer, MAX_EVENT_SIZE); status >= eVrApiEventStatus.VRAPI_EVENT_PENDING; status = VrApi_Android.ovr_GetNextPendingEvent(eventBuffer, MAX_EVENT_SIZE))
                {
                    if (status != eVrApiEventStatus.VRAPI_EVENT_PENDING)
                    {
                        if (status != eVrApiEventStatus.VRAPI_EVENT_CONSUMED)
                        {
                            //ALOGE("Error %i handing System Activities Event", status);
                        }
                        continue;
                    }
                }

            }

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
