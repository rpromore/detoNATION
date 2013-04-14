using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;

namespace TestGame
{
	class PlayerManager
	{
		private Dictionary<long, Player> players;
		private Engine engine;

		public Dictionary<long, Player> Players
        {
            get { return players; }
            set { players = value; }
        }

		public PlayerManager (Engine e)
		{
			engine = e;
			players = new Dictionary<long, Player>();
		}

		public Player AddPlayer(long id, Player p)
        {
            SpawnPoint spawn = engine.BlockManager.RequestSpawnPoint();
            p.Position = new Vector2(spawn.Point.X*32, spawn.Point.Y*32);
            p.Engine = engine;
			p.ID = id;
            players.Add(id, p);
			return p;
        }

        public Player AddPlayer(Player p)
        {
            long id = 0;
            if( players.Keys.Count > 0 )
                id = players.Keys.Last() + 1;
            return AddPlayer(id, p);
        }

        public Player AddHumanPlayer(HumanPlayer p)
        {
            return AddPlayer(p);
        }

        public Player AddHumanPlayer(long id, HumanPlayer p)
        {
            return AddPlayer(id, p);
        }

        public Player AddHumanPlayer()
        {
            return AddPlayer(new HumanPlayer());
        }

        public Player AddAIPlayer(AiPlayer p)
        {
            return AddPlayer(p);
        }

        public Player AddAIPlayer(long id, AiPlayer p)
        {
            return AddPlayer(id, p);
        }

        public Player AddAIPlayer()
        {
            return AddPlayer(new AiPlayer());
        }

        public Player GetPlayer(long id)
        {
            Player p;
            players.TryGetValue(id, out p);
            return p;
        }

        public void RemovePlayer(long id)
        {
            players.Remove(id);
        }

		public void Update (GameTime gameTime)
		{
			foreach (Player p in players.Values) {
				// if this player is dead, respawn them
				if (!p.Active) {
					SpawnPoint spawn = engine.BlockManager.RequestSpawnPoint();
            		p.Position = new Vector2(spawn.Point.X*32, spawn.Point.Y*32);
					p.Revive ();
				}
			}
		}
	}
}

