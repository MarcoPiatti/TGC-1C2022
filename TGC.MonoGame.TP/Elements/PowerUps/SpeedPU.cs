using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class SpeedPU : PowerUp
    {
        private TrianglePrismPrimitive t1;
        private Matrix t1W;
        private Vector3 t1P = new Vector3(1f, 0, 0);

        private TrianglePrismPrimitive t2;
        private Matrix t2W;
        private Vector3 t2P = new Vector3(-0.2f, 0, 0);

        private TrianglePrismPrimitive t3;
        private Matrix t3W;
        private Vector3 t3P = new Vector3(-1.4f, 0, 0);

        public SpeedPU(GraphicsDevice graphicsDevice, ContentManager content, Vector3 posicion): base(graphicsDevice, content, posicion)
        {
            t1 = new TrianglePrismPrimitive(graphicsDevice, content, 1f, Color.Aqua);
            t2 = new TrianglePrismPrimitive(graphicsDevice, content, 1f, Color.Aqua);
            t3 = new TrianglePrismPrimitive(graphicsDevice, content, 1f, Color.Aqua);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Matrix triangleRotation = Matrix.CreateRotationY(Angle + MathF.PI/2);
            t1W = Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationZ(3 * MathF.PI / 4) * Matrix.CreateTranslation(Position + t1P) * triangleRotation;
            t2W = Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationZ(3 * MathF.PI / 4) * Matrix.CreateTranslation(Position + t2P) * triangleRotation;
            t3W = Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationZ(3 * MathF.PI / 4) * Matrix.CreateTranslation(Position + t3P) * triangleRotation;
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
