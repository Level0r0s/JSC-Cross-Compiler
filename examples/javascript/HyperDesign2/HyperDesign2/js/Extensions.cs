﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace HyperDesign2.js
{
	[Script]
	static class Extensions
	{
		public static int Random(this int i)
		{
			return new Random().Next(i);
		}
	}
}
