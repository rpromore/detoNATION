using System;
using Microsoft.Xna.Framework;

namespace TestGame
{
	class BombUpgrade : UpgradeBlock
	{
		public BombUpgrade (Point gridPosition) : base(gridPosition)
		{
		}

		public override void Pickup (Player p)
		{
			if (p.BombMax < Config.BombMaxNumber) {
				p.BombMax++;
				base.Pickup (p);
			}
		}
	}
}

