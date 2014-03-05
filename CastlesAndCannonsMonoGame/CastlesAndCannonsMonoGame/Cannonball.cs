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
        // Used to identify which type of Cannonball is shot.
        public enum State
        {

        }

        // Used to identify which direction the Cannonball is going in.
        public enum Direction
        {
            UP = 1,
            LEFT = 2,
            RIGHT = 3,
            DOWN = 4
        }

        public static float speed = 10;
        private int damage;
        private int armor;
        private int health;
        private Vector2 velocity;
        private bool isAlive;
        private Vector2 pos;
        private Rectangle bounds; // make circle at some point
        private Rectangle actualBounds; 
        
        // Constructs a new Cannonball. Direction d represents the direction
        // the Cannonball is going to be going and the Vector2, position, represents
        // the initial position the Cannonball is going to be in.
        public Cannonball(Direction d, Vector2 position)
        {
            damage = 20; // regular cannonball damage
            armor = 30;
            health = 100;
            isAlive = true;
            pos = position;
            bounds = new Rectangle((int)position.X, (int)position.Y, Grid.PANEL_SIZE, Grid.PANEL_SIZE);
            float dividingConstant = (float) (3 / (Math.Sqrt(2)));
            actualBounds = new Rectangle((int)position.X, (int)position.Y, (int) (Grid.PANEL_SIZE / dividingConstant), (int) (Grid.PANEL_SIZE / dividingConstant));
            switch (d) // Direction
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

        // Updates the Cannonball movement as it is firing.
        public void Update(GameTime gameTime)
        {
            int seconds = (int) Grid.elapsedGameTime;
            if (seconds != 0)
            {
                pos.X += velocity.X;
                pos.Y += velocity.Y; 
                bounds.X = (int) pos.X;
                bounds.Y = (int) pos.Y;
                actualBounds.X = (int)pos.X;
                actualBounds.Y = (int)pos.Y;
            }

        }
        
        // Draws the updated Cannonball.
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {   
            spriteBatch.Draw(Textures.cannonTexture, bounds, Color.White);
        }

        // Returns the Rectangle representing the bounds of the Cannonball.
        public Rectangle Bounds()
        {
            return bounds;
        }

        public Rectangle ActualBounds()
        {
            return actualBounds;
        }

        // Returns the amount of damage the Cannonball deals.
        public int Damage
        {
            get
            {
                return damage;
            }
        }
    }
}
