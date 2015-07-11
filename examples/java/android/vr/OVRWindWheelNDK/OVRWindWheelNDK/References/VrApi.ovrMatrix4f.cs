using ScriptCoreLib;
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
    public unsafe class __ovrMatrix4f
    {
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
                var o = (__ovrMatrix4f)so;

                object sa = (size_t)pa;
                var a = (__ovrMatrix4f)sa;

                object sb = (size_t)pb;
                var b = (__ovrMatrix4f)sb;

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



        public static implicit operator __ovrMatrix4f(float[] ptr_float16)
        {
            // Error	3	Cannot take the address of, get the size of, or declare a pointer to a managed type ('float[*,*]')	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	142	40	OVRWindWheelNDK

            //fixed (float* p = &float16)

            //var ff = &float16;
            object so = (size_t)(object)ptr_float16;
            var x = (__ovrMatrix4f)so;
            //System.Runtime.InteropServices.Marshal.pi
            // no memory relocation in our heap. thanks.
            return x;
        }

        public static implicit operator __ovrMatrix4f(ovrMatrix4f* o)
        {
            object so = (size_t)(void*)o;
            var x = (__ovrMatrix4f)so;
            //System.Runtime.InteropServices.Marshal.pi
            // no memory relocation in our heap. thanks.
            return x;
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
            __ovrMatrix4f o = &_o;
            __ovrMatrix4f a = &_a;
            __ovrMatrix4f b = &_b;

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

        public ovrMatrix4f Multiply(__ovrMatrix4f b)
        {
            return __ovrMatrix4f.Multiply(this, b);
        }

        public static ovrMatrix4f Multiply(__ovrMatrix4f a, __ovrMatrix4f b)
        {
            // perhaps the compiler could decide to optimize and move to a byref pointer version?


            // memory dereference magic
            // yet, in glsl we wont have pointers, all we will have is copy struct?
            // http://www.gamedev.net/topic/347232-glsl-pointers/

            // Error	3	A pointer must be indexed by only one value	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	93	98	OVRWindWheelNDK
            //Error	5	The type of a local declared in a fixed statement must be a pointer type	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	92	20	OVRWindWheelNDK

            ovrMatrix4f _o;
            __ovrMatrix4f o = &_o;
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


        public static ovrMatrix4f CreateRotation(float radiansX, float radiansY, float radiansZ)
        {
            // http://stackoverflow.com/questions/9087563/maximum-native-memory-that-can-be-allocated-to-an-android-app

            // Error	3	Cannot initialize object of type 'float*' with a collection initializer	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	248	17	OVRWindWheelNDK


            float sinX = math.sinf(radiansX);
            float cosX = math.cosf(radiansX);

            // Error	3	Cannot take the address of, get the size of, or declare a pointer to a managed type ('float[*,*]')	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs	292	61	OVRWindWheelNDK

            // we will leak a lot of heap...
            // not the heap. the stack thankyou. almost playing GC?
            // since we are not returning this array, it should live on stack. this way we wont leak memory as we are running without GC..
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


            __ovrMatrix4f x = rotationX;
            __ovrMatrix4f y = rotationY;
            __ovrMatrix4f z = rotationZ;

            //ovrMatrix4f o;

            ovrMatrix4f rotationXY = __ovrMatrix4f.Multiply(y, x);
            ovrMatrix4f rotationXYZ = __ovrMatrix4f.Multiply(z, &rotationXY);

            // copy it back..
            return rotationXYZ;
        }




        // inline
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ovrMatrix4f CreateTranslation(float x, float y, float z, float scale = 1.0f)
        {
            ovrMatrix4f _o;
            __ovrMatrix4f o = &_o;

            o.M[0, 0] = scale; o.M[0, 1] = 0.0f; o.M[0, 2] = 0.0f; o.M[0, 3] = x;
            o.M[1, 0] = 0.0f; o.M[1, 1] = scale; o.M[1, 2] = 0.0f; o.M[1, 3] = y;
            o.M[2, 0] = 0.0f; o.M[2, 1] = 0.0f; o.M[2, 2] = scale; o.M[2, 3] = z;
            o.M[3, 0] = 0.0f; o.M[3, 1] = 0.0f; o.M[3, 2] = 0.0f; o.M[3, 3] = 1.0f;

            return _o;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ovrMatrix4f Transpose(__ovrMatrix4f a)
        {
            ovrMatrix4f _o;
            __ovrMatrix4f o = &_o;

            o.M[0, 0] = a.M[0, 0]; o.M[0, 1] = a.M[1, 0]; o.M[0, 2] = a.M[2, 0]; o.M[0, 3] = a.M[3, 0];
            o.M[1, 0] = a.M[0, 1]; o.M[1, 1] = a.M[1, 1]; o.M[1, 2] = a.M[2, 1]; o.M[1, 3] = a.M[3, 1];
            o.M[2, 0] = a.M[0, 2]; o.M[2, 1] = a.M[1, 2]; o.M[2, 2] = a.M[2, 2]; o.M[2, 3] = a.M[3, 2];
            o.M[3, 0] = a.M[0, 3]; o.M[3, 1] = a.M[1, 3]; o.M[3, 2] = a.M[2, 3]; o.M[3, 3] = a.M[3, 3];

            return _o;
        }

    }
}
