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
        protected Rectangle movingBounds;
        protected int size;
        protected bool isMoving;
        protected bool canPressKey;
        protected int direction;
        protected Keys nextCommand;
        protected Keys prevCommand;

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
            movingBounds = new Rectangle((int)position.X + size/4, (int)position.Y + size/4, size / 2, size/ 2);
            isMoving = false;
            canPressKey = true;
            nextCommand = Keys.None;
            prevCommand = Keys.None;
        }

        public void UnloadContent()
        {
            
        }

        // All characters must override this update method (movement, behavior)
        public abstract void Update(GameTime gameTime, Panel[,] grid);

        // All characters must override this draw method (spritebatch drawing)
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        // Moves the Character based on the WASD keyboard movement
        // (W for up, A for left, S for down, and D for right)
        public void MoveCharacter(Panel[, ] panels)
        {
            Keys[] pressed = Keyboard.GetState().GetPressedKeys();
            int tempRow = row;
            int tempCol = column;
            if (pressed.Length == 0)
            {
                prevCommand = Keys.None;
            }
            else if (pressed.Length == 1) {
                if (!pressed[0].Equals(prevCommand))
                {
                    //prevCommand = pressed[0];
                    nextCommand = pressed[0];
                }
            }
            else if (pressed.Length == 2 && isMoving)
            {
                if (pressed[0].Equals(prevCommand))
                {
                    //prevCommand = Keys.None;
                    nextCommand = pressed[1];
                }
                else if (pressed[1].Equals(prevCommand))
                {
                    //prevCommand = Keys.None;
                    nextCommand = pressed[0];
                }
            }
            if (!nextCommand.Equals(Keys.None) && !isMoving && pressed.Length <= 1)
            {
                if (nextCommand.Equals(Keys.A))
                {
                    direction = 3;
                    if (column > 0)
                        tempCol = column - 1;
                }
                else if (nextCommand.Equals(Keys.D))
                {
                    direction = 1;
                    if (column < Math.Sqrt(panels.Length) - 1)
                        tempCol = column + 1;
                }
                else if (nextCommand.Equals(Keys.W))
                {
                    direction = 0;
                    if (row > 0)
                        tempRow = row - 1;
                }
                else if (nextCommand.Equals(Keys.S))
                {
                    direction = 2;
                    if (row < Math.Sqrt(panels.Length) - 1)
                        tempRow = row + 1;
                }
                desiredPos = panels[tempRow, tempCol].Position;
                column = tempCol;
                row = tempRow;
                canPressKey = false;
                isMoving = true;
                prevCommand = nextCommand;
                nextCommand = Keys.None;
                System.Diagnostics.Debug.WriteLine(prevCommand);
            }
            Move(panels[tempRow, tempCol].Position, tempRow, tempCol);
        }

        // Moves the character depending on what direction the user presses
        // (WASD). Vector2, newPos, represents the new position that the user
        // user has after moving, int newRow represents the new row after the
        // user has moved. Int newCol represents the new column after the user
        // has moved.
        private void Move(Vector2 newPos, int newRow, int newCol)
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

            }
            movingBounds.X = bounds.X + size / 4;
            movingBounds.Y = bounds.Y + size / 4;
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

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Rectangle Bounds
        {
            get
            {
                if (isMoving)
                    return movingBounds;
                return bounds;
            }
        }


    }
}
