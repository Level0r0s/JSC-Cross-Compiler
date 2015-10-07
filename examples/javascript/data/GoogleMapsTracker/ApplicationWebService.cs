using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GoogleMapsTracker
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService
    {
        static ApplicationWebService()
        {
            Console.WriteLine("ApplicationWebService cctor " + new { Environment.CurrentDirectory });

            // ApplicationWebService cctor { CurrentDirectory = W:\staging.net.debug }

            #region setup:QueryExpressionBuilder.WithConnection
            //if (ScriptCoreLib.Query.Experimental.QueryExpressionBuilder.WithConnection == null)

            // override the default
            ScriptCoreLib.Query.Experimental.QueryExpressionBuilder.WithConnection =
                y =>
                {
                    // relative path?
                    var DataSource = "file:server1.sqlite";

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
        }
    }
}

//[NullReferenceException: Object reference not set to an instance of an object.]
//ScriptCoreLib.Query.Experimental.QueryExpressionBuilder.GetInsertCommand(IQueryStrategy`1 source, IDbConnection cc, TElement value) +285
//   ScriptCoreLib.Query.Experimental.QueryExpressionBuilder.Insert(xSelect`2 source, IDbConnection cc, TElement value) +200