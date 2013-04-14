using System;
using Microsoft.Xna.Framework;
using Yna.Engine.Graphics;

namespace TestGame
{
	class BrickBlock : Block
	{
		public BrickBlock (Point gridPosition) : base(gridPosition)
		{
			Color = Color.Red;
			_texture = YnGraphics.CreateTexture(Color, Rectangle.Width, Rectangle.Height);
			IsPassable = false;
			CanCatchFire = true;
			CanPlaceBomb = false;
			CanFireSpread = false;
			IsDestroyable = true;
			ProvidesUpgrade = true;
		}
	}
}

