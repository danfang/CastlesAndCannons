using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics;

namespace CastlesAndCannonsMonoGame
{
    class Knight : Character
    { 
        private int slashDirection; // 0 = no slash, 1 is top (goes clockwise)
        private float mouseAngle; // 1 is top (clockwise)
        private Func<float, float, float> getAngle;
        public Knight(Vector2 pos, int newSize)
        {
            getAngle = (x, y) => (float) Math.Atan2(y, x);
            health = 100;
            mana = 100;
            armor = 2;
            speed = 2.5f;
            position = pos;
            size = newSize;
            bounds = new Rectangle((int)position.X, (int)position.Y, size, size);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Slash(Point mouseClick)
        {
            mouseAngle = getAngle(mouseClick.X - position.X, position.Y - mouseClick.Y) * 180 / (float) Math.PI;
            if (mouseAngle > 45 && mouseAngle < 135)
                slashDirection = 1;
            else if (mouseAngle < 45 && mouseAngle > -45)
                slashDirection = 2;
            else if (mouseAngle < -45 && mouseAngle > -135)
                slashDirection = 3;
            else
                slashDirection = 4;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.knightTexture, bounds, Color.White);
        }

        public int SlashDirection
        {
            get
            {
                return slashDirection;
            }
        }
    }
}
