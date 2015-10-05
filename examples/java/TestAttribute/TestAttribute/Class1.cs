using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace OtherNamespace
{
    [Script]
    public class FooAttribute : Attribute
    {
        public string Text;
    }

}

namespace TestAttribute
{
    //@Documented
    //@__MessageContract(WrapperName = "MobileAuthenticate", WrapperNamespace = "http://www.sk.ee/DigiDocService/DigiDocService_2_3.wsdl", IsWrapped = true)
    //@Foo(Text = "hello world")
    //public class MobileAuthenticateRequest implements IAssemblyReferenceToken

    [OtherNamespace.Foo(Text = "hello world")]
    [java.lang.annotation.DocumentedAttribute()]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "MobileAuthenticate", WrapperNamespace = "http://www.sk.ee/DigiDocService/DigiDocService_2_3.wsdl", IsWrapped = true)]
    [__MessageContractAttribute(WrapperName = "__MobileAuthenticate", WrapperNamespace = "http://www.sk.ee/DigiDocService/DigiDocService_2_3.wsdl", IsWrapped = true)]
    public partial class MobileAuthenticateRequest : ScriptCoreLibJava.IAssemblyReferenceToken
    {

    }
}
