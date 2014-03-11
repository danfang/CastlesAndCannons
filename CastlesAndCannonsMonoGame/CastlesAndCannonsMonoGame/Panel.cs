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
    class Panel
    {
        private bool isSlashed;
        private bool isSelected;
        private Rectangle bounds;
        private const int SLASH_DURATION = 25;
        private int slashDurationTimer;
        public enum Type
        {

        }

        public Panel(int y, int x, int size)
        {
            bounds = new Rectangle(x, y, size, size);
            isSelected = false;
            isSlashed = false;
            Initialize();
            LoadContent();
        }

        private void Initialize()
        {
        }

        private void LoadContent()
        {

        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime, Point mousePosition)
        {
            if (isSlashed && slashDurationTimer == 0)
                slashDurationTimer = SLASH_DURATION;

            if (slashDurationTimer > 0)
                slashDurationTimer--;

            if (slashDurationTimer == 0)
                isSlashed = false;

            if (bounds.Contains(mousePosition))
                isSelected = true;
            else
                isSelected = false;
            
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isSlashed)
                spriteBatch.Draw(Textures.normalPanelTexture, bounds, Color.Red);
            else if (isSelected)
                spriteBatch.Draw(Textures.normalPanelTexture, bounds, Color.Green);
            else
                spriteBatch.Draw(Textures.normalPanelTexture, bounds, Color.White);
        }

        /*******************
         * GET/SET METHODS *
        *******************/

        public Rectangle Bounds
        {
            get
            {
                return bounds;
            }
        }

        public Vector2 Position
        {
            get
            {
                Point p = bounds.Location;
                return new Vector2(p.X, p.Y);
            }
        }

        public bool Slashed
        {
            get
            {
                return isSlashed;
            }
            set 
            {
                isSlashed = value;
            }
        }
    }
}
