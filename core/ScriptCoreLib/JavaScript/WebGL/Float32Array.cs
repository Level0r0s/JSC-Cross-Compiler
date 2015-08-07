using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
    // 4 byte gpu float
    // float x 16 = mat4
    [Script(HasNoPrototype = true, ExternalTarget = "Float32Array", Implements = typeof(float[]))]
    public class Float32Array : ArrayBufferView
    {
        // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPFloats\ChromeUDPFloats\Application.cs

        // http://caniuse.com/#search=SIMD
        // SIMD for physics
        // http://peterjensen.github.io/html5-simd/#/7

        // X:\jsc.svn\examples\javascript\Test\TestFloatArray\TestFloatArray\Application.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/20150706/20150708
        // X:\jsc.svn\examples\javascript\WebGL\WebGLSpadeWarrior\WebGLSpadeWarrior\Application.cs



        public Float32Array(ArrayBuffer array)
		{

		}

        public Float32Array(params float[] array)
        {

        }

        public static implicit operator Float32Array(float[] a)
        {
            return (Float32Array)(object)a;
        
        }

        public static implicit operator float[](Float32Array a)
        {
            return (float[])(object)a;
        }

    }
}
