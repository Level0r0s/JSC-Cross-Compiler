using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPWindWheel
{
    class Program
    {
        static Stopwatch sw = Stopwatch.StartNew();


        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150720

        // "X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\UDPWindWheel.sln"
        static void drawElements(Action<float[]> uniformMatrix4fv)
        {
            //Matrix4x4
            // 15680 udp

            var rWind = sw.ElapsedMilliseconds * 0.01f;
            var rCube = sw.ElapsedMilliseconds * 0.09f;



            #region __mat4
            var __mat4 = new
            {
                // X:\jsc.svn\examples\javascript\Test\TestFloatArray\TestFloatArray\Application.cs
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/20150706/20150708

                // generic in the sens of caller choosing is the return type a new struct or out ref?
                perspective = new Func<float[], float, float, float, float, float[]>(
                    (that, fovy, aspect, near, far) =>
                    {
                        var f = 1.0f / (float)Math.Tan(fovy / 2f);
                        var nf = 1f / (near - far);

                        that[0] = f / aspect;
                        that[1] = 0;
                        that[2] = 0;
                        that[3] = 0;
                        that[4] = 0;
                        that[5] = f;
                        that[6] = 0;
                        that[7] = 0;
                        that[8] = 0;
                        that[9] = 0;
                        that[10] = (far + near) * nf;
                        that[11] = -1;
                        that[12] = 0;
                        that[13] = 0;
                        that[14] = (2 * far * near) * nf;
                        that[15] = 0;

                        return that;
                    }),

                // reset content of mat4
                identity = new Func<float[], float[]>(
                    that =>
                    {
                        //Array.Copy()

                        //var xx =&that;
                        var xx = new float[]
                        {
                            1, 0, 0, 0,
                            0, 1, 0, 0,
                            0, 0, 1, 0,
                            0, 0, 0, 1,
                        };

                        //is this the best way to update array contents?
                        xx.CopyTo(that, 0);

                        return xx;
                    }
                ),

                create = new Func<float[]>(
                    () =>
                    //new mat4()
                    new float[]
                    {
                        1, 0, 0, 0,
                        0, 1, 0, 0,
                        0, 0, 1, 0,
                        0, 0, 0, 1,
                    }
                ),

                #region not used?
                clone = new Func<float[], float[]>(
                    smat4 =>
                    //new mat4()
                    new float[]
                    {
                        smat4[0], smat4[1], smat4[2], smat4[3],
                        smat4[4], smat4[5], smat4[6], smat4[7],
                        smat4[8], smat4[9], smat4[10], smat4[11],
                        smat4[12], smat4[13], smat4[14], smat4[15],
                    }
                )
                #endregion

                ,
                // X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\References\VrApi.ovrMatrix4f.cs
                translate = new Func<float[], float[], float[], float[]>(
                    (float[] that, float[] output, float[] xyz) =>
                    {
                        float xx = xyz[0], y = xyz[1], z = xyz[2];

                        if (output == that)
                        {
                            that[12] = output[0] * xx + output[4] * y + output[8] * z + output[12];
                            that[13] = output[1] * xx + output[5] * y + output[9] * z + output[13];
                            that[14] = output[2] * xx + output[6] * y + output[10] * z + output[14];
                            that[15] = output[3] * xx + output[7] * y + output[11] * z + output[15];

                            return that;
                        }

                        float a00, a01, a02, a03,
                            a10, a11, a12, a13,
                            a20, a21, a22, a23;


                        a00 = output[0]; a01 = output[1]; a02 = output[2]; a03 = output[3];
                        a10 = output[4]; a11 = output[5]; a12 = output[6]; a13 = output[7];
                        a20 = output[8]; a21 = output[9]; a22 = output[10]; a23 = output[11];

                        that[0] = a00; that[1] = a01; that[2] = a02; that[3] = a03;
                        that[4] = a10; that[5] = a11; that[6] = a12; that[7] = a13;
                        that[8] = a20; that[9] = a21; that[10] = a22; that[11] = a23;

                        that[12] = a00 * xx + a10 * y + a20 * z + output[12];
                        that[13] = a01 * xx + a11 * y + a21 * z + output[13];
                        that[14] = a02 * xx + a12 * y + a22 * z + output[14];
                        that[15] = a03 * xx + a13 * y + a23 * z + output[15];


                        return that;
                    }
                ),

                rotate = new Func<float[], float[], float, float[], float[]>(
                    (that, a, rad, axis) =>
                    {
                        float x = axis[0], y = axis[1], z = axis[2];
                        float len = (float)Math.Sqrt(x * x + y * y + z * z),
                        s, c, t,
                        a00, a01, a02, a03,
                        a10, a11, a12, a13,
                        a20, a21, a22, a23,
                        b00, b01, b02,
                        b10, b11, b12,
                        b20, b21, b22;


                        if (Math.Abs(len) < float.Epsilon)
                            return that;

                        len = 1f / len;
                        x *= len;
                        y *= len;
                        z *= len;

                        s = (float)Math.Sin(rad);
                        c = (float)Math.Cos(rad);
                        t = 1 - c;

                        a00 = a[0]; a01 = a[1]; a02 = a[2]; a03 = a[3];
                        a10 = a[4]; a11 = a[5]; a12 = a[6]; a13 = a[7];
                        a20 = a[8]; a21 = a[9]; a22 = a[10]; a23 = a[11];

                        // Construct the elements of the rotation matrix
                        b00 = x * x * t + c; b01 = y * x * t + z * s; b02 = z * x * t - y * s;
                        b10 = x * y * t - z * s; b11 = y * y * t + c; b12 = z * y * t + x * s;
                        b20 = x * z * t + y * s; b21 = y * z * t - x * s; b22 = z * z * t + c;

                        // Perform rotation-specific matrix multiplication
                        that[0] = a00 * b00 + a10 * b01 + a20 * b02;
                        that[1] = a01 * b00 + a11 * b01 + a21 * b02;
                        that[2] = a02 * b00 + a12 * b01 + a22 * b02;
                        that[3] = a03 * b00 + a13 * b01 + a23 * b02;
                        that[4] = a00 * b10 + a10 * b11 + a20 * b12;
                        that[5] = a01 * b10 + a11 * b11 + a21 * b12;
                        that[6] = a02 * b10 + a12 * b11 + a22 * b12;
                        that[7] = a03 * b10 + a13 * b11 + a23 * b12;
                        that[8] = a00 * b20 + a10 * b21 + a20 * b22;
                        that[9] = a01 * b20 + a11 * b21 + a21 * b22;
                        that[10] = a02 * b20 + a12 * b21 + a22 * b22;
                        that[11] = a03 * b20 + a13 * b21 + a23 * b22;

                        if (a != that)
                        { // If the source and destination differ, copy the unchanged last row
                            that[12] = a[12];
                            that[13] = a[13];
                            that[14] = a[14];
                            that[15] = a[15];
                        }


                        return that;
                    }
                )
            };
            #endregion

            var mvMatrix = __mat4.create();
            var mvMatrixStack = new Stack<float[]>();

            // set to perspective
            var pMatrix = __mat4.create();

            #region new in lesson 03
            // how would this translate to non GC, NDK?
            Action mvPushMatrix = delegate
            {
                var copy = __mat4.create();

                mvMatrix.CopyTo(copy, 0);

                //glMatrix.mat4.set(mvMatrix, copy);
                mvMatrixStack.Push(copy);
            };

            Action mvPopMatrix = delegate
            {
                mvMatrix = mvMatrixStack.Pop();
            };
            #endregion



            #region degToRad
            Func<float, float> degToRad = (degrees) =>
            {
                return degrees * (float)Math.PI / 180f;
            };
            #endregion




            //var cubesize = 8;
            // X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\VrCubeWorld.Geometry.cs
            //var cubesize = 0.125f;


            var cubesize = 16 / 128f;

            __mat4.identity(mvMatrix);
            __mat4.translate(mvMatrix, mvMatrix, new float[] {
                // left 
                1.0f,
                // up?
                3.0f,
                // distance
                -6.0f });

            #region windwheel
            mvPushMatrix();
            __mat4.rotate(mvMatrix, mvMatrix, degToRad(rWind), new float[] { 0, 1f, 0f });

            #region DrawFrameworkWingAtX
            Action<float, float> DrawFrameworkWingAtX = (WingX, WingY) =>
            {
                mvPushMatrix();

                __mat4.translate(mvMatrix, mvMatrix, new float[] { cubesize * WingX, cubesize * WingY, 0 });

                uniformMatrix4fv(mvMatrix);

                mvPopMatrix();
            };
            #endregion

            #region DrawWingAtX
            Action<int, int, float, float> DrawWingAtX = (WingX, WingSize, WingRotationMultiplier, WingRotationOffset) =>
            {
                mvPushMatrix();
                __mat4.translate(mvMatrix, mvMatrix, new float[] { cubesize * WingX, 0, 0 });

                if (WingRotationOffset == 0)
                {
                    DrawFrameworkWingAtX(0, 0);
                }

                #region DrawWingPart
                Action<float> DrawWingPart = PartIndex =>
                {
                    mvPushMatrix();
                    __mat4.rotate(mvMatrix, mvMatrix, degToRad(WingRotationOffset + (rCube * WingRotationMultiplier)), new float[] { 1f, 0f, 0f });
                    __mat4.translate(mvMatrix, mvMatrix, new float[] { 0f, cubesize * PartIndex * 2, 0 });

                    uniformMatrix4fv(mvMatrix);


                    mvPopMatrix();
                };
                #endregion

                #region DrawWingWithSize
                Action<int> DrawWingWithSize =
                    length =>
                    {
                        for (int i = 4; i < length; i++)
                        {
                            DrawWingPart(i * 1.0f);
                            DrawWingPart(-i * 1.0f);

                        }
                    };
                #endregion

                DrawWingWithSize(WingSize);

                mvPopMatrix();

            };
            #endregion

            {
                var x = 8;

                DrawFrameworkWingAtX(x - 8, 0);

                for (int i = 0; i < 24; i++)
                {
                    DrawFrameworkWingAtX(x - 8, -2.0f * i);

                }

                DrawWingAtX(x - 6, 0, 1f, 0);
                DrawWingAtX(x - 4, 0, 1f, 0);
                DrawWingAtX(x - 2, 0, 1f, 0);

                DrawWingAtX(x + 0, 16, 1f, 0);
                DrawWingAtX(x + 0, 16, 1f, 30);
                DrawWingAtX(x + 0, 16, 1f, 60);
                DrawWingAtX(x + 0, 16, 1f, 90);
                DrawWingAtX(x + 0, 16, 1f, 120);
                DrawWingAtX(x + 0, 16, 1f, 150);

                DrawWingAtX(x + 2, 0, 1f, 0);
                DrawWingAtX(x + 4, 0, 1f, 0);
                DrawWingAtX(x + 6, 0, 1f, 0);

                DrawWingAtX(x + 8, 12, 0.4f, 0);
                DrawWingAtX(x + 8, 12, 0.4f, 60);
                DrawWingAtX(x + 8, 12, 0.4f, 120);


                DrawWingAtX(x + 8 + 2, 0, 1f, 0);
                DrawWingAtX(x + 8 + 4, 0, 1f, 0);
                DrawWingAtX(x + 8 + 6, 0, 1f, 0);

                DrawWingAtX(x + 16, 8, 0.3f, 0);
                DrawWingAtX(x + 16, 8, 0.3f, 90);
            }

            mvPopMatrix();
            #endregion




        }

        [DebuggerHidden]
        [DebuggerNonUserCode]
        static void Main(string[] args)
        {


            var frameID = 0;
            retry:
            frameID++;


            // Error	8	Adding a statement which contains an anonymous type will prevent the debug session from continuing while Edit and Continue is enabled.	X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRUDPMatrix\Program.cs	53	13	OVRUDPMatrix
            Console.Title = Process.GetCurrentProcess().Id + " frame:" + frameID;

            var a = new List<float[]>();


            drawElements(
                mvMatrix0 =>
                {
                    // detranspose?


                    a.Add(
                        new[] {
                            mvMatrix0[0], mvMatrix0[4], mvMatrix0[8],mvMatrix0[12],
                            mvMatrix0[1],mvMatrix0[5],mvMatrix0[9],mvMatrix0[13],
                            mvMatrix0[2],mvMatrix0[6],mvMatrix0[10],mvMatrix0[14],
                            mvMatrix0[3],mvMatrix0[7],mvMatrix0[11],mvMatrix0[15]
                        }
                    );
                }
            );

            a.ToArray().Send("239.1.2.3", 40014);

            System.Threading.Thread.Sleep(1000 / 60);
            //Debugger.Break();
            goto retry;
        }
    }

    unsafe static class x
    {

        public static void Send(this float[][] e, string hostname, int port)
        {
            var nicport = new Random().Next(16000, 40000);

            // https://twitter.com/ID_AA_Carmack/status/623585590848700416

            // X:\jsc.svn\examples\java\android\Test\TestUDPSend\TestUDPSend\ApplicationActivity.cs
            var socket = new UdpClient();
            // we assume we know our nic
            socket.Client.Bind(new IPEndPoint(IPAddress.Parse("192.168.1.12"), nicport));

            //var ElementSize = 4 * e[0].GetLength(0) * e[0].GetLength(1);
            var ElementSize = 4 * e[0].GetLength(0);

            var data64 = new byte[e.Length * ElementSize];

            for (int ei = 0; ei < e.Length; ei++)
            {

                //fixed (byte* output = data64)
                fixed (float* inputF = &e[ei][0])
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

            //         Unhandled Exception: System.Net.Sockets.SocketException: An operation on a socket could not be performed because the system lacked sufficient buffer space or because a queue was full
            //at System.Net.Sockets.Socket.SendTo(Byte[] buffer, Int32 offset, Int32 size, SocketFlags socketFlags, EndPoint remoteEP)
            //at System.Net.Sockets.UdpClient.Send(Byte[] dgram, Int32 bytes, String hostname, Int32 port)
            //at UDPWindWheel.x.Send(Single[][] e, String hostname, Int32 port) in x:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\UDPWindWheel\Program.cs:line 454
            //at UDPWindWheel.Program.Main(String[] args) in x:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\UDPWindWheel\Program.cs:line 409










            // can VR show our cube at the mat4 we are talking about?
            socket.Send(
                data64,
                data64.Length,
                hostname: hostname,
                port: port
            );

            socket.Dispose();
        }
    }
}
