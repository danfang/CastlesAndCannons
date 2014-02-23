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
        public Knight(Vector2 pos, int newSize, int row, int col)
        {
            health = 100;
            mana = 100;
            armor = 2;
            speed = 2.5f;
            position = pos;
            size = newSize;
            this.row = row;
            this.column = col;
            bounds = new Rectangle((int) position.X, (int) position.Y, size, size);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.knightTexture, bounds, Color.White);
        }

    }
}
