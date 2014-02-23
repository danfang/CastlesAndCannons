using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CastlesAndCannonsMonoGame
{
    static class Textures
    {
        public static Texture2D normalPanelTexture;
        public static Texture2D knightTexture;

        public static void LoadContent(Game game)
        {
            normalPanelTexture = game.Content.Load<Texture2D>("Tile");
            knightTexture = game.Content.Load<Texture2D>("Tile");
        }
    }
}
