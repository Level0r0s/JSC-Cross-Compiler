#define SAMPLES 5

float sdPlane( vec3 p )
{
	return p.y;
}

float sdSphere( vec3 p, float s )
{
    return length(p)-s;
}


float sdCylinder( vec3 p, vec2 h )
{
  vec2 d = abs(vec2(length(p.xy),p.z)) - h;
  return min(max(d.x,d.y),0.0) + length(max(d,0.0));
}

float length8( vec2 p )
{
	p = p*p; p = p*p; p = p*p;
	return pow( p.x + p.y, 1.0/8.0 );
}

float sdTorus88( vec3 p, vec2 t )
{
  vec2 q = vec2(length8(p.xz)-t.x,p.y);
  return length8(q)-t.y;
}

//----------------------------------------------------------------------

float opS( float d1, float d2 )
{
    return max(-d2,d1);
}

vec2 opU( vec2 d1, vec2 d2 )
{
	return (d1.x<d2.x) ? d1 : d2;
}

float SRamp3(float x, float k)
{
   float xp = clamp(-x * k + 0.5, 0.0, 1.0);
   
   float xp2 = xp * xp;
    
   return min(x, xp2 * (xp2 * 0.5 - xp) / k);
}

float SMin3(float a, float b, float k)
{
    return a + SRamp3(b - a, k);
}


//----------------------------------------------------------------------


float T = 0.;

vec2 map( in vec3 pos )
{
    vec2 res = vec2(sdCylinder(pos.xzy, vec2(5., .1)), 10.);
    res = opU(res, vec2(sdTorus88(pos-vec3(0., .8, 0.), vec2(3., .2)),111.));
    float ops = SMin3(            
        	sdSphere(pos-vec3(sin(T+cos(T*.3)*6. )*1.5, 1.7+cos(T*2.)*.5, cos(T+sin(T*.6)*3.)*1.5), 0.5),
        	sdSphere(pos-vec3(cos(T+cos(T*.3)*6. )*1.8, 1.7+sin(T*3.)*.5, sin(T+sin(T*.6)*3.)*1.5), 0.5) ,1.);
    res = opU(res,
        vec2(SMin3(sdSphere(pos-vec3(0., 2., 0.), 1.), ops, 1.)
            , 222.) ); 
    return res;
}

vec2 castRay( in vec3 ro, in vec3 rd )
{
    float tmin = 0.1;
    float tmax = 30.0;
    
    
	float precis = 0.002;
    float t = tmin;
    float m = -1.0;
    for( int i=0; i<80; i++ )
    {
	    vec2 res = map( ro+rd*t );
        if( res.x<precis || t>tmax ) break;
        t += res.x;
	    m = res.y;
    }

    if( t>tmax ) m=-1.0;
    return vec2( t, m );
}


float softshadow( in vec3 ro, in vec3 rd, in float mint, in float tmax )
{
	float res = 1.0;
    float t = mint;
    for( int i=0; i<30; i++ )
    {
		float h = map( ro + rd*t ).x;
        res = min( res, 8.0*h/t );
        t += clamp( h, 0.02, 0.10 );
        if( h<0.001 || t>tmax ) break;
    }
    return clamp( res, 0.0, 1.0 );

}

vec3 calcNormal( in vec3 pos )
{
	vec3 eps = vec3( 0.001, 0.0, 0.0 );
	vec3 nor = vec3(
	    map(pos+eps.xyy).x - map(pos-eps.xyy).x,
	    map(pos+eps.yxy).x - map(pos-eps.yxy).x,
	    map(pos+eps.yyx).x - map(pos-eps.yyx).x );
	return normalize(nor);
}


#define MOD2 vec2(443.8975,397.2973)
#define MOD3 vec3(443.8975,397.2973, 491.1871)
#define MOD4 vec4(443.8975,397.2973, 491.1871, 470.7827)

// *** Use these for integer ranges, ie Value-Noise/Perlin functions.
//#define MOD2 vec2(.16632,.17369)
//#define MOD3 vec3(.16532,.17369,.15787)
//#define MOD4 vec4(.16532,.17369,.15787, .14987)

//----------------------------------------------------------------------------------------
//  1 out, 1 in...
float hash11(float p)
{
	vec2 p2 = fract(vec2(p) * MOD2);
    p2 += dot(p2.yx, p2.xy+19.19);
	return fract(p2.x * p2.y);
}

//----------------------------------------------------------------------------------------
//  1 out, 2 in...
float hash12(vec2 p)
{
	p  = fract(p * MOD2);
    p += dot(p.xy, p.yx+19.19);
    return fract(p.x * p.y);
}

//----------------------------------------------------------------------------------------
//  1 out, 3 in...
float hash13(vec3 p)
{
	p  = fract(p * MOD3);
    p += dot(p.xyz, p.yzx + 19.19);
    return fract(p.x * p.y * p.z);
}

//----------------------------------------------------------------------------------------
//  2 out, 1 in...
vec2 hash21(float p)
{
	//p  = fract(p * MOD3);
    vec3 p3 = fract(vec3(p) * MOD3);
    p3 += dot(p3.xyz, p3.yzx + 19.19);
   return fract(vec2(p3.x * p3.y, p3.z*p3.x));
}

//----------------------------------------------------------------------------------------
///  2 out, 2 in...
vec2 hash22(vec2 p)
{
	vec3 p3 = fract(vec3(p.xyx) * MOD3);
    p3 += dot(p3.zxy, p3.yxz+19.19);
    return fract(vec2(p3.x * p3.y, p3.z*p3.x));
}

//----------------------------------------------------------------------------------------
//  3 out, 1 in...
vec3 hash31(float p)
{
   vec3 p3 = fract(vec3(p) * MOD3);
   p3 += dot(p3.xyz, p3.yzx + 19.19);
   return fract(vec3(p3.x * p3.y, p3.x*p3.z, p3.y*p3.z));
}


//----------------------------------------------------------------------------------------
///  3 out, 2 in...
vec3 hash32(vec2 p)
{
	vec3 p3 = fract(vec3(p.xyx) * MOD3);
    p3 += dot(p3.zxy, p3.yxz+19.19);
    return fract(vec3(p3.x * p3.y, p3.x*p3.z, p3.y*p3.z));
}

//----------------------------------------------------------------------------------------
///  3 out, 3 in...
vec3 hash33(vec3 p)
{
	p = fract(p * MOD3);
    p += dot(p.zxy, p+19.19);
    return fract(vec3(p.x * p.y, p.x*p.z, p.y*p.z));
}

//----------------------------------------------------------------------------------------
// 4 out, 1 in...
vec4 hash41(float p)
{
	vec4 p4 = fract(vec4(p) * MOD4);
    p4 += dot(p4.wzxy, p4+19.19);
    return fract(vec4(p4.x * p4.y, p4.x*p4.z, p4.y*p4.w, p4.x*p4.w));
}

//----------------------------------------------------------------------------------------
// 4 out, 2 in...
vec4 hash42(vec2 p)
{
	vec4 p4 = fract(vec4(p.xyxy) * MOD4);
    p4 += dot(p4.wzxy, p4+19.19);
    return fract(vec4(p4.x * p4.y, p4.x*p4.z, p4.y*p4.w, p4.x*p4.w));
}

//----------------------------------------------------------------------------------------
// 4 out, 3 in...
vec4 hash43(vec3 p)
{
	vec4 p4 = fract(vec4(p.xyzx) * MOD4);
    p4 += dot(p4.wzxy, p4+19.19);
    return fract(vec4(p4.x * p4.y, p4.x*p4.z, p4.y*p4.w, p4.x*p4.w));
}

//----------------------------------------------------------------------------------------
// 4 out, 4 in...
vec4 hash44(vec4 p)
{
	vec4 p4 = fract(p * MOD4);
    p4 += dot(p4.wzxy, p4+19.19);
    return fract(vec4(p4.x * p4.y, p4.x*p4.z, p4.y*p4.w, p4.x*p4.w));
}

//###############################################################################

vec3 rand_cos_hemi( const vec3 n, const vec2 p ) {
  	vec2 rv2 = hash22(p);
    
	vec3  uu = normalize( cross( n, vec3(0.0,1.0,1.0) ) );
	vec3  vv = normalize( cross( uu, n ) );
	
	float ra = sqrt(rv2.y);
	float rx = ra*cos(6.2831*rv2.x); 
	float ry = ra*sin(6.2831*rv2.x);
	float rz = sqrt( 1.0-rv2.y );
	vec3  rr = vec3( rx*uu + ry*vv + rz*n );
    
    return normalize( rr );
}

vec3 rand_pow_cos_hemi( const vec3 n, vec2 pw, const vec2 p ) {
  	vec2 rv2 = pow(hash22(p), pw);
    
	vec3  uu = normalize( cross( n, vec3(0.0,1.0,1.0) ) );
	vec3  vv = normalize( cross( uu, n ) );
	
	float ra = sqrt(rv2.y);
	float rx = ra*cos(6.2831*rv2.x); 
	float ry = ra*sin(6.2831*rv2.x);
	float rz = sqrt( 1.0-rv2.y );
	vec3  rr = vec3( rx*uu + ry*vv + rz*n );
    
    return normalize( rr );
}

vec3 rand_hemi( const vec3 n, const vec2 p ) {
    vec2 r = hash22(p)*6.2831;
	vec3 dr=vec3(sin(r.x)*vec2(sin(r.y),cos(r.y)),cos(r.x));
	return dot(dr,n) * dr;
}

vec3 light(vec3 d) {
    return vec3(1.0, 0.7, 0.3);
	//return textureCube(iChannel0, d).xyz*2.;
}

vec3 render( inout vec3 ro, inout vec3 rd, in vec2 hp )
{ 
    vec2 res = castRay(ro,rd);
    float t = res.x;
	float m = res.y;
    vec3 col = light(rd);
    if( m>-0.5 )
    {
        vec3 pos = ro + t*rd;
        vec3 nor = calcNormal( pos );
        vec3 ref = rand_pow_cos_hemi(nor, vec2(m == 222. ? 6. : 4.), hp);
        
        
		vec3  lig = normalize(vec3(0.5, 1., 0.9)+rand_cos_hemi(nor, hp));
        vec3 lc = light(lig);
        float sh = softshadow(pos, lig, 0.1, 100.);
        float dif = clamp( dot( nor, lig ), 0.0, 1.0 )*sh;
        float fre = pow( clamp(1.0+dot(nor,rd),0.0,1.0), 2.0 )*sh;
        
        vec3 dc;
        if(m == 111.) {
        	dc = vec3(0.5, 0.9, 0.3);  
        } 
        else if(m == 222.) {
            dc = vec3(0.4, 0.2, 0.8);
        }
        else if(m == 10.) {
            float f = mod( floor(5.0*pos.z) + floor(5.0*pos.x), 2.0);
        	dc = vec3(0.2, 0.2, 0.2)+f;
        }
        
       	col =   dif*dc
            	+ fre*dc*.6;

		rd = ref;
        ro += rd;
    }
	return vec3( clamp(col,0.0,1.0) );
}

mat3 setCamera( in vec3 ro, in vec3 ta, float cr )
{
	vec3 cw = normalize(ta-ro);
	vec3 cp = vec3(sin(cr), cos(cr),0.0);
	vec3 cu = normalize( cross(cw,cp) );
	vec3 cv = normalize( cross(cu,cw) );
    return mat3( cu, cv, cw );
}
vec3 tone(vec2 q, vec3 color, float gamma)
{
	float white = .9;
	float luma = dot(color, vec3(0.2126, 0.7152, 0.0722));
	float toneMappedLuma = luma * (1. + luma / (white*white)) / (1. + luma);
	color *= toneMappedLuma / luma;
    
    color *= 0.1 + 0.8*16.0*q.x*q.y*(1.0-q.x)*(1.0-q.y);
	color = pow(color, vec3(1. / gamma));
	return color;
}
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 q = fragCoord.xy/iResolution.xy;
    vec2 p = -1.0+2.0*q;
	p.x *= iResolution.x/iResolution.y;
    vec2 mo = iMouse.xy/iResolution.xy;
		 
	float d, y;
    if(dot(mo,mo) > 0.0001) {
        d =   15.0 + mo.x*10.;
        y = mo.y+.1;
    } else {
        d = iGlobalTime*.2;
        y = 1.;
    }

	// camera	
	vec3 rro = vec3(cos(d), y*.7, sin(d))*8.;
	vec3 ta = vec3( 0., 1., 0. );
	
	// camera-to-world transformation
    mat3 ca = setCamera( rro, ta, 0.0 );
    
    vec3 col = vec3(0.);
    for(int i = 0; i < SAMPLES; ++i ) {
    	// ray direction
        vec2 hp = q+float(i)*10.;
		vec3 rd = ca * normalize( vec3(p.xy+hash22(hp).xy/iResolution.xy,2.5) );
		vec3 ro = rro;
    	// render	
        T = iGlobalTime + hash12(hp)*.04;
    	col += render( ro, rd, hp*2. )*render(ro,rd,hp*3.)*render(ro,rd,hp.yx*3.);
    }
	col /= float(SAMPLES);
    

    col = tone(q, col, 2.2);
    
    vec3 cdx = dFdx(col);
    vec3 cdy = dFdy(col);
    col += (cdx+cdy)*.2;

    fragColor=vec4( col, 1.0 );
}
