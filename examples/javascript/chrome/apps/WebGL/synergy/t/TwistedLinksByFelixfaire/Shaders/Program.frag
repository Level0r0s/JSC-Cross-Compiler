uniform float time;
uniform vec2 mouse;
uniform vec2 resolution;

const float pi = 3.141592;

const int steps = 128;
const float epsilon = 0.0001;
const vec3 skyColour = vec3( 0.1 );

float nearPlane = 2.0;
float farPlane = 4.5;

void rotateY( inout vec3 v, float t )
{
	v *= mat3(cos(t), 0.0, -sin(t),
                	0.0, 1.0, 0.0,
                	sin(t), 0.0, cos(t));
}

void rotateX( inout vec3 v, float t )
{
	v *= mat3(1.0, 0.0, 0.0,
                  0.0, cos(t), -sin(t),
                  0.0, sin(t), cos(t));
}

void repeatX( inout vec3 v, float amount )
{
	v.x = mod(v.x, amount) - amount*0.5;	
}

//repeat around y axis n times
void angleRepeatY(inout vec3 p, float n) {
	float w = 2.0*pi/n;
	float a = atan(p.z, p.x);
	float r = length(p.xz);
	a = mod(a+pi*.5, w)+pi-pi/n;
	p.xz = r*vec2(cos(a),sin(a));
}

float getDist( vec3 p )
{
	float plane = p.y + 0.4;
	
	rotateY( p, -iMouse.x * 0.004 + iGlobalTime * 0.2 );
	
	angleRepeatY(p, 5.0);
	
	rotateX( p, (p.x + 0.14) * 2.0);
	
	repeatX( p, 0.5 );
	
	float d = 0.0;
	float ring1 = 0.0;
	float ring2 = 1.0;
	
	float radius = 0.2;
	float sphere1 = length( p - vec3(0.0, 0.0, 0.0) ) - radius;
	float sphere2 = length( p - vec3(0.0, 0.0, radius*0.6) ) - radius;
	float sphere3 = length( p - vec3(-0.0, 0.0, -radius*0.6) ) - radius;
	
	rotateX( p, 1.6);
	p.x = mod( p.x, 0.5 ) - 0.25;
	
	float sphere4 = length( p - vec3(0.0, 0.0, 0.0) ) - radius;
	float sphere5 = length( p - vec3(0.0, 0.0, radius*0.6) ) - radius;
	float sphere6 = length( p - vec3(-0.0, 0.0, -radius*0.6) ) - radius;
	
	ring1 = max(sphere1, - sphere2);
	ring1 = max(ring1, -sphere3);
	ring2 = max(sphere4, - sphere5);
	ring2 = max(ring2, - sphere6);
	d = min( min(ring1, ring2), plane);
	
	return d;
}

vec3 getNormal( vec3 p ) 
{
	vec2 e = vec2(0.006, 0.0);
	return (vec3( getDist(p+e.xyy), getDist(p+e.yxy), getDist(p+e.yyx)) - getDist(p)) / e.x;
}

float angleBetween( vec3 a, vec3 b )
{
	return 1.0 - acos( dot(a, b) / (length(a) * length(b)) ) / pi;	
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	
	vec2 c = fragCoord.xy / iResolution.xy;
	c = c * 2.0 - 1.0;
	c.x *= iResolution.x / iResolution.y;
	c.x *= 0.9;
	vec3 col = vec3( 0.1 );
	
	vec3 o = vec3( 0.0, 1.0 + 0.4 * sin(iGlobalTime * 0.3), -2.0 );
	vec3 ro = vec3( c.x, c.y, 0.0 ) - o;
	
	ro = normalize(ro);
	
	float t = 0.0;
	for (int i = 0; i < steps; i++)
	{
		vec3 p = o + t * ro;
		if (length(t*ro) > farPlane) break;
		
		float d = getDist( p );
		
		if (d < epsilon)
		{
			float fog = smoothstep( nearPlane, farPlane, length(t*ro));
			float backLight = angleBetween( ro, getNormal( p ) );
			backLight *= backLight;
			col += backLight * 4.0;
			col = mix( col + 0.01*float(i) * vec3( 0.6, 0.6, 0.2), skyColour, fog );
			break;
		}
		
		t += d;
	}
	
	fragColor = vec4( col, 1.0 );
}
