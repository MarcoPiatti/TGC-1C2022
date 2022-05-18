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
        private SpriteBatch SpriteBatch { get; set; }
        private SpriteFont SpriteFont { get; set; }
        private ContentManager ContentManager { get; set; }
        private bool godMode {get;set;}= false;
        //True para modo bola y false para modo dios
        public bool flag_menuSegunCamara { get; set; } = true;


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
            SpriteBatch.DrawString(SpriteFont, playerRoundPosition.ToString(), new Vector2(W - 300, H - 50), Color.Gold);

            if (flag_menuSegunCamara)
            {
                if (godMode)
                {
                    this.SpriteBatch.DrawString(this.SpriteFont, "Activar Modo God: G", new Vector2(W - 400, 25), Color.ForestGreen);
                }

                this.SpriteBatch.DrawString(this.SpriteFont, "Coins: " + Player.totalCoins.ToString(), new Vector2(W - 780, 35), Color.ForestGreen);
                this.SpriteBatch.DrawString(this.SpriteFont, "Lifes: " + Player.lifes.ToString(), new Vector2(W - 780, 10), Color.Red);
                if (!Player.currentPowerUp_1.Equals("N/A"))
                {
                    this.SpriteBatch.DrawString(this.SpriteFont, "++" + Player.currentPowerUp_1.ToString(), new Vector2(W - 780, 52), Color.IndianRed);
                }
                if (!Player.currentPowerUp_2.Equals("N/A"))
                {
                    this.SpriteBatch.DrawString(this.SpriteFont, "++" + Player.currentPowerUp_2.ToString(), new Vector2(W - 780, 77), Color.AliceBlue);
                }
                if (!Player.currentPowerUp_3.Equals("N/A"))
                {
                    this.SpriteBatch.DrawString(this.SpriteFont, "++" + Player.currentPowerUp_3.ToString(), new Vector2(W - 780, 99), Color.Red);
                }

                if (Player.lifesZero)
                {
                    this.SpriteBatch.DrawString(this.SpriteFont, "Press R to Restart", new Vector2(W - 500, H - 280), Color.Red);
                }
            }
            else
            {
                this.SpriteBatch.DrawString(this.SpriteFont, "Desactivar Modo God: G", new Vector2(W - 780, 35), Color.ForestGreen);
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
        public void ActivarGod()
        {
            godMode = true;
        }
        public void DesactivarGod()
        {
            godMode = false;
        }

        public bool godActivado() { return godMode; }

        public bool menuCambiarCamara()
        {
            if (flag_menuSegunCamara)
            {
                flag_menuSegunCamara = false;
            }
            else
            {
                flag_menuSegunCamara = true;
            }
            return flag_menuSegunCamara;
        }
    }
}
