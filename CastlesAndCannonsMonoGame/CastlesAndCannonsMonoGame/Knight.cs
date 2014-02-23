using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics;

namespace CastlesAndCannonsMonoGame
{
    class Knight : Character
    {
        public Knight(Vector2 pos)
        {
            health = 100;
            mana = 100;
            armor = 2;
            speed = 2.5f;
            position = pos;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.knightTexture, Vector2.Zero, Color.Black);
        }

    }
}
