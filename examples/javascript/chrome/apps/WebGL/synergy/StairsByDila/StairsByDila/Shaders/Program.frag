#define DUST_SAMPLES 0
#define WND_DIST 2.0
#define WND_WIDTH 3.5
#define WND_HEIGHT 3.0
#define WND_FREQ 8.0

const float pi = 3.14159265359;

mat3 xrot(float t)
{
    return mat3(1.0, 0.0, 0.0,
                0.0, cos(t), -sin(t),
                0.0, sin(t), cos(t));
}

mat3 yrot(float t)
{
    return mat3(cos(t), 0.0, -sin(t),
                0.0, 1.0, 0.0,
                sin(t), 0.0, cos(t));
}

mat3 zrot(float t)
{
    return mat3(cos(t), -sin(t), 0.0,
                sin(t), cos(t), 0.0,
                0.0, 0.0, 1.0);
}

float infBox( vec3 p, float b )
{
  	return max(abs(p.x)-b,0.0);
    //p.y -= p.z * 0.5;
    //return length(max(abs(p.xy)-vec2(b,10.0),0.0));
}

float udBox( vec3 p, vec3 b )
{
  return length(max(abs(p)-b,0.0));
}

float sdBox( vec3 p, vec3 b )
{
  vec3 d = abs(p) - b;
  return min(max(d.x,max(d.y,d.z)),0.0) +
         length(max(d,0.0));
}

vec3 post(vec3 pos, float t)
{
    //pos.x += sin(t*0.25) * 2.0;
    return pos;
}

vec3 glass(vec3 pos, vec3 size)
{
    //return vec3(1.0, 1.0, 0.0);
	vec2 uv = pos.zy / size.zy;
    uv.x *= WND_WIDTH / WND_HEIGHT;
    
    vec2 auv = abs(uv);
    
    if (auv.x > 1.0 || auv.y > 1.0) {
        //return vec3(1.0, 1.0, 0.5);
    }
    
    vec2 bv = fract(uv*5.0) * 2.0 - 1.0;
    vec2 fl = floor(uv*5.0);
    
    float lt = -0.8;
    if (bv.x<lt || bv.y<lt) {
    	return vec3(0.0);
    }
    
    vec3 col = vec3(cos(fl.x)*cos(fl.y),
                    sin(fl.x)*cos(fl.y),
                    sin(fl.y));
    
    col = col * 0.25 + 0.75;
    
	return col;
}

vec3 glassrep(vec3 pos)
{
    float wdist = 4.5;
    float sp = WND_FREQ;
    vec3 wps = pos;
    wps.y -= floor(wps.z/sp) * sp * 0.5;
    wps.z = fract(wps.z/sp)*sp;
    float wndx = abs(wps.x);
    vec3 wnd = vec3(wdist-wndx, wps.y-5.0, wps.z-4.0);
    return glass(wnd+vec3(0.0,WND_DIST,0.0), vec3(2.0,WND_HEIGHT,WND_WIDTH));
}

vec2 staircase(vec3 pos)
{
    vec3 spos = pos;
    vec3 lpos = vec3(spos.x,pos.y,spos.z);
    float d = 1000.0;
    for (int i = -1; i <= 1; ++i) {
        float repz = fract(-lpos.z) * 0.5 - 0.5 + float(i);
        float stepinc = floor(lpos.z);
        vec3 crep = vec3(lpos.x, (lpos.y*2.0-stepinc-float(i)), repz);
        float k = udBox(crep, vec3(6.0, 2.0, 1.0)*0.5);
        d = min(d, k);
    }
    return vec2(d,1.0);
}

bool alpha;

vec2 walls(vec3 pos)
{
    float wdist = 4.5;
    vec3 axp = vec3(wdist-abs(pos.x), pos.y, pos.z);
    float pl = infBox(axp, 1.0);
    
    float d = 1000.0;
    for (int i = -1; i <= 1; ++i) {
        float sp = WND_FREQ;
        vec3 wps = pos;
        wps.y -= floor(wps.z/sp) * sp * 0.5;
        wps.z = fract(wps.z/sp)*sp;
        wps.z += float(i) * sp;
        float wndx = wps.x;//abs(wps.x);
        vec3 wnd = vec3(wdist-wndx, wps.y-5.0, wps.z-4.0);
        float wp = sdBox(wnd+vec3(0.0,WND_DIST,0.0), vec3(2.0,WND_HEIGHT,WND_WIDTH));
        d = min(d, wp);
    }
    float fd = max(pl, -d);
    return vec2(fd, 2.0);
}

vec2 skybox(vec3 pos)
{
	float ps = abs(pos.y) - 20.0 - pos.z * 0.5;
    return vec2(-ps, 0.0);
}

vec2 pane(vec3 pos)
{
    if (!alpha) {
        float gl = 4.5 - abs(pos.x);
        return vec2(gl, 3.0);
    }
    return vec2(1000.0, 3.0);
}

float material(vec3 pos)
{
	vec2 sa = staircase(pos);
    vec2 wp = walls(pos);
    if (wp.x < sa.x) {
    	sa = wp;
    }
    wp = pane(pos);
    if (wp.x < sa.x)
    {
        sa = wp;
    }
	return sa.y;
}

float map(vec3 pos)
{
	vec2 sa = staircase(pos);
    vec2 wp = walls(pos);
	vec2 pn = pane(pos);
	return min(min(sa.x,wp.x),pn.x);
}

vec3 surfaceNormal(vec3 pos)
{
 	vec3 delta = vec3(0.01, 0.0, 0.0);
    vec3 normal;
    normal.x = map(pos + delta.xyz) - map(pos - delta.xyz);
    normal.y = map(pos + delta.yxz) - map(pos - delta.yxz);
    normal.z = map(pos + delta.zyx) - map(pos - delta.zyx);
    return normalize(normal);
}

float trace(vec3 o, vec3 r)
{
 	float t = 0.0;
    for (int i = 0; i < 64; ++i) {
        vec3 pos = o + r * t;
        float d = map(post(pos,t));
        if (abs(d)<0.0001) {
            break;
        }
        t += d * 0.5;
    }
    return t;
}

vec3 lightpos(vec3 world)
{
    //vec3 lps = vec3(10.0,0.0,0.0) * zrot(iGlobalTime);
    vec3 lps = vec3(10.0,4.0,0.0);
    return lps;
}

vec3 light(vec3 world, vec3 wsn)
{
    vec3 col = vec3(0.0);
    float lm = 1.0;
    vec3 lps = lightpos(world);
    vec3 lp = world + lps;
    vec3 ublv = world - lp;
    vec3 lv = ublv + wsn * 0.01;
    float ld = length(lv);
    lv /= ld;
    float lt = trace(lp, lv);
    if (lt >= ld) {
		vec3 plane = vec3(1.0, 0.0, 0.0);
        vec3 porg = vec3(3.5, 0.0, 0.0);
        vec3 del = porg - world;
        float x = dot(del, plane) / dot(normalize(ublv), plane);
		vec3 proj = world + lv * x;
        col = glassrep(proj);
    }
    return col;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xy;
    vec2 ns = uv;
    uv = uv * 2.0 - 1.0;
    uv.x *= iResolution.x / iResolution.y;
    
    vec3 r = normalize(vec3(uv, 1.0 - dot(uv,uv)*0.33)) * 0.95;
    
    //r *= xrot(-pi*0.2);
    //r *= yrot(iGlobalTime*0.25);
    r *= yrot(-pi*0.3);
    
    float gt = iGlobalTime * 1.0;
    
    vec3 o = vec3(0.0,1.0,2.0) * gt;
    
    float eve = abs(sin(gt*10.0))*0.25;
    o += vec3(0.0, 4.0, 0.0);
    
    alpha = false;
    float t = trace(o, r);
    
    vec3 world = o + r * t;
    vec3 wsn = surfaceNormal(post(world,t));
    float tp = max(dot(wsn,-r), 0.0);
    
    float fd = map(post(world,t));
    float fog = 1.0 / (1.0 + t * t * 0.01 + fd * 100.0);
    
    vec3 diff = vec3(0.0);
    
    if (material(world) == 3.0) {
		diff = glassrep(world) * 0.25;
        //vec3 ref = reflect(wsn, -r);
        //vec3 rtex = textureCube(iChannel1, ref).xyz;
        //diff = mix(diff, rtex, 1.0-tp);
    }
    
    alpha = true;
    
	vec3 lm = light(world, wsn);
    
    const int ni = DUST_SAMPLES;
    vec3 vol = vec3(0.0);
    for (int i = 0; i < ni; ++i) {
        vec3 rll = r * zrot(float(i)/float(ni)*pi*2.0);
        float xtex = atan(rll.z,rll.x);
        vec2 ltf = vec2(xtex,rll.y) * 1.0;
        float bs = texture2D(iChannel0, ltf).x;
        bs = min(pow(bs,2.0),1.0);
        float td = bs * min(t,20.0);
        float tds = 1.0 / (1.0 + td * 0.01);
        vec3 p = o + r * td;
        vec3 vl = light(p,vec3(0.0));
        vol += vl / float(ni) * tds;
    }
    
    float skyp = 1.0 / (1.0 + t * t * 1.0);
    
    vec3 lpos = lightpos(world);
    vec3 ldel = normalize(lpos - world);
    float ldn = abs(dot(ldel,-wsn));
    vec3 refv = reflect(ldel, wsn);
    float spd = max(dot(refv, -r), 0.0);
    
    diff += vec3(1.0) * pow(spd,4.0);
    
    vec3 fogc = vec3(255.0, 255.0, 255.0) / 255.0;
    
    //diff *= 0.25;
    
    vec3 fc = diff * fog;//mix(fogc, diff, fog);
    
    vec3 lit = lm * fog + vol;// * (1.0 + t * 0.05);
    
    fc += lit;
    
	fragColor = vec4(fc,1.0);
}