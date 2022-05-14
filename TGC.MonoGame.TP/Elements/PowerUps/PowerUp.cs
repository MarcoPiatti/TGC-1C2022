using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class PowerUp : Sphere
    {
        public float Angle { get; set; }

        public PowerUp(GraphicsDevice graphicsDevice, ContentManager content, Vector3 posicion): base(graphicsDevice,content, 1f, 16, new Color(0.3f, 0.3f, 0.3f, 0.1f))
        {
            Position = posicion;
            World = Matrix.CreateScale(1f, 5f, 5f) * Matrix.CreateTranslation(posicion);
        }

        public virtual void Update(GameTime gameTime)
        {
            var elapsedTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
            Angle += 1.5f * elapsedTime;
            Matrix rotation = Matrix.CreateRotationY(Angle);
            World = Matrix.CreateScale(5f, 5f, 5f) * rotation * Matrix.CreateTranslation(Position);
        }

        public override void Draw(Matrix view, Matrix projection) 
        {
            Body.Draw(World, view, projection);
        }

        public virtual void Effect()
        {
            //Funcion abstracta donde pondriamos el effecto que haria cada PowerUp cuando el jugador lo piquee
        }

    }
}
