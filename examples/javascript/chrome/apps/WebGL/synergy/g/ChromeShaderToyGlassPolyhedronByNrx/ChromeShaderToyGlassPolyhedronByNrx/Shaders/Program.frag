﻿// Forked from: https://www.shadertoy.com/view/ldfXzn

#ifdef GL_ES
precision highp float;
#endif

#define DELTA				0.001
#define RAY_COUNT			7
#define RAY_LENGTH_MAX		100.0
#define RAY_STEP_MAX		100
#define LIGHT				vec3 (1.0, 1.0, -1.0)
#define REFRACT_FACTOR		0.6
#define REFRACT_INDEX		1.6
#define AMBIENT				0.2
#define SPECULAR_POWER		3.0
#define SPECULAR_INTENSITY	0.5
#define FADE_POWER			1.0
#define M_PI				3.1415926535897932384626433832795
#define GLOW_FACTOR			1.5
#define LUMINOSITY_FACTOR	2.0

mat3 mRotate (in vec3 angle) {
	float c = cos (angle.x);
	float s = sin (angle.x);
	mat3 rx = mat3 (1.0, 0.0, 0.0, 0.0, c, s, 0.0, -s, c);

	c = cos (angle.y);
	s = sin (angle.y);
	mat3 ry = mat3 (c, 0.0, -s, 0.0, 1.0, 0.0, s, 0.0, c);

	c = cos (angle.z);
	s = sin (angle.z);
	mat3 rz = mat3 (c, s, 0.0, -s, c, 0.0, 0.0, 0.0, 1.0);

	return rz * ry * rx;
}

vec3 k;
float getDistance (in vec3 p) {
	float repeat = 20.0;
	vec3 q = p + repeat * 0.5;
	k = floor (q / repeat);
	q -= repeat * (k + 0.5);
	p = mRotate (k) * q;

	float top = p.y - 3.0;
	float angleStep = M_PI / max (2.0, abs (k.x + 2.0 * k.y + 4.0 * k.z));
	float angle = angleStep * (0.5 + floor (atan (p.x, p.z) / angleStep));
	float side = cos (angle) * p.z + sin (angle) * p.x - 2.0;
	float bottom = -p.y - 3.0;

	return max (top, max (side, bottom));
}

vec3 getFragmentColor (in vec3 origin, in vec3 direction) {
	vec3 lightDirection = normalize (LIGHT);
	vec2 delta = vec2 (DELTA, 0.0);

	vec3 fragColor = vec3 (0.0, 0.0, 0.0);
	float intensity = 1.0;

	float distanceFactor = 1.0;
	float refractionRatio = 1.0 / REFRACT_INDEX;
	float rayStepCount = 0.0;
	for (int rayIndex = 0; rayIndex < RAY_COUNT; ++rayIndex) {

		// Ray marching
		float dist = RAY_LENGTH_MAX;
		float rayLength = 0.0;
		for (int rayStep = 0; rayStep < RAY_STEP_MAX; ++rayStep) {
			dist = distanceFactor * getDistance (origin);
			float distMin = max (dist, DELTA);
			rayLength += distMin;
			if (dist < 0.0 || rayLength > RAY_LENGTH_MAX) {
				break;
			}
			origin += direction * distMin;
			++rayStepCount;
		}

		// Check whether we hit something
		vec3 backColor = vec3 (0.0, 0.0, 0.1 + 0.2 * max (0.0, dot (-direction, lightDirection)));
		if (dist >= 0.0) {
			fragColor = fragColor * (1.0 - intensity) + backColor * intensity;
			break;
		}

		// Get the normal
		vec3 normal = normalize (distanceFactor * vec3 (
			getDistance (origin + delta.xyy) - getDistance (origin - delta.xyy),
			getDistance (origin + delta.yxy) - getDistance (origin - delta.yxy),
			getDistance (origin + delta.yyx) - getDistance (origin - delta.yyx)));

		// Basic lighting
		vec3 reflection = reflect (direction, normal);
		if (distanceFactor > 0.0) {
			float relfectionDiffuse = max (0.0, dot (normal, lightDirection));
			float relfectionSpecular = pow (max (0.0, dot (reflection, lightDirection)), SPECULAR_POWER) * SPECULAR_INTENSITY;
			float fade = pow (1.0 - rayLength / RAY_LENGTH_MAX, FADE_POWER);

			vec3 localColor = max (sin (k * k), 0.2);
			localColor = (AMBIENT + relfectionDiffuse) * localColor + relfectionSpecular;
			localColor = mix (backColor, localColor, fade);

			fragColor = fragColor * (1.0 - intensity) + localColor * intensity;
			intensity *= REFRACT_FACTOR;
		}

		// Next ray...
		vec3 refraction = refract (direction, normal, refractionRatio);
		if (dot (refraction, refraction) < DELTA) {
			direction = reflection;
			origin += direction * DELTA * 2.0;
		}
		else {
			direction = refraction;
			distanceFactor = -distanceFactor;
			refractionRatio = 1.0 / refractionRatio;
		}
	}

	// Return the fragment color
	return fragColor * LUMINOSITY_FACTOR + GLOW_FACTOR * rayStepCount / float (RAY_STEP_MAX * RAY_COUNT);
}

void mainImage (out vec4 fragColor, in vec2 fragCoord) {

	// Define the ray corresponding to this fragment
	vec2 frag = (2.0 * fragCoord.xy - iResolution.xy) / iResolution.y;
	vec3 direction = normalize (vec3 (frag, 2.0));

	// Set the camera
	vec3 origin = vec3 ((15.0 * cos (iGlobalTime * 0.1)), 10.0 * sin (iGlobalTime * 0.2), 15.0 * sin (iGlobalTime * 0.1));
	vec3 forward = -origin;
	vec3 up = vec3 (sin (iGlobalTime * 0.3), 2.0, 0.0);
	mat3 rotation;
	rotation [2] = normalize (forward);
	rotation [0] = normalize (cross (up, forward));
	rotation [1] = cross (rotation [2], rotation [0]);
	direction = rotation * direction;

	// Set the fragment color
	fragColor = vec4 (getFragmentColor (origin, direction), 1.0);
}