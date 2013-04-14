using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yna.Engine.Graphics;
using Yna.Engine;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace TestGame
{
    class Player : YnSprite
    {
        #region Properties
		private long id;
        private Engine engine;
        private Point gridPosition;
        private List<Bomb> bombs;
        private int bombNumber;
        private int bombNumberMax;
        private int bombRange;
		private int score = 0;
        #endregion Properties

        #region Getters/Setters
		public long ID {
			get { return id; }
			set { id = value; }
		}
        public Point GridPosition
        {
            get { return gridPosition; }
            set { gridPosition = value; }
        }
        public Engine Engine
        {
            get { return engine; }
            set { engine = value; }
        }
        public List<Bomb> Bombs
        {
            get { return bombs; }
            set { bombs = value; }
        }
		public int BombRange {
			get { return bombRange; }
			set { bombRange = value; }
		}
		public int BombMax {
			get { return bombNumberMax; }
			set { bombNumberMax = value; }
		}
        #endregion Getters/Setters

        #region Constructors
        public Player() : this(Vector2.Zero, Color.Orange)
        {

        }

        public Player(Vector2 position)
            : this(position, Color.Orange)
        {

        }

        public Player(Vector2 position, Color c)
            : base(new Rectangle((int)position.X, (int)position.Y, 32, 32), c)
        {
            bombs = new List<Bomb>();
            bombNumber = 0;
            bombNumberMax = 3;
            bombRange = 1;
			//Acceleration = new Vector2(2f, 2f);
        }
        #endregion Constructors

        #region Methods
        public void Move(MoveDirections dir)
        {
            switch (dir)
            {
                case MoveDirections.LEFT:
                    VelocityX = -Acceleration.X;
                    VelocityY = 0;
                    break;
                case MoveDirections.UP:
                    VelocityY = -Acceleration.Y;
                    VelocityX = 0;
                    break;
                case MoveDirections.RIGHT:
                    VelocityX = Acceleration.X;
                    VelocityY = 0;
                    break;
                case MoveDirections.DOWN:
                    VelocityY = Acceleration.Y;
                    VelocityX = 0;
                    break;
                case MoveDirections.NONE:
                    Velocity = Vector2.Zero;
                    break;
            }
			Velocity = engine.BlockManager.AdjustPlayer (this);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // check grid position
            Point position = GetPosition();
            gridPosition.X = (position.X+16) / 32;
            gridPosition.Y = (position.Y+16) / 32;
			// This will help with collision, but will also detect if a player needs to die or gets an upgrade.
			engine.BlockManager.PlayerPassing (this);
			//Console.WriteLine(bombNumberMax);
		}

        public void PlaceBomb ()
		{
			Bomb b = new Bomb (gridPosition);
			b.Player = this;
			if ((bombNumber < bombNumberMax) && engine.BlockManager.PlaceBomb (b)) {
				++bombNumber;
			}
        }

		public void RemoveBomb() 
		{
			--bombNumber;
		}

		public override void Kill ()
		{
			base.Kill ();

		}

		public void Killed(Player p) {
			if (p == this)
				--score;
			else
				++score;
		}
        #endregion Methods
    }
}
