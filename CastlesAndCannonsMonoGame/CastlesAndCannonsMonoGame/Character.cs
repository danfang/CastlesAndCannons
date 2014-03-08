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
    abstract class Character
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
        protected bool canPressKey;
        protected int direction;

        public Character(Vector2 pos, int newSize, int row, int col)
        {
            health = 100;
            mana = 100;
            armor = 2;
            speed = 10f; // The unit for speed is pixels per update.
            position = pos;
            desiredPos = pos;
            size = newSize;
            this.row = row;
            this.column = col;
            bounds = new Rectangle((int)position.X, (int)position.Y, size, size);
            isMoving = false;
            canPressKey = true;
        }

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
            Keys[] pressed = Keyboard.GetState().GetPressedKeys();
            if (pressed.Length == 0 && !canPressKey)
            {
                canPressKey = true;
            }
            int tempRow = row;
            int tempCol = column;
            if (pressed.Length > 0 && !isMoving && canPressKey)
            {
                if (pressed.Contains(Keys.A))
                {
                    direction = 3;
                    if (column > 0)
                        tempCol = column - 1;
                }
                else if (pressed.Contains(Keys.D))
                {
                    direction = 1;
                    if (column < Math.Sqrt(panels.Length) - 1)
                        tempCol = column + 1;
                }
                else if (pressed.Contains(Keys.W))
                {
                    direction = 0;
                    if (row > 0)
                        tempRow = row - 1;
                }
                else if (pressed.Contains(Keys.S))
                {
                    direction = 2;
                    if (row < Math.Sqrt(panels.Length) - 1)
                        tempRow = row + 1;
                }
                desiredPos = panels[tempRow, tempCol].GetPosition();
                column = tempCol;
                row = tempRow;
                canPressKey = false;
                isMoving = true;
            }
            Move(panels[tempRow, tempCol].GetPosition(), tempRow, tempCol);
        }



        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

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
                //if (!newPos.Equals(position))
                //{
                //    row = newRow;
                //    column = newCol;
                //    desiredPos = newPos;
                //    //isMoving = true;
                //}
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
