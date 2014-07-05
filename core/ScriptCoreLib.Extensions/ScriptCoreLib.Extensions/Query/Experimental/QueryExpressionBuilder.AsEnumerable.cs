﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;
using System.Data;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System.Data.Common;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilder
    {
        public static DbCommand GetSelectCommand<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            var c = (DbCommand)cc.CreateCommand();

            var w = new SQLWriter<TElement>(source, new IQueryStrategy[0].AsEnumerable(), Command: c);

            return c;
        }

        public static IEnumerable<TElement> AsEnumerable<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            //Console.WriteLine("enter AsEnumerable");
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs

            var c = GetSelectCommand(source, cc);



            //Console.WriteLine("before ExecuteReader");
            // this wont work for chrome?
            var r = c.ExecuteReader();
            Console.WriteLine("after ExecuteReader");

            return ReadToElements(r, source);
        }

        public static IEnumerable<TElement> ReadToElements<TElement>(DbDataReader r, IQueryStrategy<TElement> source)
        {
            while (r.Read())
            {
                Console.WriteLine("enter AsEnumerable Read");

                yield return ReadToElement<TElement>(r, source, new Tuple<MemberInfo, int>[0]);
            }

            r.Dispose();
        }

        public static TElement ReadToElement<TElement>(DbDataReader r, IQueryStrategy source, Tuple<MemberInfo, int>[] Target)
        {
            var xTake = source as xTake;
            if (xTake != null)
            {
                return ReadToElement<TElement>(r, xTake.source, Target);
            }

            var xSelect = source as xSelect;
            if (xSelect != null)
            {
                var xMemberInitExpression = xSelect.selector.Body as MemberInitExpression;
                if (xMemberInitExpression != null)
                {
                    var xRow = default(TElement);
                    var xRowType = xMemberInitExpression.NewExpression.Type;

                    if (xRowType == null)
                        Debugger.Break();


                    // X:\jsc.svn\examples\javascript\Test\TestSQLiteConnection\TestSQLiteConnection\Application.cs
                    xRow = (TElement)Activator.CreateInstance(xRowType);
                    //xRow = xMemberInitExpression.NewExpression.Constructor.Invoke(new object[0]);

                    xMemberInitExpression.Bindings.WithEachIndex(
                        (SourceBinding, i) =>
                        {
                            //var k = xSelect.selector.Parameters[0].Name + SourceBinding.Member.Name;
                            var xMemberName = "" + SourceBinding.Member.Name;

                            var v = r[xMemberName];

                            // Additional information: Object of type 'System.Int64' cannot be converted to type 'System.DateTime'.
                            var f = SourceBinding.Member as FieldInfo;

                            // is it a long?

                            Console.WriteLine(new { xMemberName, f.FieldType });


                            if (f.FieldType == typeof(DateTime))
                                v = global::ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(v);

                            f.SetValue(xRow, v);
                        }
                    );

                    return xRow;
                }

                var xNewExpression = xSelect.selector.Body as NewExpression;
                if (xNewExpression != null)
                {
                    var args = xNewExpression.Arguments.Select(
                       (SourceArgument, i) =>
                       {
                           var xMemberExpression = SourceArgument as MemberExpression;

                           var k = "" + xMemberExpression.Member.Name;

                           var v = r[k];

                           // Additional information: Object of type 'System.Int64' cannot be converted to type 'System.DateTime'.
                           var f = xMemberExpression.Member as FieldInfo;

                           if (f != null)
                               if (f.FieldType == typeof(DateTime))
                                   v = global::ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(v);

                           return v;
                       }
                   ).ToArray();

                    var xRow = (TElement)xNewExpression.Constructor.Invoke(args);



                    return xRow;
                }

                var xParameterExpression = xSelect.selector.Body as ParameterExpression;
                if (xParameterExpression != null)
                {
                    // proxy?    
                    return ReadToElement<TElement>(r, xSelect.source,

                        Target.Concat(new[] { Tuple.Create(default(MemberInfo), 0) }).ToArray()
                        );
                }
            }

            return default(TElement);
        }


    }

}
