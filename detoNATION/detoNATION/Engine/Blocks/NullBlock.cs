using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TestGame
{
    class NullBlock : Block
    {
        public NullBlock(Point gridPosition)
            : base(gridPosition)
        {
			IsPassable = false;
			CanCatchFire = false;
			CanPlaceBomb = false;
			CanFireSpread = true;
			IsDestroyable = false;
        }
    }
}
