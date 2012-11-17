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
        public SpriteBatch spriteBatch;
        public Texture2D SpriteTexture;
        public Rectangle pos;

        public Surface(ContentManager Content, SpriteBatch spriteBatch, int width, int height, int xStart, int yStart)
        {
            this.spriteBatch = spriteBatch;
            SpriteTexture = Content.Load<Texture2D>("groundTile");
            this.pos.Height = height;
            this.pos.Width = width;
            this.pos.X = xStart;
            this.pos.Y = yStart;
        }

        public void draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(SpriteTexture, pos, Color.White);
            spriteBatch.End();
        }
    }
}
