using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Numerics
{

    // https://msdn.microsoft.com/en-us/library/system.numerics.vector4(v=vs.110).aspx

    // https://github.com/dotnet/corefx/blob/master/src/System.Numerics.Vectors/src/System/Numerics/Matrix4x4.cs

    // X:\jsc.svn\core\ScriptCoreLib\GLSL\ivec4.cs


    // Error	42	The FieldOffset attribute can only be placed on members of types marked with the StructLayout(LayoutKind.Explicit)	X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Numerics\Matrix4x4.cs	29	10	ScriptCoreLib

    [StructLayout(LayoutKind.Explicit)]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Numerics.Matrix4x4")]
    internal unsafe struct __Matrix4x4
    {
        // cannot actually get the newer version into a CLR project???
        // useless.

        // X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRUDPMatrix\Program.cs
        // X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs
        // X:\jsc.svn\core\ScriptCoreLib\GLSL\mat4.cs

        // can this class be used in WebGL and VR/NDK?

        // VR/SIMD



        // they seem to not use fixed length array, nor dimensional array

        [FieldOffset(0)]
        public float M11;








        // union
        [FieldOffset(0)]
        public fixed float M[16];


        [FieldOffset(0)]
        public fixed float M1[4];


        // union
        //[FieldOffset(0)]
        //public float[,] M = new float[4, 4];


        // http://blogs.msdn.com/b/dotnet/archive/2014/11/05/using-system-numerics-vector-for-graphics-programming.aspx

        // https://msdn.microsoft.com/en-us/library/ms171868(v=vs.110).aspx

    }
}
