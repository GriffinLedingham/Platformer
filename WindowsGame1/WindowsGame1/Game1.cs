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

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Circle MyCircle;
        public static List<Surface> surfaces = new List<Surface>();

        public static int windowHeight, windowWidth;

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
            // TODO: Add your initialization logic here

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
            
            windowWidth = Window.ClientBounds.Width;
            windowHeight = Window.ClientBounds.Height - 37;
            MyCircle = new Circle(Content, spriteBatch);
            for (int i = 0; i < (float)Math.Ceiling((double)800 / (double)43)-3; i++)
            {
                if (i == 6 || i ==7) continue;
                surfaces.Add(new Surface(Content, spriteBatch, 43, 42, i*43,windowHeight));
            }


            surfaces.Add(new Surface(Content, spriteBatch, 43, 42, 200, windowHeight - 42));
            surfaces.Add(new Surface(Content, spriteBatch, 43, 42, 200, windowHeight - 42));
            surfaces.Add(new Surface(Content, spriteBatch, 43, 42, 243, windowHeight - 42*2));
            surfaces.Add(new Surface(Content, spriteBatch, 43, 42, 200+43*2, windowHeight - 42*3));
            surfaces.Add(new Surface(Content, spriteBatch, 43, 42, 200 + 43*5, windowHeight - 42*4));

            surfaces.Add(new Surface(Content, spriteBatch, 43, 42, 200 + 43 * 6, windowHeight - 42 * 4));
            surfaces.Add(new Surface(Content, spriteBatch, 43, 42, 200 + 43 * 7, windowHeight - 42 * 4));
            surfaces.Add(new Surface(Content, spriteBatch, 43, 42, 200 + 43 * 8, windowHeight - 42 * 4));


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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();



            // TODO: Add your update logic here
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Left))
            {
                MyCircle.keyPressed(Keys.Left);
            }
            if (keyState.IsKeyDown(Keys.Right))
            {
                MyCircle.keyPressed(Keys.Right);
            }
            if (keyState.IsKeyDown(Keys.Up))
            {
                MyCircle.keyPressed(Keys.Up);
            }
            /*if (keyState.IsKeyDown(Keys.Down))
            {
                MyCircle.keyPressed(Keys.Down);
            }*/



            MyCircle.update();

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //spriteBatch.Begin();
            //MyLevel1.draw();
            for (int i = 0; i < surfaces.Count; i++)
            {
                surfaces[i].draw();
            }

            MyCircle.draw();
            //spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}