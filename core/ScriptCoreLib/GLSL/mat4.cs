﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    [Script]
    public struct mat4
    {
        // see also: http://cgkit.sourceforge.net/doc2/mat4.html

        public static mat4 operator *(mat4 x, mat4 y)
        {
            throw new NotImplementedException();
        }
    }
}
