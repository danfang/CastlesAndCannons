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
    public class HUD
    {
        private Vector2 scorePos;
        public SpriteFont font { get; set; }
        public int Score { get; set; }

        public HUD()
        {
            scorePos = new Vector2(40, 40);
            Score = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Score : " + Score.ToString(), scorePos, Color.White);
        }

    }
}
