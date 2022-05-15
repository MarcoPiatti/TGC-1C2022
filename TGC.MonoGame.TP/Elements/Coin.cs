using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using TGC.MonoGame.Samples.Collisions;
using TGC.MonoGame.TP;
using TGC.MonoGame.TP.Elements;
using TGC.MonoGame.TP.Geometries;
namespace TGC.MonoGame.TP.Elements
{
    public class Coin : LogicalCyllinder
    {
        private float CoinAngle { get; set; }

        private ContentManager localContent { get; set; }
        private GraphicsDevice localGraphics { get; set; }
        private SoundEffect sound { get; set; }

        public Coin(GraphicsDevice graphicsDevice, ContentManager content, Vector3 posicion) : base(graphicsDevice, content, Color.Gold)
        {
            localContent = content;
            var SoundName = "coin_get";
            sound = content.Load<SoundEffect>("Music/" + SoundName);
            Collider = new BoundingCylinder(posicion, 5f, 5f);
            Position = posicion;
            World = Matrix.CreateScale(5f, 1f, 5f) * Matrix.CreateTranslation(posicion);
        }
        public override void logicalAction(Player player)
        {
            flagCollide = true;
            //playSound();
            player.AddCoin();

        }
        public void Update(GameTime gameTime)
        {
            if (flagCollide) {
                destroyItself();
            } else
            {
                var elapsedTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
                CoinAngle += 1.5f * elapsedTime;
                Matrix rotation = Matrix.CreateRotationY(CoinAngle);
                World = Matrix.CreateScale(5f, 0.5f, 5f) * Matrix.CreateRotationZ(MathF.PI / 2) * rotation * Matrix.CreateTranslation(Position);
            }

        }

        private void playSound()
        {
            sound.Play();
        }

    }
}
