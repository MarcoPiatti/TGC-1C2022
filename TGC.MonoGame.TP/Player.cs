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
        private float Gravity = 0.7f;
        private float MoveForce = 1f;
        private float JumpForce = 2f;
        private float Bounce = 0.5f;
        private float CCC = 0.01f; //Collider Correction Constant

        private  Vector3 scale = new Vector3(5, 5, 5);
        private State estado { get; set; }

        public Sphere Body { get; set; }



        public Cube JumpLine { get; set; }

        public Vector3 Position { get; set; }


        public Player(GraphicsDevice graphics, ContentManager content)
        {
            Body = new Sphere(graphics, content, 1f, 16, Color.Green);
            Body.WorldUpdate(scale, new Vector3(0, 15, 0), Quaternion.Identity);
            JumpLine = new Cube(graphics, content, Body.Position);
            JumpLine.WorldUpdate(scale, Body.Position, Quaternion.Identity);
        }

        public void Draw(Matrix view, Matrix projection)
        {
            JumpLine.Draw(view, projection);
            Body.Draw(view, projection);
        }

        public void Update(GameTime gameTime, List<TP.Elements.Object> objects, List <TP.Elements.LogicalObject> logicalObjects)
        {
            VectorSpeed += Vector3.Down * Gravity;
            var elapsedTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
            var scaledSpeed = VectorSpeed * elapsedTime;
            //float rotationSpeed = MathF.Sqrt(MathF.Pow(scaledSpeed.X, 2) + MathF.Pow(scaledSpeed.Z, 2));
            Body.WorldUpdate(scale, scaledSpeed, Quaternion.Identity);
            Position = Body.Position;
            JumpLine.WorldUpdate(scale, Body.Position, Quaternion.Identity);
            PhyisicallyInteract(objects, elapsedTime);
            LogicalInteract(logicalObjects);
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
        public void LogicalInteract(List<TP.Elements.LogicalObject> logicalObjects)
        {
            foreach (TP.Elements.LogicalObject o in logicalObjects)
            {
                if (o.Intersects(Body))
                {
                    o.logicalAction(Body);
                }
            }
        }



        /*
        public static void Move(Vector3 direction)
        {
            VectorSpeed += direction * MoveForce;
        }
        */

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

