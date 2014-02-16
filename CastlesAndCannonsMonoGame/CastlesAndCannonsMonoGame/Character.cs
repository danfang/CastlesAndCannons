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
    abstract class Character
    {
        private int health;
        private int mana;
        private int armor;
        private float speed;
        private Vector2 position;
        private int row;
        private int column;

        abstract public void UnloadContent();
        abstract public void Update(GameTime gameTime);
        abstract public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
