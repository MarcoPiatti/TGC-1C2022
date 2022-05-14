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
        private List<Cylinder> cyl = new List<Cylinder>();

        private List<TrianglePrism> triang = new List<TrianglePrism>();

        private Vector3 P1 = new Vector3(0.8f, -1.2f, 0);
        private Vector3 P2 = new Vector3(1.4f, -0.5f, 0);
        private Vector3 P3 = new Vector3(1.6f, 0.3f, 0);
        private Vector3 P4 = new Vector3(0.3f, -1.4f, 0);

        public GladePU(GraphicsDevice graphicsDevice, ContentManager content, Vector3 posicion): base(graphicsDevice, content, posicion)
        {
            triang.Add(new TrianglePrism(graphicsDevice, content, posicion, Color.White));
            triang.Add(new TrianglePrism(graphicsDevice, content, posicion, Color.White));
            triang.Add(new TrianglePrism(graphicsDevice, content, posicion, Color.White));
            triang.Add(new TrianglePrism(graphicsDevice, content, posicion, Color.White));
            triang.Add(new TrianglePrism(graphicsDevice, content, posicion, Color.White));
            triang.Add(new TrianglePrism(graphicsDevice, content, posicion, Color.White));
            cyl.Add(new Cylinder(graphicsDevice, content, Color.White));
            cyl.Add(new Cylinder(graphicsDevice, content, Color.White));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Matrix rotation1 = Matrix.CreateRotationY(Angle + MathF.PI/2);
            Matrix rotation2 = Matrix.CreateRotationY(Angle - MathF.PI / 2);
            triang[0].World = Matrix.CreateScale(1f, 0.8f, 1f) * Matrix.CreateRotationZ(MathC.ToRadians(-130)) * Matrix.CreateTranslation(Position + P1) * rotation1;
            triang[1].World = Matrix.CreateScale(1f, 1.2f, 1f) * Matrix.CreateRotationZ(MathC.ToRadians(-100)) * Matrix.CreateTranslation(Position + P2) * rotation1;
            triang[2].World = Matrix.CreateScale(1f, 1.6f, 1f) * Matrix.CreateRotationZ(MathC.ToRadians(-70)) * Matrix.CreateTranslation(Position + P3) * rotation1;
            triang[3].World = Matrix.CreateScale(1f, 0.8f, 1f) * Matrix.CreateRotationZ(MathC.ToRadians(-130)) * Matrix.CreateTranslation(Position + P1) * rotation2;
            triang[4].World = Matrix.CreateScale(1f, 1.2f, 1f) * Matrix.CreateRotationZ(MathC.ToRadians(-100)) * Matrix.CreateTranslation(Position + P2) * rotation2;
            triang[5].World = Matrix.CreateScale(1f, 1.6f, 1f) * Matrix.CreateRotationZ(MathC.ToRadians(-70)) * Matrix.CreateTranslation(Position + P3) * rotation2;
            cyl[0].World = Matrix.CreateScale(0.4f, 1f, 0.4f) * Matrix.CreateRotationX(MathC.ToRadians(90)) * Matrix.CreateTranslation(Position + P4) * rotation1;
            cyl[1].World = Matrix.CreateScale(0.4f, 1f, 0.4f) * Matrix.CreateRotationX(MathC.ToRadians(90)) * Matrix.CreateTranslation(Position + P4) * rotation2;
        }

        public override void Draw(Matrix view, Matrix projection) 
        {
            foreach (TrianglePrism t in triang)
            {
                t.Draw(view, projection);
            }
            foreach (Cylinder c in cyl)
            {
                c.Draw(view, projection);
            }
            base.Draw(view, projection);
        }

    }
}
