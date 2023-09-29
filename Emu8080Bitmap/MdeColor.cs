using System;
namespace Emu8080Bitmap
{
	public class MdeColor
	{
		public readonly byte R, G, B;

		public MdeColor(byte r, byte g, byte b)
		{
			R = r;
			G = g;
			B = b;
		}

        public static MdeColor Random(Random rand)
        {
            byte r = (byte)rand.Next(256);
            byte g = (byte)rand.Next(256);
            byte b = (byte)rand.Next(256);
            return new MdeColor(r, g, b);
        }
    }
}

