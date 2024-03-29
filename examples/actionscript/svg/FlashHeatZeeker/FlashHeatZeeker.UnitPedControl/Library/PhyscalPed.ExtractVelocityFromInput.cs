﻿using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using FlashHeatZeeker.UnitPed.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using starling.display;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitPedControl.Library
{
    public partial class PhysicalPed : IPhysicalUnit
    {
        public class Velocity
        {
            public double AngularVelocity;
            public double LinearVelocityX;
            public double LinearVelocityY;
        }
        Velocity velocity = new Velocity();

        public void ExtractVelocityFromInput(KeySample __keyDown, Velocity value)
        {


            value.AngularVelocity = 0;
            value.LinearVelocityX = 0;
            value.LinearVelocityY = 0;



            if (__keyDown != null)
            {
                #region alt
                Func<Keys, Keys, bool> alt =
                    (k1, k2) =>
                    {
                        if (__keyDown[Keys.Alt])
                        {
                            return __keyDown[k2];
                        }
                        return __keyDown[k1];
                    };
                #endregion

                // script: error JSC1000: ActionScript : unable to emit br.s at 'FlashHeatZeeker.UnitPedControl.Library.PhysicalPed.ExtractVelocityFromInput'#004c: invalid br opcode
                var k = new
                {
                    up = __keyDown[Keys.Up],
                    down = __keyDown[Keys.Down],

                    left = alt(Keys.Left, Keys.A),
                    right = alt(Keys.Right, Keys.D),

                    strafeleft = alt(Keys.A, Keys.Left),
                    straferight = alt(Keys.D, Keys.Right),
                };

                if (k.up)
                {
                    // we have reasone to keep walking

                    value.LinearVelocityY = 1;
                }

                if (k.down)
                {
                    // we have reasone to keep walking
                    // go slow backwards
                    value.LinearVelocityY = -0.5;

                }

                if (k.left)
                {
                    // we have reasone to keep walking

                    value.AngularVelocity = -1;

                }

                if (k.right)
                {
                    // we have reasone to keep walking

                    value.AngularVelocity = 1;

                }
                if (k.strafeleft)
                {
                    // we have reasone to keep walking

                    value.LinearVelocityX = -1;

                }

                if (k.straferight)
                {
                    // we have reasone to keep walking

                    value.LinearVelocityX = 1;

                }
            }


        }

    }

}
