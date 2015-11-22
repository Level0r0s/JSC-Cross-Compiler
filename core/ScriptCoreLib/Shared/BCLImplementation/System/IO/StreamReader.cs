using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using ScriptCoreLib.Shared.BCLImplementation.System.IO;

namespace ScriptCoreLib.Shared.BCLImplementation.System.IO
//namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/IO/StreamReader.cs

    [Script(Implements = typeof(global::System.IO.StreamReader))]
    internal class __StreamReader : __TextReader
    {
        // Z:\jsc.svn\examples\javascript\io\DropLESTToDisplay\DropLESTToDisplay\Application.cs
        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\IO\TextReader.cs

        //        Revision: 1203
        //Author: zproxy
        //Date: 4. september 2008. a. 14:45:27
        //Message:

        //----
        //Added : /core/ScriptCoreLib/ActionScript/BCLImplementation/System/IO/StreamReader.cs
        //Added : /core/ScriptCoreLib/ActionScript/BCLImplementation/System/IO/StringReader.cs
        //Added : /core/ScriptCoreLib/ActionScript/BCLImplementation/System/IO/TextReader.cs
        //Modified : /core/ScriptCoreLib/ScriptCoreLib.csproj
        //Modified : /core/ScriptCoreLib.Avalon/ScriptCoreLib.Avalon/Shared/Avalon/Extensions/AvalonExtensions.cs



        public virtual Stream BaseStream { get { return _BaseStream; } }
        readonly Stream _BaseStream;


        public __StreamReader(Stream s)
        {
            this._BaseStream = s;
        }

        public override string ReadLine()
        {
            //Console.WriteLine("enter ReadLine");

            var m = new MemoryStream();

            var r = true;
            var any = false;

            while (r)
            {
                if (m.Length > 1024)
                {
                    r = false;
                }

                // Z:\jsc.svn\examples\javascript\io\test\TestMemoryStreamReadByte\TestMemoryStreamReadByte\Application.cs
                // does memoryStream return -1 if end of stream?
                var x = _BaseStream.ReadByte();

                if (x < 0)
                {
                    r = false;
                }
                else
                {
                    any = true;

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
            }

            if (any)
                return Encoding.UTF8.GetString(m.ToArray());



            // Z:\jsc.svn\examples\java\hybrid\ubuntu\UbuntuTCPMultiplex\Program.cs
            return null;
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
