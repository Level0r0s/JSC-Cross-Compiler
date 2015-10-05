using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.lang.annotation;

namespace ScriptCoreLibJava.BCLImplementation.System.ServiceModel
{
    // https://msdn.microsoft.com/en-us/library/system.servicemodel.messagecontractattribute(v=vs.110).aspx
    // C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.ServiceModel.dll

    [Script(ImplementsViaAssemblyQualifiedName = "System.ServiceModel.ServiceContractAttribute")]
    //public class __MessageContractAttribute  : ScriptCoreLib.Shared.BCLImplementation.System.__Attribute
    public class __ServiceContractAttribute  : Attribute
    {
        public string Namespace { get; set; }

        public string ConfigurationName { get; set; }
    }

   
}
