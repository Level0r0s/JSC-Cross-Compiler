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
using UbuntuTestMySQLInsert.Data;


// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151025
// need it?
//[assembly: ScriptCoreLib.Shared.ScriptResources("assets/UbuntuTestMySQLInsert")]

namespace UbuntuTestMySQLInsert
{
    using ScriptCoreLib.Query.Experimental;


    public static class xDriver
    {
        public static object Create()
        {
            object x = null;

            try
            {
                // add References jar
                // native build of java core.
                x = new global::com.mysql.jdbc.Driver { };
            }
            catch
            {
                throw;
            }

            return x;
        }
    }

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        static ApplicationWebService()
        {
            try
            {

                #region MySQLConnection

                // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectGroupByAndConstant\TestSelectGroupByAndConstant\ApplicationWebService.cs

                // the safe way to hint we need to talk PHP dialect

                // nuget add XMysql
                ScriptCoreLib.Query.Experimental.QueryExpressionBuilder.Dialect = QueryExpressionBuilderDialect.MySQL;
                ScriptCoreLib.Query.Experimental.QueryExpressionBuilder.WithConnection =
                    y =>
                    {
                        Console.WriteLine("enter WithConnection");

                        // nuget?
                        // <package id="System.Data.XMySQL" version="1.0.0.0" targetFramework="net451" />
                        var cc0 = new System.Data.MySQL.MySQLConnection(

                            new System.Data.MySQL.MySQLConnectionStringBuilder
                            {
                                //Database = 

                                UserID = "root",
                                Server = "127.0.0.1",

                                //SslMode = MySQLSslMode.VerifyFull

                                //ConnectionTimeout = 3000

                            }.ToString()
                            //new MySQLConnectionStringBuilder { DataSource = "file:PerformanceResourceTimingData2.xlsx.sqlite" }.ToString()
                        );



                        // rdbms.driver=com.mysql.jdbc.Driver 

                        // http://www.codejava.net/java-se/jdbc/connect-to-mysql-database-via-jdbc

                        try
                        {
                            // mysql-connector-java-5.1.37-bin.jar

                            //java.lang.ClassLoader.getSystemClassLoader().

                            // Caused by: java.lang.ClassNotFoundException: com.mysql.jdbc.Driver


                            //java.sql.Driver drv = new xDriver { };
                            var drv = (java.sql.Driver)xDriver.Create();

                            Console.WriteLine(new { drv });

                            java.sql.DriverManager.registerDriver(drv);

                            //var cDriver = java.lang.Class.forName("com.mysql.jdbc.Driver");

                        }
                        catch
                        {
                            throw;
                        }


                        ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__MySQLConnection.OpenConnectionPrefix = "jdbc:mysql://";


                        cc0.Open();

                        // http://stackoverflow.com/questions/1457716/what-is-the-mysql-jdbc-driver-connection-string

                        //Caused by: java.lang.RuntimeException: __SQLiteConnection { Message = No suitable driver found for jdbc:google:rdbms://127.0.0.1, StackTrace = java.sql.SQLException: No suitable driver found for jdbc:google:rdbms://127.0.0.1
                        //        at java.sql.DriverManager.getConnection(Unknown Source)
                        //        at java.sql.DriverManager.getConnection(Unknown Source)
                        //        at ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__MySQLConnection.Open(__MySQLConnection.java:78)


                        #region use db
                        {
                            var QDataSource = "UbuntuTestMySQLInsert";

                            // QDataSource.Length = 76
                            var QLengthb = QDataSource.Length;

                            // Database	64
                            cc0.CreateCommand("CREATE DATABASE IF NOT EXISTS `" + QDataSource + "`").ExecuteNonQuery();
                            cc0.CreateCommand("use `" + QDataSource + "`").ExecuteNonQuery();
                        }
                        #endregion

                        y(cc0);


                        // jsc java does the wrong thing here
                        cc0.Close();
                        //cc0.Dispose();
                        Console.WriteLine("exit WithConnection");
                    };
                #endregion




            }
            catch (Exception err)
            {
                Console.WriteLine(new { err.Message, err.StackTrace });
            }

        }

        public async Task<long> AddAndCount(XElement value)
        {

            var n = new PerformanceResourceTimingData2ApplicationPerformance();


            // using 
            var rid = n.Insert(
                new PerformanceResourceTimingData2ApplicationPerformanceRow
                {
                    connectStart = 5,
                    connectEnd = 13,

                    // conversion done in AddParameter
                    // .stack rewriter needs to store struct. can we create new byref struct parameters?
                    //EventTime = DateTime.Now.AddDays(-0),

                    // conversion done in Insert?
                    z = new XElement("goo2", value)
                }
            );

            // { LastInsertRowId = 2 }
            Console.WriteLine("after insert " + new { rid });


            var c = new PerformanceResourceTimingData2ApplicationPerformance().Count();

            Console.WriteLine(new { c, rid });

            // Error	10	Cannot implicitly convert type 'long' to 'int'. An explicit conversion exists (are you missing a cast?)	Z:\jsc.svn\examples\javascript\ubuntu\Test\UbuntuTestMySQLInsert\UbuntuTestMySQLInsert\ApplicationWebService.cs	191	20	UbuntuTestMySQLInsert

            return c;
        }

        public async Task<PerformanceResourceTimingData2ApplicationPerformanceRow> TakeOne()
        {
            Console.WriteLine("enter TakeOne ascending");

            return (
                from x in new PerformanceResourceTimingData2ApplicationPerformance()

                where x.connectStart == 5

                orderby x.Key ascending

                select x

            ).FirstOrDefault();
        }

        public async Task<PerformanceResourceTimingData2ApplicationPerformanceRow> TakeLastOne()
        {
            Console.WriteLine("enter TakeOne descending");

            var value = (
                from x in new PerformanceResourceTimingData2ApplicationPerformance()

                where x.connectStart == 5

                // can we get get the last one thanks?
                orderby x.Key descending

                select x

            ).FirstOrDefault();

            Console.WriteLine("exit TakeOne descending " + new { value.Key });
            return value;
        }


        public async Task<PerformanceResourceTimingData2ApplicationPerformanceRow[]> ReadAll()
        {
            Console.WriteLine("enter ReadAll");

            return (
                from x in new PerformanceResourceTimingData2ApplicationPerformance()

                where x.connectStart == 5

                // can we get get the last one thanks?
                orderby x.Key descending

                select x

            ).ToArray();
        }
    }
}
