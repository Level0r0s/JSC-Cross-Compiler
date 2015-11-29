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

    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using TestSwitchToServiceContextAsync;


    #region HopToParent
    // Z:\jsc.svn\examples\javascript\Test\TestHopToIFrameAndBack\Application.cs
    // Z:\jsc.svn\examples\javascript\Test\TestHopFromIFrame\TestHopFromIFrame\Application.cs
    public struct HopToParent : System.Runtime.CompilerServices.INotifyCompletion
    {
        // basically we have to hibernate the current state to resume
        public HopToParent GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }

        public static Action<HopToParent, Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation) { VirtualOnCompleted(this, continuation); }

        public void GetResult() { }
    }
    #endregion


    #region HopToIFrame
    // Z:\jsc.svn\examples\javascript\Test\TestHopFromIFrame\TestHopFromIFrame\Application.cs
    public struct HopToIFrame : System.Runtime.CompilerServices.INotifyCompletion
    {
        // basically we have to hibernate the current state to resume
        public HopToIFrame GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }

        public static Action<HopToIFrame, Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation) { VirtualOnCompleted(this, continuation); }

        public void GetResult() { }


        public IHTMLIFrame frame;
        public static explicit operator HopToIFrame(IHTMLIFrame frame)
        {
            return new HopToIFrame { frame = frame };
        }
    }
    #endregion

    public sealed class Application : ApplicationWebService
    {
        #region magic
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151123/ubuntumidexperiment

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151105
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151031

        static Func<string, string> DecoratedString = x => x.Replace("-", "_").Replace("+", "_").Replace("<", "_").Replace(">", "_");

        // cuz state jumping wont restore in memory refs yet...
        static IHTMLIFrame iframe;
        #endregion


        static ApplicationWebService __base;

        public Application(IApp page)
        {
            // per iframe there is one only.
            // we should be respawning async state this pointer while jmping into iframe.
            __base = this;

            #region  magic
            var isroot = Native.window.parent == Native.window.self;

            //new IHTMLPre { new { isroot } }.AttachToDocument();

            if (!isroot)
            {
                #region HopToParent
                HopToParent.VirtualOnCompleted = async (that, continuation) =>
                {
                    // the state is in a member variable?

                    var r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);

                    // should not be a zero state
                    // or do we have statemachine name clash?

                    //new IHTMLPre {
                    //    "iframe about to jump to parent " + new { r.shadowstate.state }
                    //}.AttachToDocument();

                    Native.window.parent.postMessage(r.shadowstate);

                    // we actually wont use the response yet..
                };
                #endregion


                // start the handshake
                // we gain intellisense, but the type is partal, likely not respawned, acivated, initialized 

                //  new IHTMLPre {
                //  "inside iframe awaiting state"
                //}.AttachToDocument();




                var c = new MessageChannel();

                c.port1.onmessage +=
                    m =>
                    {
                        var that = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)m.data;

                        //new IHTMLPre {
                        //            "inside iframe got state " +
                        //            new { that.state }
                        //      }.AttachToDocument();

                        // about to invoke

                        #region xAsyncStateMachineType
                        var xAsyncStateMachineType = typeof(Application).Assembly.GetTypes().FirstOrDefault(
                              item =>
                              {
                                  // safety check 1

                                  //Console.WriteLine(new { sw.ElapsedMilliseconds, item.FullName });

                                  var xisIAsyncStateMachine = typeof(IAsyncStateMachine).IsAssignableFrom(item);
                                  if (xisIAsyncStateMachine)
                                  {
                                      //Console.WriteLine(new { item.FullName, isIAsyncStateMachine });

                                      return item.FullName == that.TypeName;
                                  }

                                  return false;
                              }
                          );
                        #endregion


                        var NewStateMachine = FormatterServices.GetUninitializedObject(xAsyncStateMachineType);
                        var isIAsyncStateMachine = NewStateMachine is IAsyncStateMachine;

                        var NewStateMachineI = (IAsyncStateMachine)NewStateMachine;

                        #region 1__state
                        xAsyncStateMachineType.GetFields(
                                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                                ).WithEach(
                                 AsyncStateMachineSourceField =>
                                 {

                                     Console.WriteLine(new { AsyncStateMachineSourceField });

                                     if (AsyncStateMachineSourceField.Name.EndsWith("1__state"))
                                     {
                                         AsyncStateMachineSourceField.SetValue(
                                             NewStateMachineI,
                                             that.state
                                          );
                                     }

                                     var xStringField = that.StringFields.AsEnumerable().FirstOrDefault(
                                         f => DecoratedString(f.FieldName) == DecoratedString(AsyncStateMachineSourceField.Name)
                                     );

                                     if (xStringField != null)
                                     {
                                         // once we are to go back to client. we need to reverse it?

                                         AsyncStateMachineSourceField.SetValue(
                                             NewStateMachineI,
                                             xStringField.value
                                          );
                                         // next xml?
                                         // before lets send our strings back with the new state!
                                         // what about exceptions?
                                     }
                                 }
                            );
                        #endregion

                        //new IHTMLPre {
                        //        "inside iframe invoke state"
                        //    }.AttachToDocument();

                        NewStateMachineI.MoveNext();

                        // we can now send one hop back?
                    };

                c.port1.start();
                c.port2.start();

                Native.window.parent.postMessage(null,
                    "*",
                    c.port2
                );





                return;
            }

            var lookup = new Dictionary<IHTMLIFrame, MessageEvent> { };

            #region HopToIFrame
            HopToIFrame.VirtualOnCompleted = async (that, continuation) =>
            {
                var m = default(MessageEvent);
                var r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);

                if (lookup.ContainsKey(that.frame))
                {
                    //    new IHTMLPre {
                    //    "parent already nows the iframe..."
                    //}.AttachToDocument();

                    m = lookup[that.frame];

                }
                else
                {
                    //    new IHTMLPre {
                    //    "parent is awaiting handshake of the newly loaded iframe..."
                    //}.AttachToDocument();


                    // X:\jsc.svn\examples\javascript\Test\TestSwitchToIFrame\TestSwitchToIFrame\Application.cs
                    //var m = await that.frame.contentWindow.async.onmessage;
                    m = await that.frame.async.onmessage;

                    lookup[that.frame] = m;



                    #region onmessage
                    that.frame.ownerDocument.defaultView.onmessage +=
                        e =>
                        {
                            if (e.source != that.frame.contentWindow)
                                return;

                            var shadowstate = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)e.data;

                            // are we jumping into a new statemachine?

                            //      new IHTMLPre {
                            //      "parent saw iframe instructions to jump back " + new { shadowstate.state }
                            //}.AttachToDocument();

                            // about to invoke

                            #region xAsyncStateMachineType
                            var xAsyncStateMachineType = typeof(Application).Assembly.GetTypes().FirstOrDefault(
                                  item =>
                                  {
                                      // safety check 1

                                      //Console.WriteLine(new { sw.ElapsedMilliseconds, item.FullName });

                                      var xisIAsyncStateMachine = typeof(IAsyncStateMachine).IsAssignableFrom(item);
                                      if (xisIAsyncStateMachine)
                                      {
                                          //Console.WriteLine(new { item.FullName, isIAsyncStateMachine });

                                          return item.FullName == shadowstate.TypeName;
                                      }

                                      return false;
                                  }
                              );
                            #endregion


                            var NewStateMachine = FormatterServices.GetUninitializedObject(xAsyncStateMachineType);
                            var isIAsyncStateMachine = NewStateMachine is IAsyncStateMachine;

                            var NewStateMachineI = (IAsyncStateMachine)NewStateMachine;

                            #region 1__state
                            xAsyncStateMachineType.GetFields(
                                        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                                    ).WithEach(
                                     AsyncStateMachineSourceField =>
                                     {

                                         Console.WriteLine(new { AsyncStateMachineSourceField });

                                         if (AsyncStateMachineSourceField.Name.EndsWith("1__state"))
                                         {
                                             AsyncStateMachineSourceField.SetValue(
                                                 NewStateMachineI,
                                                 shadowstate.state
                                              );
                                         }

                                         var xStringField = shadowstate.StringFields.AsEnumerable().FirstOrDefault(
                                               f => DecoratedString(f.FieldName) == DecoratedString(AsyncStateMachineSourceField.Name)
                                           );

                                         if (xStringField != null)
                                         {
                                             // once we are to go back to client. we need to reverse it?

                                             AsyncStateMachineSourceField.SetValue(
                                                 NewStateMachineI,
                                                 xStringField.value
                                              );
                                             // next xml?
                                             // before lets send our strings back with the new state!
                                             // what about exceptions?
                                         }
                                     }
                                );
                            #endregion

                            //      new IHTMLPre {
                            //      "parent saw iframe instructions to jump back. invoking... "
                            //}.AttachToDocument();

                            NewStateMachineI.MoveNext();

                        };
                    #endregion

                }




                //new IHTMLPre {
                //    "parent is sending state after handshake..."
                //}.AttachToDocument();


                m.postMessage(r.shadowstate);
            };
            #endregion


            #endregion



            // can we log in via MID?
            // where is the last test?
            // Z:\jsc.svn\examples\javascript\crypto\VerifyIdentityAffinity\VerifyIdentityAffinity\Application.cs

            // can we modify roslyn to compile comments into IL?
            new IHTMLPre { "guest, EID, MID. which are we?" }.AttachToDocument();
            new IHTMLPre { new { Native.document.location.protocol, Native.document.location.host } }.AttachToDocument();
            // NFC ?
            new IHTMLPre { new { Native.window.navigator.userAgent } }.AttachToDocument();

            var hostname = Native.document.location.host.TakeUntilIfAny(":");
            var hostport = Native.document.location.host.SkipUntilOrEmpty(":");

            if (string.IsNullOrEmpty(hostport))
                hostport = "443";


            new IHTMLPre { new { base.identity, hostname, hostport } }.AttachToDocument();

            // { identity = { value = guest, signature = 70d1638ccb1627209f7d5751b989dd5cc399ff17c72aff075f2e05ff1b3c9a1f474cf5813c6470b8e9ee77b5911316acee62c6bf3534b2bc4942bc9de4344fc9 } }

            // doesnt jsc assetslibary do public key?
            // should we want to validate the identity on client?
            // can we? if we run on http we cant.



            new IHTMLButton { "EID" }.AttachToDocument().With(
                async e =>
                {
                    #region statemachine fixup?
                    await Task.CompletedTask;
                    #endregion

                    // are we running nfc web browser?
                    if (Native.window.navigator.userAgent.Contains("NFCDID"))
                        e.innerText = "NFC DID";


                    // NFC ?
                    await e.async.onclick;

                    Native.document.body.Clear();

                    // https://technet.microsoft.com/en-us/library/dd979547(v=ws.10).aspx
                    new IHTMLPre { e.innerText + " - Please insert smart card..." }.AttachToDocument();

                    // need hopping support.

                    var hostport1 = Convert.ToInt32(hostport) + 1;
                    var host1 = hostname + ":" + hostport1;
                    var baseURI1 = "https://" + host1;

                    iframe = new IHTMLIFrame { src = baseURI1, frameBorder = "0" }.AttachToDocument();

                    // if the iframe is on another port, ssl client certificate may be prompted
                    //await (HopTo)iframe;
                    await (HopToIFrame)iframe;

                    // ding ding ding.

                    //new IHTMLPre { "hello __base?" }.AttachToDocument();



                    //var xidenity_value = base.identity.value;
                    //var xidenity_signature = base.identity.signature;

                    var xidenity_value = __base.identity.value;
                    //var xidenity_signature = __base.identity.signature;

                    // jsc wont do byte array jumps yet?
                    var xidenity_signature = Convert.ToBase64String(__base.identity.signature);


                    await default(HopToParent);

                    //new IHTMLPre { new { xidenity_value, xidenity_signature, base.identity } }.AttachToDocument();
                    //new IHTMLPre { new { xidenity_value, xidenity_signature, __base.identity } }.AttachToDocument();


                    __base.identity = new VerifiableString
                    {
                        value = xidenity_value,
                        signature = Convert.FromBase64String(xidenity_signature)
                    };

                    __base.foo = "bar";

                    Native.document.body.Clear();
                    new IHTMLPre { new { __base.identity, __base.foo } }.AttachToDocument();

                    // IE cant verify?

                    // verify our new identity.

                    await new IHTMLButton { "Verify" }.AttachToDocument().async.onclick;
                    Native.document.body.style.backgroundColor = "yellow";
                    if (await __base.Verify())
                        Native.document.body.style.backgroundColor = "cyan";
                    else
                        Native.document.body.style.backgroundColor = "red";


                    //await new IHTMLButton { "close" }.AttachToDocument().async.onclick;

                    //// cant close?
                    //Native.window.close();
                }
            );

            new IHTMLButton { "MID" }.AttachToDocument().With(
              async e =>
              {
                  await e.async.onclick;

                  Native.document.body.Clear();

                  new IHTMLPre { "MID login..." }.AttachToDocument();

                  var state = await base.MobileAuthenticateAsync15();

                  if (state == null)
                  {
                      new IHTMLPre { "fault" }.AttachToDocument();
                      return;
                  }

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

                  await new IHTMLButton { "Verify" }.AttachToDocument().async.onclick;
                  Native.document.body.style.backgroundColor = "yellow";
                  if (await __base.Verify())
                      Native.document.body.style.backgroundColor = "cyan";
                  else
                      Native.document.body.style.backgroundColor = "red";
              }
          );
        }

    }

    public class ApplicationWebService
    {
        public string foo;

        public VerifiableString identity = new VerifiableString { value = "guest" }.Sign(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters);

        public async Task<bool> Verify()
        {
            Console.WriteLine("Verify " + new { identity });

            var verify = identity.Verify(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters);
            return verify;
        }


        const string serviceuri = "https://digidocservice.sk.ee:443";
        //const string serviceuri = "https://tsp.demo.sk.ee:443";
        //const string ServiceName = "Testimine";
        const string ServiceName = "Estfeed_Mikro";



        //ServiceName = "Testimine",
        //ServiceName = "Estfeed001",


        #region magic
        public void Handler(ScriptCoreLib.Ultra.WebService.WebServiceHandler h)
        {
            // ssl handshake gives certificate to global, it gives it to the handler, we give it to UI

            // Console.WriteLine("enter Handler " + new { h.ClientCertificate });

            h.ClientCertificate.With(
                c =>
                {
                    //this.id = new { c.Subject }.ToString();
                    //this.status.Value = this.id;

                    Console.WriteLine("WebServiceHandler " + new { h.ClientCertificate.Subject });


                    var UserIDCode = c.Subject.SkipUntilOrEmpty("SERIALNUMBER=").TakeUntilOrEmpty(",");

                    this.identity = new VerifiableString { value = UserIDCode }.Sign(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters);
                });

        }
        #endregion





        #region MobileAuthenticateAsync15
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

            var sw = Stopwatch.StartNew();
            Console.WriteLine("invoke MobileAuthenticateAsync " + new { sw.ElapsedMilliseconds, serviceuri, ServiceName });



            // Add Service Reference
            // https://tsp.demo.sk.ee/?wsdl 
            //var c = new sk.DigiDocServicePortTypeClient("DigiDocService", "https://tsp.demo.sk.ee:443");
            //var c = new sk.DigiDocServicePortTypeClient(new BasicHttpsBinding(), new EndpointAddress("https://tsp.demo.sk.ee:443"));
            var c = new sk.DigiDocServicePortTypeClient(new BasicHttpsBinding(), new EndpointAddress(serviceuri));




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


                //IDCode = "14212128025",

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
                //ServiceName = "Testimine",
                ServiceName = ServiceName,

                //Täiendav tekst, mis autentimise PIN-i küsimise
                //eelselt lisaks teenuse nimetuse kasutaja telefonile
                //kuvatakse. Maksimaalne pikkus 40 baiti (ladina
                //tähtede puhul tähendab see ühtlasi ka 40 sümboli
                //pikkust teksti, aga näiteks kirillitsa teksti puhul
                //võidakse tähti kodeerida 2 baidistena ja siis ei saa
                //saata pikemat kui 20-sümbolilist teksti).
                //MessageToDisplay = "Testimine",
                MessageToDisplay = "minuenergia.ee",

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

            if (x == null)
            {
                Console.WriteLine("after MobileAuthenticateAsync fault");

                return null;
            }

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

            //var c = new sk.DigiDocServicePortTypeClient(new BasicHttpsBinding(), new EndpointAddress("https://tsp.demo.sk.ee:443"));
            var c = new sk.DigiDocServicePortTypeClient(new BasicHttpsBinding(), new EndpointAddress(serviceuri));

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

            //NB! Enne esimese staatuse päringu saatmist on soovitatav oodata vähemalt 15
            //sekundit kuna autentimise protsess ei saa tehniliste ja inimlike piirangute tõttu
            //kiiremini lõppeda. Mobiil-ID toimingud aeguvad hiljemalt 4 minuti jooksul.

            if (xGetMobileAuthenticateStatusResponse.Status == "USER_AUTHENTICATED")
            {
                this.identity = new VerifiableString { value = args.MobileAuthenticateAsync15_UserIDCode.value }.Sign(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters);
            }

            return new MobileAuthenticateAsync15State
            {
                MobileAuthenticateAsync15Continue_Status = new VerifiableString { value = xGetMobileAuthenticateStatusResponse.Status }.Sign(NamedKeyPairs.WebServiceAuthorityPrivateKey.RSAParameters)
            };
        }


        #endregion
    }
}

