using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestDimensionalArray
{

    //struct foo
    //{
    //    public fixed int buffer[4, 4];
    //}

    // Severity	Code	Description	Project	File	Line
    //Error CS0650  Bad array declarator: To declare a managed array the rank specifier precedes the variable's identifier. To declare a fixed size buffer field, use the fixed keyword before the field type.	TestDimensionalArray	X:\jsc.svn\examples\c\Test\TestStructArrayField\TestDimensionalArray\TestDimensionalArray\Program.cs	15
    //Error CS7092  A fixed buffer may only have one dimension.TestDimensionalArray    X:\jsc.svn\examples\c\Test\TestStructArrayField\TestDimensionalArray\TestDimensionalArray\Program.cs    15

    class goo
    {
        // Severity	Code	Description	Project	File	Line
        //Error CS0573	'goo': cannot have instance property or field initializers in structs TestDimensionalArray    X:\jsc.svn\examples\c\Test\TestStructArrayField\TestDimensionalArray\TestDimensionalArray\Program.cs	24

        // once in C we could now behave as if we knew the size the array?
        public readonly int[,,] fixedbuffer = new int[4, 4, 4];

        // __that->fixedbuffer = calloc [?][?];
        //     int* fixedbuffer;

        // 
    }

    class Program
    {
        static void Main(string[] args)
        {
            // https://msdn.microsoft.com/en-us/library/2yd9wwz4.aspx

            // 1>script : error JSC1000: C : (corelib referenced?) implementation for System.Int32[,] not found - Void .ctor(Int32, Int32)

            var buffer_calloc = new int[4 * 4];

            buffer_calloc[2] = 2;


            //Error CS0030  Cannot convert type 'int[]' to 'int[*,*]'   TestDimensionalArray X:\jsc.svn\examples\c\Test\TestStructArrayField\TestDimensionalArray\TestDimensionalArray\Program.cs    24

            //var buffer_cast44 = (int[4,4])(object)buffer_calloc;
            var buffer_cast = (int[,])(object)buffer_calloc;

            // 1>   System.Int32[,] for Void Set(Int32, Int32, Int32) used at 
            buffer_cast[2, 2] = 22;
            var x22 = buffer_cast[2, 2];

            //int* numArray0;

            //numArray0 = calloc[?][?];

            var buffer = new int[4, 4, 4];

            buffer[3, 3, 3] = 333;

            var x33 = buffer[3, 3, 3];
        }
    }
}

//void main(char** args)
//{
//    int* numArray0;
//    int* numArray1;
//    int num2;
//    int* numArray3;
//    int num4;

//    numArray0 = (int*)malloc(sizeof(int) * 16);
//    numArray0[2] = 2;
//    numArray1 = ((int*)numArray0);
//    x[][] = y;
//    num2 = x[][];
//    numArray3 = calloc[?][?];
//    x[][] = y;
//    num4 = x[][];
//}


