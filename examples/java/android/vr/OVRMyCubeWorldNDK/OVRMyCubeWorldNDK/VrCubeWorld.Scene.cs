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
        public const int NUM_INSTANCES = 8 * 8 * 3;


        // member of ovrApp
        // member of ovrRenderThread
        // created by ovrApp_Clear
        public unsafe class ovrScene
        {
            public readonly ovrVector3f[] CubePositions0 = new ovrVector3f[NUM_INSTANCES];



            // ovrRenderer_RenderFrame
            // how are they sent to gpu?
            public readonly ovrVector3f[] CubePositions = new ovrVector3f[NUM_INSTANCES];
            public readonly ovrVector3f[] CubeRotations = new ovrVector3f[NUM_INSTANCES];

            public readonly ovrProgram Program = new ovrProgram();
            public readonly ovrGeometry Cube = new ovrGeometry();

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


                    rx = 3 * (((i / 3) / 8) - 2);

                    rz = 3 * (((i / 3) % 8) - 2);


                    ry = (i % 3 - 1) * 1;

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


            // HUDp50 thread!
            public void Update()
            {
                // UI thread writes, VR thread reads..

                for (int i = 0; i < NUM_INSTANCES; i++)
                {
                    this.CubePositions[i].x = this.CubePositions0[i].x + 0.05f * com.oculus.gles3jni.GLES3JNILib.fields_xvalue;
                    this.CubePositions[i].y = this.CubePositions0[i].y;
                    this.CubePositions[i].z = this.CubePositions0[i].z + 0.05f * com.oculus.gles3jni.GLES3JNILib.fields_yvalue;
                }

            }
        }


    }


}
