using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestIsinst
{
    public class Class1
    {
        //public static void Invoke3(object e)
        //{
        //    var isClass1 = e is Class1;
        //    if (isClass1)
        //        InvokeClass1((Class1)e);

        //}


        //public static void Invoke2(object e)
        //{
        //    var isInt = e is int;
        //    if (isInt)
        //        InvokeInteger((int)e);

        //}

        public static void Invoke(object e)
        {
            if (e is int)
                InvokeInteger((int)e);

        }

        //if (((((Object)e) instanceof  Integer) ? (Integer)((Object)e) : (Integer)null)!=null)
        // {
        //     Class1.InvokeInteger(((Integer)e).intValue());
        // }



        public static void InvokeInteger(int e)
        {
        }

        public static void InvokeClass1(Class1 e)
        {
        }
    }
}
