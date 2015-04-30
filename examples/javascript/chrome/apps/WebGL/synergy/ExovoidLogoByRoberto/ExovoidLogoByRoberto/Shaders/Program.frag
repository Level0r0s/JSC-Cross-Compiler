//------------------------------------------------------------------------------------------
//Exovoid logo fragment shader using raymarching, by Roberto Marra @shagan
//Lighting from IQ
//Thanks to Inigo Quilez for all explanations and articles. 


float time = iGlobalTime*2.5;

float perlin(vec3 p) {
    vec3 i = floor(p);
    vec4 a = dot(i, vec3(1., 57., 21.)) + vec4(0., 57., 21., 78.);
    vec3 f = cos((p-i)*acos(-1.))*(-.5)+.5;
    a = mix(sin(cos(a)*a),sin(cos(1.+a)*(1.+a)), f.x);
    a.xy = mix(a.xz, a.yw, f.y);
    return mix(a.x, a.y, f.z);
}


vec2 merge(vec2 d1,vec2 d2) {
    return (d1.x<d2.x) ? d1 : d2;
}

float merge_substract( float d1, float d2) {
    return max(-d2,d1);
}

float subPlane(vec3 p) {
   vec3 noise=perlin(vec3(p.x,p.y,p.z))+vec3(sin(time)+cos(time));
   //return p.y-clamp(noise.z,0.,0.3);
    return p.y-clamp(noise.z,-0.1,0.1);
}

float subSphere(vec3 p,float rad) {
    return length(p)-rad;
}

float subBox(vec3 p,vec3 b) {
  return length(max(abs(p)-b,0.0));
}

vec2 map( in vec3 pos ) {
    vec2 res = vec2(subPlane(pos), 1.0 );


    float depth=0.2;

    res = merge(res,
        vec2(
            subBox(pos,vec3(2.8,2,depth)),2
            )
          );


    res = merge(res,
        vec2(
            subBox(pos-vec3(0,1,0),vec3(0.8,0.8,depth)),3
            )
          );

    res = merge(res, vec2(
         merge_substract(
         subBox(pos-vec3(0,1,0),vec3(0.2,1,depth)),
         subBox(pos-vec3(0,1,0),vec3(0.5,0.5,depth))
         ),2
         ));

    res = merge(res,vec2(subBox(pos-vec3(0,1,0),vec3(0.5,0.5,depth)),2));

    res = merge(res, vec2(subBox(pos-vec3(-1,1,0),vec3(0.2,0.2,depth)),3.0));
    res = merge(res, vec2(subBox(pos-vec3(1,1,0),vec3(0.2,0.2,depth)),3.0));
    res = merge(res, vec2(subBox(pos-vec3(1.7,abs(sin(time)+cos(time))*0.75,-1.),vec3(0.2,0.2,0.2)),5.0));

    return res;
}

vec2 castRay( in vec3 rayOrigin, in vec3 rayDirection ) {
    float tmin = 0.1;       //travel min
    float tmax = 20.0;      //travel max
	float precis = 0.0002;  //precision
    float step = tmin;
    float m = -1.0;
    for( int i=0; i<50; i++ ) {
	    vec2 res = map(rayOrigin+rayDirection*step);

        if( res.x<precis || step>tmax ) break;  //check limit

        step += res.x;
	    m = res.y;
    }

    if( step>tmax ) m=-1.0;
    return vec2( step, m );
}


//---- Lighting from Inigo Quilez  ---------------------------------------------------------
float softshadow( in vec3 ro, in vec3 rd, in float mint, in float tmax )
{
	float res = 1.0;
    float t = mint;
    for( int i=0; i<16; i++ )
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

float calcAO( in vec3 pos, in vec3 nor )
{
	float occ = 0.0;
    float sca = 1.0;
    for( int i=0; i<5; i++ )
    {
        float hr = 0.01 + 0.12*float(i)/4.0;
        vec3 aopos =  nor * hr + pos;
        float dd = map( aopos ).x;
        occ += -(dd-hr)*sca;
        sca *= 0.95;
    }
    return clamp( 1.0 - 3.0*occ, 0.0, 1.0 );
}


vec3 calcLight(in vec3 col, in vec3 pos, in vec3 rd) {
         vec3 nor = calcNormal( pos );
         vec3 ref = reflect( rd, nor ); //calculate the reflection direction for an incident vector
         float occ = calcAO( pos, nor );
 		 vec3  lig = normalize( vec3(0,2,-2.));
 		 float amb = clamp( 0.5+0.5*nor.y, 0.0, 1.0 );
         float dif = clamp( dot( nor, lig ), 0.0, 1.0 );
         float bac = clamp( dot( nor, normalize(vec3(-lig.x,0.0,-lig.z))), 0.0, 1.0 )*clamp( 1.0-pos.y,0.0,1.0);
         float dom = smoothstep( -0.1, 0.1, ref.y );
         float fre = pow( clamp(1.0+dot(nor,rd),0.0,1.0), 2.0 );
 		 float spe = pow(clamp( dot( ref, lig ), 0.0, 1.0 ),16.0);

         dif *= softshadow( pos, lig, 0.5, 2.5 );
         dom *= softshadow( pos, ref, 0.5, 2.5 );

 		 vec3 brdf = vec3(0);
         brdf += 1.20*dif*vec3(1.00,0.90,0.60);
 		 brdf += 1.20*spe*vec3(1.00,0.90,0.60)*dif;
         brdf += 0.30*amb*vec3(0.50,0.70,1.00)*occ;
         brdf += 0.40*dom*vec3(0.50,0.70,1.00)*occ;
         brdf += 0.30*bac*vec3(0.25,0.25,0.25)*occ;
         brdf += 0.40*fre*vec3(1.00,1.00,1.00)*occ;
 		 brdf += 0.02;
 		 col = col*brdf;
         return col;
}
//------------------------------------------------------------------------------------------



vec3 render( in vec3 ro, in vec3 rd ) {
    vec3 col = vec3(0.8, 0.9, 1.0);

    vec2 res = castRay(ro,rd);
    float t = res.x;
	float m = res.y;
    if( m>0.) {
        vec3 pos = ro + t*rd;

        // material
        if(m==3.0) {
            col= vec3(0);
        }else if(m==5.0){
            col= vec3(0,0.68,0.92);
        }else{
            col= vec3(1);
        }

        if( m<1.5){
            float f = mod( floor(2.0*pos.x)+floor(2.0*pos.z) , 2.0);
            vec3 cc = vec3(clamp(0.5*f,0.3,1.0));
            cc =  vec3(cc.x*0.5,cc.y*0.7,cc.z*0.9);
            col = clamp(cc,0.,1.);
        }

        col = calcLight(col,pos,rd);
    }

	return vec3( clamp(col,0.0,1.0) ); 
}


mat3 getRotMatrix(vec3 ang) {
	vec2 a1 = vec2(sin(ang.x),cos(ang.x));
    vec2 a2 = vec2(sin(ang.y),cos(ang.y));
    vec2 a3 = vec2(sin(ang.z),cos(ang.z));
    mat3 m;
    m[0] = vec3(a1.y*a3.y+a1.x*a2.x*a3.x,a1.y*a2.x*a3.x+a3.y*a1.x,-a2.y*a3.x);
	m[1] = vec3(-a2.y*a1.x,a1.y*a2.y,a2.x);
	m[2] = vec3(a3.y*a1.x*a2.x+a1.y*a3.x,a1.x*a3.x-a1.y*a3.y*a2.x,a2.y*a3.y);
	return m;
}


void mainImage( out vec4 fragColor, in vec2 fragCoord ) {

   float g_aspectRatio = iResolution.x / iResolution.y;
    vec2 mo = iMouse.xy/iResolution.xy;

    float focalLength = 1.5;

    vec3 camPos		= vec3(0, 1, -5);						// The eye position in the world
    vec3 camUp		= normalize(vec3(0.0, 1.0, 0.0));		// The upward-vector of the image plane
    vec3 camRight	= normalize(vec3(1.0, 0.0, 0.0));		// The right-vector of the image plane
    vec3 camForward	= cross(camRight, camUp);	    		// The forward-vector of the image plane

	vec2 q = fragCoord.xy/iResolution.xy;
    vec2 uv = -1.0+2.0*q;

    vec3 ang = vec3(0.0,0,sin(iGlobalTime * 0.5));
    if(iMouse.z > 1.0) ang = vec3(0.0,-1.0*iMouse.y*0.001,iMouse.x*0.01);
    mat3 rot = getRotMatrix(ang);

	vec3 ro = camPos;
	vec3 rd = normalize((camForward * focalLength) + (camRight * uv.x * g_aspectRatio) + (camUp * uv.y));

    ro *= rot;
    rd *= rot;

    vec3 col = render( ro, rd );
    
    
    fragColor=vec4( col, 1.0 );
}

