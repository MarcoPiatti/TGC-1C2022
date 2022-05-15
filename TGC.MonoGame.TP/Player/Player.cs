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
        private float MoveForceAir = 0.3f;
        private float JumpForce = 6f;
        public float Bounce = 0.5f;
        private float CCC = 0.01f; //Collider Correction Constant
        private int totalCoins = 0;

        public string typeName = "base";

        private  Vector3 scale = new Vector3(5, 5, 5);
        private State estado { get; set; }

        public Sphere Body { get; set; }

        public bool grounded = false;

        public Sphere JumpLine { get; set; }
        public Vector3 JumpLinePos = new Vector3(0, -3f, 0);

        public Vector3 Position { get; set; }

        public Player(GraphicsDevice graphics, ContentManager content)
        {
            Body = new Sphere(graphics, content, 1f, 16, Color.Green);
            Body.WorldUpdate(scale, new Vector3(0, 15, 20), Quaternion.Identity);
            Position = Body.Position;
            JumpLine = new Sphere(graphics, content, 1f, 4, new Color(0f, 1f, 0f, 0.3f));
            JumpLine.WorldUpdate(new Vector3(3, 3, 3), Position + JumpLinePos, Quaternion.Identity);
        }

        public void Draw(Matrix view, Matrix projection)
        {
            Body.Draw(view, projection);
            JumpLine.Draw(view, projection);
        }

        public void Update(GameTime gameTime, List<TP.Elements.Object> objects, List <TP.Elements.LogicalObject> logicalObjects)
        {
            if(!grounded)
                VectorSpeed += Vector3.Down * Gravity;
            var elapsedTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
            var scaledSpeed = VectorSpeed * elapsedTime;
            Body.WorldUpdate(scale, scaledSpeed, Matrix.CreateRotationZ(VectorSpeed.X) * Matrix.CreateRotationX(VectorSpeed.Z));
            Position = Body.Position;
            JumpLine.WorldUpdate(new Vector3(1, 1f, 1), scaledSpeed, Quaternion.Identity);
            //JumpLine.Position = Vector3.Zero;
            PhyisicallyInteract(objects, elapsedTime);
            LogicalInteract(logicalObjects);
            grounded = CanJump(objects);
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
                        JumpLine.WorldUpdate(new Vector3(1, 1f, 1), VectorSpeed * CCC, Quaternion.Identity);
                    }
                    VectorSpeed *= Bounce;
                }
            }
        }

        public bool CanJump(List<TP.Elements.Object> objects)
        {
            foreach (TP.Elements.Object o in objects)
            {
                if (o.Intersects(JumpLine))
                {
                    return true;
                }
            }
            return false;
        }

        public void LogicalInteract(List<TP.Elements.LogicalObject> logicalObjects)
        {
            foreach (TP.Elements.LogicalObject o in logicalObjects)
            {
                if (o.Intersects(Body))
                {
                    o.logicalAction(this);
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
            if(grounded)
                VectorSpeed += direction * MoveForce;
            else
                VectorSpeed += direction * MoveForceAir;
        }

        public void Jump()

        {
            if(grounded)
                VectorSpeed += Vector3.Up * JumpForce;

        }

        public void AddCoin()
        {
            totalCoins++;
        }
        public void Restart()
        {
            //Body.World = Matrix.CreateScale(scale) * Matrix.CreateTranslation(Position + traslation) * Matrix.CreateFromQuaternion(rotation);
            //Body.Position = Position + traslation;
        }
    }

}

