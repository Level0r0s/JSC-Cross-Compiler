precision mediump float;
varying vec4 v_Color;
varying vec3 v_Grid;

void main() {
    float depth = gl_FragCoord.z / gl_FragCoord.w; // Calculate world-space distance.

    if ((mod(abs(v_Grid.x), 10.0) < 0.1) || (mod(abs(v_Grid.z), 10.0) < 0.1)) {
        gl_FragColor = 
			max(0.0, (90.0-depth) / 90.0) * 
				vec4(
				// if we change glsl here, would we be able to udp patch a running app on android in realtime?
					(0.0 + v_Color.r) / 2.0, 
					(0.0 + v_Color.g) / 2.0, 
					(0.0 - v_Color.b) / 2.0, 
					
					1.0
			    )

                + min(1.0, depth / 90.0) * v_Color;
    } else {
        gl_FragColor = v_Color;
    }
}
