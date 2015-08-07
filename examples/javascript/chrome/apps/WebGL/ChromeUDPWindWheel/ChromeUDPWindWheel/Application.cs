using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ChromeUDPWindWheel;
using ChromeUDPWindWheel.Design;
using ChromeUDPWindWheel.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;

namespace ChromeUDPWindWheel
{
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150719/chromeudpwindwheel

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            #region += Launched chrome.app.window
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
                {
                    Console.WriteLine("chrome.app.window.create, is that you?");

                    // pass thru
                }
                else
                {
                    // should jsc send a copresence udp message?
                    chrome.runtime.UpdateAvailable += delegate
                    {
                        new chrome.Notification(title: "UpdateAvailable");

                    };

                    chrome.app.runtime.Launched += async delegate
                    {
                        // 0:12094ms chrome.app.window.create {{ href = chrome-extension://aemlnmcokphbneegoefdckonejmknohh/_generated_background_page.html }}
                        Console.WriteLine("chrome.app.window.create " + new { Native.document.location.href });

                        new chrome.Notification(title: "ChromeUDPSendAsync");

                        // https://developer.chrome.com/apps/app_window#type-CreateWindowOptions
                        var xappwindow = await chrome.app.window.create(
                               Native.document.location.pathname, options: new
                               {
                                   alwaysOnTop = true,
                                   visibleOnAllWorkspaces = true
                               }
                        );

                        //xappwindow.setAlwaysOnTop

                        xappwindow.show();

                        await xappwindow.contentWindow.async.onload;

                        Console.WriteLine("chrome.app.window loaded!");
                    };


                    return;
                }
            }
            #endregion



            // X:\jsc.svn\examples\javascript\WorkerMD5Experiment\WorkerMD5Experiment\Application.cs
            // "C:\Users\Arvo\AppData\Local\Google\Chrome SxS\Application\chrome.exe - es3.lnk"


            var size = 600;
            var gl = new WebGLRenderingContext();

            var canvas = gl.canvas.AttachToDocument();

            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0, size, size);

            canvas.width = size;
            canvas.height = size;


            var gl_viewportWidth = size;
            var gl_viewportHeight = size;


            // can AssetLibrary create a special type
            // and define the variables
            // for it we need to parse glsl?
            // if we dont parse the code yet,
            // do we parse the macros and fields neogh already to do that?

            // Geometry
            var shaderProgram = gl.createProgram(
                new WebGLWindWheel.Shaders.GeometryVertexShader(),
                new WebGLWindWheel.Shaders.GeometryFragmentShader()
           );



            gl.linkProgram(shaderProgram);
            gl.useProgram(shaderProgram);

            var shaderProgram_vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");

            gl.enableVertexAttribArray((uint)shaderProgram_vertexPositionAttribute);

            // new in lesson 02
            var shaderProgram_vertexColorAttribute = gl.getAttribLocation(shaderProgram, "aVertexColor");
            gl.enableVertexAttribArray((uint)shaderProgram_vertexColorAttribute);

            var shaderProgram_pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix");
            var shaderProgram_mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix");

            // https://hacks.mozilla.org/2014/10/introducing-simd-js/
            // https://github.com/toji/gl-matrix/blob/master/src/gl-matrix/mat4.js
            // http://www.i-programmer.info/news/167-javascript/8578-chrome-to-support-simdjs.html

            // https://code.google.com/p/v8/issues/detail?id=2228
            //var SIMD_mat4 = new System.Numerics.Matrix4x4();
            //var SIMD_mat4s = new Stack<System.Numerics.Matrix4x4>();


            // can we convert this code to NDK friendly non GC library?
            // for gearVR 90FOV and cardboard wearality 150FOV
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



            // set to identity
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


            #region setMatrixUniforms
            Action setMatrixUniforms =
                delegate
                {
                    gl.uniformMatrix4fv(shaderProgram_pMatrixUniform, false, pMatrix);
                    gl.uniformMatrix4fv(shaderProgram_mvMatrixUniform, false, mvMatrix);
                };
            #endregion




            #region init buffers


            #region cube
            var cubeVertexPositionBuffer = new WebGLBuffer(gl);
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);

            //var cubesize = 1.0f * 0.05f;
            //var cubesize = 1.0f * 0.1f;
            var cubesize = 16 / 128f;

            //var cubesize = 1.0f;
            var vertices = new[]{
                // Front face
                -cubesize, -cubesize,  cubesize,
                 cubesize, -cubesize,  cubesize,
                 cubesize,  cubesize,  cubesize,
                -cubesize,  cubesize,  cubesize,

                // Back face
                -cubesize, -cubesize, -cubesize,
                -cubesize,  cubesize, -cubesize,
                 cubesize,  cubesize, -cubesize,
                 cubesize, -cubesize, -cubesize,

                // Top face
                -cubesize,  cubesize, -cubesize,
                -cubesize,  cubesize,  cubesize,
                 cubesize,  cubesize,  cubesize,
                 cubesize,  cubesize, -cubesize,

                // Bottom face
                -cubesize, -cubesize, -cubesize,
                 cubesize, -cubesize, -cubesize,
                 cubesize, -cubesize,  cubesize,
                -cubesize, -cubesize,  cubesize,

                // Right face
                cubesize, -cubesize, -cubesize,
                 cubesize,  cubesize, -cubesize,
                 cubesize,  cubesize,  cubesize,
                 cubesize, -cubesize,  cubesize,

                // Left face
                -cubesize, -cubesize, -cubesize,
                -cubesize, -cubesize,  cubesize,
                -cubesize,  cubesize,  cubesize,
                -cubesize,  cubesize, -cubesize
            };


            gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW);

            var cubeVertexPositionBuffer_itemSize = 3;
            var cubeVertexPositionBuffer_numItems = 6 * 6;

            var squareVertexColorBuffer = new WebGLBuffer(gl);
            gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexColorBuffer);

            // 216, 191, 18
            var colors = new[]{
                1.0f, 0.6f, 0.0f, 1.0f, // Front face
                1.0f, 0.6f, 0.0f, 1.0f, // Front face
                1.0f, 0.6f, 0.0f, 1.0f, // Front face
                1.0f, 0.6f, 0.0f, 1.0f, // Front face

                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face
                0.8f, 0.4f, 0.0f, 1.0f, // Back face

                0.9f, 0.5f, 0.0f, 1.0f, // Top face
                0.9f, 0.5f, 0.0f, 1.0f, // Top face
                0.9f, 0.5f, 0.0f, 1.0f, // Top face
                0.9f, 0.5f, 0.0f, 1.0f, // Top face

                1.0f, 0.5f, 0.0f, 1.0f, // Bottom face
                1.0f, 0.5f, 0.0f, 1.0f, // Bottom face
                1.0f, 0.5f, 0.0f, 1.0f, // Bottom face
                1.0f, 0.5f, 0.0f, 1.0f, // Bottom face


                1.0f, 0.8f, 0.0f, 1.0f, // Right face
                1.0f, 0.8f, 0.0f, 1.0f, // Right face
                1.0f, 0.8f, 0.0f, 1.0f, // Right face
                1.0f, 0.8f, 0.0f, 1.0f, // Right face

                1.0f, 0.8f, 0.0f, 1.0f,  // Left face
                1.0f, 0.8f, 0.0f, 1.0f,  // Left face
                1.0f, 0.8f, 0.0f, 1.0f,  // Left face
                1.0f, 0.8f, 0.0f, 1.0f  // Left face
            };



            gl.bufferData(gl.ARRAY_BUFFER, colors, gl.STATIC_DRAW);
            var cubeVertexColorBuffer_itemSize = 4;
            var cubeVertexColorBuffer_numItems = 24;

            // ELEMENT_ARRAY_BUFFER : WebGLBuffer?
            // drawElements
            var cubeVertexIndexBuffer = new WebGLBuffer(gl);
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
            //var cubeVertexIndices = new UInt16[]{
            var cubeVertexIndices = new byte[]{
                0, 1, 2,      0, 2, 3,    // Front face
                4, 5, 6,      4, 6, 7,    // Back face
                8, 9, 10,     8, 10, 11,  // Top face
                12, 13, 14,   12, 14, 15, // Bottom face
                16, 17, 18,   16, 18, 19, // Right face
                20, 21, 22,   20, 22, 23  // Left face
            };

            // ushort[]?
            //gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(cubeVertexIndices), gl.STATIC_DRAW);
            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndices, gl.STATIC_DRAW);
            var cubeVertexIndexBuffer_itemSize = 1;
            var cubeVertexIndexBuffer_numItems = 36;

            #endregion

            #endregion




            gl.clearColor(0.0f, 0.0f, 0.0f, alpha: 1.0f);
            gl.enable(gl.DEPTH_TEST);


            var rWindDelta = 0.1f;
            var rCubeDelta = 1.0f;




            var rCube = 0f;
            var rWind = 0f;

            var lastTime = 0L;
            Action animate = delegate
            {
                var timeNow = new IDate().getTime();
                if (lastTime != 0)
                {
                    var elapsed = timeNow - lastTime;

                    rCube -= ((75 * elapsed) / 1000.0f) * rCubeDelta;
                    rWind -= ((75 * elapsed) / 1000.0f) * rWindDelta;
                }
                lastTime = timeNow;
            };

            #region degToRad
            Func<float, float> degToRad = (degrees) =>
            {
                return degrees * (float)Math.PI / 180f;
            };
            #endregion




            #region AtResize
            Action AtResize =
                delegate
                {
                    gl_viewportWidth = Native.window.Width;
                    gl_viewportHeight = Native.window.Height;

                    canvas.style.SetLocation(0, 0, gl_viewportWidth, gl_viewportHeight);

                    canvas.width = gl_viewportWidth;
                    canvas.height = gl_viewportHeight;
                };

            Native.window.onresize +=
                            e =>
                            {
                                AtResize();
                            };
            AtResize();
            #endregion

            #region ui to vr
            var keys_ad = 0;
            var keys_ws = 0;
            var keys_c = 0;

            var mousebutton = 0;
            var mousewheel = 0;

            var mx = 0;
            var my = 0;

            #endregion


            canvas.tabIndex = 1;


            #region onkeydown
            canvas.onkeydown +=
                 async e =>
                 {
                     var A = e.KeyCode == 65;
                     var D = e.KeyCode == 68;

                     if (A || D)
                     {
                         keys_ad = e.KeyCode;

                         //Native.document.title = new { e.CursorX, e.CursorY }.ToString();
                         //wasd.innerText = new { e.KeyCode, ad = keys_ad, ws = keys_ws }.ToString();

                         while ((await canvas.async.onkeyup).KeyCode != e.KeyCode) ;

                         //var ee = await div.async.onkeyup;

                         keys_ad = 0;

                         //wasd.innerText = new { e.KeyCode, ad = keys_ad, ws = keys_ws }.ToString();

                         return;
                     }

                     // CS
                     // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704
                     if (e.KeyCode == 67)
                     {
                         keys_c = e.KeyCode;

                         //Native.document.title = new { e.CursorX, e.CursorY }.ToString();
                         //wasd.innerText = new { e.KeyCode, keys_ad, keys_ws, keys_c }.ToString();

                         while ((await canvas.async.onkeyup).KeyCode != e.KeyCode) ;

                         //var ee = await div.async.onkeyup;

                         keys_c = 0;

                         //wasd.innerText = new { e.KeyCode, keys_ad, keys_ws, keys_c }.ToString();

                         return;
                     }

                     {
                         keys_ws = e.KeyCode;

                         //Native.document.title = new { e.CursorX, e.CursorY }.ToString();
                         //wasd.innerText = new { e.KeyCode, ad = keys_ad, ws = keys_ws }.ToString();

                         while ((await canvas.async.onkeyup).KeyCode != e.KeyCode) ;
                         //var ee = await div.async.onkeyup;

                         keys_ws = 0;

                         //wasd.innerText = new { e.KeyCode, ad = keys_ad, ws = keys_ws }.ToString();
                     }
                 };
            #endregion


            #region onmousemove
            canvas.onmousewheel += e =>
            {
                // since we are a chrome app. is chrome sending us wheel delta too?
                mousewheel += e.WheelDirection;
            };

            canvas.onmousemove +=
                e =>
                {
                    // we could tilt the svg cursor
                    // like we do on heat zeeker:D

                    mx += e.movementX;
                    my += e.movementY;


                    //Native.document.title = new { e.CursorX, e.CursorY }.ToString();
                    //xy.innerText = new { x, y }.ToString();

                };

            canvas.onmousedown +=
                async e =>
                {
                    // wont work for RemoteApp users tho

                    mousebutton = (int)e.MouseButton;

                    // await ?
                    canvas.requestPointerLock();
                    //e.CaptureMouse();

                    var ee = await canvas.async.onmouseup;
                    //var ee = await div.async.ondblclick;

                    if (ee.MouseButton == IEvent.MouseButtonEnum.Right)
                        Native.document.exitPointerLock();

                    mousebutton = 0;

                };
            #endregion

            var vertexTransform = new float[0];

            new { }.With(
                async delegate
                {
                    var n = await chrome.socket.getNetworkList();
                    var n24 = n.Where(nn => nn.prefixLength == 24).ToArray();

                    n24.WithEach(
                        async nic =>
                        {
                            var port = new Random().Next(16000, 40000);
                            var socket = new UdpClient();
                            socket.Client.Bind(

                                //new IPEndPoint(IPAddress.Any, port: 40000)
                                new IPEndPoint(IPAddress.Parse(nic.address), port)
                            );


                            // this will eat too much memory?
                            //div.ownerDocument.defaultView.onframe +=
                            canvas.onframe +=
                                delegate
                                {
                                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704
                                    var nmessage = mx + ":" + my + ":" + keys_ad + ":" + keys_ws + ":" + keys_c + ":" + mousebutton + ":" + mousewheel;


                                    var data = Encoding.UTF8.GetBytes(nmessage);       //creates a variable b of type byte



                                    //new IHTMLPre { "about to send... " + new { data.Length } }.AttachToDocument();

                                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                                    socket.Send(
                                          data,
                                          data.Length,
                                          hostname: "239.1.2.3",
                                          port: 41814
                                      );
                                };



                            var uu = new UdpClient(40014);
                            uu.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"));
                            while (true)
                            {
                                var xx = await uu.ReceiveAsync(); // did we jump to ui thread?
                                // https://www.khronos.org/registry/typedarray/specs/latest/
                                vertexTransform = new Float32Array(new Uint8ClampedArray(xx.Buffer).buffer);
                            }
                        }
                    );


                }
            );





            var sw = Stopwatch.StartNew();

            Native.window.onframe += e =>
            {
                gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                #region pMatrix
                __mat4.perspective(
                    pMatrix,
                    90f,
                    (float)gl_viewportWidth / (float)gl_viewportHeight,
                    0.1f,
                    100.0f
                );

                //__mat4.rotate(pMatrix, pMatrix, (float)Math.PI + mx * 0.005f, new float[] { 0, 1f, 0f });
                __mat4.rotate(pMatrix, pMatrix, mx * 0.005f, new float[] { 0, 1f, 0f });

                #endregion


                //var cubesize = 1.0f * 0.05f;

                if (vertexTransform.Length == 0)
                {
                    __mat4.identity(mvMatrix);
                    __mat4.translate(mvMatrix, mvMatrix, new float[] { -1.5f, 0.0f, -3.0f });

                    #region windwheel
                    mvPushMatrix();
                    __mat4.rotate(mvMatrix, mvMatrix, degToRad(rWind), new float[] { 0, 1f, 0f });

                    #region DrawFrameworkWingAtX
                    Action<float, float> DrawFrameworkWingAtX = (WingX, WingY) =>
                    {
                        mvPushMatrix();

                        __mat4.translate(mvMatrix, mvMatrix, new float[] { cubesize * WingX, cubesize * WingY, 0 });

                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexColorBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                        setMatrixUniforms();
                        gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_BYTE, 0);

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

                                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexColorBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                                setMatrixUniforms();
                                gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_BYTE, 0);

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

                    mvPopMatrix();
                    #endregion

                }


                #region draw cube on the right to remind where we started
                //glMatrix.mat4.translate(mvMatrix, new float[] { 3.0f, 2.0f, 0.0f });

                // how do we scale it?

                //mvMatrix = __mat4.create();
                //mvMatrix = new float[]
                //{
                //        1, 0, 0, 0,
                //        0, 1, 0, 0,
                //        0, 0, 1, 0,
                //        0, 0, 0, 1,
                //};



                //f = new Float32Array( [1, 0, 0, 1.5, 0, 1, 0, 2, 0, 0, 1, -3, 0, 0, 0, 1]);
                //a[0].__mat4.identity.WRMABg6gjTWKCO_aNXgMMAA(a[0].mvMatrix);
                //a[0].__mat4.translate.YRMABiCCKT_aaNWnR3f4qdg(a[0].mvMatrix, a[0].mvMatrix, new Float32Array( [1.5, 2, -3]));





                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexColorBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);

                gl.uniformMatrix4fv(shaderProgram_pMatrixUniform, false, pMatrix);



                // https://www.physicsforums.com/threads/what-is-the-purpose-of-the-transpose.261849/
                var mvMatrix0transpose = new float[]
                {
                    9, 0, 0,0,
                    0, 4, 0, 0,
                    0, 0, 4, 0,
                     1.5f, 2.0f, -9.0f, 1,
                };
                // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPFloats\ChromeUDPFloats\Application.cs


                var ElementCount = vertexTransform.Length / (64 / 4);

                for (int ElementIndex = 0; ElementIndex < ElementCount; ElementIndex++)
                {
                    var mvMatrix0 = new float[16];

                    for (int j = 0; j < 16; j++)
                    {
                        var ff = vertexTransform[j + ElementIndex * 64 / 4];


                        //Console.WriteLine(new { j, ff });

                        mvMatrix0[j] = ff;
                    }

                    mvMatrix0transpose = new[] {
                        mvMatrix0[0], mvMatrix0[4], mvMatrix0[8],mvMatrix0[12],
                        mvMatrix0[1],mvMatrix0[5],mvMatrix0[9],mvMatrix0[13],
                        mvMatrix0[2],mvMatrix0[6],mvMatrix0[10],mvMatrix0[14],
                        mvMatrix0[3],mvMatrix0[7],mvMatrix0[11],mvMatrix0[15]
                    };

                    gl.uniformMatrix4fv(shaderProgram_mvMatrixUniform, false, mvMatrix0transpose);
                    gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_BYTE, 0);
                }

                #endregion

                animate();

            };
        }

    }
}
