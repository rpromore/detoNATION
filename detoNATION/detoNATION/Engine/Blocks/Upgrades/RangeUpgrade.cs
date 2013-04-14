using System;
using Microsoft.Xna.Framework;

namespace TestGame
{
	class RangeUpgrade : UpgradeBlock
	{
		public RangeUpgrade (Point gridPosition) : base(gridPosition)
		{
		}

		public override void Pickup (Player p)
		{
			if (p.BombRange < Config.BombMaxRange) {
				p.BombRange++;
				// mark for death
				base.Pickup (p);
			}
		}
	}
}

