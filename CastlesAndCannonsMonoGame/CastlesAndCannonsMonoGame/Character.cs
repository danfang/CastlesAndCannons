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

        public void UnloadContent()
        {

        }
        public void Update(GameTime gameTime)
        {

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }
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

        public Vector2 GetVector()
        {
            return position;
        }

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
