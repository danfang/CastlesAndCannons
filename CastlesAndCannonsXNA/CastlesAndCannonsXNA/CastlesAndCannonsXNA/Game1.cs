using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CastlesAndCannonsXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int height;
        int width;
        bool toRecord;
        double anchorTime;
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            // TODO: Add your initialization logic here
            height = graphics.GraphicsDevice.Viewport.Height;
            width = graphics.GraphicsDevice.Viewport.Width;
            toRecord = true;
            anchorTime = 0;
            font = Content.Load<SpriteFont>("font");
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (toRecord)
            {
                anchorTime = gameTime.TotalGameTime.TotalSeconds;
                toRecord = false;
            }
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // exits the game if q is pressed on the keyboard
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                this.Exit();
            // TODO: Add your update logic here

            // exits the game if 3 seconds have passed
            if (timeElapsed(gameTime, anchorTime, 3.0))
               this.Exit();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Maroon);
            // TODO: Add your drawing code here
            //BoundingSphere bs = new BoundingSphere(new Vector3(100, 100, 0), 100.0f);
           
            spriteBatch.Begin();
            drawMessage(spriteBatch, "This is Castles and Cannons!", Color.White);
            spriteBatch.End();
        }

        private bool timeElapsed(GameTime gameTime, double originalTime, double timeElapsed)
        {
            if (gameTime.TotalGameTime.TotalSeconds - originalTime >= timeElapsed)
                return true;
            return false;
        }

        private void drawMessage(SpriteBatch batch, string message, Color color)
        {
            Vector2 fontOrigin = font.MeasureString(message) / 2;
            spriteBatch.DrawString(font, message, new Vector2(width / 2, height / 2),
                                      color, 0, fontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}
