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
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
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

                  var state = await base.MobileAuthenticateAsync15();

                  new IHTMLPre { new { state.MobileAuthenticateAsync15_UserIDCode } }.AttachToDocument();
                  new IHTMLPre { new { state.MobileAuthenticateAsync15_Sesscode } }.AttachToDocument();
                  new IHTMLPre { new { state.MobileAuthenticateAsync15_ChallengeID } }.AttachToDocument();

                  new IHTMLCenter { state.MobileAuthenticateAsync15_ChallengeID.value }.AttachToDocument();

                  var sw = Stopwatch.StartNew();

                  //new IHTMLPre { base.MobileAuthenticateAsync15_ChallengeID, () => new { sw.ElapsedMilliseconds } }.AttachToDocument();
                  //using (new IHTMLPre { () => new { sw.ElapsedMilliseconds } }.AttachToDocument())
                  var u = (new IHTMLPre { () => new { sw.ElapsedMilliseconds } }.AttachToDocument());
                  {
                      await Task.Delay(15000);
                      sw.Stop();
                  }
                  u.Dispose();

                  Native.document.documentElement.style.borderLeft = "2px solid yellow";

                  await base.MobileAuthenticateAsync15Continue(state);

                  // { identity = { value = 14212128025, signature = 818a158d2497c54e9bde7cc7ee498b264282b709a87394be7037c09a7e38d57d515d34fa3cd7a93e8487bd477e4bd20d3f6bfbdce3c07926a1ede5935b6e5d3e } }

                  Native.document.body.Clear();
                  new IHTMLPre { new { base.identity } }.AttachToDocument();
              }
          );
        }

    }

    public class ApplicationWebService
    {
        public VerifiableString identity = new VerifiableString { value = "guest" }.Sign(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters);


        public sealed class MobileAuthenticateAsync15State
        {
            public VerifiableString MobileAuthenticateAsync15_Sesscode;
            public VerifiableString MobileAuthenticateAsync15_UserIDCode;
            public VerifiableString MobileAuthenticateAsync15_ChallengeID;


            public VerifiableString MobileAuthenticateAsync15Continue_Status;
        }

        public async Task<MobileAuthenticateAsync15State> MobileAuthenticateAsync15()
        {
            // Z:\jsc.svn\examples\java\hybrid\JVMCLRWSDLMID\Program.cs

            // Add Service Reference
            // https://tsp.demo.sk.ee/?wsdl 
            //var c = new sk.DigiDocServicePortTypeClient("DigiDocService", "https://tsp.demo.sk.ee:443");
            var c = new sk.DigiDocServicePortTypeClient(new BasicHttpsBinding(), new EndpointAddress("https://tsp.demo.sk.ee:443"));


            var sw = Stopwatch.StartNew();

            Console.WriteLine("invoke MobileAuthenticateAsync " + new { sw.ElapsedMilliseconds });

            #region MobileAuthenticateRequest
            var xa = c.MobileAuthenticateAsync(new sk.MobileAuthenticateRequest
            {
                // http://www.id.ee/?id=30340

                // http://www.sk.ee/upload/files/DigiDocService_spec_est.pdf

                // Autenditava isiku isikukood.
                //Kohustuslik on kas IDCode v�i PhoneNo,
                //soovitatav on kasutada m�lemat
                //sisendparameetrit! Leedu Mobiil-ID kasutajate
                //puhul on kohustuslikud IDCode ja PhoneNo
                IDCode = "14212128025",

                // Isikukoodi v�lja andnud riik, kasutatakse ISO 3166 
                // 2 t�helisi riigikoode (n�iteks: EE)
                CountryCode = "EE",



                // Autenditava isiku telefoninumber koos riigikoodiga
                //kujul +xxxxxxxxx (n�iteks +3706234566).
                //Kui on m��ratud nii PhoneNo kui ka IDCode
                //parameetrid, kontrollitakse telefoninumbri
                //vastavust isikukoodile ja mittevastavuse korral
                //tagastatakse SOAP veakood 301. Kohustuslik on
                //kas IDCode v�i PhoneNo, soovitatav on kasutada
                //m�lemat sisendparameetrit! Leedu Mobiil-ID
                //kasutajate puhul on kohustuslikud IDCode ja
                //PhoneNo (vt. peat�kk 5.2). Kui element �PhoneNo�
                //on m��ratud, siis teenuse siseselt l�htutakse
                //prefiksis m��ratud riigi tunnusest (s�ltumata
                //elemendi "CountryCode" v��rtusest)
                //PhoneNo = "+37200007"
                PhoneNo = "37200007",

                // Telefonile kuvatavate teadete keel. Kasutatakse: 3-
                // t�helisi koode suurt�htedes.V�imalikud variandid:
                //(EST, ENG, RUS, LIT).
                Language = "EST",

                // Autentimisel telefonil kuvatav teenuse nimetus,
                //maksimaalne pikkus 20 t�hem�rki.
                //Eelnevalt on vajalik kasutatava teenuse nimetuse
                //kokkuleppimine teenuse pakkujaga
                ServiceName = "Testimine",

                //T�iendav tekst, mis autentimise PIN-i k�simise
                //eelselt lisaks teenuse nimetuse kasutaja telefonile
                //kuvatakse. Maksimaalne pikkus 40 baiti (ladina
                //t�htede puhul t�hendab see �htlasi ka 40 s�mboli
                //pikkust teksti, aga n�iteks kirillitsa teksti puhul
                //v�idakse t�hti kodeerida 2 baidistena ja siis ei saa
                //saata pikemat kui 20-s�mbolilist teksti).
                MessageToDisplay = "Testimine",

                //- Rakenduse pakkuja poolt genereeritud juhuslik 10
                //baidine tekst, mis on osa (autentimise k�igus)
                //kasutaja poolt signeeritavast s�numist.
                //Edastatakse HEX stringina.
                //NB! Suurema turvalisuse huvides on soovitatav
                //see v�li alati t�ita, iga kord erineva juhusliku
                //v��rtusega. Kui autentimine �nnestub, on
                //soovitatav ka kontrollida, et kasutaja poolt
                //allkirjastatud v��rtus t�epoolest ka sisaldab antud
                //SPChallenge-i v��rtust. T�psem info viimase
                //verifitseerimise kohta on peat�kis
                //�GetMobileAuthenticateStatus�, �Signature�-
                //elemendi kirjelduse all.

                SPChallenge = "03010400000000000000",

                // Autentimise toimingu vastuse tagastamise viis.
                //V�imalikud variandid:
                //- �asynchClientServer� � rakendus teeb p�rast
                //MobileAuthenticate meetodi v�ljakutsumist
                //t�iendavaid staatusp�ringuid (kasutades
                //meetodit GetMobileAuthenticateStatus).
                //- �asynchServerServer� � toimingu l�ppemisel
                //v�i vea tekkimisel saadetakse vastus
                //kliendirakendusele as�nkroonselt (vt.
                //parameeter AsyncConfiguration).
                MessagingMode = "asynchClientServer",

                //AsyncConfiguration = 
                // M��rab as�nkroonselt vastuse tagasisaatmise 
                //konfiguratsiooni.Antud parameetri v��rtust
                //kasutatakse ainult juhul kui MessagingMode on
                //�asynchServerServer�. Konfiguratsioon lepitakse
                //kokku teenuse kasutaja ja teenuse pakkuja vahel.
                //Hetkel on toetatud vastuse tagasi saatmine
                //kasutades Java Message Services(JMS) liidest


                // Kui v��rtus on �TRUE�, tagastatakse vastuses
                //autenditava isiku sertifikaat. Sertifikaat on vajalik,
                //kui rakenduse pakkuja soovib talletada ja
                //iseseisvalt kontrollida signatuuri korrektsust ja
                //kehtivusinfot.

                ReturnCertData = true,

                //V��rtuse �TRUE� korral tagastatakse sertifikaadi
                //kehtivusinfo vastuses RevocationData v�ljal.
                ReturnRevocationData = false

            });
            #endregion

            // poll for status?





            Console.WriteLine("after MobileAuthenticateAsync " + new { sw.ElapsedMilliseconds });
            sk.MobileAuthenticateResponse x = xa.Result;
            Console.WriteLine("after MobileAuthenticateAsync done " + new { sw.ElapsedMilliseconds, x.Sesscode, x.ChallengeID, x.Status });


            //    // if we can verify it later, we can trust it to be set by the web service. otherwise we cannot trust it.
            //    // this would also enable state sharing now.
            //    // signed and perhaps encrypted too..


            // sign status
            // mouse over shows the sig. yay.
            // sign state hop?

            // at this point jsc does not yet allow state sharing nor is it signed
            return new MobileAuthenticateAsync15State
            {
                MobileAuthenticateAsync15_Sesscode = new VerifiableString { value = x.Sesscode.ToString() }.Sign(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters),
                MobileAuthenticateAsync15_UserIDCode = new VerifiableString { value = x.UserIDCode }.Sign(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters),
                MobileAuthenticateAsync15_ChallengeID = new VerifiableString { value = x.ChallengeID }.Sign(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters)
            };
        }

        public async Task<MobileAuthenticateAsync15State> MobileAuthenticateAsync15Continue(MobileAuthenticateAsync15State args)
        {
            var sw = Stopwatch.StartNew();

            var c = new sk.DigiDocServicePortTypeClient(new BasicHttpsBinding(), new EndpointAddress("https://tsp.demo.sk.ee:443"));

            Console.WriteLine("before GetMobileAuthenticateStatusAsync  " + new { sw.ElapsedMilliseconds, args.MobileAuthenticateAsync15_Sesscode });

            var xGetMobileAuthenticateStatusResponseTask = c.GetMobileAuthenticateStatusAsync(
               new sk.GetMobileAuthenticateStatusRequest
               {
                   Sesscode = Convert.ToInt32(args.MobileAuthenticateAsync15_Sesscode.value),
                   WaitSignature = true
               }
           );

            // are we to show xa.Result.Sesscode
            // to client. signed?

            Console.WriteLine("after GetMobileAuthenticateStatusAsync  " + new { sw.ElapsedMilliseconds });


            // we need to switch to ui and back 

            var xGetMobileAuthenticateStatusResponse = xGetMobileAuthenticateStatusResponseTask.Result;

            Console.WriteLine("after GetMobileAuthenticateStatusAsync done " + new
            {
                sw.ElapsedMilliseconds,

                xGetMobileAuthenticateStatusResponse.Status,

                xGetMobileAuthenticateStatusResponse.Signature
            });

            //NB! Enne esimese staatuse p�ringu saatmist on soovitatav oodata v�hemalt 15
            //sekundit kuna autentimise protsess ei saa tehniliste ja inimlike piirangute t�ttu
            //kiiremini l�ppeda. Mobiil-ID toimingud aeguvad hiljemalt 4 minuti jooksul.

            if (xGetMobileAuthenticateStatusResponse.Status == "USER_AUTHENTICATED")
            {
                this.identity = new VerifiableString { value = args.MobileAuthenticateAsync15_UserIDCode.value }.Sign(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters);
            }

            return new MobileAuthenticateAsync15State
            {
                MobileAuthenticateAsync15Continue_Status = new VerifiableString { value = xGetMobileAuthenticateStatusResponse.Status }.Sign(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters)
            };
        }
    }
}

