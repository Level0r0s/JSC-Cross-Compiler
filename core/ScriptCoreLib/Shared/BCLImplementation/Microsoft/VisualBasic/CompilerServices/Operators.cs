using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.Microsoft.VisualBasic.CompilerServices
{
    [Script(Implements = typeof(global::Microsoft.VisualBasic.CompilerServices.Operators))]
    internal class __Operators
    {
        public static bool ConditionalCompareObjectEqual(object Left, object Right, bool TextCompare)
        {
            // https://javascriptweblog.wordpress.com/2010/09/27/the-secret-life-of-javascript-primitives/

            // LINQ orderby?
            //return (double)Left == (double)Right;
            return Convert.ToDouble(Left).CompareTo(Convert.ToDouble(Right)) == 0;
        }

        public static object AddObject(object Left, object Right)
        {
            return Convert.ToDouble(Left) + Convert.ToDouble(Right);
        }

        public static object SubtractObject(object Left, object Right)
        {
            return Convert.ToDouble(Left) - Convert.ToDouble(Right);
        }

        public static object DivideObject(object Left, object Right)
        {
            // Z:\jsc.svn\examples\javascript\vb\LEST97\LEST97\Library\lest_function_vba.vb

            return Convert.ToDouble(Left) / Convert.ToDouble(Right);
        }

        public static object MultiplyObject(object Left, object Right)
        {
            // Z:\jsc.svn\examples\javascript\vb\LEST97\LEST97\Library\lest_function_vba.vb

            return Convert.ToDouble(Left) * Convert.ToDouble(Right);
        }

        public static int CompareString(string Left, string Right, bool TextCompare)
        {
            int num2;
            if (Left == Right)
            {
                return 0;
            }
            if (Left == null)
            {
                if (Right.Length == 0)
                {
                    return 0;
                }
                return -1;
            }
            if (Right == null)
            {
                if (Left.Length == 0)
                {
                    return 0;
                }
                return 1;
            }

            // todo: We are diverting from BCL behaviour :)
            num2 = Left.CompareTo(Right);

            if (num2 == 0)
            {
                return 0;
            }
            if (num2 > 0)
            {
                return 1;
            }
            return -1;
        }

        public static object ExponentObject(object Left, object Right)
        {
            return Math.Pow(Convert.ToDouble(Left), Convert.ToDouble(Right));

        }



    }
}

// script: error JSC1000: No implementation found for this native method, please implement [static Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(System.Object, System.Object, System.Boolean)]
