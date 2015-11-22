using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;
using java.lang.reflect;
using java.lang;

namespace ScriptCoreLibJava.BCLImplementation.System.Reflection
{
    // http://referencesource.microsoft.com/#mscorlib/system/reflection/methodbase.cs

    [Script(Implements = typeof(MethodBase))]
    public abstract class __MethodBase : __MemberInfo
    {
        public virtual bool InternalIsAbstract()
        {
            throw new NotImplementedException();
        }


        public virtual bool InternalIsStatic()
        {
            throw new NotImplementedException();
        }

        public virtual bool InternalIsPublic()
        {
            throw new NotImplementedException();
        }

        public virtual bool InternalIsFamily()
        {
            throw new NotImplementedException();
        }

        public virtual object InternalInvoke(object obj, object[] parameters)
        {
            throw new NotImplementedException();
        }

        public bool IsAbstract { get { return InternalIsAbstract(); } }

        public bool IsStatic { get { return InternalIsStatic(); } }

        public bool IsPublic { get { return InternalIsPublic(); } }

        public bool IsFamily { get { return InternalIsFamily(); } }

        public object Invoke(object obj, object[] parameters)
        {
            return InternalInvoke(obj, parameters);
        }



        public abstract ParameterInfo[] GetParameters();


        public override string ToString()
        {
            // System.Collections.Generic.IEnumerable`1[System.Xml.Linq.XElement] _Elements_b__1(T)

            var w = new StringBuilder();

            w.Append(this.Name);
            w.Append("(");

            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs
            var p = this.GetParameters();
            if (p == null)
            {
                w.Append("?");
            }
            else
            {
                for (int i = 0; i < p.Length; i++)
                {
                    if (i > 0)
                        w.Append(", ");

                    w.Append(p[i].ParameterType.FullName);
                }

            }
            w.Append(")");

            return w.ToString();
        }



        public virtual MethodBody GetMethodBody()
        {
            // Z:\jsc.svn\examples\javascript\ubuntu\Test\TestApplicationTypeInfo\TestApplicationTypeInfo\Application.cs
            // ENC databinding?

            // would we allow some methods to be defined in IL?
            // would need runtime IL decompiler to run in webview then?
            // run on worker thread, load async on demand, only if debuger attached to server?

            return (MethodBody)(object)new __MethodBody { };
        }



        // Z:\jsc.svn\examples\java\hybrid\ubuntu\UbuntuTCPMultiplex\Program.cs
        // jsc meta should replace it to ldtoken ref?
        public static MethodBase GetCurrentMethod()
        {
            throw null;
        }
    }
}
