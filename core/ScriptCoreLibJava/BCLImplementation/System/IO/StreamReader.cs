﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using ScriptCoreLib.Shared.BCLImplementation.System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/IO/StreamReader.cs

    [Script(Implements = typeof(global::System.IO.StreamReader))]
    internal class __StreamReader : __TextReader
    {
        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\IO\TextReader.cs

        readonly Stream _BaseStream;


        public __StreamReader(Stream s)
        {
            this._BaseStream = s;
        }

        public override string ReadLine()
        {
            Console.WriteLine("enter ReadLine");

            var m = new MemoryStream();

            var r = true;

            while (r)
            {
                if (m.Length > 1024)
                {
                    r = false;
                }

                var x = _BaseStream.ReadByte();

                if (x == '\n')
                {
                    r = false;
                }
                else if (x == '\r')
                {
                    x = _BaseStream.ReadByte();

                    // it better be '\n' or we have just swallowed it
                    // needs more code here...
                    r = false;
                }
                else
                {
                    m.WriteByte((byte)x);
                }
            }

            return Encoding.UTF8.GetString(m.ToArray());
        }

        public override string ReadToEnd()
        {
            var m = new MemoryStream();
            // real slow implementation
            var i = _BaseStream.ReadByte();

            while (i >= 0)
            {
                m.WriteByte((byte)i);
                i = _BaseStream.ReadByte();
            }

            return Encoding.UTF8.GetString(m.ToArray());
        }
    }
}
