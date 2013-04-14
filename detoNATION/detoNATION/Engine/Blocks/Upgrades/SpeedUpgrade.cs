using System;
using Microsoft.Xna.Framework;

namespace TestGame
{
	class SpeedUpgrade : UpgradeBlock
	{
		public SpeedUpgrade (Point gridPosition) : base(gridPosition)
		{
		}

		public override void Pickup (Player p)
		{
			Console.WriteLine ("More speed!");
			p.Acceleration = new Vector2(p.Acceleration.X+.1f, p.Acceleration.Y+.1f);
			base.Pickup (p);
		}
	}
}

