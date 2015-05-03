using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestAmbiguousNull
{
	public class x
	{
	}

	public class Class1
	{
		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150503/udp

		public static void Invoke(x e)
		{
		}

		public static void Invoke(object e)
		{
		}

		public static void Invoke()
		{
			Invoke(default(object));
			Invoke(default(x));
		}
	}
}

//Class1.Invoke_06000003(null);
//        Class1.Invoke_06000002(null);


//[javac]
//Compiling 665 source files to W:\bin\classes
//[javac] W:\src\__AnonymousTypes__AndroidIMEIActivity_AndroidActivity\__f__AnonymousType_81_0_1.java:34: error: reference to Format is ambiguous, both method Format(String, Object, Object) in __String and method Format(__IFormatProvider, String, Object[]) in __String match
//[javac]         return __String.Format(null, "{{ imei = {0} }}", objectArray2);
//    [javac]