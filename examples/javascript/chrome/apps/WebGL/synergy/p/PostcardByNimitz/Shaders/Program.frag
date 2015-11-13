//Postcard by nimitz (twitter: @stormoid)

/*
	Implementation of: http://iquilezles.org/www/articles/dynclouds/dynclouds.htm
	
	Added some raymarched mountains and normal mapped water to complete the scene.

	One thing I did differently is modyfying the scale of the fbm based on the distance
	from the shaded clouds allowing for a much less "planar" look to the cloud layer.  
*/

//Compare with simple clouds
//#define BASIC_CLOUDS

#define time iGlobalTime*2.
#define FAR 420.

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




//------------------------------------------------------------------
//----------------------Utility functions---------------------------
//------------------------------------------------------------------
vec3 rotx(vec3 p, float a){
    float s = sin(a), c = cos(a);
    return vec3(p.x, c*p.y - s*p.z, s*p.y + c*p.z);
}
vec3 roty(vec3 p, float a){
    float s = sin(a), c = cos(a);
    return vec3(c*p.x + s*p.z, p.y, -s*p.x + c*p.z);
}
//from Dave (https://www.shadertoy.com/view/4djSRW)
float hash(vec2 p){
	p  = fract(p * vec2(5.3983, 5.4427));
    p += dot(p.yx, p.xy + vec2(21.5351, 14.3137));
	return fract(p.x * p.y * 95.4337);
}
float noise(in vec2 p) {
    vec2 i = floor( p );
    vec2 f = fract( p );	
	vec2 u = f*f*(3.0-2.0*f);
    return -1.0+2.0*mix( mix( hash( i + vec2(0.0,0.0) ), 
                     hash( i + vec2(1.0,0.0) ), u.x),
                mix( hash( i + vec2(0.0,1.0) ), 
                     hash( i + vec2(1.0,1.0) ), u.x), u.y);
}
//------------------------------------------------------------------
//---------------------------Terrain--------------------------------
//------------------------------------------------------------------
float terrain(in vec2 p)
{
    p*= 0.035;
    float rz = 0.;
    float m = 1.;
    float z = 1.;
    for(int i=0; i<=2; i++) 
    {
        rz += (sin(noise(p/m)*1.7)*0.5+0.5)*z;
        m *= -0.25;
        z *= .2;
    }
    rz=exp2(rz-1.5);
    rz -= sin(p.y*.2+sin(p.x*.45));
    return rz*20.-14.;
}

float tmap(in vec3 p){ return p.y-terrain(p.zx);}
//Using "cheap AA" from eiffie (https://www.shadertoy.com/view/XsSXDt)
vec3 tmarch(in vec3 ro, in vec3 rd, in float d)
{
	float precis = 0.01;
    float h=precis*2.0;
    float hm = 100., dhm = 0.;
    for( int i=0; i<15; i++ )
    {   
        d += h = tmap(ro+rd*d)*1.5;
        if (h < hm)
        {
            hm = h;
            dhm = d;
        }
        if( abs(h)<precis||d>FAR ) break;
    }
	return vec3(d, hm, dhm);
}


vec3 normal( in vec3 pos, float t )
{
	float e = 0.001*t;
    vec2  eps = vec2(e,0.0);
    float h = terrain(pos.xz);
    return normalize(vec3( terrain(pos.xz-eps.xy)-h, e, terrain(pos.xz-eps.yx)-h ));
}

float plane( in vec3 ro, in vec3 rd, vec3 c, vec3 u, vec3 v )
{
	vec3 q = ro - c;
	vec3 n = cross(u,v);
    return -dot(n,q)/dot(rd,n);
}
//------------------------------------------------------------------
//-------------------------2d Clouds--------------------------------
//------------------------------------------------------------------
vec3 lgt = normalize(vec3(-1.0,0.1,.0));
vec3 hor = vec3(0);

float nz(in vec2 p){return 
    
    snoise(p*.3);
  //  texture2D(iChannel0, p*.01).x;
                   
                   }
mat2 m2 = mat2( 0.80,  0.60, -0.60,  0.80 );
float fbm(in vec2 p, in float d)
{	
	d = smoothstep(0.,100.,d);
    p *= .3/(d+0.2);
    float z=2.;
	float rz = 0.;
    p  -= time*0.02;
	for (float i= 1.;i <=5.;i++ )
	{
		rz+= (sin(nz(p)*6.5)*0.5+0.5)*1.25/z;
		z *= 2.1;
		p *= 2.15;
        p += time*0.027;
        p *= m2;
	}
    return pow(abs(rz),2.-d);
}

vec4 clouds(in vec3 ro, in vec3 rd, in bool wtr)
{   
	
    //Base sky coloring is from iq's "Canyon" (https://www.shadertoy.com/view/MdBGzG)
    float sun = clamp(dot(lgt,rd),0.0,1.0 );
    hor = mix( 1.*vec3(0.70,1.0,1.0), vec3(1.3,0.55,0.15), 0.25+0.75*sun );
    vec3 col = mix( vec3(0.5,0.75,1.), hor, exp(-(4.+ 2.*(1.-sun))*max(0.0,rd.y-0.05)) );
    col *= 0.4;
	
    if (!wtr)
    {
        col += 0.8*vec3(1.0,0.8,0.7)*pow(sun,512.0);
        col += 0.2*vec3(1.0,0.4,0.2)*pow(sun,32.0);
    }
    else 
    {
        col += 1.5*vec3(1.0,0.8,0.7)*pow(sun,512.0);
        col += 0.3*vec3(1.0,0.4,0.2)*pow(sun,32.0);
    }
    col += 0.1*vec3(1.0,0.4,0.2)*pow(sun,4.0);
    
	float pt = (90.0-ro.y)/rd.y; 
    vec3 bpos = ro + pt*rd;
    float dist = sqrt(distance(ro,bpos));
    float s2p = distance(bpos,lgt*100.);
    
    const float cls = 0.002;
    float bz = fbm(bpos.xz*cls,dist);
    float tot = bz;
    const float stm = .0;
    const float stx = 1.15;
    tot = smoothstep(stm,stx,tot);
    float ds = 2.;
    for (float i=0.;i<=3.;i++)
    {

        vec3 pp = bpos + ds*lgt;
        float v = fbm(pp.xz*cls,dist);
        v = smoothstep(stm,stx,v);
        tot += v;
        #ifndef BASIC_CLOUDS
        ds *= .14*dist;
        #endif
    }

    col = mix(col,vec3(.5,0.5,0.55)*0.2,pow(bz,1.5));
    tot = smoothstep(-7.5,-0.,1.-tot);
    vec3 sccol = mix(vec3(0.11,0.1,0.2),vec3(.2,0.,0.1),smoothstep(0.,900.,s2p));
    col = mix(col,sccol,1.-tot)*1.6;
    vec3 sncol = mix(vec3(1.4,0.3,0.),vec3(1.5,.65,0.),smoothstep(0.,1200.,s2p));
    float sd = pow(sun,10.)+.7;
    col += sncol*bz*bz*bz*tot*tot*tot*sd;
    
    if (wtr) col = mix(col,vec3(0.5,0.7,1.)*0.3,0.4); //make the water blue-er
    return vec4(col,tot);
}
//------------------------------------------------------------------
//-------------------------------Extras-----------------------------
//------------------------------------------------------------------
float bnoise(in vec2 p)
{
    float d = sin(p.x*1.5+sin(p.y*.2))*0.1;
    return d += texture2D(iChannel0,p.xy*0.01+time*0.001).x*0.04;
}

vec3 bump(in vec2 p, in vec3 n, in float t)
{
    vec2 e = vec2(40.,0)/(t*t);
    float n0 = bnoise(p);
    vec3 d = vec3(bnoise(p+e.xy)-n0,2., bnoise(p+e.yx)-n0)/e.x;
    n = normalize(n-d);
    return n;
}



//------------------------------------------------------------------
//------------------------------------------------------------------
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{	
    vec2 bp = fragCoord.xy/iResolution.xy*2.-1.;
    vec2 p  = bp;
	p.x*=iResolution.x/iResolution.y;
	vec2 mo = iMouse.xy / iResolution.xy-.5;
    mo = (mo==vec2(-.5))?mo=vec2(-0.4,-0.15):mo;
	mo.x *= iResolution.x/iResolution.y;
	vec3 ro = vec3(1000.,0.,100.);
    vec3 rd = normalize(vec3(p,-2.7));
    rd = rotx(rd,0.15+mo.y*0.4);rd = roty(rd,1.5+mo.x*5.5);
    vec3 brd = rd;
    vec3 col = vec3(0);
		
	float pln = plane(ro, rd, vec3(0.,-4.,0), vec3(1.,0.,0.), vec3(0.0,.0,1.0));
    vec3 ppos = ro + rd*pln;
    bool wtr = false;
    vec3 bm = vec3(0);
    if (pln < 500. && pln > 0.)
    {
        vec3 n = vec3(0,1,0);
        float d= distance(ro,ppos);
        n = bump(ppos.xz,n,d);
        bm = n;
        rd = reflect(rd,n);
        wtr = true;
    }
    vec4 clo = clouds(ro, rd, wtr);
    col = clo.rgb;
    
    vec3 rz = tmarch(ro,brd,350.);
    float px = 3.5/iResolution.y;
    if (rz.x < FAR && (rz.x < pln || pln < 0.))
    {
        vec3 pos = ro + brd*rz.x;
        float dst = distance(pos, ro);
        vec3 nor = normal(pos,dst);
        float nl = clamp(dot(nor,lgt),0.,1.);
        vec3 mcol = vec3(0.04)+vec3(nl)*0.4*vec3(.5,0.35,0.1);
        mcol = mix(mcol,hor,smoothstep(210.,400.,rz.x-(pos.y+18.)*5.));//fogtains
        col = mix(mcol,col,clamp(rz.y/(px*rz.z),0.,1.));
    }
    
    //smooth water edge
    if (wtr && rz.x > pln)col = mix(col,hor*vec3(0.3,0.4,.6)*0.4,smoothstep(10.,200.,pln));
    
    //post
    col = pow(clamp(col,0.0,1.0), vec3(.9));
    col.g *= 0.93;
    //fancy vignetting
    float vgn1 = pow(smoothstep(0.0,.3,(bp.x + 1.)*(bp.y + 1.)*(bp.x - 1.)*(bp.y - 1.)),.5);
    float vgn2 = 1.-pow(dot(vec2(bp.x*.3, bp.y),bp),3.);
    col *= mix(vgn1,vgn2,.4)*.5+0.5;
	fragColor = vec4( col, 1.0 );
}