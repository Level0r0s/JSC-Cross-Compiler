uniform bool performClip;
			uniform vec4 clipPlane;
			varying vec4 worldPos;
			varying vec3 worldNorm;
			uniform vec3 debugColor;
			uniform vec3 lightDir;
			void main() {
				if( performClip && dot(worldPos, clipPlane) < 0.0 ) discard;
				vec3 wallColor = debugColor * (dot(worldNorm,lightDir)*0.5+0.5);
				gl_FragColor = vec4(wallColor.xyz,1.0);
			}