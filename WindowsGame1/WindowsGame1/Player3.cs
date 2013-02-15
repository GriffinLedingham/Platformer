using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace WindowsGame1
{
    public class Player3
    {
        int currentFrame = 0;
        public Vector2 Pos;
        public Vector2 Velocity;
        public SpriteBatch spriteBatch;
        public Texture2D SpriteTexture;
        bool jumping = true;
        float Yacceleration = -0.3f;
        public ContentManager Content;
        Vector2 oldPos;
        int frameSkip = 0;
        bool stillFrame = true;
        public bool horizDown = false;
        public bool starHit = false;
        bool falling = true;
        float lastVel = 0;
        bool wasFlip = false;
        public float distance = 0;
        public int Score = 0;


        public Player3(ContentManager Content, SpriteBatch spriteBatch)
        {
            this.Content = Content;
            this.spriteBatch = spriteBatch;
            SpriteTexture = Content.Load<Texture2D>("player3f1");
            Pos.Y = Game1.windowHeight - 120;
            jumping = true;
            Pos.X = 49;
            oldPos = Pos;

            for (int i = Game1.surfaces.Count - 1; i >= 0; i--)
            {
                while (Game1.surfaces[i].CheckCollision(this) != 0)
                {
                    Pos.Y -= 4;
                }
            }
        }

        public void animate()
        {
            currentFrame++;
        }

        GamePadState previousGamePadState;

        public void update()
        {
            distance = (float)Math.Sqrt((Math.Pow((Pos.Y - Game1.starPos.Y), 2) + Math.Pow((Pos.X - Game1.starPos.X), 2)));
            if (Velocity.X == 0.0f)
            {
                frameSkip = 0;
            }
            else
            {
                frameSkip += 1;
            }

            for (int i = 0; i < Game1.items.Count; i++)
            {
                if (CheckCollision(this, Game1.items[i]))
                {
                    if (Game1.items[i].type == "cherry")
                    {
                        if (Game1.items[i].existing)
                        {
                            Score += 10;
                        }
                        Game1.items[i].existing = false;
                        
                    }
                    else if (Game1.items[i].type == "star")
                    {
                        Score += 50;
                        Game1.items[i].existing = false;
                        Game1.bgColor = Color.Blue;
                        starHit = true;

                    }
                }

            }

            bool noHit = true;
            for (int i = 0; i < Game1.surfaces.Count; i++)
            {
                if (Game1.surfaces[i].CheckCollision(this) != 0)
                {
                    noHit = false;
                }

            }
            if (noHit)
            {
                jumping = true;
            }

            Pos.X += Velocity.X;

            for (int i = 0; i < Game1.surfaces.Count; i++)
            {
                if (Game1.surfaces[i].CheckCollision(this) != 0)
                {
                    Pos.X -= Velocity.X;
                    Velocity.X = 0;
                    break;
                }
            }

            float sign;
            if (Velocity.X > 0)
            {
                sign = 1;
            }
            else if (Velocity.X < 0)
            {
                sign = -1;
            }
            else
            {
                sign = 0;
            }
            if (Velocity.X != lastVel)
            {
                lastVel = Velocity.X;
            }
            Velocity.X -= .1f * sign;

            if (Velocity.X > -.1 && Velocity.X < .1)
            {
                Velocity.X = 0;
            }

            if (Velocity.Y > 3)
            {
                Velocity.Y = 3.0f;
            }

            if (jumping == true)
            {
                Velocity.Y -= Yacceleration;
                    Yacceleration -= 0.09f;
            }

            Pos.Y += Velocity.Y;

            for (int i = 0; i < Game1.surfaces.Count; i++)
            {
                if (Game1.surfaces[i].CheckCollision(this) != 0)
                {
                    Pos.Y -= Velocity.Y;


                    //Yacceleration = 0.0f;
                    if (Velocity.Y > 0)
                    {
                        while (Game1.surfaces[i].CheckCollision(this) == 0)
                        {
                            Pos.Y += Velocity.Y / Math.Abs(Velocity.Y);
                        }
                        Pos.Y -= Velocity.Y / Math.Abs(Velocity.Y);
                        Velocity.Y = 0;
                        Yacceleration = 0;
                        jumping = false;
                    }
                    else
                    {
                        Velocity.Y = -Velocity.Y / 2;
                    }

                    //jumping = false;
                    break;
                }
            }

            if (frameSkip == 8)
            {
                if (stillFrame)
                {
                    SpriteTexture = Content.Load<Texture2D>("player3f2");
                    stillFrame = false;
                }
                else
                {
                    SpriteTexture = Content.Load<Texture2D>("player3f1");
                    stillFrame = true;
                }
                frameSkip = 0;
            }

            GamePadState currentState = GamePad.GetState(PlayerIndex.Three);
            // Process input only if connected.
            if (currentState.IsConnected)
            {
                // Increase vibration if the player is tapping the A button.
                // Subtract vibration otherwise, even if the player holds down A
                if (currentState.Buttons.A == ButtonState.Pressed)
                {
                    if (jumping == false)
                    {
                        jumping = true;
                        Yacceleration = 1.0f;
                    }
                }
                if (currentState.ThumbSticks.Left.X < 0 && Velocity.X > -3.0f)
                {
                    Velocity.X -= 0.2f;
                }
                else if (currentState.ThumbSticks.Left.X > 0 && Velocity.X < 3.0f)
                {
                    Velocity.X += 0.2f;
                }

                // Update previous gamepad state.
                previousGamePadState = currentState;
            }

        }




        public void keyPressed(Keys pressed)
        {
            if (pressed == Keys.Left)
            {
                Velocity.X -= 0.2f;
            }
            if (pressed == Keys.Right)
            {
                Velocity.X += 0.2f;
            }


            if (Velocity.X < -3.0f)
            {
                Velocity.X = -3.0f;
            }
            if (Velocity.X > 3.0f)
            {
                Velocity.X = 3.0f;
            }


            else if (pressed == Keys.Up)
            {
                if (jumping == false)
                {
                    jumping = true;
                    Yacceleration = 1.0f;
                }
            }
        }

        public bool CheckCollision(Player3 s, Item i)
        {
            float leftA, leftB;
            float rightA, rightB;
            float topA, topB;
            float bottomA, bottomB;

            leftA = i.Pos.X;
            rightA = i.Pos.X + i.SpriteTexture.Width;
            topA = i.Pos.Y;
            bottomA = i.Pos.Y + i.SpriteTexture.Height;

            leftB = s.Pos.X;
            rightB = s.Pos.X + s.SpriteTexture.Width;
            topB = s.Pos.Y;
            bottomB = s.Pos.Y + s.SpriteTexture.Height;

            if (bottomA <= topB)
            {
                return false;
            }

            if (topA >= bottomB)
            {
                return false;
            }

            if (rightA <= leftB)
            {
                return false;
            }

            if (leftA >= rightB)
            {
                return false;
            }

            //If none of the sides from A are outside B
            return true;

        }

        public void draw()
        {
            //spriteBatch.Begin();
            if (Velocity.X > 0 || (Velocity.X == 0 && lastVel > 0))
            {

                spriteBatch.Draw(SpriteTexture, Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
                wasFlip = false;
            }
            else if (Velocity.X < 0 || (Velocity.X == 0 && lastVel < 0))
            {
                spriteBatch.Draw(SpriteTexture, Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 0.0f);
                wasFlip = true;
            }
            else
            {
                if (wasFlip)
                {
                    spriteBatch.Draw(SpriteTexture, Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 0.0f);

                }
                else
                {
                    spriteBatch.Draw(SpriteTexture, Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

                }
            }
            //spriteBatch.End();
        }

        public bool starStatus()
        {
            if (starHit)
            {
                starHit = false;
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
