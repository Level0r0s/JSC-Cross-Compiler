using ScriptCoreLib.Query.Experimental;
using System;
using System.Data;
using System.Data.MySQL;
using System.Data.SQLite;
using System.Linq.Expressions;
using System.Reflection;
using ScriptCoreLib.Extensions;
using TestXMySQLCLRInsert;
using System.Diagnostics;
using System.Threading;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectGroupByAndConstant\TestSelectGroupByAndConstant\ApplicationWebService.cs

        #region MySQLConnection

        // X:\jsc.svn\examples\javascript\LINQ\test\TestSelectGroupByAndConstant\TestSelectGroupByAndConstant\ApplicationWebService.cs

        // the idea is to test MySQL as we have LINQ to SQL also running in chrome now
        //var mysqld = @"C:\util\xampp-win32-1.8.0-VC9\xampp\mysql\bin\mysqld.exe";
        var mysqld = @"X:\util\xampp-win32-1.8.3-5-VC11\xampp\mysql\bin\mysqld.exe";
        
        // --standalone --console

        var mysqldp = Process.Start(mysqld, " --standalone --console");

        // C:\util\xampp\mysql\bin\mysqld.exe --defaults-file=C:\util\xampp-win32-1.8.0-VC9\xampp\mysql\bin\my.ini mysql
        // already running as a service?

        //151019 12:23:06  InnoDB: Operating system error number 32 in a file operation.
        //InnoDB: The error means that another program is using InnoDB's files.
        //InnoDB: This might be a backup or antivirus software or another instance
        //InnoDB: of MySQL. Please close it to get rid of this error.

        // Additional information: WaitForInputIdle failed.  This could be because the process does not have a graphical interface.
        //mysqldp.WaitForInputIdle();
        Thread.Sleep(3000);

        // the safe way to hint we need to talk PHP dialect
        QueryExpressionBuilder.Dialect = QueryExpressionBuilderDialect.MySQL;
        QueryExpressionBuilder.WithConnection =
            y =>
            {
                var DataSource = "file:xApplicationPerformance.xlsx.sqlite";
                var cc0 = new MySQLConnection(

                    new System.Data.MySQL.MySQLConnectionStringBuilder
                {


                    UserID = "root",
                    Server = "127.0.0.1",

                    //SslMode = MySQLSslMode.VerifyFull

                    //ConnectionTimeout = 3000

                }.ToString()
                    //new MySQLConnectionStringBuilder { DataSource = "file:PerformanceResourceTimingData2.xlsx.sqlite" }.ToString()
                );





                // Additional information: Authentication to host '' for user '' using method 'mysql_native_password' failed with message: Access denied for user ''@'asus7' (using password: NO)
                // Additional information: Unable to connect to any of the specified MySQL hosts.
                cc0.Open();

                // Additional information: Authentication to host '127.0.0.1' for user 'root' using method 'mysql_native_password' failed with message: Access denied for user 'root'@'localhost' (using password: NO)
                // https://www.apachefriends.org/download.html

                #region use db
                {
                    var a = Assembly.GetExecutingAssembly().GetName();


                    // SkipUntilIfAny ???
                    var QDataSource = a.Name + ":" + DataSource.SkipUntilIfAny("file:").TakeUntilIfAny(".xlsx.sqlite");

                    // QDataSource.Length = 76
                    var QLengthb = QDataSource.Length;

                    // Database	64
                    cc0.CreateCommand("CREATE DATABASE IF NOT EXISTS `" + QDataSource + "`").ExecuteScalar();
                    cc0.CreateCommand("use `" + QDataSource + "`").ExecuteScalar();
                }
                #endregion

                y(cc0);

                cc0.Dispose();
            };
        #endregion




        var n = new PerformanceResourceTimingData2ApplicationPerformance();

        var rid = n.Insert(
             new PerformanceResourceTimingData2ApplicationPerformanceRow
        {
            connectStart = 5,
            connectEnd = 13,
            EventTime = DateTime.Now.AddDays(-0),

            z = new XElement("goo", "foo")
        }
         );
        // { LastInsertRowId = 2 }


        var c = new PerformanceResourceTimingData2ApplicationPerformance().Count();

        Console.WriteLine(new { c, rid });


        // +		$exception	{"Process has exited, so the requested information is not available."}	System.Exception {System.InvalidOperationException}
        mysqldp.CloseMainWindow();

        Debugger.Break();

    }
}
