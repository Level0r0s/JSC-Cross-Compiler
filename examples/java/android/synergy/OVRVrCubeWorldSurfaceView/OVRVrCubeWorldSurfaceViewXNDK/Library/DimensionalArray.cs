using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVRVrCubeWorldSurfaceViewXNDK.Library
{
    [Script]
    public class DimensionalArray<TElement>
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150618
        // did it once for as already. jsc, where is it?

        TElement[] data;

        int LengthRank0;
        int LengthRank1;

        public DimensionalArray(int LengthRank0, int LengthRank1)
        {
            // do we even have the typeinfo needed?

            //public ovrRenderTexture[,] RenderTextures;

            // TElement is void*
            data = new TElement[LengthRank0 * LengthRank1];
        }

        public TElement this[int x, int y]
        {
            get
            {
                // this would be a copy, what if we want to work on struct ref level?
                return data[x + y * LengthRank0];
            }
        }
    }
}
