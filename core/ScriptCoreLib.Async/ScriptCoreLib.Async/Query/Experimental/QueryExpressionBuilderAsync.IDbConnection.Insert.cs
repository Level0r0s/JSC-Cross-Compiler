using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;
using System.Data;
using System.Threading.Tasks;
using System.Data.Common;

// Z:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Query\Experimental\QueryExpressionBuilderAsync.IDbConnection.Insert.cs



namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilderAsync
    {
        // X:\jsc.svn\examples\javascript\Test\TestSQLiteConnection\TestSQLiteConnection\Application.cs
        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\Experimental\QueryExpressionBuilder.IDbConnection.Insert.cs





        class __InsertAsync<TElement, TInsertAsync0Key>
        {
            public TaskCompletionSource<TInsertAsync0Key[]> z = new TaskCompletionSource<TInsertAsync0Key[]>();


            //W:\XSLXAssetWithXElement.ApplicationWebService\staging.java\web\java\ScriptCoreLib\Query\Experimental\QueryExpressionBuilderAsync___InsertAsync_2___c__DisplayClass2c___c__DisplayClass2e.java:23: error: non-static type variable TInsertAsyncKey cannot be referenced from a static context
            //    public static __Func_2<__Task_1<TInsertAsyncKey>, TInsertAsyncKey> CS___9__CachedAnonymousMethodDelegate30;
            //                                    ^
            //W:\XSLXAssetWithXElement.ApplicationWebService\staging.java\web\java\ScriptCoreLib\Query\Experimental\QueryExpressionBuilderAsync___InsertAsync_2___c__DisplayClass2c___c__DisplayClass2e.java:23: error: non-static type variable TInsertAsyncKey cannot be referenced from a static context
            //    public static __Func_2<__Task_1<TInsertAsyncKey>, TInsertAsyncKey> CS___9__CachedAnonymousMethodDelegate30;
            //                                                      ^


            public QueryExpressionBuilder.xSelect<TInsertAsync0Key, TElement> source;
            public TElement[] collection;




            private IEnumerable<Task<TInsertAsync0Key>> i;


            public void SetResult()
            {
                z.SetResult(
                    i.Select(x => x.Result).ToArray()
                );
            }


            // no CachedAnonymousMethodDelegate thanks.
            public void Invoke(IDbConnection cc)
            {
                //W:\XSLXAssetWithXElement.ApplicationWebService\staging.java\web\java\ScriptCoreLib\Query\Experimental\QueryExpressionBuilderAsync___InsertAsync_2.java:26: error: non-static type variable TInsertAsync0Key cannot be referenced from a static context
                //    public static __Func_2<__Task_1<TInsertAsync0Key>, TInsertAsync0Key> CS___9__CachedAnonymousMethodDelegate29;
                //                                    ^
                //W:\XSLXAssetWithXElement.ApplicationWebService\staging.java\web\java\ScriptCoreLib\Query\Experimental\QueryExpressionBuilderAsync___InsertAsync_2.java:26: error: non-static type variable TInsertAsync0Key cannot be referenced from a static context
                //    public static __Func_2<__Task_1<TInsertAsync0Key>, TInsertAsync0Key> CS___9__CachedAnonymousMethodDelegate29;
                //                                                       ^

                this.i = collection.Select(c => InsertAsync(source, cc, c));


                //var i = from c in collection
                //        select InsertAsync(source, cc, c);


                //Task.Factory.ContinueWhenAll(
                // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.WhenAll.cs
                // X:\jsc.svn\examples\java\hybrid\test\JVMCLRWhenAll\JVMCLRWhenAll\Program.cs
                Task.WhenAll(this.i.ToArray()).ContinueWith(
                    delegate
                    {
                        //Console.WriteLine("after InsertAsync");

                        SetResult();
                    }
                );
            }


        }

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150911/mysql
        // ! to track down jvm issues, lets name generics with method name
        public static Task<TInsertAsync1Key[]> InsertAsync<TElement, TInsertAsync1Key>(this QueryExpressionBuilder.xSelect<TInsertAsync1Key, TElement> source, params TElement[] collection)
        {
            // tested by?


            // used by
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectAverage\Program.cs
            // x:\jsc.svn\examples\javascript\linq\test\auto\testselect\testweborderbythengroupby\application.cs
            //Console.WriteLine("enter InsertAsync");
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs

            var v = new __InsertAsync<TElement, TInsertAsync1Key>
            {
                source = source,
                collection = collection
            };

            //v.Invoke();
            QueryExpressionBuilder.WithConnection(v.Invoke);


            return v.z.Task;
        }

        public static Task<TInsertAsync2Key> InsertAsync<TElement, TInsertAsync2Key>(this QueryExpressionBuilder.xSelect<TInsertAsync2Key, TElement> source, TElement value)
        {
            //Console.WriteLine("enter InsertAsync");
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestWebInsert\Application.cs

            var z = new TaskCompletionSource<TInsertAsync2Key>();

            // was it manually set?
            //QueryExpressionBuilder.WithConnectionAsync(
            QueryExpressionBuilder.WithConnection(
                (IDbConnection cc) =>
                {
                    InsertAsync(source, cc, value).ContinueWith(
                        task =>
                        {
                            //Console.WriteLine("after InsertAsync");

                            z.SetResult(task.Result);
                        }
                    );
                }
            );
            //Console.WriteLine("exit InsertAsync");
            return z.Task;
        }

        public static Task<TInsertAsync3Key> InsertAsync<TElement, TInsertAsync3Key>(this QueryExpressionBuilder.xSelect<TInsertAsync3Key, TElement> source, IDbConnection cc, TElement value)
        {
            // in CLR and in browser this would work.

            var xDbCommand = QueryExpressionBuilder.GetInsertCommand(source, cc, value) as DbCommand;
            // why ExecuteNonQueryAsync is not part of CLR, now we need to link in SQLite and PHP!

            if (xDbCommand != null)
            {
                //Console.WriteLine("before ExecuteNonQueryAsync");
                var n = xDbCommand.ExecuteNonQueryAsync();
                // n = Id = 0x00000001, Status = RanToCompletion, Method = "{null}", Result = "1"

                var c = new TaskCompletionSource<TInsertAsync3Key>();

                n.ContinueWith(
                    task =>
                    {
                        // jsc makes all Keys of long, yet data layer seems to talk int?
                        long LastInsertRowId = IDbConnectionExtensions.GetLastInsertRowId(cc);

                        //Console.WriteLine("InsertAsync " + new { LastInsertRowId });

                        c.SetResult(
                            (TInsertAsync3Key)(object)LastInsertRowId
                        );
                    }
                );

                return c.Task;
            }


            // how would this work in the browser if scriptcorelib does not yet provide the implementation?
            //var xMySQLCommand = c as System.Data.MySQL.MySQLCommand;
            //if (xMySQLCommand != null)
            //{
            //    var n = xMySQLCommand.ExecuteNonQueryAsync();
            //    return n;
            //}

            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs
            // should we report back the new key?

            throw new NotSupportedException();
        }


    }

}
