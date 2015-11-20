using ScriptCoreLib.GLSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeToEquirectangular.Shaders
{


    class __ProgramFragmentShader : FragmentShader
    {
        // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\synergy\r\RoomScanningEffectByRosme\Shaders\ProgramFragmentShader.cs
        // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\360\x360chameleon\Shaders\ProgramFragmentShader.cs
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150906/roomscanningeffectbyrosme
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
        samplerCube iChannel0;



        void mainImage(out vec4 fragColor, vec2 fragCoord)
        {
            vec2 texCoord = fragCoord.xy / iResolution.xy;
            vec2 thetaphi = ((texCoord * 2.0f) - vec2(1.0f)) * vec2(3.1415926535897932384626433832795f, 1.5707963267948966192313216916398f);
            vec3 rayDirection = vec3(cos(thetaphi.y) * cos(thetaphi.x), sin(thetaphi.y), cos(thetaphi.y) * sin(thetaphi.x));
            fragColor = textureCube(iChannel0, rayDirection);

        }
    }
}
