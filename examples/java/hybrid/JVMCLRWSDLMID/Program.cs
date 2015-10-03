using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRWSDLMID
{
    public class InspectorBehavior : IEndpointBehavior
    {
        public string LastRequestXML
        {
            get
            {
                return myMessageInspector.LastRequestXML;
            }
        }

        public string LastResponseXML
        {
            get
            {
                return myMessageInspector.LastResponseXML;
            }
        }


        public class MyMessageInspector : IClientMessageInspector
        {
            public string LastRequestXML { get; private set; }
            public string LastResponseXML { get; private set; }
            public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
            {
                LastResponseXML = reply.ToString();
            }

            public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
            {
                LastRequestXML = request.ToString();
                return request;
            }
        }

        private MyMessageInspector myMessageInspector = new MyMessageInspector();
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {

        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }


        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(myMessageInspector);
        }
    }



    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // https://tsp.demo.sk.ee/?wsdl 

            //sk.DigiDocServicePortTypeClient


            //  __Task.<TResult>Run_06003e0c(Program.CS___9__CachedAnonymousMethodDelegate2).Wait();



            //public static Task<TResult> Run<TResult>(Func<Task<TResult>> function);
            //public static Task Run(Func<Task> function);
            //public static Task<TResult> Run<TResult>(Func<TResult> function);

            //Task.Run<Task>(

            //  System.ServiceModel.ClientBase`1 .ctor


            // http://stackoverflow.com/questions/1082850/java-reflection-create-an-implementing-class

            //{ Message = , StackTrace = java.lang.NullPointerException
            //        at JVMCLRWSDLMID.sk.DigiDocServicePortTypeClient.MobileAuthenticateAsync(DigiDocServicePortTypeClient.java:108)
            //        at JVMCLRWSDLMID.Program.main(Program.java:54)

            try
            {
                // java.lang.reflect.Proxy to the rescue!

                var c = new sk.DigiDocServicePortTypeClient();

                var requestInterceptor = new InspectorBehavior();

                c.Endpoint.EndpointBehaviors.Add(requestInterceptor);

                //<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/">
                //  <s:Header>
                //    <Action s:mustUnderstand="1" xmlns="http://schemas.microsoft.com/ws/2005/05/addressing/none" />
                //  </s:Header>
                //  <s:Body s:encodingStyle="http://schemas.xmlsoap.org/soap/encoding/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                //    <q1:MobileAuthenticate xmlns:q1="http://www.sk.ee/DigiDocService/DigiDocService_2_3.wsdl">
                //      <IDCode xsi:type="xsd:string">14212128025</IDCode>
                //      <CountryCode xsi:type="xsd:string">EE</CountryCode>
                //      <PhoneNo xsi:type="xsd:string">37200007</PhoneNo>
                //      <Language xsi:type="xsd:string">EST</Language>
                //      <ServiceName xsi:type="xsd:string">Testimine</ServiceName>
                //      <MessageToDisplay xsi:type="xsd:string">Testimine</MessageToDisplay>
                //      <SPChallenge xsi:type="xsd:string">03010400000000000000</SPChallenge>
                //      <MessagingMode xsi:type="xsd:string">asynchClientServer</MessagingMode>
                //      <AsyncConfiguration xsi:type="xsd:int">0</AsyncConfiguration>
                //      <ReturnCertData xsi:type="xsd:boolean">true</ReturnCertData>
                //      <ReturnRevocationData xsi:type="xsd:boolean">false</ReturnRevocationData>
                //    </q1:MobileAuthenticate>
                //  </s:Body>
                //</s:Envelope>

                //<SOAP-ENV:Envelope xmlns:SOAP-ENV="http://schemas.xmlsoap.org/soap/envelope/" xmlns:SOAP-ENC="http://schemas.xmlsoap.org/soap/encoding/" xmlns:dig="http://www.sk.ee/DigiDocService/DigiDocService_2_3.wsdl" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
                //  <SOAP-ENV:Header />
                //  <SOAP-ENV:Body SOAP-ENV:encodingStyle="http://schemas.xmlsoap.org/soap/encoding/">
                //    <dig:MobileAuthenticateResponse>
                //      <Sesscode xsi:type="xsd:int">1176437113</Sesscode>
                //      <Status xsi:type="xsd:string">OK</Status>
                //      <UserIDCode xsi:type="xsd:string">14212128025</UserIDCode>
                //      <UserGivenname xsi:type="xsd:string">SEITSMES</UserGivenname>
                //      <UserSurname xsi:type="xsd:string">TESTNUMBER</UserSurname>
                //      <UserCountry xsi:type="xsd:string">EE</UserCountry>
                //      <UserCN xsi:type="xsd:string">TESTNUMBER,SEITSMES,14212128025</UserCN>
                //      <CertificateData xsi:type="xsd:string">MIIE0TCCA7mgAwIBAgIQPQ9ysbj2GdNVIlXEV07JAzANBgkqhkiG9w0BAQsFADBs
                //MQswCQYDVQQGEwJFRTEiMCAGA1UECgwZQVMgU2VydGlmaXRzZWVyaW1pc2tlc2t1
                //czEfMB0GA1UEAwwWVEVTVCBvZiBFU1RFSUQtU0sgMjAxMTEYMBYGCSqGSIb3DQEJ
                //ARYJcGtpQHNrLmVlMB4XDTE1MDQwNjA5NDU0MFoXDTE2MTIzMTIxNTk1OVowgasx
                //CzAJBgNVBAYTAkVFMRswGQYDVQQKDBJFU1RFSUQgKE1PQklJTC1JRCkxFzAVBgNV
                //BAsMDmF1dGhlbnRpY2F0aW9uMSgwJgYDVQQDDB9URVNUTlVNQkVSLFNFSVRTTUVT
                //LDE0MjEyMTI4MDI1MRMwEQYDVQQEDApURVNUTlVNQkVSMREwDwYDVQQqDAhTRUlU
                //U01FUzEUMBIGA1UEBRMLMTQyMTIxMjgwMjUwgZ8wDQYJKoZIhvcNAQEBBQADgY0A
                //MIGJAoGBAMFo0cOULrm6HHJdMsyYVq6bBmCU4rjg8eonNnbWNq9Y0AAiyIQvJ3xD
                //ULnfwJD0C3QI8Y5RHYnZlt4U4Yt4CI6JenMySV1hElOtGYP1EuFPf643V11t/mUD
                //gY6aZaAuPLNvVYbeVHv0rkunKQ+ORABjhANCvHaErqC24i9kv3mVAgMBAAGjggGx
                //MIIBrTAJBgNVHRMEAjAAMA4GA1UdDwEB/wQEAwIEsDCBmQYDVR0gBIGRMIGOMIGL
                //BgorBgEEAc4fAwMBMH0wWAYIKwYBBQUHAgIwTB5KAEEAaQBuAHUAbAB0ACAAdABl
                //AHMAdABpAG0AaQBzAGUAawBzAC4AIABPAG4AbAB5ACAAZgBvAHIAIAB0AGUAcwB0
                //AGkAbgBnAC4wIQYIKwYBBQUHAgEWFWh0dHA6Ly93d3cuc2suZWUvY3BzLzAnBgNV
                //HREEIDAegRxzZWl0c21lcy50ZXN0bnVtYmVyQGVlc3RpLmVlMB0GA1UdDgQWBBSB
                //iUUnibDAPTHAuhRAwSvWzPfoEjAgBgNVHSUBAf8EFjAUBggrBgEFBQcDAgYIKwYB
                //BQUHAwQwIgYIKwYBBQUHAQMEFjAUMAgGBgQAjkYBATAIBgYEAI5GAQQwHwYDVR0j
                //BBgwFoAUQbb+xbGxtFMTjPr6YtA0bW0iNAowRQYDVR0fBD4wPDA6oDigNoY0aHR0
                //cDovL3d3dy5zay5lZS9yZXBvc2l0b3J5L2NybHMvdGVzdF9lc3RlaWQyMDExLmNy
                //bDANBgkqhkiG9w0BAQsFAAOCAQEANgEwKY0U7zsBtovuU/etxd+tprvTQNpffa92
                //juMIT6G3m9VTaL51Pg/9/MkQnBrcmh5W9QAdqe/3OiJFAtge0DXHyuAw4YqPUVAB
                //0if8u/ZXNSyE9YtWViRSu1ClK5fx3wv5dQbsVvUok085vBm7O62rNG3uKpKILCaL
                //bfOhtxQexbVQME26Vj4TYF9wYy9RXwglMFBKsxD3fQwz6WwmJA3CyRnpTpOA3WPx
                //bsuKIbkBUY0jwJ7baPWqy6dedrdsZ7SzG31o01pAMvOPMvZ5QLOGL4QCLL651iGk
                //nmuUjmgqLbTRGO3Dv4f5UgPfN4tbWp74rwJ97Jkp5zJwQZqrxg==</CertificateData>
                //      <ChallengeID xsi:type="xsd:string">0057</ChallengeID>
                //      <Challenge xsi:type="xsd:string">030104000000000000009A038E07092E19E49EB9</Challenge>
                //    </dig:MobileAuthenticateResponse>
                //  </SOAP-ENV:Body>
                //</SOAP-ENV:Envelope>

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

                sk.MobileAuthenticateResponse x = xa.Result;

                // did we actually do wsdl coms or was it intercepted by core?
                // { Sesscode = 0, Status = , UserIDCode = , UserCN = , ChallengeID = , Challenge =  }

                Console.WriteLine(new
                {

                    // loodud sessiooni identifikaator
                    x.Sesscode,



                    // kasutaja autentimiseks tuleb
                    //teha t�iendavad staatusep�ringud kuni
                    //autentimistoimingu olek on �USER_AUTHENTICATED�
                    x.Status,

                    // Autenditava isiku isikukood. V��rtus v�etakse
                    //isikutuvastuse sertifikaadi eraldusnime �Serial Number�
                    //v�ljalt.
                    x.UserIDCode,

                    // Autenditava isiku isikutuvastuse sertifikaadi p�hinimi.
                    //V��rtus v�etakse isikutuvastuse sertifikaadi eraldusnime
                    //CN (Common Name) v�ljalt.
                    x.UserCN,

                    // 4 t�hem�rgiline kontrollkood, mis arvutatakse kasutaja
                    //                            telefonile signeerimiseks saadetava Challenge v��rtuse
                    //p�hjal.Antud kontrollkood tuleb mobiilautentimist
                    //v�imaldaval rakendusel kuvada kasutajale ja selle kaudu
                    //on v�imalik kasutajal veenduda p�ringu autentsuses.
                    x.ChallengeID,

                    //Kasutaja poolt autentimisel allkirjastatav s�num,
                    //koosneb rakenduse looja poolt saadetud s�numist
                    //(SPChallange, 10 baiti) ja teenuse poolt lisatust (samuti
                    //10 baiti). Tagastatakse vaid juhul, kui p�ringus
                    //SPChallenge v�li on v��rtustatud.

                    x.Challenge
                });

            }
            catch (Exception err)
            {
                Console.WriteLine(new { err.Message, err.StackTrace });

            }

            ////Task.Run(
            ////    async delegate
            ////    {
            ////        var c = new sk.DigiDocServicePortTypeClient();


            ////        //c.MobileAuthenticate()


            ////        ////try
            ////        ////{
            ////        //    function &MobileAuthenticate($IDCode, $CountryCode, $PhoneNo, $Language, $ServiceName, $MessageToDisplay, $SPChallenge, $MessagingMode, $AsyncConfiguration, $ReturnCertData, $ReturnRevocationData)
            ////        // MobileAuthenticate("", "", $tel_no, $lang, "Testimine", "", "00000000000000000000","asynchClientServer", NULL, true, FALSE);

            ////        var xa = c.MobileAuthenticateAsync(new sk.MobileAuthenticateRequest
            ////        {
            ////            // http://www.id.ee/?id=30340

            ////            // http://www.sk.ee/upload/files/DigiDocService_spec_est.pdf

            ////            // Autenditava isiku isikukood.
            ////            //Kohustuslik on kas IDCode v�i PhoneNo,
            ////            //soovitatav on kasutada m�lemat
            ////            //sisendparameetrit! Leedu Mobiil-ID kasutajate
            ////            //puhul on kohustuslikud IDCode ja PhoneNo
            ////            IDCode = "14212128025",

            ////            // Isikukoodi v�lja andnud riik, kasutatakse ISO 3166 
            ////            // 2 t�helisi riigikoode (n�iteks: EE)
            ////            CountryCode = "EE",



            ////            // Autenditava isiku telefoninumber koos riigikoodiga
            ////            //kujul +xxxxxxxxx (n�iteks +3706234566).
            ////            //Kui on m��ratud nii PhoneNo kui ka IDCode
            ////            //parameetrid, kontrollitakse telefoninumbri
            ////            //vastavust isikukoodile ja mittevastavuse korral
            ////            //tagastatakse SOAP veakood 301. Kohustuslik on
            ////            //kas IDCode v�i PhoneNo, soovitatav on kasutada
            ////            //m�lemat sisendparameetrit! Leedu Mobiil-ID
            ////            //kasutajate puhul on kohustuslikud IDCode ja
            ////            //PhoneNo (vt. peat�kk 5.2). Kui element �PhoneNo�
            ////            //on m��ratud, siis teenuse siseselt l�htutakse
            ////            //prefiksis m��ratud riigi tunnusest (s�ltumata
            ////            //elemendi "CountryCode" v��rtusest)
            ////            //PhoneNo = "+37200007"
            ////            PhoneNo = "37200007",

            ////            // Telefonile kuvatavate teadete keel. Kasutatakse: 3-
            ////            // t�helisi koode suurt�htedes.V�imalikud variandid:
            ////            //(EST, ENG, RUS, LIT).
            ////            Language = "EST",

            ////            // Autentimisel telefonil kuvatav teenuse nimetus,
            ////            //maksimaalne pikkus 20 t�hem�rki.
            ////            //Eelnevalt on vajalik kasutatava teenuse nimetuse
            ////            //kokkuleppimine teenuse pakkujaga
            ////            ServiceName = "Testimine",

            ////            //T�iendav tekst, mis autentimise PIN-i k�simise
            ////            //eelselt lisaks teenuse nimetuse kasutaja telefonile
            ////            //kuvatakse. Maksimaalne pikkus 40 baiti (ladina
            ////            //t�htede puhul t�hendab see �htlasi ka 40 s�mboli
            ////            //pikkust teksti, aga n�iteks kirillitsa teksti puhul
            ////            //v�idakse t�hti kodeerida 2 baidistena ja siis ei saa
            ////            //saata pikemat kui 20-s�mbolilist teksti).
            ////            MessageToDisplay = "Testimine",

            ////            //- Rakenduse pakkuja poolt genereeritud juhuslik 10
            ////            //baidine tekst, mis on osa (autentimise k�igus)
            ////            //kasutaja poolt signeeritavast s�numist.
            ////            //Edastatakse HEX stringina.
            ////            //NB! Suurema turvalisuse huvides on soovitatav
            ////            //see v�li alati t�ita, iga kord erineva juhusliku
            ////            //v��rtusega. Kui autentimine �nnestub, on
            ////            //soovitatav ka kontrollida, et kasutaja poolt
            ////            //allkirjastatud v��rtus t�epoolest ka sisaldab antud
            ////            //SPChallenge-i v��rtust. T�psem info viimase
            ////            //verifitseerimise kohta on peat�kis
            ////            //�GetMobileAuthenticateStatus�, �Signature�-
            ////            //elemendi kirjelduse all.

            ////            SPChallenge = "03010400000000000000",

            ////            // Autentimise toimingu vastuse tagastamise viis.
            ////            //V�imalikud variandid:
            ////            //- �asynchClientServer� � rakendus teeb p�rast
            ////            //MobileAuthenticate meetodi v�ljakutsumist
            ////            //t�iendavaid staatusp�ringuid (kasutades
            ////            //meetodit GetMobileAuthenticateStatus).
            ////            //- �asynchServerServer� � toimingu l�ppemisel
            ////            //v�i vea tekkimisel saadetakse vastus
            ////            //kliendirakendusele as�nkroonselt (vt.
            ////            //parameeter AsyncConfiguration).
            ////            MessagingMode = "asynchClientServer",

            ////            //AsyncConfiguration = 
            ////            // M��rab as�nkroonselt vastuse tagasisaatmise 
            ////            //konfiguratsiooni.Antud parameetri v��rtust
            ////            //kasutatakse ainult juhul kui MessagingMode on
            ////            //�asynchServerServer�. Konfiguratsioon lepitakse
            ////            //kokku teenuse kasutaja ja teenuse pakkuja vahel.
            ////            //Hetkel on toetatud vastuse tagasi saatmine
            ////            //kasutades Java Message Services(JMS) liidest


            ////            // Kui v��rtus on �TRUE�, tagastatakse vastuses
            ////            //autenditava isiku sertifikaat. Sertifikaat on vajalik,
            ////            //kui rakenduse pakkuja soovib talletada ja
            ////            //iseseisvalt kontrollida signatuuri korrektsust ja
            ////            //kehtivusinfot.

            ////            ReturnCertData = true,

            ////            //V��rtuse �TRUE� korral tagastatakse sertifikaadi
            ////            //kehtivusinfo vastuses RevocationData v�ljal.
            ////            ReturnRevocationData = false

            ////        });


            ////        //xa.GetAwaiter
            ////        var x = await xa;

            ////        // { UserIDCode = 14212128025 }
            ////        // x error CS0103: The name 'x' does not exist in the current context

            ////        // 102
            ////        // { UserIDCode = 14212128025 }




            ////        // { Sesscode = 976450644, Status = OK, UserIDCode = 14212128025, UserCN = TESTNUMBER,SEITSMES,14212128025, ChallengeID = 0029, Challenge = 03010400000000000000CC9C9010FF770270701D }

            ////        Console.WriteLine(new
            ////        {

            ////            // loodud sessiooni identifikaator
            ////            x.Sesscode,



            ////            // kasutaja autentimiseks tuleb
            ////            //teha t�iendavad staatusep�ringud kuni
            ////            //autentimistoimingu olek on �USER_AUTHENTICATED�
            ////            x.Status,

            ////            // Autenditava isiku isikukood. V��rtus v�etakse
            ////            //isikutuvastuse sertifikaadi eraldusnime �Serial Number�
            ////            //v�ljalt.
            ////            x.UserIDCode,

            ////            // Autenditava isiku isikutuvastuse sertifikaadi p�hinimi.
            ////            //V��rtus v�etakse isikutuvastuse sertifikaadi eraldusnime
            ////            //CN (Common Name) v�ljalt.
            ////            x.UserCN,

            ////            // 4 t�hem�rgiline kontrollkood, mis arvutatakse kasutaja
            ////            //                            telefonile signeerimiseks saadetava Challenge v��rtuse
            ////            //p�hjal.Antud kontrollkood tuleb mobiilautentimist
            ////            //v�imaldaval rakendusel kuvada kasutajale ja selle kaudu
            ////            //on v�imalik kasutajal veenduda p�ringu autentsuses.
            ////            x.ChallengeID,

            ////            //Kasutaja poolt autentimisel allkirjastatav s�num,
            ////            //koosneb rakenduse looja poolt saadetud s�numist
            ////            //(SPChallange, 10 baiti) ja teenuse poolt lisatust (samuti
            ////            //10 baiti). Tagastatakse vaid juhul, kui p�ringus
            ////            //SPChallenge v�li on v��rtustatud.

            ////            x.Challenge
            ////        });

            ////        ////}
            ////        ////catch (Exception err)
            ////        ////{
            ////        ////    // http://www.sk.ee/upload/files/DigiDocService_spec_est.pdf

            ////        ////    // { err = System.ServiceModel.FaultException: 102
            ////        ////    // M�ni kohustuslik sisendparameeter on m��ramata

            ////        ////    Console.WriteLine(new { err }
            ////        ////    );


            ////        ////}

            ////    }
            ////).Wait();



            // X:\jsc.svn\examples\java\android\Test\TestUDPSend\TestUDPSend\ApplicationActivity.cs

            // 2012desktop?

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );



            CLRProgram.CLRMain();
        }


    }


    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            MessageBox.Show("click to close");

        }
    }


}
