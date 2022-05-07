using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class Coin: Sphere
    {
        private float CoinRotation { get; set; }
        private float CoinAngle { get; set; }

        public Coin(GraphicsDevice graphicsDevice, ContentManager content, Vector3 posicion): base(graphicsDevice,content, 1f, 16, Color.Gold)
        {
            Position = posicion;
            World = Matrix.CreateScale(1f, 5f, 5f) * Matrix.CreateTranslation(posicion);
        }

        public void Update(GameTime gameTime)
        {
            var elapsedTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
            CoinAngle += 1.5f * elapsedTime;
            Matrix rotation = Matrix.CreateRotationY(CoinAngle);
            World = Matrix.CreateScale(1f, 10f, 10f) * rotation * Matrix.CreateTranslation(Position);
        }

    }
}
