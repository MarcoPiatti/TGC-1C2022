using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class SpeedPU : PowerUp
    {
        private SoundEffect sound { get; set; }
        private List<TrianglePrism> triang = new List<TrianglePrism>();

        private Vector3 P1 = new Vector3(1.4f, 0, 1.4f);
        private Vector3 P2 = new Vector3(0.2f, 0, 0.2f);
        private Vector3 P3 = new Vector3(-1f, 0, -1f);

        public SpeedPU(GraphicsDevice graphicsDevice, ContentManager content, Vector3 posicion): base(graphicsDevice, content, posicion)
        {
            var SoundName = "powerUpPicked";
            sound = content.Load<SoundEffect>("Music/" + SoundName);
            triang.Add(new TrianglePrism(graphicsDevice, content, posicion, Color.Aqua));
            triang.Add(new TrianglePrism(graphicsDevice, content, posicion, Color.Aqua));
            triang.Add(new TrianglePrism(graphicsDevice, content, posicion, Color.Aqua));
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
                Matrix triangleRotation = Matrix.CreateRotationY(Angle + MathF.PI / 2);
                Vector3 RPE = new Vector3(MathF.Sin(Angle), 1, MathF.Cos(Angle)); //Rotacion de la posicion
                triang[0].World = Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationZ(3 * MathF.PI / 4) * triangleRotation * Matrix.CreateTranslation(Position + P1 * RPE);
                triang[1].World = Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationZ(3 * MathF.PI / 4) * triangleRotation * Matrix.CreateTranslation(Position + P2 * RPE);
                triang[2].World = Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationZ(3 * MathF.PI / 4) * triangleRotation * Matrix.CreateTranslation(Position + P3 * RPE);
            }
        }

        public override void Draw(Matrix view, Matrix projection) 
        {
            foreach (TrianglePrism t in triang)
            {
                t.Draw(view, projection);
            }
            base.Draw(view, projection);
        }

        public override void logicalAction(Player player)
        {
            sound.Play();
            base.logicalAction(player);
        }

        public override void destroyItself()
        {
            Collider = new BoundingSphere(new Vector3(0f, 1000f, 0f), 0f);
            base.destroyItself();
            triang[0].World = Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateTranslation(Position + new Vector3(0,100,0));
            triang[1].World = Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateTranslation(Position + new Vector3(0, 100, 0));
            triang[2].World = Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateTranslation(Position + new Vector3(0, 100, 0));
        }

        public override void Effect(Player player)
        {
            player.speedPuTime = 10;
        }

    }
}
