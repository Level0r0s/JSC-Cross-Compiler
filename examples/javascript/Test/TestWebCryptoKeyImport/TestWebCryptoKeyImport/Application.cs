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
using TestWebCryptoKeyImport;
using TestWebCryptoKeyImport.Design;
using TestWebCryptoKeyImport.HTML.Pages;
using System.Diagnostics;

namespace TestWebCryptoKeyImport
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151105

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoSHA1\TestWebCryptoSHA1\Application.cs



            var sw = Stopwatch.StartNew();


            new IHTMLPre {

                new {
                m = this.m.Length, sw.ElapsedMilliseconds }
            }.AttachToDocument();

            // {{ m = 256 }}

            var algorithm = new
            {
                name = "RSA-OAEP",
                //hash = new { name = "SHA-256" },
                hash = new { name = "SHA-1" },

                //modulusLength = 2048,
                //publicExponent,
            };


            var m64padding = Convert.ToBase64String(this.m);
            var m64 = m64padding;


            while (m64.EndsWith("=="))
                m64 = m64.Substring(0, m64.Length - 2);


            // make URL friendly:
            //str = str.replace(/\+/g, '-').replace(/\//g, '_').replace(/\=+$/, '');

            // http://stackoverflow.com/questions/4492426/remove-trailing-when-base64-encoding
            m64 = m64.Replace("+", "-").Replace("/", "_").Replace("=+", "");


            var keyData = new
            {
                alg = "RSA-OAEP",
                e = Convert.ToBase64String(this.e),
                ext = false,
                kty = "RSA",
                n = m64
            };

            //   // Base64 padding was added to the "d" member (==).

            new IHTMLPre { 
                new{keyData}
            }.AttachToDocument();

            var p = Native.crypto.subtle.importKey(
                format: "jwk",
                keyData: keyData,
                algorithm: algorithm,
                extractable: false,
                keyUsages: new[] { "encrypt", 
                
                    // onError { z = SyntaxError: Cannot create a key using the specified key usages. }
                    "verify" 
                }
            );

            p.then(
                onSuccess: z =>
            {
                // onSuccess {{ z = [object CryptoKey], ElapsedMilliseconds = 9278 }}


                new IHTMLPre { "onSuccess " + new { z, sw.ElapsedMilliseconds } }.AttachToDocument();

                new IHTMLButton { "encrypt for server" }.AttachToDocument().onclick +=
                async delegate
                {
                    // Man in the middle?
                    // layered security
                    var data = Encoding.UTF8.GetBytes("hello from client");
                    var esw = Stopwatch.StartNew();

                    var ebytes = await Native.crypto.subtle.encryptAsync(algorithm, z, data);
                    new IHTMLPre { "encryptAsync " + new { esw.ElapsedMilliseconds } }.AttachToDocument();

                    await UploadEncryptedString(
                        ebytes
                    );
                };


            },
                onError: z =>
            {
                // onError { z = DataError: The JWK member "n" could not be base64url decoded or contained padding }

                new IHTMLPre {
                    "onError " +
                new {
               z}
            }.AttachToDocument();
            }

            );

        }

    }
}
