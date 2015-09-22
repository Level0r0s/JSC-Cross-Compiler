// Created by David Bargo - davidbargo/2015
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

// Comment to generate a static but faster image
#define ANIMATE

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 q = fragCoord.xy/iResolution.yy;
    q.x += 2.0;
    vec2 p = 10.*q*mat2(0.7071, -0.7071, 0.7071, 0.7071);
    
    vec2 pi = floor(p);    
    vec4 v = vec4( pi.xy, pi.xy + 1.0);
    v -= 64.*floor(v*0.015);
    v.xz = v.xz*1.435 + 34.423;
    v.yw = v.yw*2.349 + 183.37;
    v = v.xzxz*v.yyww;
    v *= v;
    
#ifdef ANIMATE
    v *= iGlobalTime*0.000004 + 0.5;
    vec4 vx = 0.25*sin(fract(v*0.00047)*6.2831853);
    vec4 vy = 0.25*sin(fract(v*0.00074)*6.2831853);
#else
    vec4 vx = 0.5*(step(0.0,fract(v*0.00047)-0.5)-0.5);
    vec4 vy = 0.5*(step(0.0,fract(v*0.00074)-0.5)-0.5);
#endif
    
    vec2 pf = p - pi;
    vx += vec4(0., 1., 0., 1.) - pf.xxxx;
    vy += vec4(0., 0., 1., 1.) - pf.yyyy;
    v = vx*vx + vy*vy;
    
    v.xy = min(v.xy, v.zw);
    vec3 col = mix(vec3(0.0,0.4,0.9), vec3(0.0,0.95,0.9), min(v.x, v.y) );     
	fragColor = vec4(col, 1.0);
}