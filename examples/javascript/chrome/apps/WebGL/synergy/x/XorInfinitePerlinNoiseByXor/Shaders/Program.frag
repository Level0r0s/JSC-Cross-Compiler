﻿float r(vec2 n)
{
    return fract(cos(dot(n,vec2(36.26,73.12)))*354.63);
}
float noise(vec2 n)
{
    vec2 fn = floor(n);
    vec2 sn = smoothstep(vec2(0),vec2(1),fract(n));
    
    float h1 = mix(r(fn),r(fn+vec2(1,0)),sn.x);
    float h2 = mix(r(fn+vec2(0,1)),r(fn+vec2(1)),sn.x);
    return mix(h1,h2,sn.y);
}
float perlin(vec2 n)
{
    float total;
    total = noise(n/32.)*0.5875+noise(n/16.)*0.2+noise(n/8.)*0.1+noise(n/4.)*0.05+noise(n/2.)*0.025+noise(n)*0.0125;
 	return total;
}
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	fragColor = vec4(vec3(perlin(iGlobalTime*16.+fragCoord.xy/4.)),1.0);
}