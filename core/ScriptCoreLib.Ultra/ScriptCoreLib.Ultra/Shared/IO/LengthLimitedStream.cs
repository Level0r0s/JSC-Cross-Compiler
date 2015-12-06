using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.IO
{
    public class LengthLimitedStream : Stream
    {
        public Stream BaseStream;

        public override bool CanRead
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanSeek
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanWrite
        {
            get { throw new NotImplementedException(); }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public long InternalLength;
        public override long Length
        {
            get { return InternalLength; }
        }

        public override long Position
        {
            get;
            set;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            // Z:\jsc.svn\examples\javascript\Test\TestMultipartRelated\Application.cs
            //Console.WriteLine("LengthLimitedStream Read " + new { Length, BaseStream });

            var c = count;

            if (Length > 0)
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201512/20151206
                // what if we dont know the len?

                c = (int)Math.Min(Length - Position, count);
            }

            var BaseStreamRead = this.BaseStream.Read(buffer, offset, c);

            Position += BaseStreamRead;

            //Console.WriteLine("LengthLimitedStream Read " + new { BaseStreamRead });

            return BaseStreamRead;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
