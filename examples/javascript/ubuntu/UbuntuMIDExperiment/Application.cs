using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UbuntuMIDExperiment;
using UbuntuMIDExperiment.Design;
using UbuntuMIDExperiment.HTML.Pages;

namespace UbuntuMIDExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151123/ubuntumidexperiment

        public Application(IApp page)
        {
            // can we log in via MID?
            // where is the last test?
            // Z:\jsc.svn\examples\javascript\crypto\VerifyIdentityAffinity\VerifyIdentityAffinity\Application.cs

            // can we modify roslyn to compile comments into IL?
            new IHTMLPre { "guest, EID, MID. which are we?" }.AttachToDocument();
            new IHTMLPre { new { Native.document.location.protocol, Native.document.location.host } }.AttachToDocument();
            // NFC ?
            new IHTMLPre { new { Native.window.navigator.userAgent } }.AttachToDocument();
            new IHTMLPre { new { base.identity } }.AttachToDocument();

            // { identity = { value = guest, signature = 70d1638ccb1627209f7d5751b989dd5cc399ff17c72aff075f2e05ff1b3c9a1f474cf5813c6470b8e9ee77b5911316acee62c6bf3534b2bc4942bc9de4344fc9 } }

            // doesnt jsc assetslibary do public key?
            // should we want to validate the identity on client?
            // can we? if we run on http we cant.



            new IHTMLButton { "EID" }.AttachToDocument().With(
                async e =>
                {
                    // are we running nfc web browser?
                    if (Native.window.navigator.userAgent.Contains("NFCDID"))
                        e.innerText = "NFC DID";


                    // NFC ?
                    await e.async.onclick;

                    Native.document.body.Clear();

                    new IHTMLPre { e.innerText + " login..." }.AttachToDocument();

                    // need hopping support.
                }
            );

            new IHTMLButton { "MID" }.AttachToDocument().With(
              async e =>
              {
                  await e.async.onclick;

                  Native.document.body.Clear();

                  new IHTMLPre { "MID login..." }.AttachToDocument();
              }
          );
        }

    }

    public class ApplicationWebService
    {
        public VerifiableString identity = new VerifiableString { value = "guest" }.Sign(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters);


    }
}
