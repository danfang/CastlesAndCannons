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
        private Panel slashedPanel;
        private float mouseAngle; // 1 is top (clockwise)
        private Func<float, float, float> GetAngle;

        public enum SlashDirections
        {
            UP, RIGHT, LEFT, DOWN
        }


        public Knight(Vector2 pos, int newSize, int row, int col)
           : base(pos, newSize, row, col)
        {
            GetAngle = (x, y) => (float) Math.Atan2(y, x);
        }

        public void Update(GameTime gameTime)
        {
            
        }

        
        public void Slash(Point mouseClick)
        {
            mouseAngle = GetAngle(mouseClick.X - position.X, position.Y - mouseClick.Y) * 180 / (float) Math.PI;
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
            spriteBatch.Draw(Textures.knightTextures[direction], bounds, Color.White);
        }

        public int SlashDirection
        {
            get
            {
                return slashDirection;
            }
            set
            {
                slashDirection = value;
            }
        }

        public Panel SlashedPanel
        {
            get
            {
                return slashedPanel;
            }
            set
            {
                slashedPanel = value;
            }
        }

    }
}
