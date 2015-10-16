mat2 rot(float t)
{
 	return mat2(cos(t), sin(t), -sin(t), cos(t));   
}

/* thanks to iq */
float udRoundBox( vec3 p, vec3 b, float r )
{
  return length(max(abs(p)-b,0.0))-r;
}

vec3 times()
{
    float gt = fract(iGlobalTime * 0.5) * 3.0;
    float a = clamp(gt - 0.0, 0.0, 1.0);
    float b = clamp(gt - 1.0, 0.0, 1.0);
    float c = clamp(gt - 2.0, 0.0, 1.0);
    return vec3(a, b, c);
}

float map(vec3 p)
{
    float height = 1.0;
    float ground = p.y + height;
    
    vec3 pt = times();
    float pound = 1.0 - pow(1.0-pt.y, 2.0) - pow(pt.z, 32.0);
    pound *= 2.0;
    
	float srot = smoothstep(0.0, 1.0, (pt.y+pt.z)*0.5);
    mat2 mrot = rot(-0.3 + srot * 3.14);
    
    vec3 boxoff = vec3(0.0, pound, 0.0);
    p.xz *= mrot;
    float box = udRoundBox(p - boxoff, vec3(height)*0.5, height*0.25);
 	return min(ground, box);
}

float trace(vec3 o, vec3 r)
{
 	float t = 0.0;
    for (int i = 0; i < 32; ++i) {
        vec3 p = o + r * t;
        float d = map(p);
        t += d * 0.5;
    }
    return t;
}

float rayplane(vec3 o, vec3 r, vec3 p, vec3 n)
{
	return dot(p - o, n) / dot(r, n);
}

vec3 texture(vec3 p)
{
	//vec3 ta = texture2D(iChannel2, p.xz).xyz;
    //vec3 tb = texture2D(iChannel2, p.yz).xyz;
    //vec3 tc = texture2D(iChannel2, p.xy).xyz;
    //return (ta*ta + tb*tb + tc*tc) / 3.0;

    	return vec3(0.7, 0.4, 0.1);
}

vec3 normal(vec3 p)
{
	vec3 o = vec3(0.01, 0.0, 0.0);
    return normalize(vec3(map(p+o.xyy) - map(p-o.xyy),
                          map(p+o.yxy) - map(p-o.yxy),
                          map(p+o.yyx) - map(p-o.yyx)));
}

vec3 smoke(vec3 o, vec3 r, vec3 f, float t)
{
    vec3 tms = times();
    vec3 sm = vec3(0.0);
    const int c = 32;
    float fc = float(c);
    for (int i = 0; i < c; ++i)
    {
        float j = float(i) / fc;
        float bout = 1.0 + tms.x;
        vec3 p = vec3(cos(j*6.28), 0.0, sin(j*6.28)) * bout;
        p.y = -1.0;
        float pt = rayplane(o, r, p, f);
        if (pt < 0.0) continue;
        if (pt > t)  continue;
        vec3 pp = o + r * pt;
        float cd = length(pp - p);
        vec2 uv = (pp - p).xy * 0.1 + vec2(j,j) * 2.0;
        //vec3 tex = texture2D(iChannel1, uv).xyz;
        //tex *= tex;
        vec3 tex = vec3(0.5, 0.5, 0.5);
        vec3 part = tex;
        part /= 1.0 + cd * cd * 10.0 * tms.x;
        part *= clamp(abs(t - pt), 0.0, 1.0);
        part /= 1.0 + pt * pt;
        part *= clamp(pt, 0.0, 1.0);
        sm += part;
    }
    sm *= 1.0 - smoothstep(0.0, 1.0, tms.x);
    return sm;
}

vec3 shade(vec3 o, vec3 r, vec3 f, vec3 w, float t)
{
    vec3 tuv = w;
    if (tuv.y > -0.85)
    {
        vec3 pt = times();
		float srot = smoothstep(0.0, 1.0, (pt.y+pt.z)*0.5);
    	mat2 mrot = rot(-0.3 + srot * 3.14);
        tuv.xz *= mrot;
        float pound = 1.0 - pow(1.0-pt.y, 2.0) - pow(pt.z, 32.0);
        pound *= 2.0;
        tuv.y -= pound;
    }
    vec3 tex = texture(tuv * 0.5);
    vec3 sn = normal(w);
	vec3 ground = vec3(1.0, 1.0, 1.0);
    vec3 sky = vec3(1.0, 0.9, 0.9);
    vec3 slight = mix(ground, sky, 0.5+0.5*sn.y);
    float aoc = 0.0;
    const int aocs = 8;
    for (int i = 0; i < aocs; ++i) {
        vec3 p = w - r * float(i) * 0.2;
        float d = map(p);
        aoc += d * 0.5;
    }
    aoc /= float(aocs);
    aoc = 1.0 - 1.0 / (1.0 + aoc);
    float fog = 1.0 / (1.0 + t * t * 0.01);
    vec3 smk = smoke(o, r, f, t);
    float fakeocc = 0.5 + 0.5 * pow(1.0 - times().y, 4.0);
    vec3 fc = slight * tex * aoc + smk * sky;
    fc = mix(fc * fakeocc, sky, 1.0-fog);
    return fc;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xy;
    uv = uv * 2.0 - 1.0;
    uv.x *= iResolution.x / iResolution.y;
    
    vec3 r = normalize(vec3(uv, 0.8 - dot(uv, uv) * 0.2));
    vec3 o = vec3(0.0, 0.125, -1.5);
    vec3 f = vec3(0.0, 0.0, 1.0);
    
    vec3 pt = times();
    
    float shake = pow(1.0 - pt.x, 4.0);
    vec3 smack = vec3(1.0, 1.0, 1.0);
    smack *= shake;
    
    o.x += smack.x * shake * 0.25;
    o.z += smack.y * shake * 0.1;
    
    mat2 smackrot = rot(0.3 + smack.z * shake * 0.1);
    r.xy *= smackrot;
    f.xy *= smackrot;
    
    float t = trace(o, r);
    vec3 w = o + r * t;
    float fd = map(w);
    
    vec3 fc = shade(o, r, f, w, t);
    
	fragColor = vec4(sqrt(fc), 1.0);
}