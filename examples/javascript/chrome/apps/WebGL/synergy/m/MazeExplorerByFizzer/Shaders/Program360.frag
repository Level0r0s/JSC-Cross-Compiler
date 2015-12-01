
float time=iGlobalTime;

#define TL  30.0
#define TL2 33.0

uniform vec3 uCameraTargetOffset;   

float hash(float n)
{
    return fract(sin(n)*43758.5453);
}

float noise(vec2 p)
{
    return hash(p.x + p.y*57.0);
}

float smoothNoise(vec2 p)
{
    float n0=noise(floor(p)+vec2(0,0));
    float n1=noise(floor(p)+vec2(1,0));
    float n2=noise(floor(p)+vec2(0,1));
    float n3=noise(floor(p)+vec2(1,1));
    float u=fract(p.x),v=fract(p.y);
    return mix(mix(n0,n1,u),mix(n2,n3,u),v);
}

bool blocked(vec2 p)
{
    float r=acos(-1.)*2./8.;
    vec2 p2=(p-.5)*mat2(cos(r),sin(r),sin(r),-cos(r))*length(vec2(1));
    vec2 c=floor(p2); 
    bool s=step(.5,noise(c))>.5;
    bool hv=mod(c.x+c.y,2.)>.5;
    return hv==s;
}

float mazeDist(vec3 p)
{
    vec3 cp=fract(p)-vec3(.5),acp=abs(cp);
    float r=acos(-1.)*2./8.;
    vec2 p2=(p.xz-.5)*mat2(cos(r),sin(r),sin(r),-cos(r))*length(vec2(1));
    vec2 c=floor(p2),f=fract(p.xz);
    float a=step(.5,noise(c));
    float s=.1;
    if(a>.5)
        return acp.x-s;
    return acp.z-s;
}

float f(vec3 p)
{
    return min(length(vec2(max(0.,mazeDist(p)),p.y-.04))-.08,p.y);
}

vec2 startPoint(float t)
{
    return vec2(floor(t)*7., floor(t)*3.);
}

vec2 startDirection(float t)
{
    return vec2(-1.+mod(t*2.,2.),0.);
}

float cubic(float x)
{
    return (3.*x-2.*x*x)*x;
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
	//iGlobalTime
    //iMouse
    //iResolution
    
	vec2 uv = fragCoord.xy / iResolution.xy;
	vec2 uv2 = 2.0*fragCoord.xy / iResolution.xy - 1.0;
	
	//uv2 *= 0.5;
	//uv2 /= 0.5;
	//uv2 *= .1 * 3600.0 / 60.0;
	//uv2 /= 0.55;
	//uv2 /= 0.01 * iGlobalTime ;
	uv2 /= 0.01 * 3333.0 / 60.0;
	// .55 ?

	//uv2 /= 0.01 * 4500.0 / 60.0;
	//uv2 /= 0.01 * 3600.0 / 60.0;
	//uv2 /= 0.01 * 6000.0 / 60.0;

    //vec2 c=vec2(fragCoord/iResolution.xy-.5)*2.;
    
	vec2 c=uv2;
	
	//vec2 c=vec2(fragCoord/iResolution.xy-.5)*1.;
    //c.x*=iResolution.x/iResolution.y;
	//c /= 0.25;



    //float time2=time*.7;
    float time2=time*1.0;
    //float tsec=floor(time2/TL2);
    float tsec=0.;
    vec2 bp=startPoint(tsec),pbp=bp;
    //vec2 bd=startDirection(tsec),pbd=bd;
    vec2 bd0=startDirection(tsec),pbd=bd0;
	vec2 bd=bd0;

    //vec2 bd=startDirection(0.0),pbd=bd;

    float st=mod(time2,TL2);

    for(int i=0;i<int(TL);++i)
    {
        if(i>int(floor(st)))
            break;
        vec2 od=bd.yx*vec2(1.,-1.)*(1.-2.*step(.5,hash(float(i))));
        pbp=bp;
        pbd=bd;
        if(!blocked(bp+bd/2.))
        {
            bp+=bd;
            if(!blocked(bp+od/2.))
                bd=od;
        }
        else
		{
            //this wont help with the direction flip after the flight..
            bd=bd.yx*vec2(1,-1);
		}
    }

    bp=mix(pbp,bp,fract(min(st,TL-1e-4)));
    bd=normalize(mix(pbd,bd,fract(min(st,TL-1e-4))));
    float ry=.5;
	/* what does this do? nothing?? */
    if(floor(st)>=TL)
    {
        float t=(st-TL)/(TL2-TL);
        bp=mix(bp,startPoint(tsec+1.),cubic(t));
        float ba=mix(atan(bd.y,bd.x),atan(startDirection(tsec+1.).y,startDirection(tsec+1.).x),cubic(t));
        bd=vec2(cos(ba),sin(ba));
        ry+=1.*sin(t*acos(-1.));
    }
	/* */

    vec3 up=vec3(0,1,0);
    //float p=-.3;
	float p=-.0; // keep horizon line
    vec2 yz=vec2(c.y,1.8)*mat2(cos(p),sin(p),-sin(p),cos(p));;
    
	//vec3 rd=normalize(yz.x*up+yz.y*vec3(bd.x,0,bd.y)-cross(up,vec3(bd.x,0,bd.y))*c.x);
	vec3 rd=normalize(yz.x*up+yz.y*vec3(bd0.x,0,bd0.y)-cross(up,vec3(bd0.x,0,bd0.y))*c.x);
    
	vec3 ro=vec3(bp.x,ry,bp.y);


		mat3 camRotate = 
	
		// bottom
		(uCameraTargetOffset.y == -1.0) ? rotationMatrix(vec3(0., 0., 1.), radians(90.0)) 
		 * rotationMatrix(vec3(0., 1., 0.), radians(90.0))
		:

		// top
		(uCameraTargetOffset.y == 1.0) ? rotationMatrix(vec3(0., 0., 1.), radians(-90.0)) 
		* rotationMatrix(vec3(0., 1., 0.), radians(90.0))
		:
	
		rotationMatrix(vec3(0., 1., 0.), radians(
        
		
		// left
		(uCameraTargetOffset.z == 1.0) ? 270. :

		// right
		(uCameraTargetOffset.z == -1.0) ? 90. :

		// back
		 (uCameraTargetOffset.x == -1.0) ?  180. : 
		
		
		// front
		/* (uCameraTargetOffset.x == 1.0) ? */ 0. 

    ));

	rd *= camRotate;




    float t=0.,d=0.;
    for(int i=0;i<120;++i)
    {
        d=f(ro+rd*t);
        if(d<1e-3||t>18.)
            break;
        t+=d;
    }

    if(t>18.)
    {
        fragColor.rgb=mix(vec3(1),vec3(.6,.6,1),rd.y);
        return;
    }

    vec3 rp=ro+rd*t;
    float eps=1e-1;
    float d2=clamp((f(rp+normalize(vec3(-1,1,1.5))*eps)-d)/eps,-1.,1.);

    float eps2=1e-3;
    float d22=clamp((f(rp+normalize(vec3(-1,1,1.5))*eps2)-d)/eps2,-1.,1.);

    float eps3=2e-1;
    float d23=clamp((f(rp+normalize(vec3(0,1,0))*eps3)-d)/eps3,-1.,1.);

    float eps4=2e-1;
    float d24=clamp((f(rp+normalize(rd)*eps4)-d)/eps4,-1.,1.);

    vec3 irr=.5*vec3(1,1,.8)*(mix(.5,1.,1.-clamp(d22-d2,0.,1.)))*(.5+.5*d22);

    irr+=.5*vec3(.8,.85,1.)*pow(.5+.5*d23,3.);

    vec3 alb=mix(vec3(1.),vec3(.4,.35,.3)*1.4,step(rp.y,.1e-2))*mix(.7,1.,step(fract(rp.x*2.+.5*step(.5,fract(rp.z*2.))),.5));

    vec3 spec=vec3(pow(clamp(.6+.25*d24,0.,1.)*1.4,16.))*14.*
        (smoothNoise(rp.xz*256.)/2.+smoothNoise(rp.xz*512.)/3.);

    fragColor.rgb=sqrt(mix(vec3(1),alb*irr+spec,exp2(-t/25.)));
}
