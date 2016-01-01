using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestFirefoxWebServiceField
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public VerifiableString MyLegalPersonCode = new VerifiableString { value = "guest" };
        // is firefox sending it?

        // 204 no content? no fields?
        public async Task<string> Login()
        {

            { var ref0 = typeof(NamedKeyPairs.WebServiceAuthorityPrivateKey); }


            //<h2> <i>Could not load file or assembly 'TestFirefoxWebServiceField.AssetsLibrary, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.</i> </h2></span>


            this.MyLegalPersonCode = new VerifiableString { value = "xxx" }.Sign(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters);

            Console.WriteLine("Login " + new { MyLegalPersonCode });

            // .field field_MyLegalPersonCode:<_02000013>%0d%0a  <_04000021>eHh4</_04000021>%0d%0a  <_04000022>I5FCZt058sQiF3nG9HzMHsegGdbe2qicSx/4ZXYPJEM+TFSVKol19jGJqNj05vMiAJBGtgpjMyHV2hzVhwCFqA==</_04000022>%0d%0a</_02000013>


            // Z:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\JavaScript\Remoting\InternalWebMethodRequest.cs
            return "not 204. got my fields firefox?";
        }


        public async Task<bool> Verify()
        {
            //-		InnerException	{"An error occurred while loading attribute 'ServiceContractAttribute' on type 'DigiDocServicePortType'.  Please see InnerException for more details."}	System.Exception {System.InvalidOperationException}
            //+		InnerException	{"Could not load file or assembly 'ScriptCoreLibA, Version=4.6.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.":"ScriptCoreLibA, Version=4.6.0.0, Culture=neutral, PublicKeyToken=null"}	System.Exception {System.IO.FileNotFoundException}



            // do we have a IP trace?
            // if the ip changes 

            var verify = MyLegalPersonCode.Verify(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters);

            Console.WriteLine("Verify " + new { MyLegalPersonCode, verify });

            return verify;
        }


    }
}
