using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    [Script]
    public struct mat4
    {
		// 5.6 Matrix Components

		// see also: http://cgkit.sourceforge.net/doc2/mat4.html

        // X:\jsc.svn\examples\javascript\WebGL\WebGLSpadeWarrior\WebGLSpadeWarrior\Shaders\GeometryVertexShader.cs
        // X:\jsc.svn\examples\javascript\WebGL\WebGLSpadeWarrior\WebGLSpadeWarrior\Application.cs
        public mat4(float i)
        {

        }

		#region []
		public vec4 this[int i]
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
        #endregion

        public static mat4 operator *(mat4 x, mat4 y)
        {
            throw new NotImplementedException();
        }
    }
}
