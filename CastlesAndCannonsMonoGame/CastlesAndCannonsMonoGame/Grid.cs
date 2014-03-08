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
        private const int NUMBER_OF_PATTERNS = 4;
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
        private Boolean isGameOver;
        private Pattern selectedPattern; // -1 if not selected
        private int patternCount;

        // Creates a new instance of Grid. Puts the Character in the Grid at row 2
        // column 2.
        public Grid()
        {
            Initialize();
            LoadContent();
        }

        public enum Pattern
        {
            NOT_SELECTED = 0,
            CASCADE_DOWN = 1,
            CASCADE_UP = 2,
            THREE_SHOT = 3,
            COMPLETELY_RANDOM = 4
        }

        // Defines the constants and objects of Grid.
        private void Initialize()
        {
            panels = new Panel[GRID_SIZE, GRID_SIZE];
            score = 0;
            enemies = new LinkedList<Cannonball>();
            PANEL_SIZE = (Game1.height - 100) / GRID_SIZE;
            GRID_WIDTH_OFFSET = (Game1.width - (PANEL_SIZE * GRID_SIZE)) / 2;
            GRID_HEIGHT_OFFSET = (Game1.height - (PANEL_SIZE * GRID_SIZE)) / 2;
            ENEMY_SPAWN_BUFFER = PANEL_SIZE * 3;
            elapsedGameTime = 0;
            enemySpawnRate = .5f;
            isGameOver = false;
            generator = new Random();
            selectedPattern = Pattern.NOT_SELECTED;
            enemySpawnTimer = 1f;
            patternCount = 0;
        }

        // Creates the actual Grid and puts the Character in the Grid.
        private void LoadContent()
        {
            for (int row = 0; row < GRID_SIZE; row++)
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
            if (!isGameOver)
            {
                backgroundHealthBar = new Rectangle(50, 20, 100, 20);
                mousePosition.X = Mouse.GetState().X;
                mousePosition.Y = Mouse.GetState().Y;

                elapsedGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                foreach (Panel p in panels)
                {
                    p.Update(gameTime, mousePosition);
                    p.Slashed(false);
                }
                SpawnEnemies(gameTime);
                UpdateCannonballs(gameTime);
                ((Knight)c).Update(gameTime, panels);
            }
            else
            {
                UnloadContent();
            }
        }

        // Updates all of the cannonballs and if a collision occurs, removes
        // the CannonBall all together and interacts with the enemy player
        // depending on what CannonBall it is.
        // The GameTime parameter represents the current state of the game.
        private void UpdateCannonballs(GameTime gameTime)
        {
            Queue<Cannonball> toDestroy = new Queue<Cannonball>();
            Queue<Cannonball> toRemove = new Queue<Cannonball>();
            foreach (Cannonball cannonball in enemies)
            {
                cannonball.Update(gameTime);
                if (cannonball.ActualBounds().Intersects(c.Bounds()))
                {
                    toDestroy.Enqueue(cannonball);
                }
                if (((Knight)c).SlashedPanel != null &&
                    (cannonball.Bounds().Intersects(((Knight)c).SlashedPanel.GetBounds())))
                {
                    toRemove.Enqueue(cannonball);
                    Game1.scoreDisplay.Score += 900;
                }
                if (cannonball.Bounds().X > Game1.width || cannonball.Bounds().Y > Game1.height + (PANEL_SIZE * 4)
                    || cannonball.Bounds().X < -(PANEL_SIZE * 3) || cannonball.Bounds().Y < -(PANEL_SIZE * 3))
                {
                    toRemove.Enqueue(cannonball);
                }
            }
            DestroyCannonballs(toDestroy);
            RemoveCannonballs(toRemove);
            UpdateHealthBar();
        }

        // Destroys the Cannonball if the Cannonball hits the user.
        private void DestroyCannonballs(Queue<Cannonball> collided)
        {
            while (collided.Count != 0)
            {
                Cannonball collide = collided.Dequeue();
                enemies.Remove(collide);
                c.Health -= collide.Damage;
                System.Diagnostics.Debug.WriteLine(c.Health);
                if (c.Health == 0) // Character is dead
                {
                    System.Diagnostics.Debug.WriteLine("the character is dead");
                    isGameOver = false;
                }
            }
        }

        // Removes the Cannonballs that are off the screen from the LinkedList of Cannonballs.
        // Also updates the score.
        private void RemoveCannonballs(Queue<Cannonball> toRemove)
        {
            while (toRemove.Count != 0)
            {
                Cannonball offScreen = toRemove.Dequeue();
                enemies.Remove(offScreen);
                Game1.scoreDisplay.Score += 100; // Add score for dodging CannonBall 
            }
        }

        // Updates the health bar with the new character health if it changed.
        private void UpdateHealthBar()
        {
            curHealthBar = new Rectangle(50, 20, c.Health, 20);
        }

        // Draws the Grid, Cannonballs, and Knight
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!isGameOver)
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
            else // game is over
            {
                GameOverScreen g = new GameOverScreen();
                g.Draw(gameTime, spriteBatch);
            }
        }

        // Returns the Character playing.
        public Character GetCharacter()
        {
            return c;
        }

        

        // Spawns a single Cannonball.
        private void SpawnEnemies(GameTime gameTime)
        {
            float timer = enemySpawnTimer - elapsedGameTime;
            if (timer < 0)
            {
                if (selectedPattern == Pattern.NOT_SELECTED)
                {
                    selectedPattern = (Pattern)generator.Next(1, NUMBER_OF_PATTERNS + 1);
                }
                activatePattern();
            }
        }

        // Performs one iteration of the currently selected Pattern.
        private void activatePattern()
        {
            Vector2 position = new Vector2();
            switch (selectedPattern)
            {
                case Pattern.CASCADE_DOWN:
                    {
                        ActivateCascadingDown(position);
                        break;
                    }
                case Pattern.CASCADE_UP:
                    {
                        ActivateCascadingUp(position);
                        break;
                    }
                case Pattern.THREE_SHOT:
                    {
                        ActivateThreeShot(position);
                        break;
                    }
                case Pattern.COMPLETELY_RANDOM:
                    {
                        CompletelyRandom(position);
                        break;
                    }
            }
            enemySpawnTimer = elapsedGameTime + .5f;
        }

        // Activates one part in the Cascading Down pattern. The Pattern resembles
        // a downward motion that cycles through the columns in the grid starting from
        // the left most column. After it cycles through the columns in teh grid
        // from the right most column, it deselects itself.
        private void ActivateCascadingDown(Vector2 position)
        {
            position = panels[0, patternCount].GetPosition();
            position.Y -= ENEMY_SPAWN_BUFFER;
            enemies.AddLast(new Cannonball(Cannonball.Direction.DOWN, position));
            checkToDeselect();
        }

        // Activates one iteration in the Cascading Up pattern. The Pattern resembles
        // an upward motion that cycles through the columns in the grid starting from
        // the left most column. After it cycles through the columns in the grid from
        // the left most column, it then deselects itself.
        private void ActivateCascadingUp(Vector2 position)
        {
            position = panels[GRID_SIZE - 1, patternCount].GetPosition();
            position.Y += ENEMY_SPAWN_BUFFER;
            enemies.AddLast(new Cannonball(Cannonball.Direction.UP, position));
            checkToDeselect();
        }


        // Activates one iteration of the MultiShot pattern. The MultiShot pattern fires
        // # of panels / 2 cannon balls first coming in from the right then coming in from the left
        // after a second has passed in between, then coming in from below and above respectively.
        // After this has happened, the pattern deselects itself.
        private void ActivateThreeShot(Vector2 position)
        {
            if (patternCount == 0)
            {
                for (int i = 0; i < GRID_SIZE / 2; i++)
                {
                    position = panels[i * 2, 0].GetPosition();
                    position.X -= ENEMY_SPAWN_BUFFER;
                    enemies.AddLast(new Cannonball(Cannonball.Direction.RIGHT, position));
                }
                patternCount++;
            }
            else if(patternCount == 1) // used to give a 1 second interval in between
            {
                for (int i = 0; i < GRID_SIZE / 2; i++)
                {
                    position = panels[i * 2 + 1, GRID_SIZE - 1].GetPosition();
                    position.X += ENEMY_SPAWN_BUFFER;
                    enemies.AddLast(new Cannonball(Cannonball.Direction.LEFT, position));
                }
                patternCount++;
            }
            else if (patternCount == 2)
            {
                for (int i = 0; i < GRID_SIZE / 2; i++)
                {
                    position = panels[GRID_SIZE - 1, i * 2 + 1].GetPosition();
                    position.Y += ENEMY_SPAWN_BUFFER;
                    enemies.AddLast(new Cannonball(Cannonball.Direction.UP, position));
                }
                patternCount++;
            }
            else // pattern count == 3
            {
                for (int i = 0; i < GRID_SIZE / 2; i++)
                {
                    position = panels[0, i * 2].GetPosition();
                    position.Y -= ENEMY_SPAWN_BUFFER;
                    enemies.AddLast(new Cannonball(Cannonball.Direction.DOWN, position));
                }
                selectedPattern = Pattern.NOT_SELECTED;
                patternCount = 0;
            }
        }

        // Activates one iteration of the Completely Random pattern. The Random pattern fires
        // at random from any direction and from any Panel. After the pattern has fired GRID_SIZE
        // Cannonballs, the pattern deselects itself.
        private void CompletelyRandom(Vector2 position)
        {
            Cannonball.Direction direction = (Cannonball.Direction)generator.Next(1, NUMBER_OF_PATTERNS + 1);
            int index = generator.Next(GRID_SIZE);
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
            checkToDeselect();
        }

        // Performs a check if the Pattern needs to be deselected. If the Pattern has fired
        // GRID_SIZE Cannonballs, it will deselect itself.
        private void checkToDeselect()
        {
            patternCount++;
            if (patternCount == Grid.GRID_SIZE)
            {
                selectedPattern = Pattern.NOT_SELECTED; // deselect
                patternCount = 0;
            }
        }

    }
}
