using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.TestDriversSync.Library;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using starling.core;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FlashHeatZeeker.TestDriversSync
{
    [SWF(backgroundColor = 0xB27D51)]
    public sealed class ApplicationSprite : Sprite
    {
        #region __transport_in_fakelag
        public event Action<string> __transport_out;

        public void __transport_in_fakelag(string xmlstring)
        {
            PendingInput.Add(xmlstring);
        }

        public void __transport_in(string xmlstring)
        {
            var xml = XElement.Parse(xmlstring);

            if (xml.Name.LocalName == "enterorexit")
            {
                StarlingGameSpriteWithTestDriversSync.__at_enterorexit(
                    egoid: xml.Attribute("egoid").Value,
                    to: xml.Attribute("to").Value,
                    from: xml.Attribute("from").Value
                );
            }

            if (xml.Name.LocalName == "sync")
            {
                StarlingGameSpriteWithTestDriversSync.__at_sync(
                    xml.Attribute("egoid").Value
                );
            }


            if (xml.Name.LocalName == "SetVerticalVelocity")
            {
                StarlingGameSpriteWithTestDriversSync.__at_SetVerticalVelocity(
                    sessionid: xml.Attribute("__sessionid").Value,
                    identity: xml.Attribute("identity").Value,
                    value: xml.Attribute("value").Value
                );
            }

            if (xml.Name.LocalName == "SetVelocityFromInput")
            {
                StarlingGameSpriteWithTestDriversSync.__at_SetVelocityFromInput(
                    __egoid: xml.Attribute("egoid").Value,
                    __identity: xml.Attribute("i").Value,
                    __KeySample: xml.Attribute("k").Value,
                    __fixup_x: xml.Attribute("x").Value,
                    __fixup_y: xml.Attribute("y").Value,
                    __fixup_angle: xml.Attribute("angle").Value
                );
            }
        }

        public void __raise_transport_out(string xml)
        {
            if (__transport_out != null)
                __transport_out(xml);
        }

        Queue<List<string>> lag = new Queue<List<string>>();
        List<string> PendingInput = new List<string>();
        #endregion

        public ApplicationSprite()
        {
            #region __transport
            for (int i = 0; i < 7; i++)
            {
                lag.Enqueue(new List<string>());
            }

            var lagtimer = new ScriptCoreLib.ActionScript.flash.utils.Timer(1000 / 15);

            lagtimer.timer +=
                delegate
                {
                    lag.Enqueue(PendingInput);

                    PendingInput = lag.Dequeue();

                    foreach (var xml in PendingInput)
                    {
                        this.__transport_in(xml);
                    }

                    PendingInput.Clear();
                };

            lagtimer.start();

            StarlingGameSpriteWithTestDriversSync.__raise_enterorexit +=
                (string egoid, string from, string to) =>
                {
                    var xml = new XElement("enterorexit",

                        new XAttribute("egoid", egoid),


                        new XAttribute("from", from),
                        new XAttribute("to", to)

                    );

                    if (__transport_out != null)
                        __transport_out(xml.ToString());
                };

            StarlingGameSpriteWithTestDriversSync.__raise_sync +=
               egoid =>
               {
                   // Error	8	Argument 1: cannot convert from 'System.Xml.Linq.XAttribute' to 'System.Xml.Linq.XStreamingElement'	X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.UnitPedSync\ApplicationSprite.cs	40	33	FlashHeatZeeker.UnitPedSync

                   var xml = new XElement("sync", new XAttribute("egoid", egoid));

                   if (__transport_out != null)
                       __transport_out(xml.ToString());
               };

            StarlingGameSpriteWithTestDriversSync.__raise_SetVerticalVelocity +=
                (string __sessionid, string identity, string value) =>
                {
                    var xml = new XElement("SetVerticalVelocity",

                        new XAttribute("__sessionid", __sessionid),


                        new XAttribute("identity", identity),
                        new XAttribute("value", value)

                    );

                    if (__transport_out != null)
                        __transport_out(xml.ToString());
                };

            StarlingGameSpriteWithTestDriversSync.__raise_SetVelocityFromInput +=
                (
                    string __egoid,
                    string __identity,
                    string __KeySample,
                    string __fixup_x,
                    string __fixup_y,
                    string __fixup_angle

                    ) =>
                {
                    var xml = new XElement("SetVelocityFromInput",

                        new XAttribute("egoid", __egoid),


                        new XAttribute("i", __identity),
                        new XAttribute("k", __KeySample),
                        new XAttribute("x", __fixup_x),
                        new XAttribute("y", __fixup_y),
                        new XAttribute("angle", __fixup_angle)

                    );

                    if (__transport_out != null)
                        __transport_out(xml.ToString());
                };
            #endregion


            this.InvokeWhenStageIsReady(
              delegate
              {
                  // http://gamua.com/starling/first-steps/
                  // http://forum.starling-framework.org/topic/starling-air-desktop-extendeddesktop-fullscreen-issue
                  Starling.handleLostContext = true;

                  var s = new Starling(
                      typeof(StarlingGameSpriteWithTestDriversSync).ToClassToken(),
                      this.stage
                  );


                  //Starling.current.showStats

                  s.showStats = true;

                  #region atresize
                  Action atresize = delegate
                  {
                      // http://forum.starling-framework.org/topic/starling-stage-resizing

                      s.viewPort = new ScriptCoreLib.ActionScript.flash.geom.Rectangle(
                          0, 0, this.stage.stageWidth, this.stage.stageHeight
                      );

                      s.stage.stageWidth = this.stage.stageWidth;
                      s.stage.stageHeight = this.stage.stageHeight;


                      //b2stage_centerize();
                  };

                  atresize();
                  #endregion

                  StarlingGameSpriteBase.onresize =
                      yield =>
                      {
                          this.stage.resize += delegate
                          {
                              atresize();

                              yield(this.stage.stageWidth, this.stage.stageHeight);
                          };

                          yield(this.stage.stageWidth, this.stage.stageHeight);
                      };




                  this.stage.enterFrame +=
                      delegate
                      {




                          StarlingGameSpriteBase.onframe(this.stage, s);
                      };

                  s.start();

              }
          );
        }

    }

}
