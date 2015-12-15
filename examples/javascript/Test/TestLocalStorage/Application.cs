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
using TestLocalStorage;
using TestLocalStorage.Design;
using TestLocalStorage.HTML.Pages;

namespace TestLocalStorage
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
            // http://caniuse.com/#search=localstorage


            new IHTMLPre { () => new { foo = Native.window.sessionStorage["foo"] } }.AttachToDocument();


            new IHTMLButton { "RED" }.AttachToDocument().onclick += delegate
            {
                Native.window.sessionStorage["foo"] = "RED";
            };

            new IHTMLButton { "clear" }.AttachToDocument().onclick += delegate
            {
                //Native.window.localStorage["foo"] = null;
                //Native.window.localStorage["foo"] = null;
                //Native.window.localStorage.removeItem("foo");
                Native.window.sessionStorage.removeItem("foo");
            };

            new { }.With(
                async delegate
                {
                    new IHTMLPre { "awaiting for foo..." }.AttachToDocument();

                    await sessionStorageAsync("foo");

                    new IHTMLPre { "awaiting for foo... done!" }.AttachToDocument();
                }
            );
        }

        public static Task<string> sessionStorageAsync(string key)
        {
            var x = new TaskCompletionSource<string> { };

            new { }.With(
                async delegate
                {
                    var z = default(string);

                    do
                    {
                        z = Native.window.sessionStorage[key];


                        await Native.window.async.onframe;
                    }
                    while (string.IsNullOrEmpty(z));

                    x.SetResult(z);
                }
            );

            return x.Task;
        }
    }
}
