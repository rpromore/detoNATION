using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Yna.Engine;
using Yna.Engine.Graphics;
using Yna.Engine.Graphics.Event;

namespace TestGame
{
    class BlockManager
    {
        #region Properties

		private Vector2 mapSize;
        private Engine engine;
		private UpgradeManager upgradeManager;
        private Dictionary<Point, Bomb> bombs;
        private Dictionary<Point, YnGroup> blocks;
        private int count = 0;
        // The default size of each block.
        private Vector2 blockSize;
        private MapGenerator mapGenerator;
        private Queue<SpawnPoint> spawnPoints;

        #endregion Properties

        #region Getters/Setters

        public Dictionary<Point, Bomb> Bombs
        {
            get { return bombs; }
            set { bombs = value; }
        }
        public Dictionary<Point, YnGroup> Blocks
        {
            get { return blocks; }
            set { blocks = value; }
        }
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        public Vector2 BlockSize
        {
            get { return blockSize; }
            set { blockSize = value; }
        }
        public Queue<SpawnPoint> SpawnPoints
        {
            get { return spawnPoints; }
            set { spawnPoints = value; }
        }

        #endregion Getters/Setters

        #region Constructors

        public BlockManager(Engine e, Vector2 blockSize)
        {
            engine = e;
			upgradeManager = new UpgradeManager();
            bombs = new Dictionary<Point, Bomb>();
            blocks = new Dictionary<Point, YnGroup>();
            this.blockSize = blockSize;
			this.mapSize = new Vector2(16, 16);
            mapGenerator = new MapGenerator();

            List<Block> generatedBlocks = mapGenerator.Generate(new Vector2(0, 0), mapSize, blockSize);
            spawnPoints = mapGenerator.SpawnPoints();

			// add a layer of air
			for (int i = 0; i < 16; i++) { 
				for (int j = 0; j < 16; j++) {
					AddBlock (new AirBlock(new Point(i, j)));
				}
			}

            foreach (Block b in generatedBlocks)
            {
                AddBlock(b);
            }
        }

        public BlockManager(Engine e)
            : this(e, new Vector2(32, 32))
        {

        }

        #endregion Constructors

        #region Methods

        public void AddBlock(Block b)
        {
            YnGroup list;
            if( !blocks.TryGetValue(b.GridPosition, out list)) {
                list = new YnGroup();
                blocks.Add(b.GridPosition, list);
            }
			b.MouseClicked += new EventHandler<MouseClickEntityEventArgs>(BlockClicked);
			b.Killed += new EventHandler<EventArgs>(RemoveBlock);
            list.Add(b);
            ++count;
        }

		public void BlockClicked<TEventArgs>(object sender, TEventArgs e)
        {
			Block b = (Block) sender;
			Console.WriteLine (b);
        }

		public Block BlockAt(Point p)
        {
            // return the last block added, which will be the "top"
            YnGroup b;
            if (blocks.TryGetValue(p, out b))
                return (Block)b.Members[b.Members.Count-1];
            else
                return new NullBlock(p);
        }

		public void RemoveBlock<TEventArgs>(object sender, TEventArgs e)
		{
			Block b = (Block) sender;
			RemoveBlock (b);
		}

		public void RemoveBlock (Block b)
		{
			blocks[b.GridPosition].Remove(b);
			b.Die ();
		}

		/*
        public void RemoveBlockAt(Point gridPosition)
        {
            Block b = BlockAt(gridPosition);
			Console.WriteLine (b);
			b.Kill ();
			b.Die ();
			blocks[gridPosition].Remove (b);
        }
        */

        public SpawnPoint RequestSpawnPoint()
        {
            SpawnPoint p = spawnPoints.Dequeue();
            p.Time = DateTime.Now;
            spawnPoints.Enqueue(p);
            return p;
        }

        public bool PlaceBomb(Bomb b)
        {
			Block block = BlockAt (b.GridPosition);
            if (!block.CanPlaceBomb)
            {
                return false;
            }
            else
            {
                bombs.Add(b.GridPosition, b);
                AddBlock(b);
                return true;
            }
        }

		public void GenerateFirePoints(Bomb b)
        {
			int range = b.Player.BombRange;
            // Add this grid position to list of fire.
            b.FirePoints.Add(new FireBlock(b, b.GridPosition));

            // Add adjacent grid positions to list of fire.
            Point p;
            Block block;

			// TODO: explosion types, for now, just adjacent
			// left side
			for (int i = 1; i <= range; i++)
			{
				p = new Point(b.GridPosition.X-i, b.GridPosition.Y);
				block = BlockAt (p);
				if (!block.CanCatchFire)
				{
					break;
				}
				else
				{
					if (block.GetType () == typeof(Bomb)) {
						Bomb thisbomb = (Bomb)block;
						thisbomb.Detonate ();
					}
					FireBlock f = new FireBlock(b, p);
					f.Kill ();
					b.FirePoints.Add (f);
					if (!block.CanFireSpread)
						break;
				}
			}
			// top side
			for (int i = 1; i <= range; i++)
			{
				p = new Point(b.GridPosition.X, b.GridPosition.Y-i);
				block = BlockAt (p);
				if (!block.CanCatchFire)
				{
					break;
				}
				else
				{
					if (block.GetType () == typeof(Bomb)) {
						Bomb thisbomb = (Bomb)block;
						thisbomb.Detonate ();
					}
					FireBlock f = new FireBlock(b, p);
					f.Kill ();
					b.FirePoints.Add (f);
					if (!block.CanFireSpread)
						break;
				}
			}
			// right side
			for (int i = 1; i <= range; i++)
			{
				p = new Point(b.GridPosition.X+i, b.GridPosition.Y);
				block = BlockAt (p);
				if (!block.CanCatchFire)
				{
					break;
				}
				else
				{
					if (block.GetType () == typeof(Bomb)) {
						Bomb thisbomb = (Bomb)block;
						thisbomb.Detonate ();
					}
					FireBlock f = new FireBlock(b, p);
					f.Kill ();
					b.FirePoints.Add (f);
					if (!block.CanFireSpread)
						break;
				}
			}
			// bottom side
			for (int i = 1; i <= range; i++)
			{
				p = new Point(b.GridPosition.X, b.GridPosition.Y+i);
				block = BlockAt (p);
				if (!block.CanCatchFire)
				{
					break;
				}
				else
				{
					if (block.GetType () == typeof(Bomb)) {
						Bomb thisbomb = (Bomb)block;
						thisbomb.Detonate ();
					}
					FireBlock f = new FireBlock(b, p);
					f.Kill ();
					b.FirePoints.Add (f);
					if (!block.CanFireSpread)
						break;
				}
			}

			/*
            for (int i = -range; i <= range; i++)
            {
                for (int j = -range; j <= range; j++)
                {
                    p = new Point(b.GridPosition.X + i, b.GridPosition.Y + j);
                    // If explosion type is adjacent and the block being examined isn't adjacent, skip it.
                    if (b.ExplosionType == ExplosionType.ADJACENT && !(
                        p.X == b.GridPosition.X && p.Y != b.GridPosition.Y ||
                        p.X != b.GridPosition.X && p.Y == b.GridPosition.Y)
                    )
                        continue;

					/*if (bombs[p] != null)
						bombs[p].Detonate ();*

                    block = BlockAt(p);
                    if (!block.CanCatchFire())
                    {
                        continue;
                    }
                    else
                    {
						FireBlock f = new FireBlock(p);
						f.Kill ();
						b.FirePoints.Add (f);
                    }
                }
            }
            */
        }

        public Bomb BombAt(Point p)
        {
            Bomb b;
            if (!bombs.TryGetValue(p, out b))
                return null;
            return b;
        }

		public void RemoveBomb (Bomb b)
		{
			//b.Player.RemoveBomb ();
			bombs.Remove (b.GridPosition);
			RemoveBlock (b);
		}

		public void PlayerPassing (Player player)
		{
			Block block;
			List<Block> examined = new List<Block> ();
			// check each corner of the player
			// top left
			block = BlockAt (new Point ((int)Math.Floor (player.X / blockSize.X), (int)Math.Floor (player.Y / blockSize.Y)));
			examined.Add (block);
			block.OnPass (player);

			// top right
			block = BlockAt (new Point ((int)Math.Floor ((player.X + player.Width-1) / blockSize.X), (int)Math.Floor (player.Y / blockSize.Y)));
			if (!examined.Contains (block)) {
				examined.Add (block);
				block.OnPass (player);
			}
			// bottom right
			block = BlockAt (new Point((int)Math.Floor ((player.X+player.Width-1)/blockSize.X), (int)Math.Floor ((player.Y+player.Height-1)/blockSize.Y)));
			if (!examined.Contains (block)) {
				examined.Add (block);
				block.OnPass (player);
			}
			// bottom left
			block = BlockAt (new Point((int)Math.Floor (player.X/blockSize.X), (int)Math.Floor ((player.Y+player.Height-1)/blockSize.Y)));
			if (!examined.Contains (block)) {
				examined.Add (block);
				block.OnPass (player);
			}
		}

		public Vector2 AdjustPlayer (Player p)
		{
			Vector2 velocity = p.Velocity * p.Acceleration;
			Vector2 newPosition = p.Position + velocity;
			if (newPosition.X < 0 || newPosition.X > blockSize.X * 32 || newPosition.Y < 0 || newPosition.Y > blockSize.Y * 32)
				return Vector2.Zero;

			Point examine = p.GridPosition;
			Point examinenext = p.GridPosition;
			Block block;
			Block blocknext;

			Vector2 offset = Vector2.Divide (blockSize, 2.5f);

			if (velocity.X > 0) { // examine right block
				++examine.X;
				++examinenext.X;
				block = BlockAt (examine);

				if (p.Y < examine.Y * blockSize.Y && p.Y + p.Height < examine.Y * blockSize.Y + blockSize.Y) {
					examinenext.Y--;
				} else if (p.Y > examine.Y * blockSize.Y && p.Y + p.Height > examine.Y * blockSize.Y + blockSize.Y) {
					examinenext.Y++;
				}

				blocknext = BlockAt (examinenext);

				if (block.IsPassable && blocknext.IsPassable) {
					//if (newPosition.X+p.Width > block.X)
					//	velocity.X = (newPosition.X+p.Width-block.X);
					return velocity;
				}
				if (!block.IsPassable && !blocknext.IsPassable && (newPosition.X+p.Width) > block.X)
					return Vector2.Zero;
				// otherwise if at least one of the blocks is passable
				else if (block.IsPassable && !blocknext.IsPassable || !block.IsPassable && blocknext.IsPassable) {
					if ((newPosition.X+p.Width) < block.X)
						return velocity;

					// determine which block we will move around
					Block e = !block.IsPassable ? block : blocknext;
					// if it's the above block we need to compare the player's y position to the block's bottom y position
					if (e.Y < p.Y && p.Y > e.Y+offset.Y && p.Y+p.Height > (e.Y+blockSize.Y-offset.Y)) 
						return new Vector2(0, velocity.X);
					if (e.Y > p.Y && p.Y < e.Y-offset.Y && p.Y+p.Height < (e.Y+blockSize.Y-offset.Y))
						return new Vector2(0, -velocity.X);
					return Vector2.Zero;
				}

			} else if (velocity.X < 0) { // examine left block
				--examine.X;
				--examinenext.X;
				block = BlockAt (examine);

				if (p.Y < examine.Y * blockSize.Y && p.Y + p.Height < examine.Y * blockSize.Y + blockSize.Y) {
					examinenext.Y--;
				} else if (p.Y > examine.Y * blockSize.Y && p.Y + p.Height > examine.Y * blockSize.Y + blockSize.Y) {
					examinenext.Y++;
				}

				blocknext = BlockAt (examinenext);

				if (block.IsPassable && blocknext.IsPassable)
					return velocity;
				if (!block.IsPassable && !blocknext.IsPassable && newPosition.X < (block.X+blockSize.X))
					return Vector2.Zero;
				// otherwise if at least one of the blocks is passable
				else if (block.IsPassable && !blocknext.IsPassable || !block.IsPassable && blocknext.IsPassable) {
					if (newPosition.X > (block.X+blockSize.X))
						return velocity;

					// determine which block we will move around
					Block e = !block.IsPassable ? block : blocknext;
					// if it's the above block we need to compare the player's y position to the block's bottom y position
					if (e.Y < p.Y && p.Y > e.Y+offset.Y && p.Y+p.Height > (e.Y+blockSize.Y-offset.Y)) 
						return new Vector2(0, -velocity.X);
					if (e.Y > p.Y && p.Y < e.Y-offset.Y && p.Y+p.Height < (e.Y+blockSize.Y-offset.Y))
						return new Vector2(0, velocity.X);
					return Vector2.Zero;
				}

			} else if (velocity.Y > 0) { // examine bottom block
				++examine.Y;
				++examinenext.Y;
				block = BlockAt (examine);

				if (p.X < examine.X * blockSize.X && (p.X + p.Width) < examine.X * blockSize.X + blockSize.X) {
					examinenext.X--;
				} else if (p.X > examine.X * blockSize.X && (p.X + p.Width) > examine.X * blockSize.X + blockSize.X) {
					examinenext.X++;
				}

				blocknext = BlockAt (examinenext);

				if (block.IsPassable && blocknext.IsPassable)
					return velocity;

				if (!block.IsPassable && !blocknext.IsPassable && (newPosition.Y+p.Height) > block.Y)
					return Vector2.Zero;
				// otherwise if at least one of the blocks is passable
				else if ((block.IsPassable && !blocknext.IsPassable) || (!block.IsPassable && blocknext.IsPassable)) {
					if ((newPosition.Y+p.Height) < block.Y)
						return velocity;

					// determine which block we will move around
					Block e = !block.IsPassable ? block : blocknext;

					if (e.X < p.X && p.X > e.X+offset.X && p.X+p.Width > (e.X+blockSize.X-offset.X)) 
						return new Vector2(velocity.Y, 0);
					if (e.X > p.X && p.X < e.X-offset.X && p.X+p.Width < (e.X+blockSize.X-offset.X)) {
						velocity = new Vector2(-velocity.Y, 0);
						return velocity;
					}
					return Vector2.Zero;
				}

			} else if (velocity.Y < 0) { // examine top block
				--examine.Y;
				--examinenext.Y;
				block = BlockAt (examine);

				if (p.X < examine.X * blockSize.X && p.X + p.Width < examine.X * blockSize.X + blockSize.X) {
					examinenext.X--;
				} else if (p.X > examine.X * blockSize.X && p.X + p.Width > examine.X * blockSize.X + blockSize.X) {
					examinenext.X++;
				}

				blocknext = BlockAt (examinenext);

				if (block.IsPassable && blocknext.IsPassable)
					return velocity;
				if (!block.IsPassable && !blocknext.IsPassable && newPosition.Y < (block.Y+blockSize.Y))
					return Vector2.Zero;
				// otherwise if at least one of the blocks is passable
				else if (block.IsPassable && !blocknext.IsPassable || !block.IsPassable && blocknext.IsPassable) {
					if (newPosition.Y > (block.Y+blockSize.Y))
						return velocity;

					// determine which block we will move around
					Block e = !block.IsPassable ? block : blocknext;
					if (e.X < p.X && p.X > e.X+offset.X && p.X+p.Width > (e.X+blockSize.X-offset.X)) 
						return new Vector2(-velocity.Y, 0);
					if (e.X > p.X && p.X < e.X-offset.X && p.X+p.Width < (e.X+blockSize.X-offset.X))
						return new Vector2(velocity.Y, 0);
					return Vector2.Zero;
				}
			}

			return velocity;
		}

		public void Update (GameTime gameTime)
		{
			// check bombs
            for (int i = 0; i < bombs.Count; i++)
            {
                Bomb b = (Bomb)bombs.ElementAt(i).Value;
                b.Update(gameTime);
                if (b.State == BombState.EXPLODED)
                {
					YnGroup list;
					for(int j = 0; j < b.FirePoints.Count; j++)
			        {
						FireBlock p = b.FirePoints[j];
						// Remove any blocks that will be destroyed by fire.
						if (blocks.TryGetValue (p.GridPosition, out list)) {
							for (int k = 0; k < list.Count; k++) {
								Block block = (Block)list[k];
								if(block.IsDestroyable) {
									RemoveBlock (block);

									if (block.ProvidesUpgrade) { // if this block can provide an upgrade upon its destruction
										Block u = upgradeManager.SpawnUpgrade (p.GridPosition);
										if (u != null) {
	 										AddBlock (u);
											Console.WriteLine (u);
										}
									}
								}
							}
						}

						RemoveBlock (p);

					}
					RemoveBomb (b);
                }
                else if (b.State == BombState.EXPLODING && !b.FirePointsAdded)
                {
					// tell the player they can place another bomb
					b.Player.RemoveBomb ();
                    // get list of fire points and update blocks
					GenerateFirePoints(b);
					for (int j = 0; j < b.FirePoints.Count; j++)
                    {
						FireBlock p = b.FirePoints[j];
						p.Revive ();
						blocks[p.GridPosition].Add (p);
                    }
					b.FirePointsAdded = true;
                }
            }
		}

        #endregion Methods
    }
}
