#define pixProj(p) sqrt(dot(dFdx(p),dFdx(p)) + dot(dFdy(p),dFdy(p)))
struct Ray
{
	vec3 origin;
	vec3 direction;
};

struct Material
{
	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
	float shin;
};

struct Esfera
{
	vec3 center;
	float r;
	int id;
	Material m;
};

struct Plane
{
	vec3 p0;
	vec3 n;
	int id;
	Material m;
};

struct Light
{
	vec3 position;
	vec3 diffuse;
	vec3 specular;
	float cntAt, linAt, quaAt;
};

struct HitInfo
{
	vec3 hitPos;
	vec3 hitNrm;
	float dist;
	int obj;
	Material m;
};

Material mb = Material(
	vec3(0.0), 
	vec3(0.0), 
	vec3(0.0), 
	0.);

Material mee = Material(
	vec3(0.2), vec3(0.9,0.3, 0.1),
	vec3(0.9), 120.);

Material me = Material(
	vec3(0.2, 0.2, 0.2),
	vec3(0.3, 0.8,0.3),
	vec3(0.9, 0.9, 0.9),
	120.);

Material mp = Material(
	vec3(0.5, 1., 1),
	vec3(1, 1,1),
	vec3(0.0),
	0.);

Esfera es[2];

Plane P = Plane(
	vec3(0.0, -3.0,0.0),
	vec3(0.0, 1.0, 0.0),
	1, mp);

Light L = Light(
	vec3(30.0, 90.0, 50.0),
	vec3(1.0, 1.0, 1.0),
	vec3(1.0, 1.0, 1.0),
	0.,1.,0.03);
Ray getRays(Ray R, HitInfo hit, out Ray refracted, out bool internalRefl);
bool ray_esfera(Ray R, Esfera E, out HitInfo hit);
bool ray_plane(Ray R, Plane P, out HitInfo hit);
vec3 local(HitInfo hit, Light L, Material m);
bool intersect(Ray R, out HitInfo Info);
bool shadow(HitInfo Info, Light L);
float fbm(vec3 p);
vec3 trace(Ray R);


vec3 trace(Ray R)
{
    vec3 resolution = iResolution;
	vec3 col = vec3(0.);
	HitInfo info = HitInfo(vec3(0.0), vec3(0.0), 1000.0, -1, mb);
	HitInfo refl2, refr;
	Ray temp = R, refracted;
	bool c;
	if(intersect(temp, info))
			col = local(info, L, info.m);
	if(info.obj != -1)
	{
		col += -0.5 + smoothstep(.3, .6, fbm(info.hitPos));	
		Ray refl = getRays(temp, info, refracted, c);
		if(intersect(refl, info))
			col += 0.1*local(info, L, info.m);
		temp = refl;
	}
	else
	{
		vec2 p = gl_FragCoord.xy/resolution.xy;
		col = vec3(0.0, 0.0, .4);
		col += fbm(vec3(gl_FragCoord.xy/resolution.xy, 1.0))
			     + 0.4*fbm(vec3(gl_FragCoord.yx/resolution.xy, 0.0));
		float dist = 0.1*distance(p, vec2(0.0));
		col *= exp(-dist*sin(iGlobalTime));
	}	
	return col;

}


void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec3 resolution = iResolution;
	vec2 uv = gl_FragCoord.xy/resolution.xy;
	uv = 2.0*uv - 1.0;
	uv.x *= resolution.x/resolution.y;
	vec2 m = iMouse.xy/resolution.xy;
    m = 2.0*m - 1.0;
    m.x *= resolution.x/resolution.y;
	es[0] = Esfera(
	vec3(3.0, -1.0, 0.0),
	2.0, 0, mee);

	es[1] = Esfera(
	vec3(-3.0, -1.0, -5.0),
	2.0, 0, me);
	
	vec3 at = vec3(0.0);
	vec3 eye = vec3(6.*2.*sin(0.5*iGlobalTime), 5, 10);
	vec3 look = normalize(at - eye);
	vec3 up = vec3(0.0, 1.0, 0.0);
	vec3 ww = cross(look, up);
	vec3 vv = cross(ww, look);
	vec3 dx = tan(radians(30.0))*ww;
	vec3 dy = tan(radians(30.0))*vv;
    eye.xy *= abs(m.xy);
	Ray R = Ray(eye, normalize(look + dx*uv.x + dy*uv.y));
	vec3 col = trace(R);
	fragColor = vec4(col, 1.0);

}


mat3 m = mat3( 0.00,  0.80,  0.60,
              -0.80,  0.36, -0.48,
              -0.60, -0.48,  0.64 );

float hash( float n )
{
    return fract(sin(n)*43758.5453);
}

float noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);

    f = f*(3.0-2.0*f);

    float n = p.x + p.y*57.0 + 113.0*p.z;

    float res = mix(mix(mix( hash(n+  0.0), hash(n+  1.0),f.x),
                        mix( hash(n+ 57.0), hash(n+ 58.0),f.x),f.y),
                    mix(mix( hash(n+113.0), hash(n+114.0),f.x),
                        mix( hash(n+170.0), hash(n+171.0),f.x),f.y),f.z);
    return res;
}

float fbm( vec3 p )
{
    float f = 0.0, s = .5, t= 0.;

    f += s*noise( p ); t+=s; p = m*p*2.02; s*= .5; if(pixProj(p)> 1.) return f/t;
    f += s*noise( p ); t+=s; p = m*p*2.03; s*= .5; if(pixProj(p)> 1.) return f/t;
    f += s*noise( p ); t+=s; p = m*p*2.01; s*= .5; if(pixProj(p)> 1.) return f/t;
    f += s*noise( p ); t+= s;

    return f/t;
}

bool shadow(HitInfo Info, Light L)
{
	vec3 lightv = normalize(L.position - Info.hitPos); 
	Ray sh = Ray(Info.hitPos, lightv);
	HitInfo hit = HitInfo(vec3(0.0), vec3(0.0), 1000.0, -1, mb);
	if(intersect(sh, hit))
	{
		float distToLight = distance(Info.hitPos, L.position);
		float distToObj = distance(Info.hitPos, hit.hitPos);
		if(distToLight > distToObj)
			return true;	
	}
	return false;
}

Ray getRays(Ray R, HitInfo hit, out Ray refracted, out bool internalRefl)
{
	Ray reflected;
	vec3 dir = reflect(R.direction, hit.hitNrm);
	reflected = Ray(hit.hitPos, normalize(dir));
	vec3 nl = hit.hitNrm;
	float nls = dot(hit.hitNrm, R.direction);
	if(nls > 0.0) nl *= -1.;
	float nc = 1.0, nt = 1.5;
	float eta = nt/nc;
	bool into = dot(nl, hit.hitNrm) > 0.;
	float ddn = dot(R.direction, nl);
	if(into)
		eta = nc/nt;
	float c2 = 1.0 - eta*eta*(1.0 - ddn*ddn);
	if(c2 < 0.0)
	{
		internalRefl = true;
		refracted = Ray(vec3(0.0), vec3(0.0));
	}
	else
	{
		internalRefl = false;
		refracted = Ray(hit.hitPos, normalize(refract(R.direction, hit.hitNrm, eta)));
	}
	return reflected;
}

bool intersect(Ray R, out HitInfo Info)
{
	HitInfo hit = HitInfo(vec3(0.0), vec3(0.0), 10000.0, -1, mb);
	HitInfo hitz;
	for(int i = 0; i < 2; i++)
		if(ray_esfera(R ,es[i], hitz))
            if(hitz.dist < hit.dist)
				hit = hitz;
	if(ray_plane(R, P, hitz))
		if(hitz.dist < hit.dist)
			hit = hitz;
	Info = hit;
	if(hit.obj != -1)
		return true;
	return false;
}

vec3 getTexture(HitInfo hitInfo)
{
	float size = 2.0;
	vec3 p = hitInfo.hitPos/size;
    p = vec3(sin(3.14*hitInfo.hitPos/size));
    float s = pixProj(p);
    return vec3(0.3)*smoothstep(-s,s,p.x*p.y*p.z);
}

vec3 local(HitInfo hit, Light L, Material m)
{
	vec3 col = vec3(0.0);
	vec3 lightv = L.position - hit.hitPos;
	float dist = length(lightv);
	lightv = normalize(lightv);
	float attenuation = 900.0/(L.cntAt + L.linAt*dist + L.quaAt*dist*dist);
	vec3 amb = attenuation*m.diffuse*m.ambient;
	vec3 dif = attenuation*m.diffuse*L.diffuse*dot(lightv, hit.hitNrm);	
	float s = dot(lightv, hit.hitNrm);
	vec3 spec = vec3(0.0);
	if(s > 0.0)
		spec = pow(max(0.0, s), m.shin)*m.specular*L.specular;
	col = amb + dif + spec;
	if(shadow(hit, L))
		col = vec3(0.);
	if(hit.obj == 1)
		col *= getTexture(hit);
	return col;
}

bool ray_esfera(Ray R, Esfera E, out HitInfo hit)
{
	vec3 or = R.origin - E.center;
	vec3 dir = R.direction;
	float a = dot(dir, dir);
	float b = 2.0*dot(or, dir);
	float c = dot(or, or) - E.r*E.r;
	float delt = b*b - 4.*a*c;
	if(delt > 0.0)
	{
		float t0 = -b -sqrt(delt);
		float t1 = -b +sqrt(delt);
		float t = min(max(0.0, t0), max(0.0, t1));
		t /= 2.0*a;
		if(t > 0.001)
		{
			hit.hitPos = R.origin + t*R.direction;
			hit.hitNrm = normalize(hit.hitPos - E.center);
			hit.obj = E.id;
			hit.dist = t;
			hit.m = E.m;
			return true;
		}
	}
	return false;
}

bool ray_plane(Ray R, Plane P, out HitInfo hit)
{
	float t = dot(P.p0 - R.origin, P.n)/dot(R.direction, P.n);
	if(t > 0.001 && t < 70.0)
	{
		hit.hitPos = R.origin + t*R.direction;
		hit.hitNrm = P.n;
		hit.dist = t;
		hit.obj = P.id;
		hit.m = P.m;
		return true;
	}
	return false;
}