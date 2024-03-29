using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Data.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSQLJoin.Data;

namespace TestSQLJoin
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140429
        // X:\jsc.svn\examples\javascript\test\TestLINQJoin\TestLINQJoin\Application.cs

        // http://stackoverflow.com/questions/485398/how-to-create-a-join-in-an-expression-tree-for-linq
        // http://msdn.microsoft.com/en-us/library/bb399415(v=vs.110).aspx

        // 4.5
        // under .net the IEnumerable is not buffered. the caller needs to enforce it ?
        public
            //async
            Task<IEnumerable<Book1TheViewRow>> GetTheViewData()
        {
            // http://stackoverflow.com/questions/38549/difference-between-inner-and-outer-join
            // X:\jsc.svn\examples\javascript\Test\TestSQLNestedJoin\TestSQLNestedJoin\ApplicationWebService.cs

            #region InsertDemoData
            Action InsertDemoData = delegate
            {
                new[] { 
                    new Book1DealerContactRow { DealerId = 2, DealerContactText = ""},
                    new Book1DealerContactRow { DealerId = 3, DealerContactText = "hello "},
                    new Book1DealerContactRow { DealerId = 4, DealerContactText = ""}
                }.Select(new Book1.DealerContact().Insert).ToArray();



                new[] { 
                    new Book1DealerRow { ID = 1, DealerText = ""},
                    new Book1DealerRow { ID = 3, DealerText = "world"},
                    new Book1DealerRow { ID = 5, DealerText = ""}
                }.Select(new Book1.Dealer().Insert).ToArray();


                new[] { 
                    new Book1DealerOtherRow { ID = 0, DealerOtherText = ""},
                    new Book1DealerOtherRow { ID = 3, DealerOtherText = "!!"},
                    new Book1DealerOtherRow { ID = 20, DealerOtherText = ""}
                }.Select(new Book1.DealerOther().Insert).ToArray();
            };


            InsertDemoData();
            #endregion


            Console.WriteLine("after inserts");

            var DealerContact = new Book1.DealerContact();
            var Dealer = new Book1.Dealer();
            var DealerOther = new Book1.DealerOther();
            var View = new Book1.TheView();

            Console.WriteLine("at 94");

            // can we join twice, taking the first and then the last?
            var z =
                from contact in DealerContact
                join dealer in Dealer on contact.DealerId equals dealer.ID
                join other in DealerOther on contact.DealerId equals other.ID
                select new Book1TheViewRow
                {
                    Timestamp = contact.Timestamp,
                    Tag = "no tag",

                    DealerOther = 0,
                    DealerOtherText = other.DealerOtherText,
                    Dealer = dealer.Key,
                    DealerContact = contact.Key,
                    DealerContactText = contact.DealerContactText,
                    DealerText = dealer.DealerText,
                };


            //DataTable zz = z;

            // public static DataTable AsDataTable(this Book1.TheView value);

            //Error	31	The call is ambiguous between the following methods or properties: 
            // 'TestSQLJoin.Data.Book1Extensions.AsDataTable(TestSQLJoin.Data.Book1.TheView)' and
            // 'TestSQLJoin.Data.Book1Extensions.AsDataTable(System.Collections.Generic.IEnumerable<TestSQLJoin.Data.Book1TheViewRow>)'	
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	146	28	TestSQLJoin


            // Error	2	The call is ambiguous between the following methods or properties: 'TestSQLJoin.Data.Book1Extensions.AsEnumerable(TestSQLJoin.Data.Book1.TheView)' and 'System.Linq.Enumerable.AsEnumerable<TestSQLJoin.Data.Book1TheViewRow>(System.Collections.Generic.IEnumerable<TestSQLJoin.Data.Book1TheViewRow>)'	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	153	22	TestSQLJoin


            //var zz = (Book1.TheView)z;
            //DataTable zzz = zz.AsDataTable();

            //var zz = z.AsEnumerable();

            // Error	26	Instance argument: cannot convert from 'System.Linq.ParallelQuery<TResult>' to 'TestSQLJoin.Data.Book1.DealerContact'	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs	189	22	TestSQLJoin
            //var zz = z.AsEnumerable();


            //var zz0 = zz.First();

            //            at System.Data.DataRow.GetDataColumn(String columnName)
            //at System.Data.DataRow.get_Item(String columnName)
            //at TestSQLJoin.Data.Book1TheViewRow.op_Implicit(DataRow )




            //var e0 = z.AsEnumerable();
            //var count0 = z.Count();
            //var data0 = z.AsDataTable();

            //var data = QueryStrategyExtensions.AsDataTable(z);

            //     public static IEnumerable<Book1TheViewRow> AsEnumerable(this Book1.TheView value);
            //new Book1.TheView().AsEnumerable();

            //var zz = data.Rows.AsEnumerable().Select(x => (Book1TheViewRow)x).ToArray();


            //Book1Extensions.




            //            select 0 as Key,
            //         __h__TransparentIdentifier0.contact_Timestamp as Timestamp,
            //         'no tag' as Tag,
            //         0 as DealerOther,
            //         other.DealerOtherText as DealerOtherText,
            //         __h__TransparentIdentifier0.dealer_Key as Dealer,
            //         __h__TransparentIdentifier0.contact_Key as DealerContact,
            //         __h__TransparentIdentifier0.contact_DealerContactText as DealerContactText,
            //         __h__TransparentIdentifier0.dealer_DealerText as DealerText
            //from (select contact.DealerId as contact_DealerId,
            //                 0 as Key,
            //                 contact.Timestamp as contact_Timestamp,
            //                 'no tag' as Tag,
            //                 0 as DealerOther,
            //                 dealer.Key as dealer_Key,
            //                 contact.Key as contact_Key,
            //                 contact.DealerContactText as contact_DealerContactText,
            //                 dealer.DealerText as dealer_DealerText
            //        from `Book1.DealerContact` as contact inner join `Book1.Dealer` as dealer
            //        on contact.`DealerId` = dealer.`ID`
            //        ) as __h__TransparentIdentifier0 inner join `Book1.DealerOther` as other
            //on __h__TransparentIdentifier0.`contact_DealerId` = other.`ID`
            //limit @arg0




            Console.WriteLine("at 160");

            var z11 = z.Take(11);

            Console.WriteLine("at 164");

            var a0 = z11.AsDataTable();

            Console.WriteLine("at 168");

            var a1 = z11.AsEnumerable();

            // client cannot handle too much data! yet.
            return a1.AsResult();
        }

    }



}
