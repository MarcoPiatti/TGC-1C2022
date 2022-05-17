using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class GladePU : PowerUp
    {
        private List<Cylinder> cyl = new List<Cylinder>();
        private SoundEffect sound { get; set; }
        private List<TrianglePrism> triang = new List<TrianglePrism>();

        private Vector3 P1 = new Vector3(0.8f, -1.2f, 0.8f);
        private Vector3 P2 = new Vector3(1.4f, -0.5f, 1.4f);
        private Vector3 P3 = new Vector3(1.6f, 0.3f, 1.6f);
        private Vector3 P4 = new Vector3(0.3f, -1.4f, 0.3f);

        public GladePU(GraphicsDevice graphicsDevice, ContentManager content, Vector3 posicion): base(graphicsDevice, content, posicion)
        {
            var SoundName = "powerUpPicked";
            sound = content.Load<SoundEffect>("Music/" + SoundName);
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
            if (flagCollide == true)
            {
                destroyItself();
            }
            else
            {
                base.Update(gameTime);
                Matrix rotation1 = Matrix.CreateRotationY(Angle + MathF.PI / 2);
                Matrix rotation2 = Matrix.CreateRotationY(Angle - MathF.PI / 2);
                Vector3 RPE = new Vector3(MathF.Sin(Angle), 1, MathF.Cos(Angle)); //Rotacion de la posicion
                Vector3 YInv = new Vector3(1, -1, 1); //Invierto la posicion en Y;
                triang[0].World = Matrix.CreateScale(1f, 0.8f, 1f) * Matrix.CreateRotationZ(MathC.ToRadians(-130)) * rotation1 * Matrix.CreateTranslation(Position - P1 * YInv * RPE);
                triang[1].World = Matrix.CreateScale(1f, 1.2f, 1f) * Matrix.CreateRotationZ(MathC.ToRadians(-100)) * rotation1 * Matrix.CreateTranslation(Position - P2 * YInv * RPE);
                triang[2].World = Matrix.CreateScale(1f, 1.6f, 1f) * Matrix.CreateRotationZ(MathC.ToRadians(-70)) * rotation1 * Matrix.CreateTranslation(Position - P3 * YInv * RPE);
                triang[3].World = Matrix.CreateScale(1f, 0.8f, 1f) * Matrix.CreateRotationZ(MathC.ToRadians(-130)) * rotation2 * Matrix.CreateTranslation(Position + P1 * RPE);
                triang[4].World = Matrix.CreateScale(1f, 1.2f, 1f) * Matrix.CreateRotationZ(MathC.ToRadians(-100)) * rotation2 * Matrix.CreateTranslation(Position + P2 * RPE);
                triang[5].World = Matrix.CreateScale(1f, 1.6f, 1f) * Matrix.CreateRotationZ(MathC.ToRadians(-70)) * rotation2 * Matrix.CreateTranslation(Position + P3 * RPE);
                cyl[0].World = Matrix.CreateScale(0.4f, 1f, 0.4f) * Matrix.CreateRotationX(MathC.ToRadians(90)) * rotation1 * Matrix.CreateTranslation(Position - P4 * YInv * RPE);
                cyl[1].World = Matrix.CreateScale(0.4f, 1f, 0.4f) * Matrix.CreateRotationX(MathC.ToRadians(90)) * rotation2 * Matrix.CreateTranslation(Position + P4 * RPE);
            }
        }
        public override void destroyItself()
        {
            Collider = new BoundingSphere(new Vector3(0f, 1000f, 0f), 0f);
            base.destroyItself();
            triang[0].World = Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateTranslation(Position + new Vector3(0, 100, 0));
            triang[1].World = Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateTranslation(Position + new Vector3(0, 100, 0));
            triang[2].World = Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateTranslation(Position + new Vector3(0, 100, 0));
            triang[3].World = Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateTranslation(Position + new Vector3(0, 100, 0));
            triang[4].World = Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateTranslation(Position + new Vector3(0, 100, 0));
            triang[5].World = Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateTranslation(Position + new Vector3(0, 100, 0));
            cyl[0].World = Matrix.CreateScale(0f, 0f, 00f) * Matrix.CreateRotationX(MathC.ToRadians(90)) * Matrix.CreateTranslation(Position + P4);
            cyl[1].World = Matrix.CreateScale(0f, 0f, 00f) * Matrix.CreateRotationX(MathC.ToRadians(90)) * Matrix.CreateTranslation(Position + P4);
        }
        public override void logicalAction(Player player)
        {
            sound.Play();
            base.logicalAction(player);
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

        public override void Effect(Player player)
        {
            player.gladePuTime = 5;
        }
    }
}
