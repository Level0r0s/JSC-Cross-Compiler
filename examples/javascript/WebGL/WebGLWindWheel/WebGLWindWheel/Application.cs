using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using WebGLWindWheel.HTML.Pages;
using WebGLWindWheel.Shaders;

namespace WebGLWindWheel
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;



    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {
            var size = 600;
            var gl = new WebGLRenderingContext();

            var canvas = gl.canvas.AttachToDocument();

            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0, size, size);

            canvas.width = size;
            canvas.height = size;


            var gl_viewportWidth = size;
            var gl_viewportHeight = size;



            var shaderProgram = gl.createProgram(
                new GeometryVertexShader(),
                new GeometryFragmentShader()
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


            // https://github.com/toji/gl-matrix/blob/master/src/gl-matrix/mat4.js

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
                        var x = new float[]
                        {
                            1, 0, 0, 0,
                            0, 1, 0, 0,
                            0, 0, 1, 0,
                            0, 0, 0, 1,
                        };

                        //is this the best way to update array contents?
                        x.CopyTo(that, 0);

                        return x;
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

                translate = new Func<float[], float[], float[], float[]>(
                    (that, a, v) =>
                    {
                        float x = v[0], y = v[1], z = v[2];

                        if (a == that)
                        {
                            that[12] = a[0] * x + a[4] * y + a[8] * z + a[12];
                            that[13] = a[1] * x + a[5] * y + a[9] * z + a[13];
                            that[14] = a[2] * x + a[6] * y + a[10] * z + a[14];
                            that[15] = a[3] * x + a[7] * y + a[11] * z + a[15];

                            return that;
                        }

                        float a00, a01, a02, a03,
                            a10, a11, a12, a13,
                            a20, a21, a22, a23;


                        a00 = a[0]; a01 = a[1]; a02 = a[2]; a03 = a[3];
                        a10 = a[4]; a11 = a[5]; a12 = a[6]; a13 = a[7];
                        a20 = a[8]; a21 = a[9]; a22 = a[10]; a23 = a[11];

                        that[0] = a00; that[1] = a01; that[2] = a02; that[3] = a03;
                        that[4] = a10; that[5] = a11; that[6] = a12; that[7] = a13;
                        that[8] = a20; that[9] = a21; that[10] = a22; that[11] = a23;

                        that[12] = a00 * x + a10 * y + a20 * z + a[12];
                        that[13] = a01 * x + a11 * y + a21 * z + a[13];
                        that[14] = a02 * x + a12 * y + a22 * z + a[14];
                        that[15] = a03 * x + a13 * y + a23 * z + a[15];


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

            var cubesize = 1.0f * 0.05f;
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


            var rWindDelta = 0.0f;
            var rCubeDelta = 1.0f;

            if (page != null)
            {

                #region WindLeft
                page.WindLeft.onmousedown +=
                    delegate
                    {
                        rWindDelta = -2.0f;
                    };
                page.WindLeft.onmouseup +=
                 delegate
                 {
                     rWindDelta = 0.0f;
                 };
                #endregion

                #region WindRight
                page.WindRight.onmousedown +=
                    delegate
                    {
                        rWindDelta = 2.0f;
                    };
                page.WindRight.onmouseup +=
                 delegate
                 {
                     rWindDelta = 0.0f;
                 };
                #endregion


                #region SpeedSlow
                page.SpeedSlow.onmousedown +=
                    delegate
                    {
                        rCubeDelta = 0.1f;
                    };
                page.SpeedSlow.onmouseup +=
                 delegate
                 {
                     rCubeDelta = 1.0f;
                 };
                #endregion

                #region SpeedFast
                page.SpeedFast.onmousedown +=
                  delegate
                  {
                      rCubeDelta = 4.0f;
                  };
                page.SpeedFast.onmouseup +=
                 delegate
                 {
                     rCubeDelta = 1.0f;
                 };
                #endregion
            }


            #region animate
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
            #endregion

            #region degToRad
            Func<float, float> degToRad = (degrees) =>
            {
                return degrees * (f)Math.PI / 180f;
            };
            #endregion


            #region drawScene
            Action drawScene = delegate
            {
                gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                // glMatrix.mat4.perspective(
                //     45f,
                //     (float)gl_viewportWidth / (float)gl_viewportHeight,
                //     0.1f,
                //     100.0f,
                //     pMatrix
                //);

                __mat4.perspective(
                    pMatrix,

                    45f,
                    (float)gl_viewportWidth / (float)gl_viewportHeight,
                    0.1f,
                    100.0f
                    );

                __mat4.identity(mvMatrix);
                //glMatrix.mat4.identity(mvMatrix);

                __mat4.translate(mvMatrix, mvMatrix, new float[] { -1.5f, 0.0f, -7.0f });
                //glMatrix.mat4.translate(mvMatrix, new float[] { -1.5f, 0.0f, -7.0f });

                mvPushMatrix();
                //glMatrix.mat4.rotate(mvMatrix, degToRad(rWind), new float[] { 0f, 1f, 0f });
                __mat4.rotate(mvMatrix, mvMatrix, degToRad(rWind), new float[] { 0, 1f, 0f });


                #region DrawFrameworkWingAtX
                Action<float, float> DrawFrameworkWingAtX =
            (WingX, WingY) =>
            {
                #region draw center cube
                mvPushMatrix();

                __mat4.translate(mvMatrix, mvMatrix, new float[] { cubesize * WingX, cubesize * WingY, 0 });
                //glMatrix.mat4.translate(mvMatrix, new float[] { cubesize * WingX, cubesize * WingY, 0 });

                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexColorBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                setMatrixUniforms();
                //gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_SHORT, 0);
                gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_BYTE, 0);

                mvPopMatrix();
                #endregion
            };
                #endregion

                #region DrawWingAtX
                Action<int, int, float, float> DrawWingAtX =
                    (WingX, WingSize, WingRotationMultiplier, WingRotationOffset) =>
                    {
                        mvPushMatrix();

                        __mat4.translate(mvMatrix, mvMatrix, new float[] { cubesize * WingX, 0, 0 });
                        //glMatrix.mat4.translate(mvMatrix, new float[] { cubesize * WingX, 0, 0 });

                        if (WingRotationOffset == 0)
                        {
                            DrawFrameworkWingAtX(0, 0);
                        }

                        #region DrawWingPart
                        Action<float> DrawWingPart =
                    PartIndex =>
                    {
                        mvPushMatrix();
                        //glMatrix.mat4.rotate(mvMatrix, degToRad(WingRotationOffset + (rCube * WingRotationMultiplier)), new float[] { 1f, 0f, 0f });
                        __mat4.rotate(mvMatrix, mvMatrix, degToRad(WingRotationOffset + (rCube * WingRotationMultiplier)), new float[] { 1f, 0f, 0f });
                        //glMatrix.mat4.translate(mvMatrix, new float[] { 0f, cubesize * PartIndex * 2, 0 });
                        __mat4.translate(mvMatrix, mvMatrix, new float[] { 0f, cubesize * PartIndex * 2, 0 });

                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexColorBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                        setMatrixUniforms();
                        //gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_SHORT, 0);
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


                #region draw cube on the right to remind where we started
                //glMatrix.mat4.translate(mvMatrix, new float[] { 3.0f, 2.0f, 0.0f });
                __mat4.translate(mvMatrix, mvMatrix, new float[] { 3.0f, 2.0f, 0.0f });

                mvPushMatrix();
                //glMatrix.mat4.rotate(mvMatrix, degToRad(rCube), new float[] { 1f, 1f, 1f });
                __mat4.rotate(mvMatrix, mvMatrix, degToRad(rCube), new float[] { 1f, 1f, 1f });



                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                gl.bindBuffer(gl.ARRAY_BUFFER, squareVertexColorBuffer);
                gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                setMatrixUniforms();
                //gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_SHORT, 0);
                gl.drawElements(gl.TRIANGLES, cubeVertexPositionBuffer_numItems, gl.UNSIGNED_BYTE, 0);

                mvPopMatrix();
                #endregion

            };
            drawScene();
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

            #region IsDisposed
            var IsDisposed = false;

            this.Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                canvas.Orphanize();
            };
            #endregion

            #region requestFullscreen
            Native.document.body.ondblclick +=
                delegate
                {
                    if (IsDisposed)
                        return;

                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    Native.document.body.requestFullscreen();


                };
            #endregion




            //var c = 0;

            #region tick

            Native.window.onframe += e =>
                        {
                            if (IsDisposed)
                                return;

                            //c++;

                            // frameID
                            Native.document.title = "" + e.counter;

                            drawScene();
                            animate();

                        };

            #endregion

            if (page != null)
                page.Mission.Orphanize().AttachToDocument().style.SetLocation(left: size - 56, top: 16);
        }

        public Action Dispose;

    }


}
