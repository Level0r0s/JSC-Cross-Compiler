using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestNullArgument
{
    class Program
    {
        public virtual Program[] GetInvocationList()
        {
            //  return  (Program[])null;
            return null;
        }

        static void Main(Program e = null)
        {
        }

        [STAThread]
        public static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151019/ubuntuwebapplication

            // lets make it cast nulls.
            Main();

            // Z:\jsc.svn\examples\javascript\appengine\Test\TestAppEngineApplicationId\TestAppEngineApplicationId\ApplicationWebService.cs
        }


    }
}
