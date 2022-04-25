using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Niveles
{
    public class Sala0 : Sala
    {
        private CubePrimitive ParedSur { get; set; }
        private CubePrimitive FirstPlatform { get; set; }

        private SpherePrimitive Coin { get; set; }

        private float CoinRotation { get; set; }
        private float CoinAngle { get; set; }

        private Matrix CoinWorld { get; set; }
        private Matrix ParedSurWorld { get; set; }
        private Matrix FirstPlatformWorld { get; set; }

        public Sala0(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion) : base(content, graphicsDevice, posicion)
        {
     
            Coin = new SpherePrimitive(graphicsDevice);
            CoinWorld = Matrix.CreateScale(1f, 10f, 10f) * Matrix.CreateTranslation(new Vector3(25, 20, 0) + posicion);
            FirstPlatform = new CubePrimitive(graphicsDevice);
            FirstPlatformWorld = Matrix.CreateScale(10f, 1f, 10f) * Matrix.CreateTranslation(new Vector3(25, 10, 0) + posicion);


            ParedSur = new CubePrimitive(graphicsDevice);
            ParedSurWorld = Matrix.CreateScale(1f, Size, Size) * Matrix.CreateTranslation(new Vector3(-Size / 2, 0, 0) + posicion);
        }

        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime, view, projection);
            FirstPlatform.Draw(FirstPlatformWorld, view, projection);
            ParedSur.Draw(ParedSurWorld, view, projection);
            Coin.Draw(CoinWorld, view, projection);
        }
        
        
        public override void Update(GameTime gameTime)
        {
            CoinAngle += 0.01f;
            var rotation = Matrix.CreateRotationY(CoinAngle);
            CoinWorld = Matrix.CreateScale(1f, 10f, 10f) * rotation * Matrix.CreateTranslation(new Vector3(25, 20, 0));
            //CoinRotation += Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }
        
    }
}
