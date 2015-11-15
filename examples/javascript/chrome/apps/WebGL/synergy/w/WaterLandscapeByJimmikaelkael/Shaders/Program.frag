// many thanks to IQ for his useful articles !

vec3 sunColor = vec3(1.8, 1.1, 0.6);
vec3 skyColor = vec3(0.4, 0.6, 0.85);
vec3 sunLightColor = vec3(1.5, 1.25, 0.9);
vec3 skyLightColor = vec3(0.15, 0.2, 0.3);
vec3 indLightColor = vec3(0.4, 0.3, 0.2);
vec3 cloudsColor = vec3(1.0, 1.0, 1.0);
vec3 horizonColor = vec3(0.7, 0.75, 0.8);
vec3 fogColorB = vec3(0.7, 0.8, 0.9);
vec3 fogColorR = vec3(0.8, 0.7, 0.6);

vec3 sunDirection = normalize(vec3(0.6, 0.8, 0.5));

float cloudsHeight = 800.0;
float cloudsDensity = 0.3;
float cloudsCover = 0.2;


// --------------------- START of SIMPLEX NOISE
//
// Description : Array and textureless GLSL 2D simplex noise function.
//      Author : Ian McEwan, Ashima Arts.
//  Maintainer : ijm
//     Lastmod : 20110822 (ijm)
//     License : Copyright (C) 2011 Ashima Arts. All rights reserved.
//               Distributed under the MIT License. See LICENSE file.
//               https://github.com/ashima/webgl-noise
// 

vec3 mod289(vec3 x) {
  return x - floor(x * (1.0 / 289.0)) * 289.0;
}

vec2 mod289(vec2 x) {
  return x - floor(x * (1.0 / 289.0)) * 289.0;
}

vec3 permute(vec3 x) {
  return mod289(((x*34.0)+1.0)*x);
}

float snoise(vec2 v)
  {
  const vec4 C = vec4(0.211324865405187,  // (3.0-sqrt(3.0))/6.0
                      0.366025403784439,  // 0.5*(sqrt(3.0)-1.0)
                     -0.577350269189626,  // -1.0 + 2.0 * C.x
                      0.024390243902439); // 1.0 / 41.0
// First corner
  vec2 i  = floor(v + dot(v, C.yy) );
  vec2 x0 = v -   i + dot(i, C.xx);

// Other corners
  vec2 i1;
  //i1.x = step( x0.y, x0.x ); // x0.x > x0.y ? 1.0 : 0.0
  //i1.y = 1.0 - i1.x;
  i1 = (x0.x > x0.y) ? vec2(1.0, 0.0) : vec2(0.0, 1.0);
  // x0 = x0 - 0.0 + 0.0 * C.xx ;
  // x1 = x0 - i1 + 1.0 * C.xx ;
  // x2 = x0 - 1.0 + 2.0 * C.xx ;
  vec4 x12 = x0.xyxy + C.xxzz;
  x12.xy -= i1;

// Permutations
  i = mod289(i); // Avoid truncation effects in permutation
  vec3 p = permute( permute( i.y + vec3(0.0, i1.y, 1.0 ))
		+ i.x + vec3(0.0, i1.x, 1.0 ));

  vec3 m = max(0.5 - vec3(dot(x0,x0), dot(x12.xy,x12.xy), dot(x12.zw,x12.zw)), 0.0);
  m = m*m ;
  m = m*m ;

// Gradients: 41 points uniformly over a line, mapped onto a diamond.
// The ring size 17*17 = 289 is close to a multiple of 41 (41*7 = 287)

  vec3 x = 2.0 * fract(p * C.www) - 1.0;
  vec3 h = abs(x) - 0.5;
  vec3 ox = floor(x + 0.5);
  vec3 a0 = x - ox;

// Normalise gradients implicitly by scaling m
// Approximation of: m *= inversesqrt( a0*a0 + h*h );
  m *= 1.79284291400159 - 0.85373472095314 * ( a0*a0 + h*h );

// Compute final noise value at P
  vec3 g;
  g.x  = a0.x  * x0.x  + h.x  * x0.y;
  g.yz = a0.yz * x12.xz + h.yz * x12.yw;
  return 130.0 * dot(m, g);
}

// --------------------- END of SIMPLEX NOISE


float noiseT(in vec2 p) {
    return snoise( p*0.15 ) * 2.0 - 1.0;
}

float noiseW(in vec2 p) {
    return snoise(  p*0.15);
}

float fBm(in vec2 p) {
    float sum = 0.0;
    float amp = 1.0;
    for(int i = 0; i < 4; i++) {
        sum += amp * noiseT(p);
        amp *= 0.5;
        p *= 2.0;
    }
    return sum;
}

float fBmC(in vec2 p) {
    float sum = 0.0;
    float amp = 1.0;
    for(int i = 0; i < 5; i++) {
        sum += amp * noiseT(p);
        amp *= 0.5;
        p *= 2.0;
    }
    return sum;
}

float fBmW(in vec2 p) {
    float sum = 0.0;
    float amp = 0.5;
    for(int i = 0; i < 5; i++) {
        sum += amp * noiseT(p);
        amp *= 0.5;
        p *= 2.0;
    }
    return sum * 0.2;
}

float raymarchTerrain(in vec3 ro, in vec3 rd, in float tmin, in float tmax) {
    float t = tmin;
    for (int i = 0; i < 300; i++) {
        vec3 p = ro + rd * t;
        float d = p.y - fBm(p.xz);
        if (d < (0.001 * t) || t > tmax)
            break;
        t += 0.2 * d;
    }
    return t;
}

vec3 getTerrainNormal(in vec3 p, float t) {
    float eps = 0.025;
    return normalize(vec3(fBm(vec2(p.x - eps, p.z)) - fBm(vec2(p.x + eps, p.z)),
                          2.0 * eps,
                          fBm(vec2(p.x, p.z - eps)) - fBm(vec2(p.x, p.z + eps))));
}

vec3 getWaterNormal(in vec3 p, float t) {
    float eps = 0.025;
    return normalize(vec3(fBmW(vec2(p.x - eps, p.z)) - fBmW(vec2(p.x + eps, p.z)),
                          2.0 * eps,
                          fBmW(vec2(p.x, p.z - eps)) - fBmW(vec2(p.x, p.z + eps))));
}

float raymarchAO(in vec3 ro, in vec3 rd, float tmin) {
    float ao = 0.0;
    for (float i = 0.0; i < 5.0; i++) {
        float t = tmin + pow(i / 5.0, 2.0);
        vec3 p = ro + rd * t;
        float d = p.y - fBm(p.xz);
        ao += max(0.0, t - 0.5 * d - 0.05);
    }
    return 1.0 - 0.4 * ao;
}

float raymarchWaterAO(in vec3 ro, in vec3 rd, float tmin) {
    float ao = 0.0;
    for (float i = 0.0; i < 5.0; i++) {
        float t = tmin + pow(i / 5.0, 2.0);
        vec3 p = ro + rd * t;
        float d = p.y - fBmW(p.xz);
        ao += max(0.0, t - 0.5 * d - 0.05);
    }
    return 1.0 - 0.4 * ao;
}

float raymarchShadow(in vec3 ro, in vec3 rd, float tmin, float tmax) {
    float sh = 1.0;
    float t = tmin;
    for(int i = 0; i < 50; i++) {
        vec3 p = ro + rd * t;
        float d = p.y - fBm(p.xz);
        sh = min(sh, 16.0 * d / t);
        t += 0.5 * d;
        if (d < (0.001 * t) || t > tmax)
            break;
    }
    return sh;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord ) {

    vec2 p = fragCoord.xy / iResolution.xy * 2.0 - 1.0;

    vec3 eye = vec3(0.0, 2.0, 1.0);
    vec2 rot = 6.2831 * (vec2(0.0, 0.25) + vec2(1.0, 0.25) * (iMouse.xy - iResolution.xy * 0.5) / iResolution.x);
    eye.yz = cos(rot.y) * eye.yz + sin(rot.y) * eye.zy * vec2(-1.0, 1.0);
    eye.xz = cos(rot.x) * eye.xz + sin(rot.x) * eye.zx * vec2(1.0, -1.0);

    vec3 ro = eye + vec3(cos(iGlobalTime * 0.1) * 1.25, sin(iGlobalTime * 0.1) * 0.5 + 0.15, iGlobalTime * 0.5);
    ro.y += 2.5;
    vec3 ta = vec3(0.0, -0.75, 0.0);

    // build camera matrix
    vec3 cw = normalize(ta - eye);
    vec3 cu = normalize(cross(vec3(0.0, 1.0, 0.0), cw));
    vec3 cv = normalize(cross(cw, cu));
    mat3 cam = mat3(cu, cv, cw);

    // compute ray direction
    vec3 rd = cam * normalize(vec3(p.xy, 1.0));

    // the powerful sun dot
    float sunDot = clamp(dot(sunDirection, rd), 0.0, 1.0);

    // terrain marching
    float tmin = 0.1;
    float tmax = 50.0;
    float t = raymarchTerrain(ro, rd, tmin, tmax);
    vec3 color = vec3(0.0);
    if (t < tmax) {
        vec3 tpos = ro + rd * t;

        if (tpos.y < 0.15 - sin(iGlobalTime) * 0.005) {
            tpos += -rd * (tpos.y / rd.y);
            tpos += fBmW(tpos.xz + iGlobalTime * 0.15) - fBmW(tpos.xz + 32.0 - iGlobalTime * 0.15);
            vec3 tnorm = getWaterNormal(tpos, t);
            vec3 ref = reflect(rd, tnorm);
            vec2 p = tpos.xz + vec2(ref.x + ref.z, ref.y - ref.z);
            float surface = noiseW(p * 0.5) * noiseW(p) * noiseW(p * 4.0) * noiseW(p * 5.0) * noiseW(p * 6.0) * noiseW(p * 8.0);
            // light from skydome
            float sky = clamp(pow(0.5 + 0.5 * tnorm.y, 100.0), 0.0, 1.0);
            // water AO
            float occ = clamp(raymarchWaterAO(tpos, tnorm, 0.25), 0.0, 1.0);
            // terrain shadows on water
            float sha = 0.25 * clamp(raymarchShadow(tpos, sunDirection, 0.5, 50.0), 0.0, 1.0);
            // amount of sun reflecting
            float sun = 10.0 * sha * clamp(pow(dot(sunDirection, ref), 1000.0), 0.0, 1.0);

            // water
            color = vec3(sky * 0.01 + sun * 0.7 + 0.02 + surface, sky * 0.02 + sun * 0.6 + 0.05 + surface, sky * 0.05 + sun * 0.4 + 0.035 + surface) * occ;
            color = mix(color, pow(vec3(sha), vec3(1.0, 0.8, 0.8)), 0.05);
            // foam
            float foam = fBmC(32.0 * tpos.xz) * 0.4 + 0.2;
            color = mix(color, 0.75 * vec3(1.0), 0.5 * smoothstep(0.66 + cos(iGlobalTime) * 0.01, 1.0, foam));
            foam = fBmC(16.0 * tpos.xz) * 0.3 + 0.2;
            color = mix(color, 0.75 * vec3(1.0), 0.5 * smoothstep(0.43 + cos(iGlobalTime) * 0.01, 1.0, foam));

        } else {
            vec3 tnorm = getTerrainNormal(tpos, t);

            // light from sun direction
            float sun = clamp(dot(sunDirection, tnorm), 0.0, 1.0);
            // light from skydome
            float sky = clamp(0.5 + 0.5 * tnorm.y, 0.0, 1.0);
            // indirect light reflected back in opposite direction of the sun
            float ind = clamp(dot(vec3(-sunDirection.x, 0.0, -sunDirection.z), tnorm), 0.0, 1.0);
            // raymarching AO
            float occ = clamp(raymarchAO(tpos, tnorm, 0.1), 0.0, 1.0);
            // raymarching penumbra shadows
            float sha = clamp(raymarchShadow(tpos, sunDirection, 0.5, 50.0), 0.0, 1.0);
            // light color
            vec3 lightColor = 1.2 * sun * sunLightColor;
            lightColor *= pow(vec3(sha), vec3(1.0, 1.2, 1.5));
            lightColor += 0.7 * sky * skyLightColor * occ;
            lightColor += 0.3 * ind * indLightColor * occ;
            
            // rock
            color = texture2D(iChannel0, tpos.xz * 0.7).xyz;
            color = mix(0.2 * vec3(0.25, 0.2, 0.15), color, 0.2); // gray with dots from noise
            color = mix(0.15 * vec3(0.25, 0.2, 0.15), color, 2.0 * texture2D(iChannel0, 0.002 * vec2(tpos.x + noiseT(tpos.xz) * 80.0, tpos.y * 80.0 + noiseT(tpos.xz) * 8.0)).x); // stripes
            color = mix(0.15 * vec3(0.15, 0.2, 0.05), color, (tpos.y + 0.0));
            color *= 1.0 * lightColor;
        }

        // fog
        vec3 fogColor = mix(fogColorB, fogColorR, pow(sunDot, 4.0));
        color = mix(color, 0.8 * fogColor, 1.0 - exp(-0.0005 * t * t));

    } else {
        // sky and sun
        float sky = clamp(0.6 * (1.0 - 0.8 * rd.y), 0.0, 1.0);
        float diffuse = clamp(0.4 * sunDot, 0.0, 1.0);
        color = sky * skyColor + pow(sunDot, 800.0) * sunColor + diffuse * skyLightColor;

        // clouds
        t = (cloudsHeight - ro.y) / rd.y;
        if (t > 0.0) {
            vec3 pos = ro + rd * t;
            pos.z += iGlobalTime * 20.0;
            float clouds = fBmC(0.0025 * pos.xz) * cloudsDensity + cloudsCover;
            color = mix(color, mix(cloudsColor * 1.1, sunColor + diffuse * sunLightColor, 0.25), 0.8 * smoothstep(0.1, 0.9, clouds));
        }

        // horizon
        color = mix(color, horizonColor, pow(1.0 - rd.y, 4.0));
    }

    // gamma correction
    vec3 gamma = vec3(1.0 / 2.2);
    fragColor = vec4(pow(color, gamma), 1.0);
}