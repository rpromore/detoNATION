using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yna.Engine.Graphics;
using Microsoft.Xna.Framework;
using Yna.Engine;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
    class Engine
    {
        #region Properties

        private BlockManager blockManager;
		private PlayerManager playerManager;
        private bool doDraw;
        private YnGroup drawPlayers;
        private YnGroup drawBlocks;

        #endregion Properties

        #region Getters/Setters

        public BlockManager BlockManager
        {
            get { return blockManager; }
            set { blockManager = value; }
        }

		public PlayerManager PlayerManager
        {
            get { return playerManager; }
            set { playerManager = value; }
        }

        public bool DoDraw {
            get { return doDraw; }
            set { doDraw = value; }
        }

        public YnGroup DrawPlayers
        {
            get { return drawPlayers; }
            set { drawPlayers = value; }
        }
        public YnGroup DrawBlocks
        {
            get { return drawBlocks; }
            set { drawBlocks = value; }
        }

        #endregion Getters/Setters

        #region Constructors

        public Engine()
        {
            blockManager = new BlockManager(this);
			playerManager = new PlayerManager(this);
            doDraw = false;
        }

        #endregion Constructors

        #region Methods

		public void Update (GameTime gameTime)
		{
			blockManager.Update (gameTime);
			playerManager.Update (gameTime);
		}

        #endregion Methods
    }
}
