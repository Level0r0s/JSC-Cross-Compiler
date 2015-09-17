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
using TestDelegateIfIfReturn;
using TestDelegateIfIfReturn.Design;
using TestDelegateIfIfReturn.HTML.Pages;

namespace TestDelegateIfIfReturn
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {



        static TaskCompletionSource<object> vsync0ambient;
        static TaskCompletionSource<object> vsync1renderman;


        static void nop() { }



        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            Native.window.onframe +=
            e =>
            {
                // Z:\jsc.svn\examples\javascript\test\TestDelegateIfIfReturn\Application.cs

                // let render man know..

                var flag1 = vsync1renderman != null;
                if (flag1)
                // this if block is not detected?
                {
                    // whats going on  with if clauses?
                    nop();

                    // wtf???
                    var flag0 = vsync1renderman.Task.IsCompleted;
                    if (flag0)
                        return;
                }
                if (vsync0ambient != null)
                    if (vsync0ambient.Task.IsCompleted)
                        return;

                Console.WriteLine("Native.window.onframe " + new { vsync1renderman, vsync0ambient });

                return;
            };



        }
    }
}

//// TestDelegateIfIfReturn.Application+<>c.<.ctor>b__3_0
//type$ni_by0Jy05D268ibj8Ka8FA.agAABpy05D268ibj8Ka8FA = function(b)
//{
//    var a = [this], c, d, e, f, g, h;

//    c = AgAABGElIzKyeEqXtzhaDg != null;
//    d = c;
//    AQAABmElIzKyeEqXtzhaDg();
//    e = AgAABGElIzKyeEqXtzhaDg.kAcABp_b1ITCbIktNs3el5Q().dgQABqwxMjO1zVAJb5WXKA();
//    f = e;

//    if (!f)
//    {
//        g = AQAABGElIzKyeEqXtzhaDg != null;
//        h = AQAABGElIzKyeEqXtzhaDg.kAcABp_b1ITCbIktNs3el5Q().dgQABqwxMjO1zVAJb5WXKA();

//        if (!h)
//        {
//            NyEABsIdETWGcpPJmN6_b2A(_6SEABvwogD2bZu0XDXN_a2g('Native.window.onframe ', new ctor$fQAABiwAQjOdIIVQWxvykw(AgAABGElIzKyeEqXtzhaDg, AQAABGElIzKyeEqXtzhaDg)));
//        }

//    }

//};
