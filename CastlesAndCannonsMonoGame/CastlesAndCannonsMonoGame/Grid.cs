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
    class Grid
    {
        public enum State
        {

        }

        public static int SIZE = 8; 
        private Panel[,] panels;
        private int score;
        private LinkedList<Cannonball> enemies;
        private Character c;
       

        public Grid()
        {
            Initialize();
            LoadContent();
        }

        private void Initialize()
        {
            panels = new Panel[SIZE, SIZE];
            score = 0;
            enemies = new LinkedList<Cannonball>();
            c = new Knight();
        }

        private void LoadContent()
        {
            for(int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    panels[row, col] = new Panel();
                }
            }
        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            c.Update(gameTime);

            foreach (Panel p in panels)
            {
                p.Update(gameTime);
            }

            foreach (Cannonball b in enemies)
            {
                b.Update(gameTime);
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Panel p in panels)
            {
                p.Draw(gameTime, spriteBatch);
            }

            foreach (Cannonball c in enemies)
            {
                c.Draw(gameTime, spriteBatch);
            }
        }
    }
}
