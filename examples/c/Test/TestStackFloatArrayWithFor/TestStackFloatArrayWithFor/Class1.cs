using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]


namespace TestStackFloatArrayWithFor
{
    public class Class1
    {
        // X:\jsc.svn\examples\c\Test\TestStackFloatArray\TestStackFloatArray\Class1.cs

        public void CreateRotation(float radiansX, float radiansY, float radiansZ)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150715/localoc
            // in C++ it is executed on the same thread as soon as the object is deleted or popped off the stack,

            float sinX = 1.2f;
            float cosX = 1.1f;



            float[] stack_rotationX_alloca =
            {
                1,    0,     0, 0 ,
                0, cosX, -sinX, 0 ,
                0, sinX,  cosX, 0 ,
                0,    0,     0, 1
            };

            for (int i = 0; i < 4; i++)
            {

                sinX = stack_rotationX_alloca[i];
            }
        }
    }
}
