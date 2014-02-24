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
    class Cannonball
    {
        public enum State
        {

        }

        public enum Direction
        {
            UP = 1,
            LEFT = 2,
            RIGHT = 3,
            DOWN = 4
        }

        public static float speed = 10;
        private int armor;
        private int health;
        private Vector2 velocity;
        private bool isAlive;
        private Vector2 pos;
        private Rectangle bounds; // make circle at some point
        
        public Cannonball(Direction d, Vector2 position)
        {
            armor = 30;
            health = 100;
            isAlive = true;
            pos = position;
            bounds = new Rectangle((int)position.X, (int)position.Y, Grid.PANEL_SIZE, Grid.PANEL_SIZE);
            switch (d)
            {
                case Direction.UP: // up
                    velocity = new Vector2(0, -1 * speed);
                    break;
                case Direction.RIGHT: // right
                    velocity = new Vector2(speed, 0);
                    break;
                case Direction.DOWN: // down
                    velocity = new Vector2(0, speed);
                    break;
                case Direction.LEFT: // left
                    velocity = new Vector2(-1 * speed, 0);
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            int seconds = (int) Grid.elapsedGameTime;
            if (seconds != 0)
            {
                pos.X += velocity.X;
                pos.Y += velocity.Y; 
                bounds.X = (int) pos.X;
                bounds.Y = (int) pos.Y;
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {   
            spriteBatch.Draw(Textures.cannonTexture, bounds, Color.White);
        }
    }
}
