using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WindowsGame1
{
    public class Item
    {
        public Vector2 Pos;
        public Texture2D SpriteTexture;
        public SpriteBatch spriteBatch;
        public bool existing = true;


        public Item(ContentManager Content, SpriteBatch spriteBatch, int xPos, int yPos, string spriteName)
        {
            Pos.X = xPos;
            Pos.Y = yPos;
            SpriteTexture = Content.Load<Texture2D>(spriteName);
            this.spriteBatch = spriteBatch;

        }

        public void spawn()
        {
            if (existing)
            {

                spriteBatch.Draw(SpriteTexture, Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

            }
        }

    }
}
