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
    class SinglePlayer : YnState2D
    {
        private Engine engine;
        //private YnGroup players;
        //private YnGroup blocks;

        public SinglePlayer(string name)
            : base(name)
        {
            YnSprite background = new YnSprite(new Rectangle(0, 0, YnG.Width, YnG.Height), Color.AliceBlue);
            Add(background);
        }

        public override void Initialize()
        {
            base.Initialize();

            engine = new Engine();
            engine.PlayerManager.AddHumanPlayer();

            foreach (YnGroup g in engine.BlockManager.Blocks.Values)
            {
                Add(g);
            }
            foreach (Player p in engine.PlayerManager.Players.Values)
            {
                Add(p);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
			engine.Update(gameTime);
			if (YnG.Keys.JustPressed (Keys.J)) {
				Console.WriteLine ("Spawning player");
				Add(engine.PlayerManager.AddAIPlayer());
			}
        }
    }
}
