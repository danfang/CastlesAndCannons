using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace CastlesAndCannonsMonoGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Player p;
        private Grid g;
        public static HUD scoreDisplay;
        public static int height;
        public static int width;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Assets";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            App.Current.DebugSettings.EnableFrameRateCounter = true;
            base.Initialize();
            height = graphics.GraphicsDevice.Viewport.Height;
            width = graphics.GraphicsDevice.Viewport.Width;
            g = new Grid();
            Textures.LoadContent(this);
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
            scoreDisplay = new HUD();
            scoreDisplay.font = Content.Load<SpriteFont>("SpriteFont2");
            // When loading fonts, go to CastlesAndCannonsMonoGame -> bin -> Windows8 -> Debug
            // AppX -> Assets and paste the Font in there
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
            // TODO: Add your update logic here
            base.Update(gameTime);
            g.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                App.Current.Exit();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            g.Draw(gameTime, spriteBatch);
            scoreDisplay.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
