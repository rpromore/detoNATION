using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yna.Engine;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace TestGame
{
    class HumanPlayer : Player
    {
        public HumanPlayer() : base()
        {

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if (YnG.Keys.Pressed(Keys.Left))
            {
                Move(MoveDirections.LEFT);
            }
            else if (YnG.Keys.Pressed(Keys.Right))
            {
                Move(MoveDirections.RIGHT);
            }
            else if (YnG.Keys.Pressed(Keys.Up))
            {
                Move(MoveDirections.UP);
            }
            else if (YnG.Keys.Pressed(Keys.Down))
            {
                Move(MoveDirections.DOWN);
            }
            else
            {
                Move(MoveDirections.NONE);
            }

            if (YnG.Keys.Pressed(Keys.Space))
            {
                PlaceBomb();
            }
        }
    }
}
