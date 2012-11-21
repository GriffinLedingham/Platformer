using System;


using System.Collections.Generic;


using System.Linq;


using System.Text;


using Microsoft.Xna.Framework.Graphics;


using Microsoft.Xna.Framework;


using Microsoft.Xna.Framework.Content;


namespace WindowsGame1
{



    public enum CollisionType
    {

        top, bottom, left, right, none, topCorner, bottomCorner

    }



    public class Surface
    {



        public static float collisionBuffer = 10;


        public SpriteBatch spriteBatch;


        public Texture2D SpriteTexture;


        public Rectangle Pos;


        public int tileXPos;


        public Color tileColor;

        public int gridX, gridY;


        public Surface(ContentManager Content, SpriteBatch spriteBatch, int width, int height, int xStart, int yStart,int gridX,int gridY, Color tileColor, int random, bool filler)
        {



            this.spriteBatch = spriteBatch;
            if (!filler)
            {
                if (gridY == Game1.currentLevel.Grid.Count - 1)
                {
                    if (random < 49)
                    {
                        SpriteTexture = Content.Load<Texture2D>("groundTileNew");
                    }
                    else
                    {
                        SpriteTexture = Content.Load<Texture2D>("groundTileNewFlip");
                    }
                }
                else
                {
                    SpriteTexture = Content.Load<Texture2D>("Tile");
                }
            }
            else
            {
                    SpriteTexture = Content.Load<Texture2D>("dirtTile");
            }
            


            this.Pos.Height = height;


            this.Pos.Width = width;


            this.Pos.X = xStart;


            this.Pos.Y = yStart;


            this.tileColor = tileColor;

            this.gridX = gridX;
            this.gridY = gridY;

        }



        public Vector2 Center()
        {
            return (new Vector2(this.Pos.X, this.Pos.Y) - new Vector2(this.SpriteTexture.Width / 2.0f));
        }


        public void draw()
        {

            spriteBatch.Draw(SpriteTexture, Pos, Color.White);//tileColor);//Color.White);

        }


        public CollisionType[] CheckCollision(Circle s)
        {

            if (!CheckCollisionHelper(s)) return new CollisionType[] { CollisionType.none, CollisionType.none, CollisionType.none };

            else
            {
                CollisionType[] returnVal = new CollisionType[3];

                if (s.Pos.Y - (s.scaledHeight - s.SpriteTexture.Height) / 2 >= this.Pos.Y + SpriteTexture.Height - collisionBuffer) returnVal[0] = CollisionType.top;
                else if (s.Pos.Y - (s.scaledHeight - s.SpriteTexture.Height) / 2 + s.scaledHeight <= this.Pos.Y) returnVal[0] = CollisionType.bottom;
                else returnVal[0] = CollisionType.none;

                if (s.Pos.X - (s.scaledWidth - s.SpriteTexture.Width) / 2 + s.scaledWidth <= this.Pos.X) returnVal[1] = CollisionType.left;
                //else if (s.Pos.X - (s.scaledWidth - s.SpriteTexture.Width) / 2 >= this.Pos.X + this.SpriteTexture.Width - 6) returnVal[1] = CollisionType.right;
                else if (s.Pos.X + s.SpriteTexture.Width / 2.0f > this.Pos.X + this.SpriteTexture.Width / 2.0f + this.SpriteTexture.Width / 4.0f)
                {
                    returnVal[1] = CollisionType.right;
                }

                else returnVal[1] = CollisionType.none;


                if (s.Pos.X - (s.scaledWidth - s.SpriteTexture.Width) / 2 + s.scaledWidth <= this.Pos.X &&
                    s.Pos.Y - (s.scaledHeight - s.SpriteTexture.Height) / 2 + s.scaledHeight <= this.Pos.Y ||
                s.Pos.X - (s.scaledWidth - s.SpriteTexture.Width) / 2 >= this.Pos.X + SpriteTexture.Width &&
                    s.Pos.Y - (s.scaledHeight - s.SpriteTexture.Height) / 2 + s.scaledHeight <= this.Pos.Y) returnVal[2] = CollisionType.topCorner;

                else returnVal[2] = CollisionType.none;

                return returnVal;

            }

        }



        private bool CheckCollisionHelper(Circle s)
        {



            float a, dx, dy;
            Vector2 p1 = s.Pos + new Vector2(this.SpriteTexture.Width / 2.0f);
            Vector2 p2 = new Vector2(this.Pos.X, this.Pos.Y) + new Vector2(this.SpriteTexture.Width / 2.0f);
            a = (this.SpriteTexture.Height / 2.0f + s.scaledHeight / 2.0f) * (this.SpriteTexture.Width / 2.0f + s.scaledHeight / 2.0f);
            dx = (float)(p1.X - p2.X);
            dy = (float)(p1.Y - p2.Y);

            //CIRCLE INSIDE SQUARE or viceversa
            if (a > (dx * dx) + (dy * dy) || Intersect(s))
            {
                return true;
            }

            return false;

        }



        private bool Intersect(Circle s)
        {

            Vector2 circleDistance;

            //THIS IS A SQUARE
            circleDistance.X = Math.Abs(s.Pos.X - (s.scaledWidth - s.SpriteTexture.Width) / 2.0f - this.Pos.X);
            circleDistance.Y = Math.Abs(s.Pos.Y - (s.scaledHeight - s.SpriteTexture.Height) / 2.0f - this.Pos.Y);
            if (circleDistance.X > (this.SpriteTexture.Height / 2.0f + s.scaledWidth / 2.0f)) { return false; }
            if (circleDistance.Y > (this.SpriteTexture.Height / 2.0f + s.scaledWidth / 2.0f)) { return false; }
            if (circleDistance.X <= (this.SpriteTexture.Width / 2.0f)) { return true; }

            if (circleDistance.Y <= (this.SpriteTexture.Height / 2.0f)) { return true; }
            float cornerDistance_sq = (circleDistance.X - this.SpriteTexture.Width / 2.0f) * (circleDistance.X - this.SpriteTexture.Width / 2.0f) +
               (circleDistance.Y - this.SpriteTexture.Height / 2.0f) * (circleDistance.Y - this.SpriteTexture.Height / 2.0f);


            return (cornerDistance_sq <= ((s.scaledWidth / 2.0f) * (s.scaledWidth / 2.0f)));

        }



    }

}

