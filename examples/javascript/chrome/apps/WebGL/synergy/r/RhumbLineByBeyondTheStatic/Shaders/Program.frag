/*
	Rhumb line
	2015 BeyondTheStatic

	Credits:
		- isectSphere() contains code modified from the POV-Ray docs
		- c_log() is from: https://raw.githubusercontent.com/julesb/glsl-util/master/complexvisual.glsl
*/
const float	Zoom		= 4.0;	// camera zoom
const float LThk		= .02;	// rhumb line thickness
const float GThk		= .02;	// grid line thickness
const float	GridFreq	= 10.0;	// grid frequency
const vec2	LnOrig		= vec2(-.1, 0.1); // line origin (polar coords, y=0.0==equator)

float s, c;
#define rotate(p, a) mat2(c=cos(a), s=-sin(a), -s, c) * p
#define PI 3.14159265

void rotXY(inout vec3 p, vec2 r) {
    p.yz = rotate(p.yz, r.x);
    p.xz = rotate(p.xz, r.y);
}

float grid(in vec2 p){ return 2. * min(abs(fract(p.x)-.5), abs(fract(p.y)-.5)); }

// complex log function
vec2 c_log(vec2 a) {
    float rpart = length(a);
    float ipart = atan(a.y, a.x);
    //if (ipart > PI) ipart -= 2. * PI;
    return vec2(log(rpart), ipart);
}

// returns both near & far distances
vec2 isectSphere(vec3 p, vec3 d, vec3 sPos, float sRad) {
    vec2 ret;
    vec3 v   = p - sPos;
    float r  = sRad;
    float dv = dot(d, v);
    float d2 = dot(d, d);
    float sq = dv*dv - d2 * (dot(v, v)-r*r);
    if(sq < 0.) {
        return vec2(-1.);
    }
    else {
    	sq = sqrt(sq);
        float t1 = (-dv+sq)/d2;
    	float t2 = (-dv-sq)/d2;
    	return (t1<t2 ? vec2(t1, t2) : vec2(t2, t1));
    }
}

vec4 getSphereCol(vec3 p) {
    float time	= PI/2. - .33 * iGlobalTime;
    float rep	= sin(time);
    float fl, fg;
    
    // globe mapping
    p.z  += 1.;
    p    /= dot(p, p);
    p.yx = (c_log(p.xy+vec2(0.,.5))-c_log(p.xy-vec2(0.,.5)))/PI/2.;
    
    // line UVs
    vec2 lp	= p.xy - LnOrig;
    vec2 l	= rotate(lp, time);
    if(rep != 0.)
        l.y = mod(l.y-.5*rep, rep) - .5*rep;
    
    // line, dot, grid
    fl =	max(0., 1.-abs(l.y)/LThk);
    fl =	max(fl, 2.*max(0., 1.-length(lp)/LThk));
	fg =	max(0., 1.-grid(fract(GridFreq*p.xy)-.5)/(2.*GridFreq*GThk));
    
	return vec4(mix(fg*vec3(0., .5, .5), vec3(1., 1., 0.), fl), 1.-max(fl, fg));
}

void mainImage(out vec4 fragColor, in vec2 fragCoord) {
	vec2 res	= iResolution.xy;
    vec2 uv		= (fragCoord-.5*res) / res.y;
	vec2 mPos	= (iMouse.xy-.5*res) / res.y;
    
    vec2 nav = (iMouse.z>0. ? 4.*mPos.yx : vec2(-.5, .1));
    
    // beginning of ray, direction of ray
    vec3 rayBeg	= vec3(.0, .0, -10.);
    vec3 rayDir	= normalize(vec3(uv, Zoom));
    rotXY(rayBeg, nav);
    rotXY(rayDir, nav);
    
    // get two sphere intersections
    vec2 hitDists = isectSphere(rayBeg, rayDir, vec3(0.), 1.);
    vec3 hit1 = rayBeg + rayDir * hitDists.x;
    vec3 hit2 = rayBeg + rayDir * hitDists.y;
    
    // final color...
    vec4 RGBA = vec4(0.,0.,0.,1.);
    if(hitDists.x>0.) {
        RGBA.rgb = getSphereCol(hit1).rgb;
    	RGBA.rgb = mix(RGBA.rgb, getSphereCol(hit2).rgb/(1.+2.*(hitDists.y-hitDists.x)), getSphereCol(hit1).a);
	}
	fragColor = RGBA;
}