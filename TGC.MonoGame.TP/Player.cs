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

        public bool Intersects(OrientedBoundingBox box)
        {
            //var obbSpaceSphere = new BoundingSphere(Vector3.Transform(difference, box.Orientation), Body.Collider.Radius);
            //var obbSpaceSphere = new BoundingSphere(box.ToOBBSpace(Body.Collider.Center), Body.Collider.Radius);
            var aabb = new BoundingBox(box.Center - box.Extents + box.Center, box.Extents);
            return aabb.Intersects(Body.Collider);
        }

        public void Update(GameTime gameTime, List<Cube> objects)
        {
            VectorSpeed += Vector3.Down * Gravity;
            var elapsedTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
            PhyisicallyInteract(objects, elapsedTime);
            var scaledSpeed = VectorSpeed * elapsedTime;
            Body.WorldUpdate(scale, scaledSpeed, Quaternion.Identity);
            Position = Body.Position;
            
        }

        public void PhyisicallyInteract(List<Cube> objects,float elapsedTime)
        {
            foreach (Cube o in objects)
            {
                if (Intersects(o.Collider))
                {
                    VectorSpeed = VectorSpeed * GetDirectionFromBoxCollision(o);
                    //VectorSpeed *= Bounce;
                    //Body.WorldUpdate(scale, VectorSpeed * elapsedTime , Quaternion.Identity);
                    //Position = Body.Position;
                }
            }
        }

        public Vector3 GetDirectionFromBoxCollision(Cube c) {
            float X = Math.Abs(c.Collider.Center.X - c.Collider.Extents.X);
            float Y = Math.Abs(c.Collider.Center.Y - c.Collider.Extents.Y);
            float Z = Math.Abs(c.Collider.Center.Z - c.Collider.Extents.Z);
            float Xdistance = Math.Abs(c.Collider.Center.X - Body.Collider.Center.X) + Body.Collider.Radius;
            float Ydistance = Math.Abs(c.Collider.Center.X - Body.Collider.Center.X) + Body.Collider.Radius;
            float Zdistance = Math.Abs(c.Collider.Center.X - Body.Collider.Center.X) + Body.Collider.Radius;
            if (X > Xdistance && Y > Ydistance)
                return new Vector3(1, 1, -1);
            if (X > Xdistance && Z > Zdistance)
                return new Vector3(1, -1, 1);
            if (Y > Ydistance && Z > Zdistance)
                return new Vector3(-1, 1, 1);
            return new Vector3(0, 0, 0);
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

