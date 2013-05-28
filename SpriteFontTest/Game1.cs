using System;

using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SpriteFontTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Microsoft.Xna.Framework.Graphics.SpriteFont font1;
        Microsoft.Xna.Framework.Graphics.SpriteFont font2;
        float rotation;
        Vector2 textSize;

        private RenderTargetText rtt1;
        private RenderTargetText rtt2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;
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
            graphics.IsFullScreen = false;
            textures = new List<Texture2D>(n);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font1 = Content.Load<SpriteFont>("SpriteFont1");
            font2 = Content.Load<SpriteFont>("Arial32");
            textSize = font1.MeasureString("MonoGame");
            textSize = new Vector2(textSize.X / 2, textSize.Y / 2);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (!renderTargetsCreated) 
                CreateRenderTargets();

            // TODO: Add your update logic here		
            rotation += 0.1f;
            if (rotation > MathHelper.TwoPi)
            {
                rotation = 0.0f;
            }
            base.Update(gameTime);
        }

        private bool renderTargetsCreated = false;
        private int n = 25;
        private List<Texture2D> textures;

        private void CreateRenderTargets()
        {
            renderTargetsCreated = true;
            SpriteFont font = font1;

            for (int i = 0; i < n; i++)
            {
                RenderTargetText rtt = new RenderTargetText(GraphicsDevice, font, "Generic generated text: " + i);
                Console.Out.WriteLine("Created target " + i);
                rtt.Begin();
                rtt.Draw();
                rtt.End();

                textures.Add(rtt.Texture);

                if (i == n / 2)
                    font = font2;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.DrawString(font1, "Not from a render target", new Vector2(200, 150), Color.Red);
            for (int i = 0; i < n; i++)
            {
                spriteBatch.Draw(textures[i], new Vector2(0, 20*i), Color.White);
            }
            spriteBatch.DrawString(font1, "Not from a render target", new Vector2(200, 250), Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
