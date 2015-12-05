#define MAX_ITERATIONS 256
#define MIN_DISTANCE  .001

#define BIAS .01
#define SUN_DIR normalize(vec3(45.,30.,-45.))
#define AMB .3

struct Ray {
  vec3 ori;
  vec3 dir;
};
struct Dist {
  float dst;
  int id;
};
struct Hit {
  vec3 p;
  int id;
};
  
float noise(vec2 p) {

   return 0.;
    
}
    
float distFlag(vec3 p, vec3 pos, vec3 b) {

    const float waveIntensity = .1;
    const float waveSpeed     = 6.;
    
    p.y += waveIntensity * cos(waveSpeed * iGlobalTime + p.x) * sin(waveSpeed * iGlobalTime + p.z);
    p.z += waveIntensity * cos(waveSpeed * iGlobalTime + p.x) * sin(waveSpeed * iGlobalTime + p.z);
    
    return length(max(abs(pos - p) - b, 0.));
    
}

float distPole(vec3 p, vec3 pos, vec2 h) {
 
    vec3 q = pos - p;
    vec2 d = abs(vec2(length(q.xz),q.y)) - h;
    return min(max(d.x,d.y),0.) + length(max(d,0.));
    
}

float distFloor(vec3 p, float y) {
 
    return p.y - y;
    
}

Dist distScene(vec3 p) {
 
    float dFlag = distFlag (p, vec3(0.,1.3,1.), vec3(2.,1.5,.1));
    float dPole = distPole (p, vec3(-2.,-3.12,1.), vec2(.3,6.));
    float dFlor = distFloor(p, -6.);
    
    float d = min(min(dFlag,dPole), dFlor);
	return Dist(d, d == dFlag ? 0 : d == dPole ? 1 : 2);
    
}

Hit raymarch(Ray ray) {
 
    vec3 p = ray.ori;
    int id = -1;
    
    for(int i = 0; i < MAX_ITERATIONS; i++) {
     
        Dist dst = distScene(p);
        p += ray.dir * dst.dst;
        
        if(dst.dst <= MIN_DISTANCE) {
         
            id = dst.id;
            break;
            
        }
        
    }
    
    return Hit(p,id);
    
}

mat2 rot2D(float angle) {
 
    float s = sin(angle);
    float c = cos(angle);
    
    return mat2(c,s,-s,c);
    
}

vec3 calcNormal(vec3 p) {
 
    const vec2 eps = vec2(.1,0.);
    vec3 n = vec3(distScene(p + eps.xyy).dst - distScene(p - eps.xyy).dst,
                  distScene(p + eps.yxy).dst - distScene(p - eps.yxy).dst,
                  distScene(p + eps.yyx).dst - distScene(p - eps.yyx).dst);
    return normalize(n);
    
}

float getLighting(Hit hit, Ray ray, vec3 n, bool shadows) {
  
    float d = AMB + max(dot(SUN_DIR,n), 0.);
    
    if(shadows) {
     
        Ray sr = Ray(hit.p + (SUN_DIR * BIAS), SUN_DIR);
    	Hit sh = raymarch(sr);
    
    	if(sh.id != -1) {
     
        	d = AMB;
        
    	}
        
    }
    
    return d;
    
}

vec3 clearColor(vec3 dir) {
 
    if (dir.y < 0.0)
    return vec3(0.0,  0.3 + 0.4 * -dir.y, 0.0);
    
        return vec3(0.0, 0.0, 0.3 + 0.4 * -dir.y);
//    return textureCube(iChannel0, dir).xyz;
    
}

vec3 shadeFlag(Ray ray, Hit hit) {
 
    vec3  n = calcNormal(hit.p);
    float d = getLighting(hit, ray, n, false);
        
    //vec3  c = texture2D(iChannel1, mod(hit.p.xy / .02, 1.)).xyz;
    return vec3(1.00, 0.0, 0.0)*d;
    
}

vec3 shadeFloor(Ray ray, Hit hit) {
 
    vec3 n = calcNormal(hit.p);
    float d = getLighting(hit, ray, n, true);
        
    if(d > AMB) d = 1.;
    
    return clearColor(ray.dir) * d;
    
}

vec3 shade(Ray ray) {
 
    Hit scene = raymarch(ray);
    
    if(scene.id == 0) {
     
        return shadeFlag(ray, scene);
        
    } else if(scene.id == 1) {
     
        vec3 n  = calcNormal(scene.p);
        vec3 r  = reflect(ray.dir, n);
        vec3 rc = vec3(0.);
        
        Ray rr = Ray(scene.p + (r * BIAS), r);
        Hit rh = raymarch(rr);
        
        if(rh.id == 0) {
         
            rc = shadeFlag(rr,rh);
            
        } else if(rh.id == 2) {
          
            rc = shadeFloor(rr,rh);
            
        } else {
         
            rc = clearColor(rr.dir);
            
        }
        
        float diff = getLighting(scene, ray, n, true) - AMB;
        float spec = max(pow(dot(r,SUN_DIR), 30.), 0.) * diff;
        return (diff * rc) + vec3(spec);
        
    } else if(scene.id == 2) {
     
        return shadeFloor(ray, scene);
        
    }
    
    return clearColor(ray.dir);
    
}
    
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = (fragCoord.xy - iResolution.xy / 2.) / iResolution.y;
    vec2  m = (iMouse.xy - iResolution.xy / 2.) / iResolution.y;
    
    vec3 ori = vec3(3.,2.,-22.);
    vec3 dir = vec3(uv, 1.);
    
    vec3 col = shade(Ray(ori,dir));
	fragColor = vec4(col,1.0);
}