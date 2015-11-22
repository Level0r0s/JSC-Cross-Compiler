using ScriptCoreLib.Shared.BCLImplementation.System.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

//namespace ScriptCoreLib.ActionScript.BCLImplementation.System.IO
namespace ScriptCoreLib.Shared.BCLImplementation.System.IO
{
    [Script(Implements = typeof(global::System.IO.StreamWriter))]
    internal class __StreamWriter : __TextWriter
    {
        // Z:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Diagnostics\Process.cs

        public Stream InternalStream;

        public __StreamWriter(Stream s)
        {
            InternalStream = s;
        }

        public override void Write(string value)
        {
            Console.WriteLine("__StreamWriter Write");
            var x = Encoding.UTF8.GetBytes(value);

            this.InternalStream.Write(x, 0, x.Length);
        }


        public override Encoding Encoding
        {
            get { throw new NotImplementedException(); }
        }
    }
}
