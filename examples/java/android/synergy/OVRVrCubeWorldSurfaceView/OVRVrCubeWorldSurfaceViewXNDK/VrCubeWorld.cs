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
        class ovrUniform
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
            readonly ovrVertexAttribute[] ProgramVertexAttributes =
               new[]
                {
                    new ovrVertexAttribute { location = ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_POSITION,  name = "vertexPosition" },
                    new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_COLOR,      name = "vertexColor" },
                    new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_UV,      name =         "vertexUv" },
                    new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_TRANSFORM,      name =  "vertexTransform" }

                };

            //readonly ovrUniform[] ProgramUniforms =
            //    new[]
            //        {
            //new ovrUniform { index=ovrUniform_index.UNIFORM_MODEL_MATRIX,         type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ModelMatrix" },
            //new ovrUniform { index=ovrUniform_index.UNIFORM_VIEW_MATRIX,          type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ViewMatrix" },
            //new ovrUniform { index=ovrUniform_index.UNIFORM_PROJECTION_MATRIX,    type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ProjectionMatrix" }

            //        };


            // called by ovrScene_Clear
            public void ovrProgram_Clear() { }

            // called by  ovrScene_Create
            public void ovrProgram_Create(string vertexSource, string fragmentSource)
            {
                // 554

                var r = default(int);

                this.VertexShader = gl3.glCreateShader(gl3.GL_VERTEX_SHADER);

                gl3.glShaderSource(this.VertexShader, 1, ref vertexSource, null);

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
                gl3.glShaderSource(this.FragmentShader, 1, ref fragmentSource, null);
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
                //for (int i = 0; i < ProgramUniforms.Length; i++)
                //{
                //    this.Uniforms[(int)ProgramUniforms[i].index] = gl3.glGetUniformLocation(this.Program, ProgramUniforms[i].name);
                //}

                gl3.glUseProgram(this.Program);

                // Get the texture locations.
                //for ( int i = 0; i < MAX_PROGRAM_TEXTURES; i++ )
                //{
                //    char name[32];
                //    sprintf( name, "Texture%i", i );
                //    program->Textures[i] = glGetUniformLocation( program->Program, name );
                //    if ( program->Textures[i] != -1 )
                //    {
                //        GL( glUniform1i( program->Textures[i], i ) );
                //    }
                //}

                gl3.glUseProgram(0);

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
