using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class GladePU : PowerUp
    {
        private TrianglePrismPrimitive t1;
        private Matrix t1W;
        private Vector3 t1P = new Vector3(0.6f, -0.3f, 0);

        private TrianglePrismPrimitive t2;
        private Matrix t2W;
        private Vector3 t2P = new Vector3(0.6f, -0.3f, 0);

        private TrianglePrismPrimitive t3;
        private Matrix t3W;
        private Vector3 t3P = new Vector3(1.5f, 0.1f, 0);

        public GladePU(GraphicsDevice graphicsDevice, ContentManager content, Vector3 posicion): base(graphicsDevice, content, posicion)
        {
            t1 = new TrianglePrismPrimitive(graphicsDevice, content, 1f, Color.White);
            t2 = new TrianglePrismPrimitive(graphicsDevice, content, 1f, Color.White);
            t3 = new TrianglePrismPrimitive(graphicsDevice, content, 1f, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Matrix triangleRotation1 = Matrix.CreateRotationY(Angle + MathF.PI/2);
            Matrix triangleRotation2 = Matrix.CreateRotationY(Angle - MathF.PI / 2);
            t1W = Matrix.CreateScale(1f, 0.8f, 1f) * Matrix.CreateRotationZ(MathC.ToRadians(-110)) * Matrix.CreateTranslation(Position + t1P) * triangleRotation1;
            t2W = Matrix.CreateScale(1f, 0.8f, 1f) * Matrix.CreateRotationZ(MathC.ToRadians(-110)) * Matrix.CreateTranslation(Position + t2P) * triangleRotation2;
            //t3W = Matrix.CreateScale(1f, 1.6f, 1f) * Matrix.CreateRotationZ(MathC.ToRadians(260)) * Matrix.CreateTranslation(Position + t3P) * triangleRotation1;
        }

        public override void Draw(Matrix view, Matrix projection) 
        {
            t1.Draw(t1W, view, projection);
            t2.Draw(t2W, view, projection);
            t3.Draw(t3W, view, projection);
            base.Draw(view, projection);
        }

    }
}
