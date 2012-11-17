using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    class Circle
    {
        int currentFrame = 0;
        public Vector2 Pos;
        public Vector2 Velocity;
        public float frictionVal = 0.5f;
        public SpriteBatch spriteBatch;
        private Texture2D SpriteTexture;
        bool jumping = false;
        float Yacceleration = 0.0f;

        private int bounced = 0;

        public Circle(ContentManager Content, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            SpriteTexture = Content.Load<Texture2D>("ballsmall");
            Pos.Y = Game1.windowHeight - 38;

        }

        public void animate()
        {
            currentFrame++;
        }

        public void update()
        {


            List<Surface> surfacesUnder = new List<Surface>();

            for (int i = 0; i < Game1.surfaces.Count; i++)
            {
                if (Game1.surfaces[i].pos.X <= this.Pos.X + SpriteTexture.Width / 2.0f && Game1.surfaces[i].pos.X + Game1.surfaces[i].SpriteTexture.Width >= this.Pos.X + SpriteTexture.Width / 2.0f)
                {
                    surfacesUnder.Add(Game1.surfaces[i]);
                }
            }

            if (surfacesUnder.Count == 0 && jumping == false)
            {
                //FALL
                Velocity.Y -= Yacceleration;
                Yacceleration -= 0.1f;

            }

            Surface closest = null;
            foreach (Surface surf in surfacesUnder)
            {
                if (closest == null)
                {
                    //if(surf.pos.Y >= this.Pos.Y + this.SpriteTextured.Height)
                    if (surf.pos.Y >= this.Pos.Y + this.SpriteTexture.Height)
                        closest = surf;
                }
                else if (surf.pos.Y >= this.Pos.Y + this.SpriteTexture.Height && surf.pos.Y <= closest.pos.Y)
                {
                    closest = surf;
                }

            }

            if (closest == null) jumping = true;


            if (jumping)
            {
                if (closest == null || Pos.Y + SpriteTexture.Height <= closest.pos.Y)
                {
                    Velocity.Y -= Yacceleration;
                    Yacceleration -= 0.1f;
                }
                else if (closest != null)
                {
                    //if (closest != null)
                    //{
                        //if (closest.pos.Y > Pos.Y + SpriteTexture.Height)
                        //{
                            Pos.Y = closest.pos.Y - SpriteTexture.Height;//Game1.windowHeight - SpriteTexture.Height;
                        //}
                    //}
                    Velocity.Y = 0;
                    Yacceleration = 0;
                    jumping = false;
                }

            }

            Pos.X += Velocity.X;
            Pos.Y += Velocity.Y;
        }

        public void keyPressed(Keys pressed)
        {

            if (pressed == Keys.Left)
            {
                Velocity.X -= 0.1f;
            }
            else if (pressed == Keys.Right)
            {
                Velocity.X += 0.1f;
            }

            if (Velocity.X < -5.0f)
            {
                Velocity.X = -5.0f;
            }
            if (Velocity.X > 5.0f)
            {
                Velocity.X = 5.0f;
            }

            else if (pressed == Keys.Up)
            {
                if (jumping == false)
                {
                    jumping = true;
                    Yacceleration = 1.0f;
                }
                //Velocity.Y -= f;
            }
            /* else if(pressed == Keys.Down)
             {
                 Velocity.Y += 0.1f;
             }*/
        }

        public void gravity()
        {
            /*if (Velocity.X > 0)
            {
                if (Velocity.X - frictionVal < 0)
                {
                    Velocity.X = 0;
                }
                else
                {
                    Velocity.X -= frictionVal;
                }
            }
            else if (Velocity.X < 0)
            {
                if (Velocity.X + frictionVal > 0)
                {
                    Velocity.X = 0;
                }
                else
                {
                    Velocity.X += frictionVal;
                }
            }*/
            if (Velocity.Y > 0)
            {

                Velocity.Y -= frictionVal;

            }
            else if (Velocity.Y < 0)
            {
                Velocity.Y += frictionVal;

            }
        }

        public void draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(SpriteTexture, Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.End();
        }
    }
}