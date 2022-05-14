using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Menus
{
    public class MainMenu : Menu
    {
        public Vector2 selector = Vector2.Zero;

        public int selectedPlayer = 0;

        public Player[] playerTypes;


        public MainMenu(SpriteFont SpriteFont, SpriteBatch SpriteBatch, Player[] playerTypes) : base(SpriteFont, SpriteBatch)
        {
            this.playerTypes = playerTypes;
        }

        public override void Update(GraphicsDevice graphicsDevice, GameTime gameTime, KeyboardState keyboardState)
        {
            base.Update(graphicsDevice, gameTime, keyboardState);
            float time = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);

            if (KeyCoolDown <= 0)
            {
                KeyUpdate(keyboardState);
                KeyCoolDown = 0.1f;
            }

            if (KeyCoolDown > 0) KeyCoolDown -= time;

            if (selector.Y > 3) selector.Y = 0;
            if (selector.Y < 0) selector.Y = 3;
        }

        public override void Draw(GraphicsDevice graphicsDevice)
        {
            base.Draw(graphicsDevice);
            DrawCenterTextY("Rogue it up!", windowSize.Y * 1 / 12, 2, Color.CornflowerBlue);

            DrawSelectedText("COMENZAR PARTIDA", 0, - windowSize.Y * 1 / 7, 1, 0 - selector.Y);

            DrawSelectedText("< "+ playerTypes[selectedPlayer].typeName +" >", 0, 0, 1, 1 - selector.Y);

            DrawSelectedText("OPCIONES (WIP)", 0, windowSize.Y * 1 / 7, 1, 2 - selector.Y);

            DrawSelectedText("SALIR", 0, windowSize.Y * 2 / 7, 1, 3 - selector.Y);
            //DrawCenterText("Presiona ENTER para comenzar", 1, Color.White);
        }

        private void KeyUpdate(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                if (selector.Y == 0)
                {
                    operations.Add("playMusic");
                    ChangeMenu(0);
                }
                if (selector.Y == 3)
                    operations.Add("exitGame");
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                selector.Y += 1;
            }
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                selector.Y -= 1;
            }
            if (selector.Y == 1) { 
                if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                {
                    selectedPlayer += 1;
                }
                if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                {
                    selectedPlayer -= 1;
                }
                if (selectedPlayer > playerTypes.Length - 1) selectedPlayer = 0;
                if (selectedPlayer < 0) selectedPlayer = playerTypes.Length - 1;
            }
        }

        public override int SelectedPlayer()
        {
            return selectedPlayer;
        }

    }
}
