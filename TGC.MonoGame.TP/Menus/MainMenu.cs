using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TGC.MonoGame.TP.Elements;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Menus
{
    public class MainMenu : Menu
    {
        public Vector2 selector = Vector2.Zero;

        public int selectedPlayer = 0;
        public bool flag { get; set; } = false;
        public Player[] playerTypes;
        public GameTime currentGameTime { get; set; }
        public float Cooldown { get; set; }

        public List<Cylinder> cylinders;
        public Cylinder piso;

        public MainMenu(GraphicsDevice graphicsDevice, SpriteFont SpriteFont, SpriteBatch SpriteBatch, Player[] playerTypes, ContentManager content) : base(SpriteFont, SpriteBatch, content)
        {
            this.playerTypes = playerTypes;
            piso = new Cylinder(graphicsDevice, content, Color.Orange);
            piso.World = Matrix.CreateScale(3, 2f, 3) * Matrix.CreateTranslation(-23, 0f, 3);
        }

        public override void Update(GraphicsDevice graphicsDevice, ContentManager content, GameTime gameTime, KeyboardState keyboardState)
        {
            currentGameTime = gameTime;
            base.Update(graphicsDevice, content, gameTime, keyboardState);
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

        public override void Draw(GraphicsDevice graphicsDevice, ContentManager content, Matrix view, Matrix projection)
        {
            base.Draw(graphicsDevice, content, view, projection);

            piso.Draw(view, projection);

            GeometricPrimitive player = playerTypes[selectedPlayer].Body.Body;
            Matrix playerWorld = Matrix.CreateTranslation(-23, 1.3f, 3);

            player.Draw(playerWorld, view, projection);

            DrawCenterTextY("Rogue       ", windowSize.Y * 1 / 12, 2, Color.Green);
            DrawCenterTextY("      it    ", windowSize.Y * 1 / 12, 2, Color.PaleGreen);
            DrawCenterTextY("         up ", windowSize.Y * 1 / 12, 2, Color.SpringGreen);
            DrawCenterTextY("           !", windowSize.Y * 1 / 12, 2, Color.YellowGreen);
            //DrawCenterTextY("Rogue it up!", windowSize.Y * 1 / 12, 2, Color.CornflowerBlue);
            //DrawCenterTextY("Rogue it up!", windowSize.Y * 1 / 12, 2, Color.CornflowerBlue);


            //DrawCenterTextY("Rogue it up!", windowSize.Y * 1 / 12, 2, Color.CornflowerBlue);
            DrawSelectedText("COMENZAR PARTIDA", 0, -windowSize.Y * 1 / 7, 1, 0 - selector.Y);

            DrawSelectedText("< " + playerTypes[selectedPlayer].typeName + " >", 0, 0, 1, 1 - selector.Y);

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
                    menu_select.Play();
                    var flag = true;
                    if (flag)
                    {
                        //play song
                    }
                   
                    //goodStartoo(flag);
                  


                    operations.Add("playMusic");
                    operations.Add("showCoins");
                    ChangeMenu(0);


                }
                if (selector.Y == 3)
                    //menu_select.Play();
                    operations.Add("exitGame");
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                menu_move.Play();
                selector.Y += 1;

            }
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                menu_move.Play();
                selector.Y -= 1;
            }
            if (selector.Y == 1) {
                if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                {
                    menu_move.Play();
                    selectedPlayer += 1;
                }
                if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                {
                    menu_move.Play();
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
        public bool goodStartoo(bool flag)
        {
            System.Threading.Thread.Sleep(500);
            menu_select.Play();
            flag = true;
            System.Threading.Thread.Sleep(400);
            //Wait();
            menu_select.Play();
            System.Threading.Thread.Sleep(400);
            menu_select.Play();
            System.Threading.Thread.Sleep(300);
            menu_select.Play();
            System.Threading.Thread.Sleep(300);
            menu_select.Play();
            System.Threading.Thread.Sleep(200);
            menu_select.Play();
            System.Threading.Thread.Sleep(200);
            menu_select.Play();
            System.Threading.Thread.Sleep(200);
            menu_select.Play();
            System.Threading.Thread.Sleep(100);
            menu_select.Play();
            System.Threading.Thread.Sleep(100);
            menu_select.Play();
            System.Threading.Thread.Sleep(100);
            menu_select.Play();
            System.Threading.Thread.Sleep(50);
            menu_select.Play();
            System.Threading.Thread.Sleep(50);
            menu_select.Play();
            //Wait();
            flag = false;
            return flag;

        }
        public void Wait()
        {
            Cooldown = 0.25f;
            while (Cooldown > 0)
                Cooldown -= Convert.ToSingle(currentGameTime.ElapsedGameTime.TotalSeconds);
        }

    }
}
