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
        public static bool leftMouseClicked;
        public static bool rightMouseClicked;
        public static Point leftMouseClick;

        private const int NUMBER_OF_PATTERNS = 5;
        private int score; // to be implemented
        private int patternCount;
        private float enemySpawnTimer; // 
        private bool isGameOver;
        private Panel[,] panels;
        private LinkedList<Cannonball> enemies;
        private Character c;
        private Point mousePosition;
        private Random generator; // used to randomly generate enemies
        private Rectangle curHealthBar; // current health
        private Rectangle curManaBar;
        private Rectangle backgroundHealthBar; // background health
        private Pattern selectedPattern; // -1 if not selected

        public enum Pattern
        {
            NOT_SELECTED = 0,
            CASCADE_DOWN = 1,
            CASCADE_UP = 2,
            THREE_SHOT = 3,
            COMPLETELY_RANDOM = 4,
            TARGET_SHOT = 5
        }

        // Creates a new instance of Grid. Puts the Character in the Grid at row 2
        // column 2.
        public Grid()
        {
            Initialize();
            LoadContent();
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
            isGameOver = false;
            generator = new Random();
            selectedPattern = Pattern.NOT_SELECTED;
            enemySpawnTimer = 1f;
            patternCount = 0;
            curHealthBar = new Rectangle(50, 20, 100, 20);
            curManaBar = new Rectangle(50, 70, 100, 20);
        }

        // Creates the actual Grid and puts the Character in the Grid.
        private void LoadContent()
        {
            for (int row = 0; row < GRID_SIZE; row++)
            {
                for (int col = 0; col < GRID_SIZE; col++)
                    panels[row, col] = new Panel(GRID_HEIGHT_OFFSET + row * PANEL_SIZE, GRID_WIDTH_OFFSET + col * PANEL_SIZE, PANEL_SIZE);
                }
            c = new Knight(panels[2, 2].Position, PANEL_SIZE, 2, 2);
            }

        public void UnloadContent()
        {

        }

        // Updates the Grid class. In effect, it spawns new enemies, updates the cannonballs,
        // moves the character if there is movement.
        public void Update(GameTime gameTime)
        {
            UpdateMouseClicks();
            if (!isGameOver)
            {
                backgroundHealthBar = new Rectangle(50, 20, 100, 20);

                elapsedGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                foreach (Panel p in panels)
                {
                    p.Update(gameTime, mousePosition);
                }
                SpawnEnemies(gameTime);
                UpdateCannonballs(gameTime);
                if (c.GetType().Equals(typeof(Knight)))
                ((Knight)c).Update(gameTime, panels);
            }
            else
                UnloadContent();
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

                if (cannonball.ActualBounds.Intersects(c.Bounds))
                    toDestroy.Enqueue(cannonball);

                if (c.GetType().Equals(typeof(Knight)))
                    ((Knight)c).removeCannonBall(cannonball, toRemove);

                if (cannonball.Bounds.X > Game1.width || cannonball.Bounds.Y > Game1.height + (PANEL_SIZE * 4)
                    || cannonball.Bounds.X < -(PANEL_SIZE * 3) || cannonball.Bounds.Y < -(PANEL_SIZE * 3))
                    toRemove.Enqueue(cannonball);
            }
            DestroyCannonballs(toDestroy);
            RemoveCannonballs(toRemove);
            HealthBar = c.Health;
            ManaBar = c.Mana;
        }

        // Destroys the Cannonball if the Cannonball hits the user.
        private void DestroyCannonballs(Queue<Cannonball> collided)
        {
            while (collided.Count != 0)
            {
                Cannonball collide = collided.Dequeue();
                enemies.Remove(collide);
                if (c.GetType().Equals(typeof(Knight)) && ((Knight)c).Shielded)
                    c.Health -= collide.Damage / 3;
                else 
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


        // Draws the Grid, Cannonballs, Knight, and health/mana bars.
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!isGameOver)
            {
                foreach (Panel p in panels)
                    p.Draw(gameTime, spriteBatch);

                foreach (Cannonball cannonball in enemies)
                    cannonball.Draw(gameTime, spriteBatch);

                c.Draw(gameTime, spriteBatch);
                spriteBatch.Draw(Textures.backgroundTexture, backgroundHealthBar, Color.White);
                spriteBatch.Draw(Textures.healthTexture, curHealthBar, Color.Red);
                spriteBatch.Draw(Textures.manaTexture, curManaBar, Color.Blue);
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
                if (selectedPattern == Pattern.NOT_SELECTED) {
                    selectedPattern = (Pattern)generator.Next(1, NUMBER_OF_PATTERNS + 1);
                }
                ActivatePattern();
            }
        }

        // Performs one iteration of the currently selected Pattern.
        private void ActivatePattern()
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
                        ActivateCompletelyRandom(position);
                        break;
                    }
                case Pattern.TARGET_SHOT:
                    {
                        ActivateTargetShot(position);
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
            position = panels[0, patternCount].Position;
            position.Y -= ENEMY_SPAWN_BUFFER;
            enemies.AddLast(new Cannonball(Cannonball.Direction.DOWN, position, 10));
            CheckToDeselect();
        }

        // Activates one iteration in the Cascading Up pattern. The Pattern resembles
        // an upward motion that cycles through the columns in the grid starting from
        // the left most column. After it cycles through the columns in the grid from
        // the left most column, it then deselects itself.
        private void ActivateCascadingUp(Vector2 position)
        {
            position = panels[GRID_SIZE - 1, patternCount].Position;
            position.Y += ENEMY_SPAWN_BUFFER;
            enemies.AddLast(new Cannonball(Cannonball.Direction.UP, position, 10));
            CheckToDeselect();
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
                    position = panels[i * 2, 0].Position;
                    position.X -= ENEMY_SPAWN_BUFFER;
                    enemies.AddLast(new Cannonball(Cannonball.Direction.RIGHT, position, 10));
                }
                patternCount++;
            }
            else if(patternCount == 1) // used to give a 1 second interval in between
            {
                for (int i = 0; i < GRID_SIZE / 2; i++)
                {
                    position = panels[i * 2 + 1, GRID_SIZE - 1].Position;
                    position.X += ENEMY_SPAWN_BUFFER;
                    enemies.AddLast(new Cannonball(Cannonball.Direction.LEFT, position, 10));
                }
                patternCount++;
            }
            else if (patternCount == 2)
            {
                for (int i = 0; i < GRID_SIZE / 2; i++)
                {
                    position = panels[GRID_SIZE - 1, i * 2 + 1].Position;
                    position.Y += ENEMY_SPAWN_BUFFER;
                    enemies.AddLast(new Cannonball(Cannonball.Direction.UP, position, 10));
                }
                patternCount++;
            }
            else // pattern count == 3
            {
                for (int i = 0; i < GRID_SIZE / 2; i++)
                {
                    position = panels[0, i * 2].Position;
                    position.Y -= ENEMY_SPAWN_BUFFER;
                    enemies.AddLast(new Cannonball(Cannonball.Direction.DOWN, position, 10));
                }
                selectedPattern = Pattern.NOT_SELECTED;
                patternCount = 0;
            }
        }

        // Activates one iteration of the Completely Random pattern. The Random pattern fires
        // at random from any direction and from any Panel. After the pattern has fired GRID_SIZE
        // Cannonballs, the pattern deselects itself.
        private void ActivateCompletelyRandom(Vector2 position)
        {
            Cannonball.Direction direction = (Cannonball.Direction)generator.Next(1, 5);
            int index = generator.Next(GRID_SIZE);
            switch (direction)
            {
                case Cannonball.Direction.UP: // going up, starts at bottom
                    position = panels[GRID_SIZE - 1, index].Position;
                    position.Y += ENEMY_SPAWN_BUFFER;
                    break;
                case Cannonball.Direction.RIGHT: // going right, starts at left
                    position = panels[index, 0].Position;
                    position.X -= ENEMY_SPAWN_BUFFER;
                    break;
                case Cannonball.Direction.DOWN: // going down, starts at top
                    position = panels[0, index].Position;
                    position.Y -= ENEMY_SPAWN_BUFFER;
                    break;
                case Cannonball.Direction.LEFT: // going left, starts at right
                    position = panels[index, GRID_SIZE - 1].Position;
                    position.X += ENEMY_SPAWN_BUFFER;
                    break;
            }
            enemies.AddLast(new Cannonball(direction, position, 10));
            CheckToDeselect();
        }

        private void ActivateTargetShot(Vector2 position)
        {
            position = panels[c.Row, 0].Position;
            position.X -= ENEMY_SPAWN_BUFFER;
            enemies.AddLast(new Cannonball(Cannonball.Direction.RIGHT, position, 12));
            position = panels[c.Row, GRID_SIZE - 1].Position;
            position.X += ENEMY_SPAWN_BUFFER;
            enemies.AddLast(new Cannonball(Cannonball.Direction.LEFT, position, 12));
            position = panels[GRID_SIZE - 1, c.Column].Position;
            position.Y += ENEMY_SPAWN_BUFFER;
            enemies.AddLast(new Cannonball(Cannonball.Direction.UP, position, 12));
            position = panels[0, c.Column].Position;
            position.Y -= ENEMY_SPAWN_BUFFER;
            enemies.AddLast(new Cannonball(Cannonball.Direction.DOWN, position, 12));
            selectedPattern = Pattern.NOT_SELECTED;
            patternCount = 0;
        }

        // Performs a check if the Pattern needs to be deselected. If the Pattern has fired
        // GRID_SIZE Cannonballs, it will deselect itself.
        private void CheckToDeselect()
        {
            patternCount++;
            if (patternCount == Grid.GRID_SIZE)
            {
                selectedPattern = Pattern.NOT_SELECTED; // deselect
                patternCount = 0;
            }
        }

        private void UpdateMouseClicks()
        {
            mousePosition.X = Mouse.GetState().X;
            mousePosition.Y = Mouse.GetState().Y;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                leftMouseClicked = true;
                leftMouseClick.X = mousePosition.X;
                leftMouseClick.Y = mousePosition.Y;
            }
            else
                leftMouseClicked = false;

            if (Mouse.GetState().RightButton == ButtonState.Pressed)
                rightMouseClicked = true;
            else
                rightMouseClicked = false;
        }

        /*******************
        * GET/SET METHODS *
        *******************/

        // Updates the health bar with the new character health if it changed.
        private int HealthBar
        {
            set {
                curHealthBar.Width = value;
            }
        }

        // Updates the mana bar with the new mana if it changed.
        private int ManaBar
        {
            set
            {
                curManaBar.Width = value;
            }
        }
    }
}
