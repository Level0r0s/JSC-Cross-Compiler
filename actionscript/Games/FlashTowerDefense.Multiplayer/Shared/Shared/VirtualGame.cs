﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using ScriptCoreLib.Shared.Nonoba.Generic;

using ScriptCoreLib;

namespace FlashTowerDefense.Shared
{

	[Script]
	public class VirtualGame : ServerGameBase<SharedClass1.IEvents, SharedClass1.IMessages, VirtualPlayer>
	{

		public override void UserJoined(VirtualPlayer player)
		{
			Console.WriteLine("UserJoined " + player.Username);

			player.FromPlayer.Ping += e => player.LastMessage = DateTime.Now;
			player.FromPlayer.PlayerAdvertise += e => player.ToOthers.ServerPlayerAdvertise(player.UserId, player.Username, e.ego);
			player.FromPlayer.ReadyForServerRandomNumbers += e => player.GameEventStatus = VirtualPlayer.GameEventStatusEnum.Ready;
			player.FromPlayer.CancelServerRandomNumbers += e => player.GameEventStatus = VirtualPlayer.GameEventStatusEnum.Cancelled;

			player.FromPlayer.AddKillScore += e => player.AddScore("killscore", e.killscore);

			player.ToPlayer.ServerPlayerHello(player.UserId, player.Username);

			player.ToOthers.ServerPlayerJoined(
			   player.UserId, player.Username
			);

			AtDelay(
				delegate
				{
					player.ToPlayer.ServerMessage("Game will start shortly!");
				},
				25
			);

		}

		public override void UserLeft(VirtualPlayer player)
		{
			player.ToOthers.ServerPlayerLeft(player.UserId, player.Username);
		}

		public override void GameStarted()
		{
			AtInterval(SendNextWave, 5000);
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		private void SendNextWave()
		{
			var z = GenerateRandomNumbers();


			Console.WriteLine("Next Wave");

			foreach (var i in Users)
			{
				i.ToPlayer.ServerRandomNumbers(z);
			}

		}

		private double[] GenerateRandomNumbers()
		{
			var a = new List<double>();
			var r = new Random();

			for (int i = 0; i < 100; i++)
			{
				a.Add(r.NextDouble());
			}
			var z = a.ToArray();
			return z;
		}
	}
}
