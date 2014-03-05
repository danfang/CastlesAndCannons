using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics;

namespace CastlesAndCannonsMonoGame
{
    class Character
    {
        protected int health;
        protected int mana;
        protected int armor;
        protected float speed;
        protected Vector2 position;
        protected Vector2 desiredPos;
        protected int row;
        protected int column;
        protected Rectangle bounds;
        protected int size;
        protected bool isMoving;
        protected int direction;

        public void UnloadContent()
        {
            
        }

        // Updates the Character, namely moving the Character. gameTime represents the
        // current game time where as the Panel[, ], grid, represents the grid of Panels
        // that is used to delegate the movements.
        public void Update(GameTime gameTime, Panel[, ] grid)
        {
            MoveCharacter(grid);
        }

        // Moves the Character based on the WASD keyboard movement
        // (W for up, A for left, S for down, and D for right)
        private void MoveCharacter(Panel[, ] panels)
        {
            int tempRow = row;
            int tempCol = column;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                direction = 3;
                if (column > 0)
                    tempCol--;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                direction = 1;
                if (column < Math.Sqrt(panels.Length))
                    tempCol++;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                direction = 0;
                if (column >= 0)
                    tempRow--;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                direction = 2;
                if (column < Math.Sqrt(panels.Length))
                    tempRow++;
            }
            // TODO: temp fix... checking bounds
            if (tempRow < 0)
            {
                tempRow++;  
            }
            if (tempRow > Math.Sqrt(panels.Length) - 1)
            {
                tempRow--;
            }
            if (tempCol > Math.Sqrt(panels.Length) - 1)
            {
                tempCol--;
            }
            if (tempCol < 0)
            {
                tempCol++;
            }
            Move(panels[tempRow, tempCol].GetPosition(), tempRow, tempCol);
        }



        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }

        // Moves the character depending on what direction the user presses
        // (WASD). Vector2, newPos, represents the new position that the user
        // user has after moving, int newRow represents the new row after the
        // user has moved. Int newCol represents the new column after the user
        // has moved.
        public void Move(Vector2 newPos, int newRow, int newCol)
        {
            if (isMoving)
            {
                if (position.X - desiredPos.X > speed)
                {
                    position.X -= speed;
                    bounds.X = (int)position.X;
                }
                else if (position.X - desiredPos.X < -speed)
                {
                    position.X += speed;
                    bounds.X = (int)position.X;
                }
                else if (position.Y - desiredPos.Y > speed)
                {
                    position.Y -= speed;
                    bounds.Y = (int)position.Y;
                }
                else if (position.Y - desiredPos.Y < -speed)
                {
                    position.Y += speed;
                    bounds.Y = (int)position.Y;
                }
                else
                {
                    isMoving = false;
                    position = desiredPos;
                    bounds.X = (int)position.X;
                    bounds.Y = (int)position.Y;
                }
            }
            else
            {
                if (!newPos.Equals(position))
                {
                    row = newRow;
                    column = newCol;
                    desiredPos = newPos;
                    isMoving = true;
                }
            }
        }

        /*******************
         * GET/SET METHODS *
         *******************/

        public bool IsMoving
        {
            get
            {
                return isMoving;
            }
            set
            {
                isMoving = value;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }

        }

        public int Mana
        {
            get
            {
                return mana;
            }
            set
            {
                mana = value;
            }

        }

        public int Armor
        {
            get
            {
                return armor;
            }
            set
            {
                armor = value;
            }

        }

        public float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }

        public int Row
        {
            get
            {
                return row;
            }
            set
            {
                row = value;
            }
        }

        public int Column
        {
            get
            {
                return column;
            }
            set
            {
                column = value;
            }
        }

        // Returns the position at which the Character is in.
        public Vector2 GetVector()
        {
            return position;
        }

        // Sets the position vector of the Character.
        public void SetVector(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }

        public Rectangle Bounds()
        {
            return bounds;
        }


    }
}
