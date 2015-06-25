using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestPointerIndexer
{
    public unsafe struct ovrHmdInfo
    {
        public fixed int SuggestedEyeResolution[2];
    }



    public unsafe class Class1
    {
        //public static int invoke(int* e)
        //{
        //    return e[1];
        //    //return e[2];
        //}

        //public static int invoke(int* e, int i)
        //{
        //    return e[i];
        //}

        public void ovrRenderer_Create(ref ovrHmdInfo hmdInfo)
        {

            fixed (int* hmdInfo_SuggestedEyeResolution = hmdInfo.SuggestedEyeResolution)
            {
                //var x = hmdInfo_SuggestedEyeResolution[2];
                var x = hmdInfo_SuggestedEyeResolution[1];
            }
        }
    }
}
