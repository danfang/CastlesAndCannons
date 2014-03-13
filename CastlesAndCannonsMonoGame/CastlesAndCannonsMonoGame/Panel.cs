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
        private Type panelType;
        private int column;
        private int row;
        private float panelDespawn;

        public enum Type
        {
            NORMAL, LAVA, WALL
        }

        public Panel(int x, int y, int size, int row, int column)
        {
            bounds = new Rectangle(x, y, size, size);
            this.column = column;
            this.row = row;
            panelDespawn = 1f;
            isSelected = false;
            isSlashed = false;
            panelType = Type.NORMAL;
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

            if (panelType != Type.NORMAL)
            {
                DespawnPanel();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isSlashed)
                spriteBatch.Draw(Textures.normalPanelTexture, bounds, Color.Red);
            else if (panelType == Type.LAVA)
                spriteBatch.Draw(Textures.normalPanelTexture, bounds, Color.Yellow);
            else if (isSelected)
                spriteBatch.Draw(Textures.normalPanelTexture, bounds, Color.LightPink);
            else
                spriteBatch.Draw(Textures.normalPanelTexture, bounds, Color.White);
        }

        private void DespawnPanel() {
            float timer = panelDespawn - Grid.elapsedGameTime;
            if (timer < 0)
            {
                panelType = Type.NORMAL;
            }
        }


        public void SpawnPanel(Type type)
        {
            PanelType = Panel.Type.LAVA;
            panelDespawn = Grid.elapsedGameTime + 5f;
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

        public int Column
        {
            get
            {
                return column;
            }
        }

        public int Row
        {
            get
            {
                return row;
            }
        }

        public Type PanelType
        {
            get
            {
                return panelType;
            }
            set
            {
                panelType = value;
            }
        }
    }
}
