#define PI 3.14159265359
#define ZMAX 12.0
#define GRIDINTERVAL 40.0/(2.0*PI)
#define DASHINTERVAL 200.0/(2.0*PI)
uniform vec3 uCameraTargetOffset;   

struct Intersection
{
	vec3 p;
    bool vis;
	float dist;
};

Intersection floorintersect(vec3 raydir, vec3 origin) {
    Intersection i;
    i.vis = false;
    
    float t = (-0.5 - origin.y)/raydir.y;
    if (t > 0.) {
    	i.p = origin + raydir*t;
        i.vis = true;
        i.dist = t;
    } 
	return i;
}


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


void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 pos = -1.0 + 2.0 * ( fragCoord.xy / iResolution.xy );
	vec2 posAR;
	posAR.x = pos.x * (iResolution.x/iResolution.y);
	posAR.y = pos.y;
    
	// the first textureless shadertoy to break for missing iDate.w
    //vec3 origin = vec3(0,0,iDate.w-10.0);

	// slow it down for 60fps
    vec3 origin = vec3(0,0,iGlobalTime * (0.05) -10.0);
    vec3 raydir = vec3(posAR.x,posAR.y,1) - vec3(0,0,0);

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

	raydir *= camRotate;


    Intersection i = floorintersect(raydir,origin);
    
    if (i.vis) {
        float d = 0.0;
        bool draw = (
            (
                sin(i.p.x*GRIDINTERVAL) < (-0.999) &&
            	sin(i.p.z*DASHINTERVAL) > 0.95
            ) 
            ||
            (
                sin(i.p.z*GRIDINTERVAL) < -0.999 &&
                sin(i.p.x*DASHINTERVAL) > 0.95
            )
        );
        d = (draw ? 1.0 : 0.0);
        fragColor = vec4(0,d,0,1);
        fragColor *= ((i.dist < ZMAX) ? 1. - i.dist/ZMAX : 0.);
    }
    else {
    	fragColor = vec4(0,0,0,0);
    }
}