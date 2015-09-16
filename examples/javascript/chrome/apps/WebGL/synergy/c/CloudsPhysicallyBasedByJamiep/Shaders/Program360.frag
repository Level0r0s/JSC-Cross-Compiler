uniform vec3 uCameraTargetOffset;   




// hash based 3d value noise
float hash( float n )
{
    return fract(sin(n)*43758.5453);
}
float noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);
    f = f*f*(3.0-2.0*f);
    float n = p.x + p.y*57.0 + 113.0*p.z;
    return mix(mix(mix( hash(n+  0.0), hash(n+  1.0),f.x),
                   mix( hash(n+ 57.0), hash(n+ 58.0),f.x),f.y),
               mix(mix( hash(n+113.0), hash(n+114.0),f.x),
                   mix( hash(n+170.0), hash(n+171.0),f.x),f.y),f.z);
}


const float KAPPA = 0.08;  // extinction coefficient

vec3 sun_direction()
{
	float freq = 1.0;
	float phase = 2.0 + freq*iGlobalTime;
	return normalize(vec3(cos(phase), 0.1, sin(phase)));
}

// returns clouds extinction coeff. as a function of world position
float clouds_extinction( in vec3 p )
{	
	float spatial_period = 32.0;
	p /= spatial_period;
	
	float timefreq = 0.5;
	vec3 q = p - vec3(1.0,0.1,0.0)*timefreq*iGlobalTime;
	
	float cloud_height = 2.5;
	float layer_y = -1.3*cloud_height;

	float N;
	float g = 1.0;
	float beta = 0.4;
    N  = g * noise( q ); q *= 2.0; g *= beta;
    N += g * noise( q ); q *= 2.0; g *= beta;
    N += g * noise( q ); q *= 2.0; g *= beta;
    N += g * noise( q );
	
	float s = 0.16; // edge smoothness (0=hard, 1=smooth)
	
	float cloudtop_y = layer_y + cloud_height*pow(abs(N), 0.7);
	return KAPPA * (smoothstep(p.y-s*cloud_height, p.y+s*cloud_height, cloudtop_y));
}


// return sun+sky radiance
vec3 sky_color( in vec3 rd )
{
	vec3 skyColor = vec3(0.6,0.71,0.78) - rd.y*0.25*vec3(1.0,0.5,1.0);
	float sp = dot(sun_direction(), rd) - cos(radians(5.0));
	vec3 sun = 2.0e6 * vec3(1.0,0.9,0.7) * pow(sp * step(0.0, sp), 2.3);
	skyColor += sun;
	return skyColor;
}


// return radiance reaching the point pos from the sun
vec3 reduced_sun( in vec3 pos )
{
	const int num_steps = 2;
	const float max_dist = 10.0;
	float dl = max_dist/float(num_steps);
	float Transmittance = 1.0;
	for(int i=0; i<num_steps; i++)
	{
		if(Transmittance < 0.001) break;
		pos += dl*sun_direction();
		float kappa = clouds_extinction( pos );
		Transmittance *= exp(-kappa*dl);
	}
	return Transmittance * sky_color(sun_direction());
}


// Henyey-Greenstein phase function
float phasefunction(in vec3 a, in vec3 b)
{
	float mu = dot(a, b);
	float g = 0.25;
	float gSqr = g*g;
	float oofp = 1.0/(4.0*3.141592);
	return oofp * (1.0 - gSqr) / pow(1.0 - 2.0*g*mu + gSqr, 1.5);	
}


// raymarch to complete volume rendering integral
vec3 primary( in vec3 ro, in vec3 rd )
{
	const float max_dist = 250.0;
	const int num_steps = 64;
	float dl = max_dist/float(num_steps);
	
	vec3 pos = ro;
	float Transmittance = 1.0;
	
	// Calculate volume rendering integral along primary ray
	vec3 InScattered = vec3(0.0);
	for(int i=0; i<num_steps; i++)
	{
		if(Transmittance < 0.01) break;
		pos += dl*rd;
		float kappa = clouds_extinction( pos );
		Transmittance *= exp(-kappa*dl);
		
		// single scattering given by in-scatter of sunlight
		const vec3 albedo = vec3(0.85, 0.82, 0.79);
		vec3 single_scatt = albedo * kappa * dl * reduced_sun(pos) * phasefunction(sun_direction(), rd);
		
		// Fake multiple scattering by a constant emission field (power is an ad-hoc aesthetic tune-
		// though the power 1.0 possibly has some physical basis, since the diffuse light can be expected
		// to be roughly proportional to the density of scatterers
		vec3 fake_multiple_scatt = albedo * kappa * dl * pow(kappa/KAPPA, 1.0) * 2.5 * vec3(0.33, 0.35, 0.34);
		
		// Accumulate integrand
		InScattered += Transmittance * (single_scatt + fake_multiple_scatt);
	}

	vec3 AttenuatedBackground = Transmittance*sky_color(rd);
	return InScattered + AttenuatedBackground;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xy;
	uv = uv * 2.0 - 1.0;
	uv.x *= iResolution.x / iResolution.y;



	vec2 q = fragCoord.xy / iResolution.xy;
    vec2 p = -1.0 + 2.0*q;
    p.x *= iResolution.x/ iResolution.y;
    vec2 mo = -1.0 + 2.0*iMouse.xy / iResolution.xy;
    
    // camera
    vec3 ro = 4.0*normalize(vec3(cos(2.75-3.0*mo.x), 1.5+(2.0*mo.y+1.0), sin(2.75-3.0*mo.x)));
	//vec3 ta = ro + vec3(0.0, 1.0, 0.0);
	vec3 ta = ro + uCameraTargetOffset;
	

    vec3 ww = normalize( ta - ro);
    vec3 uu = normalize(cross( vec3(0.0,1.0,0.0), ww ));
    vec3 vv = normalize(cross(ww,uu));
    vec3 rd = normalize( p.x*uu + p.y*vv + 1.0*ww );

	// raymarch to obtain transmittance along ray through clouds
    vec3 L = primary( ro, rd );

    fragColor = vec4( L, 1.0 );
}
