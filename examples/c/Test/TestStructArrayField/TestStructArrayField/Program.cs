using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestStructArrayField
{
    public struct ovrMessageParms
    {
        // union?

        public object Pointer;
        public int Integer;
        public float Float;


    }

    class Program
    {
        public ovrMessageParms[] Parms;

        public void ovrMessage_SetPointerParm(int i, object value)
        {
            Parms[i].Pointer = value;
        }

        public object ovrMessage_GetPointerParm(int i)
        {
            //var p = (size_t*)Parms[i];
            //return (object)(*p);
            return Parms[i].Pointer;
        }

        public void ovrMessageQueue_PostMessage(int i, ref ovrMessageParms message)
        {
            // (&(__that->Parms[i])) = message[0];

            Parms[i] = message;

        }

        public void ovrMessageQueue_GetNextMessage(int i, out ovrMessageParms message)
        {
            message = this.Parms[i];
        }

        static void Main(string[] args)
        {
        }
    }
}

//// instance TestStructArrayField.Program.ovrMessage_SetPointerParm
//void TestStructArrayField_Program_ovrMessage_SetPointerParm(LPTestStructArrayField_Program __that, int i, void* value)
//{
//     = value;
//}

//// instance TestStructArrayField.Program.ovrMessage_GetPointerParm
//void* TestStructArrayField_Program_ovrMessage_GetPointerParm(LPTestStructArrayField_Program __that, int i)
//{
//    return  __that->Parms[i]->Pointer;
//}

