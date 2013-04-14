using System;
using System.Security.Cryptography;

namespace TestGame
{
	public class BetterRandom
	{
		private const int BufferSize = 1024;
		private byte[] RandomBuffer;
		private int offset;
		private RNGCryptoServiceProvider rng;

		public BetterRandom ()
		{
			RandomBuffer = new byte[BufferSize];
			rng = new RNGCryptoServiceProvider();
			offset = RandomBuffer.Length;
		}

		private void Fill ()
		{
			rng.GetBytes (RandomBuffer);
			offset = 0;
		}

		public int Next ()
		{
			if (offset >= RandomBuffer.Length) {
				Fill ();
			}
			int val = BitConverter.ToInt32 (RandomBuffer, offset) & 0x7fffffff;
			offset += sizeof(int);
			return val;
		}
		public int Next(int max) 
		{
			return (int)(Next () % max);
		}
		public int Next(int min, int max) 
		{
			if (max < min)
				throw new ArgumentOutOfRangeException();
			int range = max-min;
			return min+Next (range);
		}
		public void GetBytes (byte[] buff)
		{
			rng.GetBytes (buff);
		}
	}
}

