﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics;

namespace CastlesAndCannonsMonoGame
{
    class Knight : Character
    {
        private int slashDirection; // 0 = no slash, 1 is top (goes clockwise)
        private Panel slashedPanel;
        private float mouseAngle; // 1 is top (clockwise)
        private Func<float, float, float> GetAngle;
        private Point mouseClick;

        public enum SlashDirections
        {
            UP, RIGHT, LEFT, DOWN
        }


        public Knight(Vector2 pos, int newSize, int row, int col)
           : base(pos, newSize, row, col)
        {
            GetAngle = (x, y) => (float) Math.Atan2(y, x);
        }

        public void Update(GameTime gameTime, Panel[,] grid)
        {
            base.Update(gameTime, grid);
            SlashedPanel = null;
            SlashDirection = 0;
            Slash(grid);

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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
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
        // TODO: Possible moving to Knight and implement in Knight Update?
        private void Slash(Panel[,] panels)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                mouseClick.X = Mouse.GetState().X;
                mouseClick.Y = Mouse.GetState().Y;
                Slash(mouseClick);

                if (CheckSlashDirection(SlashDirection) && !IsMoving)
                {
                    switch (SlashDirection)
                    {
                        case 1:
                            SlashedPanel = panels[Row - 1, Column];
                            break;
                        case 2:
                            SlashedPanel = panels[Row, Column + 1];
                            break;
                        case 3:
                            SlashedPanel = panels[Row + 1, Column];
                            break;
                        case 4:
                            SlashedPanel = panels[Row, Column - 1];
                            break;
                    }
                    SlashedPanel.Slashed(true);
                }
            }
        }

        // TODO: Document
        private bool CheckSlashDirection(int slashDirection)
        {
            switch (slashDirection)
            {
                case 1:
                    if (Row - 1 < 0)
                        return false;
                    break;
                case 2:
                    if (Column + 1 > Grid.GRID_SIZE - 1)
                        return false;
                    break;
                case 3:
                    if (Row + 1 > Grid.GRID_SIZE - 1)
                        return false;
                    break;
                case 4:
                    if (Column - 1 < 0)
                        return false;
                    break;
            }
            return true;
        }

    }
}
