/*

Copyright 2015 Valtteri "tsone" Heikkilä

This work is licensed under the Creative Commons Attribution 4.0 International License.
To view a copy of this license, visit http://creativecommons.org/licenses/by/4.0/

*/

#define PI			3.141592653589793
#define SQRT2		1.414213562373095
#define INVSQRT2	0.707106781186548

// Set 1 to use power instead of flux for the light.
#define VARY_LIGHT_INTENSITY 0
// Set 1 to detect HDR range overflow.
#define HDR_DETECTOR 0
// HDR range maximum. (0 is minimum.)
#define HDR_MAX 18.0


struct Material
{
    float roughness;
    float tailamount;
    float tailtheta;
    float F0;
    vec3 basecolor;
  	vec3 specularcolor;
};

struct RectLight
{
    vec3 p;
    mat3 m;
    vec2 s;
    float flux;
};

    
Material mat = Material(
    0.160, // Roughness.
    0.500, // Tail amount.
    PI/2.0/3.0, // Specular cone tail theta angle.
    0.02, // Schlick Fresnel coefficient for zero viewing angle.
    vec3(1.00), // Base color.
   	vec3(0.45) // Specular color.
);

RectLight light = RectLight(
 	vec3(0.0, 6.0, -15.0), // Light position (center).
 	mat3( // Light basis.
    	1.0, 0.0, 0.0,
    	0.0, 1.0, 0.0,
    	0.0, 0.0, 1.0
	),
 	vec2(15.0, 0.6), // Light size.
    18.00 // Light flux.
);


float sqr(float x) { return x*x; }

float Line2Plane(in vec3 P, in vec3 D, in vec3 planeP, in vec3 planeN)
{
    return dot(planeN, planeP - P) / dot(planeN, D);
}

// Approximate irradiance "weight" for a rectangular emitter.
// Uses distance function to rectangle and a Gaussian standard
// normal distribution (variance=0.5) to approximate the irradiance
// from the "cone". Result is weight in (0,1] in units 1/(m^2*sr).
// Parameter theta is the cone angle at 1x variance.
// NOTE: Intersection of cone and plane is a conic, but here
// distance to sphere is used. It's not accurate, particularly on
// glazing angles.
float RectLight_calcWeight(in vec3 P, in vec3 R, in RectLight light, float theta)
{
    float d = Line2Plane(P, R, light.p, light.m[2]);
    if (d < 0.0) {
    	return 0.0;
    }
    // PlC: Point on plane.
    vec3 PlC = P + d*R - light.p;
    // uvPl: UV coordinate on plane.
    vec2 PlUV = vec2(dot(PlC, light.m[0]), dot(PlC, light.m[1]));
    // r: Radius of cone at distance d.
    float r = d * tan(theta);
    // s: Rect size shifted by radius. This for weigth 1 inside the rect.
    vec2 s = max(light.s - 0.5*r, 0.0);
    // h: Distance from rect on plane.
    float h = length(max(abs(PlUV) - s, 0.0));
    // sr: Steradians from the sphere cap equation: sr = 2pi * (1-cos(a))
    float sr = 2.0*PI * (1.0 - cos(theta));
    return exp(-sqr(h / r)) / (1.0 + sqr(d)*sr);
}

vec3 RectLight_BRDF(in RectLight light, in Material material, in vec3 P, in vec3 N, in vec3 R, float NoR)
{
    // Schlick Fresnel.
    float Fr = material.F0 + (1.0-material.F0) * pow(1.0 - NoR, 5.0);
    
    // Approximate specular/glossy.
    float theta = mix(PI*0.003, PI/2.0/3.0, material.roughness);
    float Cs = RectLight_calcWeight(P, R, light, theta) * NoR;
    // Specular glossy tail. Using other than Gaussian could help. 
    float Ct = RectLight_calcWeight(P, R, light, material.tailtheta);
    
    // Crude hack for diffuse.
    // Average normal and inversed emitter direction to create
    // a vector W that points towards the light.
    vec3 W = normalize(N - light.m[2]);
    float Cd = RectLight_calcWeight(P, W, light, PI*INVSQRT2/3.0) * max(dot(N, W), 0.0);
	
    return light.flux * mix(
        (Cd) * material.basecolor,
        (mix(Cs, Ct, material.tailamount) * NoR) * material.specularcolor,
        Fr);
}

vec3 ToneMap(in vec3 c, float maxc)
{
#if HDR_DETECTOR
    if (c.r > maxc || c.g > maxc || c.b > maxc) {
    	return vec3(1.0, 0.0, 0.0);
    } else if (c.r < 0.0 || c.g < 0.0 || c.b < 0.0) {
    	return vec3(0.0, 1.0, 0.0);
    }
#endif
   	float v = max(c.r, max(c.g, c.b));
    return c / (1.0 + v / maxc);
}

vec3 Tex3D(in vec3 P, in vec3 N, float s)
{
    P *= s;
    mat3 tc = mat3(
        texture2D(iChannel0, P.yz).rgb,
        texture2D(iChannel0, P.xz).rgb,
        texture2D(iChannel0, P.xy).rgb
    );
    N = pow(abs(N), vec3(16.0));
    N = N / (N.x+N.y+N.z);
    return pow(tc*N, vec3(2.2)); // Gamma decode.
}

vec2 Union(in vec2 a, in vec2 b)
{
    return (a.x < b.x) ? a : b;
}

vec2 Map(in vec3 P)
{
    vec2 d = vec2(-abs(P.y-light.p.y) + light.p.y, 0.16);
    d.x = min(d.x, -abs(P.x) - light.p.z);
    d.x = min(d.x, -abs(P.z) - light.p.z);
    for (float i = -3.0; i <= 3.0; ++i) {
	    for (float j = -1.0; j <= 2.0; ++j) {
    	  	d = Union(d, vec2(
                length(P+4.0*vec3(i, -0.8, j+1.25))-1.2,
                (i+3.0)/6.0)
            );
    	}
    }
    return d;
}

vec3 Gradient(in vec3 P)
{
    const vec3 d = vec3(0.05, 0.0, 0.0);
    return vec3(
        Map(P + d.xyy).x - Map(P - d.xyy).x,
        Map(P + d.yxy).x - Map(P - d.yxy).x,
        Map(P + d.yyx).x - Map(P - d.yyx).x
    );
}

vec2 March(in vec3 P, in vec3 D)
{
    float t = 0.01;
    float m = 0.0;
    for (int i = 0; i < 64; ++i) {
        vec2 d = Map(P + D*t);
        if (d.x <= 0.008) {
            break;
        }
        t += d.x + 0.004;
        m = d.y;
    }
    return vec2(t, m);
}

mat3 LookAt(in vec3 P, in vec3 focusP)
{
    vec3 up = vec3(0.0, 1.0, 0.0);
    vec3 dir = normalize(P - focusP);
    vec3 left = cross(up, dir);
    up = cross(dir, left);
    return mat3(left, up, dir);
}

void mainImage(out vec4 fragColor, in vec2 fragCoord)
{
	vec2 uv = (2.0*fragCoord.xy - iResolution.xy) / iResolution.xx;
    float t = iGlobalTime;
    
    // Modify light.
    light.s.x = 0.1 + 14.6*(0.5+0.5*sin(t));
    light.s.y = 0.1 + 5.6*(0.5+0.5*cos(t));
    
#if VARY_LIGHT_INTENSITY
    // A bit hacky. Instead assume flux is power, so adjust by light area.
    light.flux = light.flux * light.s.x*light.s.y / (13.0*4.0);
#endif

    // Camera position and basis.
    vec2 a = vec2(0.25*t, 0.0);
    if (iMouse.z > 0.0) {
    	a = (2.0*iMouse.xy/iResolution.xy - 1.0);
    	a.x *= 0.5*PI;
    }
    vec3 camP = vec3(14.0*sin(a.x), light.p.y+light.p.y*a.y, -14.0+24.0*sqr(cos(a.x)));
    mat3 camM = LookAt(camP, vec3(0.0,light.p.y,light.p.z+1.0));
    
    // Cast ray and vignette.
    vec3 D = INVSQRT2*(uv.x*camM[0] + uv.y*camM[1]) - camM[2];
    vec3 P = camP + D;
    D = normalize(D);
    float vignette = 0.77 + 0.23*sqr(D.z);

    // March for surface point.
    vec2 res = March(P, D);
    P = P + res.x * D;
    
    // Normal and reflection vectors.
    vec3 N = normalize(Gradient(P));
    float NoR = -dot(N, D);
    vec3 R = D + (2.0*NoR)*N;
    NoR = max(NoR, 0.0);
    
    // Modify material.
    vec3 texColor = Tex3D(P, N, 0.35);
    float maxv = max(texColor.r, max(texColor.g, texColor.b));
    mat.basecolor = min(0.33+maxv, 1.0) * vec3(1.0, 0.56, 0.33);
    //mat.roughness = res.y;
    
    vec3 C = vec3(0.0);
    if (P.z <= light.p.z+0.15) {
        if (abs(P.y-light.p.y) < light.s.y && abs(P.x-light.p.x) < light.s.x) {
    		C = vec3(HDR_MAX);
        } else {
            C = vec3(0.0);
        }
    } else {
    	C = RectLight_BRDF(light, mat, P, N, R, NoR);
    }
    
    C = C * vignette; // Vignette.
    C = ToneMap(C, HDR_MAX); // Better tone map.
    C = pow(C, vec3(1.0/2.2)); // Gamma encode.
	fragColor = vec4(C, 1.0);
}
