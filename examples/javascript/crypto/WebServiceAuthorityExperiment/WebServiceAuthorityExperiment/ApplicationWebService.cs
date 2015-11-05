using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebServiceAuthorityExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {

        public async Task<bool> Verify(VerifiableString x)
        {
            var verify = x.Verify(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters);

            //Console.WriteLine(
            //    new { isoriginal = o == x.value, o = o.Length, t = x.value.Length, verify }.ToString()
            //    );

            return verify;
        }

        static string o;

        public async Task<VerifiableString> GetData()
        {
            Console.WriteLine("enter GetData");
            o = @"

web service claims you have 5 dollars. 

claim signed by " + typeof(NamedKeyPairs.WebServiceAuthorityPrivateKey).Name + @". 

---
"
                + new { Environment.StackTrace }
               ;






            o = o.Replace(Environment.NewLine, "\n").Replace("\n", Environment.NewLine);

            var x = new VerifiableString
            {
                value = o

                // if we can verify it later, we can trust it to be set by the web service. otherwise we cannot trust it.
                // this would also enable state sharing now.
                // signed and perhaps encrypted too..
            }.Sign(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters);

            Verify(x);

            return x;
        }


        //Additional information: Could not load file or assembly 'WebServiceAuthorityExperiment.AssetsLibrary, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.

    }
}

//{ isoriginal = false, o = 4568, t = 4609, verify = false }