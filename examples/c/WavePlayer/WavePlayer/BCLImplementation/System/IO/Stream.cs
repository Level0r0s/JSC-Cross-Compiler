﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace WavePlayer.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.Stream))]
	internal abstract class __Stream
	{

		public __Stream()
		{

		}


		public InternalFunc<object, long> __Stream_get_Length;
		public long Length
		{
			get
			{
				if (__Stream_get_Length == null)
				{
					return 0;
				}

				return __Stream_get_Length(this);
			}
		}

		public InternalFunc<object, long, SeekOrigin, long> __Stream_Seek;
		public long Seek(long offset, SeekOrigin origin)
		{
			// no such thing as abstract in c...
			// we need a vTable?

			return __Stream_Seek(this, offset, origin);
		}

		public InternalAction<object, byte[], int, int> __Stream_Write;
		public void Write(byte[] buffer, int offset, int count)
		{
			if (__Stream_Write == null)
				return;

			// no such thing as abstract in c...
			// we need a vTable?

			__Stream_Write(this, buffer, offset, count);
		}

		public void WriteByte(byte e)
		{
			Write(new[] { e }, 0, 1);
		}

		public InternalFunc<object, byte[], int, int, int> __Stream_Read;
		public int Read(byte[] buffer, int offset, int count)
		{
			return __Stream_Read(this, buffer, offset, count);
		}
	}
}
