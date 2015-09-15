using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    [Script]
    public struct mat3
    {
        // Dot products are to be to be generated!
        public float xyz { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }


        // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\synergy\s\SpheresByFelipevsfbr\Shaders\ProgramFragmentShader.cs
        // https://www.khronos.org/registry/webgl/sdk/tests/conformance/glsl/matrices/glsl-mat3-construction.html?webglVersion=1
        public mat3(
            float y0x0, float y0x1, float y0x2,
            float y1x0, float y1x1, float y1x2,
            float y2x0, float y2x1, float y2x2
            )
        {

        }
    }
}
