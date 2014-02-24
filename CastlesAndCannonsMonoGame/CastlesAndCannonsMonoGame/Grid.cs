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
        public static int GRID_WIDTH_OFFSET;
        public static int GRID_HEIGHT_OFFSET;
        private Panel[,] panels;
        private int score;
        private LinkedList<Cannonball> enemies;
        private Character c;
        private Point mousePosition;
        private Point mouseClick;
       

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
            PANEL_SIZE = (Game1.height - 100) / GRID_SIZE;
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
           c = new Knight(panels[2, 2].getPosition(), PANEL_SIZE);
        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            mousePosition = Mouse.GetState().Position;
            c.Update(gameTime);

            foreach (Panel p in panels)
            {
                p.Update(gameTime, mousePosition);
                p.Slashed(false);
            }
            

            foreach (Cannonball cannonball in enemies)
            {
                cannonball.Update(gameTime);
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                ((Knight)c).Slash(Mouse.GetState().Position);
                switch (((Knight)c).SlashDirection)
                {
                    case 1: panels[c.Row - 1, c.Column].Slashed(true);
                        break;
                    case 2: panels[c.Row, c.Column + 1].Slashed(true);
                        break;
                    case 3: panels[c.Row + 1, c.Column].Slashed(true);
                        break;
                    case 4: panels[c.Row, c.Column - 1].Slashed(true);
                        break;
                }
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
            ((Knight)c).Draw(gameTime, spriteBatch);
        }

        public Character GetCharacter()
        {
            return c;
        }
    }
}
