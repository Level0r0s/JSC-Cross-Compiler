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

namespace OVRMyCubeWorldNDK
{
    public static unsafe partial class VrCubeWorld
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewNDK\staging\jni\VrCubeWorld_SurfaceView.c

        // 779
        //public const int NUM_INSTANCES = 1500;
        //public const int NUM_INSTANCES = 12;
        //public const int NUM_INSTANCES = 6;


        public const int floors = 5;
        public const int NUM_INSTANCES = 8 * 8 * floors;


        // member of ovrApp
        // member of ovrRenderThread
        // created by ovrApp_Clear
        public unsafe class ovrScene
        {
            public readonly ovrVector3f[] CubePositions0 = new ovrVector3f[NUM_INSTANCES];



            // ovrRenderer_RenderFrame
            // how are they sent to gpu?
            // used by CreateTranslation
            public readonly ovrVector3f[] CubePositions = new ovrVector3f[NUM_INSTANCES];
            public readonly ovrVector3f[] CubeRotations = new ovrVector3f[NUM_INSTANCES];

            public readonly ovrProgram Program = new ovrProgram();
            public readonly ovrGeometry Cube = new ovrGeometry();

            // set after ovrScene_Create
            // 815
            public bool CreatedScene = false;
            public bool CreatedVAOs = false;


            // used by glMapBufferRange
            // set by glGenBuffers
            // deleted by ovrScene_Destroy
            public uint InstanceTransformBuffer = 0;

            // VRAPI_FRAME_INIT_LOADING_ICON_FLUSH
            public bool ovrScene_IsCreated()
            {
                return this.CreatedScene;
            }

            // called by ovrScene_Create
            public void ovrScene_CreateVAOs()
            {
                // 832
                ConsoleExtensions.trace("enter ovrScene_CreateVAOs, call ovrGeometry_CreateVAO");

                if (this.CreatedVAOs)
                    return;

                this.Cube.ovrGeometry_CreateVAO();

                // Modify the VAO to use the instance transform attributes.
                gl3.glBindVertexArray(this.Cube.VertexArrayObject);
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, this.InstanceTransformBuffer);

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
                    vert.ToString(),
                    frag.ToString()
                );

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


                    rx = 2.0f * (((i / floors) / 8) - floors);

                    rz = 2.0f * (((i / floors) % 8) - floors);


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

                gl3.glDeleteBuffers(1, ref InstanceTransformBuffer);
                this.CreatedScene = false;

            }



            static float wasd_x;
            static float wasd_y;



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
                var flatlandMouseForwardFrameSpeed = .05f;

                var flatlandFrameSpeed = .7f;
                var flatlandFrameSpeedStrafe = .5f;

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
                }

                // D
                if (GLES3JNILib.fields_ad == 68)
                {
                    // GLES3JNILib.fields_mousex * 0.005f
                    wasd_x += (float)Math.Cos(GLES3JNILib.fields_mousex * 0.005f + Math.PI) * flatlandFrameSpeedStrafe;
                    wasd_y += (float)Math.Sin(GLES3JNILib.fields_mousex * 0.005f + Math.PI) * flatlandFrameSpeedStrafe;
                }

                // W
                if (GLES3JNILib.fields_ws == 87)
                {
                    // GLES3JNILib.fields_mousex * 0.005f
                    wasd_x += (float)Math.Cos(GLES3JNILib.fields_mousex * 0.005f + Math.PI / 2) * flatlandFrameSpeed;
                    wasd_y += (float)Math.Sin(GLES3JNILib.fields_mousex * 0.005f + Math.PI / 2) * flatlandFrameSpeed;
                }

                // S
                if (GLES3JNILib.fields_ws == 83)
                {
                    wasd_x += (float)Math.Cos(GLES3JNILib.fields_mousex * 0.005f - Math.PI / 2) * flatlandFrameSpeed;
                    wasd_y += (float)Math.Sin(GLES3JNILib.fields_mousex * 0.005f - Math.PI / 2) * flatlandFrameSpeed;
                }


                var y = 0f;

                // C crouch or jump?
                if (GLES3JNILib.fields_c == 67)
                {
                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704

                    // how low can we go?
                    // if we change only one opcode, can we send out a patch via udp?
                    //y = -20f;
                    //y = +2f;
                    y = 0.5f;
                    // as a workaround right now we press run.

                }

                var touchpadSpeed = 0.05f;

                //var flatlandFrameSpeed = 1.0f;



                var wasd_x0 = wasd_x;
                var wasd_y0 = wasd_y;

                // allow trackpad
                wasd_x0 += touchpadSpeed * com.oculus.gles3jni.GLES3JNILib.fields_xvalue;
                wasd_y0 += touchpadSpeed * com.oculus.gles3jni.GLES3JNILib.fields_yvalue;

                // at the direction
                //wasd_x0 += (float)Math.Cos(GLES3JNILib.fields_mousex * 0.005f) * flatlandFrameSpeedStrafe;
                //wasd_y0 += (float)Math.Sin(GLES3JNILib.fields_mousex * 0.005f) * flatlandFrameSpeedStrafe;

                for (int i = 0; i < NUM_INSTANCES; i++)
                {
                    this.CubePositions[i].x = this.CubePositions0[i].x + wasd_x0;
                    this.CubePositions[i].y = this.CubePositions0[i].y + y;
                    this.CubePositions[i].z = this.CubePositions0[i].z + wasd_y0;
                }


                old_mousey = GLES3JNILib.fields_mousey;
                old_mousey_defined = true;
            }
        }


    }


}
