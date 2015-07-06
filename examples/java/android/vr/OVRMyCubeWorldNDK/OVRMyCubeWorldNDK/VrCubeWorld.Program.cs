﻿using ScriptCoreLib;
using ScriptCoreLib.GLSL;
using ScriptCoreLibAndroidNDK.Library;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using ScriptCoreLibNative.SystemHeaders.GLES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVRMyCubeWorldNDK
{
    public static unsafe partial class VrCubeWorld
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewNDK\staging\jni\VrCubeWorld_SurfaceView.c

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

        struct ovrUniform
        {
            public ovrUniform_index index;
            public ovrUniform_type type;
            public string name;

            // using static enum?
        }

        // 508
        public const int MAX_PROGRAM_UNIFORMS = 8;
        public const int MAX_PROGRAM_TEXTURES = 8;

        // a field at ovrScene, keep it as class as fixed causes uglyness
        // created by ovrScene
        public class ovrProgram
        {
            // set by ovrProgram_Create
            // sent to glUniformMatrix4fv
            public readonly int[] Uniforms = new int[MAX_PROGRAM_UNIFORMS];      // ProgramUniforms[].name
            public readonly int[] Textures = new int[MAX_PROGRAM_TEXTURES];      // Texture%i

            // 545
            // sent to glUseProgram
            public uint Program = 0;

            public uint VertexShader = 0;
            public uint FragmentShader = 0;

            // These will be -1 if not used by the program.

            readonly ovrVertexAttribute[] ProgramVertexAttributes = new[]
            {
                new ovrVertexAttribute { location = ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_POSITION,  name = "vertexPosition" },
                new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_COLOR,      name = "vertexColor" },
                new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_UV,      name =         "vertexUv" },
                new ovrVertexAttribute { location =  ovrVertexAttribute_location.VERTEX_ATTRIBUTE_LOCATION_TRANSFORM,      name =  "vertexTransform" }
            };

            // 536
            readonly ovrUniform[] ProgramUniforms = new[]
            {
                new ovrUniform { index=ovrUniform_index.UNIFORM_MODEL_MATRIX,         type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ModelMatrix" },
                new ovrUniform { index=ovrUniform_index.UNIFORM_VIEW_MATRIX,          type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ViewMatrix" },
                new ovrUniform { index=ovrUniform_index.UNIFORM_PROJECTION_MATRIX,    type=ovrUniform_type.UNIFORM_TYPE_MATRIX4X4, name="ProjectionMatrix" }
            };




            // called by  ovrScene_Create
            // called after VRAPI_FRAME_INIT_LOADING_ICON_FLUSH
            public bool ovrProgram_Create(string vertexSource, string fragmentSource)
            //public bool ovrProgram_Create(VertexShader vert, FragmentShader frag)
            {
                // 554
                ConsoleExtensions.trace("enter ovrProgram_Create, glCreateShader");

                //var vertexSource = vert.ToString();
                //var fragmentSource = frag.ToString();

                var r = default(int);

                this.VertexShader = gl3.glCreateShader(gl3.GL_VERTEX_SHADER);
                gl3.glShaderSource(this.VertexShader, 1, ref vertexSource, null);
                gl3.glCompileShader(this.VertexShader);
                gl3.glGetShaderiv(this.VertexShader, gl3.GL_COMPILE_STATUS, out r);

                ConsoleExtensions.tracei("ovrProgram_Create VertexShader GL_COMPILE_STATUS ", r);

                #region glGetShaderInfoLog
                if (r == gl3.GL_FALSE)
                {
                    //I/xNativeActivity( 9698): x:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRMyCubeWorldNDK\VrCubeWorld.Program.cs:104 ovrProgram_Create VertexShader GL_COMPILE_STATUS  0 errno: 0 Success
                    //I/xNativeActivity( 9698): x:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRMyCubeWorldNDK\VrCubeWorld.Program.cs:111 0:12: L0001: Typename expected, found ';'
                    //I/xNativeActivity( 9698):  0 errno: 0 Success

                    var msg = new byte[4096];
                    var len = 0;
                    gl3.glGetShaderInfoLog(this.VertexShader, 4096, out len, msg);
                    ConsoleExtensions.tracei((string)(object)msg);
                    unistd._exit(-1);
                    return false;
                }
                #endregion

                this.FragmentShader = gl3.glCreateShader(gl3.GL_FRAGMENT_SHADER);
                gl3.glShaderSource(this.FragmentShader, 1, ref fragmentSource, null);
                gl3.glCompileShader(this.FragmentShader);
                gl3.glGetShaderiv(this.FragmentShader, gl3.GL_COMPILE_STATUS, out r);

                ConsoleExtensions.tracei("ovrProgram_Create FragmentShader GL_COMPILE_STATUS ", r);
                // I/xNativeActivity( 6203): x:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRMyCubeWorldNDK\VrCubeWorld.Program.cs:121 ovrProgram_Create FragmentShader GL_COMPILE_STATUS  1 errno: 0 Success

                #region glGetShaderInfoLog
                if (r == gl3.GL_FALSE)
                {
                    var msg = new byte[4096];
                    var len = 0;
                    gl3.glGetShaderInfoLog(this.FragmentShader, 4096, out len, msg);
                    ConsoleExtensions.tracei((string)(object)msg);
                    unistd._exit(-1);
                    return false;
                }
                #endregion

                this.Program = gl3.glCreateProgram();

                gl3.glAttachShader(this.Program, this.VertexShader);
                gl3.glAttachShader(this.Program, this.FragmentShader);

                // Bind the vertex attribute locations.
                for (int i = 0; i < ProgramVertexAttributes.Length; i++)
                {
                    //ConsoleExtensions.tracei("ovrProgram_Create glBindAttribLocation i ", i);
                    gl3.glBindAttribLocation((uint)this.Program, (uint)ProgramVertexAttributes[i].location, ProgramVertexAttributes[i].name);
                }

                gl3.glLinkProgram(this.Program);
                gl3.glGetProgramiv(this.Program, gl3.GL_LINK_STATUS, out r);

                ConsoleExtensions.tracei("ovrProgram_Create Program GL_LINK_STATUS ", r);

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
                    //ConsoleExtensions.tracei("ovrProgram_Create glGetUniformLocation i ", i);
                    this.Uniforms[(int)ProgramUniforms[i].index] = gl3.glGetUniformLocation(this.Program, ProgramUniforms[i].name);
                }

                gl3.glUseProgram(this.Program);

                //// Get the texture locations.
                for (int i = 0; i < MAX_PROGRAM_TEXTURES; i++)
                {
                    //    fixed char name[32] = {0};

                    //    sprintf( name, "Texture%i", i );
                    //    this.Textures[i] = gl3.glGetUniformLocation( this.Program, name );
                    //    if ( this.Textures[i] != -1 )
                    //    {
                    //        gl3.glUniform1i( this.Textures[i], i  );
                    //    }
                }

                gl3.glUseProgram(0);

                ConsoleExtensions.trace("exit ovrProgram_Create");
                return true;
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
