uniform vec3 uCameraTargetOffset;
// @eddbiddulph
// Use the mouse to rotate the view!

#define EPS vec2(1e-3, 0.0)

vec3 rotateX(float a, vec3 v)
{
   return vec3(v.x, cos(a) * v.y + sin(a) * v.z, cos(a) * v.z - sin(a) * v.y);
}

vec3 rotateY(float a, vec3 v)
{
   return vec3(cos(a) * v.x + sin(a) * v.z, v.y, cos(a) * v.z - sin(a) * v.x);
}

float cube(vec3 p, vec3 s)
{
   return length(max(vec3(0.0), abs(p) - s));
}

float customCube(vec3 p, vec3 s)
{
   return cube(p, s) - 0.01;
}

float circular(float t)
{
   return sqrt(1.0 - t * t);
}

float processionOfCubes(vec3 p)
{
   float t = -iGlobalTime * 0.2;
   p.z -= t * 2.5;
   float rad = 0.1 + cos(floor(p.z) * 10.0) * 0.07;
   p.x += cos(floor(p.z) * 2.0) * 0.3;
   p.z -= floor(p.z);
   t /= rad;
   t -= floor(t);
   vec3 org = vec3(0.0, circular((t - 0.5) * 1.5) * length(vec2(rad, rad)), 0.5);
   return customCube(rotateX(t * 3.1415926 * 0.5, p - org), vec3(rad));
}

float scene(vec3 p)
{
   float tunnel0 = max(-p.y, length(max(vec2(0.0), vec2(-abs(p.z) + 2.0, max(length(p.xy) - 2.0, 0.9 - length(p.xy))))) - 0.03);
   return min(tunnel0, processionOfCubes(p));
}

vec3 sceneGrad(vec3 p)
{
   float d = scene(p);
   return (vec3(scene(p + EPS.xyy), scene(p + EPS.yxy), scene(p + EPS.yyx)) - vec3(d)) / EPS.x;
}

vec3 veil(vec3 p)
{
   float l = length(p);
   return vec3(1.0 - smoothstep(0.0, 4.0, l)) * vec3(1.0, 1.0, 0.75);
}

vec3 environment(vec3 ro, vec3 rd)
{
   float t = -ro.y / rd.y;
   vec2 tc = ro.xz + rd.xz * t;
   vec3 p = vec3(tc.x, 0.0, tc.y);
   float d = scene(p);

   float u = fract(dot(tc, vec2(1.0)) * 20.0);
   float s = t * 2.0;
   float stripes = smoothstep(0.0, 0.1 * s, u) - smoothstep(0.5, 0.5 + 0.1 * s, u);


   vec3 col = mix(vec3(0.3, 0.3, 0.6), vec3(0.3, 0.3, 0.6) * 1.3, stripes);

   return veil(p) * col * mix(0.0,
         mix(0.9, 1.0, 1.0) *
         mix(0.5, 1.0, sqrt(smoothstep(0.0, 0.3, d))) *
         mix(0.5, 1.0, sqrt(smoothstep(0.0, 0.06, d))), step(0.0, t));
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
	vec2 res	= iResolution.xy;
    vec2 uv		= (fragCoord-.5*res) / iResolution.y;
    

	//uv *= 0.5;
	uv /= 0.5;



	vec3 ro = vec3(0.4, 0.8, 1.0);
	//vec3 ro = vec3(1.0, 1.0, 1.0);

	//vec3 ro = vec3(0.0, 1.2, 2.0);
	
	vec3 rd = vec3(uv, 1.0);
	
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

	rd *= camRotate;

	vec2 angs = vec2(1.0-iMouse.y * 0.003, 1.0-iMouse.x * 0.01);
	
	//ro = rotateY(angs.y, ro) + vec3(0.0, 0.6, 0.0);
	//rd = rotateY(angs.y, rotateX(angs.x, rd));




	
	fragColor.rgb = environment(ro, rd);
	
	for(int i = 0; i < 200; i += 1)
	{
		float d = scene(ro);
		
		if(abs(d) < 1e-3)
		{
			fragColor.rgb = veil(ro) * mix(vec3(0.1, 0.1, 0.4) * 0.25, vec3(1.0),
						  (1.0 + dot(normalize(sceneGrad(ro)), normalize(vec3(0.0, 1.0, 0.4)))) * 0.5);
		}
		
		ro += rd * d * 0.2;
	}
	
	fragColor.rgb = sqrt(fragColor.rgb);
	fragColor.a = 1.0;
}


