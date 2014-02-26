using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CastlesAndCannonsMonoGame
{
    public static class Textures
    {
        public static Texture2D normalPanelTexture;
        public static Texture2D knightTexture;
        public static Texture2D cannonTexture;
        public static Texture2D healthTexture;
        public static Texture2D backgroundTexture;

        public static void LoadContent(Game game)
        {
            normalPanelTexture = game.Content.Load<Texture2D>("Tile");
            knightTexture = game.Content.Load<Texture2D>("KnightSprite");
            cannonTexture = game.Content.Load<Texture2D>("CannonBall");
            healthTexture = game.Content.Load<Texture2D>("Health");
            backgroundTexture = game.Content.Load<Texture2D>("HealthBackground");
        }
    }
}
