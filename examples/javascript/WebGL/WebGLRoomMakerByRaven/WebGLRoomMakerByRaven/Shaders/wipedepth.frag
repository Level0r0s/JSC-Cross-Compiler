#extension GL_EXT_frag_depth : require
			uniform bool performClip;
			uniform vec4 clipPlane;
			varying vec4 worldPos;
			uniform vec3 debugColor;
			void main() {
				if( performClip && dot(worldPos, clipPlane) < 0.0 ) discard;
				gl_FragColor = vec4(debugColor,1.0);
				gl_FragDepthEXT = 1.0;
			}