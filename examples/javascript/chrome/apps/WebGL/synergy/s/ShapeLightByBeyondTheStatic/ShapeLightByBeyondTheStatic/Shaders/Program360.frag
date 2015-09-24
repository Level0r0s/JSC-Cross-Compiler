uniform vec3 uCameraTargetOffset
/*
ShapeLight
2015 BeyondTheStatic

Raymarching demo showing how to produce low cost, shape-based lighting.

This scheme is definitely not a physical model, and to get smooth results great care
must be taken when designing L()... but maybe some of you will expand on the idea.

My apologies if this has been done before!
*/

//const int	MaxRaySteps	= 64;		// maximum # of ray steps
const int	MaxRaySteps	= 72;		// maximum # of ray steps
//const float	MaxDist		= 15.0;		// maximum distance a ray travels before giving up
const float	MaxDist		= 24.0;		// maximum distance a ray travels before giving up
const float	FudgeFactor	= 1.0;		// lower this if artifacts appear
const float	Accuracy	= 0.005;	// minimum ray step
const float	NormAcc		= 0.005;	// surface normal accuracy

mat3 rotationMatrix(vec3 axis, float angle)
{
    axis = normalize(axis);
    float s = sin(angle);
    float c = cos(angle);
    float oc = 1. - c;
    return mat3(oc * axis.x * axis.x + c,           oc * axis.x * axis.y - axis.z * s,  oc * axis.z * axis.x + axis.y * s,
                oc * axis.x * axis.y + axis.z * s,  oc * axis.y * axis.y + c,           oc * axis.y * axis.z - axis.x * s,
                oc * axis.z * axis.x - axis.y * s,  oc * axis.y * axis.z + axis.x * s,  oc * axis.z * axis.z + c);
}

// object struct; contains a distance estimate and a type flag (light or not)
struct object {
    float form;
    bool isALight;
};

// light shape
object L(in vec3 p)
{
    mat3 rot = rotationMatrix(vec3(0., 1., 0.), iGlobalTime);
    if(iMouse.z>0.)
    {
        p.xz -= 10.*(iMouse.xy/iResolution.xy-.5);
    }
    else
    {
    	p.z -= 2.;
    	p *= rot;
    	p.yz -= vec2(.25, 2.);
        p *= rot;
    }
    p.xz = max(vec2(0.), abs(p.zx)-vec2(.25, 3.));
    
    return object(length(p)-.1, true);
}

// non-light shape
object DE(in vec3 p)
{
	float f;
    f = 1.;
    p.xz = fract(p.xz) - .5;
    p.y += 1.;
    f = length(p-vec3(0., .225, 0.)) - .1;
    f = min(f, length(max(vec3(0.), abs(p)-.075))-.05);
    f = min(f, p.y+.125);
    return object(f, false);
}

// L() & DE() functions, merged
object LDE(in vec3 p){
	object A = L(p), B = DE(p);
    return object(min(A.form, B.form), (A.form<B.form ? A.isALight : B.isALight));
}

// retrieve normal from DE() function
vec3 getNorm(vec3 p)
{
	return
		normalize(
			vec3(
				DE(p+vec3(NormAcc, 0., 0.)).form - DE(p+vec3(-NormAcc, 0., 0.)).form,
				DE(p+vec3(0., NormAcc, 0.)).form - DE(p+vec3(0., -NormAcc, 0.)).form,
				DE(p+vec3(0., 0., NormAcc)).form - DE(p+vec3(0., 0., -NormAcc)).form
			)
		);
}

// retrieve normal from L() function
vec3 getNormL(vec3 p)
{
	float lna = L(p).form;
    //float lna = NormAcc;
    return
		normalize(
			vec3(
				L(p+vec3(lna, 0., 0.)).form - L(p+vec3(-lna, 0., 0.)).form,
				L(p+vec3(0., lna, 0.)).form - L(p+vec3(0., -lna, 0.)).form,
				L(p+vec3(0., 0., lna)).form - L(p+vec3(0., 0., -lna)).form
			)
		);
}

// known by most as ambient occlusion, but I'm pretty sure it's a proximity pattern :)
float getProx(vec3 p, float dist)
{
	return
    	(
            DE(p+vec3(dist, 0., 0.)).form + DE(p+vec3(-dist, 0., 0.)).form +
			DE(p+vec3(0., dist, 0.)).form + DE(p+vec3(0., -dist, 0.)).form +
			DE(p+vec3(0., 0., dist)).form + DE(p+vec3(0., 0., -dist)).form  
		)/dist;
}

// Shadow function, modified from Fragmentarium examples to handle nearby light sources.
// - thanks to iq for the soft shadows!
float getShadow(vec3 hit, vec3 lightDir, float lightDist)
{
	float dist;
	float k = 16.; // shadow hardness
	float totalDist = 1. / k; // starting distance based on shadow hardness
	float res = 1.;
	for(int steps=0; steps<MaxRaySteps; steps++)
	{
		vec3 P = hit + totalDist * lightDir;
		dist = DE(P).form;
		if(dist < Accuracy) return 0.;
        if(totalDist >= min(MaxDist, lightDist)) break;
		res = min(res, k*dist/float(steps));
		totalDist += dist;
	}
	return res;
}

// get last and total distances from a ray traced from camPos to rayDir
vec2 intersect(vec3 camPos, vec3 rayDir)
{
	float dist;
	float totalDist = 0.;
	vec3 p;
	for(int steps=0; steps<MaxRaySteps; steps++)
	{	
		p = camPos + totalDist * rayDir;
		dist = LDE(p).form * FudgeFactor;
		totalDist += dist;
		if(dist<Accuracy || totalDist>MaxDist) break;
	}
    return vec2(dist, totalDist);
}

// get shading
vec3 getColor(vec3 hit, vec3 rayDir, vec2 dists)
{
    vec3 lightCol	= 2. * vec3(2., 1., .5);
    //vec3 fogCol		= .75 * vec3(1., 1.25, 1.5);
    vec3 fogCol		= .0175 * vec3(1., 1.25, 1.5);
    float fogDist	= 40.;
    vec3 visible;
	if(dists.x < Accuracy) // a surface was hit, do some shading
	{
		// test object to see if it's a light or not
        if(LDE(hit).isALight) // surface is a light
        {
            visible = lightCol;
        }
        else // surface is not a light
        {
            // surface normal
            vec3 norm = getNorm(hit);

            // light direction and position
            vec3 lightDir = -getNormL(hit);
            vec3 lightPos = hit + L(hit).form * lightDir;
            
            float shadow = getShadow(hit, lightDir, distance(hit, lightPos));
            float brilliance = .5;
            float diffuse = .75 * pow(max(0.0, dot(norm, lightDir)), brilliance);
			
            float lightFade = 0.;

            // apply distance fading for light
            if(true)
            {
                float fadeDistance = 4.;
                lightFade =
                    1./(
                        1. +
                        pow(
                            distance(hit, lightPos) / fadeDistance,
                            2. // fade_power
                        )
                    );
                diffuse *= lightFade;
            }
            
            // apply lighting
            visible = lightCol * vec3(diffuse);
            
            // apply specular highlight
            if(true)
			{
				vec3 ref = reflect(lightDir, norm);
				float spec = max(0., dot(rayDir, ref));
				float rough = .1;
                visible +=
					lightCol *
                    (lightFade>0. ? lightFade : 1.) *
					max(0., pow(spec, max(0., 1./rough)));
			}
            
            // apply shadows
            visible *= shadow;
            
            // apply prox
            visible += .025 * fogCol *vec3(getProx(hit, .035));
            
            // apply fog
            visible = mix(fogCol, visible, exp(-dists.y/fogDist));
        }
		
	}
	else // no surface hit, return bg color
	{
		visible = fogCol;
	}
	
	return visible;
}

void mainImage(out vec4 fragColor, in vec2 fragCoord)
{
	vec2 res	= iResolution.xy;
    vec2 uv		= (fragCoord-.5*res) / iResolution.y;
    

	//uv *= 0.5;
	uv /= 0.5;

	//vec3 camPos		= vec3(0., 0., -2.);
	vec3 camPos		= vec3(0., 0., 0.);
    vec3 rayDir		= normalize(vec3(uv, 1.));
    
    
    //mat3 camRotate	= rotationMatrix(vec3(1., 0., 0.), radians(090.));
    //mat3 camRotate	= rotationMatrix(vec3(0., 1., 0.), radians(
    //mat3 camRotate	= ;
    
    //rayDir *= rotationMatrix(vec3(1., 0., 0.), radians(
    mat3 camRotate = 
	
		// bottom
		(uCameraTargetOffset.y == -1.0) ? rotationMatrix(vec3(1., 0., 0.), radians(90.0)) * rotationMatrix(vec3(0., 1., 0.), radians(90.0)):

		// top
		(uCameraTargetOffset.y == 1.0) ? rotationMatrix(vec3(1., 0., 0.), radians(-90.0)) * rotationMatrix(vec3(0., 1., 0.), radians(90.0)):
	
		rotationMatrix(vec3(0., 1., 0.), radians(
        
		
		// left
		(uCameraTargetOffset.z == -1.0) ? 270. :

		// right
		(uCameraTargetOffset.z == 1.0) ? 90. :

		// back
		 (uCameraTargetOffset.x == -1.0) ?  180. : 
		
		
		// front
		/* (uCameraTargetOffset.x == 1.0) ? */ 0. 

    ));

	rayDir *= camRotate;
    //camPos *= camRotate;

    
    vec2 dists	= intersect(camPos, rayDir) ;
    vec3 hit	= camPos + dists.y * rayDir;
    vec3 RGB	= getColor(hit, rayDir, dists);
    
    fragColor = vec4(RGB, 1.);
}