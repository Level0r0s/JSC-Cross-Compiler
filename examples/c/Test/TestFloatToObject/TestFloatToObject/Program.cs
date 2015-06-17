using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestFloatToObject
{
    class Program
    {
        public object[] Parms;



        public void ovrMessage_SetFloatParm(int i, float value)
        {
            //var p = (float*)Parms[i];
            Parms[i] = value;
        }

        public float ovrMessage_GetFloatParm(int i)
        {
            //jni/OVRVrCubeWorldSurfaceViewXNDK.dll.c: In function 'OVRVrCubeWorldSurfaceViewXNDK_VrCubeWorld_ovrMessage_ovrMessage_SetFloatParm':
            //jni/OVRVrCubeWorldSurfaceViewXNDK.dll.c:221:22: error: incompatible types when assigning to type 'void *' from type 'float'
            //     __that->Parms[i] = value;

            // X:\jsc.svn\examples\c\Test\TestFloatToObject\TestFloatToObject\Program.cs

            //var p = (float*)Parms[i];
            return (float)Parms[i];
            //return *p;
        }

        static void Main(string[] args)
        {
        }
    }
}
