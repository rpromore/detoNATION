using System;
using TestGame;
using Yna.Engine.Graphics;
using Microsoft.Xna.Framework;

namespace TestGame
{
	class UpgradeBlock : Block
	{
		// The chance parameter tells the blockmanager the chances of this upgrade appearing.
		// If chance is 1, there is a 1:1 chance of it appearing.
		// If chance is 100, there is a 1:100 chance of it appearing.
		private int chance = 1;

		public int Chance {
			get { return chance; }
			set { Chance = value; }
		}

		public UpgradeBlock (Point gridPosition) : base(gridPosition)
		{
			Color = Color.Green;
			_texture = YnGraphics.CreateTexture(Color, Rectangle.Width, Rectangle.Height);
			IsPassable = true;
			CanCatchFire = true;
			CanPlaceBomb = true;
			CanFireSpread = true;
			IsDestroyable = false;
		}

		public override void OnPass (Player p)
		{
			base.OnPass (p);
			Pickup (p);
		}

		public virtual void Pickup(Player p) {
			Kill ();
		}
	}
}

