using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TestGame
{
    interface Map
    {
        List<Block> Generate(Vector2 startGridPosition, Vector2 endGridPosition, Vector2 blockSize);
        Queue<SpawnPoint> SpawnPoints();
    }
}
