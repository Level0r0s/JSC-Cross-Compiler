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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRWSDLMID
{
    public class Class1
    {
        public Class1()
        {
        }
    }

    public class Class1<T>
    {
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

            Task.Run(
                async delegate
                {
                    var c = new sk.DigiDocServicePortTypeClient();


                    //c.MobileAuthenticate()


                    try
                    {
                        //    function &MobileAuthenticate($IDCode, $CountryCode, $PhoneNo, $Language, $ServiceName, $MessageToDisplay, $SPChallenge, $MessagingMode, $AsyncConfiguration, $ReturnCertData, $ReturnRevocationData)
                        // MobileAuthenticate("", "", $tel_no, $lang, "Testimine", "", "00000000000000000000","asynchClientServer", NULL, true, FALSE);

                        var x = await c.MobileAuthenticateAsync(new sk.MobileAuthenticateRequest
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

                        // { UserIDCode = 14212128025 }
                        // x error CS0103: The name 'x' does not exist in the current context

                        // 102
                        // { UserIDCode = 14212128025 }




                        // { Sesscode = 976450644, Status = OK, UserIDCode = 14212128025, UserCN = TESTNUMBER,SEITSMES,14212128025, ChallengeID = 0029, Challenge = 03010400000000000000CC9C9010FF770270701D }

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
                        // http://www.sk.ee/upload/files/DigiDocService_spec_est.pdf

                        // { err = System.ServiceModel.FaultException: 102
                        // Mõni kohustuslik sisendparameeter on määramata

                        Console.WriteLine(new { err }
                        );


                    }

                }
            ).Wait();



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
