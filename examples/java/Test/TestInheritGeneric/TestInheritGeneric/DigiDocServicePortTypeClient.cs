using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestInheritGeneric
{
    //1>   Implementation not found for type import : 
    //1>   type: System.ServiceModel.ClientBase`1[[TestInheritGeneric.DigiDocServicePortType, TestInheritGeneric, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
    //1>   method: Void .ctor()
    //1>   Did you forget to add the [Script] attribute? 
    //1>   Please double check the signature! 
    //1>   


    //partial class DigiDocServicePortTypeClient : System.ServiceModel.ClientBase<DigiDocServicePortType>, IDigiDocServicePortType, ScriptCoreLibJava.IAssemblyReferenceToken
    partial class DigiDocServicePortTypeClient : System.ServiceModel.ClientBase<object>, ScriptCoreLibJava.IAssemblyReferenceToken
    //partial class DigiDocServicePortTypeClient : __ClientBase<DigiDocServicePortType>, IDigiDocServicePortType
    {

        // public class DigiDocServicePortTypeClient extends __ClientBase_1<TChannel> implements IDigiDocServicePortType, IAssemblyReferenceToken
        // public class DigiDocServicePortTypeClient extends TestInheritGeneric.__ClientBase_1<DigiDocServicePortType> implements IDigiDocServicePortType

    }

    //public class __ClientBase<TChannel>
    //{
    //}
}
