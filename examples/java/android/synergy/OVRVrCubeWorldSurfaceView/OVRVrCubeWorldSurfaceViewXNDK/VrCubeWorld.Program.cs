using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using ScriptCoreLibNative.SystemHeaders.GLES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVRVrCubeWorldSurfaceViewXNDK
{
    public static unsafe partial class VrCubeWorld
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewNDK\staging\jni\VrCubeWorld_SurfaceView.c


        [Script]
        struct ovrVertexAttribute
        {
            public ovrVertexAttribute_location location;
            public string name;
        }


        public enum ovrUniform_index
        {
            UNIFORM_MODEL_MATRIX,
            UNIFORM_VIEW_MATRIX,
            UNIFORM_PROJECTION_MATRIX
        }

        public enum ovrUniform_type
        {
            UNIFORM_TYPE_VECTOR4,
            UNIFORM_TYPE_MATRIX4X4,
        }

        [Script]
        struct ovrUniform
        {
            public ovrUniform_index index;
            public ovrUniform_type type;
            public string name;

            // using static enum?
        }


        public const int MAX_PROGRAM_UNIFORMS = 8;
        public const int MAX_PROGRAM_TEXTURES = 8;

        // a field at ovrScene, keep it as class as fixed causes uglyness
        [Script]
        //struct ovrProgram
        class ovrProgram
        {
            // sent to glUseProgram
            public uint Program;

            public uint VertexShader;
            public uint FragmentShader;
            // These will be -1 if not used by the program.

            public readonly int[] Uniforms = new int[MAX_PROGRAM_UNIFORMS];      // ProgramUniforms[].name
            public readonly int[] Textures = new int[MAX_PROGRAM_TEXTURES];      // Texture%i

            //Error CS1663  Fixed size buffer type must be one of the following: bool, byte, short, int, long, char, sbyte, ushort, uint, ulong, float or double OVRVrCubeWorldSurfaceViewXNDK   X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs	130
            //Error CS1642  Fixed size buffer fields may only be members of structs OVRVrCubeWorldSurfaceViewXNDK X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs	129


            //// does jsc initialize statically ot at cctor?
            // if its readonly then we could remember the size of it?
            readonly ovrVertexAttribute[] ProgramVertexAttributes = new[]
            {
                new ovrVertexAttribute { location = ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_POSITION,  name = "vertexPosition" },
                new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_COLOR,      name = "vertexColor" },
                new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_UV,      name =         "vertexUv" },
                new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_TRANSFORM,      name =  "vertexTransform" }
            };

            readonly ovrUniform[] ProgramUniforms = new[]
            {
                new ovrUniform { index=ovrUniform_index.UNIFORM_MODEL_MATRIX,         type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ModelMatrix" },
                new ovrUniform { index=ovrUniform_index.UNIFORM_VIEW_MATRIX,          type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ViewMatrix" },
                new ovrUniform { index=ovrUniform_index.UNIFORM_PROJECTION_MATRIX,    type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ProjectionMatrix" }
            };


            public ovrProgram()
            {
                ovrProgram_Clear();
            }
            // called by ovrScene_Clear
            void ovrProgram_Clear()
            {
                // 545

                this.Program = 0;
                this.VertexShader = 0;
                this.FragmentShader = 0;
            }

            // called by  ovrScene_Create
            public void ovrProgram_Create(string vertexSource, string fragmentSource)
            {
                // 554

                var r = default(int);

                this.VertexShader = gl3.glCreateShader(gl3.GL_VERTEX_SHADER);


                var vertexSource0 = new[] { vertexSource };
                gl3.glShaderSource(this.VertexShader, 1, vertexSource0, null);

                gl3.glCompileShader(this.VertexShader);
                gl3.glGetShaderiv(this.VertexShader, gl3.GL_COMPILE_STATUS, out r);
                //if ( r == gl3.GL_FALSE )
                //{
                //    GLchar msg[4096];
                //    GL( glGetShaderInfoLog( program->VertexShader, sizeof( msg ), 0, msg ) );
                //    ALOGE( "%s\n%s\n", vertexSource, msg );
                //    return false;
                //}

                this.FragmentShader = gl3.glCreateShader(gl3.GL_FRAGMENT_SHADER);
                var fragmentSource0 = new[] { fragmentSource };
                gl3.glShaderSource(this.FragmentShader, 1, fragmentSource0, null);
                gl3.glCompileShader(this.FragmentShader);
                gl3.glGetShaderiv(this.FragmentShader, gl3.GL_COMPILE_STATUS, out r);
                //if ( r == GL_FALSE )
                //{
                //    GLchar msg[4096];
                //    GL( glGetShaderInfoLog( program->FragmentShader, sizeof( msg ), 0, msg ) );
                //    ALOGE( "%s\n%s\n", fragmentSource, msg );
                //    return false;
                //}

                this.Program = gl3.glCreateProgram();

                gl3.glAttachShader(this.Program, this.VertexShader);
                gl3.glAttachShader(this.Program, this.FragmentShader);

                // Bind the vertex attribute locations.
                for (int i = 0; i < ProgramVertexAttributes.Length; i++)
                {
                    gl3.glBindAttribLocation((uint)this.Program, (uint)ProgramVertexAttributes[i].location, ProgramVertexAttributes[i].name);
                }

                gl3.glLinkProgram(this.Program);
                gl3.glGetProgramiv(this.Program, gl3.GL_LINK_STATUS, out r);
                //if ( r == GL_FALSE )
                //{
                //    GLchar msg[4096];
                //    GL( glGetProgramInfoLog( program->Program, sizeof( msg ), 0, msg ) );
                //    ALOGE( "Linking program failed: %s\n", msg );
                //    return false;
                //}

                // Get the uniform locations.
                //memset( program->Uniforms, -1, sizeof( program->Uniforms ) );
                for (int i = 0; i < ProgramUniforms.Length; i++)
                {
                    this.Uniforms[(int)ProgramUniforms[i].index] = gl3.glGetUniformLocation(this.Program, ProgramUniforms[i].name);
                }

                //gl3.glUseProgram(this.Program);

                //// Get the texture locations.
                //for ( int i = 0; i < MAX_PROGRAM_TEXTURES; i++ )
                //{
                //    fixed char name[32] = {0};

                //    sprintf( name, "Texture%i", i );
                //    this.Textures[i] = gl3.glGetUniformLocation( this.Program, name );
                //    if ( this.Textures[i] != -1 )
                //    {
                //        gl3.glUniform1i( this.Textures[i], i  );
                //    }
                //}

                //gl3.glUseProgram(0);

            }

            // called by ovrScene_Destroy
            public void ovrProgram_Destroy()
            {
                // 628
                if (this.Program != 0)
                {
                    gl3.glDeleteProgram(this.Program);
                    this.Program = 0;
                }
                if (this.VertexShader != 0)
                {
                    gl3.glDeleteShader(this.VertexShader);
                    this.VertexShader = 0;
                }
                if (this.FragmentShader != 0)
                {
                    gl3.glDeleteShader(this.FragmentShader);
                    this.FragmentShader = 0;
                }
            }

        }






    }


}
