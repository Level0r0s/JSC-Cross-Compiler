using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{

    [Script]
    public struct sampler2D
    {
        // what about  samplerExternalOES 
        // on android, one process would produce a texture while another uses it?


        // http://stackoverflow.com/questions/6414003/using-surfacetexture-in-android

        // https://www.khronos.org/registry/gles/extensions/OES/OES_EGL_image_external.txt
        // http://stackoverflow.com/questions/14514768/android-how-to-use-samplerexternaloes-and-sampler2d-in-same-fragment-shader
    }
}
