using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Menus
{
    public class PauseMenu : Menu
    {
        public Vector2 selector = Vector2.Zero;

        public int selectedPlayer = 0;



        public PauseMenu(SpriteFont SpriteFont, SpriteBatch SpriteBatch) : base(SpriteFont, SpriteBatch)
        {
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

            if (selector.Y > 2) selector.Y = 0;
            if (selector.Y < 0) selector.Y = 2;
        }

        public override void Draw(GraphicsDevice graphicsDevice)
        {
            base.Draw(graphicsDevice);
            DrawCenterTextY("PAUSA", windowSize.Y * 1 / 12, 2, Color.CornflowerBlue);

            DrawSelectedText("REANUDAR", 0, - windowSize.Y * 1 / 7, 1, 0 - selector.Y);

            DrawSelectedText("OPCIONES (WIP)", 0, 0, 1, 1 - selector.Y);

            DrawSelectedText("VOLVER AL MENU PRINCIPAL", 0, windowSize.Y * 1 / 7, 1, 2 - selector.Y);
            //DrawCenterText("Presiona ENTER para comenzar", 1, Color.White);
        }

        private void KeyUpdate(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                if (selector.Y == 0)
                {
                    operations.Add("upMusic");
                    ChangeMenu(0);
                }
                if (selector.Y == 2)
                {
                    operations.Add("resetGame");
                    ChangeMenu(1);
                }
            }
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                operations.Add("upMusic");
                ChangeMenu(0);
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                selector.Y += 1;
            }
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                selector.Y -= 1;
            }
        }

        public override int SelectedPlayer()
        {
            return selectedPlayer;
        }

    }
}
