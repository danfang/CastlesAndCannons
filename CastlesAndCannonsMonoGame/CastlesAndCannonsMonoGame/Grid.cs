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

        public static int GRID_SIZE = 8;
        public static int PANEL_SIZE;
        public static int GRID_WIDTH_OFFSET;
        public static int GRID_HEIGHT_OFFSET;
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
            c = new Character();
            PANEL_SIZE = (Game1.height - 100) / GRID_SIZE;
            c = new Knight();
            GRID_WIDTH_OFFSET = (Game1.width - (PANEL_SIZE * GRID_SIZE)) / 2;
            GRID_HEIGHT_OFFSET = (Game1.height - (PANEL_SIZE * GRID_SIZE)) / 2;
        }

        private void LoadContent()
        {
            for(int row = 0; row < GRID_SIZE; row++)
            {
                for (int col = 0; col < GRID_SIZE; col++)
                {
                    panels[row, col] = new Panel(GRID_WIDTH_OFFSET + row * PANEL_SIZE, GRID_HEIGHT_OFFSET + col * PANEL_SIZE, PANEL_SIZE);
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
