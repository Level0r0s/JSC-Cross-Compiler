using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using System.Net.Sockets;
using ScriptCoreLibJava.BCLImplementation.System.IO;
using ScriptCoreLib.Shared.BCLImplementation.System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Sockets
{
    // http://referencesource.microsoft.com/#System/net/System/Net/Sockets/NetworkStream.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Net.Sockets/NetworkStream.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\Sockets\NetworkStream.cs
    // x:\jsc.svn\core\scriptcorelibjava\bclimplementation\system\net\sockets\networkstream.cs

    [Script(Implements = typeof(global::System.Net.Sockets.NetworkStream))]
    public class __NetworkStream : __Stream
    {
        // Z:\jsc.svn\examples\java\hybrid\Test\JVMCLRSSLServerSocket\JVMCLRSSLServerSocket\Program.cs

        // tested by?

        // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/IO/Stream.cs

        public bool InternalDiagnostics;

        public global::java.io.OutputStream InternalOutputStream;
        public global::java.io.InputStream InternalInputStream;

        //public override int ReadTimeout { get; set; }
        public override int ReadTimeout { get; set; }


        public __NetworkStream()
        {
            this.ReadTimeout = 8000;
        }


        //[Obsolete("does not work for android? why?")]
        public virtual bool DataAvailable
        {
            // wont work for ubuntu either.
            get
            {
                // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Shared\IO\SmartStreamReader.cs


                //I/System.Console(10354): { DataAvailable = false, ElapsedMilliseconds = 8229 }
                //I/System.Console(10354): { ss = 0, DataAvailable = true, ElapsedMilliseconds = 8231 }

                // I/System.Console(29412): { DataAvailable = false, ElapsedMilliseconds = 8230 }
                // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\WebClient.cs

                // I/System.Console(26083): { DataAvailable = false }
                var w = Stopwatch.StartNew();
                var buffer = new byte[0];

                // I/System.Console(11709): bytes: {{ Length = 1166 }}


                // 3g network lags upto 3000ms?
                // I/System.Console(28055): { DataAvailable = false, ElapsedMilliseconds = 8151 }

                while (w.ElapsedMilliseconds < this.ReadTimeout)
                {
                    var ss = this.Read(buffer, 0, 0);
                    if (InternalDataAvailable())
                        break;

                    global::System.Threading.Thread.Sleep(1);
                }

                return InternalDataAvailable();
            }
        }

        private bool InternalDataAvailable()
        {
            var flag = false;

            try
            {
                flag = this.InternalInputStream.available() > 0;
            }
            catch
            {
            }

            return flag;
        }

        public override void Close()
        {
            Flush();

            try
            {
                if (this.InternalOutputStream != null)
                    this.InternalOutputStream.close();

                if (this.InternalInputStream != null)
                    this.InternalInputStream.close();
            }
            catch
            {
                throw;
            }
        }

        public override void Flush()
        {
            try
            {
                if (this.InternalOutputStream != null)
                    this.InternalOutputStream.flush();
            }
            catch
            {
                Console.WriteLine("err Flush ");

                throw;
            }
        }

        public override long Length
        {
            get
            {
                var i = 0;

                try
                {
                    i = this.InternalInputStream.available();
                }
                catch
                {
                }

                return i;
            }
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override long Seek(long offset, global::System.IO.SeekOrigin origin)
        {
            throw new NotImplementedException();
        }




        #region Read
        // called by?
        public override global::System.Threading.Tasks.Task<int> ReadAsync(byte[] buffer, int offset, int count)
        {
            //Console.WriteLine("enter ReadAsync");
            var c = new TaskCompletionSource<int>();

            __Task.Run(
                delegate
                {
                    //Console.WriteLine("before Read");
                    var x = default(int);

                    try
                    {
                        x = this.Read(buffer, offset, count);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("err Read " + new { ex });
                    }

                    //Console.WriteLine("after Read, SetResult " + new { x });

                    // faulty?
                    c.SetResult(x);

                    //Console.WriteLine("after Read, SetResult done");
                }
            );


            //Console.WriteLine("exit ReadAsync");
            return c.Task;
        }


        public override int Read(byte[] buffer, int offset, int count)
        {
            //Console.WriteLine("enter NetworkStream Read " + new { count, this.InternalInputStream });
            var i = -1;

            //E/AndroidRuntime( 6564): Caused by: java.net.SocketException: Socket closed
            //E/AndroidRuntime( 6564):        at org.apache.harmony.luni.platform.OSNetworkSystem.read(Native Method)
            //E/AndroidRuntime( 6564):        at dalvik.system.BlockGuard$WrappedNetworkSystem.read(BlockGuard.java:273)
            //E/AndroidRuntime( 6564):        at org.apache.harmony.luni.net.PlainSocketImpl.read(PlainSocketImpl.java:458)
            //E/AndroidRuntime( 6564):        at org.apache.harmony.luni.net.SocketInputStream.read(SocketInputStream.java:85)
            //E/AndroidRuntime( 6564):        at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__NetworkStream.Read(__NetworkStream.java:207)
            //E/AndroidRuntime( 6564):        ... 20 more

            try
            {
                i = this.InternalInputStream.read((sbyte[])(object)buffer, offset, count);

                // InternalDiagnostics

                if (i > 0)
                    if (InternalDiagnostics)
                    {
                        //Console.WriteLine("rx " + Encoding.ASCII.GetString(buffer, offset, i));
                        Console.WriteLine("rx 0x" + i.ToString("x4") + " bytes\n" + Encoding.UTF8.GetString(buffer, offset, i));
                    }
            }
            catch
            {
                // Z:\jsc.svn\examples\javascript\ubuntu\UbuntuInsecureOrigins\ApplicationWebService.cs
                // Z:\jsc.svn\examples\javascript\test\TestMultipartRelated\Application.cs

                //Console.WriteLine("err Read ");
                // bail
            }

            return i;
        }
        #endregion


        #region Write
        public override Task WriteAsync(byte[] buffer, int offset, int count)
        {
            var c = new TaskCompletionSource<object>();

            __Task.Run(
                delegate
                {
                    //E/AndroidRuntime( 4264): Caused by: java.net.SocketException: Socket closed
                    //E/AndroidRuntime( 4264):        at org.apache.harmony.luni.platform.OSNetworkSystem.write(Native Method)
                    //E/AndroidRuntime( 4264):        at dalvik.system.BlockGuard$WrappedNetworkSystem.write(BlockGuard.java:284)
                    //E/AndroidRuntime( 4264):        at org.apache.harmony.luni.net.PlainSocketImpl.write(PlainSocketImpl.java:472)
                    //E/AndroidRuntime( 4264):        at org.apache.harmony.luni.net.SocketOutputStream.write(SocketOutputStream.java:57)
                    //E/AndroidRuntime( 4264):        at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__NetworkStream.Write(__NetworkStream.java:251)
                    //E/AndroidRuntime( 4264):        ... 20 more

                    try
                    {
                        this.InternalOutputStream.write((sbyte[])(object)buffer, offset, count);
                        c.SetResult(null);
                    }
                    catch (Exception ex)
                    {
                        //throw;
                        Console.WriteLine("WriteAsync " + new { ex.Message, ex.StackTrace });

                    }

                }
            );


            return c.Task;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            try
            {
                if (InternalDiagnostics)
                {
                    //Console.WriteLine("tx " + Encoding.ASCII.GetString(buffer, offset, count));
                    Console.WriteLine("tx 0x" + count.ToString("x4") + " bytes\n" + Encoding.UTF8.GetString(buffer, offset, count));
                }

                this.InternalOutputStream.write((sbyte[])(object)buffer, offset, count);
            }
            catch
            {
                //fault RuntimeException{ Message = Connection closed by remote host, StackTrace = java.lang.RuntimeException: Connec
                //        at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__NetworkStream.Write(__NetworkStream.java:279)
                //        at UbuntuMIDExperiment.ApplicationWebServiceActivity.AtConnection(ApplicationWebServiceActivity.java:763)

                //Console.WriteLine("err Write ");

                throw;
            }

        }
        #endregion


        public static implicit operator __NetworkStream(java.io.InputStream s)
        {
            return new __NetworkStream { InternalInputStream = s };
        }

        public static implicit operator NetworkStream(__NetworkStream s)
        {
            return (NetworkStream)(object)s;
        }
    }
}
