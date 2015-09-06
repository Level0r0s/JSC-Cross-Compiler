using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace RoomScanningEffectByRosme.Shaders
{
    public abstract class __ProgramFragmentUniforms : FragmentShader
    {
        // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\synergy\r\RoomScanningEffectByRosme\Shaders\ProgramFragmentShader.cs
        // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\360\x360dual\Shaders\ProgramFragmentShader.cs
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150906/roomscanningeffectbyrosme
        // https://www.shadertoy.com/view/XsBSDR#

        // all fields are inferred to be uniform?

        // what about inherited fields/ or interface fields?

        #region generic
        [uniform]
        public vec3 iResolution;           // viewport resolution (in pixels)
        [uniform]
        public float iGlobalTime;           // shader playback time (in seconds)
        //[uniform]
        //float iChannelTime[4];       // channel playback time (in seconds)
        //[uniform]
        //vec3 iChannelResolution[4]; // channel resolution (in pixels)
        [uniform]
        public vec4 iMouse;                // mouse pixel coords. xy: current (if MLB down), zw: click

        [uniform]
        public vec4 iDate;                 // (year, month, day, time in seconds)
        [uniform]
        public float iSampleRate;           // sound sample rate (i.e., 44100)
        #endregion


        // https://github.com/tparisi/3dsMaxWebGL/blob/master/exporter/webgl/webgl2.cpp

        //[uniform]
        //samplerXX iChannel0..3;          // input channel. XX = 2D/Cube


        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150808/equirectangular
        // set by?
        [uniform]
        public samplerCube iChannel0;













        [uniform]
        public vec3 uCameraTargetOffset;

        //vec3 uCameraTargetOffset = vec3(0.0f, 0.0f, -1.0f);
    }

    // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\360\x360roomscan\Application.cs
    public class __ProgramFragmentShader : __ProgramFragmentUniforms

    // does jsc know not to process those members?

    //public class __ProgramFragmentShader : ProgramFragmentShader
    {



        // Based on shaders:
        // Template - 3D 			https://www.shadertoy.com/view/ldfSWs
        // Xor - Triangle Grid, 	https://www.shadertoy.com/view/4tSGWz

        //#define pi 3.14159265358979
        //#define size 0.5
        //#define reciproce_sqrt3 0.57735026918962576450914878050196
        //#define lineThickness 0.01

        const float pi = 3.14159265358979f;
        const float size = 0.5f;
        const float reciproce_sqrt3 = 0.57735026918962576450914878050196f;
        const float lineThickness = 0.01f;

        float planeDistance = 0.2f;
        float offset;

        //------------------------------------------------------------------------
        // Camera
        //
        // Move the camera. In this case it's using time and the mouse position
        // to orbitate the camera around the origin of the world (0,0,0), where
        // the yellow sphere is.
        //------------------------------------------------------------------------
        void doCamera(out vec3 camPos, out vec3 camTar, [In] float time, [In] vec2 mouse)
        {
            //float an = 0.0*iGlobalTime + se.x;
            //camPos = vec3(0.0, 2.0, 5.0);
            camPos = vec3(3.5f * sin(mouse.x * 10.0f), 1.0f, 5.0f * cos(mouse.x * 10.0f));

            camTar = vec3(0.0f, 0.0f, 0.0f);
        }


        //------------------------------------------------------------------------
        // Background 
        //
        // The background color. In this case it's just a black color.
        //------------------------------------------------------------------------
        vec3 doBackground()
        {
            return vec3(0.0f, 0.0f, 0.1f);
        }


        float sdPlane(vec3 p, vec4 n)
        {
            // n must be normalized
            return dot(p, n.xyz) + n.w;
        }


        float udRoundBox(vec3 p, vec3 b, float r)
        {
            return length(max(abs(p) - b, 0.0f)) - r;
        }

        //------------------------------------------------------------------------
        // Modelling 
        //
        // Defines the shapes (a sphere in this case) through a distance field, in
        // this case it's a sphere of radius 1.
        //------------------------------------------------------------------------
        float doModel(vec3 p)
        {

            return min(udRoundBox(p - vec3(1.05f, -0.5f, 0.4f), vec3(0.05f, 0.4f, 0.5f), 0.1f),
                    min(udRoundBox(p - vec3(-1.05f, -0.5f, 0.4f), vec3(0.05f, 0.4f, 0.5f), 0.1f),
                    min(udRoundBox(p, vec3(0.8f, 0.3f, 0.1f), 0.1f),
                    min(udRoundBox(p - vec3(0.0f, -0.5f, 0.6f), vec3(0.8f, 0.1f, 0.3f), 0.1f),
                    min(sdPlane(p, vec4(0.0f, 1.0f, 0.0f, 1.0f)), sdPlane(p, vec4(0.0f, 0.0f, 1.0f, 2.0f)))))));
        }

        float r(float n)
        {
            return fract(abs(sin(n * 55.753f) * 367.34f));
        }

        float r(vec2 n)
        {
            return r(dot(n, vec2(2.46f, -1.21f)));
        }

        vec3 smallTrianglesColor(vec3 pos)
        {
            float a = (radians(60.0f));
            float zoom = 0.5f;
            vec2 c = (pos.xy + vec2(0.0f, pos.z)) * vec2(sin(a), 1.0f);//scaled coordinates
            c = ((c + vec2(c.y, 0.0f) * cos(a)) / zoom) + vec2(floor((c.x - c.y * cos(a)) / zoom * 4.0f) / 4.0f, 0.0f);//Add rotations
            float type = (r(floor(c * 4.0f)) * 0.2f + r(floor(c * 2.0f)) * 0.3f + r(floor(c)) * 0.5f);//Randomize type
            type += 0.2f * sin(iGlobalTime * 5.0f * type);

            float l = min(min((1.0f - (2.0f * abs(fract((c.x - c.y) * 4.0f) - 0.5f))),
                          (1.0f - (2.0f * abs(fract(c.y * 4.0f) - 0.5f)))),
                          (1.0f - (2.0f * abs(fract(c.x * 4.0f) - 0.5f))));
            l = smoothstep(0.06f, 0.04f, l);

            return mix(type, l, 0.5f) * vec3(0.2f, 0.5f, 1);
        }

        vec3 largeTrianglesColor(vec3 pos)
        {
            float a = (radians(60.0f));
            float zoom = 2.0f;
            vec2 c = (pos.xy + vec2(0.0f, pos.z)) * vec2(sin(a), 1.0f);//scaled coordinates
            c = ((c + vec2(c.y, 0.0f) * cos(a)) / zoom) + vec2(floor((c.x - c.y * cos(a)) / zoom * 4.0f) / 4.0f, 0.0f);//Add rotations

            float l = min(min((1.0f - (2.0f * abs(fract((c.x - c.y) * 4.0f) - 0.5f))),
                          (1.0f - (2.0f * abs(fract(c.y * 4.0f) - 0.5f)))),
                          (1.0f - (2.0f * abs(fract(c.x * 4.0f) - 0.5f))));
            l = smoothstep(0.03f, 0.02f, l);

            return mix(0.01f, l, 0.5f) * vec3(0.2f, 0.5f, 1);
        }

        vec3 gridColor(vec3 pos)
        {
            float plane5 = abs(sdPlane(pos, vec4(1.0f, 0.0f, 0.0f, 0)));
            float plane6 = abs(sdPlane(pos, vec4(0.0f, 1.0f, 0.0f, 0)));
            float plane7 = abs(sdPlane(pos, vec4(0.0f, 0.0f, 1.0f, 0)));

            float nearest = abs(mod(plane5, planeDistance) - 0.5f * planeDistance);
            nearest = min(nearest, abs(mod(plane6, planeDistance) - 0.5f * planeDistance));
            nearest = min(nearest, abs(mod(plane7, planeDistance) - 0.5f * planeDistance));

            return mix(vec3(0.3f, 0.3f, 0.5f), vec3(0.2f), smoothstep(0.0f, lineThickness, nearest));
        }


        //---------------------------------------------------------------
        // Material 
        //
        // Defines the material (colors, shading, pattern, texturing) of the model
        // at every point based on its position and normal. In this case, it simply
        // returns a constant yellow color.
        //------------------------------------------------------------------------
        vec3 doMaterial([In] vec3 pos, [In] vec3 nor)
        {
            float d = length(pos.xz - vec2(0.0f, 2.0f) + 0.5f * cos(2.0f * pos.xz + vec2(3.0f, 1.0f) * iGlobalTime)) + pos.y + 0.2f * cos(pos.y - iGlobalTime);
            float border = 12.0f * mod(iGlobalTime * 0.2f, 1.0f);

            //vec3 c = gridColor(pos);
            vec3 c1 = largeTrianglesColor(pos);
            vec3 c = smallTrianglesColor(pos);
            c *= smoothstep(border - 1.0f, border - 2.5f, d);
            c += c1;
            c = mix(c, vec3(0.01f), smoothstep(border - 4.0f, border - 10.0f, d));
            c = mix(c, vec3(0.01f), smoothstep(border - 1.0f, border, d));
            c = mix(c, vec3(0.01f), smoothstep(9.0f, 12.0f, border));

            return c;
        }
        //------------------------------------------------------------------------
        // Lighting 
        //------------------------------------------------------------------------
        //partial float calcSoftshadow([In]  vec3 ro, [In]  vec3 rd);

        vec3 doLighting([In]  vec3 pos, [In]  vec3 nor, [In]  vec3 rd, [In]  float dis, [In]  vec3 mal)
        {
            vec3 lin = vec3(0.0f);

            // key light
            //-----------------------------
            vec3 lig = normalize(vec3(1.0f, 0.7f, 0.9f));
            float dif = max(dot(nor, lig), 0.0f);
            float sha = 0.0f; if (dif > 0.01) sha = calcSoftshadow(pos + 0.01f * nor, lig);
            lin += dif * vec3(4.00f, 4.00f, 4.00f) * sha;

            // ambient light
            //-----------------------------
            lin += vec3(0.50f, 0.50f, 0.50f);


            // surface-light interacion
            //-----------------------------
            vec3 col = mal * lin;


            // fog    
            //-----------------------------
            col *= exp(-0.01f * dis * dis);

            return col;
        }

        float calcIntersection([In]  vec3 ro, [In]  vec3 rd)
        {
            const float maxd = 20.0f;           // max trace distance
            const float precis = 0.001f;        // precission of the intersection
            float h = precis * 2.0f;
            float t = 0.0f;
            float res = -1.0f;
            for (int i = 0; i < 90; i++)          // max number of raymarching iterations is 90
            {
                if (h < precis || t > maxd) break;
                h = doModel(ro + rd * t);
                t += h;
            }

            if (t < maxd) res = t;
            return res;
        }

        vec3 calcNormal([In]  vec3 pos)
        {
            const float eps = 0.002f;             // precision of the normal computation

            vec3 v1 = vec3(1.0f, -1.0f, -1.0f);
            vec3 v2 = vec3(-1.0f, -1.0f, 1.0f);
            vec3 v3 = vec3(-1.0f, 1.0f, -1.0f);
            vec3 v4 = vec3(1.0f, 1.0f, 1.0f);

            return normalize(v1 * doModel(pos + v1 * eps) +
                              v2 * doModel(pos + v2 * eps) +
                              v3 * doModel(pos + v3 * eps) +
                              v4 * doModel(pos + v4 * eps));
        }

        float calcSoftshadow([In]  vec3 ro, [In]  vec3 rd)
        {
            float res = 1.0f;
            float t = 0.0005f;                 // selfintersection avoidance distance
            float h = 1.0f;
            for (int i = 0; i < 40; i++)         // 40 is the max numnber of raymarching steps
            {
                h = doModel(ro + rd * t);
                res = min(res, 64.0f * h / t);   // 64 is the hardness of the shadows
                t += clamp(h, 0.02f, 2.0f);   // limit the max and min stepping distances
            }
            return clamp(res, 0.0f, 1.0f);
        }

        mat3 calcLookAtMatrix([In] vec3 ro, [In] vec3 ta, [In] float roll)
        {
            vec3 ww = normalize(ta - ro);
            vec3 uu = normalize(cross(ww, vec3(sin(roll), cos(roll), 0.0f)));
            vec3 vv = normalize(cross(uu, ww));
            return mat3(uu, vv, ww);
        }

        void mainImage(out vec4 fragColor, [In] vec2 fragCoord)
        {
            //planeDistance = sin(iGlobalTime);
            offset = 2.0f * sqrt(2.0f) / sqrt(24.0f);
            vec2 p = (-iResolution.xy + 2.0f * fragCoord.xy) / iResolution.y;
            vec2 m = iMouse.xy / iResolution.xy;

            //-----------------------------------------------------
            // camera
            //-----------------------------------------------------

            // camera movement
            vec3 ro, ta;
            doCamera(out ro, out ta, iGlobalTime, m);

            // camera matrix
            mat3 camMat = calcLookAtMatrix(ro, ta, 0.0f);  // 0.0 is the camera roll

            // create view ray
            vec3 rd = normalize(camMat * vec3(p.xy, 2.0f)); // 2.0 is the lens length

            //-----------------------------------------------------
            // render
            //-----------------------------------------------------

            vec3 col = doBackground();

            // raymarch
            float t = calcIntersection(ro, rd);
            if (t > -0.5)
            {
                // geometry
                vec3 pos = ro + t * rd;
                vec3 nor = calcNormal(pos);

                // materials
                vec3 mal = doMaterial(pos, nor);

                col = doLighting(pos, nor, rd, t, mal);
            }

            //-----------------------------------------------------
            // postprocessing
            //-----------------------------------------------------
            // gamma
            col = pow(clamp(col, 0.0f, 1.0f), vec3(0.5f));

            fragColor = vec4(col, 1.0f);
        }

    }
}
