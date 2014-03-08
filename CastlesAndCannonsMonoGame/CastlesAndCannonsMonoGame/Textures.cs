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
        public static Texture2D[] knightTextures = new Texture2D[4];
        public static Texture2D cannonTexture;
        public static Texture2D healthTexture;
        public static Texture2D manaTexture;
        public static Texture2D backgroundTexture;
        public static Texture2D gameOver;

        public static void LoadContent(Game game)
        {
            normalPanelTexture = game.Content.Load<Texture2D>("Tile");
            knightTextures[0] = game.Content.Load<Texture2D>("KnightSpriteUp");
            knightTextures[1] = game.Content.Load<Texture2D>("KnightSpriteRight");
            knightTextures[2] = game.Content.Load<Texture2D>("KnightSpriteDown");
            knightTextures[3] = game.Content.Load<Texture2D>("KnightSpriteLeft");
            cannonTexture = game.Content.Load<Texture2D>("CannonBall");
            healthTexture = game.Content.Load<Texture2D>("Health");
            manaTexture = game.Content.Load<Texture2D>("Mana");
            backgroundTexture = game.Content.Load<Texture2D>("HealthBackground");
            gameOver = game.Content.Load<Texture2D>("gameovertemp");
        }
    }
}
