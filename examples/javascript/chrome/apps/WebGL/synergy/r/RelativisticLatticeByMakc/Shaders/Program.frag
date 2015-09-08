// Shadertoy version of http://makc.github.io/misc/relativity-1.html

// What you are looking at here is the giant cubic lattice with the
// side of ten light seconds. Click or hold the mouse button to pick
// rocket velocity (0 at the bottom, 0.95c at the top) and the camera
// direction. The result is what the rocket pilot would actually see
// in his(her) window - also known as relativistic aberration. Funny
// shit happens at high velocities, e g you begin to see objects' far
// side - for visual explanation, see 2nd video here:
// http://people.physics.anu.edu.au/~cms130/TEE/site/tee/learning/aberration/aberration.html

// Ok, to the code...

// butt first - the rocket from https://www.shadertoy.com/view/ltjGD1

float pi = 3.14159265359;

void angularRepeat(const float a, inout vec2 v)
{
    float an = atan(v.y,v.x);
    float len = length(v);
    an = mod(an+a*.5,a)-a*.5;
    v = vec2(cos(an),sin(an))*len;
}

void angularRepeat(const float a, const float offset, inout vec2 v)
{
    float an = atan(v.y,v.x);
    float len = length(v);
    an = mod(an+a*.5,a)-a*.5;
    an+=offset;
    v = vec2(cos(an),sin(an))*len;
}

float mBox(vec3 p, vec3 b)
{
	return max(max(abs(p.x)-b.x,abs(p.y)-b.y),abs(p.z)-b.z);
}

vec2 frot(const float a, in vec2 v)
{
    float cs = cos(a), ss = sin(a);
    vec2 u = v;
    v.x = u.x*cs + u.y*ss;
    v.y = u.x*-ss+ u.y*cs;
    return v;
}

void rotate(const float a, inout vec2 v)
{
    float cs = cos(a), ss = sin(a);
    vec2 u = v;
    v.x = u.x*cs + u.y*ss;
    v.y = u.x*-ss+ u.y*cs;
}

float dfRocketBody(vec3 p)
{
    vec3 p2 = p;
    vec3 pWindow = p;
    
    angularRepeat(pi*.25,p2.zy);
    float d = p2.z;
    d = max(d, frot(pi*-.125, p2.xz+vec2(-.7,0)).y);
    d = max(d, frot(pi*-.25*.75, p2.xz+vec2(-0.95,0)).y);
    d = max(d, frot(pi*-.125*.5, p2.xz+vec2(-0.4,0)).y);
    d = max(d, frot(pi*.125*.25, p2.xz+vec2(+0.2,0)).y);
    d = max(d, frot(pi*.125*.8, p2.xz+vec2(.55,0)).y);
    d = max(d,-.8-p.x);
    d -= .5;
    
    pWindow -= vec3(.1,.0,.0);
    angularRepeat(pi*.25,pWindow.xy);
    pWindow -= vec3(.17,.0,.0);
    d = min(d,mBox(pWindow,vec3(.03,.2,.55)));
    
  	return d;
}

float dfRocketFins(vec3 p)
{
    vec3 pFins = p;
    angularRepeat(pi*.5,pFins.zy);
    pFins -= vec3(-1.0+cos(p.x+.2)*.5,.0,.0);
    rotate(pi*.25,pFins.xz);
    float scale = 1.0-pFins.z*.5;
    float d =mBox(pFins,vec3(.17,.03,3.0)*scale)*.5;
    return d;
}

float df(vec3 p)
{
    float proxy = mBox(p,vec3(2.5,.8,.8));
    if (proxy>1.0)
    	return proxy;
    return min(dfRocketBody(p),dfRocketFins(p));
}

vec3 nf(vec3 p)
{
    vec2 e = vec2(0,0.005);
    return normalize(vec3(df(p+e.yxx),df(p+e.xyx),df(p+e.xxy)));
}


void rocket (inout vec3 color, in vec3 pos, in vec3 dir) {
    
    float dist,tdist = .0;
    
    for (int i=0; i<100; i++)
    {
     	dist = df(pos);
       	pos += dist*dir;
        tdist+=dist;
        if (dist<0.000001||dist>20.0)break;
    }

    vec3 materialColor = vec3(.0);
    vec3 orangeColor = vec3(1.5,.9,.0);
    
    float dRocketBody = dfRocketBody(pos);
    float dRocketFins = dfRocketFins(pos);
    float dRocket = min(dRocketBody, dRocketFins);
    
    
    float r = pow (length(pos.yz), 1.5);

    vec3 normal = nf(pos);
    
    if (dfRocketBody(pos)<dfRocketFins(pos))
    {
        if (pos.x<-.85)
            if (pos.x<-1.30)
                materialColor = orangeColor + vec3(0.03 / r);
            else
                materialColor = vec3(.9,.1,.1);
            else
            {
                if (pos.x>1.0)
                    materialColor = vec3 (.6,.1,.1);
                else
                    materialColor = vec3(.6);
            }
    }
    else
    {
        materialColor = vec3(.9,.1,.1);
        if (length (pos - 0.1 * vec3(0.0, normal.yz)) > length (pos)) { 

            materialColor -= vec3(1.5,.9,.0) * min(0.0, pos.x + 1.3) / r;
        }
    }
    
    float ao = df(pos+normal*.125)*8.0 +
        df(pos+normal*.5)*2.0 +
    	df(pos+normal*.25)*4.0 +
    	df(pos+normal*.06125)*16.0;
    
    ao=ao*.125+.5;
    
    if (dist<1.0) color = ao * materialColor;
}

// second, the beams from https://www.shadertoy.com/view/MllXDB

const float beam_half_side = 0.1, cell_side = 10.0, beam_side = beam_half_side * 2.0;

void beams (inout vec3 color, in vec3 p, in vec3 d) {
    p = mod (p + cell_side * 0.5, cell_side);
    vec3 ad = abs(d), sd = sign(d), normal = vec3(0.0);
    vec3 offsets = (0.5 * cell_side - beam_half_side) * (sd + 1.0) + beam_half_side;
    vec2 test;
    float closest_hit_distance = 1e2 * cell_side, ray_distance, step = cell_side / max (ad.x, max (ad.y, ad.z));
    vec3 p1 = p, p2, dist;
    float p1_to_p_distance = 0.0;
    for (int i = 0; i < 10; i++) {
        dist = (offsets - mod (p1, cell_side)) / d;
        dist += 0.5 * (1.0 - sign (dist)) * cell_side / ad;

        p2 = p1 + d * dist.x;
        ray_distance = p1_to_p_distance + dist.x;
        if (closest_hit_distance > ray_distance) {
            test = mod (p2.yz + beam_half_side, cell_side);
            if ((test.x < beam_side) || (test.y < beam_side)) {
                closest_hit_distance = ray_distance;
                normal = vec3 (-sd.x, 0.0, 0.0);
            }
        }

        p2 = p1 + d * dist.y;
        ray_distance = p1_to_p_distance + dist.y;
        if (closest_hit_distance > ray_distance) {
            test = mod (p2.xz + beam_half_side, cell_side);
            if ((test.x < beam_side) || (test.y < beam_side)) {
                closest_hit_distance = ray_distance;
                normal = vec3 (0.0, -sd.y, 0.0);
            }
        }

        p2 = p1 + d * dist.z;
        ray_distance = p1_to_p_distance + dist.z;
        if (closest_hit_distance > ray_distance) {
            test = mod (p2.xy + beam_half_side, cell_side);
            if ((test.x < beam_side) || (test.y < beam_side)) {
                closest_hit_distance = ray_distance;
                normal = vec3 (0.0, 0.0, -sd.z);
            }
        }

        p1 += d * step;
        p1_to_p_distance += step;
    }

    color = (vec3 (dot (normal, vec3(0.1, 0.2, 0.3))) * 0.5 + 0.5) * (cell_side / closest_hit_distance);

    p1 = p + closest_hit_distance * d;
    color *= mod (floor ((p1.x + p1.y + p1.z) * 4.0), 2.0) * 0.5 + 0.5;
}

// third, camera utility - this calculates ray direction in spaceship frame

vec3 ray_dir (float fov, vec2 size, vec2 pos, float angle) {
    vec2 xy = pos - 0.5 * size;

    float cot_half_fov = tan ((90.0 - 0.5 * fov) * 0.01745329252);	
    float z = size.y * 0.5 * cot_half_fov;

    vec3 dir = vec3 (xy, -z);
    mat3 rot = mat3 (
        vec3 (cos (angle), 0.0, -sin (angle)),
        vec3 (        0.0, 1.0,          0.0),
        vec3 (sin (angle), 0.0,  cos (angle))
    );

    dir = rot * dir;

    #ifdef tilted
    dir = vec3 (0.7071*(dir.x + dir.y), 0.7071*(dir.x - dir.y), dir.z);
    #endif

    return normalize (dir);
}

// stuff to show the speed (by unknown author at glslsandbox.com)

float extract_bit(float n, float b);
float sprite(float n, float w, float h, vec2 p);
float digit(float num, vec2 p);

// combining the shit together...

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    float angle = 2.0 * pi * iMouse.x / iResolution.x;

    // map 0 .. 1 to 0 .. ~0.95 with more detail in upper end
    float velocity = (1.0 - exp (-pow (iMouse.y / iResolution.y, 0.7))) / 0.665;
    
    if (length (iMouse) == 0.0) {
        float t = iGlobalTime;
        if (iResolution.y < 200.0) t-= 10.0;
        angle = -0.2 * t;
        velocity = 0.85;
    }
    
    // 1st, calculate how much distance did the spaceship travel in lattice frame
    // in units where c = 1 we have:
    // spaceship time ^2 - 0 ^2 = distant time ^2 - covered distance ^2
    // and
    // covered distance = velocity * distant time
    // ergo...
    float inverse_root = inversesqrt (1.0 - velocity * velocity);
    float covered_dist = inverse_root * velocity * iGlobalTime;

    // 2nd, pick the ray in spaceship frame
    vec3 ray = ray_dir (75.0, iResolution.xy, fragCoord.xy, angle);

    // 3rd, transform the ray to distant observer frame (aka Lorentz transformation)
    // since the ray is normalized, its length divided by c is 1
    vec3 transformed_ray = vec3 (ray.xy,
         inverse_root * (ray.z + velocity));
    
    vec3 pos = vec3 (0.0, 0.0, -covered_dist);
    transformed_ray = normalize (transformed_ray);

    beams (fragColor.xyz, pos, transformed_ray);

    // now let's add the rocket on top of it
    pos.x = -5.0 * cos (angle);
    pos.y =  2.1;
    pos.z =  5.0 * sin (angle);
    rotate (.5 * pi, ray.zx);
    
    vec3 r_color = vec3 (0.0);
    rocket (r_color, pos, ray);
    
    if (length (r_color) > 0.0) {
        fragColor.xyz = r_color;
    }
    
    // finally, display the speed
    vec2 uv = fragCoord.xy/iResolution.xy;
    fragColor.xyz += vec3 (
		sprite(2., 3., 5., floor (uv * vec2(64.0, 32.0) + vec2( 11.0 -64.0, -1.0))) +
		digit(velocity * 10.0, floor (uv * vec2(64.0, 32.0) + vec2( 8.0 -64.0, -1.0))) +
		digit(velocity * 100.0, floor (uv * vec2(64.0, 32.0) + vec2( 4.0 -64.0, -1.0)))
    );
}

//returns 0/1 based on the state of the given bit in the given number
float extract_bit(float n, float b)
{
	n = floor(n);
	b = floor(b);
	b = floor(n/pow(2.,b));
	return float(mod(b,2.) == 1.);
}

float sprite(float n, float w, float h, vec2 p)
{
	float bounds = float(all(lessThan(p,vec2(w,h))) && all(greaterThanEqual(p,vec2(0,0))));
	return extract_bit(n,(2.0 - p.x) + 3.0 * p.y) * bounds;
}

//3x5 digit sprites stored in "15 bit" numbers
/*
███     111
  █     001
███  -> 111  -> 111001111100111 -> 29671
█       100
███     111
*/

float c_0 = 31599.0;
float c_1 = 9362.0;
float c_2 = 29671.0;
float c_3 = 29391.0;
float c_4 = 23497.0;
float c_5 = 31183.0;
float c_6 = 31215.0;
float c_7 = 29257.0;
float c_8 = 31727.0;
float c_9 = 31695.0;

float digit(float num, vec2 p)
{
	num = mod(floor(num),10.0);
	
	if(num == 0.0) return sprite(c_0, 3., 5., p);
	if(num == 1.0) return sprite(c_1, 3., 5., p);
	if(num == 2.0) return sprite(c_2, 3., 5., p);
	if(num == 3.0) return sprite(c_3, 3., 5., p);
	if(num == 4.0) return sprite(c_4, 3., 5., p);
	if(num == 5.0) return sprite(c_5, 3., 5., p);
	if(num == 6.0) return sprite(c_6, 3., 5., p);
	if(num == 7.0) return sprite(c_7, 3., 5., p);
	if(num == 8.0) return sprite(c_8, 3., 5., p);
	if(num == 9.0) return sprite(c_9, 3., 5., p);
	
	return 0.0;
}