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
        private TrianglePrism t1;
        private Vector3 P1 = new Vector3(1f, 0, 0);

        private TrianglePrism t2;
        private Vector3 P2 = new Vector3(-0.2f, 0, 0);

        private TrianglePrism t3;
        private Vector3 P3 = new Vector3(-1.4f, 0, 0);

        public SpeedPU(GraphicsDevice graphicsDevice, ContentManager content, Vector3 posicion): base(graphicsDevice, content, posicion)
        {
            t1 = new TrianglePrism(graphicsDevice, content, posicion, Color.Aqua);
            t2 = new TrianglePrism(graphicsDevice, content, posicion, Color.Aqua);
            t3 = new TrianglePrism(graphicsDevice, content, posicion, Color.Aqua);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Matrix triangleRotation = Matrix.CreateRotationY(Angle + MathF.PI/2);
            t1.World = Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationZ(3 * MathF.PI / 4) * Matrix.CreateTranslation(Position + P1) * triangleRotation;
            t2.World = Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationZ(3 * MathF.PI / 4) * Matrix.CreateTranslation(Position + P2) * triangleRotation;
            t3.World = Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationZ(3 * MathF.PI / 4) * Matrix.CreateTranslation(Position + P3) * triangleRotation;
        }

        public override void Draw(Matrix view, Matrix projection) 
        {
            t1.Draw( view, projection);
            t2.Draw( view, projection);
            t3.Draw( view, projection);
            base.Draw(view, projection);
        }

    }
}
