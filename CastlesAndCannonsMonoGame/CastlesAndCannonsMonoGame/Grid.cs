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
        public static float elapsedGameTime;
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
            elapsedGameTime = 0;
            enemies.AddFirst(new Cannonball()); // testing cannonball
        }

        
        private void LoadContent()
        {
            for(int row = 0; row < GRID_SIZE; row++)
            {
                for (int col = 0; col < GRID_SIZE; col++)
                {
                    panels[row, col] = new Panel(GRID_HEIGHT_OFFSET + row * PANEL_SIZE, GRID_WIDTH_OFFSET + col * PANEL_SIZE, PANEL_SIZE);
                }
            }
           c = new Knight(panels[2, 2].getPosition(), PANEL_SIZE, 2, 2);
        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            mousePosition = Mouse.GetState().Position;
            c.Update(gameTime);
            elapsedGameTime += (float) gameTime.ElapsedGameTime.TotalSeconds;

            ((Knight) c).Update(gameTime);

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
                if (c.Column > 0)
                {
                    c.move(panels[c.Row, c.Column - 1].getPosition(), c.Row, c.Column - 1);
                }
            } 
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (c.Column < Math.Sqrt(panels.Length) - 1)
                {
                    c.move(panels[c.Row, c.Column + 1].getPosition(), c.Row, c.Column + 1);
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (c.Row > 0)
                {
                    c.move(panels[c.Row - 1, c.Column].getPosition(), c.Row - 1, c.Column);
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (c.Row < Math.Sqrt(panels.Length) - 1)
                {
                    c.move(panels[c.Row + 1, c.Column].getPosition(), c.Row + 1, c.Column);
                }
            }
            c.Update(gameTime);
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
