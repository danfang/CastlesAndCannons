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
        public enum Type
        {

        }

        public Panel(int x, int y, int size)
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

        public Rectangle getBounds()
        {
            return bounds;
        }

        public Vector2 getPosition()
        {
            Point p = bounds.Location;
            return new Vector2(p.X, p.Y);
        }

        public void Slashed(bool newSlash)
        {
            isSlashed = newSlash;
        }
    }
}
