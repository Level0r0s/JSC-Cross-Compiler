using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.BCLImplementation.System.ServiceModel;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TestMessageContractAttribute
{
    // { item = @_module_.SHA162f3f57cbcab2562516fc3940f865e63e73fbac7_597372619_00000018_0000000c() }

    //:\staging\web\java\TestMessageContractAttribute\MobileAuthenticateRequest.java:8: error: cannot find symbol
    //__MessageContract(WrapperName = "MobileAuthenticate", WrapperNamespace = "http://www.sk.ee/DigiDocService/Di
    //^

    [System.ServiceModel.MessageContractAttribute(WrapperName = "MobileAuthenticate", WrapperNamespace = "http://www.sk.ee/DigiDocService/DigiDocService_2_3.wsdl", IsWrapped = true)]


    //  error: duplicate annotation
    //[__MessageContractAttribute(WrapperName = "__MobileAuthenticate", WrapperNamespace = "http://www.sk.ee/DigiDocService/DigiDocService_2_3.wsdl", IsWrapped = true)]
    public partial class MobileAuthenticateRequest
    {

    }


    static class Program
    {
        // this still works! :D

        [STAThread]
        public static void Main(string[] args)
        {

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );


            var a = new MobileAuthenticateRequest { };
            var t = a.GetType();

            {
                var c = t.ToClass();

                var aa = c.getAnnotations();

                // { item = @ScriptCoreLibJava.BCLImplementation.System.ServiceModel.__MessageContract(WrapperName=MobileAuthenticate, WrapperNamespace=http://www.sk.ee/DigiDocService/DigiDocService_2_3.wsdl, IsWrapped=true) }

                foreach (var item in aa)
                {
                    Console.WriteLine(new { item });



                }

                // { item = @ScriptCoreLibJava.BCLImplementation.System.ServiceModel.__MessageContract(WrapperName=MobileAuthenticate, WrapperNamespace=http://www.sk.ee/DigiDocService/DigiDocService_2_3.wsdl, IsWrapped=true) }

            }

            {
                var aa = t.GetCustomAttributes(false);

                foreach (var xCustomAttribute in aa)
                {
                    //{ xCustomAttribute = @_module_.SHA1565471238dd20a9db7dfe7689f8c721bd4b56878_499320750_00000018_0000000c(), xMessageContractAttribute =  }
                    //{ xCustomAttribute = @ScriptCoreLibJava.BCLImplementation.System.ServiceModel.__MessageContract(WrapperName=MobileAuthenticate, WrapperNamespace=http://www.sk.ee/DigiDocService/DigiDocService_2_3.wsdl, IsWrapped=true), xMessageContractAttribute =  }

                    var xMessageContractAttribute = xCustomAttribute as System.ServiceModel.MessageContractAttribute;
                    Console.WriteLine(new { xCustomAttribute, xMessageContractAttribute });

                    if (xMessageContractAttribute != null)
                    {
                        Console.WriteLine(new { xMessageContractAttribute.WrapperNamespace });
                    }
                }
            }

            CLRProgram.CLRMain();
        }


    }


    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            MessageBox.Show("click to close");

        }
    }


}
