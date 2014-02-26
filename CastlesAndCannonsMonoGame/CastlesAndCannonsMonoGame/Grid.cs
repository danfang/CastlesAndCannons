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
    class Grid
    {
        public enum State
        {

        }

        public const int GRID_SIZE = 8;
        public static int PANEL_SIZE;
        public static int GRID_WIDTH_OFFSET;
        public static int GRID_HEIGHT_OFFSET;
        public static int ENEMY_SPAWN_BUFFER;
        public static float elapsedGameTime;
        private Panel[,] panels;
        private int score; // to be implemented
        private LinkedList<Cannonball> enemies;
        private Character c;
        private Point mousePosition;
        private Point mouseClick;
        private float enemySpawnRate; // enemies per second
        private float enemySpawnTimer; // 
        private Random generator; // used to randomly generate enemies
        private Rectangle curHealthBar; // current health
        private Rectangle backgroundHealthBar; // background health
       
        // Creates a new instance of Grid. Puts the Character in the Grid at row 2
        // column 2.
        public Grid()
        {
            Initialize();
            LoadContent();
        }

        // Defines the constants of the Grid.
        private void Initialize()
        {
            panels = new Panel[GRID_SIZE, GRID_SIZE];
            score = 0;
            enemies = new LinkedList<Cannonball>();
            PANEL_SIZE = (Game1.height - 100) / GRID_SIZE;
            GRID_WIDTH_OFFSET = (Game1.width - (PANEL_SIZE * GRID_SIZE)) / 2;
            GRID_HEIGHT_OFFSET = (Game1.height - (PANEL_SIZE * GRID_SIZE)) / 2;
            ENEMY_SPAWN_BUFFER = PANEL_SIZE * 2;
            elapsedGameTime = 0;
            enemySpawnRate = .5f;
            generator = new Random();
        }

        // Creates the actual Grid and puts the Character in the Grid.
        private void LoadContent()
        {
            for(int row = 0; row < GRID_SIZE; row++)
            {
                for (int col = 0; col < GRID_SIZE; col++)
                {
                    panels[row, col] = new Panel(GRID_HEIGHT_OFFSET + row * PANEL_SIZE, GRID_WIDTH_OFFSET + col * PANEL_SIZE, PANEL_SIZE);
                }
            }
           c = new Knight(panels[2, 2].GetPosition(), PANEL_SIZE, 2, 2);
        }

        public void UnloadContent()
        {
            
        }

        // Updates the Grid class. In effect, it spawns new enemies, updates the cannonballs,
        // moves the character if there is movement.
        public void Update(GameTime gameTime)
        {
            backgroundHealthBar = new Rectangle(50, 20, 100, 20);
            mousePosition.X = Mouse.GetState().X;
            mousePosition.Y = Mouse.GetState().Y;

            elapsedGameTime += (float) gameTime.ElapsedGameTime.TotalSeconds;
            foreach (Panel p in panels)
            {
                p.Update(gameTime, mousePosition);
                p.Slashed(false);
            }
            SpawnEnemies(gameTime);
            UpdateCannonballs(gameTime);
            Slash();
            c.Update(gameTime, panels);
        }

        // Updates all of the cannonballs and if a collision occurs, removes
        // the CannonBall all together and interacts with the enemy player
        // depending on what CannonBall it is.
        // The GameTime parameter represents the current state of the game.
        private void UpdateCannonballs(GameTime gameTime)
        {
            Queue<Cannonball> toDestroy = new Queue<Cannonball>();
            foreach (Cannonball cannonball in enemies)
            {
                cannonball.Update(gameTime);
                if (cannonball.Bounds().Intersects(c.Bounds()))
                {
                    toDestroy.Enqueue(cannonball);
                }
            }
            while (toDestroy.Count != 0)
            {
                Cannonball collide = toDestroy.Dequeue();
                enemies.Remove(collide);
                c.Health -= collide.Damage;
                System.Diagnostics.Debug.WriteLine(c.Health);
                if (c.Health == 0) // Character is dead
                { 
                    System.Diagnostics.Debug.WriteLine("the character is dead");
                }
            }
            UpdateHealthBar();
        }

        // Updates the health bar with the new character health if it changed.
        private void UpdateHealthBar()
        {
            curHealthBar = new Rectangle(50, 20, c.Health, 20);
        }

        // Draws the Grid, Cannonballs, and Knight
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
            {
            foreach (Panel p in panels)
            {
                p.Draw(gameTime, spriteBatch);
            }

            foreach (Cannonball cannonball in enemies)
            {
                cannonball.Draw(gameTime, spriteBatch);
            }
            ((Knight)c).Draw(gameTime, spriteBatch);
            spriteBatch.Draw(Textures.backgroundTexture, backgroundHealthBar, Color.White);
            spriteBatch.Draw(Textures.healthTexture, curHealthBar, Color.Red);
        }

        // Returns the Character playing.
        public Character GetCharacter()
        {
            return c;
        }

        // TODO: Possible moving to Knight and implement in Knight Update?
        private void Slash()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                mouseClick.X = Mouse.GetState().X;
                mouseClick.Y = Mouse.GetState().Y;
                ((Knight)c).Slash(mouseClick);

                if (CheckSlashDirection(((Knight)c).SlashDirection) && !c.IsMoving)
                {
                    switch (((Knight)c).SlashDirection)
                    {
                        case 1: panels[c.Row - 1, c.Column].Slashed(true);
                            break;
                        case 2: panels[c.Row, c.Column + 1].Slashed(true);
                            break;
                        case 3: panels[c.Row + 1, c.Column].Slashed(true);
                            break;
                        case 4: panels[c.Row, c.Column - 1].Slashed(true);
                            break;
                    }
                }
            }

        }

        // TODO: Document
        private bool CheckSlashDirection(int slashDirection)
        {
            switch (slashDirection)
        {
                case 1:
                    if (c.Row - 1 < 0)
                        return false;
                    break;
                case 2:
                    if (c.Column + 1 > GRID_SIZE - 1)
                        return false;
                    break;
                case 3:
                    if (c.Row + 1 > GRID_SIZE - 1)
                        return false;
                    break;
                case 4:
                    if (c.Column - 1 < 0)
                        return false;
                    break;
            }
            return true;
        }
        
        // TODO: Document
        private void SpawnEnemies(GameTime gameTime)
        {
            enemySpawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (enemySpawnTimer < 0)
            {
                int seed = generator.Next((int)(1 / enemySpawnRate));
                if (seed == 0)
                {
                    Cannonball.Direction direction = (Cannonball.Direction) generator.Next(1, 5);
                    int index = generator.Next(GRID_SIZE);
                    Vector2 position = new Vector2();
                    switch (direction) 
                    {
                        case Cannonball.Direction.UP: // going up, starts at bottom
                            position = panels[GRID_SIZE - 1, index].GetPosition();
                            position.Y += ENEMY_SPAWN_BUFFER;
                            break;
                        case Cannonball.Direction.RIGHT: // going right, starts at left
                            position = panels[index, 0].GetPosition();
                            position.X -= ENEMY_SPAWN_BUFFER;
                            break;
                        case Cannonball.Direction.DOWN: // going down, starts at top
                            position = panels[0, index].GetPosition();
                            position.Y -= ENEMY_SPAWN_BUFFER;
                            break;
                        case Cannonball.Direction.LEFT: // going left, starts at right
                            position = panels[index, GRID_SIZE - 1].GetPosition();
                            position.X += ENEMY_SPAWN_BUFFER;
                            break;
                    }
                    enemies.AddLast(new Cannonball(direction, position));
                    System.Diagnostics.Debug.WriteLine(enemies.Count);
                }
                enemySpawnTimer = 1;
            }

        }
    }
}
