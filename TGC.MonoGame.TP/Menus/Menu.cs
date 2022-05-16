using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Menus
{
    public class Menu
    {
        public Vector2 windowSize;

        private SpriteFont SpriteFont;

        public SpriteBatch SpriteBatch;

        public List<string> operations = new List<string>();
        public SoundEffect menu_move { get; set; }
        public SoundEffect menu_select { get; set; }
        public Song menu_music { get; set; }

        public int nextMenu;

        public float KeyCoolDown = 0f;

        public Menu(SpriteFont SpriteFont, SpriteBatch SpriteBatch, ContentManager content)
        {
            var menuMove = "menu_move";
            menu_move = content.Load<SoundEffect>("Music/" + menuMove);
            var menuSelect = "menu_select";
            menu_select = content.Load<SoundEffect>("Music/" + menuSelect);

            this.SpriteFont = SpriteFont;
            this.SpriteBatch = SpriteBatch;
        }

        public virtual void Update(GraphicsDevice graphicsDevice, GameTime gameTime, KeyboardState keyboardState)
        {
            windowSize = new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        }

        public virtual void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.Black);
            MenuHelper();
        }

        public void DrawCenterText(string msg, float escala, Color color)
        {
            var W = windowSize.X;
            var H = windowSize.Y;
            var size = SpriteFont.MeasureString(msg) * escala;
            SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                Matrix.CreateScale(escala) * Matrix.CreateTranslation((W - size.X) / 2, (H - size.Y) / 2, 0));
            SpriteBatch.DrawString(SpriteFont, msg, new Vector2(0, 0), color);
            SpriteBatch.End();
        }

        public void DrawCenterTextY(string msg, float Y, float escala, Color color)
        {
            var W = windowSize.X;
            var H = windowSize.Y;
            var size = SpriteFont.MeasureString(msg) * escala;
            SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                Matrix.CreateScale(escala) * Matrix.CreateTranslation((W - size.X) / 2, Y, 0));
            SpriteBatch.DrawString(SpriteFont, msg, new Vector2(0, 0), color);
            SpriteBatch.End();
        }

        public void DrawTextFromCenter(string msg, float X, float Y, float escala, Color color)
        {
            var W = windowSize.X;
            var H = windowSize.Y;
            var size = SpriteFont.MeasureString(msg) * escala;
            SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                Matrix.CreateScale(escala) * Matrix.CreateTranslation(((W - size.X) / 2) + X, ((H - size.Y) / 2) + Y, 0));
            SpriteBatch.DrawString(SpriteFont, msg, new Vector2(0, 0), color);
            SpriteBatch.End();
        }

        public void DrawTextFromCenterNotCentered(string msg, float X, float Y, float escala, Color color)
        {
            var W = windowSize.X;
            var H = windowSize.Y;
            SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                Matrix.CreateScale(escala) * Matrix.CreateTranslation((W / 2) + X, (H / 2) + Y, 0));
            SpriteBatch.DrawString(SpriteFont, msg, new Vector2(0, 0), color);
            SpriteBatch.End();
        }

        public void DrawSelectedText(string msg, float X, float Y, float escala, float selectorY, float selectorX = 0)
        {
            if(selectorY == 0 && selectorX == 0)
                DrawTextFromCenter(msg, X, Y, escala, Color.Yellow);
            else
                DrawTextFromCenter(msg, X, Y, escala, Color.White);
        }

        public void ChangeMenu (int menu)
        {
            operations.Add("changeMenu");
            nextMenu = menu;
        }

        public virtual int SelectedPlayer()
        {
            return -1;
        }

        public void MenuHelper()
        {
            DrawTextFromCenterNotCentered("WASD | Flechas = Seleccionar", - windowSize.X * 1 / 2 + 10f, windowSize.Y * 26 / 70, 0.5f, Color.Purple);
            DrawTextFromCenterNotCentered("Enter = Aceptar", -windowSize.X * 1 / 2 + 10f, windowSize.Y * 28 / 70, 0.5f, Color.MediumPurple);
            DrawTextFromCenterNotCentered("Esc = Volver", -windowSize.X * 1 / 2 + 10f, windowSize.Y * 30 / 70, 0.5f, Color.Violet);
        }

    }
}
