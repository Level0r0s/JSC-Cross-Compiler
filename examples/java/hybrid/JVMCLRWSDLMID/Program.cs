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
                        // http://www.sk.ee/upload/files/DigiDocService_spec_est.pdf

                        // { err = System.ServiceModel.FaultException: 102
                        // M�ni kohustuslik sisendparameeter on m��ramata

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
