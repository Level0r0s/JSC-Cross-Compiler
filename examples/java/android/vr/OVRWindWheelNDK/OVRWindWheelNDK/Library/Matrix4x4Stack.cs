using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVRWindWheelNDK.Library
{
    // NDK friendly 4x4 stack
    //struct Matrix4x4Stack


    // we want struct, cuz we want to define it inline, yet will we be able to reference it from delegates? no
    // lets make it a class instead. before frame exit we have to pop all values to allow reentry
    //unsafe struct mat4stack256
    unsafe class mat4stack256
    {
        // not to be constructed within a loop

        // offset0 means the default value?
        byte __offset;




        // will it translate to js?
        //fixed float __value[4 * 4 * (sizeof(__offset))];
        //Error	2	Fixed size buffer fields may only be members of structs	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\Library\Matrix4x4Stack.cs	27	21	OVRWindWheelNDK

        //fixed float __value[4 * 4 * (256)];

        float[] __value = new float[4 * 4 * 256];

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150717/matrix4x4
        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Numerics\Matrix4x4.cs




        static void Push(mat4stack256 that, ref ovrMatrix4f value)
        {
            that.__offset++;

            fixed (float* __value_M = value.M)
            //fixed (float* __value = that.__value)
            {
                //fixed ( &that.__value
                //fixed (that)

                //var current = &__value[offset];
                //var current_ovrMatrix4f = (ovrMatrix4f*)current;


                //// copy our floats out
                //value = *current_ovrMatrix4f;

                //jni/OVRWindWheelNDK.dll.c:354:16: error: incompatible types when assigning to type 'ovrMatrix4f' from type 'struct ovrMatrix4f *'
                //     (*(value)) = matrix4f_3;

                // lets do a slower copy?

                for (int i = 0; i < 4 * 4; i++)
                {
                    //var f = __value[offset + i];
                    //var f = that.__value[offset + i];

                    //// Error	19	You cannot use fixed size buffers contained in unfixed expressions. Try using the fixed statement.	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\Library\Matrix4x4Stack.cs	92	21	OVRWindWheelNDK
                    ////value.M[i] = f;
                    //__value_M[i] = f;

                    //var f = value.M[i];
                    var f = __value_M[i];

                    that.__value[that.__offset + i] = f;

                }
            }
        }

        // transactional memory/historic undo button
        public void Push(ovrMatrix4f value)
        {
            Push(this, ref value);
        }

        public void Pop()
        {
            __offset--;
        }

        public ovrMatrix4f Peek()
        {
            //jni/OVRWindWheelNDK.dll.c: In function 'OVRWindWheelNDK_Library_mat4stack256_Peek':
            //jni/OVRWindWheelNDK.dll.c:368:5: error: incompatible types when returning type 'void *' but 'ovrMatrix4f' was expected
            //     return  NULL;
            //     ^

            //var value0 = default(ovrMatrix4f);
            //var value = value0;


            //jni/OVRWindWheelNDK.dll.c: In function 'OVRWindWheelNDK_Library_mat4stack256_Peek':
            //jni/OVRWindWheelNDK.dll.c:369:15: error: incompatible types when assigning to type 'ovrMatrix4f' from type 'struct ovrMatrix4f *'
            //     matrix4f0 = (&matrix4f1);


            // Error	17	Cannot convert null to 'OVRWindWheelNDK.ovrMatrix4f' because it is a non-nullable value type	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\Library\Matrix4x4Stack.cs	61	33	OVRWindWheelNDK
            var value = default(ovrMatrix4f);
            Peek(this, out value);
            return value;
        }

        static void Peek(mat4stack256 that, out ovrMatrix4f value)
        {
            var offset = that.__offset * 4 * 4;

            fixed (float* __value_M = value.M)
            //fixed (float* __value = that.__value)
            {
                //fixed ( &that.__value
                //fixed (that)

                //var current = &__value[offset];
                //var current_ovrMatrix4f = (ovrMatrix4f*)current;


                //// copy our floats out
                //value = *current_ovrMatrix4f;

                //jni/OVRWindWheelNDK.dll.c:354:16: error: incompatible types when assigning to type 'ovrMatrix4f' from type 'struct ovrMatrix4f *'
                //     (*(value)) = matrix4f_3;

                // lets do a slower copy?

                for (int i = 0; i < 4 * 4; i++)
                {
                    //var f = __value[offset + i];
                    var f = that.__value[offset + i];

                    // Error	19	You cannot use fixed size buffers contained in unfixed expressions. Try using the fixed statement.	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\Library\Matrix4x4Stack.cs	92	21	OVRWindWheelNDK
                    //value.M[i] = f;
                    __value_M[i] = f;
                }
            }
        }


        //static void Peek(ref mat4stack256 that, out ovrMatrix4f value)
        //{
        //    var offset = that.__offset * 4 * 4;

        //    fixed (float* __value_M = value.M)
        //    fixed (float* __value = that.__value)
        //    {
        //        //fixed ( &that.__value
        //        //fixed (that)

        //        var current = &__value[offset];
        //        //var current_ovrMatrix4f = (ovrMatrix4f*)current;


        //        //// copy our floats out
        //        //value = *current_ovrMatrix4f;

        //        //jni/OVRWindWheelNDK.dll.c:354:16: error: incompatible types when assigning to type 'ovrMatrix4f' from type 'struct ovrMatrix4f *'
        //        //     (*(value)) = matrix4f_3;

        //        // lets do a slower copy?

        //        for (int i = 0; i < 4 * 4; i++)
        //        {
        //            var f = __value[offset + i];

        //            // Error	19	You cannot use fixed size buffers contained in unfixed expressions. Try using the fixed statement.	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\Library\Matrix4x4Stack.cs	92	21	OVRWindWheelNDK
        //            //value.M[i] = f;
        //            __value_M[i] = f;
        //        }
        //    }
        //}

        public static implicit operator ovrMatrix4f(mat4stack256 e)
        {
            return e.Peek();
        }
    }
}
