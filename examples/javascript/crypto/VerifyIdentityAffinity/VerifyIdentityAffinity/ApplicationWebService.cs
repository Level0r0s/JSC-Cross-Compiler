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
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151123/ubuntumidexperiment

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
                //Kohustuslik on kas IDCode või PhoneNo,
                //soovitatav on kasutada mõlemat
                //sisendparameetrit! Leedu Mobiil-ID kasutajate
                //puhul on kohustuslikud IDCode ja PhoneNo
                IDCode = "14212128025",

                // Isikukoodi välja andnud riik, kasutatakse ISO 3166 
                // 2 tähelisi riigikoode (näiteks: EE)
                CountryCode = "EE",



                // Autenditava isiku telefoninumber koos riigikoodiga
                //kujul +xxxxxxxxx (näiteks +3706234566).
                //Kui on määratud nii PhoneNo kui ka IDCode
                //parameetrid, kontrollitakse telefoninumbri
                //vastavust isikukoodile ja mittevastavuse korral
                //tagastatakse SOAP veakood 301. Kohustuslik on
                //kas IDCode või PhoneNo, soovitatav on kasutada
                //mõlemat sisendparameetrit! Leedu Mobiil-ID
                //kasutajate puhul on kohustuslikud IDCode ja
                //PhoneNo (vt. peatükk 5.2). Kui element “PhoneNo”
                //on määratud, siis teenuse siseselt lähtutakse
                //prefiksis määratud riigi tunnusest (sõltumata
                //elemendi "CountryCode" väärtusest)
                //PhoneNo = "+37200007"
                PhoneNo = "37200007",

                // Telefonile kuvatavate teadete keel. Kasutatakse: 3-
                // tähelisi koode suurtähtedes.Võimalikud variandid:
                //(EST, ENG, RUS, LIT).
                Language = "EST",

                // Autentimisel telefonil kuvatav teenuse nimetus,
                //maksimaalne pikkus 20 tähemärki.
                //Eelnevalt on vajalik kasutatava teenuse nimetuse
                //kokkuleppimine teenuse pakkujaga
                ServiceName = "Testimine",

                //Täiendav tekst, mis autentimise PIN-i küsimise
                //eelselt lisaks teenuse nimetuse kasutaja telefonile
                //kuvatakse. Maksimaalne pikkus 40 baiti (ladina
                //tähtede puhul tähendab see ühtlasi ka 40 sümboli
                //pikkust teksti, aga näiteks kirillitsa teksti puhul
                //võidakse tähti kodeerida 2 baidistena ja siis ei saa
                //saata pikemat kui 20-sümbolilist teksti).
                MessageToDisplay = "Testimine",

                //- Rakenduse pakkuja poolt genereeritud juhuslik 10
                //baidine tekst, mis on osa (autentimise käigus)
                //kasutaja poolt signeeritavast sõnumist.
                //Edastatakse HEX stringina.
                //NB! Suurema turvalisuse huvides on soovitatav
                //see väli alati täita, iga kord erineva juhusliku
                //väärtusega. Kui autentimine õnnestub, on
                //soovitatav ka kontrollida, et kasutaja poolt
                //allkirjastatud väärtus tõepoolest ka sisaldab antud
                //SPChallenge-i väärtust. Täpsem info viimase
                //verifitseerimise kohta on peatükis
                //„GetMobileAuthenticateStatus“, „Signature“-
                //elemendi kirjelduse all.

                SPChallenge = "03010400000000000000",

                // Autentimise toimingu vastuse tagastamise viis.
                //Võimalikud variandid:
                //- “asynchClientServer” – rakendus teeb pärast
                //MobileAuthenticate meetodi väljakutsumist
                //täiendavaid staatuspäringuid (kasutades
                //meetodit GetMobileAuthenticateStatus).
                //- “asynchServerServer” – toimingu lõppemisel
                //või vea tekkimisel saadetakse vastus
                //kliendirakendusele asünkroonselt (vt.
                //parameeter AsyncConfiguration).
                MessagingMode = "asynchClientServer",

                //AsyncConfiguration = 
                // Määrab asünkroonselt vastuse tagasisaatmise 
                //konfiguratsiooni.Antud parameetri väärtust
                //kasutatakse ainult juhul kui MessagingMode on
                //“asynchServerServer”. Konfiguratsioon lepitakse
                //kokku teenuse kasutaja ja teenuse pakkuja vahel.
                //Hetkel on toetatud vastuse tagasi saatmine
                //kasutades Java Message Services(JMS) liidest


                // Kui väärtus on “TRUE”, tagastatakse vastuses
                //autenditava isiku sertifikaat. Sertifikaat on vajalik,
                //kui rakenduse pakkuja soovib talletada ja
                //iseseisvalt kontrollida signatuuri korrektsust ja
                //kehtivusinfot.

                ReturnCertData = true,

                //Väärtuse “TRUE” korral tagastatakse sertifikaadi
                //kehtivusinfo vastuses RevocationData väljal.
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

            //NB! Enne esimese staatuse päringu saatmist on soovitatav oodata vähemalt 15
            //sekundit kuna autentimise protsess ei saa tehniliste ja inimlike piirangute tõttu
            //kiiremini lõppeda. Mobiil-ID toimingud aeguvad hiljemalt 4 minuti jooksul.

            //invoke MobileAuthenticateAsync { ElapsedMilliseconds = 0 }
            //after MobileAuthenticateAsync { ElapsedMilliseconds = 715 }
            //after MobileAuthenticateAsync done { ElapsedMilliseconds = 1527, Sesscode = 622288131 }
            //after GetMobileAuthenticateStatusAsync  { ElapsedMilliseconds = 1531 }
            //after GetMobileAuthenticateStatusAsync done { ElapsedMilliseconds = 16566, Signature = qxgf

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


        public int MobileAuthenticateAsync15_Sesscode;
        public string MobileAuthenticateAsync15_UserIDCode;
        public string MobileAuthenticateAsync15_ChallengeID;

        // can we have signed int?
        public async Task MobileAuthenticateAsync15()
        {
            // Z:\jsc.svn\examples\java\hybrid\JVMCLRWSDLMID\Program.cs

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
                //Kohustuslik on kas IDCode või PhoneNo,
                //soovitatav on kasutada mõlemat
                //sisendparameetrit! Leedu Mobiil-ID kasutajate
                //puhul on kohustuslikud IDCode ja PhoneNo
                IDCode = "14212128025",

                // Isikukoodi välja andnud riik, kasutatakse ISO 3166 
                // 2 tähelisi riigikoode (näiteks: EE)
                CountryCode = "EE",



                // Autenditava isiku telefoninumber koos riigikoodiga
                //kujul +xxxxxxxxx (näiteks +3706234566).
                //Kui on määratud nii PhoneNo kui ka IDCode
                //parameetrid, kontrollitakse telefoninumbri
                //vastavust isikukoodile ja mittevastavuse korral
                //tagastatakse SOAP veakood 301. Kohustuslik on
                //kas IDCode või PhoneNo, soovitatav on kasutada
                //mõlemat sisendparameetrit! Leedu Mobiil-ID
                //kasutajate puhul on kohustuslikud IDCode ja
                //PhoneNo (vt. peatükk 5.2). Kui element “PhoneNo”
                //on määratud, siis teenuse siseselt lähtutakse
                //prefiksis määratud riigi tunnusest (sõltumata
                //elemendi "CountryCode" väärtusest)
                //PhoneNo = "+37200007"
                PhoneNo = "37200007",

                // Telefonile kuvatavate teadete keel. Kasutatakse: 3-
                // tähelisi koode suurtähtedes.Võimalikud variandid:
                //(EST, ENG, RUS, LIT).
                Language = "EST",

                // Autentimisel telefonil kuvatav teenuse nimetus,
                //maksimaalne pikkus 20 tähemärki.
                //Eelnevalt on vajalik kasutatava teenuse nimetuse
                //kokkuleppimine teenuse pakkujaga
                ServiceName = "Testimine",

                //Täiendav tekst, mis autentimise PIN-i küsimise
                //eelselt lisaks teenuse nimetuse kasutaja telefonile
                //kuvatakse. Maksimaalne pikkus 40 baiti (ladina
                //tähtede puhul tähendab see ühtlasi ka 40 sümboli
                //pikkust teksti, aga näiteks kirillitsa teksti puhul
                //võidakse tähti kodeerida 2 baidistena ja siis ei saa
                //saata pikemat kui 20-sümbolilist teksti).
                MessageToDisplay = "Testimine",

                //- Rakenduse pakkuja poolt genereeritud juhuslik 10
                //baidine tekst, mis on osa (autentimise käigus)
                //kasutaja poolt signeeritavast sõnumist.
                //Edastatakse HEX stringina.
                //NB! Suurema turvalisuse huvides on soovitatav
                //see väli alati täita, iga kord erineva juhusliku
                //väärtusega. Kui autentimine õnnestub, on
                //soovitatav ka kontrollida, et kasutaja poolt
                //allkirjastatud väärtus tõepoolest ka sisaldab antud
                //SPChallenge-i väärtust. Täpsem info viimase
                //verifitseerimise kohta on peatükis
                //„GetMobileAuthenticateStatus“, „Signature“-
                //elemendi kirjelduse all.

                SPChallenge = "03010400000000000000",

                // Autentimise toimingu vastuse tagastamise viis.
                //Võimalikud variandid:
                //- “asynchClientServer” – rakendus teeb pärast
                //MobileAuthenticate meetodi väljakutsumist
                //täiendavaid staatuspäringuid (kasutades
                //meetodit GetMobileAuthenticateStatus).
                //- “asynchServerServer” – toimingu lõppemisel
                //või vea tekkimisel saadetakse vastus
                //kliendirakendusele asünkroonselt (vt.
                //parameeter AsyncConfiguration).
                MessagingMode = "asynchClientServer",

                //AsyncConfiguration = 
                // Määrab asünkroonselt vastuse tagasisaatmise 
                //konfiguratsiooni.Antud parameetri väärtust
                //kasutatakse ainult juhul kui MessagingMode on
                //“asynchServerServer”. Konfiguratsioon lepitakse
                //kokku teenuse kasutaja ja teenuse pakkuja vahel.
                //Hetkel on toetatud vastuse tagasi saatmine
                //kasutades Java Message Services(JMS) liidest


                // Kui väärtus on “TRUE”, tagastatakse vastuses
                //autenditava isiku sertifikaat. Sertifikaat on vajalik,
                //kui rakenduse pakkuja soovib talletada ja
                //iseseisvalt kontrollida signatuuri korrektsust ja
                //kehtivusinfot.

                ReturnCertData = true,

                //Väärtuse “TRUE” korral tagastatakse sertifikaadi
                //kehtivusinfo vastuses RevocationData väljal.
                ReturnRevocationData = false

            });
            #endregion

            // poll for status?





            Console.WriteLine("after MobileAuthenticateAsync " + new { sw.ElapsedMilliseconds });
            sk.MobileAuthenticateResponse x = xa.Result;
            Console.WriteLine("after MobileAuthenticateAsync done " + new { sw.ElapsedMilliseconds, x.Sesscode, x.ChallengeID, x.Status });


            MobileAuthenticateAsync15_Sesscode = x.Sesscode;
            MobileAuthenticateAsync15_UserIDCode = x.UserIDCode;
            MobileAuthenticateAsync15_ChallengeID = x.ChallengeID;

            var v = new VerifiableString
            {
                value = new
                {
                    MobileAuthenticateAsync15_Sesscode,
                    MobileAuthenticateAsync15_UserIDCode,
                    MobileAuthenticateAsync15_ChallengeID
                }.ToString()

                // if we can verify it later, we can trust it to be set by the web service. otherwise we cannot trust it.
                // this would also enable state sharing now.
                // signed and perhaps encrypted too..
            }.Sign(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters);


            // sign status
            // mouse over shows the sig. yay.
            status.SetAttributeValue("title", Convert.ToBase64String(v.signature));
            status.Value = v.value;

            // sign state hop?
        }

        public async Task<bool> MobileAuthenticateAsync15Continue()
        {
            var sw = Stopwatch.StartNew();

            var c = new sk.DigiDocServicePortTypeClient(new BasicHttpsBinding(), new EndpointAddress("https://tsp.demo.sk.ee:443"));

            Console.WriteLine("before GetMobileAuthenticateStatusAsync  " + new { sw.ElapsedMilliseconds, MobileAuthenticateAsync15_Sesscode });

            var xGetMobileAuthenticateStatusResponseTask = c.GetMobileAuthenticateStatusAsync(
               new sk.GetMobileAuthenticateStatusRequest
               {
                   Sesscode = MobileAuthenticateAsync15_Sesscode,
                   WaitSignature = true
               }
           );

            // are we to show xa.Result.Sesscode
            // to client. signed?

            Console.WriteLine("after GetMobileAuthenticateStatusAsync  " + new { sw.ElapsedMilliseconds });


            // we need to switch to ui and back 

            var xGetMobileAuthenticateStatusResponse = xGetMobileAuthenticateStatusResponseTask.Result;

            Console.WriteLine("after GetMobileAuthenticateStatusAsync done " + new { sw.ElapsedMilliseconds, xGetMobileAuthenticateStatusResponse.Signature });

            //NB! Enne esimese staatuse päringu saatmist on soovitatav oodata vähemalt 15
            //sekundit kuna autentimise protsess ei saa tehniliste ja inimlike piirangute tõttu
            //kiiremini lõppeda. Mobiil-ID toimingud aeguvad hiljemalt 4 minuti jooksul.

            //invoke MobileAuthenticateAsync { ElapsedMilliseconds = 0 }
            //after MobileAuthenticateAsync { ElapsedMilliseconds = 715 }
            //after MobileAuthenticateAsync done { ElapsedMilliseconds = 1527, Sesscode = 622288131 }
            //after GetMobileAuthenticateStatusAsync  { ElapsedMilliseconds = 1531 }
            //after GetMobileAuthenticateStatusAsync done { ElapsedMilliseconds = 16566, Signature = qxgf

            var v = new VerifiableString
            {
                value = new
                {

                    MobileAuthenticateAsync15_UserIDCode,
                    //MobileAuthenticateAsync15_Sesscode,
                    xGetMobileAuthenticateStatusResponse.Status
                }.ToString()

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

