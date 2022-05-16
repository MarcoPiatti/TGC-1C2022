using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace TGC.MonoGame.TP.Menus
{
    public class HUD
    {
        public Vector2 windowSize;
        public Player Player { get; set; }
        private SpriteBatch SpriteBatch {get; set;}
        private SpriteFont SpriteFont { get; set; }
        private ContentManager ContentManager { get; set; }


        public HUD(SpriteFont SpriteFont, SpriteBatch SpriteBatch, ContentManager content, Player player) 
        {
            this.SpriteFont = SpriteFont;
            this.SpriteBatch = SpriteBatch;
            this.Player = player;
        }
        public void Draw(GraphicsDevice graphicsDevice, GameTime gameTime)
        {
            this.SpriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null);
            this.SpriteBatch.DrawString(this.SpriteFont, Player.totalCoins.ToString(), new Vector2(graphicsDevice.Viewport.Width - 400, 0), Color.White);
            this.SpriteBatch.End();
            //this.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend,
            //SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
            //
            //SpriteBatch.End();

        }
        public void Update(GraphicsDevice graphicsDevice, GameTime gameTime, KeyboardState keyboardState)
        {
            windowSize = new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        }


    }
}
