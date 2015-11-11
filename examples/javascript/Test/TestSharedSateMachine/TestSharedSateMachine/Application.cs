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
using TestSharedSateMachine;
using TestSharedSateMachine.Design;
using TestSharedSateMachine.HTML.Pages;

namespace TestSharedSateMachine
{
    public struct HopToService : System.Runtime.CompilerServices.INotifyCompletion
    {
        // basically we have to hibernate the current state to resume
        public HopToService GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }

        public static Action<Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation) { VirtualOnCompleted(continuation); }

        public void GetResult() { }
    }

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // the problem is
        // the server wont have the client side code available to jump into
        // would override InvokeSharedState help as a hint?

     
        // the server should have the typeinfo of the application available
        // and the statemachine info to understand the safe jump targets.
        public Application(IApp page)
        {
            new { }.With(
                async delegate
                {
                    await InvokeSharedState();


                    // hop to server


                    // if we were to hop to server
                    // the server needs the client code.
                    // without direct UI access that is.

                    //the beenfit would be to have shared anonymous types available.

                    await default(HopToService);

                    // would jsc know how to make this hop point available on the server?
                    Console.WriteLine("on server?");

                }
            );
        }

        public override async Task InvokeSharedState()
        {
            Console.WriteLine("still int ui?");

        }
    }

    //public abstract partial class ApplicationWebService
    public partial class ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151111




        // can jsc handle abstract webservice methods yet?
        //public abstract Task InvokeSharedState();
        public virtual async Task InvokeSharedState() { }


        public Func<Task> vInvokeSharedState;

        // would calling the virtual mean we are calling into ui?


        public async Task InvokeOnServer()
        {
            Console.WriteLine("on server?");

            await InvokeSharedState();

            Console.WriteLine("back from ui?");

            await vInvokeSharedState();

            Console.WriteLine("back from ui?");


        }
    }
}
