using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CastlesAndCannonsMonoGame
{
    class Cannonball
    {
        public static enum State
        {

        }

        private int armor;
        private int health;
        private float speed;
        private bool isAlive;
        private Vector2 position;

        public Cannonball()
        {
            Initialize();
            LoadContent();
        }

        private void Initialize()
        {
        }

        private void LoadContent()
        {

        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
