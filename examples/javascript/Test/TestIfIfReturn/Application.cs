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
using TestIfIfReturn;
using TestIfIfReturn.Design;
using TestIfIfReturn.HTML.Pages;

namespace TestIfIfReturn
{
    //type$Y9ZyDOUaCzCDriZNhC_bFFA.AgAABuUaCzCDriZNhC_bFFA = function (b)
    // {
    //   this.constructor.apply(this);
    //   var a = [this], c, d, e;

    //   a[0].AwAABtDhDzCJA639reMAAQ();
    //   c = !(AgAABOUaCzCDriZNhC_bFFA == null);
    //   e = !c;

    //   if (!e)
    //   {
    //     AQAABuUaCzCDriZNhC_bFFA();
    //     d = AgAABOUaCzCDriZNhC_bFFA.kAcABp_b1ITCbIktNs3el5Q().dgQABqwxMjO1zVAJb5WXKA();
    //     e = !d;

    //     if (!e)
    //     {
    //       return;
    //     }

    //   }

    //   e = (AQAABOUaCzCDriZNhC_bFFA == null);

    //   if (!e)
    //   {
    //     e = !AQAABOUaCzCDriZNhC_bFFA.kAcABp_b1ITCbIktNs3el5Q().dgQABqwxMjO1zVAJb5WXKA();

    //     if (!e)
    //     {
    //       return;
    //     }

    //   }

    //   NyEABsIdETWGcpPJmN6_b2A(_6SEABvwogD2bZu0XDXN_a2g('Native.window.onframe ', new ctor$egAABgICKzWKEBHtd_adcGA(AgAABOUaCzCDriZNhC_bFFA, AQAABOUaCzCDriZNhC_bFFA)));
    // };

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
            // Z:\jsc.svn\examples\javascript\test\TestDelegateIfIfReturn\Application.cs

            // let render man know..

            var flag1 = vsync1renderman != null;
            //does nonrolsyn have also the problem with rewriter?
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
        }

    }
}

//// TestIfIfReturn.Application..ctor
//type$Y9ZyDOUaCzCDriZNhC_bFFA.AgAABuUaCzCDriZNhC_bFFA = function(b)
//{
//    this.constructor.apply(this);
//    var a = [this], c, d, e, f, g, h;

//    a[0].AwAABtDhDzCJA639reMAAQ();
//    c = AgAABOUaCzCDriZNhC_bFFA != null;
//    d = c;
//    AQAABuUaCzCDriZNhC_bFFA();
//    e = AgAABOUaCzCDriZNhC_bFFA.kAcABp_b1ITCbIktNs3el5Q().dgQABqwxMjO1zVAJb5WXKA();
//    f = e;

//    if (!f)
//    {
//        g = AQAABOUaCzCDriZNhC_bFFA != null;
//        h = AQAABOUaCzCDriZNhC_bFFA.kAcABp_b1ITCbIktNs3el5Q().dgQABqwxMjO1zVAJb5WXKA();

//        if (!h)
//        {
//            NyEABsIdETWGcpPJmN6_b2A(_6SEABvwogD2bZu0XDXN_a2g('Native.window.onframe ', new ctor$egAABgICKzWKEBHtd_adcGA(AgAABOUaCzCDriZNhC_bFFA, AQAABOUaCzCDriZNhC_bFFA)));
//        }

//    }

//};