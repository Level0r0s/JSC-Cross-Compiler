using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilder
    {
        // https://code.google.com/p/chromium/issues/detail?id=369239

        partial class SQLWriter<TElement>
        {
        }

        public class xOrderBySelector
        {
            public LambdaExpression keySelector;
            public bool ascending;
        }

        public class xOrderBy
        {
            public IQueryStrategy source;


            public IEnumerable<xOrderBySelector> keySelector;

            public override string ToString()
            {
                var f = keySelector.FirstOrDefault();

                if (f.ascending)
                    return "orderby ascending";

                return "orderby descending";
            }
        }

        public class xOrderBy<TElement> : xOrderBy, IQueryStrategy<TElement>
        {
        }

        public static IQueryStrategy<TElement> ThenBy<TElement, TKey>(this IQueryStrategy<TElement> source, Expression<Func<TElement, TKey>> keySelector)
        {
            var xOrderBy = source as xOrderBy;
            if (xOrderBy != null)
            {
                // flatten orderbys
                return new xOrderBy<TElement>
                {
                    source = xOrderBy.source,
                    keySelector = xOrderBy.keySelector.Concat(new[] { new xOrderBySelector { keySelector = keySelector, ascending = true } })
                };
            }

            return new xOrderBy<TElement>
            {
                source = source,
                keySelector = new[] { new xOrderBySelector { keySelector = keySelector, ascending = true } }
            };
        }

        public static IQueryStrategy<TElement> ThenByDescending<TElement, TKey>(this IQueryStrategy<TElement> source, Expression<Func<TElement, TKey>> keySelector)
        {
            var xOrderBy = source as xOrderBy;
            if (xOrderBy != null)
            {
                // flatten orderbys
                return new xOrderBy<TElement>
                {
                    source = xOrderBy.source,
                    keySelector = xOrderBy.keySelector.Concat(new[] { new xOrderBySelector { keySelector = keySelector, ascending = false } })
                };
            }

            return new xOrderBy<TElement>
            {
                source = source,
                keySelector = new[] { new xOrderBySelector { keySelector = keySelector, ascending = false } }
            };
        }

        //[Obsolete("mutable")]
        public static IQueryStrategy<TElement> OrderBy<TElement, TKey>(this IQueryStrategy<TElement> source, Expression<Func<TElement, TKey>> keySelector)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151025
            // where is this command rendered?
            // tested by Z:\jsc.svn\examples\javascript\ubuntu\Test\UbuntuTestMySQLInsert\UbuntuTestMySQLInsert\ApplicationWebService.cs

            return new xOrderBy<TElement>
            {
                source = source,
                keySelector = new[] { new xOrderBySelector { keySelector = keySelector, ascending = true } }
            };
        }


        public static IQueryStrategy<TElement> OrderByDescending<TElement, TKey>(this IQueryStrategy<TElement> source, Expression<Func<TElement, TKey>> keySelector)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestOrderByDescending\Program.cs

            return new xOrderBy<TElement>
            {
                source = source,
                keySelector = new[] { new xOrderBySelector { keySelector = keySelector, ascending = false } }
            };
        }

    }

}
