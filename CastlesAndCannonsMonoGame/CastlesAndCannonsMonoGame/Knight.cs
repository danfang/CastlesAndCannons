using System;
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
        private const int SLASH_COST = 50;
        private const int SHIELD_COST = 50;
        private int slashDirection; // 0 = no slash, 1 is top (goes clockwise)
        private int slashStamina;
        private int shieldStamina;
        private float mouseAngle;
        private bool isShielded;
        private Panel slashedPanel;
        private Func<float, float, float> GetAngle;
        private Rectangle reloadBar;
        private Rectangle shieldBar;
        private Dictionary<Controls, Skills> controllerDict;

        public enum SlashDirections
        {
            UP, RIGHT, LEFT, DOWN
        }

        public enum Controls
        {
            RIGHT_MOUSE,
            LEFT_MOUSE
        }

        public enum Skills
        {
            SLASH, SHIELD, TELEPORT
        }

        //
        public Knight(Vector2 pos, int newSize, int row, int col)
           : base(pos, newSize, row, col)
        {
            GetAngle = (x, y) => (float) Math.Atan2(y, x);
            reloadBar = new Rectangle(200, 20, SLASH_COST * 2, 20);
            shieldBar = new Rectangle(200, 50, SHIELD_COST * 2, 20);
            slashStamina = SLASH_COST;
            shieldStamina = SHIELD_COST;
            controllerDict = new Dictionary<Controls, Skills>();
            controllerDict.Add(Controls.LEFT_MOUSE, Skills.SLASH);
            controllerDict.Add(Controls.RIGHT_MOUSE, Skills.SHIELD);
        }

        public override void Update(GameTime gameTime, Panel[,] grid)
        {
            base.MoveCharacter(grid);
            foreach (KeyValuePair<Controls, Skills> pair in controllerDict) 
                Action(pair.Value, grid, pair.Key);

            reloadBar.Width = slashStamina * 2;
            shieldBar.Width = shieldStamina * 2;
        }
        
        // Draws the knight sprite.
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(!isShielded)
                spriteBatch.Draw(Textures.knightTextures[direction], bounds, Color.White);
            else
                spriteBatch.Draw(Textures.knightTextures[direction], bounds, Color.Blue);
            spriteBatch.Draw(Textures.healthTexture, reloadBar, Color.Purple);
            spriteBatch.Draw(Textures.healthTexture, shieldBar, Color.Maroon);
        }

        // Calculates the slash direction based on a given mouse click (one of four
        // directions) relative to the knight.
        private void SetSlashDirection(Point mouseClick)
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

        // Checks the collision between the knight slashed panel and the given cannonball.
        // Adds the cannonball to the toRemove queue if a collision is detected.
        public void removeCannonBall(Cannonball cannonball, Queue<Cannonball> toRemove)
        {
            if (SlashedPanel != null && cannonball.Bounds.Intersects(SlashedPanel.Bounds))
            {
                toRemove.Enqueue(cannonball);
                Game1.scoreDisplay.Score += 900;
            }
        }

        // Sets the pointer to the desired slashed panel.
        private void Slash(Panel[,] panels, Controls control)
        {
            SlashDirection = 0;
            if (slashStamina < SLASH_COST)
                slashStamina += 2;

            if (SlashedPanel != null)
            {
                if (SlashedPanel.Slashed && isMoving)
                {
                    SlashedPanel.Slashed = false;
                    SlashedPanel = null;
                }
                else if (!SlashedPanel.Slashed)
                    SlashedPanel = null;
            }

            if (Grid.leftMouseClicked && (slashStamina - SLASH_COST >= 0))
            {
                slashStamina -= SLASH_COST;
                SetSlashDirection(Grid.leftMouseClick);

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
                    SlashedPanel.Slashed = true;
                }
            }
        }

        // Returns true if the desired slash direction is valid (within the bounds of
        // the grid) and returns false otherwise.
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

        private void Shield(Controls control)
        {
            if (!isShielded && shieldStamina < SHIELD_COST)
                shieldStamina += 2;

            if (shieldStamina <= 0)
                isShielded = false;
            
            if (Grid.rightMouseClicked && shieldStamina > 0)
            {
                isShielded = true;
                shieldStamina-=3;
            }
            else
                isShielded = false;
            
        }

        private void Action(Skills skill, Panel[,] grid, Controls control)
        {
            if (skill == Skills.SLASH)
                Slash(grid, control);
            else if (skill == Skills.SHIELD)
                Shield(control);
            else if (skill == Skills.TELEPORT) { }
        }

        /*******************
        * GET/SET METHODS *
        *******************/

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

        public bool Shielded
        {
            get
            {
                return isShielded;
            }
        }
    }
}
