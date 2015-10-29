vec2 rotate(in vec2 uv, float a)
{
    a = radians(a);
	return (vec2(uv.x * cos(a) - uv.y * sin(a), uv.x * sin(a) + uv.y * cos(a)));
}

bool isInCircle(vec2 uv, vec2 c, float r)
{
    return length(uv - c) <= r;
}

bool isOnCircle(vec2 uv, vec2 c, float r)
{
    return isInCircle(uv, c, r) && length(uv - c) > r - 0.005;
}

bool isNeedle(vec2 no, float w, float h, float a, float offset)
{
    no = rotate(no, a);
    w /= 2.0;
    return (no.x > -w && no.x < w && no.y > offset && no.y - offset < h);
}

#define V(X, Y) vec2(X.0, Y.0)

struct CHAR
{
    bool l00;bool l01;bool l02;bool l03;bool l04;bool l05;bool l06;
    bool l10;bool l11;bool l12;bool l13;bool l14;bool l15;bool l16;
    bool l20;bool l21;bool l22;bool l23;bool l24;bool l25;bool l26;
    bool l30;bool l31;bool l32;bool l33;bool l34;bool l35;bool l36;
    bool l40;bool l41;bool l42;bool l43;bool l44;bool l45;bool l46;
    bool l50;bool l51;bool l52;bool l53;bool l54;bool l55;bool l56;
    bool l60;bool l61;bool l62;bool l63;bool l64;bool l65;bool l66;
};
    
CHAR zero = CHAR(
    false,	true,	true,	true,	true,	true,	false,
    true, 	false, 	false, 	false, 	false, 	true,	true,
    true, 	false, 	false, 	false, 	true, 	false ,	true,
    true, 	false, 	false, 	true, 	false, 	false, 	true,
    true, 	false, 	true, 	false, 	false, 	false, 	true,
    true, 	true, 	false, 	false, 	false, 	false, 	true,
    false, 	true, 	true, 	true, 	true, 	true, 	false
);

CHAR one = CHAR(
    false, 	false, 	false, 	false,	true, 	false, 	false,
    false, 	false, 	false, 	true,	true, 	false, 	false,
    false, 	false, 	true, 	false,	true, 	false ,	false,
    false, 	false, 	false, 	false, 	true, 	false, 	false,
    false, 	false, 	false, 	false, 	true, 	false, 	false,
    false, 	false, 	false, 	false, 	true, 	false, 	false,
    false, 	false, 	true, 	true, 	true, 	true, 	true
);

CHAR two = CHAR(
	false, 	false, 	true, 	true, 	true, 	false, 	false,
    false, 	true, 	false, 	false, 	false, 	true, 	false,
    false, 	false, 	false, 	false, 	false ,	true, 	false,
    false, 	false, 	false, 	false, 	true, 	false, 	false,
    false, 	false, 	false, 	true, 	false, 	false, 	false,
    false, 	false, 	true, 	false, 	false, 	false, 	false,
    false, 	true, 	true, 	true, 	true, 	true, 	false
);

CHAR three = CHAR(
	false, 	false, 	true, 	true, 	true, 	false, 	false,
    false, 	true, 	false, 	false, 	false, 	true, 	false,
    false, 	false, 	false, 	false, 	false ,	true, 	false,
    false, 	false, 	false, 	true, 	true, 	false, 	false,
    false, 	false, 	false, 	false, 	false, 	true, 	false,
    false, 	true, 	false, 	false, 	false, 	true, 	false,
    false, 	false, 	true, 	true, 	true, 	false, 	false
);

CHAR four = CHAR(
    false, 	false, 	false, 	false, 	false, 	true, 	false,
    false, 	false, 	false, 	false, 	true ,	true, 	false,
    false, 	false, 	false, 	true, 	false, 	true, 	false,
    false, 	false, 	true, 	false, 	false, 	true, 	false,
    false, 	true, 	false, 	false, 	false, 	true, 	false,
    false, 	true, 	true, 	true, 	true, 	true, 	true,
    false, 	false, 	false, 	false, 	false, 	true, 	false
);

CHAR five = CHAR(
    false, 	true, 	true, 	true, 	true, 	true, 	false,
    false, 	true, 	false, 	false, 	false ,	false, 	false,
    false, 	true, 	false, 	false, 	false, 	false, 	false,
    false, 	false, 	true, 	true, 	true, 	true, 	false,
    false, 	false, 	false, 	false, 	false, 	false, 	true,
    false, 	true, 	false, 	false, 	false, 	false, 	true,
    false, 	false, 	true, 	true, 	true, 	true, 	false
);

CHAR six = CHAR(
    false, 	false, 	true, 	true, 	true, 	true, 	false,
    false, 	true, 	false, 	false, 	false ,	false, 	false,
    false, 	true, 	false, 	false, 	false, 	false, 	false,
    false, 	false, 	true, 	true, 	true, 	true, 	false,
    false, 	true, 	false, 	false, 	false, 	false, 	true,
    false, 	true, 	false, 	false, 	false, 	false, 	true,
    false, 	false, 	true, 	true, 	true, 	true, 	false
);

CHAR seven = CHAR(
    false, 	true, 	true, 	true, 	true, 	true, 	true,
    false, 	false, 	false, 	false, 	false, 	false, 	true,
    false, 	false, 	false, 	false, 	false ,	true, 	false,
    false, 	false, 	false, 	false, 	true, 	false, 	false,
    false, 	false, 	false, 	true, 	false, 	false, 	false,
    false, 	false, 	true, 	false, 	false, 	false, 	false,
    false, 	true, 	false, 	false, 	false, 	false, 	false
);

CHAR eight = CHAR(
    false, 	false, 	true, 	true, 	true, 	true, 	false,
    false, 	true, 	false, 	false, 	false ,	false, 	true,
    false, 	true, 	false, 	false, 	false, 	false, 	true,
    false, 	false, 	true, 	true, 	true, 	true, 	false,
    false, 	true, 	false, 	false, 	false, 	false, 	true,
    false, 	true, 	false, 	false, 	false, 	false, 	true,
    false, 	false, 	true, 	true, 	true, 	true, 	false
);

CHAR nine = CHAR(
    false, 	false, 	true, 	true, 	true, 	true, 	false,
    false, 	true, 	false, 	false, 	false ,	false, 	true,
    false, 	true, 	false, 	false, 	false, 	false, 	true,
    false, 	false, 	true, 	true, 	true, 	true, 	true,
    false, 	false, 	false, 	false, 	false, 	false, 	true,
    false, 	true, 	false, 	false, 	false, 	false, 	true,
    false, 	false, 	true, 	true, 	true, 	true, 	false
);

CHAR colon = CHAR(
    false, 	false, 	false, 	false, 	false, 	false, 	false,
    false, 	false, 	false, 	true, 	false, 	false, 	false,
    false, 	false, 	false, 	false, 	false, 	false, 	false,
    false, 	false, 	false, 	false, 	false, 	false, 	false,
    false, 	false, 	false, 	false, 	false, 	false, 	false,
    false, 	false, 	false, 	true, 	false, 	false, 	false,
    false, 	false, 	false, 	false, 	false, 	false, 	false
);

CHAR num[11];

vec3 drawChar(vec2 xy, vec2 pos, CHAR c, vec3 c1, vec3 c2, float height)
{
    vec2 p = (pos + height - xy) / (height / 7.0);
	p.x = 7.0 - p.x;
    
    if (p.y < 0.0 || p.x < 0.0) return c1;
	if (p.x < 1.0 && p.y < 1.0) return (c.l00 ? c2 : c1);
	if (p.x < 2.0 && p.y < 1.0) return (c.l01 ? c2 : c1);
	if (p.x < 3.0 && p.y < 1.0) return (c.l02 ? c2 : c1);
	if (p.x < 4.0 && p.y < 1.0) return (c.l03 ? c2 : c1);
	if (p.x < 5.0 && p.y < 1.0) return (c.l04 ? c2 : c1);
	if (p.x < 6.0 && p.y < 1.0) return (c.l05 ? c2 : c1);
    if (p.x < 7.0 && p.y < 1.0) return (c.l06 ? c2 : c1);
	if (p.x < 1.0 && p.y < 2.0) return (c.l10 ? c2 : c1);
	if (p.x < 2.0 && p.y < 2.0) return (c.l11 ? c2 : c1);
	if (p.x < 3.0 && p.y < 2.0) return (c.l12 ? c2 : c1);
	if (p.x < 4.0 && p.y < 2.0) return (c.l13 ? c2 : c1);
	if (p.x < 5.0 && p.y < 2.0) return (c.l14 ? c2 : c1);
	if (p.x < 6.0 && p.y < 2.0) return (c.l15 ? c2 : c1);
	if (p.x < 7.0 && p.y < 2.0) return (c.l16 ? c2 : c1);
    if (p.x < 1.0 && p.y < 3.0) return (c.l20 ? c2 : c1);
	if (p.x < 2.0 && p.y < 3.0) return (c.l21 ? c2 : c1);
	if (p.x < 3.0 && p.y < 3.0) return (c.l22 ? c2 : c1);
	if (p.x < 4.0 && p.y < 3.0) return (c.l23 ? c2 : c1);
	if (p.x < 5.0 && p.y < 3.0) return (c.l24 ? c2 : c1);
	if (p.x < 6.0 && p.y < 3.0) return (c.l25 ? c2 : c1);
	if (p.x < 7.0 && p.y < 3.0) return (c.l26 ? c2 : c1);
    if (p.x < 1.0 && p.y < 4.0) return (c.l30 ? c2 : c1);
	if (p.x < 2.0 && p.y < 4.0) return (c.l31 ? c2 : c1);
	if (p.x < 3.0 && p.y < 4.0) return (c.l32 ? c2 : c1);
	if (p.x < 4.0 && p.y < 4.0) return (c.l33 ? c2 : c1);
	if (p.x < 5.0 && p.y < 4.0) return (c.l34 ? c2 : c1);
	if (p.x < 6.0 && p.y < 4.0) return (c.l35 ? c2 : c1);
	if (p.x < 7.0 && p.y < 4.0) return (c.l36 ? c2 : c1);
    if (p.x < 1.0 && p.y < 5.0) return (c.l40 ? c2 : c1);
	if (p.x < 2.0 && p.y < 5.0) return (c.l41 ? c2 : c1);
	if (p.x < 3.0 && p.y < 5.0) return (c.l42 ? c2 : c1);
	if (p.x < 4.0 && p.y < 5.0) return (c.l43 ? c2 : c1);
	if (p.x < 5.0 && p.y < 5.0) return (c.l44 ? c2 : c1);
	if (p.x < 6.0 && p.y < 5.0) return (c.l45 ? c2 : c1);
	if (p.x < 7.0 && p.y < 5.0) return (c.l46 ? c2 : c1);
    if (p.x < 1.0 && p.y < 6.0) return (c.l50 ? c2 : c1);
	if (p.x < 2.0 && p.y < 6.0) return (c.l51 ? c2 : c1);
	if (p.x < 3.0 && p.y < 6.0) return (c.l52 ? c2 : c1);
	if (p.x < 4.0 && p.y < 6.0) return (c.l53 ? c2 : c1);
	if (p.x < 5.0 && p.y < 6.0) return (c.l54 ? c2 : c1);
	if (p.x < 6.0 && p.y < 6.0) return (c.l55 ? c2 : c1);
	if (p.x < 7.0 && p.y < 6.0) return (c.l56 ? c2 : c1);
    if (p.x < 1.0 && p.y < 7.0) return (c.l60 ? c2 : c1);
	if (p.x < 2.0 && p.y < 7.0) return (c.l61 ? c2 : c1);
	if (p.x < 3.0 && p.y < 7.0) return (c.l62 ? c2 : c1);
	if (p.x < 4.0 && p.y < 7.0) return (c.l63 ? c2 : c1);
	if (p.x < 5.0 && p.y < 7.0) return (c.l64 ? c2 : c1);
	if (p.x < 6.0 && p.y < 7.0) return (c.l65 ? c2 : c1);
    if (p.x < 7.0 && p.y < 7.0) return (c.l66 ? c2 : c1);
    return (c1);
}

void init_nums()
{
    num[0] = zero;
    num[1] = one;
    num[2] = two;
    num[3] = three;
    num[4] = four;
    num[5] = five;
    num[6] = six;
    num[7] = seven;
    num[8] = eight;
    num[9] = nine;
    num[10] = colon;
}

vec3 putNbr(vec2 uv, vec2 p, float size, vec3 c, vec3 c2, float n)
{
    if (c.y != 0.0)
        return c;
    float div = 1.0;
    if (n > 0.0)
        n = -n;
    for (float d=1.0; d > 0.0; ++d)
    {
        if (floor(n / div) > -10.0)
            break;
        div *= 10.0;
    }
    vec3 color = c;
    for (float d=1.0; d > 0.0; d++)
    {
        int N = int(mod(abs(n / div), 10.0));
        for (int i = 0; i < 10; ++i)
        {
            if (i == N)
            {
                color = drawChar(uv, p, num[i], c, c2, size);
            	break;
            }
        }
        if (div == 1.0 || color.y != 0.0)
            break;
		div /= 10.0;
        p.x += size;
    }
    return color;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    //init_nums(); // wtf, it work without init tab and it's more fluid ????????????????????
    vec2 xy = fragCoord.xy;
	vec2 uv = 2.0 * fragCoord.xy / iResolution.xy - 1.0;
    uv.x = uv.x * iResolution.x / iResolution.y;
    vec3 c = vec3(0.0);
    vec3 c2 = vec3(0,1,0);
    float s = mod(iDate.w, 60.0);
    float m = mod(iDate.w / 60.0, 60.0);
    float h = mod(iDate.w / 3600.0, 12.0);
    if (isNeedle(uv, 0.03, 0.6, m * 360.0 / 60.0, 0.0))
        c = vec3(0.0, 0.0, 1.0);
    if (isNeedle(uv, 0.03, 0.4, h * 360.0 / 12.0, 0.0))
        c = vec3(0.0, 1.0, 0.0);
   	if (isNeedle(uv, 0.02, 0.75, floor(floor(s) * 360.0 / 60.0), 0.0))
  		c = vec3(1.0, 0.0, 0.0);
	for (float i = 0.0; i < 360.0; i += 6.0)
    {
        float m = mod(i, 30.0);
        float l = m > -0.01 && m < 0.01 ? 0.2 : 0.1;
    	if (isNeedle(uv, 0.025, l, i, 1.0 - l))
    	    c = vec3(1.0);
    }
    if (xy.x > iResolution.x - 148.0 && xy.y < 22.0)
    {
    	c = drawChar(xy, vec2(iResolution.x - 63.0, 0), num[10], c, c2, 21.0);
   		c = drawChar(xy, vec2(iResolution.x - 126.0, 0), num[10], c, c2, 21.0);
    	if (s < 10.0)
    		c = putNbr(xy, vec2(iResolution.x - 42.0, 0), 21.0, c, c2, 0.0);
    	if (m < 10.0)
    		c = putNbr(xy, vec2(iResolution.x - 105.0, 0), 21.0, c, c2, 0.0);
    	c = putNbr(xy, vec2(iResolution.x - (s < 10.0 ? 21.0 : 42.0), 0), 21.0, c,c2, s);
    	c = putNbr(xy, vec2(iResolution.x - (m < 10.0 ? 84.0 : 105.0), 0), 21.0, c, c2, m);
    	c = putNbr(xy, vec2(iResolution.x - 147.0, 0), 21.0, c, c2, h);
    }
    fragColor = vec4(c, 1.0);
}