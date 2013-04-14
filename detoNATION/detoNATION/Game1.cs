#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
using Yna.Engine;
using Yna.Engine.Graphics.Component;
#endregion

namespace TestGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : YnGame
    {

        public Game1()
            : base(800, 600, "Test Game")
        {
			IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            stateManager.Add(new SinglePlayer("basicSample"), true);
        }
    }
}