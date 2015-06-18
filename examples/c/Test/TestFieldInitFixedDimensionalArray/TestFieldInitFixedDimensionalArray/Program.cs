﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestFieldInitFixedDimensionalArray
{
#if future
    unsafe struct whatWeReallyWant
    {
        fixed long data[6, 6, 6];

        // C# 6
        //Error CS7092  A fixed buffer may only have one dimension.TestFieldInitFixedDimensionalArray  X:\jsc.svn\examples\c\Test\TestFieldInitFixedDimensionalArray\TestFieldInitFixedDimensionalArray\Program.cs 11
    }
#endif

    unsafe struct almostWhatWeWant
    {
        //  long long fixed1[4];
        public fixed long fixed1[4];


        public int other;
    }



    unsafe class halfWayThere
    {
        //Error CS1642  Fixed size buffer fields may only be members of structs TestFieldInitFixedDimensionalArray X:\jsc.svn\examples\c\Test\TestFieldInitFixedDimensionalArray\TestFieldInitFixedDimensionalArray\Program.cs	24

        //     long long* data;

        //public readonly long[,,] data = new long[6, 6, 6];

        // um if we bake it, data cannot ever be null!
        //public readonly long[,,] data = null;
        //public readonly long[] data1 = null;

        // now it cannot be ever null!

        //  long long* data1;
        public readonly long[] fixed1 = new long[4];
        public readonly long[,] fixed2 = new long[4, 4];

        //  TestFieldInitFixedDimensionalArray_almostWhatWeWant data[4][4];
        public readonly almostWhatWeWant[,] data = new almostWhatWeWant[4, 4];

        public int other;


        // long long fixed2[4][4];

        //  long long* fixed2;


        //   long long data1[4];

        //    __that->data = NULL;
        //__that->data = calloc[?][?];


        // TestFieldInitFixedDimensionalArray.exe.c(13) : warning C4047: '=' : 'tag_TestFieldInitFixedDimensionalArray_halfWayThere *' differs in levels of indirection from 'int'
        //  __that->next = NULL;
        // struct tag_TestFieldInitFixedDimensionalArray_halfWayThere* next;
        public halfWayThere next;
    }

    class Program : ScriptCoreLibNative.IAssemblyReferenceToken
    {
        static void Main(string[] args)
        {
            ScriptCoreLibNative.SystemHeaders.stdio_h.puts("enter");

            Console.WriteLine("hi bakery!");

            // sizeof in C?

            var halfWayThere = new halfWayThere { other = 2 };

            halfWayThere.next = new halfWayThere { };

            //  C : Opcode not implemented: stelem.i8 at TestFieldInitFixedDimensionalArray.Program.Main
            halfWayThere.fixed1[0] = 1;
            var x = halfWayThere.fixed1[0];
            //var x44 = halfWayThere.fixed1[44];

            // http://stackoverflow.com/questions/382993/why-do-compilers-not-warn-about-out-of-bounds-static-array-indices

            // TestFieldInitFixedDimensionalArray.exe.c(12) : error C2106: '=' : left operand must be l-value


            //   x[][] = y;
            halfWayThere.fixed2[2, 2] = 22;

            //  there0->fixed2[2][2] = ((signed long)(22));

            var x22 = halfWayThere.fixed2[2, 2];

            var fc = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("x22: ");
            Console.Write(x22);
            Console.WriteLine(";");
            Console.ForegroundColor = fc;

            //num2 = there0->fixed2[2][2];


            //halfWayThere.data[1, 1].fixed1[1] = 1;

            // TestFieldInitFixedDimensionalArray_almostWhatWeWant*_Address(there0->data, 1, 1)->other = 1;
            halfWayThere.data[1, 1].other = halfWayThere.other;

            Console.WriteLine("bye!");
        }
    }
}

//TestFieldInitFixedDimensionalArray.exe.c
//TestFieldInitFixedDimensionalArray.exe.c(9) : error C2065: 'NULL' : undeclared identifier
//TestFieldInitFixedDimensionalArray.exe.c(9) : warning C4047: '=' : '__int64 *' differs in levels of indirection from 'int'
//TestFieldInitFixedDimensionalArray.exe.c(10) : error C2065: 'calloc' : undeclared identifier
//TestFieldInitFixedDimensionalArray.exe.c(10) : error C2059: syntax error : '?'
//done

//TestFieldInitFixedDimensionalArray.exe.c(874) : error C2036: 'void *' : unknown size
//TestFieldInitFixedDimensionalArray.exe.c(874) : error C2069: cast of 'void' term to non-'void'
//TestFieldInitFixedDimensionalArray.exe.c(874) : warning C4047: '=' : 'unsigned char *' differs in levels of indirection from 'unsigned char'
//TestFieldInitFixedDimensionalArray.exe.c(874) : error C2106: '=' : left operand must be l-value
//TestFieldInitFixedDimensionalArray.exe.c(881) : warning C4022: 'ScriptCoreLibNative_BCLImplementation_System___Func_5_Invoke' : pointer mismatch for actual parameter 4
//TestFieldInitFixedDimensionalArray.exe.c(881) : warning C4022: 'ScriptCoreLibNative_BCLImplementation_System___Func_5_Invoke' : pointer mismatch for actual parameter 5
//TestFieldInitFixedDimensionalArray.exe.c(881) : warning C4047: 'return' : 'int' differs in levels of indirection from 'void *'
//done

//X:\jsc.svn\examples\c\Test\TestFieldInitFixedDimensionalArray\TestFieldInitFixedDimensionalArray\bin\Debug\web>TestFieldInitFixedDimensionalArray.exe
//enter
//hi bakery!
//bye!