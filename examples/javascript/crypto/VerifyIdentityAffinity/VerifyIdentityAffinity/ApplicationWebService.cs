using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VerifyIdentityAffinity
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // the idea of this experiment is
        // to allow the https user to establish the client certificate
        // either by smartcard or by MID
        // after the identity is established the rest of the api
        // can inspect the claim signed by local webserviceauthority.

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151105/namedkey

        //public async Task GetStatus(XElement status)
        // can we modify the UI like this? no not yet

        // this is synced back to ui
        public XElement status;

        public async Task GetStatus()
        {
            { var ref0 = typeof(NamedKeyPairs.WebServiceAuthorityPrivateKey); }

            var x = new VerifiableString
            {
                value = "guest"

                // if we can verify it later, we can trust it to be set by the web service. otherwise we cannot trust it.
                // this would also enable state sharing now.
                // signed and perhaps encrypted too..
            }.Sign(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters);


            // sign status
            // mouse over shows the sig. yay.
            status.SetAttributeValue("title", Convert.ToBase64String(x.signature));
            status.Value = x.value;
        }

        public async Task<bool> Verify()
        {
            // lets verify whats the ui saying.

            return new VerifiableString
            {
                value = status.Value,
                signature = Convert.FromBase64String(status.Attribute("title").Value)
            }.Verify(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters);
        }



        public async Task<bool> MobileAuthenticateAsync()
        {
            // Z:\jsc.svn\examples\java\hybrid\JVMCLRWSDLMID\Program.cs

            // https://tsp.demo.sk.ee/?wsdl 
            //var c = new sk.DigiDocServicePortTypeClient("DigiDocService", "https://tsp.demo.sk.ee:443");
            var c = new sk.DigiDocServicePortTypeClient(new BasicHttpsBinding(), new EndpointAddress("https://tsp.demo.sk.ee:443"));


            var sw = Stopwatch.StartNew();

            Console.WriteLine("invoke MobileAuthenticateAsync " + new { sw.ElapsedMilliseconds });

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

            // poll for status?





            Console.WriteLine("after MobileAuthenticateAsync " + new { sw.ElapsedMilliseconds });
            sk.MobileAuthenticateResponse x = xa.Result;
            Console.WriteLine("after MobileAuthenticateAsync done " + new { sw.ElapsedMilliseconds, x.Sesscode });

            var xGetMobileAuthenticateStatusResponseTask = c.GetMobileAuthenticateStatusAsync(
               new sk.GetMobileAuthenticateStatusRequest
               {
                   Sesscode = xa.Result.Sesscode,
                   WaitSignature = true
               }
           );

            // are we to show xa.Result.Sesscode
            // to client. signed?

            Console.WriteLine("after GetMobileAuthenticateStatusAsync  " + new { sw.ElapsedMilliseconds });


            // we need to switch to ui and back 

            var xGetMobileAuthenticateStatusResponse = xGetMobileAuthenticateStatusResponseTask.Result;

            Console.WriteLine("after GetMobileAuthenticateStatusAsync done " + new { sw.ElapsedMilliseconds, xGetMobileAuthenticateStatusResponse.Signature });

            //NB! Enne esimese staatuse p�ringu saatmist on soovitatav oodata v�hemalt 15
            //sekundit kuna autentimise protsess ei saa tehniliste ja inimlike piirangute t�ttu
            //kiiremini l�ppeda. Mobiil-ID toimingud aeguvad hiljemalt 4 minuti jooksul.


            var v = new VerifiableString
            {
                value = new { x.UserIDCode, x.Sesscode, xGetMobileAuthenticateStatusResponse.Status }.ToString()

                // if we can verify it later, we can trust it to be set by the web service. otherwise we cannot trust it.
                // this would also enable state sharing now.
                // signed and perhaps encrypted too..
            }.Sign(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters);


            // sign status
            // mouse over shows the sig. yay.
            status.SetAttributeValue("title", Convert.ToBase64String(v.signature));
            status.Value = v.value;

            return true;
        }

    }
}

//Message	"Could not find endpoint element with name 'DigiDocService' and contract 'sk.DigiDocServicePortType' in the ServiceModel client configuration section. This might be because no configuration file was found for your application, or because no endpoint element matching this name could be found in the client element."	string

//Error	4	Async methods cannot have ref or out parameters	Z:\jsc.svn\examples\javascript\crypto\VerifyIdentityAffinity\VerifyIdentityAffinity\ApplicationWebService.cs	31	37	VerifyIdentityAffinity

