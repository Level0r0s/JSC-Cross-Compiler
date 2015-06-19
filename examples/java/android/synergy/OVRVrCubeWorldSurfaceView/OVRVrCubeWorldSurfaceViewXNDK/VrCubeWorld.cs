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

      

        [Script]
        struct ovrVertexAttribPointer
        {
            public ovrVertexAttribute_location Index;

            public int Size;

            public int Type;

            public bool Normalized;

            public int Stride;
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
            public readonly ovrVertexAttribPointer[] VertexAttribs = new ovrVertexAttribPointer[MAX_VERTEX_ATTRIB_POINTERS];


            // called by ovrScene_Clear
            public void ovrGeometry_Clear()
            {
                // 391

                this.VertexBuffer = 0;
                this.IndexBuffer = 0;
                this.VertexArrayObject = 0;
                this.VertexCount = 0;
                this.IndexCount = 0;
                for (int i = 0; i < MAX_VERTEX_ATTRIB_POINTERS; i++)
                {
                    //this.VertexAttribs[i] = default(ovrVertexAttribPointer);

                    //memset( &geometry->VertexAttribs[i], 0, sizeof( geometry->VertexAttribs[i] ) );
                    this.VertexAttribs[i].Index = (ovrVertexAttribute_location)(-1);
                }
            }

            // called by ovrScene_Create
            public void ovrGeometry_CreateCube()
            {
                // 405

                //var cubeIndices = new ushort[] 
                //{
                //    0, 1, 2, 2, 3, 0,	// top
                //    4, 5, 6, 6, 7, 4,	// bottom
                //    2, 6, 7, 7, 1, 2,	// right
                //    0, 4, 5, 5, 3, 0,	// left
                //    3, 5, 6, 6, 2, 3,	// front
                //    0, 1, 7, 7, 4, 0	// back
                //};

                this.VertexCount = 8;
                this.IndexCount = 36;


                // 438


                this.VertexAttribs[0].Index = ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_POSITION;
                this.VertexAttribs[0].Size = 4;
                this.VertexAttribs[0].Type = gl3.GL_BYTE;
                this.VertexAttribs[0].Normalized = true;
                //this.VertexAttribs[0].Stride = sizeof( cubeVertices.positions[0] );
                //this.VertexAttribs[0].Pointer = (const GLvoid *)offsetof( ovrCubeVertices, positions );

                this.VertexAttribs[1].Index = ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_COLOR;
                this.VertexAttribs[1].Size = 4;
                this.VertexAttribs[1].Type = gl3.GL_UNSIGNED_BYTE;
                this.VertexAttribs[1].Normalized = true;
                //this.VertexAttribs[1].Stride = sizeof( cubeVertices.colors[0] );
                //this.VertexAttribs[1].Pointer = (const GLvoid *)offsetof( ovrCubeVertices, colors );

                gl3.glGenBuffers(1, out this.VertexBuffer);
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, this.VertexBuffer);
                //gl3.glBufferData( gl3.GL_ARRAY_BUFFER, sizeof( cubeVertices ), &cubeVertices, GL_STATIC_DRAW ) );
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, 0);

                gl3.glGenBuffers(1, out this.IndexBuffer);
                gl3.glBindBuffer(gl3.GL_ELEMENT_ARRAY_BUFFER, this.IndexBuffer);
                //gl3.glBufferData( gl3.GL_ELEMENT_ARRAY_BUFFER, sizeof( cubeIndices ), cubeIndices, gl3.GL_STATIC_DRAW ) ;
                gl3.glBindBuffer(gl3.GL_ELEMENT_ARRAY_BUFFER, 0);
            }

            public void ovrGeometry_Destroy()
            {
                // 465

                var IndexBuffer0 = new[] { IndexBuffer };
                gl3.glDeleteBuffers(1, IndexBuffer0);
                var VertexBuffer0 = new[] { VertexBuffer };
                gl3.glDeleteBuffers(1, VertexBuffer0);

                this.ovrGeometry_Clear();
            }
            public void ovrGeometry_CreateVAO()
            {
                // 473
                var VertexArrayObject0 = new[] { this.VertexArrayObject };
                gl3.glGenVertexArrays(1, VertexArrayObject0);
                gl3.glBindVertexArray(this.VertexArrayObject);

                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, this.VertexBuffer);

                for (int i = 0; i < MAX_VERTEX_ATTRIB_POINTERS; i++)
                {
                    if ((int)this.VertexAttribs[i].Index != -1)
                    {
                        gl3.glEnableVertexAttribArray((uint)this.VertexAttribs[i].Index);
                        gl3.glVertexAttribPointer((uint)this.VertexAttribs[i].Index, this.VertexAttribs[i].Size,
                                this.VertexAttribs[i].Type, this.VertexAttribs[i].Normalized,
                                this.VertexAttribs[i].Stride, this.VertexAttribs[i].Pointer);
                    }
                }

                gl3.glBindBuffer(gl3.GL_ELEMENT_ARRAY_BUFFER, this.IndexBuffer);

                gl3.glBindVertexArray(0);
            }

            // called by ovrScene_DestroyVAOs
            public void ovrGeometry_DestroyVAO()
            {
                // 496

                var VertexArrayObject0 = new[] { this.VertexArrayObject };

                gl3.glDeleteVertexArrays(1, VertexArrayObject0);
            }
        }

        enum ovrVertexAttribute_location //: uint
        {
            VERTEX_ATTRIBUTE_LOCATION_POSITION,
            VERTEX_ATTRIBUTE_LOCATION_COLOR,
            VERTEX_ATTRIBUTE_LOCATION_UV,
            VERTEX_ATTRIBUTE_LOCATION_TRANSFORM
        }

        [Script]
        struct ovrVertexAttribute
        {
            public ovrVertexAttribute_location location;
            public string name;
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
        struct ovrUniform
        {
            public ovrUniform_index index;
            public ovrUniform_type type;
            public string name;

            // using static enum?
        }


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


            //// does jsc initialize statically ot at cctor?
            // if its readonly then we could remember the size of it?
            readonly ovrVertexAttribute[] ProgramVertexAttributes = new[]
            {
                new ovrVertexAttribute { location = ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_POSITION,  name = "vertexPosition" },
                new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_COLOR,      name = "vertexColor" },
                new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_UV,      name =         "vertexUv" },
                new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_TRANSFORM,      name =  "vertexTransform" }
            };

            readonly ovrUniform[] ProgramUniforms = new[]
            {
                new ovrUniform { index=ovrUniform_index.UNIFORM_MODEL_MATRIX,         type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ModelMatrix" },
                new ovrUniform { index=ovrUniform_index.UNIFORM_VIEW_MATRIX,          type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ViewMatrix" },
                new ovrUniform { index=ovrUniform_index.UNIFORM_PROJECTION_MATRIX,    type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ProjectionMatrix" }
            };


            // called by ovrScene_Clear
            public void ovrProgram_Clear()
            {
                // 545

                this.Program = 0;
                this.VertexShader = 0;
                this.FragmentShader = 0;
            }

            // called by  ovrScene_Create
            public void ovrProgram_Create(string vertexSource, string fragmentSource)
            {
                // 554

                var r = default(int);

                this.VertexShader = gl3.glCreateShader(gl3.GL_VERTEX_SHADER);


                var vertexSource0 = new[] { vertexSource };
                gl3.glShaderSource(this.VertexShader, 1, vertexSource0, null);

                gl3.glCompileShader(this.VertexShader);
                gl3.glGetShaderiv(this.VertexShader, gl3.GL_COMPILE_STATUS, out r);
                //if ( r == gl3.GL_FALSE )
                //{
                //    GLchar msg[4096];
                //    GL( glGetShaderInfoLog( program->VertexShader, sizeof( msg ), 0, msg ) );
                //    ALOGE( "%s\n%s\n", vertexSource, msg );
                //    return false;
                //}

                this.FragmentShader = gl3.glCreateShader(gl3.GL_FRAGMENT_SHADER);
                var fragmentSource0 = new[] { fragmentSource };
                gl3.glShaderSource(this.FragmentShader, 1, fragmentSource0, null);
                gl3.glCompileShader(this.FragmentShader);
                gl3.glGetShaderiv(this.FragmentShader, gl3.GL_COMPILE_STATUS, out r);
                //if ( r == GL_FALSE )
                //{
                //    GLchar msg[4096];
                //    GL( glGetShaderInfoLog( program->FragmentShader, sizeof( msg ), 0, msg ) );
                //    ALOGE( "%s\n%s\n", fragmentSource, msg );
                //    return false;
                //}

                this.Program = gl3.glCreateProgram();

                gl3.glAttachShader(this.Program, this.VertexShader);
                gl3.glAttachShader(this.Program, this.FragmentShader);

                // Bind the vertex attribute locations.
                for (int i = 0; i < ProgramVertexAttributes.Length; i++)
                {
                    gl3.glBindAttribLocation((uint)this.Program, (uint)ProgramVertexAttributes[i].location, ProgramVertexAttributes[i].name);
                }

                gl3.glLinkProgram(this.Program);
                gl3.glGetProgramiv(this.Program, gl3.GL_LINK_STATUS, out r);
                //if ( r == GL_FALSE )
                //{
                //    GLchar msg[4096];
                //    GL( glGetProgramInfoLog( program->Program, sizeof( msg ), 0, msg ) );
                //    ALOGE( "Linking program failed: %s\n", msg );
                //    return false;
                //}

                // Get the uniform locations.
                //memset( program->Uniforms, -1, sizeof( program->Uniforms ) );
                for (int i = 0; i < ProgramUniforms.Length; i++)
                {
                    this.Uniforms[(int)ProgramUniforms[i].index] = gl3.glGetUniformLocation(this.Program, ProgramUniforms[i].name);
                }

                //gl3.glUseProgram(this.Program);

                //// Get the texture locations.
                //for ( int i = 0; i < MAX_PROGRAM_TEXTURES; i++ )
                //{
                //    fixed char name[32] = {0};

                //    sprintf( name, "Texture%i", i );
                //    this.Textures[i] = gl3.glGetUniformLocation( this.Program, name );
                //    if ( this.Textures[i] != -1 )
                //    {
                //        gl3.glUniform1i( this.Textures[i], i  );
                //    }
                //}

                //gl3.glUseProgram(0);

            }

            // called by ovrScene_Destroy
            public void ovrProgram_Destroy()
            {
                // 628
                if (this.Program != 0)
                {
                    gl3.glDeleteProgram(this.Program);
                    this.Program = 0;
                }
                if (this.VertexShader != 0)
                {
                    gl3.glDeleteShader(this.VertexShader);
                    this.VertexShader = 0;
                }
                if (this.FragmentShader != 0)
                {
                    gl3.glDeleteShader(this.FragmentShader);
                    this.FragmentShader = 0;
                }
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



        // cant make it class yet?
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
            public
            void ovrApp_Clear()
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

                this.Egl = new ovrEgl();
                //this.Egl.ovrEgl_Clear();

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
