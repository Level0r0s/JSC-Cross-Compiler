using com.oculus.gles3jni;
using ScriptCoreLib;
using ScriptCoreLibAndroidNDK.Library;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using ScriptCoreLibNative.SystemHeaders.GLES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVRWindWheelNDK
{
    public static unsafe partial class VrCubeWorld
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewNDK\staging\jni\VrCubeWorld_SurfaceView.c

        // 779
        //public const int NUM_INSTANCES = 1500;
        //public const int NUM_INSTANCES = 12;
        //public const int NUM_INSTANCES = 6;


        //public const int floors = 5;
        //public const int floors = 33;
        //public const int floors = 333;
        //public const int floors = 16;
        //public const int floors = 3;


        // can we have two sets of floors?
        public const int floors = 9;

        public const int floorwidth = 9;

        //public const int floorwidth = 19;

        //public const int NUM_INSTANCES = 8 * 8 * floors;
        public const int NUM_INSTANCES = floorwidth * floorwidth * floors;


        // one cube at a time?
        public const int NUM_INSTANCES1 = 1;

        // I/xNativeActivity(12533): \VrCubeWorld.AppThread.cs:330 vrapi_SubmitFrame  6241

        //I/xNativeActivity(11266): \VrCubeWorld.AppThread.cs:330 vrapi_SubmitFrame  241
        //I/xNativeActivity(11266): \VrCubeWorld.AppThread.cs:74 mallinfo            total allocated space:  114192224
        //I/xNativeActivity(11266): \VrCubeWorld.AppThread.cs:81 sanity check, are we leaking memory? 0


        // member of ovrApp
        // member of ovrRenderThread
        // created by ovrApp_Clear
        public unsafe class ovrScene
        {
            public ovrApp App;

            // used by? original
            public readonly ovrVector3f[] CubePositions0 = new ovrVector3f[NUM_INSTANCES];



            // ovrRenderer_RenderFrame
            // how are they sent to gpu?
            // used by CreateTranslation
            public readonly ovrVector3f[] CubePositions = new ovrVector3f[NUM_INSTANCES];
            public readonly ovrVector3f[] CubeRotations = new ovrVector3f[NUM_INSTANCES];
            // can each cube have its own color?


            public readonly ovrProgram Program = new ovrProgram();
            public readonly ovrGeometry Cube = new ovrGeometry();

            // set after ovrScene_Create
            // 815
            public bool CreatedScene = false;
            public bool CreatedVAOs = false;


            // used by glMapBufferRange
            // set by glGenBuffers
            // deleted by ovrScene_Destroy
            // GL_ARRAY_BUFFER
            public uint InstanceTransformBuffer0 = 0;

            // for the other boxes..
            public uint InstanceTransformBuffer1 = 0;

            // VRAPI_FRAME_INIT_LOADING_ICON_FLUSH
            //public bool ovrScene_IsCreated()
            //{
            //    return this.CreatedScene;
            //}

            // called by ovrScene_Create
            public void ovrScene_CreateVAOs()
            {
                // 832
                ConsoleExtensions.trace("enter ovrScene_CreateVAOs, call ovrGeometry_CreateVAO");

                if (this.CreatedVAOs)
                    return;

                this.Cube.ovrGeometry_CreateVAO();

                // Modify the VAO to use the instance transform attributes.
                gl3.glBindVertexArray(this.Cube.VertexArrayObject0);
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, this.InstanceTransformBuffer0);

                // 4 what?
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
                ConsoleExtensions.trace("exit ovrScene_CreateVAOs");
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
            // called after VRAPI_FRAME_INIT_LOADING_ICON_FLUSH
            public void ovrScene_Create()
            {
                // 864
                ConsoleExtensions.trace("enter ovrScene_Create, invoke ovrProgram_Create, ovrGeometry_CreateCube");


                var vert = new Shaders.VrCubeWorldVertexShader();
                var frag = new Shaders.VrCubeWorldFragmentShader();

                this.Program.ovrProgram_Create(
                    // jsc should keep typeinfo/virtal function pointer table tagged on objects?
                    vert.ToString(),
                    frag.ToString()
                );

                this.Cube.ovrGeometry_CreateCube();

                // Create the instance transform attribute buffer.
                gl3.glGenBuffers(1, out this.InstanceTransformBuffer0);
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, this.InstanceTransformBuffer0);
                gl3.glBufferData(gl3.GL_ARRAY_BUFFER, NUM_INSTANCES * 4 * 4 * sizeof(float), null, gl3.GL_DYNAMIC_DRAW);
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, 0);

                gl3.glGenBuffers(1, out this.InstanceTransformBuffer1);
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, this.InstanceTransformBuffer1);
                gl3.glBufferData(gl3.GL_ARRAY_BUFFER, NUM_INSTANCES1 * 4 * 4 * sizeof(float), null, gl3.GL_DYNAMIC_DRAW);
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, 0);


                #region init data
                // Setup random cube positions and rotations.
                for (int i = 0; i < NUM_INSTANCES; i++)
                {
                    // Using volatile keeps the compiler from optimizing away multiple calls to drand48().
                    //volatile float rx; ry, rz;
                    float rx = 0, ry = 0, rz = 0;


                    rx = 2.0f * (((i / floors) / floorwidth) - floors);

                    rz = 2.0f * (((i / floors) % floorwidth) - floors);


                    ry = (i % floors - 1.0f) * 0.6f;

                    //// can we offset the thing?
                    //rx = (float)(stdlib_h.drand48() - 0.5f) * (5.0f + (float)Math.Sqrt(NUM_INSTANCES));

                    //// ah its only done once?
                    ////rx += com.oculus.gles3jni.GLES3JNILib.fields_xvalue;
                    //ry = (float)(stdlib_h.drand48() - 0.5f) * (5.0f + (float)Math.Sqrt(NUM_INSTANCES));

                    //// cool now we have a set of cubes in front of us.
                    ////rz = (float)(stdlib_h.drand48() - 1.5f) * (15.0f + (float)Math.Sqrt(NUM_INSTANCES));
                    //rz = (float)(stdlib_h.drand48() - 1.5f) * (10.0f + (float)Math.Sqrt(NUM_INSTANCES));

                    //rz = (float)(stdlib_h.drand48() - 0.5f) * (15.0f + (float)Math.Sqrt(NUM_INSTANCES));
                    // how do we record our changes?
                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150629/mod




                    // keep the original around
                    this.CubePositions0[i].x = rx;
                    this.CubePositions0[i].y = ry;
                    this.CubePositions0[i].z = rz;

                    // for the offset
                    this.CubePositions[i].x = rx;
                    this.CubePositions[i].y = ry;
                    this.CubePositions[i].z = rz;

                    //ConsoleExtensions.tracei("ovrScene_Create CubePositions i: ", insert);

                    //this.CubeRotations[i].x = (float)stdlib_h.drand48();
                    //this.CubeRotations[i].y = (float)stdlib_h.drand48();
                    //this.CubeRotations[i].z = (float)stdlib_h.drand48();
                }
                #endregion


                this.CreatedScene = true;

                this.ovrScene_CreateVAOs();


                // elapsed?
                ConsoleExtensions.tracei("exit ovrScene_Create NUM_INSTANCES: ", NUM_INSTANCES);
            }

            // called by AppThreadFunction
            public void ovrScene_Destroy()
            {
                // 940
                this.ovrScene_DestroyVAOs();

                this.Program.ovrProgram_Destroy();
                this.Cube.ovrGeometry_Destroy();

                gl3.glDeleteBuffers(1, ref InstanceTransformBuffer0);
                this.CreatedScene = false;

            }





            public static float wasd_x0;
            public static float wasd_y0;
            public static float wasd_z0;



            static float wasd_x;
            static float wasd_y;
            static float wasd_z;



            static int old_mousey;
            static bool old_mousey_defined;

            // called by stringFromJNI
            // HUDp30 thread!
            public void Update()
            {
                // should move to global Translation?

                // UI thread writes, VR thread reads..


                // what about mouse wheel?
                // var flatlandMouseForwardFrameSpeed = .1f;
                var flatlandMouseForwardFrameSpeed = .005f;

                var flatlandFrameSpeed = .1f;
                var flatlandFrameSpeedStrafe = .04f;

                var y = 0.1f;
                //var y = 0f;

                // C crouch or jump?
                if (GLES3JNILib.fields_c == 67)
                {
                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704

                    // how low can we go?
                    // if we change only one opcode, can we send out a patch via udp?
                    //y = -20f;
                    //y = +2f;
                    y += 0.3f;
                    // as a workaround right now we press run.

                    flatlandMouseForwardFrameSpeed /= 3;
                    flatlandFrameSpeed /= 3;
                    flatlandFrameSpeedStrafe /= 3;
                }

                if (old_mousey_defined)
                {
                    var dyA = old_mousey - GLES3JNILib.fields_mousey;


                    wasd_x += (float)Math.Cos(GLES3JNILib.fields_mousex * 0.005f + Math.PI / 2) * dyA * flatlandMouseForwardFrameSpeed;
                    wasd_y += (float)Math.Sin(GLES3JNILib.fields_mousex * 0.005f + Math.PI / 2) * dyA * flatlandMouseForwardFrameSpeed;
                }

                // A
                if (GLES3JNILib.fields_ad == 65)
                {
                    // GLES3JNILib.fields_mousex * 0.005f
                    wasd_x += (float)Math.Cos(GLES3JNILib.fields_mousex * 0.005f) * flatlandFrameSpeedStrafe;
                    wasd_y += (float)Math.Sin(GLES3JNILib.fields_mousex * 0.005f) * flatlandFrameSpeedStrafe;

                    //wasd_z += (float)(0.1f * this.App.AppThread.tracking.HeadPose.Pose.Orientation.z / 0.7f);
                }

                // D
                if (GLES3JNILib.fields_ad == 68)
                {
                    // GLES3JNILib.fields_mousex * 0.005f
                    wasd_x += (float)Math.Cos(GLES3JNILib.fields_mousex * 0.005f + Math.PI) * flatlandFrameSpeedStrafe;
                    wasd_y += (float)Math.Sin(GLES3JNILib.fields_mousex * 0.005f + Math.PI) * flatlandFrameSpeedStrafe;

                    //wasd_z += (float)(-0.1f * this.App.AppThread.tracking.HeadPose.Pose.Orientation.z / 0.7f);
                }

                // W
                if (GLES3JNILib.fields_ws == 87)
                {
                    // GLES3JNILib.fields_mousex * 0.005f
                    wasd_x += (float)Math.Cos(GLES3JNILib.fields_mousex * 0.005f + Math.PI / 2) * flatlandFrameSpeed;
                    wasd_y += (float)Math.Sin(GLES3JNILib.fields_mousex * 0.005f + Math.PI / 2) * flatlandFrameSpeed;

                    //wasd_z += (float)(-0.1f * this.App.AppThread.tracking.HeadPose.Pose.Orientation.x / 0.7f);
                }

                // S
                if (GLES3JNILib.fields_ws == 83)
                {
                    wasd_x += (float)Math.Cos(GLES3JNILib.fields_mousex * 0.005f - Math.PI / 2) * flatlandFrameSpeed;
                    wasd_y += (float)Math.Sin(GLES3JNILib.fields_mousex * 0.005f - Math.PI / 2) * flatlandFrameSpeed;
                    //wasd_z += (float)(0.1f * this.App.AppThread.tracking.HeadPose.Pose.Orientation.x / 0.7f);
                }




                var touchpadSpeed = 0.05f;

                //var flatlandFrameSpeed = 1.0f;



                wasd_x0 = wasd_x;
                wasd_y0 = wasd_y;
                wasd_z0 = wasd_z + y;

                // allow cheap positional tracking. at 6fps? 19fps
                //wasd_x0 += 0.7f * com.oculus.gles3jni.GLES3JNILib.fields_px;
                //wasd_x0 += 0.3f * com.oculus.gles3jni.GLES3JNILib.fields_px;

                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150712-1/
                // 60hz udp is a bit slow?
                wasd_x0 += (float)Math.Cos(GLES3JNILib.fields_mousex * 0.005f + Math.PI * 0) * com.oculus.gles3jni.GLES3JNILib.fields_px * 0.5f;
                wasd_y0 += (float)Math.Sin(GLES3JNILib.fields_mousex * 0.005f + Math.PI * 0) * com.oculus.gles3jni.GLES3JNILib.fields_px * 0.5f;


                y += com.oculus.gles3jni.GLES3JNILib.fields_py * 0.4f;

                // lean forward should be exclusive to left right up down?

                if (com.oculus.gles3jni.GLES3JNILib.fields_pz < 0)
                    com.oculus.gles3jni.GLES3JNILib.fields_pz *= 0.3f;

                wasd_x0 += (float)Math.Cos(GLES3JNILib.fields_mousex * 0.005f + Math.PI / 2) * com.oculus.gles3jni.GLES3JNILib.fields_pz * 0.1f;
                wasd_y0 += (float)Math.Sin(GLES3JNILib.fields_mousex * 0.005f + Math.PI / 2) * com.oculus.gles3jni.GLES3JNILib.fields_pz * 0.1f;


                // allow trackpad
                wasd_x0 += touchpadSpeed * com.oculus.gles3jni.GLES3JNILib.fields_xvalue;
                wasd_y0 += touchpadSpeed * com.oculus.gles3jni.GLES3JNILib.fields_yvalue;

                // at the direction
                //wasd_x0 += (float)Math.Cos(GLES3JNILib.fields_mousex * 0.005f) * flatlandFrameSpeedStrafe;
                //wasd_y0 += (float)Math.Sin(GLES3JNILib.fields_mousex * 0.005f) * flatlandFrameSpeedStrafe;

                //for (int i = 0; i < NUM_INSTANCES; i++)
                //{
                //    this.CubePositions[i].x = this.CubePositions0[i].x + wasd_x0;
                //    this.CubePositions[i].y = this.CubePositions0[i].y + wasd_z0;
                //    this.CubePositions[i].z = this.CubePositions0[i].z + wasd_y0;
                //}


                old_mousey = GLES3JNILib.fields_mousey;
                old_mousey_defined = true;
            }


        }


    }


}

//---------------------------
//Restoring Network Connections
//---------------------------
//An error occurred while reconnecting R: to
//\\192.168.1.12\x$
//Microsoft Windows Network: The local device name is already in use.


//This connection has not been restored.
//---------------------------
//OK   
//---------------------------

//---------------------------
//Windows
//---------------------------
//The mapped network drive could not be created because the following error has occurred:



//The specified network name is no longer available.


//---------------------------
//OK   
//---------------------------

