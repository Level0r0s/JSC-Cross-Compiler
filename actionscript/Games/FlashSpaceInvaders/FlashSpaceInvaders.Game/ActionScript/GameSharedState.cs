﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlashSpaceInvaders.ActionScript.FragileEntities;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public partial class GameSharedState
	{
		public const int MaxObjectsPerSection = 1000;
		public const int MaxPlayers = 32;

		public readonly List<object> LocalObjects = new List<object>();

		public readonly List<object> SharedObjects = new List<object>();

		public readonly Dictionary<int, List<object>> RemoteObjects = new Dictionary<int, List<object>>();

		public GameSharedState()
		{
			for (int i = 0; i < MaxPlayers; i++)
			{
				this.RemoteObjects[i] = new List<object>();
			}
		}

		public int this[object e]
		{
			get
			{
				var i = this.LocalObjects.IndexOf(e);

				if (i == -1)
					i = this.SharedObjects.IndexOf(e);

				if (i == -1)
					throw new Exception("This object is not known shared state");

				return i + MaxObjectsPerSection;
			}
		}

		public object this[int user, int i]
		{
			get
			{
				if (i == -1)
					return null;

				if (i < MaxObjectsPerSection)
					return this.RemoteObjects[user][i];

				return this.SharedObjects[i - MaxObjectsPerSection];
			}
		}

	}

	partial class Game
	{
		public readonly GameSharedState SharedState = new GameSharedState();

		void InitializeSharedState()
		{

		}

	}
}
