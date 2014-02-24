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
           c = new Knight(panels[2, 2].GetPosition(), PANEL_SIZE, 2, 2);
        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            mousePosition.X = Mouse.GetState().X;
            mousePosition.Y = Mouse.GetState().Y;

            elapsedGameTime += (float) gameTime.ElapsedGameTime.TotalSeconds;

            foreach (Panel p in panels)
            {
                p.Update(gameTime, mousePosition);
                p.Slashed(false);
            }

            foreach (Cannonball cannonball in enemies)
            {
                cannonball.Update(gameTime);
            }

            MoveCharacter();
            Slash();
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

        private void MoveCharacter()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (c.Column > 0)
                {
                    c.Move(panels[c.Row, c.Column - 1].GetPosition(), c.Row, c.Column - 1);
                }
            } 
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (c.Column < Math.Sqrt(panels.Length) - 1)
                {
                    c.Move(panels[c.Row, c.Column + 1].GetPosition(), c.Row, c.Column + 1);
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (c.Row > 0)
                {
                    c.Move(panels[c.Row - 1, c.Column].GetPosition(), c.Row - 1, c.Column);
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (c.Row < Math.Sqrt(panels.Length) - 1)
                {
                    c.Move(panels[c.Row + 1, c.Column].GetPosition(), c.Row + 1, c.Column);
                }
            }
        }

        private void Slash()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                mouseClick.X = Mouse.GetState().X;
                mouseClick.Y = Mouse.GetState().Y;
                ((Knight) c).Slash(mouseClick);
                if(CheckSlashDirection(((Knight)c).SlashDirection)) 
        {
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
            }

        }

        private bool CheckSlashDirection(int slashDirection)
        {
            switch (slashDirection)
        {
                case 1:
                    if (c.Row - 1 < 0)
                        return false;
                    break;
                case 2:
                    if (c.Column + 1 > GRID_SIZE - 1)
                        return false;
                    break;
                case 3:
                    if (c.Row + 1 > GRID_SIZE - 1)
                        return false;
                    break;
                case 4:
                    if (c.Column - 1 < 0)
                        return false;
                    break;
            }
            return true;
        }
    }
}
