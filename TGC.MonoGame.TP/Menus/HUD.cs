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

            //var W = graphicsDevice.Viewport.Width;
            //var H = graphicsDevice.Viewport.Height;
            //var escala = 2f;
            //var size = SpriteFont.MeasureString("Holaa") * escala;

            SpriteBatch.DrawString(SpriteFont, playerRoundPosition.ToString(), new Vector2(graphicsDevice.Viewport.Width -300, graphicsDevice.Viewport.Height -50), Color.White);

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
