using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;

namespace ScriptCoreLibNative.BCLImplementation.System
{
    // X:\opensource\github\WootzJs\WootzJs.Runtime\Console.cs
    // http://referencesource.microsoft.com/#mscorlib/system/console.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Console.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Console.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Console.cs
    // https://github.com/dotnet/corefx/blob/master/src/System.Console/src/System/Console.cs
    // https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/Console.cs
    // https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Console.cs
    // https://github.com/sq/JSIL/blob/master/Proxies/Console.cs

    // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Console.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Console.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Console.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Console.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Console.cs


    [Script(Implements = typeof(global::System.Console))]
    internal class __Console
    {
        // tested by
        // X:\jsc.svn\examples\c\Test\TestConsoleWriteLine\TestConsoleWriteLine\Program.cs


        public static ConsoleColor ForegroundColor
        {
            get
            {
                // fake it
                return ConsoleColor.Gray;
            }

            set
            {
                windows_h.SetConsoleTextAttribute(
                    windows_h.GetStdHandle(windows_h.STD_OUTPUT_HANDLE)
                    , (int)value);
            }
        }

        public static void Beep()
        {
            windows_h.Beep(400, 200);
        }

        public static void Beep(int f, int d)
        {
            windows_h.Beep(f, d);
        }

        public static void Write(double i)
        {
            stdio_h.printf("%g", __arglist(i));
        }

        public static void Write(int i)
        {
            stdio_h.printf("%d", __arglist(i));
        }


        public static void Write(long i)
        {
            // X:\jsc.svn\examples\c\Test\TestFieldInitFixedDimensionalArray\TestFieldInitFixedDimensionalArray\Program.cs
            stdio_h.printf("%lld", __arglist(i));
        }

        public static void Write(char c)
        {
            stdio_h.putchar(c);

            // http://stackoverflow.com/questions/14680252/difference-between-puts-and-printf-in-c-while-using-sleep
            //stdio_h.fflush();

        }

        public static void Write(string e)
        {
            stdio_h.printf("%s", __arglist(e));


            //foreach (var c in e)
            //{
            //    Write(c);
            //}
        }

        public static void WriteLine()
        {
            WriteLine("");
        }

        public static void WriteLine(int e)
        {
            Write(e);
            WriteLine();
        }
        public static void WriteLine(string e)
        {
            stdio_h.puts(e);
        }
    }

}
