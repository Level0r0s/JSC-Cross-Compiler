using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestInitializeArray
{
    public class Class1
    {
        static readonly int[] staticfixedcubeIndices = new int[]
                {
                    0, 1, 2, 2, 3, 0,	// top
                    4, 5, 6, 6, 7, 4,	// bottom
                    2, 6, 7, 7, 1, 2,	// right
                    0, 4, 5, 5, 3, 0,	// left
                    3, 5, 6, 6, 2, 3,	// front
                    0, 1, 7, 7, 4, 0	// back
                };

        // this would only work from ctor?
        readonly ushort[] fixedcubeIndices = new ushort[]
                {
                    0, 1, 2, 2, 3, 0,	// top
                    4, 5, 6, 6, 7, 4,	// bottom
                    2, 6, 7, 7, 1, 2,	// right
                    0, 4, 5, 5, 3, 0,	// left
                    3, 5, 6, 6, 2, 3,	// front
                    0, 1, 7, 7, 4, 0	// back
                };

        static void ovrGeometry_CreateCube()
        {
            var cubeIndices = new ushort[] 
                {
                    0, 1, 2, 2, 3, 0,	// top
                    4, 5, 6, 6, 7, 4,	// bottom
                    2, 6, 7, 7, 1, 2,	// right
                    0, 4, 5, 5, 3, 0,	// left
                    3, 5, 6, 6, 2, 3,	// front
                    0, 1, 7, 7, 4, 0	// back
                };
        }
    }
}
