using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CastlesAndCannonsMonoGame
{
    class Grid
    {
        public static enum State
        {

        }

        private Panel[,] panels;
        private int score;
        private LinkedList<Cannonball> enemies = new LinkedList<Cannonball>();

        public Grid()
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
