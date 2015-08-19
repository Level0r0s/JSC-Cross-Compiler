using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms
{
	public static class References
	{
		// https://github.com/mrdoob/three.js/wiki/How-to-use-OpenGL-or-ANGLE-rendering-on-Windows
		// chrome.exe --use-gl=desktop
		//		GL_RENDERER ANGLE(Intel(R) HD Graphics Family Direct3D11 vs_5_0 ps_5_0)
		//GL_VERSION OpenGL ES 2.0 (ANGLE 2.1.524e3bde19d0)


		// http://on-demand.gputechconf.com/gtc/2014/video/S4550-shadertoy-fragment-shader.mp4

		// http://stackoverflow.com/questions/17047602/proper-way-to-initialize-a-c-sharp-dictionary-with-values-already-in-it
		// on nexus 9 we should lear to react to touch move
		// can we get a continious feedback loop going where 
		// we could add content from the live version of the debugged app?
		// like describe what we are seeing and have it stored as source code for the next iteration
		public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
		{
			// should we want to generate it?

			// group by runs on all devices, fps?
			// tags?

			//  /FilterTo:$(SolutionDir)
			// how will those shaders look like on VR?

			// this is special
			["ChromeShaderToySimpleLoadingScreenByNdel"] = () => new ChromeShaderToySimpleLoadingScreenByNdel.Shaders.ProgramFragmentShader(),

			["ChromeShaderToyColumns"] = () => new ChromeShaderToyColumns.Shaders.ProgramFragmentShader(),

			// can we have alt-tab pip ?

			#region /b/
			["BatmanLogoByIq"] = () => new BatmanLogoByIq.Shaders.ProgramFragmentShader(),
			["BatsByGaz"] = () => new BatsByGaz.Shaders.ProgramFragmentShader(),
			["BeautypiByIq"] = () => new BeautypiByIq.Shaders.ProgramFragmentShader(),
			["BeeHiveByMovax"] = () => new BeeHiveByMovax.Shaders.ProgramFragmentShader(),
			//["BiplanesInTheBadlandsByDr2"] = () => new BiplanesInTheBadlandsByDr2.Shaders.ProgramFragmentShader(),
			["BlankspaceByNBickford"] = () => new BlankspaceByNBickford.Shaders.ProgramFragmentShader(),
			["BlobsByPaulo"] = () => new BlobsByPaulo.Shaders.ProgramFragmentShader(),
			["BlueWallClockByC3d"] = () => new BlueWallClockByC3d.Shaders.ProgramFragmentShader(),
			["BoingBallByUnitZeroOne"] = () => new BoingBallByUnitZeroOne.Shaders.ProgramFragmentShader(),
			["BRDFsRUsByAntonalog"] = () => new BRDFsRUsByAntonalog.Shaders.ProgramFragmentShader(),
			["BokehBlurByKabuto"] = () => new ChromeShaderToyBokehBlurByKabuto.Shaders.ProgramFragmentShader(),
			["ButterfliesByFizzer"] = () => new ButterfliesByFizzer.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyBenderByIq"] = () => new ChromeShaderToyBenderByIq.Shaders.ProgramFragmentShader(),
			#endregion


			
			#region /e/
			["ChromeShaderToyEdgeAAByTrisomie"] = () => new ChromeShaderToyEdgeAAByTrisomie.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyEiffieBallsByEiffie"] = () => new ChromeShaderToyEiffieBallsByEiffie.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyEiffieBox"] = () => new ChromeShaderToyEiffieBox.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyExplosionByGreen"] = () => new ChromeShaderToyExplosionByGreen.Shaders.ProgramFragmentShader(),
			["EasterEggByNervus"] = () => new EasterEggByNervus.Shaders.ProgramFragmentShader(),
			["EffusingLavaByNexor"] = () => new EffusingLavaByNexor.Shaders.ProgramFragmentShader(),
			["ElectroPrimByAlex"] = () => new ElectroPrimByAlex.Shaders.ProgramFragmentShader(),
			["EllingtonVisitsShaderToyByMPlanck"] = () => new EllingtonVisitsShaderToyByMPlanck.Shaders.ProgramFragmentShader(),
			["EscherPlanariaByMattz"] = () => new EscherPlanariaByMattz.Shaders.ProgramFragmentShader(),
			["ExovoidLogoByRoberto"] = () => new ExovoidLogoByRoberto.Shaders.ProgramFragmentShader(),
			#endregion


			#region /g/
			["ChromeShaderToyGmetaballsByGermangb"] = () => new ChromeShaderToyGmetaballsByGermangb.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyGlassPolyhedronByNrx"] = () => new ChromeShaderToyGlassPolyhedronByNrx.Shaders.ProgramFragmentShader(),
			["GalaxySpiralsByGuil"] = () => new GalaxySpiralsByGuil.Shaders.ProgramFragmentShader(),
			["GameLogoByVladstorm"] = () => new GameLogoByVladstorm.Shaders.ProgramFragmentShader(),
			["GammaCorrectnessByZavie"] = () => new GammaCorrectnessByZavie.Shaders.ProgramFragmentShader(),
			["GeneratorsByKali"] = () => new GeneratorsByKali.Shaders.ProgramFragmentShader(),
			["GlassWithCausticByAndregc"] = () => new GlassWithCausticByAndregc.Shaders.ProgramFragmentShader(),
			["GlxgearsByBear"] = () => new GlxgearsByBear.Shaders.ProgramFragmentShader(),
			["GoGoLegoManByIapafoto"] = () => new GoGoLegoManByIapafoto.Shaders.ProgramFragmentShader(),
			["GrapheneByFabrice"] = () => new GrapheneByFabrice.Shaders.ProgramFragmentShader(),
			["GraphingByNimitz"] = () => new GraphingByNimitz.Shaders.ProgramFragmentShader(),
			["GuitarByAtyuwen"] = () => new GuitarByAtyuwen.Shaders.ProgramFragmentShader(),
			["GyratingGyroscopeByDr2"] = () => new GyratingGyroscopeByDr2.Shaders.ProgramFragmentShader(),
			#endregion

			#region /h/
			// alpha! via discard
			["ChromeShaderToyHardEdgeShadowByGltracy"] = () => new ChromeShaderToyHardEdgeShadowByGltracy.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyHetchyScketchyByXbe"] = () => new ChromeShaderToyHetchyScketchyByXbe.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyHolographicByTdm"] = () => new ChromeShaderToyHolographicByTdm.Shaders.ProgramFragmentShader(),
			["HalfLife3ByNikos"] = () => new HalfLife3ByNikos.Shaders.ProgramFragmentShader(),
			["HauntedForest2ByReinder"] = () => new HauntedForest2ByReinder.Shaders.ProgramFragmentShader(),
			["HeliByAvix"] = () => new HeliByAvix.Shaders.ProgramFragmentShader(),
			["HexafieldByKevs"] = () => new HexafieldByKevs.Shaders.ProgramFragmentShader(),
			["HypercubeByElias"] = () => new HypercubeByElias.Shaders.ProgramFragmentShader(),
			#endregion



			#region /j/
			["JackoLanternByPMalin"] = () => new JackoLanternByPMalin.Shaders.ProgramFragmentShader(),
			["JellyfishByVlad"] = () => new JellyfishByVlad.Shaders.ProgramFragmentShader(),
			["JoyDivisionByXbe"] = () => new JoyDivisionByXbe.Shaders.ProgramFragmentShader(),
			["JusterBeaverByMovax"] = () => new JusterBeaverByMovax.Shaders.ProgramFragmentShader(),
			#endregion

			#region /k/
			["ChromeShaderToyKajastusByMarken"] = () => new ChromeShaderToyKajastusByMarken.Shaders.ProgramFragmentShader(),
			["KalizylByBergi"] = () => new KalizylByBergi.Shaders.ProgramFragmentShader(),
			["KMoonByKali"] = () => new KMoonByKali.Shaders.ProgramFragmentShader(),
			#endregion



			#region /l/
			["ChromeShaderToyLavaDripByFabrice"] = () => new ChromeShaderToyLavaDripByFabrice.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyLightThornByVlad"] = () => new ChromeShaderToyLightThornByVlad.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyLightCycleByGreen"] = () => new ChromeShaderToyLightCycleByGreen.Shaders.ProgramFragmentShader(),

			// 32 crash xt7
			["ChromeShaderToyLimboByDaeken"] = () => new ChromeShaderToyLimboByDaeken.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyLittleMonsterByHLorenzi"] = () => new ChromeShaderToyLittleMonsterByHLorenzi.Shaders.ProgramFragmentShader(),
			["LaDecimaByCiberxtrem"] = () => new LaDecimaByCiberxtrem.Shaders.ProgramFragmentShader(),
			["LegendOfZeldaByHLorenzi"] = () => new LegendOfZeldaByHLorenzi.Shaders.ProgramFragmentShader(),
			["LightingRoomByCaosdoar"] = () => new LightingRoomByCaosdoar.Shaders.ProgramFragmentShader(),
			["LightiningByAsti"] = () => new LightiningByAsti.Shaders.ProgramFragmentShader(),
			["LineIntersectionByThe23"] = () => new LineIntersectionByThe23.Shaders.ProgramFragmentShader(),
			["LineIntersectionInteractiveByThe23"] = () => new LineIntersectionInteractiveByThe23.Shaders.ProgramFragmentShader(),
			["LittleFluffyCloudsByGreen"] = () => new LittleFluffyCloudsByGreen.Shaders.ProgramFragmentShader(),
			["LoadingOrbByBjarnia"] = () => new LoadingOrbByBjarnia.Shaders.ProgramFragmentShader(),
			["LostInTheFieldByRk"] = () => new LostInTheFieldByRk.Shaders.ProgramFragmentShader(),
			#endregion


			["ChromeShaderToyllamelsByEiffie"] = () => new ChromeShaderToyllamelsByEiffie.Shaders.ProgramFragmentShader(),



			#region /n/
			["ChromeShaderToyNeonParallaxByNimitz"] = () => new ChromeShaderToyNeonParallaxByNimitz.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyNoiseDistributionsByHornet"] = () => new ChromeShaderToyNoiseDistributionsByHornet.Shaders.ProgramFragmentShader(),
			["NanoTubesByTrisomie"] = () => new NanoTubesByTrisomie.Shaders.ProgramFragmentShader(),
			["NeptunianByEspitz"] = () => new NeptunianByEspitz.Shaders.ProgramFragmentShader(),
			["NSAEyeballByEiffie"] = () => new NSAEyeballByEiffie.Shaders.ProgramFragmentShader(),
			["NumbersByPMalin"] = () => new NumbersByPMalin.Shaders.ProgramFragmentShader(),
			#endregion


			#region /o/
			["ChromeShaderToyOblivionByNimitz"] = () => new ChromeShaderToyOblivionByNimitz.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyOcclusionClippingByIq"] = () => new ChromeShaderToyOcclusionClippingByIq.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyOculusTestByDaeken"] = () => new ChromeShaderToyOculusTestByDaeken.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyOcuLimboByDaeken"] = () => new ChromeShaderToyOcuLimboByDaeken.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyOnOffSpikesByMovAX13h"] = () => new ChromeShaderToyOnOffSpikesByMovAX13h.Shaders.ProgramFragmentShader(),
			["OblivionByHoskins"] = () => new OblivionByHoskins.Shaders.ProgramFragmentShader(),
			["OblivionRadarByNdel"] = () => new OblivionRadarByNdel.Shaders.ProgramFragmentShader(),
			["OldWarehouseByFizzer"] = () => new OldWarehouseByFizzer.Shaders.ProgramFragmentShader(),
			["OrchardNightByEiffie"] = () => new OrchardNightByEiffie.Shaders.ProgramFragmentShader(),
			["OrderedDitherByKlk"] = () => new OrderedDitherByKlk.Shaders.ProgramFragmentShader(),
			["OtherWorldByAlgorithm"] = () => new OtherWorldByAlgorithm.Shaders.ProgramFragmentShader(),
			#endregion



			["ChromeShaderToyQuadraticBezierByMattdesl"] = () => new ChromeShaderToyQuadraticBezierByMattdesl.Shaders.ProgramFragmentShader(),
			["QuadraticBezierByIq"] = () => new QuadraticBezierByIq.Shaders.ProgramFragmentShader(),

			#region /r/
			["ChromeShaderToyRavingErnieByBero"] = () => new ChromeShaderToyRavingErnieByBero.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRayFogByDemofox"] = () => new ChromeShaderToyRayFogByDemofox.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRacingGameByEiffie"] = () => new ChromeShaderToyRacingGameByEiffie.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRefractiveSpheresByKig"] = () => new ChromeShaderToyRefractiveSpheresByKig.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRedditAlienByGleurop"] = () => new ChromeShaderToyRedditAlienByGleurop.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRaymarchByElias"] = () => new ChromeShaderToyRaymarchByElias.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRaymarchEdgeDetectionByHLorenzi"] = () => new ChromeShaderToyRaymarchEdgeDetectionByHLorenzi.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRaymarchingExampleByJack"] = () => new ChromeShaderToyRaymarchingExampleByJack.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRecogniserByFizzer"] = () => new ChromeShaderToyRecogniserByFizzer.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRefractionByHLorenzi"] = () => new ChromeShaderToyRefractionByHLorenzi.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRelentlessBySrtuss"] = () => new ChromeShaderToyRelentlessBySrtuss.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRockyCoast"] = () => new ChromeShaderToyRockyCoast.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRollingBallByHoskins"] = () => new ChromeShaderToyRollingBallByHoskins.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRotatePyramidByGyabo"] = () => new ChromeShaderToyRotatePyramidByGyabo.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRotatingByGa2arch"] = () => new ChromeShaderToyRotatingByGa2arch.Shaders.ProgramFragmentShader(),
			["RainbowSlicesByFizzer"] = () => new RainbowSlicesByFizzer.Shaders.ProgramFragmentShader(),
			["RayBertByHoskins"] = () => new RayBertByHoskins.Shaders.ProgramFragmentShader(),
			["RayConeRayFrustumByRobert"] = () => new RayConeRayFrustumByRobert.Shaders.ProgramFragmentShader(),
			["RaymarchingAttempt2ByCraxic"] = () => new RaymarchingAttempt2ByCraxic.Shaders.ProgramFragmentShader(),
			["RaymarchingDisplacementByJcanabald"] = () => new RaymarchingDisplacementByJcanabald.Shaders.ProgramFragmentShader(),
			["RaymarchingTutorialByObjelisks"] = () => new RaymarchingTutorialByObjelisks.Shaders.ProgramFragmentShader(),
			["RaymarchingTweaksByLanza"] = () => new RaymarchingTweaksByLanza.Shaders.ProgramFragmentShader(),
			["RedCellsByPMalin"] = () => new RedCellsByPMalin.Shaders.ProgramFragmentShader(),
			["RefelectingCubeByTriggerHLM"] = () => new RefelectingCubeByTriggerHLM.Shaders.ProgramFragmentShader(),
			["ReflectingCatByDr2"] = () => new ReflectingCatByDr2.Shaders.ProgramFragmentShader(),
			["RiseOfShroomByAlgorithm"] = () => new RiseOfShroomByAlgorithm.Shaders.ProgramFragmentShader(),
			["RiverFlightByDr2"] = () => new RiverFlightByDr2.Shaders.ProgramFragmentShader(),
			["RoadToHellByRez"] = () => new RoadToHellByRez.Shaders.ProgramFragmentShader(),
			["RockShapesWIPByAsteropaeus"] = () => new RockShapesWIPByAsteropaeus.Shaders.ProgramFragmentShader(),
			["RoseByHoskins"] = () => new RoseByHoskins.Shaders.ProgramFragmentShader(),
			["RutherfordAtomByMattz"] = () => new RutherfordAtomByMattz.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRubikSolverByKali"] = () => new ChromeShaderToyRubikSolverByKali.Shaders.ProgramFragmentShader(),
			#endregion

	
			#region /t/
			["ChromeShaderToyTextCandyByCPU"] = () => new ChromeShaderToyTextCandyByCPU.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyTexturedEllipsoidsByFabrice"] = () => new ChromeShaderToyTexturedEllipsoidsByFabrice.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyTokyoByReinder"] = () => new ChromeShaderToyTokyoByReinder.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyTrainByDr2"] = () => new ChromeShaderToyTrainByDr2.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyTrainRideByDr2"] = () => new ChromeShaderToyTrainRideByDr2.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyTriangleDistanceByIq"] = () => new ChromeShaderToyTriangleDistanceByIq.Shaders.ProgramFragmentShader(),
			["TestOfBleedingColorByPredatiti"] = () => new TestOfBleedingColorByPredatiti.Shaders.ProgramFragmentShader(),
			["TetrahedralInterpolationByPaniq"] = () => new TetrahedralInterpolationByPaniq.Shaders.ProgramFragmentShader(),
			["TetrahedronatorByEiffie"] = () => new TetrahedronatorByEiffie.Shaders.ProgramFragmentShader(),
			["TetrahedronByCandycat"] = () => new TetrahedronByCandycat.Shaders.ProgramFragmentShader(),
			["TetrisByKali"] = () => new TetrisByKali.Shaders.ProgramFragmentShader(),
			["TileableNoiseByHoskins"] = () => new TileableNoiseByHoskins.Shaders.ProgramFragmentShader(),
			["TileableWaterCausticByHoskins"] = () => new TileableWaterCausticByHoskins.Shaders.ProgramFragmentShader(),
			["TinyCuttingByAiekick"] = () => new TinyCuttingByAiekick.Shaders.ProgramFragmentShader(),
			["ToonCloudByAntoineC"] = () => new ToonCloudByAntoineC.Shaders.ProgramFragmentShader(),
			["TopologicaByOtavio"] = () => new TopologicaByOtavio.Shaders.ProgramFragmentShader(),
			["TorusJourneyByFalcao"] = () => new TorusJourneyByFalcao.Shaders.ProgramFragmentShader(),
			["ToTheRoadOfRibbon"] = () => new ToTheRoadOfRibbon.Shaders.ProgramFragmentShader(),
			["TraceConeWithCRTByKlk"] = () => new TraceConeWithCRTByKlk.Shaders.ProgramFragmentShader(),
			["TreeInGrassBySphinx"] = () => new TreeInGrassBySphinx.Shaders.ProgramFragmentShader(),
			["TreesByGuil"] = () => new TreesByGuil.Shaders.ProgramFragmentShader(),
			["TrollsCaveByFatumR"] = () => new TrollsCaveByFatumR.Shaders.ProgramFragmentShader(),
			["TruchetTentaclesByWaha"] = () => new TruchetTentaclesByWaha.Shaders.ProgramFragmentShader(),
			["TruePinballPhysicsByArchee"] = () => new TruePinballPhysicsByArchee.Shaders.ProgramFragmentShader(),
			["TrumpetByBaldand"] = () => new TrumpetByBaldand.Shaders.ProgramFragmentShader(),
			["Tunnel1ByWaha"] = () => new Tunnel1ByWaha.Shaders.ProgramFragmentShader(),
			["TwistyTorusByBloxard"] = () => new TwistyTorusByBloxard.Shaders.ProgramFragmentShader(),
			#endregion


			["UselessBoxByMovax"] = () => new UselessBoxByMovax.Shaders.ProgramFragmentShader(),
			["UglyBrickByPsykotic"] = () => new UglyBrickByPsykotic.Shaders.ProgramFragmentShader(),


			#region /v/
			["ChromeShaderToyVornoiCubeMapByBenito"] = () => new ChromeShaderToyVornoiCubeMapByBenito.Shaders.ProgramFragmentShader(),
			["ValueToBitArrayByNikos"] = () => new ValueToBitArrayByNikos.Shaders.ProgramFragmentShader(),
			["ValveNoiseByImpossible"] = () => new ValveNoiseByImpossible.Shaders.ProgramFragmentShader(),
			["VisibleClockByDr2"] = () => new VisibleClockByDr2.Shaders.ProgramFragmentShader(),
			["VolumetricHelicesByNimitz"] = () => new VolumetricHelicesByNimitz.Shaders.ProgramFragmentShader(),
			["VolumetricRaycastingByXt95"] = () => new VolumetricRaycastingByXt95.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyVRCardboardGrid"] = () => new ChromeShaderToyVRCardboardGrid.Shaders.ProgramFragmentShader(),
			["VoxelPacManByNrx"] = () => new VoxelPacManByNrx.Shaders.ProgramFragmentShader(),
			["VoxelSaturnByGaz"] = () => new VoxelSaturnByGaz.Shaders.ProgramFragmentShader(),
			["VoxelTyreByHoskins"] = () => new VoxelTyreByHoskins.Shaders.ProgramFragmentShader(),
			["VRTestSceneByRaven"] = () => new VRTestSceneByRaven.Shaders.ProgramFragmentShader(),
			["VRMazeByNrx"] = () => new VRMazeByNrx.Shaders.ProgramFragmentShader(),
			#endregion


			#region /w/
			["WaterfallByZtri"] = () => new WaterfallByZtri.Shaders.ProgramFragmentShader(),
			["WindWakerOceanByPolyflare"] = () => new WindWakerOceanByPolyflare.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyWireframeByYasuo"] = () => new ChromeShaderToyWireframeByYasuo.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyWetStoneByTDM"] = () => new ChromeShaderToyWetStoneByTDM.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyWolfensteinByReinder"] = () => new ChromeShaderToyWolfensteinByReinder.Shaders.ProgramFragmentShader(),
			["WinterByIapafoto"] = () => new WinterByIapafoto.Shaders.ProgramFragmentShader(),
			["WireEggsByEiffie"] = () => new WireEggsByEiffie.Shaders.ProgramFragmentShader(),
			["WobblyThingByAvix"] = () => new WobblyThingByAvix.Shaders.ProgramFragmentShader(),
			["Wolf128ByFinalPatch"] = () => new Wolf128ByFinalPatch.Shaders.ProgramFragmentShader(),
			["WormsByIq"] = () => new WormsByIq.Shaders.ProgramFragmentShader(),
			#endregion


			["Xor3DAlienLandByXor"] = () => new Xor3DAlienLandByXor.Shaders.ProgramFragmentShader(),
			["XorMountainsByXor"] = () => new XorMountainsByXor.Shaders.ProgramFragmentShader(),
			["XorStormByXor"] = () => new XorStormByXor.Shaders.ProgramFragmentShader(),
			["x2001SpaceStationByOtavio"] = () => new x2001SpaceStationByOtavio.Shaders.ProgramFragmentShader(),
			["x2DFoldingByGaz"] = () => new x2DFoldingByGaz.Shaders.ProgramFragmentShader(),
			["x2DShadowCastingByTharich"] = () => new x2DShadowCastingByTharich.Shaders.ProgramFragmentShader(),
			["x2DSkullByStanton"] = () => new x2DSkullByStanton.Shaders.ProgramFragmentShader(),
			["x3DExcQByGaz"] = () => new x3DExcQByGaz.Shaders.ProgramFragmentShader(),
			["x3DLightingByXor"] = () => new x3DLightingByXor.Shaders.ProgramFragmentShader(),
			["x3DMetashapesByLewiz"] = () => new x3DMetashapesByLewiz.Shaders.ProgramFragmentShader(),

			["YellowMothByDr2"] = () => new YellowMothByDr2.Shaders.ProgramFragmentShader(),

			// cube?

			// 

		};
	}
}
