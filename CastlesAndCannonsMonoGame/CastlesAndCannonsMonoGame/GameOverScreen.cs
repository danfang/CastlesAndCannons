using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace CastlesAndCannonsMonoGame
{
    public class GameOverScreen
    {
        private KeyboardState lastState;

        public GameOverScreen()
        {

        }

        public void Update() // to be implemented
        {

        }

        public void Draw(GameTime time, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.backgroundTexture, new Rectangle(0, 0, Game1.width, Game1.height), Color.White);
           
        }

    }
}
