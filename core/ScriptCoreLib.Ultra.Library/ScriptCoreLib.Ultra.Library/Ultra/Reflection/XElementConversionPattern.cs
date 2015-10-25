using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace ScriptCoreLib.Ultra.Reflection
{
	public class XElementConversionPattern : ConversionPattern
	{
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151025/testdatarowkey

		public MethodInfo ToXElement
		{
			get
			{
				return this.LocalToTarget;
			}
		}

		public MethodInfo FromXElement
		{
			get
			{
				return this.TargetToLocal;
			}
		}

		public XElementConversionPattern(Type LocalType)
			: base(LocalType, typeof(XElement))
		{

		}
	}
}
