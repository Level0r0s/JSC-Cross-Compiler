// version 1.5 = trying to make the water surface and the sea floor more interesting
// version 1.3 = changed wing primitives and adding front flaps
// version 1.2 = working on improving the shape
// version 1.1 = multiple mantas. Added scattering attributes
// Version 1.0 = creation. Added a background. Single manta swim loop


const vec3 sun = vec3(-0.6, 0.4,-0.3);
float time = iGlobalTime+32.2;
vec3 lightDir = normalize(vec3(.1,.7, .2));
#define csb(f, con, sat, bri) mix(vec3(.5), mix(vec3(dot(vec3(.2125, .7154, .0721), f*bri)), f*bri, sat), con)
#define MATERIAL_SKIN 1.0
#define MATERIAL_NONE 0.0
#define SPACING 4.
#define ISPACING 0.25
#define BBSIZE 2.0
#define MAXDIST 25.

//--------------------------------------------------------------------------------------
// Utilities....
float hash( float n )
{
    return fract(sin(n)*43758.5453123);
}

vec3 Rotate_Y(vec3 v, float angle)
{
	vec3 vo = v; float cosa = cos(angle); float sina = sin(angle);
	v.x = cosa*vo.x - sina*vo.z;
	v.z = sina*vo.x + cosa*vo.z;
	return v;
}

// stolen from IQ.
float softMin(float a, float b)
{
    float k = 0.5;
	float h = clamp( 0.5 + 0.5*(b-a)/k, 0.0, 1.0 );
	return mix( b, a, h ) - k*h*(1.0-h);
}

float difference(float a, float b)
{
    return max(a, -b);
}

float sphere(vec3 p, float r)
{
    return length(p) - r;
}

float wings(vec3 p) 
{   
    p*=vec3(0.7,3.5,1.2);
    p.z -= p.x*p.x*0.8;
    p.y -= 0.1;
    float d = p.x+p.y*p.y+pow(abs(p.z),1.4);
    
    return d-1.0;
}

float mantabody(vec3 p)
{
    vec3 origP = p;
    // thinner wings
    p*=vec3(2.4,2.6,1.3);
    float d = sphere(p, 1.0);
    d = softMin(d, wings(origP));
    
    // body hole, scale with body
    vec3 holeP = p;
    holeP *= vec3(1.2,1.4,0.8);
    holeP += vec3(-0.2,0.13,0.6);
    d = difference(d, length(holeP) - 0.9);
    
    vec3 flapsP = origP;
    flapsP += vec3(-0.3,0.3-origP.x*0.5,0.95-origP.x*1.);
    flapsP *= vec3(6.0,12.0,6.0);
    d = softMin(d, sphere(flapsP,1.0));
    
    return d;
}


//--------------------------------------------------------------------------------------
vec2 Scene(vec3 p)
{
    p.z+= time;
	float mat = MATERIAL_SKIN;
    
    
    // Repeat
    vec3 loopP = p;
    loopP.x = mod(loopP.x+BBSIZE, SPACING)-BBSIZE;
    loopP.z = mod(loopP.z+BBSIZE, SPACING)-BBSIZE;

    //scramble
    float rowId = floor((p.x+2.)*ISPACING);
    float columnId = floor((p.z+2.)*ISPACING);
    //loopP.y += hash(7.*rowId + 11.* columnId)*4.-1.5; // starting height
	float size = 0.6 + 0.6*hash(7.*rowId + 11.* columnId);
    //float size = 1.0;
    float timeloop = time * 2.5 / (size-0.25) + // loop speed
        hash(rowId+3.*columnId) * 10.; // random offset
    loopP.y+= -sin(timeloop-0.5)*.25 * size;
    loopP.y+= sin(time*0.5 + hash(37.*rowId+11.*columnId)*17.) * 2.5;
    
    // Mirror
    loopP.x = abs(loopP.x);
    loopP/=size;
    
    float d = 3.;
    
    
    //////////////////
    // manta body
    //////////////////
    vec3 mantap = loopP;
    
    // tail
    vec3 tailp = loopP;
    
    // animate wings
    float animation = sin(timeloop-3. - 1.3*loopP.z);
    mantap.y += animation * (0.3*loopP.x*loopP.x + 0.10);
    tailp.y += animation* (0.1 + smoothstep(1.,3.,tailp.z)) - 0.1;
    
    d = min(d, mantabody(mantap));
    //d = min(d, sphere(mantap,1.0));
    
    
    float taild = length(tailp.xy)*0.4 + 
        smoothstep(0.,1.,-tailp.z) * 2.+ 
    	smoothstep(3.,5.,tailp.z) * 10.;
    
    // soft min?
    d = softMin(d,taild);

	return vec2(d, mat);
}

//--------------------------------------------------------------------------------------
vec4 Trace(vec3 rayOrigin, vec3 rayDirection, out float hit)
{
	const float minStep = 0.005;
    hit = 0.0;
	
    vec2 ret = vec2(0.0, 0.0);
    vec3 pos = rayOrigin;
	float dist = 0.0;
    for(int i=0; i < 200; i++){
		if (hit == 0.0 && dist < MAXDIST && pos.y<10.0 && pos.y > -10.0 )
        {
            pos = rayOrigin + dist * rayDirection;
            ret = Scene(pos);
            // ret.x = distance
            // ret.y = material type
            if (ret.x < 0.01)
                hit = ret.y;

            // increment
            if (ret.y >= 2.0)
                dist += ret.x * 10.0;
            else
                dist += max(ret.x * 0.2, minStep);
        }
        
    }
    return vec4(pos, ret.y);
}

//--------------------------------------------------------------------------------------
vec3 GetNormal(vec3 p)
{
	vec3 eps = vec3(0.01,0.0,0.0);
	return normalize(vec3(Scene(p+eps.xyy).x-Scene(p-eps.xyy).x,
						  Scene(p+eps.yxy).x-Scene(p-eps.yxy).x,
						  Scene(p+eps.yyx).x-Scene(p-eps.yyx).x ));
}

//--------------------------------------------------------------------------------------
vec3 GetColour(vec4 p, vec3 n, vec3 org, vec3 dir)
{
    vec3 localP = vec3(p) + vec3(0.,0.,time);
    vec3 loopP = localP.xyz;
    loopP.x = mod(loopP.x+BBSIZE, SPACING)-BBSIZE;
    loopP.z = mod(loopP.z+BBSIZE, SPACING)-BBSIZE; 
   
    
	vec3 colour = vec3(0.0);
	if (p.w < 1.5)
    {
		float v = clamp(-(n.y-.1)*6.2, 0.3, 1.0);
		v+=.35;
		colour = vec3(v*.8, v*.9, v*1.0);
	}
    
    vec2 coord = loopP.xz;
    vec3 colorUp = colour;
    float stainsUp = texture2D(iChannel1, coord).x;
    stainsUp *= stainsUp * 2.;
    stainsUp = 1.-stainsUp;
	stainsUp += smoothstep(2.,-2.,loopP.z);
    stainsUp = clamp(stainsUp, 0., 1.);
    colorUp *= stainsUp;
    
    vec3 colorDown = colour;
    float stainsDown = texture2D(iChannel1, coord*0.4).x;
    stainsDown += smoothstep(0.,-3.,loopP.z);
    stainsDown *= stainsDown;
    stainsDown = clamp(stainsDown, 0., 1.);
    colorDown *= vec3(stainsDown);
    
    colour = mix(colorUp, colorDown, smoothstep(-0.4,0.4,n.y));   
    {
        // Water caustics
        vec2 wat = p.xz*1.;
		wat +=  (texture2D(iChannel0, (wat*5.0+time*.04)*.1, 3.0).z -
			 texture2D(iChannel0, wat*.3-time*.03, 2.0).y) * .4;
		float causticLight = texture2D(iChannel0, wat* .04, 0.0).x;
		causticLight = pow(max(0.0, causticLight-.2), 1.0) * 20. * smoothstep(-5.,3.,p.y);
        colour *= vec3(1.0) + vec3(causticLight*.5, causticLight, causticLight)*max(n.y, 0.0); 
    }
	
    // shadow
    float diff = dot(n,lightDir);
    // light properties
    vec3 brightLight = vec3(0.7,0.7,0.8);
    vec3 shade = vec3(0.12,0.15,0.22);
    colour *= mix(shade,brightLight,max(diff*0.5+0.5,0.0));
    
    return colour;
}


//--------------------------------------------------------------------------------------
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec3 col;	
	vec2 uv = (fragCoord.xy / iResolution.xy) - vec2(.5);
	uv.x*=iResolution.x/iResolution.y;
	vec3 dir = normalize(vec3(uv, -1.0));
	
	//vec3 pos = vec3(1.3, sin(time+4.3)*.18-.05, sin(-time*.15)*5.0-1.35);
    vec3 pos = (sin(time*0.14)*2.+4.5)*vec3(sin(time*.5), 0.0, cos(time*.5));
    pos.z -= time;
    pos.y += 0.7 * sin(time * 0.2);
    float rot = -time*0.5;
	dir = Rotate_Y(dir, rot);

    // Sun...
	float i = max(0.0, 1./(length(sun-dir)+1.0));
	col = vec3(pow(i, 1.9), pow(i, 1.0), pow(i, .8)) * 1.3;
	
	// Water depth colour...
	col = mix(col, vec3(0.0, .25, .45), ((1.0-uv.y)*.45) * 1.8);
	
    float d;
	if (uv.y >= 0.0)
	{
		// Add water ripples...
        d = (3.0-pos.y) / -uv.y;
		
        vec2 wat = (dir * d).xz-pos.xz;
        d += 1.*sin(wat.x + time);
        wat = (dir * d).xz-pos.xz;
        wat = wat * 0.1 + 0.2* texture2D(iChannel0, wat, 0.0).xy;
        
		i = texture2D(iChannel3, wat, 0.0).x;
        
		col += vec3(i) * max(2./-d, 0.0);
	}
	else		
	{
		// Do floor stuff...
		d = (-3.0-pos.y) / uv.y;
		vec2 coord = pos.xz+(dir.xz * d);
		vec3 sand = texture2D(iChannel3, coord* .1).rgb * 1.5  + 
					texture2D(iChannel3, coord* .23).rgb;
        sand *= 0.5;
		
		float f = ((-uv.y-0.3 +sin(time*0.1)*0.2)*2.45) * .4;
		f = clamp(f, 0.0, 1.0);
		
		col = mix(col, sand, f);
	}

	float hit = 0.0;
	vec4 loc = Trace(pos, dir, hit);
	if (hit > 0.0)
	{
		vec3 norm = GetNormal(loc.xyz);
		vec3 foundColor = GetColour(loc, norm, pos, dir);
        vec3 backgroundColor = col;
    
        // total water reflection
        float facing = -dot(norm,dir);
    	float upfacing = clamp(norm.y, 0.,1.);
        float fresnel = 1.0-facing;
        fresnel = clamp(pow(fresnel, 1.0), 0.0,1.0);
        foundColor = mix(foundColor, backgroundColor*2.0, 0.5 * (0.5 + upfacing*upfacing) * fresnel);
        
        // atmos
		float dis = length(pos-loc.xyz);
		float fogAmount = clamp(max((dis-.5),0.0)/MAXDIST, 0.0, 1.0);
        
		col = mix(foundColor, backgroundColor, fogAmount );
	}
	
    // Contrast, saturation and brightness...
	col = csb(col, 1.1, 1.05, 1.22);
	
    // Fade in...
	//col *= smoothstep( 0.0, 2.5, iGlobalTime );
	fragColor = vec4(col, 1.0);
}
