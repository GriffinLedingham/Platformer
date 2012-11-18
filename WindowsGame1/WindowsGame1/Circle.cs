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
        public float scale = 1.0f;
        public ContentManager Content;
        public float scaledWidth, scaledHeight;
        
        private int bounced = 0;

        public Circle(ContentManager Content, SpriteBatch spriteBatch)
        {
            this.Content = Content;
            this.spriteBatch = spriteBatch;
            SpriteTexture = Content.Load<Texture2D>("ballsmall");
            Pos.Y = Game1.windowHeight - 80;
            jumping = true;
            scaledHeight = SpriteTexture.Height * scale;
            scaledWidth = SpriteTexture.Width * scale;
        }

        public void animate()
        {
            currentFrame++;
        }

        public Vector2 Center()
        {
            return (this.Pos - new Vector2(this.scaledWidth / 2.0f));
        }

        public void update()
        {
            
            scaledHeight = SpriteTexture.Height * scale;
            scaledWidth = SpriteTexture.Width * scale;
            for (int i = 0; i < Game1.items.Count; i++)
            {
                

                if (Game1.items[i].Pos.X >= this.Pos.X - (this.scaledWidth - this.SpriteTexture.Width)/2 && Game1.items[i].Pos.X + Game1.items[i].SpriteTexture.Width <= this.Pos.X + (this.scaledWidth - this.SpriteTexture.Width)/2 + this.scaledWidth
                    && Game1.items[i].Pos.Y >= this.Pos.Y - (this.scaledHeight - this.SpriteTexture.Height)/2 && Game1.items[i].Pos.Y + Game1.items[i].SpriteTexture.Height <= this.Pos.Y + (this.scaledHeight - this.SpriteTexture.Height)/2 + this.scaledHeight && Game1.items[i].existing)
                {
                    Game1.items[i].existing = false;
                    scale += 0.1f;
                }

            }

            List<Surface> surfacesUnder = new List<Surface>();
            CollisionType collision = CollisionType.none;
            Surface collidedSurface = null;
            int topCollided = 0;
            bool collisionTop = false;
            for (int i = 0; i < Game1.surfaces.Count; i++)
            {

                if (Game1.surfaces[i].CheckCollision(this)[0] == CollisionType.bottom)
                {
                    int k = 0;
                }

                if (Game1.surfaces[i].CheckCollision(this)[0] == CollisionType.top)
                {
                    collision = CollisionType.top;
                    collisionTop = true;
                    topCollided = Game1.surfaces[i].Pos.Y + Game1.surfaces[i].SpriteTexture.Height;
                }

                if (Game1.surfaces[i].CheckCollision(this)[2] == CollisionType.topCorner)
                {

                    this.Pos.Y = Game1.surfaces[i].Pos.Y - this.scaledHeight;
                    Velocity.Y = 0;
                    Yacceleration = 0;

                    collision = CollisionType.topCorner;

                    jumping = false;
                }

                else if ((Game1.surfaces[i].CheckCollision(this)[1] == CollisionType.left || Game1.surfaces[i].CheckCollision(this)[1] == CollisionType.right) && Game1.surfaces[i].CheckCollision(this)[0] != CollisionType.bottom)
                {
                    collision = Game1.surfaces[i].CheckCollision(this)[1];
                    collidedSurface = Game1.surfaces[i];
                }

                if (Game1.surfaces[i].Pos.X <= this.Pos.X + scaledWidth / 2.0f && Game1.surfaces[i].Pos.X + Game1.surfaces[i].SpriteTexture.Width >= this.Pos.X + scaledWidth / 2.0f)
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
                    if (surf.Pos.Y >= this.Pos.Y - (this.scaledHeight - this.SpriteTexture.Height) + this.scaledHeight)
                        closest = surf;
                }
                else if (surf.Pos.Y >= this.Pos.Y - (this.scaledHeight - this.SpriteTexture.Height) + this.scaledHeight && surf.Pos.Y <= closest.Pos.Y)
                {
                    closest = surf;
                }

            }
            if (closest == null) jumping = true;
            else if (closest.Pos.Y > this.Pos.Y - (this.scaledHeight - this.SpriteTexture.Height) + scaledHeight) jumping = true;
            if (jumping)
            {
                if (closest == null || (this.Pos.Y - (this.scaledHeight - this.SpriteTexture.Height) + this.scaledHeight <= closest.Pos.Y))//(Pos.Y + scaledHeight <= closest.Pos.Y))
                {
                    Velocity.Y -= Yacceleration;
                    Yacceleration -= 0.1f;
                }
                else if (closest != null)
                {
                    Pos.Y = closest.Pos.Y - scaledHeight;//Game1.windowHeight - SpriteTexture.Height;   
                    Velocity.Y = 0;
                    Yacceleration = 0;
                    jumping = false;
                }
            }

            if (collisionTop)
            {
                Velocity.Y *= -1;
                this.Pos.Y = topCollided + 5;
            }

            

            if (collision == CollisionType.none)
            {
                Pos.X += Velocity.X;
                Pos.Y += Velocity.Y;
            }
            else if(collision == CollisionType.right || collision == CollisionType.left)
            {
                Velocity.X *= 0;
                if(!collisionTop)
                this.Pos.X = collision == CollisionType.left ? collidedSurface.Pos.X - this.scaledWidth - 3 : collidedSurface.Pos.X + collidedSurface.SpriteTexture.Width + 3;
            }

            if (closest != null && this.Pos.Y - (this.scaledHeight - this.SpriteTexture.Height) + this.scaledHeight > closest.Pos.Y)
            {
                this.Pos.Y = closest.Pos.Y - scaledHeight;
                jumping = false;
                Velocity.Y = 0;
                Yacceleration = 0;
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
            spriteBatch.Draw(SpriteTexture, Pos, null, Color.White, 0.0f, Vector2.Zero, this.scale, SpriteEffects.None, 0.0f);
            //spriteBatch.End();
        }
    

    }
}
