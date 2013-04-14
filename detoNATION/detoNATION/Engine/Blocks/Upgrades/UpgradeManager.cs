using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Security.Cryptography;

namespace TestGame
{
	// Responsible for random spawning of upgrades.
	class UpgradeManager
	{
		List<Type> upgrades;

		public UpgradeManager ()
		{
			upgrades = new List<Type>();
			//upgrades.Add (typeof(SpeedUpgrade));
			upgrades.Add (typeof(BombUpgrade));
			upgrades.Add (typeof(RangeUpgrade));
		}

		public UpgradeBlock SpawnUpgrade (Point p)
		{
			BetterRandom r = new BetterRandom();
			int n;
			foreach (Type u in upgrades) {
				n = r.Next (10);
				if (n == 1) {
					return (UpgradeBlock)Activator.CreateInstance(u, p);
				}
			}
			return null;
		}
	}
}

