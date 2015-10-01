using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.ServiceModel.Channels;

namespace ScriptCoreLibJava.BCLImplementation.System.ServiceModel
{
    // C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.ServiceModel.dll
    // 

    //[Script(Implements = typeof(global::System.ServiceModel.ClientBase))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.ServiceModel.ClientBase`1")]
    public class __ClientBase<TChannel>
    {
        // Z:\jsc.svn\examples\java\Test\TestInheritGeneric\TestInheritGeneric\DigiDocServicePortTypeClient.cs

        // public abstract class ClientBase<TChannel> : ICommunicationObject, IDisposable where TChannel : class



        // Z:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\AsyncLocal.cs

        // Z:\jsc.svn\examples\java\hybrid\JVMCLRWSDLMID\Program.cs


        public TChannel Channel
        {
            get
            {
                return default(TChannel);
            }
        }

        public __ClientBase()
        {
            Console.WriteLine(" System.ServiceModel.ClientBase`1 .ctor ");
        }

        public __ClientBase(string endpointConfigurationName)
        {

            Console.WriteLine(" System.ServiceModel.ClientBase`1 .ctor " + new { endpointConfigurationName });
        }

        public __ClientBase(string endpointConfigurationName, string remoteAddress)
        {
            // https://msdn.microsoft.com/en-us/library/ms574922(v=vs.110).aspx

            Console.WriteLine(" System.ServiceModel.ClientBase`1 .ctor " + new { endpointConfigurationName, remoteAddress });
        }

        public __ClientBase(string endpointConfigurationName, __EndpointAddress remoteAddress)
        {
            // https://msdn.microsoft.com/en-us/library/ms574922(v=vs.110).aspx

            Console.WriteLine(" System.ServiceModel.ClientBase`1 .ctor " + new { endpointConfigurationName, remoteAddress });
        }

        public __ClientBase(__Binding endpointConfigurationName, __EndpointAddress remoteAddress)
        {
            // https://msdn.microsoft.com/en-us/library/ms574922(v=vs.110).aspx

            Console.WriteLine(" System.ServiceModel.ClientBase`1 .ctor " + new { endpointConfigurationName, remoteAddress });
        }
    }
}
