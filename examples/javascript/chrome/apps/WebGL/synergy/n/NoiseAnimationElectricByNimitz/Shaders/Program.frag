//Noise animation - Electric
//by nimitz (stormoid.com) (twitter: @stormoid)

//The domain is displaced by two fbm calls one for each axis.
//Turbulent fbm (aka ridged) is used for better effect.

#define time iGlobalTime*0.15
#define tau 6.2831853

mat2 makem2(in float theta){float c = cos(theta);float s = sin(theta);return mat2(c,-s,s,c);}

float r(vec2 n)
{
    return fract(cos(dot(n,vec2(36.26,73.12)))*354.63);
}
float xxnoise(vec2 n)
{
    vec2 fn = floor(n);
    vec2 sn = smoothstep(vec2(0),vec2(1),fract(n));
    
    float h1 = mix(r(fn),r(fn+vec2(1,0)),sn.x);
    float h2 = mix(r(fn+vec2(0,1)),r(fn+vec2(1)),sn.x);
    return mix(h1,h2,sn.y);
}
float perlin(vec2 n)
{
    float total;
    total = xxnoise(n/32.)*0.5875+xxnoise(n/16.)*0.2+xxnoise(n/8.)*0.1+xxnoise(n/4.)*0.05+xxnoise(n/2.)*0.025+xxnoise(n)*0.0125;
 	return total;
}

float noise( in vec2 x ){return perlin(  x/0.04) ;}

float fbm(in vec2 p)
{	
	float z=2.;
	float rz = 0.;
	vec2 bp = p;
	for (float i= 1.;i < 6.;i++)
	{
		rz+= abs((noise(p)-0.5)*2.)/z;
		z = z*2.;
		p = p*2.;
	}
	return rz;
}

float dualfbm(in vec2 p)
{
    //get two rotated fbm calls and displace the domain
	vec2 p2 = p*.7;
	vec2 basis = vec2(fbm(p2-time*1.6),fbm(p2+time*1.7));
	basis = (basis-.5)*.2;
	p += basis;
	
	//coloring
	return fbm(p*makem2(time*0.2));
}

float circ(vec2 p) 
{
	float r = length(p);
	r = log(sqrt(r));
	return abs(mod(r*4.,tau)-3.14)*3.+.2;

}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	//setup system
	vec2 p = fragCoord.xy / iResolution.xy-0.5;
	p.x *= iResolution.x/iResolution.y;
	p*=4.;
	
    float rz = dualfbm(p);
	
	//rings
	p /= exp(mod(time*10.,3.14159));
	rz *= pow(abs((0.1-circ(p))),.9);
	
	//final color
	vec3 col = vec3(.2,0.1,0.4)/rz;
	col=pow(abs(col),vec3(.99));
	fragColor = vec4(col,1.);
}