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
    class Cannonball
    {
        public enum State
        {

        }

        public static float speed = 60;
        private int armor;
        private int health;
        private bool isAlive;
        private Vector2 pos;
        private Rectangle bounds; // make circle at some point
        
        public Cannonball()
        {
            Initialize();
            LoadContent();
        }

        private void Initialize() 
        {
            armor = 30;
            health = 100;
            isAlive = true;
            pos = new Vector2(Grid.GRID_WIDTH_OFFSET - Grid.PANEL_SIZE * 2, Grid.GRID_HEIGHT_OFFSET);
            bounds = new Rectangle(Grid.GRID_WIDTH_OFFSET - Grid.PANEL_SIZE * 2, Grid.GRID_HEIGHT_OFFSET, Grid.PANEL_SIZE, Grid.PANEL_SIZE);
        }

        private void LoadContent()
        {

        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            int seconds = (int) Grid.elapsedGameTime;
            if (seconds != 0)
            {
                pos.X += Grid.PANEL_SIZE * Grid.GRID_SIZE / speed; 
                bounds.X = (int) pos.X;
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {   
            spriteBatch.Draw(Textures.cannonTexture, bounds, Color.White);
        }
    }
}
