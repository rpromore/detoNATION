using System;
using Microsoft.Xna.Framework;
using Yna.Engine.Graphics;

namespace TestGame
{
	class AirBlock : Block
	{
		public AirBlock (Point gridPosition) : base(gridPosition)
		{
			Color = Color.Blue;
			_texture = YnGraphics.CreateTexture(Color, Rectangle.Width, Rectangle.Height);
			IsPassable = true;
			CanCatchFire = true;
			CanPlaceBomb = true;
			CanFireSpread = true;
			IsDestroyable = false;
		}
	}
}

