
vec2 pixpos;
vec4 background;

vec2 rotate(vec2 v, float degree){
    float c = cos(degree/180.0*3.415);
    float s = sin(degree/180.0*3.415);
	return vec2(v.x*c - v.y*s, v.x*s + v.y*c);
}

float box(vec2 pos, vec2 size){
    size -= vec2(0.01);
    vec2 q = abs(pos - pixpos);
    return length(max(q - size, 0.0)) - 0.01;
}

float boxrot(vec2 pos, vec2 size, float r){
    size -= vec2(0.01);
    vec2 q = abs(rotate(pos - pixpos, r));
    return length(max(q - size, 0.0)) - 0.01;
}


float sphere(vec2 pos, float radius){
    return length(pos - pixpos) - radius;
}

float add(float a, float b){
    return min(a, b);
}

float subtract(float a, float b){
    return max(a, -b);
}

vec4 select(float a, vec4 inner, vec4 outer){
    //return a > 0.0 ? outer : inner;
    return mix(inner, outer, smoothstep(0.0, 0.01, a));
}
    
vec4 select(float a, vec4 inner){
    return select(a, inner, vec4(0, 0, 0, 0));
}

vec2 supershapes(vec4 n, float phi){
	float m = n.x * phi * 0.25;
	float t1 = pow(abs(cos(m)), n.z);
	float t2 = pow(abs(sin(m)), n.w);
	float radius = 1.0 / pow(t1 + t2, 1.0 / n.y);
	return vec2(cos(phi), sin(phi)) * radius;
}

vec2 gbase;

vec4 G(){
    vec2 base = vec2(0.1, 0.5) + gbase;
    
    float gcircle = sphere(base, 0.5);
    float ginnercircle = sphere(base, 0.4);
    float gbox = box(base+vec2(0.25, 0.0), vec2(0.24, 0.05));
    float grotbox = boxrot(base+vec2(0.37, 0.2), vec2(0.2, 0.2), -40.0);

    float g = gcircle;
    g = subtract(g, ginnercircle);
    g = subtract(g, grotbox);
    g = add(g, gbox);
	return select(g, vec4(0.3, 0.5, 1, 1));	
}

vec4 o(){
    vec2 base = vec2(1.0, 0.3) + gbase;
    
    float circle = sphere(base, 0.3);
    float innercircle = sphere(base, 0.2);
 
    float g = circle;
    g = subtract(g, innercircle);
	return select(g, vec4(1, 0.2, 0.0, 1));	
}

vec4 o2(){
    vec2 base = vec2(1.7, 0.3) + gbase;
    
    float circle = sphere(base, 0.3);
    float innercircle = sphere(base, 0.2);
 
    float g = circle;
    g = subtract(g, innercircle);
	return select(g, vec4(0.9, 1.0, 0.0, 1));	
}

vec4 g(){
    vec2 base = vec2(2.4, 0.3) + gbase;
    
    float circle = sphere(base, 0.3);
    float innercircle = sphere(base, 0.2);
    float rbox = box(base+vec2(0.25, 0.0), vec2(0.05, 0.3));
    
    float circle2 = sphere(base+vec2(0, -0.3), 0.3);
    float innercircle2 = sphere(base+vec2(0, -0.3), 0.2);
    
    float vbox = boxrot(base+vec2(-0.2, -0.2), vec2(0.2, 0.6), 60.0);
 
    float g = circle;
    g = subtract(g, innercircle);
    g = add(g, rbox);
    g = add(g, subtract(subtract(circle2, innercircle2), vbox));
	return select(g, vec4(0.3, 0.5, 1, 1));	
}

vec4 l(){
    vec2 base = vec2(2.9, 0.45) + gbase;
    
    float lbox = box(base, vec2(0.05, 0.4));

    float g = lbox;
	return select(g, vec4(0.4, 1.0, 0.0, 1));	
}

vec4 e(){
    vec2 base = vec2(3.4, 0.3) + gbase;
    
    float circle = sphere(base, 0.3);
    float innercircle = sphere(base, 0.2);
    
    float nbox = boxrot(base+vec2(0.012, 0.0), vec2(0.287, 0.05), -15.0);
    float sbox = boxrot(base+vec2(0.29, -0.07), vec2(0.15, 0.13), 30.0);
 
    float g = circle;
    g = subtract(g, innercircle);
    g = subtract(g, sbox);    
    g = add(g, nbox);
	return select(g, vec4(1, 0.2, 0.0, 1));	
}

vec4 bg(vec2 fragCoord )
{
    gbase = vec2(1, 0);
    pixpos = fragCoord.xy / iResolution.yy;
    pixpos -= vec2(1.0, 0.2);
    pixpos = supershapes(vec4(length(pixpos)*10.0, 1, 1, 1), atan(pixpos.y, pixpos.x)) + (fragCoord.xy / iResolution.yy) * 1.0;
    pixpos = pixpos * (9.0 + sin(iGlobalTime)*4.0) - vec2(0.5, 0.5);
    pixpos = mod(pixpos, vec2(5.0, 2.0));
	return G() + o() + o2() + g() + l() + e();
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    gbase = vec2(1, 0);
    pixpos = (fragCoord.xy / iResolution.yy) * 1.5 + vec2(0.0, -0.3);
    pixpos -= vec2(sin(iGlobalTime*0.5)*3.0, 0.0);
	fragColor = G() + o() + o2() + g() + l() + e() + bg(fragCoord);
}

    
    