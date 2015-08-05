	#version 300 es 
	in vec3 vertexPosition; 
	in vec4 vertexColor; 
	

	// X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRUDPMatrix\Program.cs
	// set by?
	in mat4 vertexTransform; 

	// X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\VrCubeWorld.Renderer.cs
	// 370
	uniform mat4 ViewMatrix; 
	uniform mat4 ProjectionMatrix; 
	
	out vec4 fragmentColor; 

	void main() 
	{ 
		gl_Position = ProjectionMatrix * ( ViewMatrix * ( vertexTransform * vec4( vertexPosition, 1.0 ) ) ); 
		
		fragmentColor = vertexColor; 
	}

	// X:\jsc.svn\examples\javascript\WebGL\WebGLSpadeWarrior\WebGLSpadeWarrior\Shaders\Geometry.vert