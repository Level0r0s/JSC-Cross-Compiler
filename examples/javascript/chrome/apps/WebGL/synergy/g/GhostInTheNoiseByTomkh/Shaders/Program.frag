// A simple example tracing over value-noise
// tomkh@2015

const int iterations = 200;
const float dist_eps = .004;
const float ray_max = 50.0;
const float fog_density = .05;

const float cam_dist = 8.5;

// Estimated inverse "steepness" factor:
float invslope_factor = .9;

vec3 light_pos;

//------------------------------------------------------------------------
// Some of IQ's noise that is in fact a value-noise (not a gradient-noise)
// Extended with offset
float hash( float n ) { return fract(sin(n)*753.5453123); }
float noise( in vec3 x, in float offset )
{
    vec3 p = floor(x);
    vec3 f = fract(x);
    f = f*f*(3.0-2.0*f);
	
    float n = p.x + p.y*157.0 + 113.0*p.z + offset*17.0;
    return mix(mix(mix( hash(n+  0.0), hash(n+  1.0),f.x),
                   mix( hash(n+157.0), hash(n+158.0),f.x),f.y),
               mix(mix( hash(n+113.0), hash(n+114.0),f.x),
                   mix( hash(n+270.0), hash(n+271.0),f.x),f.y),f.z);
}
//------------------------------------------------------------------------

float dField(in vec3 p)
{
   // Simply take a noise value * invslope_factor as a distance:
   p += vec3(.7,1.2,-5.0);
   
   // Take distance to light as interpolation factor:
   vec3 dp = p - light_pos;
   float m = max(0.0, 3.0 - length(dp)*.5);
    
   // Interpolate two noise values:
   float value;
   if (m > 0.0) {
     float o = floor(m);
     float f = m - o;
     f = f*f*(3.0-2.0*f);
     value = mix(noise(p,o),noise(p,o+1.0),f);
   } else {
     value = noise(p,0.0);
   }
   
    return (value - .33)*invslope_factor;
}

vec3 dNormal(in vec3 p)
{
   const float eps = .005;
   const vec3 x_eps = vec3(eps,0,0);
   const vec3 y_eps = vec3(0,eps,0);
   const vec3 z_eps = vec3(0,0,eps);
   return normalize(vec3(
      dField(p + x_eps) - dField(p - x_eps),
      dField(p + y_eps) - dField(p - y_eps),
      dField(p + z_eps) - dField(p - z_eps) ));
}

vec4 trace(in vec3 ray_start, in vec3 ray_dir, inout float ray_len, inout float light_gather)
{
   vec3 p = ray_start;
   for(int i=0; i<iterations; ++i) {
   	  float dist = dField(p);
      if (dist < dist_eps) break;
      if (ray_len > ray_max) return vec4(0.0);
      
      vec3 light_dir = light_pos - p;
      float light_dist = dot(light_dir, light_dir);
      float light_falloff = (5.0/light_dist)*(1.0-dist);
      //light_falloff *= texture2D(iChannel0, normalize(light_dir).xy*.1).x*.5+.5;
      light_gather += light_falloff*dist; // gather along the ray
      
      p += dist*ray_dir;
      ray_len += dist;
   }
   return vec4(p, 1.0);
}

vec4 shade(in vec3 ray_dir, in float ray_len, in float light_gather, in vec4 hit, out vec3 norm)
{
   const vec3 ambient = vec3(-.08,.22,.08);
   const vec3 light_color = vec3(1.,1.,.7);
   vec3 fog_color = light_color*(light_gather*.07) + ambient;
   
   if (hit.w == 0.0) {
      return vec4(fog_color, 1.0);
   }
   
   norm = dNormal(hit.xyz);
   vec3 light_dir = light_pos - hit.xyz;
   float light_dist = dot(light_dir, light_dir);
   light_dir *= inversesqrt(light_dist);
   float light_falloff = min(1.5,(5.0/light_dist));
   float diffuse = max(0.0, dot(norm, light_dir));
   float spec = max(0.0,dot(reflect(light_dir,norm),ray_dir));
   spec = pow(spec, 16.0)*.5;
   diffuse *= light_falloff;
   spec *= light_falloff;

   vec3 base_color = vec3(.7,.5,.1);
   //vec3 anorm = abs(norm.xyz);
   //vec2 uv = (anorm.x>max(anorm.y,anorm.z))?hit.yz:(anorm.y>anorm.z)?hit.xz:hit.xy;
   //base_color *= texture2D(iChannel0, uv).x*.1 + .9;
   vec3 color = mix(ambient,light_color*base_color,diffuse) +
      spec*vec3(1.,1.,.9);
   
   float fog = 1.0 - 1.0/exp(ray_len*fog_density);
   color = mix(color, fog_color, fog);

   return vec4(color, 1.0);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
   vec2 uv = (fragCoord.xy - iResolution.xy*0.5) / iResolution.y;
    
   float ang, si, co;
    
   float light_anim = iGlobalTime*.25;
   si = sin(light_anim*.6); co = cos(light_anim*.4);
   light_pos = vec3(sin(light_anim)*3.0*si,co*1.0,si*cos(light_anim*.7)*3.0);
    
   //if (iMouse.z > 0.0) {
   //  invslope_factor = iMouse.y * 2.0 / iResolution.y;
   //}
   
   // Simple rotating camera:
   ang = (iMouse.z > 0.0) ? -(iMouse.x - iResolution.x*.5)*.003 : sin(iGlobalTime*.5)*.05;
   si = sin(ang); co = cos(ang);
   mat4 cam_mat = mat4(
      co, 0., si, 0.,
      0., 1., 0., 0.,
     -si, 0., co, 0.,
      0., 0., 0., 1.);
   if (iMouse.z > 0.0) {
      ang = -(iMouse.y - iResolution.y*.5)*.003;
	  si = sin(ang); co = cos(ang);
      cam_mat *= mat4(
         1., 0., 0., 0.,
         0., co, si, 0.,
         0.,-si, co, 0.,
         0., 0., 0., 1.);
   }

   vec3 pos = vec3(cam_mat*vec4(0., 0., -cam_dist, 1.0));
   vec3 dir = normalize(vec3(cam_mat*vec4(uv, 1., 0.)));

   light_pos += vec3(cam_mat*vec4(0., 0., -cam_dist + 3.0, 1.0));
   
   vec3 norm;
   
   float ray_len = 0.0, light_gather = 0.0;
   vec4 hit = trace(pos + dir*.25, dir, ray_len, light_gather);
   vec4 col = shade(dir, ray_len, light_gather, hit, norm);
   
   if (hit.w > 0.0) {
      dir = reflect(dir, norm);
      pos = hit.xyz + dir*.01;
      light_gather = 0.0;
      vec4 hit = trace(pos, dir, ray_len, light_gather);
      col = mix(col, shade(dir, ray_len, light_gather, hit, norm), .25);
   }
   
   fragColor = col;
}
