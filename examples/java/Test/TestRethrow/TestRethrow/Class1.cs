using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestRethrow
{
    public class Class2
    {
        [ScriptCoreLib.ScriptMethodThrows(typeof(object))]
        public Class2()
        {

        }
    }

    public class Class1 : Class2
    {
//Y:\staging\web\java\TestUbuntuMySQLInsert\xDriver.java:16: error: call to super must be first statement in constructor
//        try { super(); } catch (java.lang.Throwable __exc) { throw new RuntimeException(__exc.getMessage() , __exc); } ;

        public static void InvokeSuper() { }
        public static void Invoke()
        {
            try
            {
                InvokeSuper();
            }
            catch
            {
                throw;
            }
        }
    }
}

   //try
   //     {
   //         Class1.InvokeSuper();
   //     }
   //     catch (java.lang.Throwable __exc)
   //     {
   //         throw new RuntimeException(__exc.getMessage() , __exc);
   //     }