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
using TestApplicationTypeInfo;
using TestApplicationTypeInfo.Design;
using TestApplicationTypeInfo.HTML.Pages;

namespace TestApplicationTypeInfo
{
    // Error	4	Adding a 'class/struct/interface' will prevent the debug session from continuing while Edit and Continue is enabled.	Z:\jsc.svn\examples\javascript\ubuntu\Test\TestApplicationTypeInfo\TestApplicationTypeInfo\Application.cs	23	5	TestApplicationTypeInfo
    //class StaticNewClass
    //{ 
    //}

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // the fieldinit is part of the ctor which wont be rerun after a server call
        // for historic updates, we would need to track if the source changed to change the deriviate
        public static string foo = "foo";


        public static string GetBar()
        {
            // Error	1	Adding a statement which contains an anonymous type will prevent the debug session from continuing while Edit and Continue is enabled.	Z:\jsc.svn\examples\javascript\ubuntu\Test\TestApplicationTypeInfo\TestApplicationTypeInfo\Application.cs	37	13	TestApplicationTypeInfo
            // can we change IL while we are debugging yet?

            //var x = new { hello = "world" };

            return "bar223. changed after monitoring for changes.. ";
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new IHTMLButton { "simple button 1 for ENC test" }.AttachToDocument().onclick +=
                delegate
                {
                    // can ENC change ldstr here and can we update clientside?
                    new IHTMLPre { "onclick" }.AttachToDocument();
                };

            new IHTMLButton { "simple button 2 for ENC test" }.AttachToDocument().onclick +=
              delegate
              {
                  // can ENC change ldstr here and can we update clientside?
                  new IHTMLPre { "onclick " + new { foo } }.AttachToDocument();
              };


            new IHTMLButton { "simple button 3 GetBar for ENC test" }.AttachToDocument().onclick +=
            delegate
            {
                // can ENC change ldstr here and can we update clientside?
                new IHTMLPre { "onclick " + new { abr = GetBar() } }.AttachToDocument();
            };


            new { }.With(
                async delegate
                {


                    await new IHTMLButton { "InspectApplication" }.AttachToDocument().async.onclick;

                    await InspectApplication(
                        AfterChangeDetected: SourceMethod =>
                        {
                            var x = new IHTMLButton { "IL was changed for " + new { SourceMethod } }.AttachToDocument();

                            new IStyle(x)
                            {
                                position = IStyle.PositionEnum.@fixed,
                                left = "0",
                                top = "0",
                                right = "0",
                                height = "3em",

                                backgroundColor = "yellow",
                                border = "1px solid red"
                            };


                        }
                    );

                    // can edit and continue 2012 add code her?
                    // no.

                    // Error	1	Modifying a statement which contains an anonymous method will prevent the debug session from continuing while Edit and Continue is enabled.	Z:\jsc.svn\examples\javascript\ubuntu\Test\TestApplicationTypeInfo\TestApplicationTypeInfo\Application.cs	33	13	TestApplicationTypeInfo

                }
            );
        }

    }

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        //public XElement body = new XElement("body");

        //public Action ChangeDetected;

        //public async Task InspectApplication(Action<MethodInfo> AfterChangeDetected = null)
        public async Task InspectApplication(Action<string> AfterChangeDetected = null)
        {
            // how much do we know about the client side?

            // could we call the generic code from the client side, but on the server?
            // jsc should ofcourse nop out any UI related code



            // do we know which application are we to be?
            var thisGetType = this.GetType();
            // service seems to be ApplicationWebService

            // can we be abstract yet?
            Console.WriteLine(new { thisGetType });



            var a = typeof(Application);

            // is the type still available? or did jsc remove it?

            Console.WriteLine(new { a });
            Console.WriteLine(new { a.BaseType });

            // if the type is available
            // next we should be able to reflect upon it
            // and get jsx anaysis on it

            // can we monitor atleast the static string fields?

            var f = a.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Public);

            Console.WriteLine("a fields " + f.Length);

            foreach (var item in f)
            {
                Console.WriteLine(new { item });
            }

            // while in CLR debugger, those methods have the original IL attached
            // while optimized, jsc should keep the signature, if they are non UI specific,
            // but optimize out the code itself

            // monitoring should begin at launch
            var SourceMethods = a.GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Public);



            Console.WriteLine("a methods " + SourceMethods.Length);

            var SourceMethodsToMonitor = SourceMethods.Select(
                (System.Reflection.MethodInfo SourceMethod) =>
                {
                    // Error	4	The type arguments for method 'System.Linq.Enumerable.Select<TSource,TResult>(System.Collections.Generic.IEnumerable<TSource>, System.Func<TSource,TResult>)' cannot be inferred from the usage. Try specifying the type arguments explicitly.	Z:\jsc.svn\examples\javascript\ubuntu\Test\TestApplicationTypeInfo\TestApplicationTypeInfo\Application.cs	133	33	TestApplicationTypeInfo


                    Console.WriteLine(new { SourceMethod });

                    var il = SourceMethod.GetMethodBody().GetILAsByteArray().ToHexString();


                    return (Action)delegate
                    {
                        // time to check again.
                        // were we modified?

                        var il2 = SourceMethod.GetMethodBody().GetILAsByteArray().ToHexString();

                        if (il == il2)
                            return;

                        Console.WriteLine("IL was changed for " + new { SourceMethod });

                        // ask client to reload the new code?
                        // or ask client to confirm a recompile?

                        if (AfterChangeDetected != null)
                            AfterChangeDetected(SourceMethod.Name);

                    };
                }
            ).ToArray();


            // how are we to detect nested types?



            var SourceNestedTypes = a.GetNestedTypes(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
            Console.WriteLine("a SourceNestedTypes " + SourceNestedTypes.Length);

            foreach (var SourceNestedType in SourceNestedTypes)
            {
                var isIAsyncStateMachine = typeof(System.Runtime.CompilerServices.IAsyncStateMachine).IsAssignableFrom(SourceNestedType);

                Console.WriteLine(new { SourceNestedType, isIAsyncStateMachine });
            }

            // is it a statemachine?
            // { SourceNestedType = TestApplicationTypeInfo.Application+<<.ctor>b__14>d__1b, isIAsyncStateMachine = True }
            // if it is, it means, we could hop to its statemachine, if the code is available for us.
            // but is it? or is jsc optimzing it away?



            if (Debugger.IsAttached)
            {
                // while in debugger, can we change the IL and have it be translated back to UI ?
                // the other option is to have the change on the file system, and have it signaled via live server to the client.
                Debugger.Break();

                // if we do detect we are live debgging, and we detect a change in IL
                // will jsc be there for us to provide the source to be sent back to the client?


                // if we are runnin on a remote server, the compiler must signal in out of band updates.
                foreach (var SourceMethod in SourceMethodsToMonitor)
                {
                    SourceMethod();

                    //Console.WriteLine(new { SourceMethod });
                    //Console.WriteLine("IL: " + SourceMethod.GetMethodBody().GetILAsByteArray().ToHexString());
                }

                // IL was changed for { SourceMethod = System.String GetBar() }

                //{ SourceMethod = System.String GetBar() }
                //IL: 0072570000700a2b00062a
                //{ SourceMethod = System.String GetBar() }
                //IL: 0072980100700a2b00062a

                // il was changed.
                // now what?
                // should jsc monitor all methods for debugger changes?
                // it depends how the app was launched?
                // RewriteToUltraApplication.AsProgram.Launch(typeof(Application));


                // the other option is to ask the client to reload and have the new code available for upload.
            }

        }



        public void Handler(ScriptCoreLib.Ultra.WebService.WebServiceHandler handler)
        {
            // here we have the compiled version of the Application as the first app

            var app0 = handler.Applications[0];

            Console.WriteLine(new { app0.TypeName });
            Console.WriteLine(new { app0.TypeFullName });

            // can we have a lookup here?
            Console.WriteLine(new { app0.Type });
        }
    }
}
