using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestStackFloatArray
{
    public unsafe class Class1
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150711/teststackfloatarray

        // X:\jsc.svn\examples\c\Test\TestStackFloatArray\TestStackFloatArray
        // r:\jsc.svn\examples\c\Test\TestStackFloatArray\TestStackFloatArray

        // at 60hz we should not touch malloc.
        // as we would run out of RAM fast.
        // in VR matrix ops need float arrays.
        // if we do create a newarr
        // yet do not return it, nor store it, possibly only forward it
        // it means downstream functions need either to copy it
        // or signal the caller not to destroy it.
        // for now lets assume downstream functions can only copy, as our GC protocol does not exist yet.
        // glsl also wont like any heap/malloc 

        static float[] heap_rotationX;

        static readonly float[] static_initarray_rotationX = new float[]
            {
                1,    0,     0, 0 ,
                0, 1, -1, 0 ,
                0, 1,  1, 0 ,
                0,    0,     0, 1
            };

        static void Invoke(float[] pfloat16)
        {

        }

        public static void CreateRotation(float radiansX, float radiansY, float radiansZ)
        {
            // in C++ it is executed on the same thread as soon as the object is deleted or popped off the stack,

            float sinX = 1.2f;
            float cosX = 1.1f;

            float[] stack_rotationX =
            {
                1,    0,     0, 0 ,
                0, cosX, -sinX, 0 ,
                0, sinX,  cosX, 0 ,
                0,    0,     0, 1
            };
            Invoke(stack_rotationX);


            //var localloc_rotationX = stackalloc float[16]
            //{
            //    1,    0,     0, 0 ,
            //    0, cosX, -sinX, 0 ,
            //    0, sinX,  cosX, 0 ,
            //    0,    0,     0, 1
            //};
            //Invoke(localloc_rotationX);

            heap_rotationX = new float[]
            {
                1,    0,     0, 0 ,
                0, cosX, -sinX, 0 ,
                0, sinX,  cosX, 0 ,
                0,    0,     0, 1
            };
            Invoke(heap_rotationX);
        }
    }
}

// roslyn
//(float*) calloc(1, sizeof(float) * 16);
//   (float*) calloc(1, sizeof(float) * 16)[0] = 1;
//   (float*) calloc(1, sizeof(float) * 16)[5] = single1;
//   (float*) calloc(1, sizeof(float) * 16)[6] = (-(single0));
//   (float*) calloc(1, sizeof(float) * 16)[9] = single0;
//   (float*) calloc(1, sizeof(float) * 16)[10] = single1;
//   (float*) calloc(1, sizeof(float) * 16)[15] = 1;
//   singleArray2 = (float*) calloc(1, sizeof(float) * 16);
//   (float*) calloc(1, sizeof(float) * 16);
//   (float*) calloc(1, sizeof(float) * 16)[0] = 1;
//   (float*) calloc(1, sizeof(float) * 16)[5] = single1;
//   (float*) calloc(1, sizeof(float) * 16)[6] = (-(single0));
//   (float*) calloc(1, sizeof(float) * 16)[9] = single0;
//   (float*) calloc(1, sizeof(float) * 16)[10] = single1;
//   (float*) calloc(1, sizeof(float) * 16)[15] = 1;
//   TestStackFloatArray_Class1_heap_rotationX = (float*) calloc(1, sizeof(float) * 16);