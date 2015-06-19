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


        enum ovrVertexAttribute_location //: uint
        {
            VERTEX_ATTRIBUTE_LOCATION_POSITION,
            VERTEX_ATTRIBUTE_LOCATION_COLOR,
            VERTEX_ATTRIBUTE_LOCATION_UV,
            VERTEX_ATTRIBUTE_LOCATION_TRANSFORM
        }

        struct ovrVertexAttribPointer
        {
            public ovrVertexAttribute_location Index;

            public int Size;

            public int Type;

            public bool Normalized;

            public int Stride;
            public void* Pointer;
        }

        // http://stackoverflow.com/questions/8048540/sizeof-structures-not-known-why



        //Error	1	The expression being assigned to 'OVRVrCubeWorldSurfaceViewXNDK.VrCubeWorld.ovrCubeVertices.positions' must be constant	X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.Geometry.cs	60	42	OVRVrCubeWorldSurfaceViewXNDK
        //Error	2	'OVRVrCubeWorldSurfaceViewXNDK.VrCubeWorld.i8vec4' does not have a predefined size, therefore sizeof can only be used in an unsafe context (consider using System.Runtime.InteropServices.Marshal.SizeOf)	X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.Geometry.cs	60	46	OVRVrCubeWorldSurfaceViewXNDK

        //[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size = 4, Pack = 4)]
        public unsafe struct i8vec4
        {
            // https://www.opengl.org/wiki/Data_Type_(GLSL)
            // http://stackoverflow.com/questions/28159591/how-to-pack4bytes-and-unpackvec4-between-c-and-glsl

            public sbyte x, y, z, w;
        }

        public struct u8vec4
        {
            public byte x, y, z, w;
        }

        public unsafe struct ovrCubeVertices
        {
            //public fixed sbyte positions[8 * sizeof(i8vec4)];
            public fixed sbyte positions[8 * 4];
            public fixed byte colors[8 * 4];
        }

        const int MAX_VERTEX_ATTRIB_POINTERS = 3;


        // autoinit field ovrScene,
        class ovrGeometry
        {
            public readonly ovrVertexAttribPointer[] VertexAttribs = new ovrVertexAttribPointer[MAX_VERTEX_ATTRIB_POINTERS];

            public uint VertexBuffer = 0;
            public uint IndexBuffer = 0;

            // sent to glBindVertexArray
            public uint VertexArrayObject = 0;

            public int VertexCount = 0;
            public int IndexCount = 0;

            public ovrGeometry()
            {
                // 391

                for (int i = 0; i < MAX_VERTEX_ATTRIB_POINTERS; i++)
                {
                    //this.VertexAttribs[i] = default(ovrVertexAttribPointer);

                    //memset( &geometry->VertexAttribs[i], 0, sizeof( geometry->VertexAttribs[i] ) );
                    this.VertexAttribs[i].Index = (ovrVertexAttribute_location)(-1);
                }
            }

            static readonly ushort[] cubeIndices = new ushort[] 
            {
                0, 1, 2, 2, 3, 0,	// top
                4, 5, 6, 6, 7, 4,	// bottom
                2, 6, 7, 7, 1, 2,	// right
                0, 4, 5, 5, 3, 0,	// left
                3, 5, 6, 6, 2, 3,	// front
                0, 1, 7, 7, 4, 0	// back
            };

            // called by ovrScene_Create
            // called after VRAPI_FRAME_INIT_LOADING_ICON_FLUSH
            public void ovrGeometry_CreateCube()
            {
                // X:\jsc.svn\examples\c\Test\TestInitializeArray\TestInitializeArray\Class1.cs
                // 405



                this.VertexCount = 8;
                this.IndexCount = 36;


                // 438
                // https://en.wikipedia.org/wiki/Offsetof
                // http://linux.die.net/man/3/offsetof

                this.VertexAttribs[0].Index = ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_POSITION;
                this.VertexAttribs[0].Size = 4;
                this.VertexAttribs[0].Type = gl3.GL_BYTE;
                this.VertexAttribs[0].Normalized = true;
                //this.VertexAttribs[0].Stride = sizeof( cubeVertices.positions[0] );
                this.VertexAttribs[0].Stride = sizeof(i8vec4); // there are 8 elements
                //this.VertexAttribs[0].Pointer = (const GLvoid *)offsetof( ovrCubeVertices, positions );
                //this.VertexAttribs[0].Pointer = System.Runtime.InteropServices.Marshal.OffsetOf(
                this.VertexAttribs[0].Pointer = (void*)0;


                this.VertexAttribs[1].Index = ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_COLOR;
                this.VertexAttribs[1].Size = 4;
                this.VertexAttribs[1].Type = gl3.GL_UNSIGNED_BYTE;
                this.VertexAttribs[1].Normalized = true;
                //this.VertexAttribs[1].Stride = sizeof( cubeVertices.colors[0] );
                this.VertexAttribs[1].Stride = sizeof(u8vec4);
                // The macro offsetof() returns the offset of the field member from the start of the structure type.
                //this.VertexAttribs[1].Pointer = (const GLvoid *)offsetof( ovrCubeVertices, colors );
                var offset_colors = (void*)(8 * 4);
                this.VertexAttribs[1].Pointer = offset_colors;

                gl3.glGenBuffers(1, out this.VertexBuffer);
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, this.VertexBuffer);
                //gl3.glBufferData( gl3.GL_ARRAY_BUFFER, sizeof( cubeVertices ), &cubeVertices, GL_STATIC_DRAW ) );
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, 0);

                gl3.glGenBuffers(1, out this.IndexBuffer);
                gl3.glBindBuffer(gl3.GL_ELEMENT_ARRAY_BUFFER, this.IndexBuffer);
                //gl3.glBufferData( gl3.GL_ELEMENT_ARRAY_BUFFER, sizeof( cubeIndices ), cubeIndices, gl3.GL_STATIC_DRAW ) ;
                gl3.glBindBuffer(gl3.GL_ELEMENT_ARRAY_BUFFER, 0);
            }

            // called by ovrScene_Destroy
            public void ovrGeometry_Destroy()
            {
                // 465

                //var IndexBuffer0 = new[] { IndexBuffer };
                gl3.glDeleteBuffers(1, ref IndexBuffer);
                //var VertexBuffer0 = new[] { VertexBuffer };
                gl3.glDeleteBuffers(1, ref VertexBuffer);

                //this.ovrGeometry_Clear();
            }

            // called by ovrScene_CreateVAOs
            public void ovrGeometry_CreateVAO()
            {
                // 473
                //var VertexArrayObject0 = new[] { this.VertexArrayObject };
                gl3.glGenVertexArrays(1, ref VertexArrayObject);
                gl3.glBindVertexArray(this.VertexArrayObject);

                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, this.VertexBuffer);

                for (int i = 0; i < MAX_VERTEX_ATTRIB_POINTERS; i++)
                    if ((int)this.VertexAttribs[i].Index != -1)
                    {
                        gl3.glEnableVertexAttribArray((uint)this.VertexAttribs[i].Index);
                        gl3.glVertexAttribPointer((uint)this.VertexAttribs[i].Index, this.VertexAttribs[i].Size,
                                this.VertexAttribs[i].Type, this.VertexAttribs[i].Normalized,
                                this.VertexAttribs[i].Stride, this.VertexAttribs[i].Pointer);
                    }

                gl3.glBindBuffer(gl3.GL_ELEMENT_ARRAY_BUFFER, this.IndexBuffer);

                gl3.glBindVertexArray(0);
            }

            // called by ovrScene_DestroyVAOs
            public void ovrGeometry_DestroyVAO()
            {
                // 496

                //var VertexArrayObject0 = new[] { this.VertexArrayObject };

                gl3.glDeleteVertexArrays(1, ref VertexArrayObject);
            }
        }



    }


}
