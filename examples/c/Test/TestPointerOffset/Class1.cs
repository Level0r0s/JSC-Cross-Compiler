using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestPointerOffset
{
    public unsafe class Class1
    {
        public static byte* AddOffset(byte* x, int offset)
        {
            // byte_7 = ((unsigned char*)(&(byte_4[1])));

            //  return  (unsigned char*)(x + offset);
            return x + offset;
        }

        public static byte* AddOffset(byte* x)
        {
            //   return  (unsigned char*)(&(x[1]));
            const int offset = 4;

            // byte_7 = ((unsigned char*)(&(byte_4[1])));

            //  return  (unsigned char*)(x + offset);
            return x + offset;
        }

    }
}
