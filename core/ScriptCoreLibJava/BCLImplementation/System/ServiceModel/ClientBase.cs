using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.ServiceModel.Channels;
using ScriptCoreLibJava.Extensions;
using ScriptCoreLibJava.BCLImplementation.System.Reflection;
using System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.ServiceModel
{
    // C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.ServiceModel.dll
    // 


    [Script]
    static class __ClientBaseGetChannel
    {

        [Script]
        class xHandler : java.lang.reflect.InvocationHandler
        {
            public Func<object, java.lang.reflect.Method, object[], object> vinvoke;
            public object invoke(object proxy, java.lang.reflect.Method method, object[] args)
            {
                return vinvoke(proxy, method, args);
            }
        }

        public static object Invoke(object that)
        {
            var xType = that.GetType();

            var xTypeBase = xType.BaseType;
            var xMe = typeof(__ClientBase<>);


            // enter __ClientBase Channel { xType = JVMCLRWSDLMID.sk.DigiDocServicePortTypeClient }
            // enter __ClientBase Channel { xType = JVMCLRWSDLMID.sk.DigiDocServicePortTypeClient, xTypeBase = ScriptCoreLibJava.BCLImplementation.System.ServiceModel.__ClientBase_1, xMe = ScriptCoreLibJava.BCLImplementation.System.ServiceModel.__ClientBase_1 }
            // enter __ClientBase Channel { xType = JVMCLRWSDLMID.sk.DigiDocServicePortTypeClient, xTypeBase = ScriptCoreLibJava.BCLImplementation.System.ServiceModel.__ClientBase_1, xMe = ScriptCoreLibJava.BCLImplementation.System.ServiceModel.__ClientBase_1, xTypeInterface = JVMCLRWSDLMID.sk.DigiDocServicePortType }

            // magic. we just inferred what TChannel is!

            var xTypeInterface = xType.GetInterfaces().FirstOrDefault();

            Console.WriteLine("enter __ClientBase Channel " + new { xType, xTypeBase, xMe, xTypeInterface });


            // return __ClientBase_1<TChannel>._get_Channel_b__1(proxy, method, args);

            var invocationHandler = new xHandler
            {
                vinvoke = (proxy, method, args) =>
                {
                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151005

                    var xMethod = new __MethodInfo { InternalMethod = method };


                    //   at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable.FirstOrDefault(__Enumerable.java:2016)
                    var arg1 = xMethod.GetParameters().FirstOrDefault();
                    var arg1value = args.FirstOrDefault();
                    var arg1valueType = args.FirstOrDefault().GetType();




                    //Console.WriteLine("enter Proxy invocationHandler " + new { method = method.getName() });
                    //Console.WriteLine("enter Proxy invocationHandler " + new { xMethod.Name, xMethod.ReturnType, proxy, args });
                    Console.WriteLine("enter Proxy invocationHandler " + new { xMethod.Name, xMethod.ReturnType, arg1, arg1value, arg1valueType });

                    // which wsdl is in use?

                    foreach (var xCustomAttributesData in arg1valueType.GetCustomAttributesData())
                    {
                        Console.WriteLine(new { xCustomAttributesData });
                    }


                    foreach (var field in arg1valueType.GetFields())
                    {
                        var fieldvalue = field.GetValue(arg1value);

                        Console.WriteLine(new { field.Name, fieldvalue });

                    }


                    // arg1 = ScriptCoreLibJava.BCLImplementation.System.Reflection.__ParameterInfo@1bb9a58, args = [Ljava.lang.Object;@1922f46 }
                    // enter Proxy invocationHandler { Name = JVMCLRWSDLMID_sk_DigiDocServicePortType_MobileAuthenticateAsync, ReturnType = ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks.__Task_1, 
                    // arg1 = ParameterInfo { Name = arg0, ParameterType = JVMCLRWSDLMID.sk.MobileAuthenticateRequest }, arg1value = JVMCLRWSDLMID.sk.MobileAuthenticateRequest@770d2e }


                    // JVMCLRWSDLMID.sk.MobileAuthenticateRequest
                    //System.Threading.Tasks.Task<JVMCLRWSDLMID.sk.MobileAuthenticateResponse> MobileAuthenticateAsync(JVMCLRWSDLMID.sk.MobileAuthenticateRequest request);

                    var xResponseTypeName = arg1.ParameterType.FullName.Replace("Request", "Response");
                    var xResponseType = Type.GetType(xResponseTypeName);

                    Console.WriteLine(new { xResponseType });
                    // { xResponseType = JVMCLRWSDLMID.sk.MobileAuthenticateResponse }


                    // lets assume this was async call and we also know what the response will be.

                    var c = new TaskCompletionSource<object>();

                    c.SetResult(
                        Activator.CreateInstance(xResponseType)
                    );


                    //                    enter Proxy invocationHandler { method = JVMCLRWSDLMID_sk_DigiDocServicePortType_MobileAuthenticateAsync }
                    //{ Message = , StackTrace = java.lang.NullPointerException
                    //        at JVMCLRWSDLMID.Program.main(Program.java:55)

                    // enter Proxy invocationHandler { Name = JVMCLRWSDLMID_sk_DigiDocServicePortType_MobileAuthenticateAsync, ReturnType = ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks.__Task_1 }

                    // task of what?

                    //var value = Activator.CreateInstance(xMethod.ReturnType);

                    return c.Task;

                    //return null;
                }
            };


            // how would this work from js?
            var p = java.lang.reflect.Proxy.newProxyInstance(
                    xTypeInterface.ToClass().getClassLoader(),

                    interfaces: new[] { xTypeInterface.ToClass() },
                    invocationHandler: invocationHandler
            );

            return p;
        }
    }

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
                // http://stackoverflow.com/questions/1082850/java-reflection-create-an-implementing-class
                //  public partial class DigiDocServicePortTypeClient : System.ServiceModel.ClientBase<JVMCLRWSDLMID.sk.DigiDocServicePortType>, JVMCLRWSDLMID.sk.DigiDocServicePortType {




                return (TChannel)__ClientBaseGetChannel.Invoke(this);
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
