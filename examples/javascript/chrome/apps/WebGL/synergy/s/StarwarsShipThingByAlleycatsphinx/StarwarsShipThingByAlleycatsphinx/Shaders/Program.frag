//ported from http://glslsandbox.com/e#24863.4
#define resolution 				iResolution.xy
#define time 					iGlobalTime
#define mouse 					iMouse.xy

#define ASPECT               	resolution.x/resolution.y
#define PHI                  	.0001
#define EPSILON                 .00002
#define FOV                     2.
#define FARPLANE                12.
#define ITERATIONS              128

#define PI                  	(4.*atan(1.))       
#define TAU                 	(8.*atan(1.))   

struct ray
{
	vec3 origin;
	vec3 position;
	vec3 direction;
	vec2 material_range;
	float steps;
}; 

ray         view(in vec2 uv);   
ray         emit(ray r);
vec2        map(in vec3 position);
vec3        derive(in vec3 p);

float       sphere(vec3 position, float radius);
float       cube(vec3 position, vec3 scale);
float       torus( vec3 p, vec2 t );
float       cylinder(vec3 p, float l, float r);
float       cone(vec3 p, float l, vec2 r);
float       icosahedral(vec3 p, float e, float r);
float       partition_noise(vec2 uv);
float       cross(float x);

float       hash(float x);
vec2        hash(vec2 v);

mat2        rmat(in float r);

vec2        format_to_screen(vec2 uv);

//overly complex viewing system for modeling - doesnt work on shadertoy due to shader inputs
#define SCREEN_LEFT gl_FragCoord.x < resolution.x * .5
#define SCREEN_BOTTOM   gl_FragCoord.y < resolution.y * .5
#define MOUSE_LEFT  mouse.x < .5
#define MOUSE_BOTTOM    mouse.y < .5
#define VIEW_SWITCH mouse.x > .02   
#define PANEL_LEFT  (VIEW_SWITCH ? MOUSE_LEFT : SCREEN_LEFT)
#define PANEL_BOTTOM    (VIEW_SWITCH ? MOUSE_BOTTOM : SCREEN_BOTTOM)
#define UV      VIEW_SWITCH ? (gl_FragCoord.xy/resolution.xy) : fract(2.*(gl_FragCoord.xy/resolution.xy))
#define TOP         vec3(0., 7., .49)
#define TOP_V       vec3(0., 0., .5001)
#define BOTTOM      vec3(0., -7., .49)
#define BOTTOM_V    vec3(0., 0., .5001)
#define SIDE        vec3(4.5, 0., 0.5)
#define SIDE_V      vec3(0., 0., 0.5)
#define BACK        vec3(0., 0., 6.)
#define BACK_V      vec3(0., 0., 0.001)
#define QUARTER     vec3(-2.5, 2.5, -2.75)
#define ORIGIN_V    vec3(0., 0., 0.001)
#define TURRET	    vec3(1.5, 0.4, 2.5)
#define TURRET_V    vec3(0., 0., 0.5)


#define VIEWPOSITION    (PANEL_LEFT ? PANEL_BOTTOM ? TOP   : BACK   : PANEL_BOTTOM ? SIDE   : QUARTER)
#define VIEWTARGET      (PANEL_LEFT ? PANEL_BOTTOM ? TOP_V : BACK_V : PANEL_BOTTOM ? SIDE_V : ORIGIN_V)

//#define VIEWPOSITION      SIDE + vec3(0., 0., 2.)
//#define VIEWTARGET        SIDE_V  + vec3(0., 0., -2.)
//#define VIEWPOSITION      TURRET
//#define VIEWTARGET        TURRET_V
//#define VIEWPOSITION      BOTTOM
//#define VIEWTARGET        BOTTOM_V

vec2 map(in vec3 position)
{    
	//panel aware view rotations
	if(true){
		if(PANEL_BOTTOM && PANEL_LEFT)
		{
			position.zy *= rmat(mouse.x*12.56);
		}
		else
		{ 
			position.xz *= rmat(mouse.x*12.56);
		}
	}
	position.z                  	+= 1.5;
	
	float ship                  	= FARPLANE;
	

	//main hulls
	vec3 hull_position          	= position;
	hull_position.x             	= max(abs(position.x),.04);
	
	//laterial insets 
	hull_position.x             	*= hull_position.z > .1     
	&& hull_position.z < 1.1 
	? 1.05 : 1.;

	hull_position.x             	*= hull_position.z > 1.85   
	&& hull_position.z < 2. 
	? 1.025 : 1.;
	
	hull_position.y             	= abs(hull_position.y)*.75+position.z*.001;
	
	
	vec3 hull_scale             	= vec3(1.);
	hull_scale.x                	= position.z*.3+.45, 
	hull_scale.y                	= position.z*.05+.085;
	hull_scale.y               	+= -abs(max(abs(position.x*.87),position.z*.025)*.187);
	hull_scale.z               	= min(hull_position.y+4., 4.);
	hull_scale.z               	+= abs(position.y);
	
	
	//inner hull
	vec3 hull_inner_scale           = hull_scale;
	hull_inner_scale.x          	+= abs(position.x)*.03-.01-position.z * .0005;
	hull_inner_scale.y          	= position.z < 4.1 ? .05 : hull_inner_scale.y;
	hull_inner_scale.z          	+= abs(position.x)*.012 + .15;
	
	vec3 hull_inner_position        = hull_position;    
	hull_inner_position.y           = abs(position.y)<abs(hull_position.y) ? position.y : hull_position.y; //delete?

	float hull_inner        	= cube(hull_inner_position, hull_inner_scale);
	
	
	vec3 hull_outer_scale           = hull_scale * vec3(1.05,1.05, 1.031);
	hull_outer_scale.y         	+= position.y > .5 ? -position.z * .025 : 0.;
	
	hull_position.y             	= abs(hull_position.y-.05+position.z*.0025);
	
	float hull_outer            	= cube(hull_position, hull_outer_scale);

	hull_outer                  	= max(hull_outer, -hull_inner);
	ship                        	= min(hull_outer, ship);    

	bool inner_hull             	= hull_inner < hull_outer;
	bool outer_hull             	= hull_inner > hull_outer;
	
	bool engines            	= false;
	bool bridge_tower	        = false;
	bool aft 			= false;
	bool domes 			= false;
	
	//hull details
	float forward_partitions        = partition_noise(floor(position.z*8.)/8.+position.yx*8.);
	float deck_partitions		= partition_noise(floor(position.x*8.)/8.+position.yz*8.);
	
	float hull_plating          	= partition_noise
					(
					floor(position.xz*31.)/31.
					+floor(position.xz*63.)/63.
					+position.xz*2.5-.41
					+abs(position.z)*.2
					);
	
	float hull_panels       	= max(hull_plating, .85) * 4.; 
	float forward_panels       	= max(forward_partitions, .85);
	
	float z_floor_noise        	= hash
					(
					floor(position.z*13.+position.z)/8.
					+floor(position.z*5.)/3.
					);
	
	z_floor_noise         		= floor(z_floor_noise*8.)/8.;
	
	float outboard_partitions	= partition_noise
					(
					floor(position.x*16.)/8. +
					z_floor_noise +
					position.yz * 8.*
					vec2(3.5, 1.2*(1.+hull_plating*.05))
					);

	
	//aft hull && engines
	if(inner_hull && hull_position.z > 4.0 && hull_position.y < .375)
	{   
		//aft structures
		aft                 		= true;
		
		hull_inner			= max(hull_inner-.05, -hull_outer);
		vec2 absxy			= abs(position.xy*1.1);
		bool columns			= mod(absxy.x-.25, .85) > .3625;
		bool spars			= mod(absxy.x+absxy.y, .25) > .025 ^^ mod(absxy.x-absxy.y, .25) > .025;

		hull_inner_scale.z		+= columns || spars ? .00 : -.0125;     

		hull_inner			= cube(hull_inner_position, hull_inner_scale);
		hull_inner			+= columns && !spars ? -.05 : -0.;
		hull_inner			+= !columns && !spars ? fract((absxy.x-absxy.y)*16.+.9)*0.01 : 0.;
		hull_inner			+= spars ? 0.01 : 0.05;
		hull_inner			+= !spars && columns ? forward_panels*.005 : 0.;
		hull_outer			= max(hull_outer,-hull_inner);

		//main engines
		vec3 engine_position    	= position.yzx;
		engine_position.z       	= abs(engine_position.z) < .4 ? engine_position.z : abs(engine_position.z)-.8;
	
		engine_position         	+= -vec3(0.,4.1,0.);
		float engine_angle      	= atan(engine_position.x, engine_position.z)/TAU;
			
		float engine_contour    	= clamp(engine_position.y*.5,.05,.15);
		engine_contour          	= engine_position.y > .35 ? .2 : engine_contour;
			
		vec2 radii          		= vec2(engine_contour, .25);
		float depth         		= .21;
		float engine           	 	= cone(engine_position, depth, radii);
	
		engine              		+= fract(engine_position.y*8.)>.2 ? .01 : 0.;
		engine              		+= fract(engine_angle*16.)>.1 ? .01 : 0.;
		engine_position.y      	 	+= -.125;
		float engine_mask      	 	= cone(engine_position, depth, radii);
		engine_mask            	 	+= fract(engine_position.y*8.)>.2 ? .01 : 0.;
		engine              		= max(engine, -engine_mask);    
		
		//booster engines
		vec3 booster_position   	= position.yzx;
		booster_position.x      	= abs(booster_position.x)-.165;
		booster_position.z      	= abs(booster_position.z)-.4;
		booster_position        	+= vec3(0.,-4.15,0.);
		float booster_radius    	= length(booster_position.xz);
		float booster_angle     	= atan(booster_position.x, booster_position.z)/TAU;
		float booster_contour   	= booster_position.y > .1 ? .115 : .15;
		float booster_length    	= booster_radius < .065 ? .12  : .195 - booster_radius * booster_position.y;
		float booster           	= cylinder(booster_position, booster_length , booster_contour);
		
		booster             		+= fract(booster_position.y*8.)>.8 ? .01 : 0.;
		booster            		+= fract(booster_angle*16.)>.1 ? .01 : 0.;

		float engines         		= min(engine, booster);
		ship               		= min(engines, ship);
	}
	
	//conning tower
	float tower = FARPLANE;
	if(position.y > 0.)
	{                   
		//bridge
		vec3 bridge_position            = hull_position;
		bridge_position.y       	+= -hull_position.z * .05 - .46;
		bridge_position.z          	+= -3.8;
		
		vec3 bridge_scale          	= vec3(1.);
		bridge_scale.x             	= -bridge_position.z*.05+.8;
		bridge_scale.x             	+= abs(bridge_position.y) < .035 ? outboard_partitions*-.025 : 0.;
		bridge_scale.y			= -abs(bridge_position.x)*.1+.125;
		bridge_scale.z			= -bridge_position.z*.01-abs(bridge_position.x)*.15+.25;
		
		float bridge			= cube(bridge_position, bridge_scale);
		
		vec2 bridge_detail		= hash(floor(hull_position.xy*8.)+floor(hull_position.xy*18.))+.25;
		
		vec3 bridge_decks_position      = bridge_position;
		bridge_decks_position.z         = mod(bridge_position.z, .0625)-.00375;
		bridge_decks_position.z         = max(abs(bridge_decks_position.z)*2., abs(bridge_position.z)-bridge_detail.x*.1)+.05;
		float bridge_decks          	= cube(bridge_decks_position, bridge_scale);
		bridge_decks                	+= -forward_panels*.001;
		
		//sensor bar
		vec3 sensor_bar_position        = bridge_position;
		sensor_bar_position.y           += -.22;
		sensor_bar_position.z           += -.07;
		
		vec3 sensor_bar_scale           = vec3(1.);
		sensor_bar_scale.x          	= .4;
		sensor_bar_scale.y          	= sensor_bar_position.z*.01+.015;
		sensor_bar_scale.z          	= -abs(position.x)*.025+.075;
	
		float sensor_bar            	= cube(sensor_bar_position, sensor_bar_scale);
		sensor_bar                  	+= fract(sensor_bar_position.x*8.-.1)>.8 ? -.0025 : 0.;
		sensor_bar                  	+= fract(sensor_bar_position.z*8.-.3)>.4 ? .0035 : 0.;
		
		
		vec3 sensor_bar_base_position   = sensor_bar_position;
		sensor_bar_base_position.y      += .08;
		sensor_bar_base_position.z      += -.02;
		
		vec3 sensor_bar_base_scale      = vec3(1.);
		sensor_bar_base_scale.x         = -sensor_bar_position.y*.5-sensor_bar_position.z*.6-abs(sensor_bar_position.x)*.25+.05;
		sensor_bar_base_scale.y         = .074;
		sensor_bar_base_scale.z         = .08+sensor_bar_position.y*-.5;
		sensor_bar_base_scale.z         += abs(sensor_bar_base_position.x)<.1
						&& abs(sensor_bar_base_position.y-.025)<.015            
						&& sensor_bar_base_position.z<-.1
		? -.005 : 0.;       
		
		float sensor_bar_base           = cube(sensor_bar_base_position, sensor_bar_base_scale);        
		
		sensor_bar                  	= min(sensor_bar, sensor_bar_base);
		
		//ray domes
		vec3 ray_dome_position          = bridge_position;  
		ray_dome_position.z         	+= .06;
		ray_dome_position.y         	+= -.16;
		ray_dome_position.y         	*= 1.3;
		ray_dome_position.x         	= abs(ray_dome_position.x)-.625;
			
		float ray_domes             	= icosahedral(ray_dome_position*1.1, 24., .11);
			
		vec3 support_position       	= ray_dome_position;
		support_position.y          	+= .07;
		float support               	= cube(support_position, vec3(.06));        
		float support_mask          	= cube(support_position, vec3(.065,.1,.065));       
			
		vec3 absp                   	= abs(support_position);
		bool struts                 	= absp.x<.06 ^^ absp.z>.06; 
		bool spars                  	=  fract(abs(absp.x-absp.y))>.02
						&& fract(abs(absp.z-absp.y))>.02;
		
		support                     	+= spars && !struts ? .01 : -.015;
	
		support                     	= max(support, -support_mask);
			
		//tower 	
		vec3 tower_position         	= hull_position;
		tower_position.z            	+= -4.045;
		tower_position.z            	+= tower_position.z > 4. ? tower_position.y * .3 : 0.;
		tower_position.y        	+= -.51;
		
		vec3 tower_scale            	= vec3(1.);
		tower_scale.x               	= -tower_position.y*.1+.15;
		tower_scale.y               	= .2;
		tower_scale.z               	= tower_position.z*.05;
		tower_scale.z               	+= tower_position.z > .2 ? -tower_position.y*.6+.4 : tower_position.y*.25+.3;
		tower_scale.x               	+= fract(tower_position.y*16.-z_floor_noise-.3) > .6 - z_floor_noise * .05 
						&& tower_position.z + tower_position.y * .5 - z_floor_noise * .15 < .15
						&& tower_position.y < .14
						? outboard_partitions * -.015 : 0.;
		
		float tower                 	= cube(tower_position, tower_scale);
			
			
		vec3 tower_base_position    	= tower_position;
		tower_base_position.y       	+= .15;
		tower_base_position.z       	+= .05;
			
		vec3 tower_base_scale       	= tower_scale;
		tower_base_scale.x          	= -tower_position.y*.13+.19;
		tower_base_scale.z          	+= -.1;
		tower_base_scale.y          	= tower_position.z*.05+.1;
			
		float tower_base            	= cube(tower_base_position, tower_base_scale);
		
		float tower_vents       	= position.z > 3.8 ? fract(position.y*32.)*.05 : 0.;
		
		tower                       	+= abs(position.x) < .25-position.y*.1-.07 
		&& position.y < .93
		? tower_vents : 0.;
			
		//compositing
		tower                   	= min(tower, tower_base);       
		bridge_decks               	= min(bridge-.01, bridge_decks+.01);    
		bridge_decks               	= min(bridge_decks, support);
		ray_domes                  	= min(ray_domes, sensor_bar);
		domes                      	= ray_domes < bridge;
		tower                      	= min(tower, bridge_decks);
		bridge                     	= min(bridge, ray_domes); 
		tower                      	= min(bridge,tower);
		bridge_tower               	= tower < ship;
		
		ship                		= min(tower, ship);
	}
	hull_inner              	= max(hull_inner, -tower);
	hull_inner             		= max(hull_inner, -tower);
	ship                		= min(ship, hull_inner);


	//solar collector (round thing on the bottom)
	vec3 collector_position     	= position;
	collector_position.z        	+= -2.8;
	collector_position.xz       	*= rmat(3.14/4.);
	collector_position.y        	+= abs(collector_position.x)-.0125 < 0. 
					|| abs(collector_position.z)-.0125 < 0.
					? -.05 : 0.;

	collector_position.y 		*= 2.;
	collector_position.y    	+= -hull_plating * .005 + .45;
	
	float collector_scale       	= position.y < -hull_scale.y ? .45 : 0.;
	
	float collector             	= sphere(collector_position, collector_scale);
	bool solar_collector        	= collector < ship;
	
	
	//super structure for upper decks
	vec3 deck_position      	= hull_position;
	vec2 absxz              	= abs(position.xz*3.);
	if(position.y > 0. && !inner_hull)
	{
		bool indents    	= fract(absxz.y*.35 + .2)>.1;
		
		//outboard offset
		deck_position.x 	+= deck_position.z - deck_position.x * .06 > 1. + deck_position.x * .15    
					&& deck_position.z < 3.91 - deck_position.x * .985
					&& deck_position.x - deck_position.z * .135 < .2
					? -.3 : 0.;
	
		//lower decks	
		deck_position.y 	+= deck_position.z - deck_position.x > 1.75 
					&& deck_position.z > 1.285
					&& deck_position.z + deck_position.x *.3 < 4.45   
					&& deck_position.x < .4 + deck_position.z * .15
					? -.025 : 0.;
			
		//gun decks	
		deck_position.y 	+= deck_position.z - deck_position.x > 1.65 
					&& deck_position.z > 2.5
					&& deck_position.z + deck_position.x *.5 < 4.38
					&& deck_position.x < deck_position.z * .25 +.4
					? -.015 : 0.;

		//turrets
		vec3 turret_position    = hull_position - vec3(position.z * .2-.15,.045+hull_position.z*.01, 3.23);
		
		turret_position.z   	= abs(abs(abs(turret_position.z)-.125)-.125)-.125;
		turret_position.x   	= abs(turret_position.x)-.5;
			
	
		vec2 turret_radius  	= vec2(.02, .02);
		turret_radius.x     	= turret_position.y > .01 ? .04 : .01;
		turret_radius.y     	= abs(turret_position.z) > .015 
					|| turret_position.x > .0025 
					? .001 : turret_radius.y+.005;
		
		float turret_depth  	= .025;
		
		float turret        	= cone(turret_position, turret_depth, turret_radius);
		vec3 barrel_position    = turret_position;
		barrel_position.y   	+= -.045;

		
		float barrel       	= cube(barrel_position, vec3(.03,0.005,.005));
		turret          	= min(turret, barrel);

		deck_position.y     	+= turret < .05 ? .01 : 0.;   
		ship            	= position.y > 0. ? min(ship, turret) : ship;
		
		
		//mid decks
		deck_position.y 	+= deck_position.z - deck_position.x        > 2.25 
					&& deck_position.z + deck_position.x *.4    < 4. 
					&& deck_position.x + deck_position.z * .1   > .26    
					&& deck_position.x < .5 
					? -.05 : 0.;
		
		
		//upper decks
		deck_position.y 	+= deck_position.z - deck_position.x * .125 > 2.9
					&& deck_position.z + deck_position.x *.4    < 3.9 
					&& deck_position.x              	    < .3 
					? -.02 : 0.;
		
		//midship deck
		deck_position.y 	+= deck_position.z - deck_position.x * .3   > .75
					&& deck_position.z + deck_position.x        < 2.19
					&& deck_position.x < .6 
					&& deck_position.x > -.25
					? -.01 : 0.;
		
		//midship hatches
		deck_position.y 	+= deck_position.z - deck_position.x * .3   > .85
					&& deck_position.z + deck_position.x        < 1.9
					&& deck_position.x < .55 
					&& deck_position.x > .25
					? -.01 : 0.;
	
	
		//bulwarks
		deck_position.y 	+= deck_position.z - deck_position.x        > .092
					&& deck_position.x - deck_position.z *.283  < .5
					&& deck_position.z  < 4.
					&& deck_position.x-deck_position.z*.25  > -.25
					&& indents
					? -.005 * hull_position.x : 0.;
		
		
		//sensor deck
		deck_position.y 	+= deck_position.z - deck_position.x * .5   > -.48  
					&& deck_position.z + deck_position.x        < .34   
					&& deck_position.x              < .28
					? -.014  : 0.;
		
		
		//forecastle
		deck_position.y 	+= deck_position.z - deck_position.x * 5.5   > -1.45  
					&& deck_position.z + deck_position.x * 2.    < -.54   
					? -.01  : 0.;
	
		//upper deck superstructure
		vec3 deck_upper_position= hull_position;
		deck_upper_position.y   = hull_position.y- .15 - deck_position.y*.05;
		deck_upper_position.z   += -3.16;
		
		vec3 deck_upper_scale   = vec3(1.);
		deck_upper_scale.x      = hull_scale.x-deck_position.x-.8;
		deck_upper_scale.x      += deck_position.z < 2.7 ? deck_upper_position.z : 0.;
		deck_upper_scale.x      += deck_position.z > 3.9 ? -deck_upper_position.z*2.7+2. : 0.;
		deck_upper_scale.x      += deck_partitions*.025;
		deck_upper_scale.y      = hull_scale.y;
		deck_upper_scale.z      = .55 - forward_partitions*.015;
		deck_upper_scale.z      += deck_position.z > 3. ? .4 : 0.;
		
		bool upper_deck_bounds  = deck_position.z - deck_position.x * .3   > .5
					&& deck_position.z + deck_position.x            < 1.9
					&& deck_position.x < .55 
					&& deck_position.x > .15;
		
		deck_upper_scale.x  	*= !upper_deck_bounds ? 1. : 2.4;
		
		float deck_upper    	= cube(deck_upper_position, deck_upper_scale);
		
		
		//mid deck superstructure
		vec3 deck_mid_position  = hull_position;
		deck_mid_position.y     = hull_position.y - .1;
		deck_mid_position.z     -= 3.16;
		
	
		
		vec3 deck_mid_scale     = vec3(1.);
		deck_mid_scale.x        = hull_scale.x-deck_position.x;
		deck_mid_scale.x        += deck_position.z < 2.7 ? deck_mid_position.z : 0.;
		deck_mid_scale.x        += deck_position.z > 3.9 ? -deck_mid_position.z*2.7+2. : 0.;
		deck_mid_scale.x        += deck_partitions*.025;        
		deck_mid_scale.y        = hull_scale.y;        
		deck_mid_scale.z        = .93 - forward_partitions*.015;
		
		bool mid_deck_bounds    = deck_position.z - deck_position.x * .3   > .5
					&& deck_position.z + deck_position.x            < 1.9
					&& deck_position.x < .55 
					&& deck_position.x > .15;
		
		deck_mid_scale.x	*= !mid_deck_bounds ? 1. : 2.4;
	
		float deck_mid          = cube(deck_mid_position, deck_mid_scale);
	
		ship                    = min(ship, deck_upper);
		ship                    = min(ship, deck_mid);
	}
	else
	{
	
		//solar collector berth
		deck_position.y 	*= deck_position.z - deck_position.x    > 1.9
					&& deck_position.z + deck_position.x    < 3.65
					&& deck_position.x                      < deck_position.z * .23 
					? .9 : 1.;
		
		deck_position.y 	+= length(collector_position)-.55 < 0. ? .05 : 0.;
			
		vec2 absxz      	= abs(position.xz*8.);
		bool indents    	= fract(absxz.y*.35+.5)>.1;
		bool columns    	= mod(absxz.y-.25-position.y, 1.) > .5 && abs(position.x) > .10;
		bool spars      	= mod(absxz.x+absxz.y, 1.25) > .025 ^^ mod(absxz.x-absxz.y, 1.25) > .025;
		
		
		//fighter bays
		deck_position.y 	+= deck_position.z - deck_position.x        > .92
					&& deck_position.x - deck_position.z *.25   < .3
					&& deck_position.x -deck_position.z*.25     > .15
					&& deck_position.z                          < 4.
					&& indents && !spars
					? .01 * hull_position.x : 0.;
		
		
		//forward bay
		deck_position.y 	+= deck_position.z - deck_position.x * .5   > -.7   
					&& deck_position.x          < .2     
					&& deck_position.z          < -.3    
					? -.0125 - hull_plating * .0025 : 0.;      
		
		//main docking bay border
		deck_position.y 	+= deck_position.z  > .25
					&& deck_position.z  < 1.25
					&& deck_position.x  < .45
					? -.01 : 0.;
		
		//main docking bay
		bool bay_area 		= deck_position.z   > .2
					&& deck_position.z  < 1.2
					&& deck_position.x  < .4;
		
		
		deck_position.y 	+= bay_area ? -hull_position.x*0.25+.1 : 0.;

		deck_position.y 	+= bay_area
					&& columns
					? -.01 : .0;
		
		deck_position.y 	+= bay_area
					&& spars
					&& !columns
					? -0.004 : 0.;
			
					//main docking bay border
		deck_position.y 	+= deck_position.z  > .75
					&& deck_position.z  < 1.95
					&& deck_position.x  < .45
					&& !spars
					? -abs(position.x)*.001 : 0.;   
	}
	deck_position   		+= -.005;
	ship                    	= min(ship, hull_inner);
	
	bool upper_decks        	= hull_position.y != deck_position.y 
					|| hull_position.x != deck_position.x 
					|| hull_position.z != deck_position.z; 
	
		
	vec3 deck_scale         	= hull_scale;
	
	deck_scale              	*= position.y > 0. && position.z < 4. && upper_decks ? 1.01 : 1.;
	float decks             	= cube(deck_position, deck_scale);
		
	ship                    	= position.y < -.075 && position.z < 4. ? max(hull_inner,decks) : ship;
	ship                    	= min(decks, ship);
	ship                    	= min(collector, ship);
	
	
	//border around the edge of the main hull
	vec3 border_mask_position   	= abs(hull_scale);
	border_mask_position.x      	*= -abs(hull_position.x*1.5)+hull_position.z*.5+1.25;
	border_mask_position.y      	*= 1.1;
	border_mask_position.z      	+= position.z-hull_position.x*.1 < 4.5 ? .05 : 0.;
		
	float hull_border_mask      	= cube(hull_position, border_mask_position);
	
	float hull_border           	= abs(fract((hull_position.x*2.+hull_position.z*8.)*4.)-.5)*2.;
	hull_border                 	= min(hull_border, .1)*.005-.0035;
		
	bool border                 	= hull_border_mask > hull_outer && !inner_hull && !upper_decks;
	
	
	
	hull_border                 	+= outboard_partitions * .00015;
	outboard_partitions         	+= max(outboard_partitions, .85)*4.;    
		
	vec2 xz_floor_noise                	= hash(floor(hull_position.xz*8.+deck_scale.y*4.+abs(8.-hull_position.zx*1.5)*vec2(32., 23.)));
	xz_floor_noise                 	= max(floor(xz_floor_noise*32.)-25., .0)*.025*(.25-position.x)*xz_floor_noise;


	//outboard hangar bays
	vec3 bay_positions         	= position;
	bay_positions.z            	+= -3.15;
	bay_positions.z            	= mod(bay_positions.z+mod(bay_positions.z, 3. + bay_positions.z * .5), 3.4)-.5525;
	bay_positions.y           	+= z_floor_noise * .025 + xz_floor_noise.y * .005 - .01;
	
	vec3 bay_scale             	= vec3(1.);
	bay_scale.x             	= 4.;
	bay_scale.y             	= .0175 - (xz_floor_noise.x - xz_floor_noise.y) * hull_scale.x * .035 + z_floor_noise*position.x*.0025;
	bay_scale.y             	+= position.z < 0.1 ? xz_floor_noise.x * .4 -.01 : 0.;
	bay_scale.y             	+= abs(position.x) < .05 ? -.04 : 0.;
	bay_scale.z             	= position.z*.095+.1+abs(position.x)*.1;
	
	float hangar_bays           	= cube(bay_positions, bay_scale);

	
	//composite details
	bool wake_line          	= abs(position.y)<.055;
	
	hull_panels             	*= border || bridge_tower ? .5 : 1.;
	hull_panels             	*= upper_decks ? .75 : 1.;
	

	float hull_inner_detail     	= wake_line
					&& hangar_bays > 0.
					&& outboard_partitions<3.95
					? .01 : outboard_partitions *.001;

	float hull_detail_floor		= floor(8.*cross(position.z+cross(position.z*5.)+cross(position.y*3.))/3.);
	hull_inner_detail       	+= wake_line && hangar_bays > 0.
					&& outboard_partitions<3.95
					&& position.z > -1.4
					? -min(hull_detail_floor,.75)*.02 : 0.;
	
	//detailing
	ship               		+= wake_line ? (z_floor_noise < .1 ? -.0075 : 0.) : 0.;                  
	ship               		= max(ship, -hangar_bays);
	ship               		= border ? ship + hull_border : ship+ hull_border ;
	ship               		= bridge_tower || upper_decks ? ship + outboard_partitions *.0001 : ship;
	ship               		= (bridge_tower || outer_hull) && !border ? ship + hull_panels * .0025 : ship;  
	ship               		= !aft && !bridge_tower && inner_hull ? ship + hull_inner_detail: ship; 
	

	//return distance and materials
	vec2 material_range         	= vec2(0.);
	material_range.y            	= ship;
	material_range.x        	= 0.;
	return material_range;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv         	= UV;
	ray r           	= view(uv);

	r               	= emit(r);


	float distanceFog   	= clamp(r.material_range.y/FARPLANE, 0., 1.);
	float stepFog       	= clamp(r.steps/float(ITERATIONS), 0., 1.);
	stepFog        	 	= r.steps < 1. ? 1. : stepFog;
	
	vec4 result     	= vec4(0.);
	result          	= pow(stepFog, .5) * vec4(1., 1.5, 1.,1.);
	result.r        	+= r.steps > float(ITERATIONS - ITERATIONS/4) ? 1. : 0.;
	result      		= pow(result,vec4(2.));
	result      		-= r.material_range.y*.012;
	result.w   		 = 1.;

	fragColor = result;
}// sphinx

ray emit(ray r)
{
	float total_range       = r.material_range.y;
	float threshold     	= PHI;
	
	for(int i = 1; i < ITERATIONS; i++)
	{
		if(total_range < FARPLANE)
		{
			if(r.material_range.y < threshold && r.material_range.y > 0.)
			{
				r.material_range.x += r.material_range.y;
				r.material_range.y = total_range;
				r.steps            = float(i);
				break;
			}

			threshold          *= 1.04;
			r.position         += r.direction * r.material_range.y * .8;
			r.material_range   = map(r.position);
			
			if(r.material_range.y < 0.)
			{
				r.material_range.y -= threshold;
				threshold *= float(i);
			}
			total_range        += r.material_range.y;
		}
		else
		{
			r.material_range.y = 1.+length(r.origin + r.direction * FARPLANE);
			r.material_range.x = 0.;
			r.steps            = float(i);
			break;
		}
	}
	return r;
}

vec2 format_to_screen(vec2 uv)
{
	uv = uv * 2. - 1.;
	uv.x *= ASPECT;
	return uv;
}


ray view(in vec2 uv)
{ 
	uv = format_to_screen(uv);

	vec3 w          = normalize(VIEWTARGET-VIEWPOSITION);
	vec3 u          = normalize(cross(w,vec3(0.,1.,0.)));
	vec3 v          = normalize(cross(u,w));

	ray r           = ray(vec3(0.), vec3(0.), vec3(0.), vec2(0.), 0.);
	r.origin        = VIEWPOSITION;
	r.position      = VIEWPOSITION;
	r.direction     = normalize(uv.x*u + uv.y*v + FOV*w);;
	r.material_range    = vec2(0.);
	r.steps             = 0.;

	return r;
}   


float sphere(vec3 position, float radius)
{
	return length(position)-radius; 
}

float cube(vec3 p, vec3 s)
{
	vec3 d = (abs(p) - s);
	return min(max(d.x,max(d.y,d.z)),0.0) + length(max(d,0.0));
}

float torus( vec3 p, vec2 t )
{
	vec2 q = vec2(length(p.xz)-t.x, p.y);
	return length(q)-t.y;
}

float cylinder(vec3 p, float l, float r)
{
	return max(abs(p.y-l)-l, length(p.xz)-r);
}

float cone(vec3 p, float l, vec2 r)
{
	float m = 1.-(p.y*.5)/l;
	return max(length(p.xz)-mix(r.y, r.x, m), abs(p.y-l)-l);
}

float icosahedral(vec3 p, float e, float r)
{
	vec2 n = vec2(.577, -.577);
	float a = .357;
	float b = .934;
	float s = pow(abs(dot(p,n.yyy)),e);
	s += pow(abs(dot(p,n.yxx)),e);
	s += pow(abs(dot(p,n.xyx)),e);
	s += pow(abs(dot(p,n.xxy)),e);
	s += pow(abs(dot(p,vec3( 0.,a,b))),e);
	s += pow(abs(dot(p,vec3(0.,-a,b))),e);
	s += pow(abs(dot(p,vec3( b,0.,a))),e);
	s += pow(abs(dot(p,vec3(-b,0.,a))),e);
	s += pow(abs(dot(p,vec3( a,b,0.))),e);
	s += pow(abs(dot(p,vec3(-a,b,0.))),e);
	s = pow(s, 1./e);
	return s-r;
}
//// END DISTANCE FIELD FUNCTIONS


//// NOISE
//via http://glsl.herokuapp.com/e#4841.11
float partition_noise(vec2 p) 
{
	vec2 id;
	
	id = floor(floor(p)-.5);
	
	p *= floor(hash(id) * 2.)+1.;
	id = floor(p);
	
	p.yx *= floor(hash(id) * 3.)-4.;
	id -= floor(p);

	p *= floor(hash(id) * 2.)+1.;
	id = floor(p);

	p -= id;

	vec2 u = abs(p - .5) * 2.;

	return max(u.x, u.y);
}

float hash(float v)
{
	return fract(fract(v*1234.5678)*(v+v)*12345.678);
}

vec2 hash(vec2 v) 
{
	vec2 n;
	n.x=fract(cos(v.y-v.x*841.0508)*(v.y+v.x)*3456.7821);
	n.y=fract(sin(v.x+v.y*804.2048)*(v.x-v.y)*5349.2627);
	return n;
}

float cross(float x)
{
	return abs(fract(x-.5)-.5)*2.;  
}


mat2 rmat(in float r)
{
	float c = cos(r);
	float s = sin(r);
	return mat2(c, s, -s, c);
}
