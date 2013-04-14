using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yna.Engine.Graphics;
using Yna.Engine;
using Microsoft.Xna.Framework;

namespace TestGame
{
    class Bomb : Block
    {
        #region Properties
		private Player player;
        private int idleTime = 5000;
        private int explodingTime = 1500;
        private YnTimer idleTimer;
        private YnTimer explodingTimer;
        private BombState state;
        private List<FireBlock> firePoints;
		private bool firePointsAdded;
        private ExplosionType explosionType = ExplosionType.ADJACENT;
        private BombType bombType = BombType.NORMAL;
        private int range;
        #endregion Properties

        #region Getters/Setters
        public BombState State
        {
            get { return state; }
            set { state = value; }
        }
        public int Range
        {
            get { return range; }
            set { range = value; }
        }
        public List<FireBlock> FirePoints
        {
            get { return firePoints; }
        }
		public bool FirePointsAdded {
			get { return firePointsAdded; }
			set { firePointsAdded = value; }
		}
		public Player Player {
			get { return player; }
			set { player = value; }
		}
		public ExplosionType ExplosionType 
		{
			get { return explosionType; }
		}
        #endregion Getters/Setters

        #region Constructors
        public Bomb(Point gridPosition) : base(gridPosition)
        {
            state = BombState.IDLE;
            firePoints = new List<FireBlock>();
			firePointsAdded = false;
            idleTimer = new YnTimer(idleTime);
            idleTimer.Completed += new EventHandler<EventArgs>(Exploding);
            explodingTimer = new YnTimer(explodingTime);
            explodingTimer.Completed += new EventHandler<EventArgs>(Exploded);
            explodingTimer.Stopped += new EventHandler<EventArgs>(Exploding);

            idleTimer.Start();

			//Color = Color.Black;
			//_texture = YnGraphics.CreateTexture(Color, Rectangle.Width, Rectangle.Height);

			IsPassable = false;
			CanCatchFire = true;
			CanPlaceBomb = false;
			CanFireSpread = true;
			IsDestroyable = true;
        }
        #endregion Constructors

        #region Methods
        public void Detonate()
        {
            explodingTimer.Stop();
        }

        public override void Update(GameTime gameTime)
        {
            if (state == BombState.IDLE)
                idleTimer.Update(gameTime);
            if (state == BombState.EXPLODING)
                explodingTimer.Update(gameTime);
        }

        public void Exploding<TEventArgs>(object sender, TEventArgs e)
        {
            // Set state to exploding.
            state = BombState.EXPLODING;
            // Destroy timer.
            idleTimer.Kill();
            // Start exploding timer.
            explodingTimer.Start();
        }

        public void Exploded<TEventArgs>(object sender, TEventArgs e)
        {
            state = BombState.EXPLODED;
            explodingTimer.Kill();
        }
        #endregion Methods
    }
}
