using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.ServiceModel.Channels;
using ScriptCoreLibJava.Extensions;
using ScriptCoreLibJava.BCLImplementation.System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net;
using ScriptCoreLibJava.BCLImplementation.System.Net;

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


            //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\ServiceModel\__ClientBaseGetChannel.java:57: error: illegal generic type for instanceof
            //        base_14 = ((((Object)that) instanceof  ScriptCoreLibJava.BCLImplementation.System.ServiceModel.__ClientBase_1<Object>) ? (ScriptCoreLibJava.BCLI

            //var xClientBase = that as __ClientBase<object>;
            var xClientBase = that as __ClientBase__remoteAddress;
            // , xClientBase = __ClientBase { __remoteAddress = https://tsp.demo.sk.ee:443 } }

            Console.WriteLine("enter __ClientBase Channel " + new { xType, xTypeBase, xMe, xTypeInterface, xClientBase });


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

                    var xMessageContractAttribute = default(__MessageContractAttribute);

                    foreach (var xCustomAttribute in arg1valueType.GetCustomAttributes(false))
                    {
                        var locMessageContractAttribute = xCustomAttribute as __MessageContractAttribute;
                        if (locMessageContractAttribute != null)
                        {
                            xMessageContractAttribute = locMessageContractAttribute;

                            Console.WriteLine(new { xMessageContractAttribute.WrapperName, xMessageContractAttribute.WrapperNamespace });
                        }
                    }



                    #region Envelope
                    var data = new StringBuilder();


                    data.Append(@"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">");
                    data.Append(@"<s:Body s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">");


                    data.Append(@"<q1:" + xMessageContractAttribute.WrapperName + @" xmlns:q1=""" + xMessageContractAttribute.WrapperNamespace + @""">");

                    // <q1:MobileAuthenticate xmlns:q1=""http://www.sk.ee/DigiDocService/DigiDocService_2_3.wsdl"">

                    foreach (var field in arg1valueType.GetFields())
                    {
                        var fieldvalue = field.GetValue(arg1value);

                        Console.WriteLine(new { field.Name, field.FieldType, fieldvalue });

                        //var x = new XElement(field.Name, fieldvalue).ToString();
                        var x = new XElement(field.Name, "" + fieldvalue).ToString();

                        if (field.FieldType == typeof(string))
                        {
                            x = x.Replace(@"<" + field.Name, @"<" + field.Name + @" xsi:type=""xsd:string""");
                        }
                        else if (field.FieldType == typeof(int))
                        {
                            x = x.Replace(@"<" + field.Name, @"<" + field.Name + @" xsi:type=""xsd:int""");
                        }
                        else if (field.FieldType == typeof(bool))
                        {
                            x = x.Replace(@"<" + field.Name, @"<" + field.Name + @" xsi:type=""xsd:boolean""");
                        }

                        data.Append(x);
                    }

                    data.Append(@"</q1:" + xMessageContractAttribute.WrapperName + @">");

                    data.Append(@"</s:Body>");
                    data.Append(@"</s:Envelope>");
                    #endregion



                    // <s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/""><s:Body s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">

                    // <q1:MobileAuthenticate xmlns:q1=""http://www.sk.ee/DigiDocService/DigiDocService_2_3.wsdl"">

                    // <IDCode xsi:type=""xsd:string"">14212128025</IDCode>
                    // <CountryCode xsi:type=""xsd:string"">EE</CountryCode>
                    // <PhoneNo xsi:type=""xsd:string"">37200007</PhoneNo>
                    // <Language xsi:type=""xsd:string"">EST</Language>
                    // <ServiceName xsi:type=""xsd:string"">Testimine</ServiceName>
                    // <MessageToDisplay xsi:type=""xsd:string"">Testimine</MessageToDisplay>
                    // <SPChallenge xsi:type=""xsd:string"">03010400000000000000</SPChallenge>
                    // <MessagingMode xsi:type=""xsd:string"">asynchClientServer</MessagingMode>
                    // <AsyncConfiguration xsi:type=""xsd:int"">0</AsyncConfiguration>
                    // <ReturnCertData xsi:type=""xsd:boolean"">true</ReturnCertData>
                    //<ReturnRevocationData xsi:type=""xsd:boolean"">false</ReturnRevocationData>
                    // </q1:MobileAuthenticate>

                    // </s:Body></s:Envelope>

                    Console.WriteLine(new { data });


                    //{ WrapperName = MobileAuthenticate, WrapperNamespace = http://www.sk.ee/DigiDocService/DigiDocService_2_3.wsdl }
                    //{ Name = IDCode, fieldvalue = 14212128025 }
                    //{ Name = CountryCode, fieldvalue = EE }
                    //{ Name = PhoneNo, fieldvalue = 37200007 }
                    //{ Name = Language, fieldvalue = EST }
                    //{ Name = ServiceName, fieldvalue = Testimine }
                    //{ Name = MessageToDisplay, fieldvalue = Testimine }
                    //{ Name = SPChallenge, fieldvalue = 03010400000000000000 }
                    //{ Name = MessagingMode, fieldvalue = asynchClientServer }
                    //{ Name = AsyncConfiguration, fieldvalue = 0 }
                    //{ Name = ReturnCertData, fieldvalue = true }
                    //{ Name = ReturnRevocationData, fieldvalue = false }

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



                    var xPOST = new __WebClient();

                    xPOST.Headers["Content-Type"] = "text/xml; charset=UTF-8";
                    xPOST.Headers["SOAPAction"] = "\"\"";


                    var xPOSTresponse = xPOST.UploadStringTaskAsync(
                        xClientBase.__remoteAddress.uri,
                        "POST",
                        data.ToString()
                    );


                    xPOSTresponse.ContinueWith(
                        task =>
                        {
                            if (string.IsNullOrEmpty(task.Result))
                            {
                                // fault?


                                c.SetResult(
                                  null
                                );

                                return;
                            }

                            var Envelope = XElement.Parse(task.Result);
                            var Body = Envelope.Elements().Skip(1).FirstOrDefault();
                            var Response = Body.Elements().FirstOrDefault();
                            var fields = Response.Elements();


                            var Result = Activator.CreateInstance(xResponseType);

                            var ResultFields = xResponseType.GetFields(
                                global::System.Reflection.BindingFlags.DeclaredOnly | global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public);

                            foreach (var ResultField in ResultFields)
                            {
                                var ResultValueElement = fields.FirstOrDefault(x => x.Name.LocalName == ResultField.Name);
                                if (ResultValueElement == null)
                                    Console.WriteLine(new { ResultField.Name, ResultField.FieldType });
                                else
                                {
                                    // { Name = Sesscode, FieldType = int, IsInt = false, Value = 158266114 }
                                    var IsInt = ResultField.FieldType == typeof(int);
                                    if (IsInt)
                                    {
                                        ResultField.SetValue(Result, int.Parse(ResultValueElement.Value));
                                    }
                                    else if (ResultField.FieldType == typeof(string))
                                    {
                                        ResultField.SetValue(Result, ResultValueElement.Value);
                                    }

                                    Console.WriteLine(new { ResultField.Name, ResultField.FieldType, IsInt, ResultValueElement.Value });
                                }


                            }

                            c.SetResult(
                              Result
                            );
                        }
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

    [Script]
    public interface __ClientBase__remoteAddress
    {
        __EndpointAddress __remoteAddress { get; set; }
    }

    //[Script(Implements = typeof(global::System.ServiceModel.ClientBase))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.ServiceModel.ClientBase`1")]
    public class __ClientBase<TChannel> : __ClientBase__remoteAddress
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

        //   var c = new sk.DigiDocServicePortTypeClient("DigiDocService", "https://tsp.demo.sk.ee:443");

        public string __endpointConfigurationName;

        // __ClientBase__remoteAddress
        public __EndpointAddress __remoteAddress { get; set; }

        public override string ToString()
        {
            return "__ClientBase " + new { __remoteAddress };

        }

        public __ClientBase(string endpointConfigurationName, string remoteAddress)
        {
            // https://msdn.microsoft.com/en-us/library/ms574922(v=vs.110).aspx

            //Console.WriteLine(" System.ServiceModel.ClientBase`1 .ctor " + new { endpointConfigurationName, remoteAddress });

            this.__endpointConfigurationName = endpointConfigurationName;
            this.__remoteAddress = new __EndpointAddress(remoteAddress);
        }

        public __ClientBase(string endpointConfigurationName, __EndpointAddress remoteAddress)
        {
            // https://msdn.microsoft.com/en-us/library/ms574922(v=vs.110).aspx

            Console.WriteLine(" System.ServiceModel.ClientBase`1 .ctor " + new { endpointConfigurationName, remoteAddress });
        }

        public __ClientBase(__Binding binding, __EndpointAddress remoteAddress)
        {
            // Z:\jsc.svn\examples\javascript\ubuntu\UbuntuMIDExperiment\Application.cs

            // https://msdn.microsoft.com/en-us/library/ms574922(v=vs.110).aspx

            this.__remoteAddress = remoteAddress;

            //Console.WriteLine(" System.ServiceModel.ClientBase`1 .ctor " + new { endpointConfigurationName, remoteAddress });
        }
    }
}
