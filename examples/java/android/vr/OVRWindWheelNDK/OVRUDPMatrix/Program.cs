using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OVRUDPMatrix
{
    unsafe class Program
    {
        static Stopwatch sw = Stopwatch.StartNew();

        // Error	8	Modifying a 'method' which contains the 'yield return' or 'yield break' statement will prevent the debug session from continuing while Edit and Continue is enabled.	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRUDPMatrix\Program.cs	19	9	OVRUDPMatrix



        

        static IEnumerable<float[,]> Invoke()
        {
            //var mvMatrix0transpose = new float[]
            //    {
            //        9, 0, 0,0,
            //        0, 4, 0, 0,
            //        0, 0, 4, 0,
            //         1.5f, 2.0f, -9.0f, 1,
            //    };

            for (int ix = 0; ix < 4; ix++)
            {
                //var scale = (float)(4f + 0.01 * Math.Sin(sw.ElapsedMilliseconds * 0.001f));
                var scale = 4f;
                //var x = 3.0f;
                var x = 1.5f + ix * 3.0f;
                var y = 2.0f + ix * 1.0f;
                var z = -9.0f + ix * -2.0f + (float)(4f + 0.1 * Math.Sin(sw.ElapsedMilliseconds * 0.001f));

                //var mat4 = new float[,]

                // what the flip. C# dimensional arrays are syntax sugar and totally useless if we want to blit them

                // can we also stream a cube to chrome?
                var vertexTransform0 = new float[,]
                {
                    // it works. source edit design time
                    { scale  * 10f, 0,0, x },
                    {  0, scale,0, y },
                    {  0, 0,scale, z },
                    {  0, 0, 0, 1.0f },
                };


                yield return vertexTransform0;

                var vertexTransform1 = new float[,]
            {
                // it works. source edit design time
                { scale, 0,0, x   },
                {  0, scale ,0, y },
                {  0, 0,scale  * 10f, z},
                {  0, 0, 0, 1.0f },
            };


                yield return vertexTransform1;

                var vertexTransform2 = new float[,]
            {
                // it works. source edit design time
                { scale, 0,0, x   },
                {  0, scale * 10f,0, y },
                {  0, 0,scale  , z},
                {  0, 0, 0, 1.0f },
            };

                yield return vertexTransform2;
            }

        }

        static void Main(string[] args)
        {
            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Numerics\Matrix4x4.cs
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150718/udpmatrix
            // https://github.com/dotnet/corefx/blob/master/src/System.Numerics.Vectors/src/System/Numerics/Matrix4x4.cs


            // 4.6
            //var vertexTransform = default(System.Numerics.Matrix4x4);
            //var vertexTransform = System.Numerics.Matrix4x4.Identity;

            //System.Numerics.Matrix4x4.CreateTranslation();


            //vertexTransform.cr
            //System.Numerics.Matrix4x4.

            //dynamic scope = new ExpandoObject();
            //scope.gearVR.cube.vertexTransform = new [] { 0f };


            // fk u. Matrix4x4 is unavailable.


            // lets roll our own.

            // X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\Shaders\VrCubeWorld.vert
            // if we dont use dimensional floats here what good are they???



            var frameID = 0;
        retry:
            frameID++;


            // Error	8	Adding a statement which contains an anonymous type will prevent the debug session from continuing while Edit and Continue is enabled.	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRUDPMatrix\Program.cs	53	13	OVRUDPMatrix
            Console.Title = "" + frameID;



            // the first. jsc.bc ? where is it?
            //new[] { vertexTransform1, vertexTransform0, vertexTransform2 }.Send("239.1.2.3", 40014);
            Invoke().ToArray().Send("239.1.2.3", 40014);


            System.Threading.Thread.Sleep(1000 / 60);
            //Debugger.Break();
            goto retry;

            //float[] mat4xy0 = mat4.ToArray();


            //var port = new Random().Next(16000, 40000);
            //var socket = new UdpClient();
            //socket.Client.Bind(new IPEndPoint(IPAddress.Parse("192.168.1.12"), port));



            //socket.Send(mat4, "239.1.2.3", 40014);


            // https://msdn.microsoft.com/en-us/library/2yd9wwz4.aspx
            // Additional information: Type 'System.Single[,]' cannot be marshaled as an unmanaged structure; no meaningful size or offset can be computed.
            //var mat4_sizeof = Marshal.SizeOf(mat4);

            //var mat4t = mat4.GetType();

            //var mat4a = (Array)mat4;

            // +		[2]	{Single& Address(Int32, Int32)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            // Error	1	'System.Array' does not contain a definition for 'Address' and no extension method 'Address' accepting a first argument of type 'System.Array' could be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRUDPMatrix\Program.cs	70	30	OVRUDPMatrix

            //var mat4xy = new float[4 * 4];






            //var data = new MemoryStream();

            //new BinaryWriter(data).Write(mat4xy0);


            //fixed (float* mat4p = &mat4[0, 0])
            //{
            //    var ii = new IntPtr(mat4p);

            //    Marshal.Copy(ii, mat4xy, 0, 4 * 4);


            //}
            //var mat4p = mat4.Address(0, 0);


            //float* mat4p = &mat4;

            //Marshal.


            // http://www.codeproject.com/Questions/120963/Passing-C-Multidimensional-Array-to-C
            // http://stackoverflow.com/questions/8428605/can-i-access-multidimensional-array-using-a-pointer


            //Marshal.

            //Marshal.DestroyStructure(



            // Additional information: The specified arrays must have the same number of dimensions.


            //Array.Copy(mat4a, mat4xy, 4 * 4);


            // http://www.tutorialspoint.com/csharp/csharp_multi_dimensional_arrays.htm


            Debugger.Break();

            // useb broadcast
            // once
            // offline
            // edit n continue
        }

        // Severity	Code	Description	Project	File	Line
        //Error CS1069  The type name 'Matrix4x4' could not be found in the namespace 'System.Numerics'. This type has been forwarded to assembly 'System.Numerics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089' Consider adding a reference to that assembly.OVRUDPMatrix X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRUDPMatrix\Program.cs	20


    }

    unsafe static class x
    {
        // inline implicit?
        public static float[] ToArray(this float[,] e)
        {
            var output = new float[e.GetLength(0) * e.GetLength(1)];

            fixed (float* p = &e[0, 0])
            {
                Marshal.Copy(new IntPtr(p), output, startIndex: 0, length: e.Length);
            }

            return output;
        }

        public static byte[] FloatArrayToByteArray(this float[] e)
        {
            // then send float bytes over udp, into jave into ndk into shader... yay.

            var m = new MemoryStream();
            var w = new BinaryWriter(m);

            foreach (var item in e)
            {
                w.Write(item);
            }

            return m.ToArray();
        }




        public static void Send(this float[][,] e, string hostname, int port)
        {
            var nicport = new Random().Next(16000, 40000);
            var socket = new UdpClient();
            // we assume we know our nic
            socket.Client.Bind(new IPEndPoint(IPAddress.Parse("192.168.1.12"), nicport));

            var ElementSize = 4 * e[0].GetLength(0) * e[0].GetLength(1);

            var data64 = new byte[e.Length * ElementSize];

            for (int ei = 0; ei < e.Length; ei++)
            {

                //fixed (byte* output = data64)
                fixed (float* inputF = &e[ei][0, 0])
                {
                    //Marshal.Copy(new IntPtr(p), new IntPtr(data), startIndex: 0, length: e.Length);
                    //Marshal.Copy(new IntPtr(p), data, startIndex: 0, length: e.Length);

                    var input8 = (byte*)inputF;

                    for (int i = 0; i < ElementSize; i++)
                    {
                        data64[i + ei * ElementSize] = input8[i];
                    }
                }
            }

            //var data64 = FloatArrayToByteArray(data);


            // can VR show our cube at the mat4 we are talking about?
            socket.Send(
                data64,
                data64.Length,
                hostname: hostname,
                port: port
            );
        }


        // UDPSend?
        public static void Send(this float[,] e, string hostname, int port)
        {
            var nicport = new Random().Next(16000, 40000);
            var socket = new UdpClient();
            // we assume we know our nic
            socket.Client.Bind(new IPEndPoint(IPAddress.Parse("192.168.1.12"), nicport));
            socket.Send(e, hostname, port);
        }

        public static void Send(this UdpClient socket, float[,] e, string hostname, int port)
        {
            //var data = new float[1 * e.GetLength(0) * e.GetLength(1)];

            //fixed (float* inputF = &e[0, 0])
            //{
            //    Marshal.Copy(new IntPtr(inputF), data, startIndex: 0, length: e.Length);
            //}

            //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  1.000000
            //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
            //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
            //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  2.000000
            //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
            //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  1.000000
            //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
            //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  3.000000
            //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
            //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
            //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  1.000000
            //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  4.000000
            //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
            //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
            //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  0.000000
            //I/xNativeActivity(15380): \VrCubeWorld.Renderer.cs:579 ovrRenderer_RenderFrame UDPDraw[]  1.000000

            //var data = stackalloc byte[4 * e.GetLength(0) * e.GetLength(1)];
            var data64 = new byte[4 * e.GetLength(0) * e.GetLength(1)];

            //fixed (byte* output = data64)
            fixed (float* inputF = &e[0, 0])
            {
                //Marshal.Copy(new IntPtr(p), new IntPtr(data), startIndex: 0, length: e.Length);
                //Marshal.Copy(new IntPtr(p), data, startIndex: 0, length: e.Length);

                var input8 = (byte*)inputF;

                for (int i = 0; i < data64.Length; i++)
                {
                    data64[i] = input8[i];
                }
            }

            //var data64 = FloatArrayToByteArray(data);


            // can VR show our cube at the mat4 we are talking about?
            socket.Send(
                data64,
                data64.Length,
                hostname: hostname,
                port: port
            );
        }
    }
}
