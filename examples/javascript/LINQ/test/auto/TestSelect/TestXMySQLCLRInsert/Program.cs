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
                    cc0.CreateCommand("CREATE DATABASE IF NOT EXISTS `" + QDataSource + "`").ExecuteNonQuery();
                    cc0.CreateCommand("use `" + QDataSource + "`").ExecuteNonQuery();
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



        Debugger.Break();

        // +		$exception	{"Process has exited, so the requested information is not available."}	System.Exception {System.InvalidOperationException}
        mysqldp.CloseMainWindow();

    }
}


//__SQLiteCommand.InternalCreateStatement { sql = CREATE DATABASE IF NOT EXISTS `TestUbuntuMySQLInsert` }
//{ Message = , StackTrace = java.lang.RuntimeException
//        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:98)
//        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(__MethodBase.java:73)
//        at ScriptCoreLib.Shared.BCLImplementation.System.__Action_1.Invoke(__Action_1.java:28)
//        at TestUbuntuMySQLInsert__i__d.Internal.Query.Experimental.QueryExpressionBuilder.Create(QueryExpressionBuilder.java:1200)
//        at TestUbuntuMySQLInsert.PerformanceResourceTimingData2ApplicationPerformance.<init>(PerformanceResourceTimingData2ApplicationPerformance.java:114)
//        at TestUbuntuMySQLInsert.Program.main(Program.java:70)
//Caused by: java.lang.reflect.InvocationTargetException
//        at sun.reflect.NativeMethodAccessorImpl.invoke0(Native Method)
//        at sun.reflect.NativeMethodAccessorImpl.invoke(Unknown Source)
//        at sun.reflect.DelegatingMethodAccessorImpl.invoke(Unknown Source)
//        at java.lang.reflect.Method.invoke(Unknown Source)
//        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:94)
//        ... 5 more
//Caused by: java.lang.RuntimeException: { Message = Can not issue data manipulation statements with executeQuery()., StackTrace = java.sql.SQLException: Can not issue data manipulation statements with executeQuery().
//        at com.mysql.jdbc.SQLError.createSQLException(SQLError.java:959)
//        at com.mysql.jdbc.SQLError.createSQLException(SQLError.java:898)
//        at com.mysql.jdbc.SQLError.createSQLException(SQLError.java:887)
//        at com.mysql.jdbc.SQLError.createSQLException(SQLError.java:862)
//        at com.mysql.jdbc.StatementImpl.checkForDml(StatementImpl.java:465)
//        at com.mysql.jdbc.StatementImpl.executeQuery(StatementImpl.java:1325)
//        at ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__MySQLCommand.ExecuteReader_0600007e(__MySQLCommand.java:356)
//        at ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__MySQLCommand.ExecuteScalar(__MySQLCommand.java:391)
//        at ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__MySQLCommand.System_Data_IDbCommand_ExecuteScalar(__MySQLCommand.java:436)