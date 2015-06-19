using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLibAndroidNDK.WebGL
{
    // X:\jsc.svn\core\ScriptCoreLib\GLSL\Shader.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\WebGL\Shader.cs
    // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\WebGL\Shader.cs
    // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\WebGL\WebGLRenderingContext.cs


    [Script(Implements = typeof(ScriptCoreLib.GLSL.Shader))]
    internal class __Shader
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.Scene.cs

        // http://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/6230276-add-syntax-highlighting-and-intellisense-for-shad

        public override string ToString()
        {
            return "/* GLSL shader source */";
        }
    }

    [Script(Implements = typeof(ScriptCoreLib.GLSL.FragmentShader))]
    internal class __FragmentShader : __Shader
    {

    }

    [Script(Implements = typeof(ScriptCoreLib.GLSL.VertexShader))]
    internal class __VertexShader : __Shader
    {

    }
}
