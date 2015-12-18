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
using TestHashChange;
using TestHashChange.Design;
using TestHashChange.HTML.Pages;

namespace TestHashChange
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //new IHTMLPre { () => "loaded at " + new { DateTime.Now } }.AttachToDocument();
            new IHTMLPre { "loaded at " + new { DateTime.Now } }.AttachToDocument();

            Native.css[IHTMLElement.HTMLElementEnum.a].style.display = IStyle.DisplayEnum.block;




            new { }.With(
                async delegate
                {
                    var foo = new IHTMLAnchor { href = "#foo", innerText = "#foo" }.AttachToDocument();

                    var a = new AsyncHash { hash = "#foo" };

                    new IHTMLButton { "go foo" }.AttachToDocument().onclick += delegate
                    {
                        Native.document.location.hash = a.hash;
                    };



                    while (await a.onenter)
                    {
                        new IHTMLPre { "onenter" }.AttachToDocument();
                        foo.style.backgroundColor = "yellow";


                        await a.onexit;

                        new IHTMLPre { "onexit" }.AttachToDocument();
                        foo.style.backgroundColor = "";
                    }

                }
            );


            new { }.With(
              async delegate
              {
                  var bar = new IHTMLAnchor { href = "#bar", innerText = "#bar" }.AttachToDocument();

                  var a = new AsyncHash { hash = "#bar" };

                  new IHTMLButton { "go bar" }.AttachToDocument().onclick += delegate
                  {
                      //Native.document.location.hash = a.hash;

                      a.Go();
                  };




                  while (await a.onenter)
                  {
                      //new IHTMLPre { "onenter" }.AttachToDocument();
                      bar.style.backgroundColor = "cyan";


                      // using
                      var goback = new IHTMLButton { "go back" }.AttachToDocument();

                      goback.onclick += delegate
                      {
                          //Native.document.location.hash = a.hash;

                          //a.Go();

                          Native.window.history.back();
                      };

                      await a.onexit;

                      goback.Dispose();

                      //new IHTMLPre { "onexit" }.AttachToDocument();
                      bar.style.backgroundColor = "";
                  }

              }
          );


            new IHTMLAnchor { href = "", innerText = "go home with reload" }.AttachToDocument();
            //new IHTMLAnchor { href = "/", innerText = "go home" }.AttachToDocument();
            new IHTMLAnchor { href = "#", innerText = "go home" }.AttachToDocument();

            Native.window.onhashchange += delegate
            {
                new IHTMLPre { new { Native.document.location.hash } }.AttachToDocument();
            };


            // await hash of


            // await cancel
        }

    }

    public class AsyncHashEvent
    {
        public static implicit operator bool(AsyncHashEvent e)
        {
            return e != null;
        }
    }

    public class AsyncHash
    {
        public string hash;

        public void Go()
        {
            Native.document.location.hash = this.hash;
        }

        // name it ?
        public Task<AsyncHashEvent> onenter
        {
            get
            {
                var x = new TaskCompletionSource<AsyncHashEvent> { };


                if (this.hash == Native.document.location.hash)
                {
                    x.SetResult(new AsyncHashEvent { });
                }
                else
                {

                    Native.window.onhashchange += delegate
                    {
                        // can jsc detect this event should be unsubscribed?
                        if (x == null)
                            return;

                        Console.WriteLine("onhashchange " + new { Native.document.location.hash });


                        if (this.hash == Native.document.location.hash)
                        {
                            x.SetResult(new AsyncHashEvent { });
                        }

                        x = null;
                    };
                }

                return x.Task;
            }
        }

        public Task<AsyncHashEvent> onexit
        {
            get
            {
                var x = new TaskCompletionSource<AsyncHashEvent> { };

                if (this.hash != Native.document.location.hash)
                {
                    x.SetResult(new AsyncHashEvent { });
                }
                else
                {
                    Native.window.onhashchange += delegate
                    {
                        // can jsc detect this event should be unsubscribed?
                        if (x == null)
                            return;

                        // view-source:55134 0ms onhashchange { href = https://192.168.1.12:7522/#foo }

                        Console.WriteLine("onhashchange " + new { Native.document.location.hash });



                        if (this.hash != Native.document.location.hash)
                        {
                            x.SetResult(new AsyncHashEvent { });
                        }

                        x = null;
                    };
                }

                return x.Task;
            }
        }
    }
}
