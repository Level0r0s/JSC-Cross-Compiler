using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace GoogleMapsMarkerLEST97
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //var px = -2.71;
            //var py = 2.5;

            //var xy = MathUtil.Pow(px, py);
            //// xy = NaN

            {
                var xlng = 58.432703481019288;
                var xlat = 24.442902208375234;

                // rewrite broken? or vba broken?
                var xx = LEST97.lest_function_vba.geo_lest(xlng, xlat, 0);
                // x = 6477013.7889990406

                var yy = LEST97.lest_function_vba.geo_lest(xlng, xlat, 1);
                // y = 525871.67200000642

                //var Tallinn = new { x = 6501171, y = 546561 };
                var Tallinn = new { x = xx, y = yy };
                var Haapsalu = new { x = 6533398, y = 480832 };

                //B0 1.0038706937719
                //ESQ 0.00669438002290341
                //B0 1.0038706937719
                //ESQ 0.00669438002290341

                // does the rewriter breake it?

                var lat = LEST97.lest_function_vba.lest_geo(Tallinn.x, Tallinn.y, 0);
                // lat = 58.432703481010677

                var lng = LEST97.lest_function_vba.lest_geo(Tallinn.x, Tallinn.y, 1);

                // lng = 24.442902208375234


            }
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}

// http://stackoverflow.com/questions/14367448/c-sharp-math-pow-is-broken
public class MathUtil
{
    /// <summary>
    /// Wrapper for Math.Pow()
    /// Can handle cases like (-8)^(1/3) or  (-1/64)^(1/3)
    /// </summary>
    public static double Pow(double expBase, double power)
    {
        bool sign = (expBase < 0);
        if (sign && HasEvenDenominator(power))
            return double.NaN;  //sqrt(-1) = i
        else
        {
            if (sign && HasOddDenominator(power))
                return -1 * Math.Pow(Math.Abs(expBase), power);
            else
                return Math.Pow(expBase, power);
        }
    }

    private static bool HasEvenDenominator(double input)
    {
        if (input == 0)
            return false;
        else if (input % 1 == 0)
            return false;

        double inverse = 1 / input;
        if (inverse % 2 < double.Epsilon)
            return true;
        else
            return false;
    }

    private static bool HasOddDenominator(double input)
    {
        if (input == 0)
            return false;
        else if (input % 1 == 0)
            return false;

        double inverse = 1 / input;
        if ((inverse + 1) % 2 < double.Epsilon)
            return true;
        else
            return false;
    }
}
