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
using MobileAuthenticateExperiment;
using MobileAuthenticateExperiment.Design;
using MobileAuthenticateExperiment.HTML.Pages;
using System.Diagnostics;
using System.Data;

namespace MobileAuthenticateExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // can we have a website runnng on android server,
        // and have user authenticated with MID?

        // make sure to target 4.6 to enable async tasks
        // namesapce sk
        // first order of business. add https://tsp.demo.sk.ee/?wsdl 

        public Application(IApp page)
        {
            Native.window.onerror += e =>
            {
                new IHTMLPre { new { e.error, e.message } }.AttachToDocument();

            };

            // this will go full screen
            //var sheet1 = new UserControl1 { }.AttachControlToDocument();


            // wont load on S1, why?

            //- waiting for device -
            //error: protocol fault (status read)
            //- waiting for device -
            //error: more than one device and emulator

            //C:\Users\Arvo>x:\util\android-sdk-windows\platform-tools\adb.exe devices
            //List of devices attached
            //3330A17632C000EC        device

            var o = new IHTMLOutput { }.AttachToDocument();

            o.style.position = IStyle.PositionEnum.relative;
            o.style.display = IStyle.DisplayEnum.block;

            //width: 600px; height: 300px; display: block; position: relative; background: red;
            o.style.SetSize(800, 200);


            //var sheet1 = new UserControl1 { }.AttachControlTo(Native.document.body);
            var sheet1 = new UserControl1 { }.AttachControlTo(o);

            new { }.With(
                async delegate
                {
                    var enter = new IHTMLButton { "authenticate with MobileID as +37200007" }.AttachToDocument();


                    //sheet1.identitySheet1BindingSourceBindingSource.PositionChanged +=
                    sheet1.identitySheet1BindingSourceBindingSource.CurrentChanged +=
                        delegate
                        {
                            //var xDataRowView = sheet1.identitySheet1BindingSourceBindingSource.Current as DataRowView;
                            Data.identitySheet1Row row = sheet1.identitySheet1BindingSourceBindingSource.Current as DataRowView;



                            enter.innerText = "authenticate with MobileID as " + new
                            {
                                row.Tel_nr,
                                row.Isikukood,


                                //sheet1.identitySheet1BindingSourceBindingSource.Current
                                //row

                            };

                        };

                retry:
                    enter.disabled = false;

                    await enter.async.onclick;
                    {
                        Data.identitySheet1Row row = sheet1.identitySheet1BindingSourceBindingSource.Current as DataRowView;

                        enter.disabled = true;

                        // done { Sesscode = 1832228359, Status = OK, UserIDCode = 14212128025, UserCN = TESTNUMBER,SEITSMES,14212128025, ChallengeID = 0101, Challenge = 03010400000000000000F4EC792BF7FEB1263A65 }

                        var sw = Stopwatch.StartNew();
                        new IHTMLPre { () => new { sw.ElapsedMilliseconds, row.Tel_nr, row.Isikukood } }.AttachToDocument();

                        var x = await MobileID(PhoneNo: row.Tel_nr, IDCode: row.Isikukood);

                        new IHTMLPre { 
                        "done " + new{
                    
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
                    } }.AttachToDocument();


                        sw.Stop();


                    }
                    goto retry;
                }
            );
        }

    }

    public partial class ApplicationWebService
    {
        // { exc = System.NotSupportedException: The interface was not marked as [ScriptAttribute.ExplicitInterface]. { SourceInterface = MobileAuthenticateExperiment.sk.DigiDocServicePortType, SourceType = MobileAuthenticateExperiment.sk.DigiDocServicePortTypeClient, TargetMethod = MobileAuthenticateExperiment.sk.StartSessionResponse MobileAuthenticateExperiment.sk.DigiDocServicePortType.StartSession(MobileAuthenticateExperiment.sk.StartSessionRequest) }
        //[ScriptCoreLib.Script.ExplicitInterface]

        public async Task<sk.MobileAuthenticateResponse> MobileID(string PhoneNo = "37200007", string IDCode = "14212128025")
        {
            // Could not find endpoint element with name 'DigiDocService' and contract 'sk.DigiDocServicePortType' in the ServiceModel client configuration section.


#if !DEBUG
            var c = new sk.DigiDocServicePortTypeClient("DigiDocService", "https://tsp.demo.sk.ee:443");
#else
            // http://stackoverflow.com/questions/352654/could-not-find-default-endpoint-element
            var c = new sk.DigiDocServicePortTypeClient(
                new System.ServiceModel.BasicHttpsBinding(),
                new System.ServiceModel.EndpointAddress("https://tsp.demo.sk.ee:443")
            );
#endif

            // will it work on appengine, android  too?
            var xa = await c.MobileAuthenticateAsync(new sk.MobileAuthenticateRequest
            {
                // http://www.id.ee/?id=30340

                // http://www.sk.ee/upload/files/DigiDocService_spec_est.pdf

                // Autenditava isiku isikukood.
                //Kohustuslik on kas IDCode või PhoneNo,
                //soovitatav on kasutada mõlemat
                //sisendparameetrit! Leedu Mobiil-ID kasutajate
                //puhul on kohustuslikud IDCode ja PhoneNo
                //IDCode = "14212128025",
                IDCode = IDCode,

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
                //PhoneNo = "37200007",
                PhoneNo = PhoneNo,

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

            return xa;
        }
    }
}


//I/browser (15935): Console: 1ms css[type] { descendantMode = true } http://178.23.119.87:30297/view-source:53831
//I/browser (15935): Console: 262ms css[type] { descendantMode = true } http://178.23.119.87:30297/view-source:53831
//I/browser (15935): Console: 300ms css[type] { descendantMode = true } http://178.23.119.87:30297/view-source:53831
//I/browser (15935): Console: 333ms css[type] { descendantMode = true } http://178.23.119.87:30297/view-source:53831
//I/browser (15935): Console: 366ms css[type] { descendantMode = true } http://178.23.119.87:30297/view-source:53831
//I/browser (15935): Console: 459ms GetInternalFields load fromlocalstorage!  http://178.23.119.87:30297/view-source:53831
//I/browser (15935): Console: 704ms NewInstanceConstructor restore fields.. http://178.23.119.87:30297/view-source:53831
//I/browser (15935): Console: %c838ms event: enter new DataGridView() http://178.23.119.87:30297/view-source:53863
//E/browser (15935): Console: Uncaught Error: ArgumentNullException: source http://178.23.119.87:30297/view-source:24355
//V/browser (15935): BrowserActivity.onPageFinished: url = http://178.23.119.87:30297/webview = android.webkit.WebView@4050b5b0
//V/browser (15935): BrowserActivity.onProgressChanged: progress == 100 truefalsefalsefalse