using ScriptCoreLib;
using ScriptCoreLibAndroidNDK.Library;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVRWindWheelNDK
{
    //float M[4][4];
    // 

    //public unsafe struct float4
    //{
    //    float float0;
    //    float float1;
    //    float float2;
    //    float float3;
    //}

    //public unsafe struct float4x4 //: VrApi_h
    //{
    //    public fixed float __value[4 * 4];

    //    // 16 floats in size!

    //    //float4 row0;
    //    //float4 row1;
    //    //float4 row2;
    //    //float4 row3;

    //    // can we return ref float?

    //    float* this[int row]
    //    {
    //        get
    //        {
    //            return (float*)&this;
    //        }
    //    }
    //}


    // allocated by glMapBufferRange, sizeof


    // Row-major 4x4 matrix.
    [Script(IsNative = true)]
    public unsafe struct ovrMatrix4f //: VrApi_h
    {
        // Fixed sized buffers can only be one-dimensional.
        // http://stackoverflow.com/questions/665573/multidimensional-arrays-in-a-struct-in-c-sharp

        // sent to glUniformMatrix4fv

        //public float M[4,4];
        // 8 * 4 * 4 = 128
        public fixed float M[4 * 4]; // no need to init it as it is native

        // Error	3	'OVRWindWheelNDK.ovrMatrix4f.M': cannot have instance field initializers in structs	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	61	34	OVRWindWheelNDK
        //public readonly float[,] xyM = new float[4, 4];
        //public readonly float xyM[4,4];

        // http://stackoverflow.com/questions/15071775/struct-with-fixed-sized-array-of-another-struct

        // now we cannot get the size for allocator anymore?

        // X:\jsc.svn\examples\c\Test\TestSizeOfUserStruct\TestSizeOfUserStruct\Class1.cs
        //Error	7	Cannot take the address of, get the size of, or declare a pointer to a managed type ('OVRWindWheelNDK.ovrMatrix4f')	X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRWindWheelNDK\VrCubeWorld.Renderer.cs	100	44	OVRWindWheelNDK

        // *
    }


    // we want C# opeator syntax, and to be like glmatrix.
    // yet we cannot trash the heap.
    // new will allocate a geap.
    // yet what if we just do a blind dereference?
    [Script]
    //public unsafe abstract class __ovrMatrix4f

    // enables M[,]
    //public unsafe class __ovrMatrix4<float>
    //public unsafe class __ovrMatrix4f
    public unsafe class __Matrix4x4
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150717/matrix4x4
        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Numerics\Matrix4x4.cs

        public readonly float[,] M = new float[4, 4];

        //public ovrMatrix4f __value;

        // caller will keep the value on stack
        public static ovrMatrix4f Multiply(ref ovrMatrix4f ref_a, ref ovrMatrix4f ref_b)
        {
            // memory dereference magic
            // yet, in glsl we wont have pointers, all we will have is copy struct?
            // http://www.gamedev.net/topic/347232-glsl-pointers/

            // Error	3	A pointer must be indexed by only one value	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	93	98	OVRWindWheelNDK
            //Error	5	The type of a local declared in a fixed statement must be a pointer type	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	92	20	OVRWindWheelNDK

            ovrMatrix4f _o;


            //fixed (ovrMatrix4f* po = &o)
            fixed (ovrMatrix4f* pa = &ref_a)
            fixed (ovrMatrix4f* pb = &ref_b)
            {
                object so = (size_t)(void*)&_o;
                var o = (__Matrix4x4)so;

                object sa = (size_t)pa;
                var a = (__Matrix4x4)sa;

                object sb = (size_t)pb;
                var b = (__Matrix4x4)sb;

                o.M[0, 0] = a.M[0, 0] * b.M[0, 0] + a.M[0, 1] * b.M[1, 0] + a.M[0, 2] * b.M[2, 0] + a.M[0, 3] * b.M[3, 0];
                o.M[1, 0] = a.M[1, 0] * b.M[0, 0] + a.M[1, 1] * b.M[1, 0] + a.M[1, 2] * b.M[2, 0] + a.M[1, 3] * b.M[3, 0];
                o.M[2, 0] = a.M[2, 0] * b.M[0, 0] + a.M[2, 1] * b.M[1, 0] + a.M[2, 2] * b.M[2, 0] + a.M[2, 3] * b.M[3, 0];
                o.M[3, 0] = a.M[3, 0] * b.M[0, 0] + a.M[3, 1] * b.M[1, 0] + a.M[3, 2] * b.M[2, 0] + a.M[3, 3] * b.M[3, 0];

                o.M[0, 1] = a.M[0, 0] * b.M[0, 1] + a.M[0, 1] * b.M[1, 1] + a.M[0, 2] * b.M[2, 1] + a.M[0, 3] * b.M[3, 1];
                o.M[1, 1] = a.M[1, 0] * b.M[0, 1] + a.M[1, 1] * b.M[1, 1] + a.M[1, 2] * b.M[2, 1] + a.M[1, 3] * b.M[3, 1];
                o.M[2, 1] = a.M[2, 0] * b.M[0, 1] + a.M[2, 1] * b.M[1, 1] + a.M[2, 2] * b.M[2, 1] + a.M[2, 3] * b.M[3, 1];
                o.M[3, 1] = a.M[3, 0] * b.M[0, 1] + a.M[3, 1] * b.M[1, 1] + a.M[3, 2] * b.M[2, 1] + a.M[3, 3] * b.M[3, 1];

                o.M[0, 2] = a.M[0, 0] * b.M[0, 2] + a.M[0, 1] * b.M[1, 2] + a.M[0, 2] * b.M[2, 2] + a.M[0, 3] * b.M[3, 2];
                o.M[1, 2] = a.M[1, 0] * b.M[0, 2] + a.M[1, 1] * b.M[1, 2] + a.M[1, 2] * b.M[2, 2] + a.M[1, 3] * b.M[3, 2];
                o.M[2, 2] = a.M[2, 0] * b.M[0, 2] + a.M[2, 1] * b.M[1, 2] + a.M[2, 2] * b.M[2, 2] + a.M[2, 3] * b.M[3, 2];
                o.M[3, 2] = a.M[3, 0] * b.M[0, 2] + a.M[3, 1] * b.M[1, 2] + a.M[3, 2] * b.M[2, 2] + a.M[3, 3] * b.M[3, 2];

                o.M[0, 3] = a.M[0, 0] * b.M[0, 3] + a.M[0, 1] * b.M[1, 3] + a.M[0, 2] * b.M[2, 3] + a.M[0, 3] * b.M[3, 3];
                o.M[1, 3] = a.M[1, 0] * b.M[0, 3] + a.M[1, 1] * b.M[1, 3] + a.M[1, 2] * b.M[2, 3] + a.M[1, 3] * b.M[3, 3];
                o.M[2, 3] = a.M[2, 0] * b.M[0, 3] + a.M[2, 1] * b.M[1, 3] + a.M[2, 2] * b.M[2, 3] + a.M[2, 3] * b.M[3, 3];
                o.M[3, 3] = a.M[3, 0] * b.M[0, 3] + a.M[3, 1] * b.M[1, 3] + a.M[3, 2] * b.M[2, 3] + a.M[3, 3] * b.M[3, 3];
            }

            return _o;
        }



        public static implicit operator __Matrix4x4(float[] ptr_float16)
        {
            // Error	3	Cannot take the address of, get the size of, or declare a pointer to a managed type ('float[*,*]')	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	142	40	OVRWindWheelNDK

            //fixed (float* p = &float16)

            //var ff = &float16;
            object so = (size_t)(object)ptr_float16;
            var x = (__Matrix4x4)so;
            //System.Runtime.InteropServices.Marshal.pi
            // no memory relocation in our heap. thanks.
            return x;
        }



        public static implicit operator __Matrix4x4(ovrMatrix4f* o)
        {
            object so = (size_t)(void*)o;
            var x = (__Matrix4x4)so;
            //System.Runtime.InteropServices.Marshal.pi
            // no memory relocation in our heap. thanks.
            return x;
        }





        // https://github.com/dotnet/corefx/blob/master/src/System.Numerics.Vectors/src/System/Numerics/Matrix4x4.cs
        //  public static Matrix4x4 Add(Matrix4x4 value1, Matrix4x4 value2)
        public static ovrMatrix4f Add(ovrMatrix4f _a, ovrMatrix4f _b)
        {
            // when is this of any use?

            ovrMatrix4f _o;
            __Matrix4x4 o = &_o;
            __Matrix4x4 a = &_a;
            __Matrix4x4 b = &_b;

            {
                o.M[0, 0] = a.M[0, 0] + b.M[0, 0];
                o.M[1, 0] = a.M[1, 0] + b.M[1, 0];
                o.M[2, 0] = a.M[2, 0] + b.M[2, 0];
                o.M[3, 0] = a.M[3, 0] + b.M[3, 0];

                o.M[0, 1] = a.M[0, 1] + b.M[0, 1];
                o.M[1, 1] = a.M[1, 1] + b.M[1, 1];
                o.M[2, 1] = a.M[2, 1] + b.M[2, 1];
                o.M[3, 1] = a.M[3, 1] + b.M[3, 1];

                o.M[0, 2] = a.M[0, 2] + b.M[0, 2];
                o.M[1, 2] = a.M[1, 2] + b.M[1, 2];
                o.M[2, 2] = a.M[2, 2] + b.M[2, 2];
                o.M[3, 2] = a.M[3, 2] + b.M[3, 2];

                o.M[0, 3] = a.M[0, 3] + b.M[0, 3];
                o.M[1, 3] = a.M[1, 3] + b.M[1, 3];
                o.M[2, 3] = a.M[2, 3] + b.M[2, 3];
                o.M[3, 3] = a.M[3, 3] + b.M[3, 3];

                // operating on fixed buffers, yet we want the dimensional array magic.
            }

            return _o;
        }



        public static ovrMatrix4f Multiply(ovrMatrix4f _a, ovrMatrix4f _b)
        {
            // perhaps the compiler could decide to optimize and move to a byref pointer version?


            // memory dereference magic
            // yet, in glsl we wont have pointers, all we will have is copy struct?
            // http://www.gamedev.net/topic/347232-glsl-pointers/

            // Error	3	A pointer must be indexed by only one value	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	93	98	OVRWindWheelNDK
            //Error	5	The type of a local declared in a fixed statement must be a pointer type	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	92	20	OVRWindWheelNDK

            ovrMatrix4f _o;
            __Matrix4x4 o = &_o;
            __Matrix4x4 a = &_a;
            __Matrix4x4 b = &_b;

            {
                o.M[0, 0] = a.M[0, 0] * b.M[0, 0] + a.M[0, 1] * b.M[1, 0] + a.M[0, 2] * b.M[2, 0] + a.M[0, 3] * b.M[3, 0];
                o.M[1, 0] = a.M[1, 0] * b.M[0, 0] + a.M[1, 1] * b.M[1, 0] + a.M[1, 2] * b.M[2, 0] + a.M[1, 3] * b.M[3, 0];
                o.M[2, 0] = a.M[2, 0] * b.M[0, 0] + a.M[2, 1] * b.M[1, 0] + a.M[2, 2] * b.M[2, 0] + a.M[2, 3] * b.M[3, 0];
                o.M[3, 0] = a.M[3, 0] * b.M[0, 0] + a.M[3, 1] * b.M[1, 0] + a.M[3, 2] * b.M[2, 0] + a.M[3, 3] * b.M[3, 0];

                o.M[0, 1] = a.M[0, 0] * b.M[0, 1] + a.M[0, 1] * b.M[1, 1] + a.M[0, 2] * b.M[2, 1] + a.M[0, 3] * b.M[3, 1];
                o.M[1, 1] = a.M[1, 0] * b.M[0, 1] + a.M[1, 1] * b.M[1, 1] + a.M[1, 2] * b.M[2, 1] + a.M[1, 3] * b.M[3, 1];
                o.M[2, 1] = a.M[2, 0] * b.M[0, 1] + a.M[2, 1] * b.M[1, 1] + a.M[2, 2] * b.M[2, 1] + a.M[2, 3] * b.M[3, 1];
                o.M[3, 1] = a.M[3, 0] * b.M[0, 1] + a.M[3, 1] * b.M[1, 1] + a.M[3, 2] * b.M[2, 1] + a.M[3, 3] * b.M[3, 1];

                o.M[0, 2] = a.M[0, 0] * b.M[0, 2] + a.M[0, 1] * b.M[1, 2] + a.M[0, 2] * b.M[2, 2] + a.M[0, 3] * b.M[3, 2];
                o.M[1, 2] = a.M[1, 0] * b.M[0, 2] + a.M[1, 1] * b.M[1, 2] + a.M[1, 2] * b.M[2, 2] + a.M[1, 3] * b.M[3, 2];
                o.M[2, 2] = a.M[2, 0] * b.M[0, 2] + a.M[2, 1] * b.M[1, 2] + a.M[2, 2] * b.M[2, 2] + a.M[2, 3] * b.M[3, 2];
                o.M[3, 2] = a.M[3, 0] * b.M[0, 2] + a.M[3, 1] * b.M[1, 2] + a.M[3, 2] * b.M[2, 2] + a.M[3, 3] * b.M[3, 2];

                o.M[0, 3] = a.M[0, 0] * b.M[0, 3] + a.M[0, 1] * b.M[1, 3] + a.M[0, 2] * b.M[2, 3] + a.M[0, 3] * b.M[3, 3];
                o.M[1, 3] = a.M[1, 0] * b.M[0, 3] + a.M[1, 1] * b.M[1, 3] + a.M[1, 2] * b.M[2, 3] + a.M[1, 3] * b.M[3, 3];
                o.M[2, 3] = a.M[2, 0] * b.M[0, 3] + a.M[2, 1] * b.M[1, 3] + a.M[2, 2] * b.M[2, 3] + a.M[2, 3] * b.M[3, 3];
                o.M[3, 3] = a.M[3, 0] * b.M[0, 3] + a.M[3, 1] * b.M[1, 3] + a.M[3, 2] * b.M[2, 3] + a.M[3, 3] * b.M[3, 3];

                // operating on fixed buffers, yet we want the dimensional array magic.
            }

            return _o;
        }

        public ovrMatrix4f Multiply(__Matrix4x4 b)
        {
            return __Matrix4x4.Multiply(this, b);
        }

        public static ovrMatrix4f Multiply(__Matrix4x4 a, __Matrix4x4 b, __Matrix4x4 c)
        {
            var ab = Multiply(a, b);
            //__ovrMatrix4f ab = &_ab;

            var abc = Multiply(&ab, c);

            return abc;

        }

        public static ovrMatrix4f Multiply(__Matrix4x4 a, __Matrix4x4 b)
        {
            // perhaps the compiler could decide to optimize and move to a byref pointer version?


            // memory dereference magic
            // yet, in glsl we wont have pointers, all we will have is copy struct?
            // http://www.gamedev.net/topic/347232-glsl-pointers/

            // Error	3	A pointer must be indexed by only one value	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	93	98	OVRWindWheelNDK
            //Error	5	The type of a local declared in a fixed statement must be a pointer type	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	92	20	OVRWindWheelNDK

            ovrMatrix4f _o;
            __Matrix4x4 o = &_o;
            //__ovrMatrix4f a = &_a;
            //__ovrMatrix4f b = &_b;

            {
                o.M[0, 0] = a.M[0, 0] * b.M[0, 0] + a.M[0, 1] * b.M[1, 0] + a.M[0, 2] * b.M[2, 0] + a.M[0, 3] * b.M[3, 0];
                o.M[1, 0] = a.M[1, 0] * b.M[0, 0] + a.M[1, 1] * b.M[1, 0] + a.M[1, 2] * b.M[2, 0] + a.M[1, 3] * b.M[3, 0];
                o.M[2, 0] = a.M[2, 0] * b.M[0, 0] + a.M[2, 1] * b.M[1, 0] + a.M[2, 2] * b.M[2, 0] + a.M[2, 3] * b.M[3, 0];
                o.M[3, 0] = a.M[3, 0] * b.M[0, 0] + a.M[3, 1] * b.M[1, 0] + a.M[3, 2] * b.M[2, 0] + a.M[3, 3] * b.M[3, 0];

                o.M[0, 1] = a.M[0, 0] * b.M[0, 1] + a.M[0, 1] * b.M[1, 1] + a.M[0, 2] * b.M[2, 1] + a.M[0, 3] * b.M[3, 1];
                o.M[1, 1] = a.M[1, 0] * b.M[0, 1] + a.M[1, 1] * b.M[1, 1] + a.M[1, 2] * b.M[2, 1] + a.M[1, 3] * b.M[3, 1];
                o.M[2, 1] = a.M[2, 0] * b.M[0, 1] + a.M[2, 1] * b.M[1, 1] + a.M[2, 2] * b.M[2, 1] + a.M[2, 3] * b.M[3, 1];
                o.M[3, 1] = a.M[3, 0] * b.M[0, 1] + a.M[3, 1] * b.M[1, 1] + a.M[3, 2] * b.M[2, 1] + a.M[3, 3] * b.M[3, 1];

                o.M[0, 2] = a.M[0, 0] * b.M[0, 2] + a.M[0, 1] * b.M[1, 2] + a.M[0, 2] * b.M[2, 2] + a.M[0, 3] * b.M[3, 2];
                o.M[1, 2] = a.M[1, 0] * b.M[0, 2] + a.M[1, 1] * b.M[1, 2] + a.M[1, 2] * b.M[2, 2] + a.M[1, 3] * b.M[3, 2];
                o.M[2, 2] = a.M[2, 0] * b.M[0, 2] + a.M[2, 1] * b.M[1, 2] + a.M[2, 2] * b.M[2, 2] + a.M[2, 3] * b.M[3, 2];
                o.M[3, 2] = a.M[3, 0] * b.M[0, 2] + a.M[3, 1] * b.M[1, 2] + a.M[3, 2] * b.M[2, 2] + a.M[3, 3] * b.M[3, 2];

                o.M[0, 3] = a.M[0, 0] * b.M[0, 3] + a.M[0, 1] * b.M[1, 3] + a.M[0, 2] * b.M[2, 3] + a.M[0, 3] * b.M[3, 3];
                o.M[1, 3] = a.M[1, 0] * b.M[0, 3] + a.M[1, 1] * b.M[1, 3] + a.M[1, 2] * b.M[2, 3] + a.M[1, 3] * b.M[3, 3];
                o.M[2, 3] = a.M[2, 0] * b.M[0, 3] + a.M[2, 1] * b.M[1, 3] + a.M[2, 2] * b.M[2, 3] + a.M[2, 3] * b.M[3, 3];
                o.M[3, 3] = a.M[3, 0] * b.M[0, 3] + a.M[3, 1] * b.M[1, 3] + a.M[3, 2] * b.M[2, 3] + a.M[3, 3] * b.M[3, 3];

                // operating on fixed buffers, yet we want the dimensional array magic.
            }

            return _o;
        }

#if Fcalloc
        public static ovrMatrix4f CreateRotation(float radiansX, float radiansY, float radiansZ)
        {
            // Error	3	Cannot initialize object of type 'float*' with a collection initializer	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	248	17	OVRWindWheelNDK


            float sinX = math.sinf(radiansX);
            float cosX = math.cosf(radiansX);

            // Error	3	Cannot take the address of, get the size of, or declare a pointer to a managed type ('float[*,*]')	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	292	61	OVRWindWheelNDK


        // jsc cannot handle it yet
            // this seems to be the only way to do matrix math in C# ?
            float[,] rotationX = 
	         { 
		        { 1,    0,     0, 0 },
		        { 0, cosX, -sinX, 0 },
		        { 0, sinX,  cosX, 0 },
		        { 0,    0,     0, 1 }
	         };

            float sinY = math.sinf(radiansY);
            float cosY = math.cosf(radiansY);

            float[,] rotationY = 
	        { 
		        {  cosY, 0, sinY, 0 },
		        {     0, 1,    0, 0 },
		        { -sinY, 0, cosY, 0 },
		        {     0, 0,    0, 1 }
	        };

            float sinZ = math.sinf(radiansZ);
            float cosZ = math.cosf(radiansZ);

            float[,] rotationZ = 
	        { 
		        { cosZ, -sinZ, 0, 0 },
		        { sinZ,  cosZ, 0, 0 },
		        {    0,     0, 1, 0 },
		        {    0,     0, 0, 1 }
	        };



            ovrMatrix4f rotationXY = __ovrMatrix4f.Multiply(rotationY, rotationX);
            ovrMatrix4f rotationXYZ = __ovrMatrix4f.Multiply(rotationZ, &rotationXY);

            // copy it back..
            return rotationXYZ;
        }
#endif


        // AggressiveInlining could be the hint to prevent calloc?
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ovrMatrix4f CreateRotation(float radiansX, float radiansY, float radiansZ)
        {
            // X:\jsc.svn\examples\c\Test\TestStackFloatArray\TestStackFloatArray\Class1.cs

            // http://stackoverflow.com/questions/9087563/maximum-native-memory-that-can-be-allocated-to-an-android-app

            // Error	3	Cannot initialize object of type 'float*' with a collection initializer	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	248	17	OVRWindWheelNDK

            // I/DEBUG   ( 9747): signal 11 (SIGSEGV), code 1 (SEGV_MAPERR), fault addr 0x8

            float sinX = math.sinf(radiansX);
            float cosX = math.cosf(radiansX);

            // Error	3	Cannot take the address of, get the size of, or declare a pointer to a managed type ('float[*,*]')	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	292	61	OVRWindWheelNDK

            // we will leak a lot of heap...
            // not the heap. the stack thankyou. almost playing GC?
            // since we are not returning this array, it should live on stack. this way we wont leak memory as we are running without GC..
            //float[] rotationX = stackalloc float[16];
            //float[] rotationXx = new float[16];

            //// I/xNativeActivity( 7804): \VrApi.ovrMatrix4f.cs:338 out of heap? errno: 12 Out of memory
            //if (rotationXx == null)
            //{
            //    // https://android.googlesource.com/platform/development/+/c817c5210e4207908b83faaf08a2c5b95251f871/ndk/platforms/android-5/arch-x86/usr/include/malloc.h

            //    ConsoleExtensions.trace("out of heap?");

            //    //I/System.Console(19158): 4ad6:480b enter GetAllNetworkInterfaces
            //    //I/xNativeActivity(18481): \VrApi.ovrMatrix4f.cs:343 out of heap? errno: 12 Out of memory
            //    //I/xNativeActivity(18481): \VrCubeWorld.AppThread.cs:71 mallinfo  total allocated space:  -2083023504
            //    //I/xNativeActivity(18481): \VrCubeWorld.AppThread.cs:72 mallinfo  total free  space:  76049040

            //    //malloc_h.malloc_stats();

            //    VrCubeWorld.ovrAppThread.xmallinfo();

            //    unistd._exit(43);
            //}


            //stdlib_h.free(rotationXx);


            float[] rotationX =
	         { 
		        1,    0,     0, 0 ,
		        0, cosX, -sinX, 0 ,
		        0, sinX,  cosX, 0 ,
		        0,    0,     0, 1 
	         };

            float sinY = math.sinf(radiansY);
            float cosY = math.cosf(radiansY);

            float[] rotationY = 
	        { 
		         cosY, 0, sinY, 0 ,
		            0, 1,    0, 0 ,
		        -sinY, 0, cosY, 0 ,
		            0, 0,    0, 1 
	        };


            float sinZ = math.sinf(radiansZ);
            float cosZ = math.cosf(radiansZ);

            float[] rotationZ = 
	        { 
		        cosZ, -sinZ, 0, 0 ,
		        sinZ,  cosZ, 0, 0 ,
		           0,     0, 1, 0 ,
		           0,     0, 0, 1 
	        };



            __Matrix4x4 x = rotationX;
            __Matrix4x4 y = rotationY;
            __Matrix4x4 z = rotationZ;

            //ovrMatrix4f o;

            ovrMatrix4f rotationXY = __Matrix4x4.Multiply(y, x);
            ovrMatrix4f rotationXYZ = __Matrix4x4.Multiply(z, &rotationXY);

            // copy it back..
            return rotationXYZ;
        }




        // inline
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ovrMatrix4f CreateTranslation(float x, float y, float z, float scale = 1.0f)
        {
            ovrMatrix4f _o;
            __Matrix4x4 o = &_o;

            o.M[0, 0] = scale; o.M[0, 1] = 0.0f; o.M[0, 2] = 0.0f; o.M[0, 3] = x;
            o.M[1, 0] = 0.0f; o.M[1, 1] = scale; o.M[1, 2] = 0.0f; o.M[1, 3] = y;
            o.M[2, 0] = 0.0f; o.M[2, 1] = 0.0f; o.M[2, 2] = scale; o.M[2, 3] = z;
            o.M[3, 0] = 0.0f; o.M[3, 1] = 0.0f; o.M[3, 2] = 0.0f; o.M[3, 3] = 1.0f;

            return _o;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ovrMatrix4f Transpose(__Matrix4x4 a)
        {
            ovrMatrix4f _o;
            __Matrix4x4 o = &_o;

            o.M[0, 0] = a.M[0, 0]; o.M[0, 1] = a.M[1, 0]; o.M[0, 2] = a.M[2, 0]; o.M[0, 3] = a.M[3, 0];
            o.M[1, 0] = a.M[0, 1]; o.M[1, 1] = a.M[1, 1]; o.M[1, 2] = a.M[2, 1]; o.M[1, 3] = a.M[3, 1];
            o.M[2, 0] = a.M[0, 2]; o.M[2, 1] = a.M[1, 2]; o.M[2, 2] = a.M[2, 2]; o.M[2, 3] = a.M[3, 2];
            o.M[3, 0] = a.M[0, 3]; o.M[3, 1] = a.M[1, 3]; o.M[3, 2] = a.M[2, 3]; o.M[3, 3] = a.M[3, 3];

            return _o;
        }








        // should inlicing be also done by jsc merge?
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ovrMatrix4f CreateProjection(
                    float minX,
                    float maxX,
                    float minY,
                    float maxY,
                    float nearZ,
                    float farZ)
        {
            float width = maxX - minX;
            float height = maxY - minY;
            float offsetZ = nearZ;	// set to zero for a [0,1] clip space

            ovrMatrix4f that_data;
            __Matrix4x4 that = &that_data;

            // what about jagged arrays?
            if (farZ <= nearZ)
            {
                // place the far plane at infinity
                that.M[0, 0] = 2 * nearZ / width;
                that.M[0, 1] = 0;
                that.M[0, 2] = (maxX + minX) / width;
                that.M[0, 3] = 0;

                that.M[1, 0] = 0;
                that.M[1, 1] = 2 * nearZ / height;
                that.M[1, 2] = (maxY + minY) / height;
                that.M[1, 3] = 0;

                that.M[2, 0] = 0;
                that.M[2, 1] = 0;
                that.M[2, 2] = -1;
                that.M[2, 3] = -(nearZ + offsetZ);

                that.M[3, 0] = 0;
                that.M[3, 1] = 0;
                that.M[3, 2] = -1;
                that.M[3, 3] = 0;
            }
            else
            {
                // normal projection
                that.M[0, 0] = 2 * nearZ / width;
                that.M[0, 1] = 0;
                that.M[0, 2] = (maxX + minX) / width;
                that.M[0, 3] = 0;

                that.M[1, 0] = 0;
                that.M[1, 1] = 2 * nearZ / height;
                that.M[1, 2] = (maxY + minY) / height;
                that.M[1, 3] = 0;

                that.M[2, 0] = 0;
                that.M[2, 1] = 0;
                that.M[2, 2] = -(farZ + offsetZ) / (farZ - nearZ);
                that.M[2, 3] = -(farZ * (nearZ + offsetZ)) / (farZ - nearZ);

                that.M[3, 0] = 0;
                that.M[3, 1] = 0;
                that.M[3, 2] = -1;
                that.M[3, 3] = 0;
            }

            return that_data;
        }



        // http://stackoverflow.com/questions/53827/checking-available-stack-size-in-c
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ovrMatrix4f CreateProjectionFov(
            float fovRadiansX,
            float fovRadiansY,
            float offsetX,
            float offsetY,
            float nearZ,
            float farZ)
        {
            float halfWidth = nearZ * math.tanf(fovRadiansX * 0.5f);
            float halfHeight = nearZ * math.tanf(fovRadiansY * 0.5f);

            float minX = offsetX - halfWidth;
            float maxX = offsetX + halfWidth;

            float minY = offsetY - halfHeight;
            float maxY = offsetY + halfHeight;

            return CreateProjection(minX, maxX, minY, maxY, nearZ, farZ);
        }



        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        //public static ovrMatrix4f TanAngleMatrixFromProjection(__ovrMatrix4f projection)
        public static ovrMatrix4f TanAngleMatrixFromProjection(ovrMatrix4f __projection)
        {
            ConsoleExtensions.trace("enter TanAngleMatrixFromProjection");

            __Matrix4x4 projection = &__projection;

            // A projection matrix goes from a view point to NDC, or -1 to 1 space.
            // Scale and bias to convert that to a 0 to 1 space.
            //const ovrMatrix4f tanAngleMatrix =
            //float[,] tanAngleMatrix =
            //{ 
            //    { 0.5f * projection.M[0,0], 0.5f * projection.M[0,1], 0.5f * projection.M[0,2] - 0.5f, 0.5f * projection.M[0,3] },
            //    { 0.5f * projection.M[1,0], 0.5f * projection.M[1,1], 0.5f * projection.M[1,2] - 0.5f, 0.5f * projection.M[1,3] },
            //    {0.0f, 0.0f, -1.0f, 0.0f},
            //    {0.0f, 0.0f, -1.0f, 0.0f }
            //};

            float[] tanAngleMatrix =
	        { 
                 0.5f * projection.M[0,0], 0.5f * projection.M[0,1], 0.5f * projection.M[0,2] - 0.5f, 0.5f * projection.M[0,3] ,
		         0.5f * projection.M[1,0], 0.5f * projection.M[1,1], 0.5f * projection.M[1,2] - 0.5f, 0.5f * projection.M[1,3] ,
		        0.0f, 0.0f, -1.0f, 0.0f ,
		        0.0f, 0.0f, -1.0f, 0.0f 
	        };

            ovrMatrix4f that_data;
            __Matrix4x4 that = &that_data;

            //that_data.M = tanAngleMatrix;
            //that_data.M[0] = tanAngleMatrix[0];
            //for (int i = 0; i < 16; i++)
            //{
            //    // this will cause stack corruption. why?
            //    that.M[i, i] = tanAngleMatrix[i];
            //}

            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                {
                    //that.M[x, y] = tanAngleMatrix[x, y];
                    //that.M[x, y] = tanAngleMatrix[x + 4 * y];

                    // memorylayout the same as for CLR?
                    that.M[x, y] = tanAngleMatrix[y + 4 * x];
                }

            //jni/OVRWindWheelNDK.dll.c:2651:28: error: expected expression before '?' token
            //     singleArray7 = calloc [?][?];
            //                            ^

            ConsoleExtensions.trace("exit TanAngleMatrixFromProjection, stack still valid?");
            return that_data;
        }



        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ovrMatrix4f GetEyeViewMatrix(ref ovrHeadModelParms headModelParms,
                                                    ref ovrMatrix4f centerEyeViewMatrix,
                                                    int eye)
        {
            var o = (eye > 0 ? -0.5f : 0.5f);

            //float eyeOffset = (eye > 0 ? -0.5f : 0.5f) * headModelParms.InterpupillaryDistance;
            float eyeOffset = o * headModelParms.InterpupillaryDistance;
            ovrMatrix4f eyeOffsetMatrix = CreateTranslation(eyeOffset, 0.0f, 0.0f);
            return Multiply(ref eyeOffsetMatrix, ref centerEyeViewMatrix);
        }


        //[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        //static inline ovrMatrix4f vrapi_GetCenterEyeViewMatrix(	
        //    ref ovrHeadModelParms headModelParms,
        //                                                ref ovrTracking tracking,
        //                                                ref ovrMatrix4f input 
        //    )
        //{
        //    const ovrMatrix4f centerEyeTransform = vrapi_GetCenterEyeTransform( headModelParms, tracking, input );
        //    return ovrMatrix4f_Inverse( &centerEyeTransform );
        //}






        //public static ovrMatrix4f translate(ovrMatrix4f input, float x, float y, float z)
        //{
        //    ovrMatrix4f output;

        //    translate(ref output, ref input, x, y, z);


        //    return output;
        //}

        //public static void translate(ref ovrMatrix4f output, ref ovrMatrix4f input, float x, float y, float z)
        //{
        //    //float x = xyz[0], y = xyz[1], z = xyz[2];


        //    //if (input == output)
        //    //{
        //    //    output[12] = input[0] * x + input[4] * y + input[8] * z + input[12];
        //    //    output[13] = input[1] * x + input[5] * y + input[9] * z + input[13];
        //    //    output[14] = input[2] * x + input[6] * y + input[10] * z + input[14];
        //    //    output[15] = input[3] * x + input[7] * y + input[11] * z + input[15];

        //    //    return output;
        //    //}

        //    fixed (float* input_M = input.M)
        //    fixed (float* output_M = output.M)
        //    {
        //        float a00, a01, a02, a03,
        //            a10, a11, a12, a13,
        //            a20, a21, a22, a23;


        //        a00 = input_M[0]; a01 = input_M[1]; a02 = input_M[2]; a03 = input_M[3];
        //        a10 = input_M[4]; a11 = input_M[5]; a12 = input_M[6]; a13 = input_M[7];
        //        a20 = input_M[8]; a21 = input_M[9]; a22 = input_M[10]; a23 = input_M[11];

        //        output_M[0] = a00; output_M[1] = a01; output_M[2] = a02; output_M[3] = a03;
        //        output_M[4] = a10; output_M[5] = a11; output_M[6] = a12; output_M[7] = a13;
        //        output_M[8] = a20; output_M[9] = a21; output_M[10] = a22; output_M[11] = a23;

        //        output_M[12] = a00 * x + a10 * y + a20 * z + input_M[12];
        //        output_M[13] = a01 * x + a11 * y + a21 * z + input_M[13];
        //        output_M[14] = a02 * x + a12 * y + a22 * z + input_M[14];
        //        output_M[15] = a03 * x + a13 * y + a23 * z + input_M[15];
        //    }

        //}



        //public static float[] translate(float[] output, float[] input, float[] xyz)
        //{
        //    float x = xyz[0], y = xyz[1], z = xyz[2];

        //    if (input == output)
        //    {
        //        output[12] = input[0] * x + input[4] * y + input[8] * z + input[12];
        //        output[13] = input[1] * x + input[5] * y + input[9] * z + input[13];
        //        output[14] = input[2] * x + input[6] * y + input[10] * z + input[14];
        //        output[15] = input[3] * x + input[7] * y + input[11] * z + input[15];

        //        return output;
        //    }

        //    float a00, a01, a02, a03,
        //        a10, a11, a12, a13,
        //        a20, a21, a22, a23;


        //    a00 = input[0]; a01 = input[1]; a02 = input[2]; a03 = input[3];
        //    a10 = input[4]; a11 = input[5]; a12 = input[6]; a13 = input[7];
        //    a20 = input[8]; a21 = input[9]; a22 = input[10]; a23 = input[11];

        //    output[0] = a00; output[1] = a01; output[2] = a02; output[3] = a03;
        //    output[4] = a10; output[5] = a11; output[6] = a12; output[7] = a13;
        //    output[8] = a20; output[9] = a21; output[10] = a22; output[11] = a23;

        //    output[12] = a00 * x + a10 * y + a20 * z + input[12];
        //    output[13] = a01 * x + a11 * y + a21 * z + input[13];
        //    output[14] = a02 * x + a12 * y + a22 * z + input[14];
        //    output[15] = a03 * x + a13 * y + a23 * z + input[15];


        //    return output;
        //}


        //public static void Set(ref ovrMatrix4f xyz, byte* pBuffer)
        public static void Set(ref ovrMatrix4f xyz, byte[] aBuffer, int UDPMatrixIndex)
        {
            // Error	7	You cannot use fixed size buffers contained in unfixed expressions. Try using the fixed statement.	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	808	21	OVRWindWheelNDK
            // Error	7	You cannot use the fixed statement to take the address of an already fixed expression	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	782	37	OVRWindWheelNDK

            //ConsoleExtensions.tracef("ovrRenderer_RenderFrame UDPDraw[] ", f);

            //fixed (float* __xyz_M = &xyz.M[0])
            fixed (float* __xyz_M = xyz.M)
            fixed (byte* pBuffer = &aBuffer[0])
            {
                var pFloatBuffer = (float*)pBuffer;

                //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  1.000000
                //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
                //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
                //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  2.000000
                //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
                //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  1.000000
                //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
                //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  3.000000
                //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
                //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
                //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  1.000000
                //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  4.000000
                //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
                //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
                //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
                //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  1.000000

                for (int i = 0; i < 4 * 4; i++)
                {
                    var f = pFloatBuffer[i + UDPMatrixIndex * (64 / 4)];

                    //appThread.appState.tracef60("ovrRenderer_RenderFrame UDPDraw[] ", f);
                    //ConsoleExtensions.tracef("ovrRenderer_RenderFrame UDPDraw[] ", f);


                    __xyz_M[i] = f;
                    //xyz.M[i] = f;
                    // *(((float*)((&((&matrix4f2)->M)))) + (num5 * 4)) = single6;
                }
            }

            // I/DEBUG   (32762): signal 11 (SIGSEGV), code 1 (SEGV_MAPERR), fault addr 0x4
            // new data available!
            //gpuInstanceTransformBuffer1[0] = __Matrix4x4.Transpose(&xyz);
            //appThread.appState.tracei60("ovrRenderer_RenderFrame UDPDraw ? crash?");
            //ConsoleExtensions.trace("ovrRenderer_RenderFrame UDPDraw ? crash?");
        }
    }
}
