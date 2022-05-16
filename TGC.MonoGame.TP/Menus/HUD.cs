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
            Vector3 playerRoundPosition;
            Vector3 playerPosition = Player.Position;
            playerRoundPosition = new Vector3(MathF.Round(playerPosition.X, 0), MathF.Round(playerPosition.Y, 0), MathF.Round(playerPosition.Z, 0));

            var W = graphicsDevice.Viewport.Width;
            var H = graphicsDevice.Viewport.Height;
            SpriteBatch.DrawString(SpriteFont, playerRoundPosition.ToString(), new Vector2(W-300, H -50), Color.Gold);

            this.SpriteBatch.DrawString(this.SpriteFont, "Coins: "+Player.totalCoins.ToString(), new Vector2(W - 780, 35), Color.ForestGreen);
            this.SpriteBatch.DrawString(this.SpriteFont, "Lifes: " + Player.lifes.ToString(), new Vector2(W - 780, 10), Color.Red);
            if(Player.lifesZero)
            {
                this.SpriteBatch.DrawString(this.SpriteFont, "Press R to Restart", new Vector2(W - 500, H - 280), Color.Red);
            }
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
