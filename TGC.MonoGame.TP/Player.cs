using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Geometries;
using TGC.MonoGame.TP.Elements;
using TGC.MonoGame.Samples.Collisions;

namespace TGC.MonoGame.TP
{
    public class Player
    {
        public Vector3 PositionE { get; private set; }
        public Vector3 VectorSpeed { get; set; }
        public Vector3 roundPosition { get; set; }
        private static float Gravity = 0.2f;
        private static float MoveForce = 1f;
        private static float JumpForce = 2f;
        private static float Bounce = 0.5f;
        private static float CCC = 0.01f; //Collider Correction Constant

        private static Vector3 scale = new Vector3(5, 5, 5);
        private State estado { get; set; }

        public Sphere Body { get; set; }

        public Vector3 Position { get; set; }

        public Player(GraphicsDevice graphics, ContentManager content)
        {
            Body = new Sphere(graphics, content, 1f, 16, Color.Green);
            Body.WorldUpdate(scale, new Vector3(0, 15, 0), Quaternion.Identity);
        }

        public void Draw(Matrix view, Matrix projection)
        {

            Body.Draw(view, projection);
        }

        

        public void Update(GameTime gameTime, List<TP.Elements.Object> objects)
        {
            VectorSpeed += Vector3.Down * Gravity;
            var elapsedTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
            var scaledSpeed = VectorSpeed * elapsedTime;
            Body.WorldUpdate(scale, scaledSpeed, Quaternion.Identity);
            Position = Body.Position;
            PhyisicallyInteract(objects, elapsedTime);
        }

        public void PhyisicallyInteract(List<TP.Elements.Object> objects,float elapsedTime)
        {
            foreach (TP.Elements.Object o in objects)
            {
                if (o.Intersects(Body))
                {
                    VectorSpeed = VectorSpeed * o.GetDirectionFromCollision(Body);
                    while (o.Intersects(Body)) {
                        Body.WorldUpdate(scale, VectorSpeed * CCC, Quaternion.Identity);
                        Position = Body.Position;
                    }
                    VectorSpeed *= Bounce;
                }
            }
        }

        

        public void Move(Vector3 direction)
        {
            VectorSpeed += direction * MoveForce;
        }
        public void Jump()
        {
            VectorSpeed += Vector3.Up * JumpForce;
        }

    }

}

