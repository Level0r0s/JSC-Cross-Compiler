using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using ScriptCoreLib.Extensions;
using System.Threading.Tasks;

namespace GoogleMapsTracker
{
    using ScriptCoreLib.Query.Experimental;


    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
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


            Task.Run(
                async delegate
                {
                    var service = new ApplicationWebService { };


                    await service.AddHistory("headless1", 1, 1);

                    // how many are there now?

                    // can we edit and continue and ask how many lines we now have?
                    // no the extensions are missng.

                    // Severity	Code	Description	Project	File	Line
                    //Error ENC0001 Updating an active statement will prevent the debug session from continuing.GoogleMapsTracker Z:\jsc.svn\examples\javascript\data\GoogleMapsTracker\Program.cs    40
                    // ah we are on RC!

                    //var all = new Data.replayhistory().Count();
                    ;


                    // new statement?
                    ;
                    {

                        var all = new Data.replayhistory().Count();

                    }
                }
            ).Wait();



            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
