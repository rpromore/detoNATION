using System;
using Microsoft.Xna.Framework;
using Yna.Engine.Graphics;

namespace TestGame
{
	class FireBlock : Block
	{
		private Bomb bomb;

		public FireBlock (Bomb b, Point gridPosition) : base(gridPosition)
		{
			bomb = b;
			Color = Color.Yellow;
			_texture = YnGraphics.CreateTexture(Color, Rectangle.Width, Rectangle.Height);
			IsPassable = true;
			CanCatchFire = true;
			CanPlaceBomb = false;
			CanFireSpread = true;
			IsDestroyable = false;
		}

		public override void OnPass (Player p)
		{
			base.OnPass (p);
			if (p.Rectangle.Intersects (Rectangle)) {
				Vector2 offset = Vector2.Divide (new Vector2 (Width, Height), 2.5f);
				Rectangle killArea = new Rectangle(Convert.ToInt16(Rectangle.X+offset.X), Convert.ToInt16(Rectangle.Y+offset.Y), Convert.ToInt16(Rectangle.Width-offset.X), Convert.ToInt16(Rectangle.Height-offset.Y));
				if (p.Rectangle.Intersects (killArea)) {
					//p.Kill ();
					bomb.Player.Killed(p);
				}
			}
		}
	}
}

