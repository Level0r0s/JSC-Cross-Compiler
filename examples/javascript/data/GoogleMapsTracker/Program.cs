using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using ScriptCoreLib.Extensions;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace GoogleMapsTracker
{
    using ScriptCoreLib.Query.Experimental;


    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        //static string replayID = "replayID" + new Random().Next().ToString("x8");

        //static void EditAndContinue(string replayID = "demo1")
        static void EditAndContinue()
        {
            // manually restart method if modified?
            // some changes require full process restart. like LINQ?
            Console.WriteLine("enter EditAndContinue");

            //var capture = new { replayID = "demo1" };

            var replayID = "demo1";


            var service = new ApplicationWebService { };


            //await service.AddHistory("headless1", 1, 1);
            service.AddHistory(replayID, 1, 1).Wait();

            // how many are there now?



            // new statement?
            ;

            // adding a new statment here causes the method to be retried
            var all = new Data.replayhistory().Count();

            var all2 = new Data.replayhistory().Count();
            var all3 = new Data.replayhistory().Count();

            var myHistory = from x in new Data.replayhistory()
                            where x.replayID == replayID
                            select x;

            // Z:\jsc.svn\examples\javascript\data\GoogleMapsTracker\Program.cs
            var myHistoryCount = myHistory.Count();


            Debugger.Break();
            // edit and continue will reset this method and step out?
            Console.WriteLine("exit EditAndContinue");
        }

        [DebuggerNonUserCode]
        [DebuggerStepThrough]

        public static void Main(string[] args)
        {
            // detect debugger as 2015community?
            // Managed Compatibility Mode does not support Edit and Continue

            // lets do a headless simulation.

            #region setup:QueryExpressionBuilder.WithConnection
            ScriptCoreLib.Query.Experimental.QueryExpressionBuilder.WithConnection =
                y =>
                {
                    // relative path?
                    var DataSource = "file:debugger1.sqlite";

                    // nuget xsqlite?
                    var cc = new System.Data.SQLite.SQLiteConnection(new System.Data.SQLite.SQLiteConnectionStringBuilder
                    {
                        DataSource = DataSource,
                        Version = 3
                    }.ConnectionString);
                    cc.Open();
                    y(cc);
                    cc.Dispose();
                };
            #endregion



            // https://github.com/dotnet/roslyn/wiki/EnC-Supported-Edits
            // http://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/6015600-edit-continue-in-async-methods
            // Edit & Continue is one of the best feature of vs bringing huge productivity gain, unfortunatly Edit & Continue does not work in Async methods.



            new Action(EditAndContinue).InvokeAndReinvokeIfCodeModified();
            //xDebugger.InvokeAndReinvokeIfCodeModified(EditAndContinue);


            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }


    }
}


static class xDebugger
{
    [DebuggerNonUserCode]
    [DebuggerStepThrough]
    public static void InvokeAndReinvokeIfCodeModified(this Delegate EditAndContinue)
    {

        byte[] old;
        byte[] checkagain;
        do
        {
            old = EditAndContinue.Method.GetMethodBody().GetILAsByteArray();

            (EditAndContinue as dynamic)();


            checkagain = EditAndContinue.Method.GetMethodBody().GetILAsByteArray();

        } while (!Enumerable.SequenceEqual(old, checkagain));
    }
}

//Severity Code    Description Project File Line    Source
//Error   ENC0048 Capturing variable 'replayID' that hasn't been captured before will prevent the debug session from continuing.	GoogleMapsTracker	Z:\jsc.svn\examples\javascript\data\GoogleMapsTracker\Program.cs	64	IntelliSense
