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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VerifyIdentityAffinity;
using VerifyIdentityAffinity.Design;
using VerifyIdentityAffinity.HTML.Pages;

namespace VerifyIdentityAffinity
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151105/namedkey


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // 4.6
            new { }.With(
                async delegate
                {

                    //var s = new IHTMLPre { "n/a" }.AttachToDocument();
                    status = new IHTMLPre { "n/a" }.AttachToDocument();
                    // signed by server?

                    await base.GetStatus();

                    #region Verify
                    new IHTMLButton { "Verify" }.AttachToDocument().With(
                        async button =>
                        {
                            // have server check if browser changed ui data?
                            while (await button)
                            {
                                Native.document.documentElement.style.borderLeft = "2px solid yellow";

                                if (await Verify())
                                    Native.document.documentElement.style.borderLeft = "2px solid green";
                                else
                                    Native.document.documentElement.style.borderLeft = "2px solid red";
                            }
                        }
                    );
                    #endregion


                    //await base.GetStatus(s.AsXElement());

                    new IHTMLButton { "log in EID" }.AttachToDocument();
                    // jump to the other port
                    // and have browser ask for pin
                    // get the claim signed by the webservice

                    new IHTMLButton { "log in MID" }.AttachToDocument().With(
                        async MID =>
                        {

                            await MID;

                            //Native.document.body.css.button.disabled = true;
                            //Native.document.body.css.button.style.display = none;
                            //Native.document.body.css.button.style.display = IStyle.DisplayEnum.none;

                            MID.disabled = true;


                            Native.document.documentElement.style.borderLeft = "2px solid yellow";

                            if (await base.MobileAuthenticateAsync())
                            {
                                Native.document.documentElement.style.borderLeft = "2px solid green";
                            }
                            else
                            {
                                Native.document.documentElement.style.borderLeft = "2px solid red";
                            }

                        }
                    );

                    new IHTMLButton { "log in MID15" }.AttachToDocument().With(
                      async MID =>
                      {

                          await MID;

                          //Native.document.body.css.button.disabled = true;
                          //Native.document.body.css.button.style.display = none;
                          //Native.document.body.css.button.style.display = IStyle.DisplayEnum.none;

                          MID.disabled = true;


                          Native.document.documentElement.style.borderLeft = "2px solid yellow";

                          await base.MobileAuthenticateAsync15();

                          Native.document.documentElement.style.borderLeft = "2px solid cyan";

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

                          if (await base.MobileAuthenticateAsync15Continue())
                          {
                              Native.document.documentElement.style.borderLeft = "2px solid green";
                          }
                          else
                          {
                              Native.document.documentElement.style.borderLeft = "2px solid red";
                          }

                      }
                  );


                    // 

                    // by now we should have the identity
                    // and the claim is signed by the web service.

                    // both the client and the webservice can verify the claim.
                    // as server is stateless



                }
           );
        }

    }
}
