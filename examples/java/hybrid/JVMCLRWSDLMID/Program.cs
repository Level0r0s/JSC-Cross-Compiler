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
using System.ServiceModel.Channels;
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
                //HttpRequestMessageProperty property = new HttpRequestMessageProperty();

                //var userAgent = "MyUserAgent/1.0.0.0";

                //if (request.Properties.Count == 0 || request.Properties[HttpRequestMessageProperty.Name] == null)
                //{
                //    var property = new HttpRequestMessageProperty();
                //    property.Headers["User-Agent"] = userAgent;
                //    request.Properties.Add(HttpRequestMessageProperty.Name, property);
                //}
                //else
                //{
                //    ((HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name]).Headers["User-Agent"] = userAgent;
                //}

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
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151212
        // Z:\jsc.svn\examples\javascript\crypto\VerifyIdentityAffinity\VerifyIdentityAffinity\Application.cs

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // only works on partial core?
            Console.WriteLine(typeof(object).AssemblyQualifiedName);


            // Z:\jsc.svn\examples\javascript\crypto\VerifyIdentityAffinity\VerifyIdentityAffinity\ApplicationWebService.cs

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

            //xPOST.Headers[HttpRequestHeader.ContentLength] = "" + data.Length;


            // +		InnerException	{"The 'Content-Length' header must be modified using the appropriate property or method.\r\nParameter name: name"}	System.Exception {System.ArgumentException}


            // http://referencesource.microsoft.com/#System/net/System/Net/webclient.cs
            //   internal UploadBitsState(WebRequest request, Stream readStream, byte[] buffer, int chunkSize, byte[] header, byte[] footer, CompletionDelegate uploadCompletionDelegate, CompletionDelegate downloadCompletionDelegate, AsyncOperation asyncOp, ProgressData progress, WebClient webClient) {

            // http://stackoverflow.com/questions/2131245/c-webclient-assigning-chunk-size-when-using-asyncuploaddata


            // tested on ubuntu/openjdk
#if TESTINGWEBCLIENT
            #region oldschool SOAP
            try
            {

                var xPOST = new WebClient();


                // http://stackoverflow.com/questions/287126/how-to-post-soap-request-from-net

                // https://magedfarag.wordpress.com/2012/12/14/call-wcf-sevice-using-only-webclient-and-soap-xml/

                //xPOST.Headers[HttpRequestHeader.ContentType] = "application/soap+xml;charset=UTF-8";
                //xPOST.Headers[HttpRequestHeader.ContentType] = "text/xml; charset=UTF-8";

                //{ Message = , StackTrace = java.lang.NullPointerException
                //        at JVMCLRWSDLMID.Program.main(Program.java:46)
                // }

                xPOST.Headers["Content-Type"] = "text/xml; charset=UTF-8";
                xPOST.Headers["SOAPAction"] = "\"\"";

                // webclient chunks if data is more than
                // Content-Length: 1023
                var data = @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/""><s:Body s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><q1:MobileAuthenticate xmlns:q1=""http://www.sk.ee/DigiDocService/DigiDocService_2_3.wsdl""><IDCode xsi:type=""xsd:string"">14212128025</IDCode><CountryCode xsi:type=""xsd:string"">EE</CountryCode><PhoneNo xsi:type=""xsd:string"">37200007</PhoneNo><Language xsi:type=""xsd:string"">EST</Language><ServiceName xsi:type=""xsd:string"">Testimine</ServiceName><MessageToDisplay xsi:type=""xsd:string"">Testimine</MessageToDisplay><SPChallenge xsi:type=""xsd:string"">03010400000000000000</SPChallenge><MessagingMode xsi:type=""xsd:string"">asynchClientServer</MessagingMode><AsyncConfiguration xsi:type=""xsd:int"">0</AsyncConfiguration><ReturnCertData xsi:type=""xsd:boolean"">true</ReturnCertData><ReturnRevocationData xsi:type=""xsd:boolean"">false</ReturnRevocationData></q1:MobileAuthenticate></s:Body></s:Envelope>".Trim();
               

                // { xPOSTresponse =  }
                //var xPOSTresponse = xPOST.UploadString("https://tsp.demo.sk.ee/", "POST", data);
                var xPOSTresponse = xPOST.UploadString("https://tsp.demo.sk.ee:443/", "POST", data);
                //var xPOSTresponse = xPOST.UploadString("https://digidocservice.sk.ee:443/", "POST", data);

                //xPOST.UploadStringTaskAsync
                //UploadString { err = javax.net.ssl.SSLHandshakeException: sun.security.validator.ValidatorException: PKIX path building failed: sun.security.provider.certpath.SunCertPathBuilderException: unable to find valid certification path to requested target }
                //exit UploadString { conn = sun.net.www.protocol.https.DelegateHttpsURLConnection:https://tsp.demo.sk.ee:443/ }

                //var xPOSTresponse = xPOST.UploadString("https://tsp.demo.sk.ee/", data);

                Console.WriteLine(new { xPOSTresponse });

                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151005
                var Envelope = XElement.Parse(xPOSTresponse);
                // <SOAP-ENV:Envelope xmlns:SOAP-ENV="http://schemas.xmlsoap.org/soap/envelope/" xmlns:SOAP-ENC="http://schemas.xmlsoap.org/soap/encoding/" xmlns:dig="http://www.sk.ee/DigiDocService/DigiDocService_2_3.wsdl" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"><SOAP-ENV:Header/>
                // <SOAP-ENV:Body SOAP-ENV:encodingStyle="http://schemas.xmlsoap.org/soap/encoding/"><dig:MobileAuthenticateResponse><Sesscode xsi:type="xsd:int">471237527</Sesscode><Status xsi:type="xsd:string">OK</Status><UserIDCode xsi:type="xsd:string">14212128025</UserIDCode>



                // Body = <SOAP-ENV:Header xmlns:SOAP-ENV="http://schemas.xmlsoap.org/soap/envelope/" />
                var Body = Envelope.Elements().Skip(1).FirstOrDefault();


                //Body = <SOAP-ENV:Body SOAP-ENV:encodingStyle="http://schemas.xmlsoap.org/soap/encoding/" xmlns:SOAP-ENV="http://schemas.xmlsoap.org/soap/envelope/">
                //  <dig:MobileAuthenticateResponse xmlns:dig="http://www.sk.ee/DigiDocService/DigiDocService_2_3.wsdl">
                //    <Sessc...


                // loooky isnt jsc serializing api calls the same way?
                var Response = Body.Elements().FirstOrDefault();

                var fields = Response.Elements();

                // { field = <UserIDCode xsi:type="xsd:string" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">14212128025</UserIDCode> }
                foreach (var field in fields)
                {
                    //Console.WriteLine(new { field });
                    Console.WriteLine(new { field.Name.LocalName, field.Value });
                }


                //
                //Console.WriteLine(new { Response });

            }
            catch (Exception err)
            {
                Console.WriteLine(new { err.Message, err.StackTrace });
            }
            #endregion

            Console.WriteLine("done");
            Console.ReadLine();
            return;
#endif


            try
            {
            // http://www.microsoft.com/en-us/download/details.aspx?id=44226

                // java.lang.reflect.Proxy to the rescue!

                // goto x1;



            x1:

                //System.ServiceModel.OperationContext.Current.
                //var c = new sk.DigiDocServicePortTypeClient();
                //var c = new sk.DigiDocServicePortTypeClient("DigiDocService", "https://tsp.demo.sk.ee:443");
                var c = new sk.DigiDocServicePortTypeClient("DigiDocService", "https://digidocservice.sk.ee:443");

                //var requestInterceptor = new InspectorBehavior();

                //c.Endpoint.EndpointBehaviors.Add(requestInterceptor);

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

                Console.WriteLine("invoke MobileAuthenticateAsync");
                var xa = c.MobileAuthenticateAsync(new sk.MobileAuthenticateRequest
                {
                    // http://www.id.ee/?id=30340

                    // http://www.sk.ee/upload/files/DigiDocService_spec_est.pdf

                    // Autenditava isiku isikukood.
                    //Kohustuslik on kas IDCode või PhoneNo,
                    //soovitatav on kasutada mõlemat
                    //sisendparameetrit! Leedu Mobiil-ID kasutajate
                    //puhul on kohustuslikud IDCode ja PhoneNo
                    //IDCode = "14212128025",
                    IDCode = "",

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
                    PhoneNo = "+37200007",

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

                sk.MobileAuthenticateResponse x = xa.Result;
                Console.WriteLine("after MobileAuthenticateAsync");

                // did we actually do wsdl coms or was it intercepted by core?
                // { Sesscode = 0, Status = , UserIDCode = , UserCN = , ChallengeID = , Challenge =  }
                // { Sesscode = 0, Status = OK, UserIDCode = 14212128025, UserCN = TESTNUMBER,SEITSMES,14212128025, ChallengeID = 0045, Challenge = 03010400000000000000EFC2B87DBF2FF80CE0AD }
                // { Sesscode = 2070798858, Status = OK, UserIDCode = 14212128025, UserCN = TESTNUMBER,SEITSMES,14212128025, ChallengeID = 0126, Challenge = 03010400000000000000A611E8BFBABA7F76E07E }

                Console.WriteLine(new
                {

                    // loodud sessiooni identifikaator
                    x.Sesscode,



                    // kasutaja autentimiseks tuleb
                    //teha täiendavad staatusepäringud kuni
                    //autentimistoimingu olek on “USER_AUTHENTICATED”
                    x.Status,

                    // Autenditava isiku isikukood. Väärtus võetakse
                    //isikutuvastuse sertifikaadi eraldusnime “Serial Number”
                    //väljalt.
                    x.UserIDCode,

                    // Autenditava isiku isikutuvastuse sertifikaadi põhinimi.
                    //Väärtus võetakse isikutuvastuse sertifikaadi eraldusnime
                    //CN (Common Name) väljalt.
                    x.UserCN,

                    // 4 tähemärgiline kontrollkood, mis arvutatakse kasutaja
                    //                            telefonile signeerimiseks saadetava Challenge väärtuse
                    //põhjal.Antud kontrollkood tuleb mobiilautentimist
                    //võimaldaval rakendusel kuvada kasutajale ja selle kaudu
                    //on võimalik kasutajal veenduda päringu autentsuses.
                    x.ChallengeID,

                    //Kasutaja poolt autentimisel allkirjastatav sõnum,
                    //koosneb rakenduse looja poolt saadetud sõnumist
                    //(SPChallange, 10 baiti) ja teenuse poolt lisatust (samuti
                    //10 baiti). Tagastatakse vaid juhul, kui päringus
                    //SPChallenge väli on väärtustatud.

                    x.Challenge
                });

            }
            catch (Exception err)
            {

                // err = {"Could not find endpoint element with name '' and contract 'sk.DigiDocServicePortType' in the ServiceModel client configuration section. This might be because no configuration file was found for your application, or because no endpoint element matching thi...

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
            ////            //Kohustuslik on kas IDCode või PhoneNo,
            ////            //soovitatav on kasutada mõlemat
            ////            //sisendparameetrit! Leedu Mobiil-ID kasutajate
            ////            //puhul on kohustuslikud IDCode ja PhoneNo
            ////            IDCode = "14212128025",

            ////            // Isikukoodi välja andnud riik, kasutatakse ISO 3166 
            ////            // 2 tähelisi riigikoode (näiteks: EE)
            ////            CountryCode = "EE",



            ////            // Autenditava isiku telefoninumber koos riigikoodiga
            ////            //kujul +xxxxxxxxx (näiteks +3706234566).
            ////            //Kui on määratud nii PhoneNo kui ka IDCode
            ////            //parameetrid, kontrollitakse telefoninumbri
            ////            //vastavust isikukoodile ja mittevastavuse korral
            ////            //tagastatakse SOAP veakood 301. Kohustuslik on
            ////            //kas IDCode või PhoneNo, soovitatav on kasutada
            ////            //mõlemat sisendparameetrit! Leedu Mobiil-ID
            ////            //kasutajate puhul on kohustuslikud IDCode ja
            ////            //PhoneNo (vt. peatükk 5.2). Kui element “PhoneNo”
            ////            //on määratud, siis teenuse siseselt lähtutakse
            ////            //prefiksis määratud riigi tunnusest (sõltumata
            ////            //elemendi "CountryCode" väärtusest)
            ////            //PhoneNo = "+37200007"
            ////            PhoneNo = "37200007",

            ////            // Telefonile kuvatavate teadete keel. Kasutatakse: 3-
            ////            // tähelisi koode suurtähtedes.Võimalikud variandid:
            ////            //(EST, ENG, RUS, LIT).
            ////            Language = "EST",

            ////            // Autentimisel telefonil kuvatav teenuse nimetus,
            ////            //maksimaalne pikkus 20 tähemärki.
            ////            //Eelnevalt on vajalik kasutatava teenuse nimetuse
            ////            //kokkuleppimine teenuse pakkujaga
            ////            ServiceName = "Testimine",

            ////            //Täiendav tekst, mis autentimise PIN-i küsimise
            ////            //eelselt lisaks teenuse nimetuse kasutaja telefonile
            ////            //kuvatakse. Maksimaalne pikkus 40 baiti (ladina
            ////            //tähtede puhul tähendab see ühtlasi ka 40 sümboli
            ////            //pikkust teksti, aga näiteks kirillitsa teksti puhul
            ////            //võidakse tähti kodeerida 2 baidistena ja siis ei saa
            ////            //saata pikemat kui 20-sümbolilist teksti).
            ////            MessageToDisplay = "Testimine",

            ////            //- Rakenduse pakkuja poolt genereeritud juhuslik 10
            ////            //baidine tekst, mis on osa (autentimise käigus)
            ////            //kasutaja poolt signeeritavast sõnumist.
            ////            //Edastatakse HEX stringina.
            ////            //NB! Suurema turvalisuse huvides on soovitatav
            ////            //see väli alati täita, iga kord erineva juhusliku
            ////            //väärtusega. Kui autentimine õnnestub, on
            ////            //soovitatav ka kontrollida, et kasutaja poolt
            ////            //allkirjastatud väärtus tõepoolest ka sisaldab antud
            ////            //SPChallenge-i väärtust. Täpsem info viimase
            ////            //verifitseerimise kohta on peatükis
            ////            //„GetMobileAuthenticateStatus“, „Signature“-
            ////            //elemendi kirjelduse all.

            ////            SPChallenge = "03010400000000000000",

            ////            // Autentimise toimingu vastuse tagastamise viis.
            ////            //Võimalikud variandid:
            ////            //- “asynchClientServer” – rakendus teeb pärast
            ////            //MobileAuthenticate meetodi väljakutsumist
            ////            //täiendavaid staatuspäringuid (kasutades
            ////            //meetodit GetMobileAuthenticateStatus).
            ////            //- “asynchServerServer” – toimingu lõppemisel
            ////            //või vea tekkimisel saadetakse vastus
            ////            //kliendirakendusele asünkroonselt (vt.
            ////            //parameeter AsyncConfiguration).
            ////            MessagingMode = "asynchClientServer",

            ////            //AsyncConfiguration = 
            ////            // Määrab asünkroonselt vastuse tagasisaatmise 
            ////            //konfiguratsiooni.Antud parameetri väärtust
            ////            //kasutatakse ainult juhul kui MessagingMode on
            ////            //“asynchServerServer”. Konfiguratsioon lepitakse
            ////            //kokku teenuse kasutaja ja teenuse pakkuja vahel.
            ////            //Hetkel on toetatud vastuse tagasi saatmine
            ////            //kasutades Java Message Services(JMS) liidest


            ////            // Kui väärtus on “TRUE”, tagastatakse vastuses
            ////            //autenditava isiku sertifikaat. Sertifikaat on vajalik,
            ////            //kui rakenduse pakkuja soovib talletada ja
            ////            //iseseisvalt kontrollida signatuuri korrektsust ja
            ////            //kehtivusinfot.

            ////            ReturnCertData = true,

            ////            //Väärtuse “TRUE” korral tagastatakse sertifikaadi
            ////            //kehtivusinfo vastuses RevocationData väljal.
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
            ////            //teha täiendavad staatusepäringud kuni
            ////            //autentimistoimingu olek on “USER_AUTHENTICATED”
            ////            x.Status,

            ////            // Autenditava isiku isikukood. Väärtus võetakse
            ////            //isikutuvastuse sertifikaadi eraldusnime “Serial Number”
            ////            //väljalt.
            ////            x.UserIDCode,

            ////            // Autenditava isiku isikutuvastuse sertifikaadi põhinimi.
            ////            //Väärtus võetakse isikutuvastuse sertifikaadi eraldusnime
            ////            //CN (Common Name) väljalt.
            ////            x.UserCN,

            ////            // 4 tähemärgiline kontrollkood, mis arvutatakse kasutaja
            ////            //                            telefonile signeerimiseks saadetava Challenge väärtuse
            ////            //põhjal.Antud kontrollkood tuleb mobiilautentimist
            ////            //võimaldaval rakendusel kuvada kasutajale ja selle kaudu
            ////            //on võimalik kasutajal veenduda päringu autentsuses.
            ////            x.ChallengeID,

            ////            //Kasutaja poolt autentimisel allkirjastatav sõnum,
            ////            //koosneb rakenduse looja poolt saadetud sõnumist
            ////            //(SPChallange, 10 baiti) ja teenuse poolt lisatust (samuti
            ////            //10 baiti). Tagastatakse vaid juhul, kui päringus
            ////            //SPChallenge väli on väärtustatud.

            ////            x.Challenge
            ////        });

            ////        ////}
            ////        ////catch (Exception err)
            ////        ////{
            ////        ////    // http://www.sk.ee/upload/files/DigiDocService_spec_est.pdf

            ////        ////    // { err = System.ServiceModel.FaultException: 102
            ////        ////    // Mõni kohustuslik sisendparameeter on määramata

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



            //CLRProgram.CLRMain();

            Console.WriteLine("done");
            Console.ReadLine();

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
