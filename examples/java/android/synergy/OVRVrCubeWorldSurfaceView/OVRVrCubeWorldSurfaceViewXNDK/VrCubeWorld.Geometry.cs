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


        // autoinit field ovrScene,
        [Script]
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

            // called by ovrScene_Create
            // called after VRAPI_FRAME_INIT_LOADING_ICON_FLUSH
            public void ovrGeometry_CreateCube()
            {
                // 405

                var cubeIndices = new ushort[] 
                {
                    0, 1, 2, 2, 3, 0,	// top
                    4, 5, 6, 6, 7, 4,	// bottom
                    2, 6, 7, 7, 1, 2,	// right
                    0, 4, 5, 5, 3, 0,	// left
                    3, 5, 6, 6, 2, 3,	// front
                    0, 1, 7, 7, 4, 0	// back
                };

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

            // called by ovrScene_Destroy
            public void ovrGeometry_Destroy()
            {
                // 465

                var IndexBuffer0 = new[] { IndexBuffer };
                gl3.glDeleteBuffers(1, IndexBuffer0);
                var VertexBuffer0 = new[] { VertexBuffer };
                gl3.glDeleteBuffers(1, VertexBuffer0);

                //this.ovrGeometry_Clear();
            }

            // called by ovrScene_CreateVAOs
            public void ovrGeometry_CreateVAO()
            {
                // 473
                var VertexArrayObject0 = new[] { this.VertexArrayObject };
                gl3.glGenVertexArrays(1, VertexArrayObject0);
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

                var VertexArrayObject0 = new[] { this.VertexArrayObject };

                gl3.glDeleteVertexArrays(1, VertexArrayObject0);
            }
        }



    }


}
