	#version 300 es 
	in vec3 vertexPosition; 
	in vec4 vertexColor; 
	
	// set by?
	in mat4 vertexTransform; 
	uniform mat4 ViewMatrix; 
	uniform mat4 ProjectionMatrix; 
	
	out vec4 fragmentColor; 

	void main() 
	{ 
		gl_Position = ProjectionMatrix * ( ViewMatrix * ( vertexTransform * vec4( vertexPosition, 1.0 ) ) ); 
		fragmentColor = vertexColor; 
	}

	// X:\jsc.svn\examples\javascript\WebGL\WebGLSpadeWarrior\WebGLSpadeWarrior\Shaders\Geometry.vert