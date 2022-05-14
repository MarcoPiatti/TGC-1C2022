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

        private Vector3 P1 = new Vector3(1f, 0, 0);
        private Vector3 P2 = new Vector3(-0.2f, 0, 0);
        private Vector3 P3 = new Vector3(-1.4f, 0, 0);

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
                triang[0].World = Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationZ(3 * MathF.PI / 4) * Matrix.CreateTranslation(Position + P1) * triangleRotation;
                triang[1].World = Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationZ(3 * MathF.PI / 4) * Matrix.CreateTranslation(Position + P2) * triangleRotation;
                triang[2].World = Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationZ(3 * MathF.PI / 4) * Matrix.CreateTranslation(Position + P3) * triangleRotation;
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
            flagCollide = true; 
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


    }
}
