using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TGC.MonoGame.Samples.Collisions;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{

    public abstract class Object
    {
        public Matrix World;

        public Vector3 Position { get; set; }
        public static Vector3 InitialPosition{get;set;}

        public GeometricPrimitive Body { get; set; }

        public virtual void Draw(Matrix view, Matrix projection)
        {
            Body.Draw(World, view, projection);
        }

        public virtual void WorldUpdate(Vector3 scale, Vector3 newPosition, Quaternion rotation)
        {
            World = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(newPosition) ;
            Position = newPosition;
        }
        

        public virtual void WorldUpdate(Vector3 scale, Quaternion rotation)
        {
            World = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(Position) ;
        }

        public virtual void WorldUpdate(Vector3 scale, Vector3 newPosition, Matrix rotationMatrix)
        {
            World = Matrix.CreateScale(scale) * rotationMatrix * Matrix.CreateTranslation(newPosition);
            Position = newPosition;
        }

        public abstract bool Intersects(Sphere s);

        public abstract Vector3 GetDirectionFromCollision(Sphere s);
    } 

    public class Cube : Object
    {
        public OrientedBoundingBox Collider { get; set; }
        public OrientedBoundingBox InitialCollider { get; set; }
        public Cube(GraphicsDevice graphicsDevice, ContentManager content, Vector3 Position, Color color)
        {
            Collider = new OrientedBoundingBox(Position, new Vector3(1, 1, 1));
            Body = new CubePrimitive(graphicsDevice, content, 1, color);
            this.Position = Position;
        }

        public Cube(GraphicsDevice graphicsDevice, ContentManager content, Vector3 Position)
        {
            Collider = new OrientedBoundingBox(Position, new Vector3(1, 1, 1));
            Body = new CubePrimitive(graphicsDevice, content, 1, Color.White);
            this.Position = Position;
        }

        public override void WorldUpdate(Vector3 scale, Vector3 newPosition, Quaternion rotation)
        {
            base.WorldUpdate(scale, newPosition, rotation);
            Collider.Center = newPosition; 
            Collider.Extents = scale/2;
            Collider.Rotate(rotation); 
        }
        public override void WorldUpdate(Vector3 scale, Vector3 newPosition, Matrix rotationMatrix)
        {
            base.WorldUpdate(scale, newPosition, rotationMatrix);
            Collider.Center = newPosition;
            Collider.Extents = scale / 2;
            Collider.Orientation = rotationMatrix;
        }
        public override bool Intersects(Sphere s)
        {
            return Collider.Intersects(s.Collider);
        }
        public override Vector3 GetDirectionFromCollision(Sphere s)
        {
            Matrix m = Matrix.Invert(Collider.Orientation);
            Vector3 v = new Vector3(0, 0, 0);
            float X = Math.Abs(Collider.Extents.X);
            float Y = Math.Abs(Collider.Extents.Y);
            float Z = Math.Abs(Collider.Extents.Z);
            float Xdistance = Collider.Center.X - s.Collider.Center.X;
            float Ydistance = Collider.Center.Y - s.Collider.Center.Y;
            float Zdistance = Collider.Center.Z - s.Collider.Center.Z;
            if (X < Xdistance || -X > Xdistance)
                v += m.Left * Math.Sign(Xdistance);
            if (Y < Ydistance || -Y > Ydistance)
                v += m.Down * Math.Sign(Ydistance);
            if (Z < Zdistance || -Z > Zdistance)
                v += m.Forward * Math.Sign(Zdistance);
            v.Normalize();
            return v;
        }
    }

    public class TrianglePrism : Object
    {

        public TrianglePrism(GraphicsDevice graphicsDevice, ContentManager content, Vector3 Position, Color color)
        {
            Body = new TrianglePrismPrimitive(graphicsDevice, content, 1, color);
            this.Position = Position;
        }

        public TrianglePrism(GraphicsDevice graphicsDevice, ContentManager content, Vector3 Position)
        {
            Body = new TrianglePrismPrimitive(graphicsDevice, content, 1, Color.White);
            this.Position = Position;
        }

        public override bool Intersects(Sphere s)
        {
            return false;
        }
        public override Vector3 GetDirectionFromCollision(Sphere s)
        {
            return Vector3.One;
        }

    }

    public class Sphere : Object
    {

        public BoundingSphere Collider { get; set; }
        public BoundingSphere InitialCollider { get; set; }
        private SpherePrimitive currentBody { get; set; }

        public Sphere(GraphicsDevice graphicsDevice, ContentManager content, float diameter, int tessellation, Color color)
        {
            Collider = new BoundingSphere(Vector3.Zero, 1f);
 
            currentBody = new SpherePrimitive(graphicsDevice, content, diameter, tessellation, color);
            Body = currentBody;
        }

        public Sphere(GraphicsDevice graphicsDevice, ContentManager content, float diameter, int tessellation)
        {
            Collider = new BoundingSphere(Vector3.Zero, 1f);
            Body = new SpherePrimitive(graphicsDevice, content, diameter, tessellation);
        }

        public override void WorldUpdate(Vector3 scale, Vector3 newPosition, Quaternion rotation)
        {
            base.WorldUpdate(scale, newPosition, rotation);
            BoundingSphere collider = Collider;
            collider.Radius = scale.X / 2;
            collider.Center = newPosition;
            Collider = collider;
        }

        public override void WorldUpdate(Vector3 scale, Vector3 newPosition, Matrix rotationMatrix)
        {
            base.WorldUpdate(scale, newPosition, rotationMatrix);
            BoundingSphere collider = Collider;
            collider.Radius = scale.X / 2;
            collider.Center = newPosition;
            Collider = collider;
        }

        public override bool Intersects(Sphere s)
        {
            var boundingSphere = new BoundingSphere(Position, currentBody.diameter/2);
            return boundingSphere.Intersects(s.Collider);
        }
        public override Vector3 GetDirectionFromCollision(Sphere s)
        {
            return Vector3.One;
        }
    }



    public class Cylinder : Object
    {
        public BoundingCylinder Collider { get; set; }
        public Cylinder(GraphicsDevice graphicsDevice, ContentManager content, Color color, float height = 1, float diameter = 1, int tessellation = 32)
        {
            Body = new CylinderPrimitive(graphicsDevice, content, color, height, diameter, tessellation);
            Collider = new BoundingCylinder(Vector3.Zero, height / 2, diameter / 2);
        }

        public override void WorldUpdate(Vector3 scale, Vector3 newPosition, Quaternion rotation)
        {
            base.WorldUpdate(scale, newPosition, rotation);
            Collider.Center = newPosition;
            Collider.HalfHeight *= scale.Y;
            Collider.Radius *= scale.X;
        }

        public override void WorldUpdate(Vector3 scale, Vector3 newPosition, Matrix rotation)
        {
            base.WorldUpdate(scale, newPosition, rotation);
            Collider.Center = newPosition;
            Collider.HalfHeight = scale.Y/2;
            Collider.Radius = scale.X/2;
        }
        public override bool Intersects(Sphere s)
        {
            BoundingCylinder c = new BoundingCylinder(Collider.Center, Collider.Radius, Collider.HalfHeight + s.Collider.Radius);
            return c.Intersects(s.Collider);
        }
        public override Vector3 GetDirectionFromCollision(Sphere s)
        {
            var Ydistance = s.Position.Y - Position.Y;
            if (Ydistance < Collider.HalfHeight && Ydistance > -Collider.HalfHeight)
            {
                Vector3 v = s.Position - Position;
                v.Y = 0;
                v.Normalize();
                return v;
            }
            if (Vector2.Distance(new Vector2(s.Position.X, s.Position.Z), new Vector2(Position.X, Position.Z)) < Collider.Radius)
                return new Vector3(0, -1, 0) * Math.Sign(Ydistance);
            Vector3 v1 = s.Position - Position;
            v1.Y = -1 * Math.Sign(Ydistance);
            v1.Normalize();
            return v1;
        }
    }
    public abstract class LogicalObject : Object
    {
        public bool flagCollide { get; set; } = false;

        public virtual void logicalAction(Player player)
        {
            flagCollide = true;
        }

        public override Vector3 GetDirectionFromCollision(Sphere s)
        {
            return Vector3.One;
        }
        public virtual void destroyItself() {
            World = Matrix.CreateScale(new Vector3(0, 0, 0)) * Matrix.CreateTranslation(Position + new Vector3(0, 100, 0));
            //Collider = new BoundingSphere(new Vector3(0f, 1000f, 0f), 0f);
        }
        public virtual void Restart() { }
    }
    public class LogicalSphere : LogicalObject
    {

        public BoundingSphere Collider { get; set; }
        public BoundingSphere InitialCollider { get; set; }
        public LogicalSphere(GraphicsDevice graphicsDevice, ContentManager content, float diameter, int tessellation, Color color)
        {
            Collider = new BoundingSphere(Position, diameter/2);
            Body = new SpherePrimitive(graphicsDevice, content, diameter, tessellation, color);
        }

        public LogicalSphere(GraphicsDevice graphicsDevice, ContentManager content, float diameter, int tessellation)
        {
            Collider = new BoundingSphere(Position, diameter / 2);
            Body = new SpherePrimitive(graphicsDevice, content, diameter, tessellation);
        }

        public override void WorldUpdate(Vector3 scale, Vector3 newPosition, Quaternion rotation)
        {
            base.WorldUpdate(scale, newPosition, rotation);
            BoundingSphere collider = Collider;
            collider.Radius = scale.X / 2;
            collider.Center = newPosition;
            Collider = collider;
        }

        public override void WorldUpdate(Vector3 scale, Vector3 newPosition, Matrix rotationMatrix)
        {
            base.WorldUpdate(scale, newPosition, rotationMatrix);
            BoundingSphere collider = Collider;
            collider.Radius = scale.X / 2;
            collider.Center = newPosition;
            Collider = collider;
        }

        public override bool Intersects(Sphere player)
        {
           // var BoundingSphere = new BoundingSphere(player.Collider.Center, player.Collider.Radius);
            var playerCenter = player.Collider.Center;
            var playerRadius = player.Collider.Radius;
            return Math.Pow(Vector3.Dot((Collider.Center - playerCenter),(Collider.Center - playerCenter)), 2) <= Math.Pow(Collider.Radius - playerRadius, 2);

        }


    }

    public class LogicalCyllinder : LogicalObject
    {

        public BoundingCylinder Collider { get; set; }
        public BoundingCylinder InitialCollider { get; set; }
        public static Matrix InitialWorld { get; set; }
        public LogicalCyllinder(GraphicsDevice graphicsDevice, ContentManager content, Color color, float height = 1, float diameter = 1, int tessellation = 32)
        {
            Collider = new BoundingCylinder(Position, diameter/2, height);
            Body = new CylinderPrimitive(graphicsDevice, content, color, height, diameter, tessellation);
            this.Position = Position;
        }

        public LogicalCyllinder(GraphicsDevice graphicsDevice, ContentManager content, float height = 1, float diameter = 1, int tessellation = 1)
        {
            Collider = new BoundingCylinder(Position, diameter / 2, height);
            Body = new CylinderPrimitive(graphicsDevice, content, Color.White, height, diameter, tessellation);
            this.Position = Position;
        }

        public override bool Intersects(Sphere player)
        {
            var BoundingSphere = new BoundingSphere(player.Collider.Center, player.Collider.Radius);
            return Collider.Intersects(BoundingSphere);
        }

         public override void destroyItself(){
            World = Matrix.CreateScale(new Vector3(0, 0, 0)) * Matrix.CreateTranslation(Position + new Vector3(0, 100, 0));
            Collider = new BoundingCylinder(Position + new Vector3(0f, 100f, 0f), 0f, 0f);
        }
    }

    public class LogicalCube : LogicalObject
    {
        public OrientedBoundingBox Collider { get; set; }
        public OrientedBoundingBox InitialCollider { get; set; }
        public LogicalCube(GraphicsDevice graphicsDevice, ContentManager content, Vector3 Position, Color color)
        {
            Collider = new OrientedBoundingBox(Position, new Vector3(1, 1, 1));
            Body = new CubePrimitive(graphicsDevice, content, 1, color);
            this.Position = Position;
        }

        public LogicalCube(GraphicsDevice graphicsDevice, ContentManager content, Vector3 Position)
        {
            Collider = new OrientedBoundingBox(Position, new Vector3(1, 1, 1));
            Body = new CubePrimitive(graphicsDevice, content, 1, Color.White);
            this.Position = Position;
        }

        public override void WorldUpdate(Vector3 scale, Vector3 newPosition, Quaternion rotation)
        {
            base.WorldUpdate(scale, newPosition, rotation);
            Collider.Center = newPosition;
            Collider.Extents = scale / 2;
            Collider.Rotate(rotation);
        }
        public override void WorldUpdate(Vector3 scale, Vector3 newPosition, Matrix rotationMatrix)
        {
            base.WorldUpdate(scale, newPosition, rotationMatrix);
            Collider.Center = newPosition;
            Collider.Extents = scale / 2;
            Collider.Rotate(Matrix.Invert(rotationMatrix));
        }
        public override bool Intersects(Sphere s)
        {
            return Collider.Intersects(s.Collider);
        }

        public override Vector3 GetDirectionFromCollision(Sphere s)
        {
            Matrix m = Matrix.Invert(Collider.Orientation);
            Vector3 v = new Vector3(0, 0, 0);
            float X = Math.Abs(Collider.Extents.X);
            float Y = Math.Abs(Collider.Extents.Y);
            float Z = Math.Abs(Collider.Extents.Z);
            float Xdistance = Collider.Center.X - s.Collider.Center.X;
            float Ydistance = Collider.Center.Y - s.Collider.Center.Y;
            float Zdistance = Collider.Center.Z - s.Collider.Center.Z;
            if (X < Xdistance || -X > Xdistance)
                v += m.Left * Math.Sign(Xdistance);
            if (Y < Ydistance || -Y > Ydistance)
                v += m.Down * Math.Sign(Ydistance);
            if (Z < Zdistance || -Z > Zdistance)
                v += m.Forward * Math.Sign(Zdistance);
            v.Normalize();
            return v;
        }
    }

}