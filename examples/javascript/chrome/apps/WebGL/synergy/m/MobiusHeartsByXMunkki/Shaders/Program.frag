//
// See the finished object and more information at:
//    http://xmunkki.org/wiki/doku.php?id=projects:mobiushearts
//
// Based on Inigo Quilez raymarching tutorial at https://www.shadertoy.com/view/Xds3zN

#define PI 3.14159265

float sdPlane(vec3 p, float h)
{
	return p.y - h;
}

float sdCylinderY(vec3 p, vec2 h)
{
  vec2 d = abs(vec2(length(p.xz),p.y)) - h;
  return min(max(d.x,d.y),0.0) + length(max(d,0.0));
}

float sdCylinderZ(vec3 p, vec2 h)
{
  vec2 d = abs(vec2(length(p.xy),p.z)) - h;
  return min(max(d.x,d.y),0.0) + length(max(d,0.0));
}

//----------------------------------------------------------------------

float opS( float d1, float d2 )
{
    return max(d1, -d2);
}

float opU(float d1, float d2)
{
    return min(d1, d2);
}

float sminPoly( float a, float b, float k )
{
    float h = clamp( 0.5+0.5*(b-a)/k, 0.0, 1.0 );
    return mix( b, a, h ) - k*h*(1.0-h);
}

float opBlend(float d1, float d2)
{
    return sminPoly(d1, d2, 0.05);
}

// With extra information
vec2 opU2( vec2 d1, vec2 d2 )
{
	return (d1.x<d2.x) ? d1 : d2;
}

vec3 opRep( vec3 p, vec3 c )
{
    return mod(p,c)-0.5*c;
}

//----------------------------------------------------------------------
// Transforms on position to affect the model

vec3 posTranslate(vec3 p, vec3 offset)
{
    return p - offset;
}

vec3 posAxisAngle(vec3 p, vec3 axis, float angle)
{
    axis = normalize(axis);
    angle = -angle; // Inverse rotation for distance fields
    
    float c = cos(angle);
    float s = sin(angle);
    
    return
        c * p +
        s * cross(axis, p) +
        ((1.0 - c) * dot(axis, p)) * axis;
}

//----------------------------------------------------------------------

float sdCircle(vec3 p, float radius, float thickness, float width)
{
    float t_half = thickness * 0.5;

    float res = sdCylinderZ(p, vec2(radius + t_half, width * 0.5));
    res = opS(res, sdCylinderZ(p, vec2(radius - t_half, width)));
    return res;
}

float sdCircleRound(vec3 p, float radius, float thickness, float width)
{
    float t_half = thickness * 0.5;

    // Get the direction and center point for the circle border (z = 0)
    vec2 border_dir = normalize(p.xy);
	vec2 border_center = border_dir * radius;
    
    // Get the position of the projected 2D coordinate
    vec2 proj_pos = vec2(length(p.xy - border_center), p.z);
    
    // Calculate the rounded box distance
    float roundness = 0.02;
    vec2 box_size = vec2(t_half, width * 0.5);
    box_size = box_size.yx;
    return length(max(abs(proj_pos) - box_size, 0.0)) - roundness;
}

float sdCircleTopTwist(vec3 p, float radius, float thickness, float width, bool twist_orient_dir)
{
    // Do a half twist at the top
    if (p.y > 0.0)
    {
        vec3 twist_dir = normalize(vec3(p.x, p.y, 0.0));
        vec3 twist_center = twist_dir * radius;
        
        float amt_dir = dot(twist_dir, p - twist_center);
        float amt_z = p.z;

        // Map the angle range to a half twist
        float angle = atan(p.x, p.y);
        float angle_range = 0.5;
        angle_range = clamp((angle + angle_range) / (angle_range * 2.0), 0.0, 1.0);
        angle_range = smoothstep(0.0, 1.0, angle_range);
        angle = mix(0.0, (twist_orient_dir ? PI : -PI), angle_range);
        
        // Rotate the position along the twist position on the ring
        float as = sin(angle);
        float ac = cos(angle);
        
        float amt_a = ac * amt_dir - as * amt_z;
        float amt_b = ac * amt_z + as * amt_dir;
        
        vec3 new_p = twist_center +
            twist_dir * amt_a +
            vec3(0.0, 0.0, amt_b);
        
        // If still above, keep the rotation
        if (new_p.y > 0.0)
            p = new_p;
        
        //p.y -= abs(p.x) * 1.5;
    }
    
    float res = sdCircleRound(p, radius, thickness, width);
    return res;
}

float sdHeartOrigin(vec3 p, bool twist_dir)
{
    // Do a heart shape
    p.y -= pow(abs(p.x), 1.3) * 1.3;
    
    return sdCircleTopTwist(p, 0.5, 0.1, 0.15, twist_dir);
}

float sdHeart(vec3 p, bool twist_dir)
{
    p = posTranslate(p, vec3(0.0, 0.6, 0.0));
    p = posAxisAngle(p, vec3(1.0, 0.0, 0.0), -0.35); // Tilt (major)
    p = posAxisAngle(p, vec3(0.0, 0.0, 1.0), 0.15); // Tilt (side)
    p = posTranslate(p, vec3(-0.15, 0.0, 0.04));
    float res = sdHeartOrigin(p, twist_dir);
    return res;
}

float sdHearts(vec3 p)
{
    // Mirror by x axis
    vec3 p2 = vec3(-p.x, p.y, -p.z);
    
    return opU(sdHeart(p, false), sdHeart(p2, true));
}

float sdPlatform(vec3 p)
{
    float res = sdCylinderY(p, vec2(0.6, 0.1));
    res -= 0.07; // Soft edges
    
    return res;
}

float sdObject(vec3 p)
{
    float res = sdPlatform(p);
    res = opBlend(res, sdHearts(p));
    res = opS(res, sdPlane(p, 0.0)); // Cut out anything that goes through the platform
    return res;
}

//----------------------------------------------------------------------

vec2 map(in vec3 pos)
{
    vec2 res = vec2(sdPlane(pos, 0.0), 1.0);

    //res = opU2(res, vec2(sdCylinderY(pos - vec3(0,0.6,0), vec2(0.5, 0.5)), 2.0));
    //res = opU2(res, vec2(sdCylinderZ(pos - vec3(0,0.6,0), vec2(0.5, 0.5)), 2.0));
    //res = opU2(res, vec2(sdCircleRound(pos - vec3(0,0.6,0), 0.5, 0.1, 0.1), 128.0));
    //res = opU2(res, vec2(sdHeartOrigin(pos - vec3(0,0.6,0), false), 128.0));
    res = opU2(res, vec2(sdObject(pos), 128.0));
    
    // More accuracy (substepping)
    res.x *= 0.5;
    
    return res;
}

vec2 castRay(in vec3 ro, in vec3 rd)
{
    float tmin = 1.0;
    float tmax = 20.0;
    
#if 0
    float tp1 = (0.0-ro.y)/rd.y; if( tp1>0.0 ) tmax = min( tmax, tp1 );
    float tp2 = (1.6-ro.y)/rd.y; if( tp2>0.0 ) { if( ro.y>1.6 ) tmin = max( tmin, tp2 );
                                                 else           tmax = min( tmax, tp2 ); }
#endif
    
	float precis = 0.002;
    float t = tmin;
    float m = -1.0;
    for( int i=0; i<200; i++ )
    {
	    vec2 res = map( ro+rd*t );
        if( abs(res.x)<precis || t>tmax ) break;
        t += res.x;
	    m = res.y;
    }

    if( t>tmax ) m=-1.0;
    return vec2( t, m );
}


float softshadow(in vec3 ro, in vec3 rd, in float mint, in float tmax)
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

vec3 calcNormal(in vec3 pos)
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

vec3 render(in vec3 ro, in vec3 rd)
{ 
    vec3 col = vec3(0.8, 0.9, 1.0);
    vec2 res = castRay(ro,rd);
    float t = res.x;
	float m = res.y;
    if( m>-0.5 )
    {
        vec3 pos = ro + t*rd;
        vec3 nor = calcNormal( pos );
        vec3 ref = reflect( rd, nor );
        
        // material        
		col = 0.45 + 0.3*sin( vec3(0.05,0.08,0.10)*(m-1.0) );
		
        if( m<1.5 )
        {
            
            float f = mod( floor(5.0*pos.z) + floor(5.0*pos.x), 2.0);
            col = 0.4 + 0.1*f*vec3(1.0);
        }

        // lighitng        
        float occ = calcAO( pos, nor );
		vec3  lig = normalize( vec3(-0.6, 0.7, -0.5) );
		float amb = clamp( 0.5+0.5*nor.y, 0.0, 1.0 );
        float dif = clamp( dot( nor, lig ), 0.0, 1.0 );
        float bac = clamp( dot( nor, normalize(vec3(-lig.x,0.0,-lig.z))), 0.0, 1.0 )*clamp( 1.0-pos.y,0.0,1.0);
        float dom = smoothstep( -0.1, 0.1, ref.y );
        float fre = pow( clamp(1.0+dot(nor,rd),0.0,1.0), 2.0 );
		float spe = pow(clamp( dot( ref, lig ), 0.0, 1.0 ),16.0);
        
        dif *= softshadow( pos, lig, 0.02, 2.5 );
        dom *= softshadow( pos, ref, 0.02, 2.5 );

		vec3 brdf = vec3(0.0);
        brdf += 1.20*dif*vec3(1.00,0.90,0.60);
		brdf += 1.20*spe*vec3(1.00,0.90,0.60)*dif;
        brdf += 0.30*amb*vec3(0.50,0.70,1.00)*occ;
        brdf += 0.40*dom*vec3(0.50,0.70,1.00)*occ;
        brdf += 0.30*bac*vec3(0.25,0.25,0.25)*occ;
        brdf += 0.40*fre*vec3(1.00,1.00,1.00)*occ;
		brdf += 0.02;
		col = col*brdf;

    	col = mix( col, vec3(0.8,0.9,1.0), 1.0-exp( -0.0005*t*t ) );

    }

	return vec3( clamp(col,0.0,1.0) );
}

mat3 setCamera( in vec3 ro, in vec3 ta, float cr )
{
	vec3 cw = normalize(ta-ro);
	vec3 cp = vec3(sin(cr), cos(cr),0.0);
	vec3 cu = normalize( cross(cw,cp) );
	vec3 cv = normalize( cross(cu,cw) );
    return mat3( cu, cv, cw );
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 q = fragCoord.xy/iResolution.xy;
    vec2 p = -1.0+2.0*q;
	p.x *= iResolution.x/iResolution.y;
    vec2 mo = iMouse.xy/iResolution.xy;
		 
	float time = 15.0 + iGlobalTime;

	// camera	
	vec3 ro = vec3( -0.5+3.2*cos(0.1*time + 6.0*mo.x), 1.0 + 2.0*mo.y, 0.5 + 3.2*sin(0.1*time + 6.0*mo.x) );
    ro *= 0.7;
	
    vec3 ta = vec3(0.0, 0.8, 0.0); 
    //vec3 ta =vec3( -0.5, -0.4, 0.5 );
	
	// camera-to-world transformation
    mat3 ca = setCamera( ro, ta, 0.0 );
    
    // ray direction
	vec3 rd = ca * normalize( vec3(p.xy,2.5) );

    // render	
    vec3 col = render( ro, rd );

	col = pow( col, vec3(0.4545) );

    fragColor=vec4( col, 1.0 );
}