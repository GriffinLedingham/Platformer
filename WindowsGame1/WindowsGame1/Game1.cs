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
        SpriteBatch ForegroundBatch;
        SpriteFont Segoi, SegoiSmall;
        Player MyCircle;
        Player2 MyCircle2;
        Player3 MyCircle3;
        Player4 MyCircle4;
        int score1, score2, score3, score4;

        public static List<Surface> surfaces = new List<Surface>();
        public static List<Item> items = new List<Item>();
        public static float camX, camY;
        public static Level currentLevel;
        public static int score = 0;
        public static bool gameOver = false;
        public static int checkTime;
        public static Color bgColor = Color.SkyBlue;
        public static bool spawnedStar = false;
        bool resetLevel = false;
        int starAttempCount = 0;
        public static Vector2 starPos;

        public static int windowHeight, windowWidth;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;


            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);


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

        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
#if XBOX
            foreach (Microsoft.Xna.Framework.Graphics.DisplayMode displayMode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                if (displayMode.Width == 1920 || displayMode.Width == 1280)
                {
                    e.GraphicsDeviceInformation.PresentationParameters.BackBufferFormat = displayMode.Format;
                    e.GraphicsDeviceInformation.PresentationParameters.BackBufferHeight = displayMode.Height;
                    e.GraphicsDeviceInformation.PresentationParameters.BackBufferWidth = displayMode.Width;
                }
            }
#endif
        }


        Texture2D BGTexture;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Segoi = Content.Load<SpriteFont>("SpriteFont1");
            SegoiSmall = Content.Load<SpriteFont>("SpriteFont2");
            ForegroundBatch = new SpriteBatch(GraphicsDevice); 

            windowWidth = Window.ClientBounds.Width;
            windowHeight = Window.ClientBounds.Height - 37;
            MyCircle = new Player(Content, spriteBatch);
            checkTime = DateTime.Now.Second;

            BGTexture = Content.Load<Texture2D>("bhtex");

            currentLevel = new Level(@"Content/MAP.txt");
            Random random = new Random(DateTime.Now.Millisecond);

            Random random2 = new Random(DateTime.Now.Millisecond);



            for (int i = 0; i < currentLevel.Grid.Count; i++)
            {
                for (int j = 0; j < Level.Width; j++)
                {
                    if (currentLevel.Grid[i][j])
                    {
                        byte r = (byte)random2.Next(0, 255);
                        byte g = (byte)random2.Next(0, 255);
                        byte b = (byte)random2.Next(0, 255);

                        Color tempCol;
                        tempCol = new Color(r, g, b);
                        Random random3 = new Random(DateTime.Now.Millisecond);
                        int temp = random.Next(0, 100);
                        surfaces.Add(new Surface(Content, spriteBatch, 43, 42, j * 43, i * 42, j, i, tempCol, temp, false));
                        if (i == currentLevel.Grid.Count - 1)
                        {
                            for (int k = 1; k < 4; k++)
                            {
                                surfaces.Add(new Surface(Content, spriteBatch, 43, 42, j * 43, (i + k) * 42, j, i, tempCol, temp, true));
                            }
                        }
                    }
                    else
                    {

                        if (random.Next(0, 100) < 30 && i != currentLevel.Grid.Count - 1 && i > 0)
                        {
                            if (currentLevel.Grid[i][j] == false && currentLevel.Grid[i + 1][j] == true)
                            {
                                items.Add(new Item(Content, spriteBatch, j * 43 + (43 / 4), i * 42 + 12, "cherry"));
                            }
                        }
                    }
                }

            }
            int starX = (random.Next(2, Level.Width - 1));
            while (!spawnedStar)
            {
                int randHeight = random.Next(1, Level.Height - 1);
                if (currentLevel.Grid[randHeight][starX] == false && currentLevel.Grid[randHeight + 1][starX] == true)
                {
                    items.Add(new Item(Content, spriteBatch, starX * 43 + (43 / 4), (randHeight) * 42 + 12, "star"));
                    starPos.X = starX * 43 + (43 / 4);
                    starPos.Y = (randHeight) * 42 + 12;
                    spawnedStar = true;
                    //break;
                }
                else
                {
                    starX = random.Next(2, Level.Width - 1);
                }

            }

            MyCircle = new Player(Content, spriteBatch);
            MyCircle2 = new Player2(Content, spriteBatch);
            MyCircle3 = new Player3(Content, spriteBatch);
            MyCircle4 = new Player4(Content, spriteBatch);



            //items.Add(new Item(Content, spriteBatch, starX * 43 + (43 / 4), 8 * 42 + 12, "star"));









            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Exit();
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



            GamePadState currentState = GamePad.GetState(PlayerIndex.One);
            // Process input only if connected.
            if (currentState.IsConnected)
            {
                // Increase vibration if the player is tapping the A button.
                // Subtract vibration otherwise, even if the player holds down A
                if (currentState.Buttons.Y == ButtonState.Pressed && resetLevel == false)
                {
                    camX = camY = 0;
                    nextLevel();
                    resetLevel = true;
                }
                else if (currentState.Buttons.Y != ButtonState.Pressed)
                {
                    resetLevel = false;
                }
            }

            // TODO: Add your update logic here
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyUp(Keys.R))
            {
                resetLevel = false;
            }
            if (keyState.IsKeyDown(Keys.R) && resetLevel == false)
            {
                camX = camY = 0;
                nextLevel();
                resetLevel = true;
            }


            

            if (keyState.IsKeyDown(Keys.Q))
            {
                UnloadContent();
            }

            if (keyState.IsKeyDown(Keys.Left))
            {
                MyCircle.keyPressed(Keys.Left);
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                MyCircle.keyPressed(Keys.Right);
            }
            if (keyState.IsKeyDown(Keys.Up))
            {
                MyCircle.keyPressed(Keys.Up);
            }
            if (keyState.IsKeyDown(Keys.R))
            {
                //this.Run();
                //Initialize();
            }


            /*if (keyState.IsKeyDown(Keys.Down))
            {
                MyCircle.keyPressed(Keys.Down);
            }*/


            if (MyCircle.starStatus() || MyCircle2.starStatus() || MyCircle3.starStatus() || MyCircle4.starStatus())
            {
                nextLevel();
            }

            MyCircle.update();
            MyCircle2.update();
            MyCircle3.update();
            MyCircle4.update();


            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            /*Random random2 = new Random(DateTime.Now.Millisecond);

            byte r = (byte)random2.Next(0, 255);
            byte g = (byte)random2.Next(0, 255);
            byte b = (byte)random2.Next(0, 255);

            Color tempCol;
            tempCol = new Color(r, g, b); 

            GraphicsDevice.Clear(tempCol);*/

            GraphicsDevice.Clear(bgColor);
            String Score;

            


            if (gameOver == false)
            {

                //Vector2 averagePos;
                //averagePos.X = (MyCircle.Pos.X + MyCircle2.Pos.X) / 2;
                //averagePos.Y = (MyCircle.Pos.Y + MyCircle2.Pos.Y) / 2;
                // TODO: Add your drawing code here

                /*if (averagePos.Y > windowHeight / 2)
                {
                    camY = 0;
                }
                else
                {
                    camY = -averagePos.Y + windowHeight / 2.0f;
                }
                if (averagePos.X < windowWidth / 2 || Level.levelType == 1)
                {
                    camX = 0;
                }
                else
                {
                    camX = -averagePos.X + windowWidth / 2.0f;

                }*/

                if (MyCircle.Pos.Y > windowHeight / 2)
                {
                    camY = 0;
                }
                else
                {
                        camY = -MyCircle.Pos.Y + windowHeight / 2.0f;
                }
                if ((MyCircle.Pos.X < windowWidth / 2 || Level.levelType == 1))
                {
                    camX = 0;
                }
                else
                {
                        camX = -MyCircle.Pos.X + windowWidth / 2.0f;
                }

                /*if (MyCircle.Pos.X - MyCircle2.Pos.X > windowWidth)
                {
                    MyCircle2.Pos.X = MyCircle.Pos.X - windowWidth / 2 +50;
                    for (int i = Game1.surfaces.Count - 1; i >= 0; i--)
                    {
                        while (Game1.surfaces[i].CheckCollision(MyCircle2) != 0)
                        {
                            MyCircle2.Pos.X += 4;
                        }
                    }
                }

                if (MyCircle2.Pos.X - MyCircle.Pos.X > windowWidth)
                {
                    MyCircle.Pos.X = MyCircle2.Pos.X - windowWidth / 2 + 50;
                    for (int i = Game1.surfaces.Count - 1; i >= 0; i--)
                    {
                        while (Game1.surfaces[i].CheckCollision(MyCircle) != 0)
                        {
                            MyCircle.Pos.X += 4;
                        }
                    }
                }*/
                

                Vector3 transVector = new Vector3(camX, camY - 10 * currentLevel.Grid.Count, 0.0f);
                float distance = (float)Math.Sqrt((Math.Pow((MyCircle.Pos.Y - MyCircle2.Pos.Y), 2) + Math.Pow((MyCircle.Pos.X - MyCircle2.Pos.X), 2)));
                Console.WriteLine(distance);
                Matrix SpriteScale = Matrix.CreateScale(graphics.GraphicsDevice.Viewport.Width / 800f, graphics.GraphicsDevice.Viewport.Width / 800f, 1);
                //Matrix SpriteScale = Matrix.CreateScale(distance, distance, 1);
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, SpriteScale);

                spriteBatch.Draw(BGTexture, new Vector2(0, 0), Color.White);
                

                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, Matrix.CreateTranslation(transVector) * SpriteScale);
                //MyLevel1.draw()

                for (int i = 0; i < surfaces.Count; i++)
                {
                    surfaces[i].draw();
                }

                for (int i = 0; i < items.Count; i++)
                {
                    items[i].spawn();
                }

                MyCircle.draw();
                MyCircle2.draw();
                MyCircle3.draw();
                MyCircle4.draw();


                spriteBatch.End();
                
                ForegroundBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, SpriteScale);
                Score = score.ToString();
                Vector2 FontOrigin = Segoi.MeasureString("Score: " + Score) / 2;
                Vector2 fpos = new Vector2(130, 50);

                Color temp = new Color(12, 179, 238);
                ForegroundBatch.DrawString(Segoi, "Dan: " + MyCircle.Score, fpos, temp,
                    0f, FontOrigin, .5f, SpriteEffects.None, 0.5f);

                fpos = new Vector2(670, 50);

                ForegroundBatch.DrawString(Segoi, "Studly: " + MyCircle2.Score, fpos, temp,
                    0f, FontOrigin, .5f, SpriteEffects.None, 0.5f);

                fpos = new Vector2(130, 420);


                ForegroundBatch.DrawString(Segoi, "Luke: " + MyCircle3.Score, fpos, temp,
                    0f, FontOrigin, .5f, SpriteEffects.None, 0.5f);

                fpos = new Vector2(670, 420);


                ForegroundBatch.DrawString(Segoi, "Griff: " + MyCircle4.Score, fpos, temp,
                    0f, FontOrigin, .5f, SpriteEffects.None, 0.5f);
                ForegroundBatch.End();


            }

            base.Draw(gameTime);
        }
        
        


        protected void nextLevel()
        {
            surfaces.Clear();
            items.Clear();
            spawnedStar = false;
            bgColor = Color.SkyBlue;

            currentLevel = new Level(@"Content/MAP.txt");
            Random random = new Random(DateTime.Now.Millisecond);

            Random random2 = new Random(DateTime.Now.Millisecond);



            for (int i = 0; i < currentLevel.Grid.Count; i++)
            {
                for (int j = 0; j < Level.Width; j++)
                {
                    if (currentLevel.Grid[i][j])
                    {
                        byte r = (byte)random2.Next(0, 255);
                        byte g = (byte)random2.Next(0, 255);
                        byte b = (byte)random2.Next(0, 255);

                        Color tempCol;
                        tempCol = new Color(r, g, b);
                        Random random3 = new Random(DateTime.Now.Millisecond);
                        int temp = random.Next(0, 100);
                        surfaces.Add(new Surface(Content, spriteBatch, 43, 42, j * 43, i * 42, j, i, tempCol, temp, false));
                        if (i == currentLevel.Grid.Count - 1)
                        {
                            for (int k = 1; k < 4; k++)
                            {
                                surfaces.Add(new Surface(Content, spriteBatch, 43, 42, j * 43, (i + k) * 42, j, i, tempCol, temp, true));
                            }
                        }
                    }
                    else
                    {

                        if (random.Next(0, 100) < 30 && i != currentLevel.Grid.Count - 1 && i > 0)
                        {
                            if (currentLevel.Grid[i][j] == false && currentLevel.Grid[i + 1][j] == true)
                            {
                                items.Add(new Item(Content, spriteBatch, j * 43 + (43 / 4), i * 42 + 12, "cherry"));
                            }
                        }
                    }
                }

            }
            int starX = (random.Next(2, Level.Width - 1));
            while (!spawnedStar)
            {
                if (starAttempCount > 100)
                {
                    starAttempCount = 0;
                    break;
                }
                int randHeight = random.Next(1, Level.Height - 1);
                if (currentLevel.Grid[randHeight][starX] == false && currentLevel.Grid[randHeight + 1][starX] == true)
                {
                    items.Add(new Item(Content, spriteBatch, starX * 43 + (43 / 4), (randHeight) * 42 + 12, "star"));
                    starPos.X = starX * 43 + (43 / 4);
                    starPos.Y = (randHeight) * 42 + 12;
                    spawnedStar = true;
                    //break;
                }
                else
                {
                    starX = random.Next(2, Level.Width - 1);
                }

            }
            score1 = MyCircle.Score;
            score2 = MyCircle2.Score;
            score3 = MyCircle3.Score;
            score4 = MyCircle4.Score;
            MyCircle = new Player(Content, spriteBatch);
            MyCircle2 = new Player2(Content, spriteBatch);
            MyCircle3 = new Player3(Content, spriteBatch);
            MyCircle4 = new Player4(Content, spriteBatch);
            MyCircle.Score = score1;
            MyCircle2.Score = score2;
            MyCircle3.Score = score3;
            MyCircle4.Score = score4;

            if (!spawnedStar)
            {
                nextLevel();
            }
        }
    }
}
