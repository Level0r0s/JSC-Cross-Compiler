varying vec4 worldPos;
varying vec3 worldNorm;
void main() {
	worldPos = modelMatrix * vec4(position,1.0);
	worldNorm = normal;
	gl_Position = projectionMatrix * viewMatrix * worldPos;
}