void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 uv1 = fragCoord.xy  / iResolution.xy ;
 
    
	vec2 uv0 = fragCoord.xy  / iResolution.xy - vec2(0.5, 0.5);
 
    vec2 uv2 = uv0 * 2.0;
    
    
   
    
    const float pi2=6.283185307179586476925286766559;
    
    float azimuth = 0.0;
    float distance0 = length(uv2);
    float distance = length(uv2);

	float maxdistance = 0.832;
	float mindistance = 1.0;

   
		if (distance > maxdistance)
			distance = maxdistance;

        fragColor = vec4(1.0, 0.0, 0.0,1.0);

        azimuth=atan(-uv2.x,uv2.y);
        
        
        if (azimuth<0.0) azimuth+=pi2;
        if (azimuth>pi2) azimuth-=pi2;
        
        
          vec4 col2 =texture2D(iChannel0,
                   vec2(azimuth/pi2, distance)
                   );
        


		
		if (distance0 > 1.0)
			discard;

		fragColor = vec4(col2.xyz, 1.0);
        
  
}