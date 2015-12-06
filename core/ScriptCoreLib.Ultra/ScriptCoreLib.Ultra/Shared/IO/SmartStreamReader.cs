using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;

namespace ScriptCoreLib.Shared.IO
{
    internal static class SmartStreamReader_Test
    {
        public static void Assert()
        {
            var input = new MemoryStream(Encoding.ASCII.GetBytes("xxaa\rbbb\n ?ccc\r\nuuuu"));
            var r = new SmartStreamReader(input);
            var a = r.ReadLine();
            var b = r.ReadLine();

            var buffer = new byte[5];

            var c = r.Read(buffer, 0, buffer.Length);

            var u = r.ReadToEnd();
        }
    }


    /// <summary>
    /// A stream reader that can switch between text and binary mode.
    /// 
    /// Note this may be the first documented type for jsc developer program.
    /// 
    /// Reference: "Y:\jsc.community\zmovies\MovieAgent\MovieAgentCore\Server\Library\SmartStreamReader.cs"
    /// </summary>
    public class SmartStreamReader : Stream
    {
        // Z:\jsc.svn\examples\javascript\test\TestMultipartRelated\Application.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151022/httprequest

        // review: Y:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Web\SmartStreamReader.cs

        public readonly Stream BaseStream;




        public SmartStreamReader(Stream BaseStream)
        {
            this.BaseStream = BaseStream;
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void Flush()
        {
            throw new NotImplementedException("");
        }

        public override long Length
        {
            get { throw new NotImplementedException(""); }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException("");
            }
            set
            {
                throw new NotImplementedException("");
            }
        }



        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException("");
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException("");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException("");
        }

        //const int InternalBufferCapacity = 0x2000;
        public static int InternalBufferCapacity = 0x2000;

        byte[] InternalBuffer = new byte[InternalBufferCapacity];
        public int InternalBufferCount = 0;



        // called by?
        public override int Read(byte[] buffer, int offset, int count)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201512/20151206
            //Console.WriteLine("enter SmartStreamReader Read " + new { count, InternalBufferCount, BaseStream });


            // if count is 0 always return 0 ?
            // could jsc do a static early optimization for such cases?




            // buffer + stream
            var value = 0;

            var InternalBufferCountToBeRead = InternalBufferCount;

            if (InternalBufferCountToBeRead > count)
                InternalBufferCountToBeRead = count;

            for (int i = 0; i < InternalBufferCountToBeRead; i++)
            {
                buffer[offset + i] = InternalBuffer[i];
            }

            value += InternalBufferCountToBeRead;
            offset += InternalBufferCountToBeRead;
            count -= InternalBufferCountToBeRead;

            DiscardBuffer(InternalBufferCountToBeRead);


            // The total number of bytes read into the buffer. 
            // This can be less than the number of bytes requested 
            // if that many bytes are not currently available, 
            // or zero (0) if the end of the stream has been reached. 

            while (count > 0)
            {

                // this will hang forever?
                //Console.WriteLine("enter SmartStreamReader Read invoke BaseStream.Read " + new { this.BaseStream });
                var i = this.BaseStream.Read(buffer, offset, count);

                if (i > 0)
                {
                    value += i;
                    offset += i;
                    count -= i;


                }
                else
                {
                    // no more data, we must return
                    count = 0;


                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201512/20151206
                    // retuning 0 means ReadByte will return -1
                }

            }

            return value;
        }

        public MemoryStream ReadToMemoryStream(int ContentLength = 0)
        {
            //Console.WriteLine("enter ReadToMemoryStream " + new { ContentLength });

            // Z:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Web\HttpResponse.cs
            // Z:\jsc.svn\examples\javascript\ubuntu\Test\UbuntuTestUploadValues\ApplicationWebService.cs

            var target = new MemoryStream();

            //var xNetworkStream = this.BaseStream as NetworkStream;
            var xNetworkStream = default(NetworkStream);

            var flag = true;
            while (flag)
            {


                if (this.InternalBufferCount > 0)
                {
                    // any pending input?
                    //Console.WriteLine("ReadToMemoryStream before Write " + new { this.InternalBufferCount, xNetworkStream });
                    //Console.WriteLine("ReadToMemoryStream any pending input? " + new { this.InternalBufferCount });


                    var missingbytes = Math.Min(this.InternalBufferCount, ContentLength - (int)target.Length);

                    target.Write(this.InternalBuffer, 0, missingbytes);
                    //this.InternalBufferCount = 0;

                    DiscardBuffer(missingbytes);
                }


                if (target.Length == ContentLength)
                {
                    // done!
                    flag = false;
                }
                else
                {

                    //Console.WriteLine("ReadToMemoryStream after Write " + new { this.InternalBufferCount, xNetworkStream });

                    //if (xNetworkStream != null)
                    //{
                    //    // what are the timeouts to be?
                    //    xNetworkStream.ReadTimeout = 33;


                    //    this.InternalBufferCount = -1;

                    //    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\Sockets\NetworkStream.cs


                    //    // I/System.Console(17511): ReadToMemoryStream Write { InternalBufferCount = 179 }

                    //    if (!xNetworkStream.DataAvailable)
                    //    {
                    //        //Console.WriteLine("DataAvailable?");
                    //        Thread.Sleep(33);
                    //        // are we sure?
                    //    }

                    //    if (!xNetworkStream.DataAvailable)
                    //    {
                    //        //Console.WriteLine("DataAvailable?");
                    //        Thread.Sleep(33);
                    //        // are we sure?
                    //    }

                    //    if (!xNetworkStream.DataAvailable)
                    //    {
                    //        //Console.WriteLine("DataAvailable?");
                    //        Thread.Sleep(33);
                    //        // are we sure?
                    //    }

                    //    if (xNetworkStream.DataAvailable)
                    //    {
                    //        this.InternalBufferCount = this.BaseStream.Read(this.InternalBuffer, 0, InternalBufferCapacity);
                    //    }
                    //}
                    //else
                    {

                        var missingbytes = ContentLength - (int)target.Length;

                        //this.InternalBufferCount = this.BaseStream.Read(this.InternalBuffer, 0, InternalBufferCapacity);

                        this.InternalBufferCount = this.BaseStream.Read(this.InternalBuffer, 0, missingbytes);

                        //Console.WriteLine("ReadToMemoryStream, continue? " + new { this.InternalBufferCount });
                        flag = (this.InternalBufferCount > 0);
                    }

                }


            }

            //Console.WriteLine("exit ReadToMemoryStream " + new { target.Length });

            //this.InternalBufferCount = 0;
            return target;
        }

        public string ReadToEnd()
        {
            // Z:\jsc.svn\examples\javascript\test\TestMultipartRelated\Application.cs
            //Console.WriteLine("enter ReadToEnd " + new { InternalBufferCount });
            //var a = new StringBuilder();
            var m = new MemoryStream();


            var flag = true;
            while (flag)
            {


                // any pending input and new input!

                m.Write(this.InternalBuffer, 0, this.InternalBufferCount);
                //for (int i = 0; i < this.InternalBufferCount; i++)
                //{
                //    // 
                //    //a.Append((char)this.InternalBuffer[i]);
                //    // char3 = (short)(this.InternalBuffer[num2] & 0xff);
                //    //var ichar = (char)this.InternalBuffer[i];
                //    a.Append(ichar);
                //}

                //Console.WriteLine("ReadToEnd invoke BaseStream.Read");

                this.InternalBufferCount = this.BaseStream.Read(this.InternalBuffer, 0, InternalBufferCapacity);

                //Console.WriteLine("ReadToEnd " + new { InternalBufferCount });

                flag = (this.InternalBufferCount > 0);
            }

            //Console.WriteLine("exit ReadToEnd");
            return Encoding.UTF8.GetString(m.ToArray());
        }


        // called by?
        void DiscardBuffer(int bytes)
        {
            if (bytes < 1)
                return;

            for (int i = bytes; i < this.InternalBufferCount; i++)
            {
                this.InternalBuffer[i - bytes] = this.InternalBuffer[i];
            }

            this.InternalBufferCount -= bytes;
        }

        public string ReadLine()
        {
            var a = new StringBuilder();

            var LineFeedExcpected = false;
            var flag = true;
            while (flag)
            {
                for (int i = 0; i < this.InternalBufferCount; i++)
                {
                    // jsc cannot handle byte to char for java?
                    var b = (int)this.InternalBuffer[i];
                    var c = (char)b;

                    if (c == '\n')
                    {
                        DiscardBuffer(i + 1);
                        return a.ToString();
                    }
                    else if (LineFeedExcpected)
                    {
                        DiscardBuffer(i);

                        return a.ToString();
                    }

                    if (c == '\r')
                    {
                        LineFeedExcpected = true;
                        continue;
                    }

                    a.Append(c);
                }

                //this.InternalBufferCount = this.BaseStream.Read(this.InternalBuffer, 0, InternalBufferCapacity);
                var len = InternalBufferCapacity - this.InternalBufferCount;


                var read0 = this.BaseStream.Read(
                    this.InternalBuffer, this.InternalBufferCount, len
                );

                flag = (read0 > 0);
                this.InternalBufferCount += read0;
            }
            return a.ToString();

        }

        public void ReadBlockTo(int length, StringBuilder w)
        {
            var bytes = new byte[length];
            this.Read(bytes, 0, length);

            for (int i = 0; i < length; i++)
            {
                w.Append((char)bytes[i]);
            }
        }



        public MemoryStream ReadToBoundary(string e, bool StopIfAlreadyAtBoundary = false)
        {
            var BoundaryBytes = Encoding.UTF8.GetBytes(e);


            return ReadToBoundary(BoundaryBytes, StopIfAlreadyAtBoundary);
        }

        public MemoryStream ReadToBoundary(byte[] BoundaryBytes, bool StopIfAlreadyAtBoundary = false)
        {
            //Console.WriteLine("enter ReadToBoundary");

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151205/xmp
            // Z:\jsc.svn\examples\java\synergy\JVMCLRXMP\Program.cs

            var m = new MemoryStream();

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20130401/20130405-file-upload


            if (BoundaryBytes.Length >= this.InternalBuffer.Length)
                throw new Exception("Buffer too small");

            var flag = true;
            while (flag)
            {


                if (this.InternalBufferCount > BoundaryBytes.Length)
                {
                    // we now have enough data to look at

                    //int i = 0;
                    int i = 1;

                    if (StopIfAlreadyAtBoundary)
                        i = 0;

                    // how much of the buffer can we accept?
                    for (; i < this.InternalBufferCount - BoundaryBytes.Length + 1; i++)
                    {
                        // is this the start of the boundary?

                        if (InternalCompareBytes(this.InternalBuffer, i, BoundaryBytes))
                        {
                            // we found waldo!
                            flag = false;
                            break;
                        }

                    }

                    m.Write(this.InternalBuffer, 0, i);
                    DiscardBuffer(i);
                }


                if (flag)
                {
                    var len = InternalBufferCapacity - this.InternalBufferCount;




                    var read0 = this.BaseStream.Read(
                        this.InternalBuffer, this.InternalBufferCount, len
                    );

                    this.InternalBufferCount += read0;
                    flag = (read0 > 0);
                }
            }

            return m;
        }

        public static bool InternalCompareBytes(byte[] a, int aoffset, byte[] b)
        {
            var r = true;

            for (int i = 0; i < b.Length; i++)
            {
                if (a[aoffset + i] != b[i])
                {
                    r = false;
                }

            }

            return r;
        }
    }
}
