using ScriptCoreLib.GLSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquirectangularToAzimuthal.Shaders
{
    class __ProgramFragmentShader : FragmentShader
    {
        // https://www.shadertoy.com/view/XsBSDR#

        // all fields are inferred to be uniform?

        // what about inherited fields/ or interface fields?

        #region generic
        [uniform]
        vec3 iResolution;           // viewport resolution (in pixels)
        [uniform]
        float iGlobalTime;           // shader playback time (in seconds)
                                     //[uniform]
                                     //float iChannelTime[4];       // channel playback time (in seconds)
                                     //[uniform]
                                     //vec3 iChannelResolution[4]; // channel resolution (in pixels)
        [uniform]
        vec4 iMouse;                // mouse pixel coords. xy: current (if MLB down), zw: click

        [uniform]
        vec4 iDate;                 // (year, month, day, time in seconds)
        [uniform]
        float iSampleRate;           // sound sample rate (i.e., 44100)
        #endregion


        // https://github.com/tparisi/3dsMaxWebGL/blob/master/exporter/webgl/webgl2.cpp

        //[uniform]
        //samplerXX iChannel0..3;          // input channel. XX = 2D/Cube


        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150808/equirectangular
        // set by?
        [uniform]
        //samplerCube iChannel0;
        sampler2D iChannel0;


        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151016/azimuthal
        void mainImage(out vec4 fragColor, vec2 fragCoord)
        {
            const float pi2 = 6.283185307179586476925286766559f;
            vec4 c = vec4(0.0f, 0.0f, 0.0f, 1.0f);
            vec2 uv = default(vec2);        // texture coord = scaled spherical coordinates
            float a, d;      // azimuth,distance
            d = length(fragCoord);
            if (d < 1.0)      // inside projected sphere surface
            {
                a = atan(-fragCoord.x, fragCoord.y);
                if (a < 0.0) a += pi2;
                if (a > pi2) a -= pi2;
                uv.x = a / pi2;
                uv.y = d;
                c = texture2D(iChannel0, uv);
            }

            fragColor = c;
        }



    }
}
