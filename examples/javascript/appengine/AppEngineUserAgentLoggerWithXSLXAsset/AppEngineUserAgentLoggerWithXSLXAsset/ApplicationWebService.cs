extern alias global_scle;

using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppEngineUserAgentLoggerWithXSLXAsset
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService
    {
        #region appengine

        //Unhandled Exception: System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.ArgumentException: Illegal characters in path.
        //   at System.IO.Path.CheckInvalidPathChars(String path, Boolean checkAdditional)
        //   at System.IO.Path.NormalizePath(String path, Boolean fullCheck, Int32 maxPathLength, Boolean expandShortPaths)
        //   at System.IO.Path.GetFullPathInternal(String path)
        //   at System.IO.DirectoryInfo.Init(String path, Boolean checkHost)
        //   at System.IO.DirectoryInfo..ctor(String path)
        //   at ScriptCoreLib.Reflection.Options.ParameterDispatcherExtensions.AsParameterTo(String value, Object e, FieldInfo f) in x:\jsc.svn\core\ScriptCoreLib.Reflection.Options\ScriptCoreLib.Reflection.Options\ParameterDispatcherExtensions.cs:line 244

        //0001 02000058 AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService::<module>.SHA12e7f9b931303fcbae1427b5e9cb94063fc864d21@2130839642
        //Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\files
        //C:\Program Files (x86)\Java\jdk1.6.0_35\bin\javac.exe  -encoding UTF-8 -cp Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\release;C:\util\appengine-java-sdk-1.8.3\lib\impl\*;C:\util\appengine-java-sdk-1.8.3\lib\shared\* -d "Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\release" @"Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\files"
        //Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\java\AppEngineUserAgentLoggerWithXSLXAsset\Design\Book1_Sheet1__SelectAllAsEnumerable_closure.java:26: incompatible types
        //found   : java.lang.Object
        //required: long
        //        row0.Key = /* unbox Book1Sheet1Key */_arg0.get_Item("Key");
        //                                                           ^
        //Y:\AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService\staging.java\web\java\AppEngineUserAgentLoggerWithXSLXAsset\Design\Book1_Sheet2__SelectAllAsEnumerable_closure.java:26: incompatible types
        //found   : java.lang.Object
        //required: long
        //        row0.Key = /* unbox Book1Sheet2Key */_arg0.get_Item("Key");

        // X:\jsc.svn\examples\java\Test\TestUnboxEnum\TestUnboxEnum\Class1.cs

        //        InternalWebMethodInfo.AddField { FieldName = field_ClientTime, FieldValue =  }
        //Nov 25, 2013 12:41:23 AM com.google.appengine.api.rdbms.dev.LocalRdbmsServiceLocalDriver openConnection
        //SEVERE: Could not allocate a connection
        //com.mysql.jdbc.exceptions.jdbc4.CommunicationsException: Communications link failure

        //The last packet sent successfully to the server was 0 milliseconds ago. The driver has not received any packets from the server.
        //        at sun.reflect.NativeConstructorAccessorImpl.newInstance0(Native Method)

        // "C:\util\xampp-win32-1.8.0-VC9\xampp\mysql_start.bat"

        //Caused by: java.lang.RuntimeException: Table 'book1.xlsx.sqlite.sheet1' doesn't exist
        //        at ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__SQLiteCommand.ExecuteNonQuery(__SQLiteCommand.java:294)
        //        at AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1_Sheet1_Queries.Insert(Book1_Sheet1_Queries.java:56)



        #endregion

        public int ScreenWidth;
        public int ScreenHeight;

        public string ClientTime;

        #region WebServiceHandler
        [EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(
                               DesignerSerializationVisibility.Hidden)]
        [Obsolete("experimental")]
        public WebServiceHandler WebServiceHandler { set; get; }
        #endregion

        public Task<DataTable> GetVisitHeadersFor(Design.Book1Sheet1Key visit)
        {
            Console.WriteLine(new { visit });

            var visitkey = "" + (long)visit;

            Console.WriteLine(new { visitkey });

            var y = new Design.Book1.Sheet2();

            // we need a diagram showing us
            // how much faster will we make this call if
            // we move the filtering from web app into database
            //var a = y.SelectAllAsEnumerable().ToArray();
            var a = y.XSelectAllAsEnumerable().ToArray();

            Console.WriteLine(new { a.Length });

            //Caused by: java.lang.NullPointerException
            //        at AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService___c__DisplayClass1._GetVisitHeadersFor_b__0(ApplicationWebService___c__DisplayClass1.java:23)

            return a.Where(
                k =>
                {
                    if (k.Sheet1 == null)
                    {
                        Console.WriteLine("got a null, why?");
                        return false;
                    }

                    return k.Sheet1 == visitkey;
                }
            ).AsDataTable().ToTaskResult();

        }


        // Create Partial Type: AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1+Sheet1Key

        public Task<DataTable> Notfiy(Design.Book1Sheet1Key dummy = default(Design.Book1Sheet1Key))
        {

            // https://code.google.com/p/googleappengine/issues/detail?id=803

            var x = new Design.Book1.Sheet1();

            #region auto

            Console.WriteLine(
                new
                {
                    Design.Book1.Sheet1.Queries.CreateCommandText
                }
            );


            // { CreateCommandText = create table if not exists Sheet1 (Key INTEGER  NOT NULL  PRIMARY KEY AUTOINCREMENT, ScreenWidth text, ScreenHeight text, IPAddress text, ServiceTime text, ClientTime text) }
            //    Caused by: java.sql.SQLException: You have an error in your SQL syntax; check the manual that corresponds to your MySQL server version for the right syntax to use near 'INTEGER  NOT NULL  PRIMARY KEY AUTO_INCREMENT, ScreenWidth text, ScreenHeight te' at line 1
            //at com.google.cloud.sql.jdbc.internal.Exceptions.newSqlException(Exceptions.java:219)
            //at com.google.cloud.sql.jdbc.internal.SqlProtoClient.check(SqlProtoClient.java:198)

            var create0 = new Design.Book1.Sheet1.Queries().WithConnection(c => Design.Book1.Sheet1.Queries.Create(c));

            #endregion

            var now = DateTime.Now;
            var ServiceTime = now.ToString();

            var visit = x.Insert(
                new Design.Book1Sheet1Row
                {
                    // jsc experience should auto detect, 
                    // implicit column types

                    ScreenWidth = "" + this.ScreenWidth,
                    ScreenHeight = "" + this.ScreenHeight,


                    // not available for AppEngine?
                    IPAddress = WebServiceHandler.Context.Request.UserHostAddress,

                    // we are now logging all headers
                    //UserAgent = WebServiceHandler.Context.Request.UserAgent,

                    ClientTime = this.ClientTime,
                    ServiceTime = ServiceTime,
                }
            );

            //visit.Sheet2().

            var y = new Design.Book1.Sheet2();


            #region auto
            var create2 = new Design.Book1.Sheet2.Queries().WithConnection(c => Design.Book1.Sheet2.Queries.Create(c));
            #endregion


            var h = this.WebServiceHandler.Context.Request.Headers;

            foreach (var item in h.AllKeys)
            {
                y.Insert(
                   new Design.Book1Sheet2Row
                   {
                       HeaderKey = item,
                       HeaderValue = h[item],

                       // can jsc auto bind to key? 
                       Sheet1 = "" + (long)visit
                   }
               );
            }







            #region auto
            // [FileNotFoundException: Could not load file or assembly 'ScriptCoreLib.Extensions, Version=1.0.0.0, C
            var ref_ScriptCoreLib_Extensions = typeof(global_scle::ScriptCoreLib.Extensions.DataExtensions);
            #endregion

            return x.SelectAllAsDataTable().ToTaskResult();
        }

    }

    public static class X
    {

        public static IEnumerable<Design.Book1Sheet2Row> XSelectAllAsEnumerable(this Design.Book1.Sheet2 data)
        {
            var x = data.SelectAllAsDataTable();

            return x.Rows.AsEnumerable().Select(
                r =>
                {
                    Console.WriteLine("Book1Sheet2Key:");

                    //var KeyType = r.get
                    var KeyObject = r["Key"];
                    var KeyType = KeyObject.GetType();

                    Console.WriteLine(new { KeyObject, KeyType.FullName });

                    //            Caused by: java.lang.ClassCastException: java.lang.Integer cannot be cast to java.lang.Long
                    //at AppEngineUserAgentLoggerWithXSLXAsset.X._XSelectAllAsEnumerable_b__1(X.java:69)

                    var Key = (Design.Book1Sheet2Key)Convert.ToInt64(KeyObject);

                    return new Design.Book1Sheet2Row
                    {
                        Key = Key,
                        HeaderKey = (string)r["HeaderKey"],
                        HeaderValue = (string)r["HeaderValue"],
                        Sheet1 = (string)r["Sheet1"],
                    };
                }

            );

        }

        public static DataTable AsDataTable(this IEnumerable<Design.Book1Sheet2Row> source)
        {
            //java.lang.RuntimeException
            //        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:92)
            //        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(__MethodBase.java:70)
            //        at ScriptCoreLib.Shared.BCLImplementation.System.__Func_2.Invoke(__Func_2.java:28)
            //        at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable__SelectIterator_d__b_2.MoveNext(__Enumerable__SelectIterator_d__b_2.java:98)
            //        at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable__SelectIterator_d__b_2.System_Collections_IEnumerator_MoveNext(__Enumerable__SelectIterator_d__b_2.java:141)
            //        at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable__WhereIterator_d__0_1.MoveNext(__Enumerable__WhereIterator_d__0_1.java:86)
            //        at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable__WhereIterator_d__0_1.System_Collections_IEnumerator_MoveNext(__Enumerable__WhereIterator_d__0_1.java:146)
            //        at AppEngineUserAgentLoggerWithXSLXAsset.X.AsDataTable(X.java:34)
            //        at AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService.GetVisitHeadersFor(ApplicationWebService.java:67)

            var x = new DataTable();

            x.Columns.Add("Key");
            x.Columns.Add("HeaderKey");
            x.Columns.Add("HeaderValue");
            x.Columns.Add("Sheet1");

            foreach (var item in source)
            {
                x.Rows.Add(
                    "" + (long)item.Key,
                    item.HeaderKey,
                    item.HeaderValue,
                    item.Sheet1
                );
            }

            return x;
        }
    }
}