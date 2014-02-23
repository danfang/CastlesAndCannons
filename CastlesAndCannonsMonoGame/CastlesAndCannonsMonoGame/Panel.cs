﻿using System;
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
        private Rectangle bounds;
        public enum Type
        {

        }

        public Panel(int x, int y, int size)
        {
            bounds = new Rectangle(x, y, size, size);
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

        public void Update(GameTime gameTime)
        {
            

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.normalPanelTexture, bounds, Color.Beige);
        }
    }
}
