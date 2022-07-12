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
        private Vector2 Life_position { get; set; }
        private Texture2D Life_texture { get; set; }
        private Vector2 Coin_position { get; set; }
        private Texture2D Coin_texture { get; set; }
        private Vector2 Speed_position { get; set; }
        private Texture2D Speed_texture { get; set; }
        private Vector2 Glide_position { get; set; }
        private Texture2D Glide_texture { get; set; }
        private float W;
        private float H;

        private SpriteBatch SpriteBatch { get; set; }
        private SpriteFont SpriteFont { get; set; }
        private ContentManager ContentManager { get; set; }
        private bool godMode {get;set;}= false;
        //True para modo bola y false para modo dios
        public bool flag_menuSegunCamara { get; set; } = true;
        public const string ContentFolderTextureSprites = "Sprites/";


        public HUD(SpriteFont SpriteFont, SpriteBatch SpriteBatch, ContentManager content, Player player, GraphicsDevice graphicsDevice)
        {
            this.SpriteFont = SpriteFont;
            this.SpriteBatch = SpriteBatch;
            this.Player = player;
            Life_texture = content.Load<Texture2D>(ContentFolderTextureSprites + "Life");
            Coin_texture = content.Load<Texture2D>(ContentFolderTextureSprites + "Coin");
            Speed_texture = content.Load<Texture2D>(ContentFolderTextureSprites + "Run");
            Glide_texture = content.Load<Texture2D>(ContentFolderTextureSprites + "Fly");
            W = graphicsDevice.Viewport.Width;
            H = graphicsDevice.Viewport.Height;
            SpriteBatch = new SpriteBatch(graphicsDevice);
            Life_position = new Vector2(W - 780, H-475);
            Coin_position = new Vector2(W - 784, H - 445);
            Speed_position = new Vector2(W - 780, H - 400);
            Glide_position = new Vector2(W - 784, H - 330);


        }
        public void Draw(GraphicsDevice graphicsDevice, GameTime gameTime)
        {

            this.SpriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null);
            Vector3 playerRoundPosition;
            Vector3 playerPosition = Player.Position;
            playerRoundPosition = new Vector3(MathF.Round(playerPosition.X, 0), MathF.Round(playerPosition.Y, 0), MathF.Round(playerPosition.Z, 0));


            //SpriteBatch.DrawString(SpriteFont, playerRoundPosition.ToString(), new Vector2(W - 300, H - 50), Color.Gold);

            if (flag_menuSegunCamara)
            {
                if (godMode)
                {
                    this.SpriteBatch.DrawString(this.SpriteFont, "Activar Modo God: G", new Vector2(W - 400, 25), Color.ForestGreen);
                }


             
                this.SpriteBatch.DrawString(this.SpriteFont, Player.totalCoins.ToString(), Coin_position + new Vector2(40, +7), Color.Cyan);
                SpriteBatch.Draw(Coin_texture, Coin_position, null, Color.White * 0.9f, 0, Vector2.Zero, new Vector2(0.15f, 0.15f), 0, 0);
                switch (Player.lifes.ToString())
                {
                    case "3":
                        SpriteBatch.Draw(Life_texture, Life_position, null, Color.White * 0.7f, 0, Vector2.Zero, new Vector2(0.1f, 0.1f), 0, 0);
                        SpriteBatch.Draw(Life_texture, Life_position + new Vector2(50, 0), null, Color.White * 0.7f, 0, Vector2.Zero, new Vector2(0.1f, 0.1f), 0, 0);
                        SpriteBatch.Draw(Life_texture, Life_position + new Vector2(100, 0), null, Color.White * 0.7f, 0, Vector2.Zero, new Vector2(0.1f, 0.1f), 0, 0);
                        break;
                    case "2":
                        SpriteBatch.Draw(Life_texture, Life_position, null, Color.White * 0.7f, 0, Vector2.Zero, new Vector2(0.1f, 0.1f), 0, 0);
                        SpriteBatch.Draw(Life_texture, Life_position + new Vector2(50, 0), null, Color.White * 0.7f, 0, Vector2.Zero, new Vector2(0.1f, 0.1f), 0, 0);
                        break;
                    case "1":
                        SpriteBatch.Draw(Life_texture, Life_position, null, Color.White * 0.7f, 0, Vector2.Zero, new Vector2(0.1f, 0.1f), 0, 0);
                        break;
                    default:
                        // code block
                        break;

                }
                //this.SpriteBatch.DrawString(this.SpriteFont, "Lifes: " + Player.lifes.ToString(), new Vector2(W - 780, 10), Color.Red);
                if (!Player.currentPowerUp_1.Equals("N/A"))
                {
                    SpriteBatch.Draw(Speed_texture, Speed_position, null, Color.White * 0.9f, 0, Vector2.Zero, new Vector2(0.09f, 0.09f), 0, 0);
                    //this.SpriteBatch.DrawString(this.SpriteFont, "++" + Player.currentPowerUp_1.ToString(), new Vector2(W - 780, H-400), Color.IndianRed);
                }
                if (!Player.currentPowerUp_2.Equals("N/A"))
                {
                    SpriteBatch.Draw(Glide_texture, Glide_position, null, Color.White * 0.9f, 0, Vector2.Zero, new Vector2(0.13f, 0.13f), 0, 0);
                    //this.SpriteBatch.DrawString(this.SpriteFont, "++" + Player.currentPowerUp_2.ToString(), new Vector2(W - 780, H-378), Color.AliceBlue);
                }
                if (!Player.currentPowerUp_3.Equals("N/A"))
                {
                    this.SpriteBatch.DrawString(this.SpriteFont, "++" + Player.currentPowerUp_3.ToString(), new Vector2(W - 780, H - 356), Color.Red);
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
