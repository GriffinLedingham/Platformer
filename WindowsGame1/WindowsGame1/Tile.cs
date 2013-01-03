using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace WindowsGame1
{
    
    public class Surface
    {
        public static float collisionBuffer = 10;
        public SpriteBatch spriteBatch;
        public Texture2D SpriteTexture;
        public Rectangle Pos;
        public int tileXPos;
        public Color tileColor;
        public int gridX, gridY;

        public Surface(ContentManager Content, SpriteBatch spriteBatch, int width, int height, int xStart, int yStart, int gridX, int gridY, Color tileColor, int random, bool filler)
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



        public void draw()
        {

            spriteBatch.Draw(SpriteTexture, Pos, Color.White);
        }

        public int CheckCollision(Player s)
        {

            float leftA, leftB;
            float rightA, rightB;
            float topA, topB;
            float bottomA, bottomB;

            leftA = Pos.X;
            rightA = Pos.X + Pos.Width;
            topA = Pos.Y;
            bottomA = Pos.Y + Pos.Height;

            leftB = s.Pos.X;
            rightB = s.Pos.X + s.SpriteTexture.Width;
            topB = s.Pos.Y;
            bottomB = s.Pos.Y + s.SpriteTexture.Height;

            if (bottomA <= topB)
            {
                return 0;
            }

            if (topA >= bottomB)
            {
                return 0;
            }

            if (rightA <= leftB)
            {
                return 0;
            }

            if (leftA >= rightB)
            {
                return 0;
            }

            return 1;

        }
    }

}

