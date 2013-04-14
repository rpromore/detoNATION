using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Yna.Engine.Graphics;

namespace TestGame
{
    class Block : YnEntity
    {
        #region Properties

        // The type of block.
        //private BlockType type;
        // The block's position in the grid, which is x*block width, y*block height.
        private Point gridPosition;
		private int health;

		// can player walk over
		private bool isPassable;
		// can player place bomb on
		private bool canPlaceBomb;
		// can this block catch fire
		private bool canCatchFire;
		// after this block as caught fire, can it spread to further blocks
		private bool canFireSpread;
		// can this block be destroyed
		private bool isDestroyable;
		// if this block will provide an upgrade upon destruction
		private bool providesUpgrade;

        #endregion Properties

        #region Getters/Setters

		/*
        public BlockType Type
        {
            get { return type; }
            set { type = value; }
        }
        */
        public Point GridPosition
        {
            get { return gridPosition; }
            set { gridPosition = value; }
        }
		public int Health {
			get { return health; }
			set { health = value; }
		}
		public bool IsPassable {
			get { return isPassable; }
			set { isPassable = value; }
		}
		public bool CanPlaceBomb {
			get { return canPlaceBomb; }
			set { canPlaceBomb = value; }
		}
		public bool CanCatchFire {
			get { return canCatchFire; }
			set { canCatchFire = value; }
		}
		public bool CanFireSpread {
			get { return canFireSpread; }
			set { canFireSpread = value; }
		}
		public bool IsDestroyable {
			get { return isDestroyable; }
			set { isDestroyable = value; }
		}
		public bool ProvidesUpgrade {
			get { return providesUpgrade; }
			set { providesUpgrade = value; }
		}

        #endregion Getters/Setters

        #region Constructors

        public Block(/*BlockType type, */Point gridPosition)
            : base("", new Vector2(gridPosition.X*Config.BlockWidth, gridPosition.Y*Config.BlockHeight))
        {
            //this.type = type;
            this.gridPosition = gridPosition;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 32, 32);
            _assetLoaded = true;
            _position = new Vector2(Rectangle.X, Rectangle.Y);
            _rectangle = Rectangle;

			IsPassable = false;
			CanCatchFire = false;
			CanFireSpread = false;
			CanPlaceBomb = false;
			IsDestroyable = false;
			ProvidesUpgrade = false;
        }

        #endregion Constructors

		#region Methods
		public virtual void OnPass(Player p) {

		}
		#endregion Methods
    }
}
