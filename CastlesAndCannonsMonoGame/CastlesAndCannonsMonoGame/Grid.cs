using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics;

namespace CastlesAndCannonsMonoGame
{
    class Grid
    {
        public enum State
        {

        }

        public static int GRID_SIZE = 8;
        public static int PANEL_SIZE;
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
            panels = new Panel[GRID_SIZE, GRID_SIZE];
            score = 0;
            enemies = new LinkedList<Cannonball>();
            //c = new Character();
            PANEL_SIZE = (Game1.height - 100) / GRID_SIZE;
            c = new Knight(panels[2, 2].getPosition());
        }

        private void LoadContent()
        {
            for(int row = 0; row < GRID_SIZE; row++)
            {
                for (int col = 0; col < GRID_SIZE; col++)
                {
                    panels[row, col] = new Panel(row * PANEL_SIZE, col * PANEL_SIZE, PANEL_SIZE - 1);
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

            foreach (Cannonball cannonball in enemies)
            {
                cannonball.Update(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {

            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Panel p in panels)
            {
                p.Draw(gameTime, spriteBatch);
            }

            foreach (Cannonball cannonball in enemies)
            {
                cannonball.Draw(gameTime, spriteBatch);
            }
        }

        public Character GetCharacter()
        {
            return c;
        }
    }
}
