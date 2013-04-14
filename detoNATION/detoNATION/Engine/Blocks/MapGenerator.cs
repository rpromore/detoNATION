using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TestGame
{
    class MapGenerator
    {
        List<Map> maps;
        Queue<SpawnPoint> spawns;
        public MapGenerator()
        {
            maps = new List<Map>();
            maps.Add(new BasicMap());
            spawns = new Queue<SpawnPoint>();
        }
        public List<Block> Generate(Vector2 startGridPosition, Vector2 endGridPosition, Vector2 blockSize)
        {
            Random r = new Random();
            int max = maps.Count;
            Map m = maps[r.Next(1, max)-1];
            List<Block> ret = m.Generate(startGridPosition, endGridPosition, blockSize);
            spawns.Clear();
            foreach( SpawnPoint s in m.SpawnPoints() ) {
                spawns.Enqueue(s);
            }
            return ret;
        }
        public Queue<SpawnPoint> SpawnPoints()
        {
            return spawns;
        }
    }
}
