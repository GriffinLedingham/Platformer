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
    public class Circle
    {int currentFrame = 0;
        public Vector2 Pos;
        public static Vector2 Velocity;
        public float frictionVal = 0.5f;
        public SpriteBatch spriteBatch;
        public Texture2D SpriteTexture;
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

        public Vector2 Center()
        {
            return (this.Pos - new Vector2(this.SpriteTexture.Width / 2.0f));
        }

        public void update()
        {
            List<Surface> surfacesUnder = new List<Surface>();
            CollisionType collision = CollisionType.none;
            Surface collidedSurface = null;
            for (int i = 0; i < Game1.surfaces.Count; i++)
            {

                if (Game1.surfaces[i].CheckCollision(this)[2] == CollisionType.corner)
                {

                    this.Pos.Y = Game1.surfaces[i].Pos.Y - this.SpriteTexture.Height;
                    Velocity.Y = 0;
                    Yacceleration = 0;

                    jumping = false;
                }

                else if ((Game1.surfaces[i].CheckCollision(this)[1] == CollisionType.left || Game1.surfaces[i].CheckCollision(this)[1] == CollisionType.right) && Game1.surfaces[i].CheckCollision(this)[0] != CollisionType.bottom)
                {
                    collision = Game1.surfaces[i].CheckCollision(this)[1];
                    collidedSurface = Game1.surfaces[i];
                }

                if (Game1.surfaces[i].Pos.X <= this.Pos.X + SpriteTexture.Width / 2.0f && Game1.surfaces[i].Pos.X + Game1.surfaces[i].SpriteTexture.Width >= this.Pos.X + SpriteTexture.Width / 2.0f)
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
                    if (surf.Pos.Y >= this.Pos.Y + this.SpriteTexture.Height)
                        closest = surf;
                }
                else if (surf.Pos.Y >= this.Pos.Y + this.SpriteTexture.Height && surf.Pos.Y <= closest.Pos.Y)
                {
                    closest = surf;
                }

            }
            if (closest == null) jumping = true;

            if (jumping)
            {
                if (closest == null || (Pos.Y + SpriteTexture.Height <= closest.Pos.Y))
                {
                    Velocity.Y -= Yacceleration;
                    Yacceleration -= 0.1f;
                }
                else if (closest != null)
                {
                    Pos.Y = closest.Pos.Y - SpriteTexture.Height;//Game1.windowHeight - SpriteTexture.Height;   
                    Velocity.Y = 0;
                    Yacceleration = 0;
                    jumping = false;
                }
            }

            if (collision == CollisionType.none)
            {
                Pos.X += Velocity.X;
            }
            else
            {
                Velocity.X *= 0;
                this.Pos.X = collision == CollisionType.left ? collidedSurface.Pos.X - this.SpriteTexture.Width - 3 : collidedSurface.Pos.X + collidedSurface.SpriteTexture.Width + 3;
            }

            Pos.Y += Velocity.Y;

            if (closest != null && Pos.Y + this.SpriteTexture.Height > closest.Pos.Y)
            {
                this.Pos.Y = closest.Pos.Y - SpriteTexture.Height;
                jumping = false;
                Velocity.Y = 0;
                Yacceleration = 0;
            }
            else
            {
                jumping = true;
            }

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
            //spriteBatch.Begin();
            spriteBatch.Draw(SpriteTexture, Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            //spriteBatch.End();
        }
    

    }
}
