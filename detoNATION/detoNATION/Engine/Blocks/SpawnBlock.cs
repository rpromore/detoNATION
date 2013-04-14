using System;
using Microsoft.Xna.Framework;
using Yna.Engine.Graphics;

namespace TestGame
{
	class SpawnBlock : Block
	{
		public SpawnBlock (Point gridPosition) : base(gridPosition)
		{
			Color = Color.Purple;
			_texture = YnGraphics.CreateTexture(Color, Rectangle.Width, Rectangle.Height);
			IsPassable = true;
			CanCatchFire = true;
			CanPlaceBomb = true;
			CanFireSpread = true;
			IsDestroyable = false;
		}
	}
}

