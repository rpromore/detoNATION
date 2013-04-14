using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TestGame
{
    class BasicMap : Map
    {
        private Queue<SpawnPoint> spawns = new Queue<SpawnPoint>();
        public List<Block> Generate(Vector2 startGridPosition, Vector2 endGridPosition, Vector2 blockSize)
        {
            List<Block> blocks = new List<Block>();
            for (int i = (int)startGridPosition.X; i <= endGridPosition.X; i++)
            {
                for (int j = (int)startGridPosition.Y; j <= endGridPosition.Y; j++)
                {
                    if (i == startGridPosition.X && j == startGridPosition.Y
                        || i == endGridPosition.X && j == startGridPosition.Y
                        || i == endGridPosition.X && j == endGridPosition.Y
                        || i == startGridPosition.X && j == endGridPosition.Y)
                    {
                        blocks.Add(new SpawnBlock(new Point(i, j)));
                        spawns.Enqueue(new SpawnPoint(new Point(i, j)));
                    }
                    else if (i == startGridPosition.X + 1 && j == startGridPosition.Y
                        || i == startGridPosition.X && j == startGridPosition.Y + 1
                        || i == endGridPosition.X - 1 && j == startGridPosition.Y
                        || i == endGridPosition.X && j == startGridPosition.Y + 1
                        || i == endGridPosition.X - 1 && j == endGridPosition.Y
                        || i == endGridPosition.X && j == endGridPosition.Y - 1
                        || i == startGridPosition.X + 1 && j == endGridPosition.Y
                        || i == startGridPosition.X && j == endGridPosition.Y - 1)
                        blocks.Add(new AirBlock(new Point(i, j)));
                    else
                        blocks.Add(new BrickBlock(new Point(i, j)));
                }
            }
            return blocks;
        }
        public Queue<SpawnPoint> SpawnPoints()
        {
            return spawns;
        }
    }
}
