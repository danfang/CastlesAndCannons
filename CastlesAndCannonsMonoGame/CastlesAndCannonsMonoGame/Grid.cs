﻿using System;
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
