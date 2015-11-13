// ALL TAKEN FROM IQs AMAZING SITE / TUTORIALS / SHADERS:
// http://www.iquilezles.org/www/index.htm
// https://www.shadertoy.com/user/iq


const float MAX_TRACE_DISTANCE = 3.0;           // max trace distance
const float INTERSECTION_PRECISION = 0.001;        // precision of the intersection
const int NUM_OF_TRACE_STEPS = 100;
const float PI = 3.14159;


vec3 hsv(float h, float s, float v)
{
  return mix( vec3( 1.0 ), clamp( ( abs( fract(
    h + vec3( 3.0, 2.0, 1.0 ) / 3.0 ) * 6.0 - 3.0 ) - 1.0 ), 0.0, 1.0 ), s ) * v;
}


float smax(float a, float b, float k)
{
    return log(exp(k*a)+exp(k*b))/k;
}

float smin(float a, float b, float k)
{
    return -(log(exp(k*-a)+exp(k*-b))/k);
}

// q is point
// n is normal
// p is point on plane
vec3 projOnPlane( vec3 q, vec3 p , vec3 n){
    
    vec3 v = q - dot(q - p, n) * n;
    return v;
}

// Taken from https://www.shadertoy.com/view/4ts3z2
float tri(in float x){return abs(fract(x)-.5);}
vec3 tri3(in vec3 p){return vec3( tri(p.z+tri(p.y*1.)), tri(p.z+tri(p.x*1.)), tri(p.y+tri(p.x*1.)));}
                                 

// Taken from https://www.shadertoy.com/view/4ts3z2
float triNoise3D(in vec3 p, in float spd)
{
    float z=1.4;
	float rz = 0.;
    vec3 bp = p;
	for (float i=0.; i<=3.; i++ )
	{
        vec3 dg = tri3(bp*2.);
        p += (dg+iGlobalTime*.1*spd);

        bp *= 1.8;
		z *= 1.5;
		p *= 1.2;
        //p.xz*= m2;
        
        rz+= (tri(p.z+tri(p.x+tri(p.y))))/z;
        bp += 0.14;
	}
	return rz;
}

float posToFloat( vec3 p ){
 
    float f = triNoise3D( p * .2, .1 );
    return f;
    
}




float udBox( vec3 p, vec3 b )
{
  return length(max(abs(p)-b,0.0));
}


float udRoundBox( vec3 p, vec3 b, float r )
{
  return length(max(abs(p)-b,0.0))-r;
}


float sdSphere( vec3 p, float s )
{
  return length(p)-s;
}


float sdTorus( vec3 p, vec2 t )
{
  vec2 q = vec2(length(p.xz)-t.x,p.y);
  return length(q)-t.y;
}


//----
// Camera Stuffs
//----
mat3 calcLookAtMatrix( in vec3 ro, in vec3 ta, in float roll )
{
    vec3 ww = normalize( ta - ro );
    vec3 uu = normalize( cross(ww,vec3(sin(roll),cos(roll),0.0) ) );
    vec3 vv = normalize( cross(uu,ww));
    return mat3( uu, vv, ww );
}

void doCamera( out vec3 camPos, out vec3 camTar, in float time, in vec2 mouse )
{
    float an = 0.3 + 10.0*mouse.x;
	camPos = vec3(1.5*sin(an),0.,1.5*cos(an));
    camTar = vec3(0.0,0.0,0.0);
}

// ROTATION FUNCTIONS TAKEN FROM
//https://www.shadertoy.com/view/XsSSzG
mat3 xrotate(float t) {
	return mat3(1.0, 0.0, 0.0,
                0.0, cos(t), -sin(t),
                0.0, sin(t), cos(t));
}

mat3 yrotate(float t) {
	return mat3(cos(t), 0.0, -sin(t),
                0.0, 1.0, 0.0,
                sin(t), 0.0, cos(t));
}

mat3 zrotate(float t) {
    return mat3(cos(t), -sin(t), 0.0,
                sin(t), cos(t), 0.0,
                0.0, 0.0, 1.0);
}


mat3 fullRotate( vec3 r ){
 
   return xrotate( r.x ) * yrotate( r.y ) * zrotate( r.z );
    
}

float rotatedBox( vec3 p , vec3 rot , vec3 size , float rad ){
    
    vec3 q = fullRotate( rot ) * p;
    return udRoundBox( q , size , rad );
    
    
}



vec2 smoothU( vec2 d1, vec2 d2, float k)
{
    float a = d1.x;
    float b = d2.x;
    float h = clamp(0.5+0.5*(b-a)/k, 0.0, 1.0);
    return vec2( mix(b, a, h) - k*h*(1.0-h), mix(d2.y, d1.y, pow(h, 2.0)));
}


vec2 smoothSub( vec2 d1, vec2 d2, float k)
{
    return  vec2( smax( -d1.x , d2.x , k ) , d2.y );
}




// checks to see which intersection is closer
// and makes the y of the vec2 be the proper id
vec2 opU( vec2 d1, vec2 d2 ){
    
	return (d1.x<d2.x) ? d1 : d2;
    
}

/*float opRepCube( vec3 p, vec3 c )
{
    vec3 q = mod(p,c)-0.5*c;
    return primitve( q );
}
*/


float opS( float d1, float d2 )
{
    return max(-d1,d2);
}


vec2 opS( vec2 d1, vec2 d2 )
{
   return (-d1.x>d2.x) ? vec2( -d1.x , d1.y ) : d2;
}


float sdCone( vec3 p, vec2 c )
{
    // c must be normalized
    float q = length(p.xy);
    return dot(c,vec2(q,p.z));
}

float sdCapsule( vec3 p, vec3 a, vec3 b, float r )
{
    vec3 pa = p - a, ba = b - a;
    float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );
    return length( pa - ba*h ) - r;
}

float doRing( vec3 p ){
  
    vec3 pos = p;
    
     
    float lor = sign( pos.x );
    
    
    pos.x = abs( pos.x );
    
    //pos.x = mod( pos.x , 1.5 );
    float degree = atan( pos.y , pos.z );
    
    float ogD = degree;
    
    degree += iGlobalTime;// * (1. + lor * .2);
    float l = length( pos.yz );
    

    
    degree = mod( degree - 3.14159  / 8. , 3.14159  / 4. );
 
    
    pos.y = l * sin( degree );
    pos.z = l * cos( degree );

    
    return sdSphere( pos - vec3( 2.4 ,  1. , 2.39 )  * .2  , .04 *( 1. + abs(ogD) ));
    
}

float sdCylinder( vec3 p, vec3 c )
{
  return length(p.xz-c.xy)-c.z;
}



float sdCappedCylinder( vec3 p, vec2 h )
{
    
    
  vec2 d = abs(vec2(length(p.xz),p.y)) - h;
  return min(max(d.x,d.y),0.0) + length(max(d,0.0));
}



vec3 twist( vec3 p ){
 float c = cos(20.0*p.z);
  float s = sin(20.0*p.z);
    mat2  m = mat2(c,-s,s,c);
    vec3  q = vec3(m*p.xy,p.z);   
    
    return q;
}


float bentCappedCylinder( vec3 p, vec2 h )
{
    
 
  vec2 d = abs(vec2(length(p.xz),p.y)) - h;
  return min(max(d.x,d.y),0.0) + length(max(d,0.0));
}


vec2 glasses( vec3 p ){
    
    
   vec3 og = p;
    
   mat3 r = xrotate( -.18 * PI / 2. );
    
    p = r * p;
 
    
   p.x = abs( p.x );
    
   vec3 eyePos = vec3( 0.25 , 0. , 0. );
    
   r = xrotate( PI / 2. );
    
   vec2 res;
    
   // Rims
   res = vec2( sdTorus( r * (p -eyePos) , vec2( .16 , .02 ) ) , 2. );
    
    
   //spectacles
   res = smoothU( res ,vec2( sdCappedCylinder( r * (p -eyePos) , vec2( .15 , .01 ) ), 10. ), .01);
    
    
   eyePos = vec3( 0.41 , 0.1 , -0.25 );
    
   //earHolders
   res = smoothU( res ,vec2( bentCappedCylinder( r * (p -eyePos) , vec2( .01 , .25 ) ), 2. ), .03);
    
   r = xrotate(.6* PI / 2. );
   eyePos = vec3( 0.41 , 0.06 , -0.55 );
   res = smoothU( res ,vec2( bentCappedCylinder( r * (p -eyePos) , vec2( .008 , .08 ) ), 3. ), .02);
     
    
   // cross bar
    eyePos = vec3( 0. , .13 , 0. );
    
   r = zrotate( PI / 2. );
    
   res = smoothU( res , vec2( sdCappedCylinder( r * ( og - eyePos) , vec2( .01 , .13 )) , 2. ), .03);
   return res;
    
}

float sdPlane( vec3 p, vec4 n )
{
  // n must be normalized
  return dot(p,n.xyz) + n.w;
}


//--------------------------------
// Modelling 
//--------------------------------
vec2 map( vec3 pos ){  
   
    
    vec2 ring;


    vec2 res = glasses( pos );
    vec2 plane = vec2( sdPlane( pos , vec4( 0., 1. , 0. , .17 ) ), 20. );
    
    res = opU( res , plane );
    

    
    	
   	return res;
    
}



vec2 calcIntersection( in vec3 ro, in vec3 rd ){

    
    float h =  INTERSECTION_PRECISION*2.0;
    float t = 0.0;
	float res = -1.0;
    float id = -1.;
    
    for( int i=0; i< NUM_OF_TRACE_STEPS ; i++ ){
        
        if( h < INTERSECTION_PRECISION || t > MAX_TRACE_DISTANCE ) break;
	   	vec2 m = map( ro+rd*t );
        h = m.x;
        t += h;
        id = m.y;
        
    }

    if( t < MAX_TRACE_DISTANCE ) res = t;
    if( t > MAX_TRACE_DISTANCE ) id =-1.0;
    
    return vec2( res , id );
    
}




// Calculates the normal by taking a very small distance,
// remapping the function, and getting normal for that
vec3 calcNormal( in vec3 pos ){
    
	vec3 eps = vec3( 0.001, 0.0, 0.0 );
	vec3 nor = vec3(
	    map(pos+eps.xyy).x - map(pos-eps.xyy).x,
	    map(pos+eps.yxy).x - map(pos-eps.yxy).x,
	    map(pos+eps.yyx).x - map(pos-eps.yyx).x );
	return normalize(nor);
}

float softshadow( in vec3 ro, in vec3 rd, in float mint, in float tmax )
{
	float res = 1.0;
    float t = mint;
    for( int i=0; i<50; i++ )
    {
		float h = map( ro + rd*t ).x;
        res = min( res, 20.*h/t );
        t += clamp( h, 0.02, 0.10 );
        if( h<0.001 || t>tmax ) break;
    }
    return clamp( res, 0.0, 1.0 );

}


float calcAO( in vec3 pos, in vec3 nor )
{
	float occ = 0.0;
    float sca = 1.0;
    for( int i=0; i<5; i++ )
    {
        float hr = 0.01 + 0.612*float(i)/4.0;
        vec3 aopos =  nor * hr + pos;
        float dd = map( aopos ).x;
        occ += -(dd-hr)*sca;
        sca *= 0.5;
    }
    return clamp( 1.0 - 3.0*occ, 0.0, 1.0 );    
}


vec3 bodyColor( vec3 p , vec3 n ){
    return vec3( 1. );
    
}

float hash( float n ) { return fract(sin(n)*753.5453123); }
float noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);
    f = f*f*(3.0-2.0*f);
	
    float n = p.x + p.y*157.0 + 113.0*p.z;
    return mix(mix(mix( hash(n+  0.0), hash(n+  1.0),f.x),
                   mix( hash(n+157.0), hash(n+158.0),f.x),f.y),
               mix(mix( hash(n+113.0), hash(n+114.0),f.x),
                   mix( hash(n+270.0), hash(n+271.0),f.x),f.y),f.z);
}




vec3 pal( in float t, in vec3 a, in vec3 b, in vec3 c, in vec3 d )
{
    return a + b*cos( 6.28318*(c*t+d) );
} 


/*vec3 cPal( float t ){
 return pal(t, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(2.0,1.0,0.0),vec3(0.5,0.20,0.25));   
    
}*/

vec3 cPal( float t ){
 return vec3( 1. - t );    
}
vec3 lensColor( vec3 p , vec3 n , vec3 rd ){
    
   vec3 pos; vec3 col; float v;
    
   float offset = sin(iGlobalTime * .1 + sin( iGlobalTime * .3 + sin( iGlobalTime * .1)));
    
   for( int i = 0; i < 10; i++ ){
       
    pos = p + rd * .03 * float(i);
    v = noise( pos * 10. + rd * offset  );
    
    col = hsv( abs(v) * .1 + offset + float( i ) / 10. , 1. , 1. ) *(1.+ float( i ) / 10.);
       
    if( v > .5 ){ break;}
        
   }
    
   return col; 
    
}

vec3 turtleColor( vec3 p , vec3 n ){
    
   return vec3( 1. );
}

vec3 planeColor( vec3 p , vec3 n  , float ao ){
    
   
   return cPal( length( p ) * .1 ) *ao;
   
}

vec3 bgColor(){
    
   return vec3( .8 );
}






void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 p = (-iResolution.xy + 2.0*fragCoord.xy)/iResolution.y;
    vec2 m = iMouse.xy/iResolution.xy;

    //-----------------------------------------------------
    // camera
    //-----------------------------------------------------
    
    // camera movement
    vec3 ro, ta;
    doCamera( ro, ta, iGlobalTime, m );

    // camera matrix
    mat3 camMat = calcLookAtMatrix( ro, ta, 0.0 );  // 0.0 is the camera roll
    
	// create view ray
	vec3 rd = normalize( camMat * vec3(p.xy,2.0) ); // 2.0 is the lens length
    
    vec2 res = calcIntersection( ro , rd  );


    vec3 col = vec3( 0. );
    
    mat3 basis = mat3(
     
        1. , 0. , 0. ,
        0. , 1. , 0. ,
        0. , 0. , 1.
       
    );
        
    
    col = bgColor();
   
    
    if( res.y > -.5 ){
        
        vec3 pos = ro + rd * res.x;
        vec3 norm = calcNormal( pos );
        float ao = calcAO( pos , norm );
        
      
        
        vec3 refr = refract( rd , norm , 1. / 1.2 );
        
        //col = textureCube( iChannel0 , normalize(refr) ).xyz;
 
		
        // Lens
        if( res.y >= 4. && res.y < 10.1 ){
        	col = lensColor( pos , norm , rd );// vec3( pow((1. - dot( norm , normalize( pos ) )) , .3) );
        
        // Plane
        }else if( res.y == 20. ){
         	col = planeColor( pos , norm , ao ); 
            
        //Turtle
        }else if( res.y== 3. ){
         	col = vec3( 2.  ) * ao;   
            
        // body
        }else{
        	col =  vec3( .5 ) * ao;
    	}
        
        
    }

    fragColor = vec4( col , 1. );



}