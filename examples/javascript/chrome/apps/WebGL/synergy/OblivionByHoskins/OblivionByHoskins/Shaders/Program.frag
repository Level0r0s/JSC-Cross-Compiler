// [SIG15] Oblivion
// by David Hoskins.
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

// The Oblivion drone. I love all the scenes with these things in the film.
// This took the entire week to do - all 45 seconds worth of it. :)  
// You might need to rewind to sync the audio properly.
// Try it full sceen!

#define PI 3.14156
#define MOD2 vec2(443.8975,397.2973)
#define MOD3 vec3(443.8975,397.2973, 491.1871)

const vec2 add = vec2(1.0, 0.0);
vec3 sunDir = normalize(vec3(-2.3, 3.4, -5.89));
const vec3 sunCol = vec3(1.0, 1.0, .9);
vec2 gunMovement;
vec3 drone;
vec3 droneRots;
float scannerOn;
vec4 dStack;
vec4 eStack;
int emitionType = 0;

//----------------------------------------------------------------------------------------
// A hash that's the same on all platforms...
vec3 hash32(vec2 p)
{
	vec3 p3 = fract(vec3(p.xyx) * MOD3);
    p3 += dot(p3.zxy, p3.yxz+19.19);
    return fract(vec3(p3.x * p3.y, p3.x*p3.z, p3.y*p3.z));
}

//----------------------------------------------------------------------------------------
vec3 hash31(float p)
{
   vec3 p3 = fract(vec3(p) * MOD3);
   p3 += dot(p3.xyz, p3.yzx + 19.19);
   return fract(vec3(p3.x * p3.y, p3.x*p3.z, p3.y*p3.z));
}

//----------------------------------------------------------------------------------------
float hash12(vec2 p)
{
	p  = fract(p * MOD2);
    p += dot(p.xy, p.yx+19.19);
    return fract(p.x * p.y);
}

//----------------------------------------------------------------------------------------
vec3 noise3(float n)
{
    float f = fract(n);
    n = floor(n);
    f = f*f*(3.0-2.0*f);
    return mix(hash31(n), hash31(n+1.0), f);
}

//----------------------------------------------------------------------------------------
vec3 noise( in vec2 x )
{
    vec2 p = floor(x);
    vec2 f = fract(x);
    f = f*f*(1.5-f)*2.0;
    
    vec3 res = mix(mix( hash32(p), hash32(p + add.xy),f.x),
                    mix( hash32(p + add.yx), hash32(p + add.xx),f.x),f.y);
    return res;
}

//----------------------------------------------------------------------------------------
float circle(vec2 p, float s )
{
    return length(p)-s;
}
//----------------------------------------------------------------------------------------
float  sphere(vec3 p, float s )
{
    return length(p)-s;
}

float prism( vec3 p, vec2 h )
{
    vec3 q = abs(p);
    return max(q.x-h.y,max(q.z*0.6+p.y*.5,-p.y)-h.x*0.5);
}

//----------------------------------------------------------------------------------------
float prismFlip( vec3 p, vec2 h )
{
    vec3 q = abs(p);
    return max(q.x-h.y,max(q.z*.8-p.y*.5,p.y)-h.x*0.5);
}

//----------------------------------------------------------------------------------------
float roundedSquare( vec2 p, vec2 b)
{
  vec2 d = abs(p) - b;
  return min(max(d.x,d.y),0.0) + length(max(d,0.0));
    
}//----------------------------------------------------------------------------------------
float roundedBox( vec3 p, vec3 b, float r )
{
	return length(max(abs(p)-b,0.0))-r;
}

//----------------------------------------------------------------------------------------
float sMin( float a, float b, float k )
{
    
	float h = clamp(0.5 + 0.5*(b-a)/k, 0.0, 1.0 );
	return mix( b, a, h ) - k*h*(1.-h);
}

//----------------------------------------------------------------------------------------
vec2 rot2D(inout vec2 p, float a)
{
	float si = sin(a);
	float co = cos(a);
	return mat2(co, si, -si, co) * p;
}

//----------------------------------------------------------------------------------------
vec3 rot3DXY(in vec3 p, in vec2 a)
{
	vec2 si = sin(a);
	vec2 co = cos(a);
    p.xz *= mat2(co.y, -si.y, si.y, co.y);
    p.zy *= mat2(co.x, -si.x, si.x, co.x);
    return p;
}

//----------------------------------------------------------------------------------------
float boxMap( sampler2D sam, in vec3 p, in vec3 n)
{
    p = p*vec3(.1, .03, .1);
    n = abs(n);
	float x = texture2D( sam, p.yz ).y;
	float y = texture2D( sam, p.zx ).y;
	float z = texture2D( sam, p.xy ).y;
	return (x*n.x + y*n.y + z*n.z)/(n.x+n.y+n.z);
}

//----------------------------------------------------------------------------------------
vec4 numbers(vec4 mat, vec2 p)
{
    p.y *= 1.70;
    p.y+=.36;
	float d;
	d =(roundedSquare(p+vec2(1.4, -.32), vec2(.02, .8)));
    vec2 v = p;
    v.x -= v.y*.6;
    v.x = abs(v.x+.17)-.75;
	d = min(d, roundedSquare(v+vec2(0, -.7), vec2(.043, .4)));
    v = p;
    v.x -= v.y*.6;
    v.x = abs(v.x-.234)-.75;
    p.x = abs(p.x-.395)-.75;
  	d = min(d, circle(p, .5));
   	d = max(d, -circle(p, .45));
    d = max(d, -roundedSquare(v+vec2(0., -.85), vec2(.35, .9)));
    
    mat = mix(mat, vec4(.8), smoothstep(0.2, .15, d));
    return mat;
}


//----------------------------------------------------------------------------------------
// Find the drone...
float mapDE(vec3 p)
{
    p -= drone.xyz;
    p = rot3DXY(p, droneRots.xy);

    float d = sphere(p, 10.0);
	vec3 v = p;
    v.xy = abs(v.xy);
    v.xy = rot2D(v.xy, -PI/6.2);
    // Cross pieces...
    d = sMin(d, roundedBox(v-vec3(0,0,-8), vec3(4.9, .3, .5), 1.), 1.2); 
    d = max(d, -roundedBox(v-vec3(0,0,-8.5), vec3(4.8, .3, 1.), 1.));
    
    // Centre cutout...
    //d = sMin(d, roundedBox(p-vec3(0,0,-8.5), vec3(1.3, 1.4, 1.5), .7), .4); 
    d = max(d,-roundedBox(p-vec3(0,0,-9.1), vec3(2., 1.5, 4.0), .7)); 
    // Inside...
    d = min(d, sphere(p, 8.8));
    d = max(d, roundedBox(p, vec3(6.5, 12, 12.0), .8)); 
    // Make back...
    d = sMin(d, prismFlip(p+ vec3(.0, -4.1, -8.1), vec2(7., 4.7) ), 1.);
    d = max(d, -prism(p + vec3(.0, 6.4, -11.4), vec2(8.0, 10.0) ));
    d = min(d, sphere(p+ vec3(.0, 5.6, -6.2), 3.0));
    
    // Eye locations../
    d = min(d, sphere(v+ vec3(-3.5, .0, 7.4), 1.1));
    
    v = p;
    v.x = abs(v.x);
    d = sMin(d, roundedBox(v+vec3(-4.2,-6.,-10.0), vec3(1.1, .1, 4.5), 1.), 2.4); 
    
     
    // Gun placements and carriers...
   //p.yz = rot2D(p.yz, gunMovement.x);
    v =abs(p)-vec3(gunMovement.x, .0, 0.) ;
    v.x -= p.z*.1*gunMovement.y;
	float d2 = sphere(v, 10.0);
    d2 = max(d2, -roundedBox(v, vec3(6.55, 12, 12.0), .8)); 
    d = min(d2 ,d);
    d = min(d,roundedBox(v-vec3(5.5, 3.5, 3.5), vec3(2.3, .1, .1), .4));
    d = min(d,roundedBox(v-vec3(5.5, .0, 5.), vec3(2.4, .1, .1), .4));

    v =vec3(abs(p.xy)-vec2(gunMovement.x, .0), p.z);
    v.x -= p.z*.1*gunMovement.y;

    d = min(d, roundedBox(v-vec3(8., 2.8, -6.5), vec3(.3, 1., 3.), .2));
    d = min(d, roundedBox(v-vec3(8., 2.3, -10.), vec3(.2, .4, 1.2), .2));
    d = min(d, roundedBox(v-vec3(8., 3.4, -10.), vec3(.01, .01, 1.2), .4));
    d = max(d, -roundedBox(v-vec3(8., 3.4, -10.4), vec3(.01, .01, 1.2), .3));
    d = max(d, -roundedBox(v-vec3(8., 2.3, -10.4), vec3(.01, .01, 1.2), .3));
    
   
    return d;
}

//----------------------------------------------------------------------------------------
// Find the drone's material...yes, it's IFtastic! :D
vec4 mapCE(vec3 p, vec3 nor)
{
    vec4 mat;
    p -= drone.xyz;
	p = rot3DXY(p, droneRots.xy);

    const vec4 gunMetal = vec4(.05, .05, .05,.3);
    vec4 body     = vec4(.8, .8, .8,.4);
   
    //    //float dirt1 = smoothstep(-.1, .5,boxMap(iChannel1,p, nor))*.2+.8;
	float dirt1 = .8;
    mat = body*dirt1;
  
    float d = sphere(p+vec3(0,0,.5), 8.9);
    float d2;
    d = max(d, roundedBox(p, vec3(6., 12, 11.0), .72)); 
    if (d < .0 || p.z > 14.5)
    {
        d = sphere(p-vec3(-3.3 , 1.8, -8.1), .9);
        d2 = sphere(p-vec3(3.1 , 1.7, -8.1), .5);
        // EyeCam...
	    if (d < 0.0)
        {
            mat = vec4(1., 0.03, 0.0, .7);
            emitionType = 1;
        }else
		// Scanner...
       	if (d2 < 0.0)
       	{
			mat = vec4(.2, 0.2+scannerOn, 0.2+scannerOn, .3);
			emitionType = 2;
      	}
        else
	        mat = numbers(gunMetal, p.xy);
        return mat;
    }

     // Gun placements and carriers...
    vec3 v = p;
    
   	//v.yz = rot2D(p.yz, gunMovement.x);
	v =abs(v)-vec3(gunMovement.x, .0, 0.) ;
    v.x -= p.z*.1*gunMovement.y;
	d2 = sphere(v, 10.0);
    d2 = max(d2, -roundedBox(v, vec3(6.55, 12, 4.0), 1.1)); 
    
    d = min(d2, d);
    d2 = min(d,	roundedBox(v-vec3(5.5, 3.5, 3.5), vec3(2.3, .1, .1), .4));
    //d2 = min(d2,roundedBox(v-vec3(5.5, .0, 3.7), vec3(2.3, .1, .1), .4));
    d2 = min(d2, sphere(v-vec3(5., .0, 3.7), 3.8));
    if(d2 < d) mat = vec4(.0, .0, .0, 6.);
    //return mat;
    
    v = vec3(abs(p.x)-gunMovement.x, p.yz);
    v.x -= p.z*.1*gunMovement.y;
    float dirt = .8;
    body = body * dirt;
 
    v = vec3(abs(p.xy)-vec2(gunMovement.x, .0), p.z);
    v.x -= p.z*.1*gunMovement.y;
    
    if ( v.x > 7.5)  mat =mix(body, gunMetal, smoothstep(2.2, 2., v.y))*dirt;
    d2 =  roundedBox(v-vec3(8.1, 2.3, -10.5), vec3(.4, 1.6, 1.5), .2);
    if ( d2 < 0.1)  mat = gunMetal*dirt;
    
    d =  sphere(p+ vec3(.0, 5.6, -6.2), 3.1);
    if ( d < 0.0)
    {
        mat = vec4(0);
        emitionType = 3;
    }

    return mat;
}

//----------------------------------------------------------------------------------------
float shadow( in vec3 ro, in vec3 rd)
{
	float res = 1.0;
    float t = .2;


    for (int i = 0; i < 12; i++)
	{
		float h = mapDE( ro + rd*t );
        if (h< -2.) break;
		res = min(10.*h / t, res);
		t += h;
	}
    return max(res, .3);
}

//----------------------------------------------------------------------------------------
float SphereRadius(in float t)
{
    t = t*.006;
	return min(t,256.0/iResolution.x);
}

//----------------------------------------------------------------------------------------
void rayMarch(vec3 pos, vec3 dir)
{
    // Efficiently start the ray just in front of the drone...
    float l = max(length(drone-pos)-13.0, -1.0);
    float d =  l;
    l+=21.0;// ...and end it just after
    int hits = 0;
	// Collect 4 of the closest scrapes on the tracing sphere...
    for (int i = 0; i < 50; i++)
    {
        // Leave if it's gone past the drone or when it's found 8 stacks points...
        if(d > l || hits == 8) break;
        vec3 p = pos + dir * (d);
		float r= SphereRadius(d);
        //heatHaze(p, dir);
		float de = mapDE(p);
        // Only store the closest ones (roughly), which means we don't
        // have to render the 8 stack points, just the most relavent ones.
        // This also prevents the banding seen when using small stacks.
        if(de < r &&  de < eStack.x)
        {
            // Rotate the stack!...
			dStack = dStack.wxyz; dStack.x = d; 
            eStack = eStack.wxyz; eStack.x = de;
			hits++;    
        }
		d +=de;
    }
    return;
}

vec3 normal( in vec3 pos, in float r )
{
	vec2 eps = vec2( r*.5, 0.0);
	vec3 nor = vec3(
	    mapDE(pos+eps.xyy) - mapDE(pos-eps.xyy),
	    mapDE(pos+eps.yxy) - mapDE(pos-eps.yxy),
	    mapDE(pos+eps.yyx) - mapDE(pos-eps.yyx) );
	return normalize(nor);
}

float terrain( in vec2 q )
{
    q *= .5;
    q += 4.;
    return  0.023*q.x;
}

//----------------------------------------------------------------------------------------
vec3 skyUpper(in vec3 rd)
{
    vec3  sky;
    float f = pow(max(rd.y, 0.0), .5);
    sky = mix(vec3(.45, .5, .6), vec3(.8, .8, .8), f);
    float sunAmount = pow(max( dot( rd, sunDir), 0.0 ), 2.0);
    sky = sky + sunCol * sunAmount*.5;
    rd.xz = rd.zx;
   // sky -= (1.0-textureCube(iChannel3, rd).xyz)*.3;
	return clamp(sky, 0.0, 1.0);
}

#define F t+=texture2D(iChannel2,.3+ro.xz*s/6e3,-99.).rgb/s;s+=s;

vec3 fogIt(in vec3 col, in vec3 sky, in float d)
{
    return mix (col, sky, 1.0-exp(-d*0.001));
}

vec3 ground(vec3 sky, in vec3 rd, in vec3 pos)
{
  
    if (rd.y > .0) return sky;
 
	float d = -20.0/rd.y;
	vec2 p = pos.xz+rd.xz * d;
    
	vec3 tex1 = texture2D(iChannel1, p*.1).yyy;
	vec3 tex2 = texture2D(iChannel2, p*.001).xxx*vec3(1.0, .8, .8);

	vec3 gro  = vec3(1.);
    
    d-=20.0;
	float a = .0001*d*d;
        
	vec3 nor  	= vec3(0.0,		    terrain(p), 0.0);
	vec3 v2		= nor - vec3(a,		terrain(p+vec2(a, 0.0)), 0.0);
	vec3 v3		= nor - vec3(0.0,		terrain(p+vec2(0.0, a)), -a);
	nor = cross(v2, v3);
	nor = normalize(nor);
	gro = mix(tex1, tex2, nor.y*.8);
	float sha = shadow(vec3(p.x, 0.0, p.y),  sunDir);
	float z =max(dot(nor, sunDir), 0.1);
    if (dStack[0] < 0.0) dStack[0]= d;

	return gro*z*sha;
}

//----------------------------------------------------------------------------------------
// Action cue sheet, for easy manipulation...
#define cueFRONTOF 10.0
#define cueTHREAT 15.
#define cueFLYOFF 32.0

//----------------------------------------------------------------------------------------
// This is also used for the camera's delayed follow routine.
// Which make the scene more dramitic because it's a human camera operator!
vec3 dronePath(float ti)
{
    vec3 p = vec3(-2030, 340, 2000.0);
    p = mix(p, vec3(-30.0, 18.0, 300.0),		 smoothstep(0.0, 4.0, ti));
    p = mix(p, vec3(-35.0, 25.0, 10.0), 			smoothstep(4.0,8.0, ti));
    p = mix(p, vec3(30.0, 0.0, 15.0), 			smoothstep(cueFRONTOF+.5,cueFRONTOF+2.5, ti)); //../ Move to front of cam.
    
    p = mix(p, vec3(0.0, 8.0, .0), 				smoothstep(cueTHREAT, cueTHREAT+.5, ti)); 	// ...Threaten
    p = mix(p, vec3(0.0, 8.0, -4.0), 			smoothstep(cueTHREAT+2.0, cueTHREAT+2.3, ti)); 	// ...Threaten
    p = mix(p, vec3(0.0, 8., -12.0), 			smoothstep(cueTHREAT+3.0, cueTHREAT+3.3, ti)); 	// ...Threaten
    
    p = mix(p, vec3(0.0, 110.0, 0.0), 			smoothstep(cueFLYOFF,cueFLYOFF+1.5, ti)); // ...Fly off
    p = mix(p, vec3(4000.0, 110.0, -4000.0), 	smoothstep(cueFLYOFF+2.6,cueFLYOFF+10.0, ti)); 
    return p; 
}

//----------------------------------------------------------------------------------------
vec3 droneRotations(float ti)
{
    vec3 a;
    a.x = 0.;
    a.y = -.78;
    
    a.x = mix(a.x, .2,smoothstep(3.0,4.0, ti));
    a.x = mix(a.x, 0.,smoothstep(4.0,4.2, ti));

	a.y = mix(a.y, 0.0, smoothstep(4.0,4.4, ti));
    a.x = mix(a.x, .2,smoothstep(7.0,7.8, ti));
    a.x = mix(a.x, 0.,smoothstep(7.8,8.3, ti));
    
	a.y = mix(a.y, -1.5,smoothstep(cueFRONTOF,cueFRONTOF+.5, ti));// ..Turn to go right, infront
	a.y = mix(a.y, .6, 	smoothstep(cueFRONTOF+3.,cueFRONTOF+4.5, ti));

    a.y = mix(a.y, .0,  smoothstep(cueTHREAT,cueTHREAT+.5, ti));

    a.x = mix(a.x, -.28,smoothstep(cueTHREAT, cueTHREAT+.3, ti)); // ...Threaten
    
    a.x = mix(a.x, 0.0, smoothstep(cueFLYOFF-2.0, cueFLYOFF, ti)); // Normalise position, relax!
    a.x = mix(a.x, -0.5,smoothstep(cueFLYOFF, cueFLYOFF+.2, ti)); 	// ...Fly off
    a.x = mix(a.x, 0.0, smoothstep(cueFLYOFF+.2, cueFLYOFF+.7, ti));
    
    a.y = mix(a.y, -.78,smoothstep(cueFLYOFF+2., cueFLYOFF+2.3, ti)); 
//a.y= 2.2;
    return a;
}

//----------------------------------------------------------------------------------------
vec2 droneGunAni(float ti)
{
    vec2 a;
   	float mov = smoothstep(15.5, 16.5, ti);
    mov = mov * smoothstep(cueFLYOFF-.5, cueFLYOFF-3.0, ti);
    mov = mov*3.1-1.4;
    a.x = (sin(mov)+1.0)*1.5;
    a.y = smoothstep(.3,.7,sin(mov))*3.0;
    return a;
}
//----------------------------------------------------------------------------------------
float overlay(vec3 p, vec3 dir)
{
    float r = 0.0;
    vec3 pos = drone.xyz+vec3(3.25, -.48, -8.0);
    vec3 v = p-pos;
    vec3 n = vec3(0.0, 1., 0.0);
    n.zy = rot2D(n.zy, droneRots.z);
    n = normalize(n);
    float d = -dot(n, v)/ dot(n, dir);
    p = p + dir*d-pos;

    if (p.z < .0 && p.z > -20.)
    {
        float d = abs(p.z) - abs(p.x)+.4;
        r = step(.3, d)*.3;
        r += smoothstep(-.3, -.2,p.x) * smoothstep(0., -.2, p.x)*r;
        r += smoothstep(.3, .2,p.x) * smoothstep(0.0, .2, p.x)*r;
        r += smoothstep(0.1, .2, d) * smoothstep(0.4, .2, d);
    }
    r += smoothstep(0.3, 0.0,abs(droneRots.z-.4))*1.5;

    return r;
}

//----------------------------------------------------------------------------------------
void heatHaze(vec3 p, inout vec3 dir, float t)
{
    float r = 0.0;
    vec3 pos = vec3(0.0, -4.8, 7.);
    if (drone.y < 20.0)
    	pos.y += smoothstep(-.90, .5,droneRots.y)*smoothstep(.9, 0.5,droneRots.y)*-8.0;
    pos.zx = rot2D(pos.zx, droneRots.y);
    pos += drone.xyz;
    vec3 v = p-pos;
    vec3 n = vec3(0.0, 0., 1.0);

    n = normalize(n);
    float d = -dot(n, v)/ dot(n, dir);
    p = p + dir*d-pos;

    if (p.y < .0 && p.y > -30.)
    {
        float l = abs(p.y) - abs(p.x*1.5)+8.0;
        r = smoothstep(.0, 14., l);
        //p.xy *= vec2(.5,.9);
        t*= 23.0;
        dir += r*(noise(p.xy+vec2(0.0,t))-.5)*.001/(smoothstep(10.0, 5000.0, d)*300.0);
    }
}

//----------------------------------------------------------------------------------------
vec3 cameraLookAt(in vec2 uv, in vec3 pos, in vec3 target, in float roll)
{
    
	vec3 cw = normalize(target-pos);
	vec3 cp = vec3(sin(roll), cos(roll),0.0);
	vec3 cu = normalize(cross(cw,cp));
	vec3 cv = normalize(cross(cu,cw));
	return normalize(-uv.x*cu + uv.y*cv +2.*cw );
}

//----------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------
void mainImage( out vec4 outColour, in vec2 coords )
{
	vec2 xy = coords.xy / iResolution.xy;
    vec2 uv = (xy-.5)*vec2( iResolution.x / iResolution.y, 1)*2.0;
    float ti = iGlobalTime;
    //float ti = mod(iGlobalTime, 45.);
    //float ti = mod(iGlobalTime, 6.5)+cueFRONTOF-2.0;
    //float ti = mod(iGlobalTime, 24.5)+cueTHREAT;
    //float ti = mod(iGlobalTime, 10.5)+cueTHREAT+3.0;
    //float ti = mod(iGlobalTime, 8.5)+cueFLYOFF-2.5;
    
    
    // Steps back in fear...
    vec3 pos = vec3(0.0, 0.0, mix(-28.0, -40.0, smoothstep(cueTHREAT,cueTHREAT+4.0, ti)));
	  
	//---------------------------------------------------------
    // Annimations...
    drone = dronePath(ti);
    droneRots = droneRotations(ti);
    gunMovement = droneGunAni(ti);
    scannerOn = smoothstep(cueTHREAT+4.0,cueTHREAT+4.2, ti)* smoothstep(cueTHREAT+9.5,cueTHREAT+9.2, ti);
    droneRots.z = sin(ti*2.) * scannerOn;
    
    float t = smoothstep(cueTHREAT, cueTHREAT+.5, ti) *smoothstep(cueTHREAT+14.5, cueTHREAT+13.5, ti);
    float e = -droneRots.y+t*texture2D(iChannel0, vec2(.3, ti*.02)).x*.25-.25;
    vec3 eyeCam = normalize(vec3(0.3, -.4*t,  -1.0));
    eyeCam.xz = rot2D(eyeCam.xz, e);
	//---------------------------------------------------------
	vec3 tar = dronePath(ti-.25);
    // Cameraman gets nervous when the drone is close...
    float l = 30.0 / length(tar-pos);
    tar += (noise3(ti*4.0)-.5)*l;
    vec3 dir = cameraLookAt(uv, pos, tar, 0.0);
	
    //---------------------------------------------------------
    heatHaze(pos, dir, ti);
    // Reset and fill the render stack through ray marching...
    dStack = vec4(-1);
    eStack = vec4(1000.0);
    rayMarch(pos, dir);

    //---------------------------------------------------------
	// Use the last stacked value to do the shadow, seems to be OK, phew!...
    float lg = dStack[0];
	vec3 p = pos + dir * lg;
    float sha = shadow(p, sunDir);
    
	//---------------------------------------------------------
	// Render the stack...
    float alphaAcc = .0;
    vec3 col = vec3(0);
    float spe;
    for (int i = 0; i < 4; i++)
    {
        float d = dStack[i];
		if (d < 0.0) continue;
        float de = eStack[i];
        float s = SphereRadius(d);
        float alpha = max((1.0 - alphaAcc) * min(((s-de) / s), 1.0),0.0);
		alphaAcc+= alpha;
		vec3 p = pos + dir * d;
		vec3  nor = normal(p, s);
		vec4  mat = mapCE(p, nor);
		float amb = abs(nor.y)*.6; amb = amb*amb;
		vec3 c= mat.xyz * vec3(max(dot(sunDir, nor), 0.0))+ amb * mat.xyz;
		spe = pow(max(dot(sunDir, reflect(dir, nor)), 0.0), 18.0);
            
		if (emitionType != 0)
		{
			if (emitionType == 1)
			{
				d = cos(pow(max(dot(eyeCam, nor), 0.0), 4.4)*9.0)*.14;
				d += pow(dot(eyeCam, nor), 80.)*18.0;
				c*= max(d, 0.0);
	        }
            if (emitionType == 3)
			{
				d = smoothstep(.0,-.3, nor.y) * smoothstep(-1.0,-.3, nor.y);
				c+= smoothstep(.3,0.0, nor.y) * smoothstep(-.2,.1, nor.y)*.5;
				c+= d*(texture2D(iChannel2, p.xy*.01+vec2(0.0, ti*.8)).zxy)*4.0 * abs(sin(atan(nor.x, nor.z)*6.0)+.1);
              
            }

			sha = 1.0;
		}
        c += sunCol * spe * mat.w;
        col += c *sha* alpha;
     }
    
	//---------------------------------------------------------
    // Back drop...
    vec3 sky = skyUpper(dir);
    vec3 gro = ground(sky, dir, pos);
	col = mix(col, gro, 1.0-alphaAcc);
    col = fogIt(col, sky, dStack[0]);
    
    // Overlay...
    float scan = overlay(pos, dir)*scannerOn;

    col = min(col+vec3(scan*.4, scan, scan*.9), 1.0);
    
    
	//---------------------------------------------------------
	// Post effects...
    col = pow(col, vec3(.45)); 												// Gamma adjust
    col = clamp(col+hash32(xy*ti)*.1, 0.0, 1.0); 					// Random film noise
    col *= .6+0.4*pow(50.0*xy.x*xy.y*(1.0-xy.x)*(1.0-xy.y), 0.2 );	// Vignette
    col *= smoothstep(0.0, 1.5, ti)*smoothstep(45.0, 40., ti);
	outColour = vec4(col,1.0);
}
