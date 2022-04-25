using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class Coin
    {
        public SpherePrimitive body { get; set; }
        public Matrix CoinWorld { get; set; }
        public Vector3 position { get; set; }
        private float CoinRotation { get; set; }
        private float CoinAngle { get; set; }

        public Coin(GraphicsDevice graphicsDevice, Vector3 posicion){
            body = new SpherePrimitive(graphicsDevice);
            CoinWorld = Matrix.CreateScale(1f, 10f, 10f) * Matrix.CreateTranslation(posicion);
            position = posicion;
        }

        public void Update(GameTime gameTime)
        {
            CoinAngle += 0.01f;
            Matrix rotation = Matrix.CreateRotationY(CoinAngle);
            CoinWorld = Matrix.CreateScale(1f, 10f, 10f) * rotation * Matrix.CreateTranslation(position);
        }

        public void Draw(Matrix view, Matrix projection){
            body.Draw(CoinWorld, view, projection);
        }
    }
}
