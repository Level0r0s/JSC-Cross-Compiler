using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Query.Experimental;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data; //!

namespace TestUbuntuMySQLInsert
{

    //Y:\staging\web\java\TestUbuntuMySQLInsert\xDriver.java:16: error: unreported exception SQLException; must be caught or declared to be thrown
    //        super();
    //             ^

    //public class xDriver : global::com.mysql.jdbc.Driver { }

    public static class xDriver
    {
        public static object Create()
        {
            object x = null;

            try
            {
                x = new global::com.mysql.jdbc.Driver { };
            }
            catch
            {
                throw;
            }

            return x;
        }
    }


    static class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {

                #region MySQLConnection

                // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectGroupByAndConstant\TestSelectGroupByAndConstant\ApplicationWebService.cs

                // the safe way to hint we need to talk PHP dialect

                // 
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
                            var QDataSource = "TestUbuntuMySQLInsert";

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



                var n = new PerformanceResourceTimingData2ApplicationPerformance();

                var rid = n.Insert(
                    new PerformanceResourceTimingData2ApplicationPerformanceRow
                    {
                        connectStart = 5,
                        connectEnd = 13,

                        // conversion done in AddParameter
                        // .stack rewriter needs to store struct. can we create new byref struct parameters?
                        //EventTime = DateTime.Now.AddDays(-0),

                        // conversion done in Insert?
                        z = new XElement("goo", "foo")
                    }
                );

                // { LastInsertRowId = 2 }
                Console.WriteLine("after insert " + new { rid });


                var c = new PerformanceResourceTimingData2ApplicationPerformance().Count();

                Console.WriteLine(new { c, rid });

            }
            catch (Exception err)
            {
                Console.WriteLine(new { err.Message, err.StackTrace });
            }

            Console.ReadLine();


        }


    }



    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            MessageBox.Show("click to close");
        }
    }


}
