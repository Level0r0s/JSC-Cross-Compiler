using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.mx.graphics;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashRadialGradient
{
	public sealed class ApplicationSprite : Sprite
	{
		public const int Width = 200;
		public const int Height = 200;

		public static int[] colors = new[] { 0xFFCC66, 0x00FF00, 0x0000FF, 0xFF0000 };

		public int color1;
		public int colors_index = 0;

		Sprite s;
		Graphics g;

		public ApplicationSprite()
		{
			s = new Sprite().AttachTo(this);

			g = s.graphics;

			color1 = colors[0];


			addChild(
					new TextField
					{
						text = "powered by jsc",
						x = 20,
						y = 40,
						selectable = false,
						sharpness = -400,
						textColor = 0xffffff,
						mouseEnabled = false
					}
				);


			this.mouseMove +=
				ev =>
				{
					localX = (int)ev.stageX;
					localY = (int)ev.stageY;

					redraw();
				};

			this.click +=
				delegate
				{
					color1 = colors[++colors_index % (colors.Length - 1)];
					redraw();
				};

			var timer = new Timer(1000 / 24, 0);

			timer.timer +=
				delegate
				{
					counter++;

					redraw();
				};

			timer.start();

			redraw();
		}

		private void redraw()
		{
			g.clear();

			draw(counter % 360);
			draw((counter + this.localX) % 360);
			draw((counter + this.localY) % 360);
			draw((counter + this.localY + this.localX) % 360);
			draw((counter + this.localY - this.localX) % 360);

		}


		private int counter;
		private int localX;
		private int localY;

		private void draw(int angle)
		{
			var fill = new RadialGradient();


			fill.entries = new[] {
				new GradientEntry((uint)color1, 0.00, 0.5),
				new GradientEntry(0x000000, 0.33, 0.5),
				new GradientEntry(0x99FF33, 0.66, 0.0)
			};

			// Set focal point to upper left corner.
			fill.angle = angle;
			fill.focalPointRatio = -0.8;



			// Draw a box and fill it with the RadialGradient.
			g.moveTo(0, 0);

			var w = Width;
			var h = Height;

			fill.begin(g, new Rectangle(0, 0, w, h), new Point());

			{
				g.lineTo(w, 0);
				g.lineTo(w, h);
				g.lineTo(0, h);
				g.lineTo(0, 0);
			}

			fill.end(g);

		}
	}

}

//script: error JSC1000: ActionScript : Opcode not implemented: brtrue.s at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__OrderedEnumerable`1+<>c__DisplayClass6_0.<GetEnumerator>b__0
//internal compiler error at method
// assembly: X:\jsc.svn\examples\actionscript\FlashRadialGradient\FlashRadialGradient\bin\Debug\ScriptCoreLib.dll at
// type: ScriptCoreLib.Shared.BCLImplementation.System.Linq.__OrderedEnumerable`1+<>c__DisplayClass6_0, ScriptCoreLib, Version=4.6.0.0, Culture=neutral, PublicKeyToken=null
// method: <GetEnumerator>b__0
// ActionScript : Opcode not implemented: brtrue.s at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__OrderedEnumerable`1+<>c__DisplayClass6_0.<GetEnumerator>b__0

//0008 020001a3 ScriptCoreLib::ScriptCoreLib.Shared.BCLImplementation.System.__SZArrayEnumerator`1
//script: error JSC1000: ActionScript : Opcode not implemented: brtrue at ScriptCoreLib.ActionScript.Extensions.KnownEmbeddedResources.get_Item
//000c 020004bb ScriptCoreLib::ScriptCoreLib.ActionScript.BCLImplementation.System.__Task
//WriteMethodLocalVariables { DeclaringType = ScriptCoreLib.Shared.BCLImplementation.System.Data.__DataView, Name = ToTable, LocalIndex = 0, Count = 18 }
//internal compiler error at method
// assembly: X:\jsc.svn\examples\actionscript\FlashRadialGradient\FlashRadialGradient\bin\Debug\ScriptCoreLib.dll at
// type: ScriptCoreLib.ActionScript.Extensions.KnownEmbeddedResources, ScriptCoreLib, Version=4.6.0.0, Culture=neutral, PublicKeyToken=null
// method: get_Item
// ActionScript : Opcode not implemented: brtrue at ScriptCoreLib.ActionScript.Extensions.KnownEmbeddedResources.get_Item

//internal compiler error at method
// assembly: X:\jsc.svn\examples\actionscript\FlashRadialGradient\FlashRadialGradient\bin\Debug\ScriptCoreLib.dll at
// type: ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood, ScriptCoreLib, Version=4.6.0.0, Culture=neutral, PublicKeyToken=null
// method: CreatePaddedBuffer
// ActionScript : Opcode not implemented: stind.i1 at ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood.CreatePaddedBuffer
//	at jsc.Script.CompilerBase.BreakToDebugger(String e) in X:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.cs:line 267
//   at jsc.Script.CompilerBase.Break(String e) in X:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.cs:line 227
//   at jsc.Script.CompilerBase.EmitInstruction(Prestatement p, ILInstruction i, Type TypeExpectedOrDefault) in X:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.cs:line 1456
//   at jsc.Script.CompilerCLike.EmitPrestatement(Prestatement p) in X:\jsc.internal.git\compiler\jsc\Languages\CompilerCLike.cs:line 1668
//   at jsc.Script.CompilerBase.EmitPrestatementBlock(PrestatementBlock xbp, Predicate`1 predicate) in X:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.cs:line 815
//   at jsc.Script.CompilerBase.<>c__DisplayClass103_0.<WriteMethodBody>b__0() in X:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.cs:line 926
//   at jsc.Script.CompilerBase.WriteMethodBodyContent(ILBlock xb, Action h) in X:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.cs:line 853
//   at jsc.Script.CompilerBase.WriteMethodBody(MethodBase m, Predicate`1 predicate, Action CustomVariableInitialization, Action CustomReturnAction) in X:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.cs:line 903
