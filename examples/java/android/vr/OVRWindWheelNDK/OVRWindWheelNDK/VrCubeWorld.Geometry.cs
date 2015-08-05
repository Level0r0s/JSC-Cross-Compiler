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


        public enum ovrVertexAttribute_location //: uint
        {
            VERTEX_ATTRIBUTE_LOCATION_POSITION,
            VERTEX_ATTRIBUTE_LOCATION_COLOR,
            VERTEX_ATTRIBUTE_LOCATION_UV,

            VERTEX_ATTRIBUTE_LOCATION_TRANSFORM
            // ...
        }



        // glVertexAttribPointer
        public struct ovrVertexAttribPointer
        {
            public ovrVertexAttribute_location Index;

            public int Size;

            public int Type;

            public bool Normalized;

            public int Stride;
            public void* Pointer;
        }

        // http://stackoverflow.com/questions/8048540/sizeof-structures-not-known-why

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


        // X:\jsc.svn\examples\c\android\hybrid\HybridGLES3JNIActivity\HybridGLES3JNIActivityNDK\RendererES3.cs
        //public static readonly sbyte[,] ovrCubeVertices8x4_positions = new sbyte[8, 4]
        //{ 
        //        { -127, +127, -127, +127 }, { +127, +127, -127, +127 }, { +127, +127, +127, +127 }, { -127, +127, +127, +127 },	// top
        //        { -127, -127, -127, +127 }, { -127, -127, +127, +127 }, { +127, -127, +127, +127 }, { +127, -127, -127, +127 }	// bottom
        //    };


        // modified via CreateTranslation
        //const sbyte scale = 127;
        //const sbyte scale = 64;
        //const sbyte scale = 8;
        //const sbyte scale = 4;
        //const sbyte scale = 6;
        const sbyte scale = 16; // of 128

        // why cant we scale it via transform?
        // should we upgrade to float?
        public static readonly sbyte[,] ovrCubeVertices8x4_positions = new sbyte[8, 4]
        { 
                { -scale, +scale, -scale, +scale }, { +scale, +scale, -scale, +scale }, { +scale, +scale, +scale, +scale }, { -scale, +scale, +scale, +scale },	// top
                { -scale, -scale, -scale, +scale }, { -scale, -scale, +scale, +scale }, { +scale, -scale, +scale, +scale }, { +scale, -scale, -scale, +scale }	// bottom
            };

        // green blue cubes in vr
        //public static readonly byte[,] ovrCubeVertices8x4_colors = new byte[8, 4] { 
        //        {   0,   0, 0, 255}, {   0, 255,   0, 255 }, {   0,   127, 0, 255 }, { 0,   80,   0, 255 },
        //        {   0,   0, 255, 255 }, {   0, 0,   120, 255 }, { 0,   0, 100, 255 }, { 0,   0,   80, 255 }
        //    };


        // used by glVertexAttribPointer
        // what about color animation
        public static readonly byte[,] ovrCubeVertices8x4_colors_red = new byte[8, 4] { 
                {   255,   0, 0, 255}, {   255, 0,   0, 255 }, {   127,   0, 0, 255 }, { 80,   0,   0, 255 },
                {   255,   0, 0, 255 }, {   255, 0,   0, 255 }, { 127,   0, 0, 255 }, { 80,   0,   0, 255 }
        };

        public static readonly byte[,] ovrCubeVertices8x4_colors_green = new byte[8, 4] { 
                {   0,   255, 0, 255}, {   0, 255,   0, 255 }, { 0,   127, 0, 255 }, { 0,   80,   0, 255 },
                {   0,   255, 0, 255 }, {  0, 255,   0, 255 }, { 0,   127, 0, 255 }, { 0,   80,   0, 255 }
        };

        public static readonly byte[,] ovrCubeVertices8x4_colors_yellow0 = new byte[8, 4] { 
       
                {   255,   190, 0, 255}, {   255, 190,   0, 255 }, { 255,   190, 0, 255 }, { 255,   190,   0, 255 },
                {   255,   255, 0, 255 }, {  255, 255,   0, 255 }, { 255,   255, 0, 255 }, { 255,   255,   0, 255 }
        };

        // can we udp color?
        // if we change source, would jsc be able do patch the running apps via udp?
        public static readonly byte[,] ovrCubeVertices8x4_colors_yellow = new byte[8, 4] { 

                {   0x80,   0x80, 0, 255}, {   255, 255,   0, 255 }, { 255,   255, 0, 255 }, { 255,   255,   0, 255 },
                {   255,   255, 0, 255 }, {  255, 255,   0, 255 }, { 255,   255, 0, 255 }, { 255,   255,   0, 255 }

        };





        // is it needed?
        class ovrCubeVertices8x4_red
        {
            public readonly sbyte[,] positions = new sbyte[8, 4];

            // this needs to be a fixed buffer it seems for gpu
            readonly byte[,] colors = new byte[8, 4];
            //public readonly byte[,] colors = ovrCubeVertices8x4_colors;


            //public ovrCubeVertices8x4(byte[,] colors = ovrCubeVertices8x4_colors_red)
            //public ovrCubeVertices8x4(byte[,] colors)
            public ovrCubeVertices8x4_red()
            {
                ConsoleExtensions.trace("enter ovrCubeVertices8x4_red");

                //positions = ovrCubeVertices8x4_positions;

                for (int x = 0; x < 8; x++)
                    for (int y = 0; y < 4; y++)
                    {
                        positions[x, y] = ovrCubeVertices8x4_positions[x, y];
                        colors[x, y] = ovrCubeVertices8x4_colors_red[x, y];
                        //colors[x, y] = colors[x, y];

                    }
            }
        }



        class ovrCubeVertices8x4_green
        {
            public readonly sbyte[,] positions = new sbyte[8, 4];
            public readonly byte[,] colors = new byte[8, 4];
            //public readonly byte[,] colors = ovrCubeVertices8x4_colors;


            //public ovrCubeVertices8x4(byte[,] colors = ovrCubeVertices8x4_colors_red)
            //public ovrCubeVertices8x4(byte[,] colors)
            public ovrCubeVertices8x4_green()
            {
                ConsoleExtensions.trace("enter ovrCubeVertices8x4_green");

                //positions = ovrCubeVertices8x4_positions;

                for (int x = 0; x < 8; x++)
                    for (int y = 0; y < 4; y++)
                    {
                        positions[x, y] = ovrCubeVertices8x4_positions[x, y];
                        colors[x, y] = ovrCubeVertices8x4_colors_green[x, y];
                        //colors[x, y] = colors[x, y];

                    }
            }
        }

        class ovrCubeVertices8x4_yellow
        {
            public readonly sbyte[,] positions = new sbyte[8, 4];
            public readonly byte[,] colors = new byte[8, 4];
            //public readonly byte[,] colors = ovrCubeVertices8x4_colors;


            //public ovrCubeVertices8x4(byte[,] colors = ovrCubeVertices8x4_colors_red)
            //public ovrCubeVertices8x4(byte[,] colors)
            public ovrCubeVertices8x4_yellow()
            {
                ConsoleExtensions.trace("enter ovrCubeVertices8x4_yellow");

                //positions = ovrCubeVertices8x4_positions;

                for (int x = 0; x < 8; x++)
                    for (int y = 0; y < 4; y++)
                    {
                        positions[x, y] = ovrCubeVertices8x4_positions[x, y];
                        colors[x, y] = ovrCubeVertices8x4_colors_yellow[x, y];
                        //colors[x, y] = colors[x, y];

                    }
            }
        }

        const int MAX_VERTEX_ATTRIB_POINTERS = 3;


        // autoinit field ovrScene,
        public class ovrGeometry
        {
            // COLLADA geometry
            public readonly ovrVertexAttribPointer[] VertexAttribs = new ovrVertexAttribPointer[MAX_VERTEX_ATTRIB_POINTERS];

            // 391
            // set by ovrGeometry_CreateCube
            public uint VertexBuffer = 0;

            // GL_ELEMENT_ARRAY_BUFFER
            public uint IndexBuffer = 0;


            // set via glGenVertexArrays
            // sent to glBindVertexArray
            // before glDrawElementsInstanced
            public uint VertexArrayObject0 = 0;

            public int VertexCount = 8;

            // NUM_INSTANCES
            // sent to glDrawElementsInstanced
            public int IndexCount = 36;



            // sent via glBufferData
            readonly ovrCubeVertices8x4_red ovrCubeVertices8x4_red = new ovrCubeVertices8x4_red();
            readonly ovrCubeVertices8x4_green ovrCubeVertices8x4_green = new ovrCubeVertices8x4_green();
            readonly ovrCubeVertices8x4_yellow ovrCubeVertices8x4_yellow = new ovrCubeVertices8x4_yellow();

            //readonly ovrCubeVertices8x4 ovrCubeVertices8x4;


            public ovrGeometry()
            {
                //this.ovrCubeVertices8x4 = new ovrCubeVertices8x4(ovrCubeVertices8x4_colors_red);


                // 391


                for (int i = 0; i < MAX_VERTEX_ATTRIB_POINTERS; i++)
                {
                    //this.VertexAttribs[i] = default(ovrVertexAttribPointer);

                    //memset( &geometry->VertexAttribs[i], 0, sizeof( geometry->VertexAttribs[i] ) );
                    this.VertexAttribs[i].Index = (ovrVertexAttribute_location)(-1);
                }
            }



            // downgrade to byte?
            // 6x6 ushort
            static readonly ushort[] IndexBufferData = new ushort[] 
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
                ConsoleExtensions.trace("enter ovrGeometry_CreateCube");





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


                // 452
                gl3.glGenBuffers(1, out this.VertexBuffer);

                BindBufferData_red = delegate
                {
                    // how can renderer call it?

                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150716/secondary
                    //ConsoleExtensions.trace("enter ovrGeometry_CreateCube VertexBuffer BindBufferData_red");
                    gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, this.VertexBuffer);
                    // can we select it later too?
                    gl3.glBufferData(gl3.GL_ARRAY_BUFFER, sizeof(ovrCubeVertices), ovrCubeVertices8x4_red, gl3.GL_STATIC_DRAW);
                    gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, 0);
                };

                BindBufferData_green = delegate
                {
                    //ConsoleExtensions.trace("enter ovrGeometry_CreateCube VertexBuffer BindBufferData_green");

                    gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, this.VertexBuffer);
                    // can we select it later too?
                    gl3.glBufferData(gl3.GL_ARRAY_BUFFER, sizeof(ovrCubeVertices), ovrCubeVertices8x4_green, gl3.GL_STATIC_DRAW);
                    gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, 0);
                };

                // can udp stream update it?
                BindBufferData_yellow = delegate
                {
                    //ConsoleExtensions.trace("enter ovrGeometry_CreateCube VertexBuffer BindBufferData_green");

                    gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, this.VertexBuffer);
                    // can we select it later too?
                    gl3.glBufferData(gl3.GL_ARRAY_BUFFER, sizeof(ovrCubeVertices), ovrCubeVertices8x4_yellow, gl3.GL_STATIC_DRAW);
                    gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, 0);
                };

                BindBufferData_green();

                // 457
                gl3.glGenBuffers(1, out this.IndexBuffer);
                gl3.glBindBuffer(gl3.GL_ELEMENT_ARRAY_BUFFER, this.IndexBuffer);
                gl3.glBufferData(gl3.GL_ELEMENT_ARRAY_BUFFER, 6 * 6 * 4, IndexBufferData, gl3.GL_STATIC_DRAW);
                gl3.glBindBuffer(gl3.GL_ELEMENT_ARRAY_BUFFER, 0);

                //ConsoleExtensions.trace("exit ovrGeometry_CreateCube");
            }

            // ovrScene.Cube?
            public Action BindBufferData_red;
            public Action BindBufferData_green;
            public Action BindBufferData_yellow;

            // called by ovrScene_Destroy
            public void ovrGeometry_Destroy()
            {
                // 465

                gl3.glDeleteBuffers(1, ref IndexBuffer);
                gl3.glDeleteBuffers(1, ref VertexBuffer);

                //this.ovrGeometry_Clear();
                ConsoleExtensions.trace("exit ovrGeometry_Destroy");
            }

            // called by ovrScene_CreateVAOs
            public void ovrGeometry_CreateVAO()
            {
                ConsoleExtensions.trace("enter ovrGeometry_CreateVAO, glGenVertexArrays");
                // 473
                gl3.glGenVertexArrays(1, out VertexArrayObject0);
                gl3.glBindVertexArray(this.VertexArrayObject0);

                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, this.VertexBuffer);

                for (int i = 0; i < MAX_VERTEX_ATTRIB_POINTERS; i++)
                    if ((int)this.VertexAttribs[i].Index != -1)
                    {
                        ConsoleExtensions.tracei("ovrGeometry_CreateVAO VertexAttribs i: ", i);

                        gl3.glEnableVertexAttribArray((uint)this.VertexAttribs[i].Index);

                        gl3.glVertexAttribPointer(
                            (uint)this.VertexAttribs[i].Index,
                            this.VertexAttribs[i].Size,
                            this.VertexAttribs[i].Type,
                            this.VertexAttribs[i].Normalized,
                            this.VertexAttribs[i].Stride,
                            this.VertexAttribs[i].Pointer
                        );
                    }

                // why?
                gl3.glBindBuffer(gl3.GL_ELEMENT_ARRAY_BUFFER, this.IndexBuffer);

                gl3.glBindVertexArray(0);
                ConsoleExtensions.trace("exit ovrGeometry_CreateVAO");
            }

            // called by ovrScene_DestroyVAOs
            public void ovrGeometry_DestroyVAO()
            {
                // 496

                gl3.glDeleteVertexArrays(1, ref VertexArrayObject0);
            }
        }



    }


}
