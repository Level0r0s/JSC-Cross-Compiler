using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.Microsoft.VisualBasic.CompilerServices
{
    [Script(Implements = typeof(global::Microsoft.VisualBasic.CompilerServices.Conversions))]
    internal class __Conversions
    {
        public static string ToString(object e)
        {
            return Convert.ToString(e);
        }


        public static double ToDouble(object e)
        {
            return Convert.ToDouble(e);
        }





    }
}
