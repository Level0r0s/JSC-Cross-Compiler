using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using ScriptCoreLibNative.SystemHeaders.GLES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVRVrCubeWorldSurfaceViewXNDK
{
    public static unsafe partial class VrCubeWorld
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewNDK\staging\jni\VrCubeWorld_SurfaceView.c

        public const int NUM_INSTANCES = 1500;


        // member of ovrApp
        // member of ovrRenderThread
        // created by ovrApp_Clear
        unsafe class ovrScene
        {
            public readonly ovrVector3f[] CubePositions = new ovrVector3f[NUM_INSTANCES];
            public readonly ovrVector3f[] CubeRotations = new ovrVector3f[NUM_INSTANCES];

            // 815
            public bool CreatedScene = false;
            public bool CreatedVAOs = false;

            public readonly ovrProgram Program = new ovrProgram();
            public readonly ovrGeometry Cube = new ovrGeometry();

            // deleted by ovrScene_Destroy
            public uint InstanceTransformBuffer = 0;

            public bool ovrScene_IsCreated()
            {
                return this.CreatedScene;
            }

            // called by ovrScene_Create
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
            // called after VRAPI_FRAME_INIT_LOADING_ICON_FLUSH
            public void ovrScene_Create()
            {
                // 864
                //Error	2	The type 'ScriptCoreLib.GLSL.VertexShader' is defined in an assembly that is not referenced. You must add a reference to assembly 'ScriptCoreLib, Version=4.6.0.0, Culture=neutral, PublicKeyToken=null'.	X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.Scene.cs	103	17	OVRVrCubeWorldSurfaceViewXNDK
                //  ScriptCoreLib.GLSL.FragmentShader for Void .ctor() used at

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

                //var InstanceTransformBuffer0 = new[] { this.InstanceTransformBuffer };
                gl3.glDeleteBuffers(1, ref InstanceTransformBuffer);
                this.CreatedScene = false;

            }

        }


    }


}
