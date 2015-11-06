using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClientVerifiedWebServiceAuthority;
using ClientVerifiedWebServiceAuthority.Design;
using ClientVerifiedWebServiceAuthority.HTML.Pages;
using System.Diagnostics;

namespace ClientVerifiedWebServiceAuthority
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        static Application()
        {
            // 1ms { currentScript = [object HTMLScriptElement] }

            Console.WriteLine(
                new { Native.document.currentScript }
            );
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new { }.With(
                async delegate
                {
                    new IHTMLPre { new { Native.document.currentScript } }.AttachToDocument();

                    var x = await base.GetData();

                    new IHTMLPre { new { Native.document.currentScript } }.AttachToDocument();

                    // IPublicKey . VerifyAsync()

                    var verificationKey = await Native.crypto.subtle.importRSAPublicKeyForVerificationAsync(
                        this.PublicKeyModulus,
                        this.PublicKeyExponent
                    );



                    new IHTMLPre { new { verificationKey } }.AttachToDocument();

                    //new System.Security.Cryptography.RSACryptoServiceProvider().e
                    var verified = await Native.crypto.subtle.verifyAsync(verificationKey, x.signature, Encoding.UTF8.GetBytes(x.value));

                    // at any point we can now verify the data from webservice is signed and valid.
                    new IHTMLPre { new { verified } }.AttachToDocument();

                    // http://caniuse.com/#feat=cryptography


                    //#region keyData
                    //var m64padding = Convert.ToBase64String(base.PublicKeyModulus);
                    //var m64 = m64padding;


                    //while (m64.EndsWith("=="))
                    //    m64 = m64.Substring(0, m64.Length - 2);


                    //// make URL friendly:
                    ////str = str.replace(/\+/g, '-').replace(/\//g, '_').replace(/\=+$/, '');

                    //// http://stackoverflow.com/questions/4492426/remove-trailing-when-base64-encoding
                    //m64 = m64.Replace("+", "-").Replace("/", "_").Replace("=+", "");


                    //var keyData = new
                    //{
                    //    //alg = "RS256",
                    //    alg = "RS1",
                    //    //alg = "RSA-OAEP",
                    //    e = Convert.ToBase64String(base.PublicKeyExponent),
                    //    ext = false,
                    //    kty = "RSA",
                    //    n = m64
                    //};
                    //#endregion

                    //// { keyData = { alg = RSA-OAEP, e = AQAB, ext = false, kty = RSA, n = s8uxpsmasavYjVmctC_FSnC98T-qHs8zBsuQF44015gF_jSkM8apog3SZquKXf_SyT8v-HMUSoyBuu1ztbAovQ } }


                    //// http://blog.engelke.com/2014/10/21/web-crypto-and-x-509-certificates/
                    ////  "1.2.840.113549.1.1.5" 

                    //// onError { z = DataError: The JWK "alg" member was inconsistent with that specified by the Web Crypto call }
                    //// https://github.com/diafygi/webcrypto-examples


                    //var algorithm = new
                    //{
                    //    name = "RSASSA-PKCS1-v1_5",
                    //    //name = "RSA-OAEP",
                    //    //hash = new { name = "SHA-256" },
                    //    hash = new { name = "SHA-1" }

                    //    //modulusLength = 2048,
                    //    //publicExponent,
                    //};

                    //var sw = Stopwatch.StartNew();


                    // //Native.crypto.subtle.importKey
                    //var p = Native.crypto.subtle.importKey(
                    //    format: "jwk",
                    //    keyData: keyData,
                    //    algorithm: algorithm,
                    //    extractable: false,
                    //    keyUsages: new[] { // "encrypt", 

                    //            // onError { z = SyntaxError: Cannot create a key using the specified key usages. }
                    //            "verify" 
                    //        }
                    //);

                    //p.then(
                    //    onSuccess: publicKey =>
                    //    {
                    //        // onSuccess {{ z = [object CryptoKey], ElapsedMilliseconds = 9278 }}


                    //        new IHTMLPre { "onSuccess " + new { publicKey, sw.ElapsedMilliseconds } }.AttachToDocument();

                    //        //new IHTMLButton { "encrypt for server" }.AttachToDocument().onclick +=
                    //        //async delegate
                    //        //{
                    //        //    // Man in the middle?
                    //        //    // layered security
                    //        //    var data = Encoding.UTF8.GetBytes("hello from client");
                    //        //    var esw = Stopwatch.StartNew();

                    //        //    var ebytes = await Native.crypto.subtle.encryptAsync(algorithm, z, data);
                    //        //    new IHTMLPre { "encryptAsync " + new { esw.ElapsedMilliseconds } }.AttachToDocument();

                    //        //    await UploadEncryptedString(
                    //        //        ebytes
                    //        //    );
                    //        //};

                    //        // onSuccess { z = [object CryptoKey], ElapsedMilliseconds = 0 }


                    //        Native.crypto.subtle.verify(
                    //             new
                    //             {
                    //                 name = "RSASSA-PKCS1-v1_5",
                    //             },
                    //            publicKey, //from generateKey or importKey above
                    //            x.signature, //ArrayBuffer of the signature
                    //            Encoding.UTF8.GetBytes(x.value) //ArrayBuffer of the data

                    //        ).then(

                    //        onSuccess: verified =>
                    //            {
                    //                new IHTMLPre {
                    //            "verify " +
                    //                new {
                    //               verified}
                    //            }.AttachToDocument();
                    //            }
                    //        );

                    //    },

                    //    onError: z =>
                    //    {
                    //        // onError { z = DataError: The JWK member "n" could not be base64url decoded or contained padding }

                    //        new IHTMLPre {
                    //            "onError " +
                    //                new {
                    //               z}
                    //            }.AttachToDocument();
                    //    }

                    //);


                    // onError { z = SyntaxError: Cannot create a key using the specified key usages. }


                    var o = x.value;
                    var t = new IHTMLTextArea { value = x, readOnly = true }.AttachToDocument();

                    t.style.whiteSpace = IStyle.WhiteSpaceEnum.nowrap;
                    t.style.width = "80em";
                    //t.style.right = "1em";
                    t.style.height = "20em";

                    var status = new IHTMLPre { }.AttachToDocument();

                    t.style.paddingLeft = "1em";
                    //t.style.borderLeft = "1em solid green";
                    t.readOnly = false;

                    ////while (await t.async.onchange)

                    do
                    {
                        t.style.borderLeft = "1em solid yellow";

                        // Z:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\TextBox.cs
                        x.value = t.value.Replace(Environment.NewLine, "\n").Replace("\n", Environment.NewLine);


                        verified = await Native.crypto.subtle.verifyAsync(verificationKey, x.signature, Encoding.UTF8.GetBytes(x.value));
                        //var verify = await base.Verify(x);
                        status.innerText = new { isoriginal = o == x.value, o = o.Length, t = x.value.Length, verified }.ToString();

                        if (verified)
                            t.style.borderLeft = "1em solid green";
                        else
                            t.style.borderLeft = "1em solid red";
                    }
                    while (await t.async.onkeyup);




                }
            );
        }

    }
}

//146c:01:01 0042:0012 ClientVerifiedWebServiceAuthority.Application create ScriptCoreLib.Ultra::ScriptCoreLib.JavaScript.Remoting.InternalWebMethodRequest+<>c__DisplayClassc
//146c:01:01 0046:0013 ClientVerifiedWebServiceAuthority.Application create ScriptCoreLib.Ultra::ScriptCoreLib.JavaScript.Remoting.InternalWebMethodRequest+<>c__DisplayClass1
//146c:01:01:0f RewriteToAssembly error: System.NotImplementedException: { SourceType = System.Security.Cryptography.RSAParameters }
//   at jsc.meta.Library.ILStringConversions.Prepare(Type SourceType, Func`2 FieldSelector) in x:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Library\ILStringConversions.cs:line 1557
//   at jsc.meta.Commands.Rewrite.RewriteToJavaScriptDocument.WebServiceForJavaScript.WriteGetFields(ILGenerator InvokeCallback_il, Action fthis, Action fWebRequest, Boolean AfterInternalFieldsFromTypeInitializerAndReadyForBinding) in x:\js