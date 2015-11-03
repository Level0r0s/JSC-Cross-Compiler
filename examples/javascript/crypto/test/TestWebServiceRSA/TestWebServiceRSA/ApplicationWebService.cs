using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestWebServiceRSA
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // SignedString ?
        // SecureString
        public string SpecialData;
        public byte[] SpecialDataSignature48;

        public ApplicationWebService()
        {
            var sw = Stopwatch.StartNew();
            Console.WriteLine("enter ApplicationWebService");

            { var copy = typeof(NamedKeyPairs.Key1PrivateKey); }


            this.SpecialData = "hello world";
            this.SpecialDataSignature48 = new RSACryptoStream(NamedKeyPairs.Key1PrivateKey.CSPBlob).SignString(this.SpecialData);
            Console.WriteLine("exit ApplicationWebService " + new { sw.ElapsedMilliseconds });
        }

        public async Task<bool> Verify()
        {
            var sw = Stopwatch.StartNew();
            Console.WriteLine("enter Verify");
            var v = new RSACryptoStream(NamedKeyPairs.Key1PrivateKey.CSPBlob).VerifyString(this.SpecialData, this.SpecialDataSignature48);
            Console.WriteLine("exit Verify " + new { sw.ElapsedMilliseconds });
            return v;
        }

        //{ SpecialData = hello world, sig = 41c4b11f1bdc0626b90696ebc86df6a839c4d5a0478bacbca292c97d9561c51912e1f7cb2e5161eaef47147eeee5f8a6, Verify = true }
        //{ SpecialData = HELLO WORLD, sig = 41c4b11f1bdc0626b90696ebc86df6a839c4d5a0478bacbca292c97d9561c51912e1f7cb2e5161eaef47147eeee5f8a6, Verify = false }
        //{ SpecialData = hello world, sig = 41c4b11f1bdc0626b90696ebc86df6a839c4d5a0478bacbca292c97d9561c51912e1f7cb2e5161eaef47147eeee5f8a6, Verify = true }
        //{ SpecialData = hello world, sig = ccc4b11f1bdc0626b90696ebc86df6a839c4d5a0478bacbca292c97d9561c51912e1f7cb2e5161eaef47147eeee5f8a6, Verify = false }

    }
}

//0200001f TestWebServiceRSA.Application+ctor>b__4>d__6+<MoveNext>0600002b
//no implementation for System.Tuple`4[ScriptCoreLib.JavaScript.DOM.HTML.IHTMLPre,System.Int64,System.String,System.String] f80c9c70-a297-37ce-b67d-52afe3b94259
//script: error JSC1000: No implementation found for this native method, please implement [System.Tuple`4.get_Item1()]
//script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
//script: error JSC1000: error at TestWebServiceRSA.Application+ctor>b__4>d__6+<MoveNext>0600002b.<00de> ldarg.0.try,
// assembly: T:\TestWebServiceRSA.Application.exe
// type: TestWebServiceRSA.Application+ctor>b__4>d__6+<MoveNext>0600002b, TestWebServiceRSA.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// offset: 0x001a
//  method:Int32 <00de> ldarg.0.try(<MoveNext>0600002b, ctor>b__4>d__6 ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[System.Boolean] ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[System.Boolean] ByRef)
//*** Compiler cannot continue... press enter to quit.



