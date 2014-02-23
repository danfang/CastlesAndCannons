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
        protected int row;
        protected int column;

        public void UnloadContent()
        {

        }
        public void Update(GameTime gameTime)
        {

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }

        /*******************
         * GET/SET METHODS *
         *******************/

        public void move(Vector2 newPos)
        {
            position = newPos;
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


    }
}
